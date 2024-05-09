use Restaurant
go

CREATE OR ALTER PROCEDURE uspAddOrderRecovery(@orderDate date, @totalAmount decimal(20,2), @paymentMethod varchar(10), @cid integer, @OID integer OUTPUT)
AS
	SET NOCOUNT ON
	BEGIN TRAN
	BEGIN TRY
		IF (dbo.ufCheckTotalAmount(@totalAmount) <> 1)
		BEGIN
			RAISERROR('Total Amount is invalid', 14, 1)
			RETURN
		END
		INSERT INTO Orders (OrderDate, TotalAmount, PaymentMethod, CID) VALUES (@orderDate, @totalAmount, @paymentMethod, @cid)
		SET @OID = SCOPE_IDENTITY()
		INSERT INTO LogTable (TypeOperation, TableOperation, ExecutionDate) VALUES ('add', 'order', GETDATE())
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
GO

CREATE OR ALTER PROCEDURE uspAddMenuItemsRecover(@itemName varchar(100), @price decimal(20,2), @mitid integer, @MID integer OUTPUT)
AS
	SET NOCOUNT ON
	BEGIN TRAN
	BEGIN TRY
		IF (dbo.ufCheckItemName(@itemName) <> 1)
		BEGIN
			RAISERROR('Item name is invalid', 14, 1)
			RETURN
		END
		IF (dbo.ufCheckPrice(@price) <> 1)
		BEGIN
			RAISERROR('Price is invalid', 14, 1)
			RETURN
		END
		INSERT INTO MenuItems (ItemName, Price, MITID) VALUES (@itemName, @price, @mitid)
		SET @MID = SCOPE_IDENTITY()
		INSERT INTO LogTable (TypeOperation, TableOperation, ExecutionDate) VALUES ('add', 'menu item', GETDATE())
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
GO

CREATE OR ALTER PROCEDURE uspAddOrdersMenuItemsRecover(@oid integer, @mid integer, @quantity integer)
AS
BEGIN
    SET NOCOUNT ON
    BEGIN TRAN
    BEGIN TRY
        IF @oid IS NULL OR @mid IS NULL
        BEGIN
            RAISERROR('Order ID or Menu Item ID is NULL', 16, 1);
            RETURN;
        END

        IF (dbo.ufCheckTotalAmount(@quantity) <> 1)
        BEGIN
            RAISERROR('Quantity is invalid', 14, 1);
            RETURN;
        END
        
        IF EXISTS (SELECT * FROM OrdersMenuItems O WHERE O.OID = @oid AND O.MID = @mid)
        BEGIN
            RAISERROR('OrdersMenuItems already exists', 14, 1);
            RETURN;
        END

        INSERT INTO OrdersMenuItems (OID, MID, Quantity) VALUES (@oid, @mid, @quantity);
        INSERT INTO LogTable (TypeOperation, TableOperation, ExecutionDate) VALUES ('add', 'Orders Menu Items', GETDATE());
        COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        PRINT ERROR_MESSAGE();
    END CATCH
END
GO


CREATE OR ALTER PROCEDURE uspGoodInsertScenario
AS
	BEGIN
		DECLARE @OrderID INT, @MenuItemID INT 
		EXEC uspAddOrderRecovery '2030-04-05', 124.5, 'cash', 2, @OrderID OUTPUT
		EXEC uspAddMenuItemsRecover 'burger', 12.10, 2, @MenuItemID OUTPUT
		EXEC uspAddOrdersMenuItems @OrderID, @MenuItemID, 5
	END
GO

CREATE OR ALTER PROCEDURE uspBadInsertScenario
AS
BEGIN
    DECLARE @OrderID INT, @MenuItemID INT;
    
    EXEC uspAddOrderRecovery '2035-04-05', 100.5, 'card', 1, @OrderID OUTPUT;
    
    BEGIN TRY
        EXEC uspAddMenuItemsRecover 'burger', -12.10, 2, @MenuItemID OUTPUT;  -- This will fail
    END TRY
    BEGIN CATCH
        PRINT 'Failed to add menu item, skipping addition to OrdersMenuItems.';
        RETURN;  -- Stop execution if menu item addition fails
    END CATCH

    EXEC uspAddOrdersMenuItemsRecover @OrderID, @MenuItemID, 5;  -- This should not execute if menu item fails
END
GO


EXEC uspGoodInsertScenario

SELECT * FROM LogTable

SELECT * FROM Orders
SELECT * FROM MenuItems
SELECT * FROM OrdersMenuItems

EXEC uspBadInsertScenario


SELECT * FROM LogTable

SELECT * FROM Orders
SELECT * FROM MenuItems
SELECT * FROM OrdersMenuItems


DELETE FROM OrdersMenuItems WHERE Quantity = 5
DELETE FROM Orders WHERE OrderDate = '2030-04-05' or OrderDate = '2035-04-05'
DELETE FROM MenuItems WHERE ItemName = 'burger' and Price = 12.10
