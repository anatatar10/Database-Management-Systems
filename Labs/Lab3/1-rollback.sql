use Restaurant

DROP TABLE IF EXISTS LogTable 
CREATE TABLE LogTable(
	Lid INT IDENTITY PRIMARY KEY,
	TypeOperation VARCHAR(50),
	TableOperation VARCHAR(50),
	ExecutionDate DATETIME
);

GO

-- use m:n relation Orders - MenuItems

CREATE OR ALTER FUNCTION ufCheckTotalAmount (@TotalAmount DECIMAL(20,2))
RETURNS int
AS
BEGIN
	DECLARE @return INT
	SET @return = 1
    IF @TotalAmount < 0
		BEGIN
			SET @return = 0
    END
        RETURN @return  
END
GO

CREATE OR ALTER FUNCTION ufCheckPrice (@Price DECIMAL(20,2))
RETURNS int
AS
BEGIN
	DECLARE @return INT
	SET @return = 1
    IF @Price < 0
		BEGIN
			SET @return = 0
    END
        RETURN @return  
END
GO

CREATE or alter FUNCTION ufCheckItemName (@ItemName VARCHAR(100))
RETURNS INT
AS
BEGIN
	DECLARE @return INT
	SET @return = 0  -- Assume invalid by default
    IF LEN(@ItemName) > 0 AND LEN(@ItemName) <= 100
       SET @return = 1  -- Valid
    RETURN @return
END
GO

CREATE OR ALTER PROCEDURE uspAddOrder(@orderDate date, @totalAmount decimal(20,2), @paymentMethod varchar(10), @cid integer, @OID integer OUTPUT)
AS
	SET NOCOUNT ON
	IF (dbo.ufCheckTotalAmount(@totalAmount) <> 1)
	BEGIN
		RAISERROR('Total Amount is invalid', 14, 1)
		RETURN
	END
	INSERT INTO Orders (OrderDate, TotalAmount, PaymentMethod, CID) VALUES (@orderDate, @totalAmount, @paymentMethod, @cid)
	SET @OID = SCOPE_IDENTITY()
	INSERT INTO LogTable (TypeOperation, TableOperation, ExecutionDate) VALUES ('add', 'order', GETDATE())
GO

CREATE OR ALTER PROCEDURE uspAddMenuItems(@itemName varchar(100), @price decimal(20,2), @mitid integer, @MID integer OUTPUT)
AS
	SET NOCOUNT ON
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
GO


CREATE OR ALTER PROCEDURE uspAddOrdersMenuItems(@oid integer, @mid integer, @quantity integer)
AS
	SET NOCOUNT ON
	IF (dbo.ufCheckTotalAmount(@quantity) <> 1)
	BEGIN
		RAISERROR('Quantity is invalid', 14, 1)
	END
	IF EXISTS (SELECT * FROM OrdersMenuItems O where O.OID = @oid AND O.MID = @mid)
	BEGIN
		RAISERROR('OrdersMenuItems already exists', 14, 1)
	END
	INSERT INTO OrdersMenuItems VALUES (@oid, @mid, @quantity)
	INSERT INTO LogTable VALUES ('add', 'Orders Menu Items', GETDATE())
GO

CREATE OR ALTER PROCEDURE uspAddCommitScenario --successful
AS
BEGIN
    DECLARE @OrderID INT, @MenuItemID INT 

    BEGIN TRAN
    BEGIN TRY
        EXEC uspAddOrder '2008-11-11', 208.10, 'card', 2, @OrderID OUTPUT

        EXEC uspAddMenuItems 'pizza', 10, 1, @MenuItemID OUTPUT

        EXEC uspAddOrdersMenuItems @OrderID, @MenuItemID, 30

        COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
        RETURN
    END CATCH
END
GO


CREATE OR ALTER PROCEDURE uspAddRollbackScenario -- unsuccessful
AS
BEGIN
    DECLARE @OrderID INT, @MenuItemID INT 
	BEGIN TRAN
	BEGIN TRY
		EXEC uspAddOrder '2024-10-09', 18.10,'cash',1, @OrderID OUTPUT
		EXEC uspAddMenuItems 'pasta', -10, 2, @MenuItemID OUTPUT
        EXEC uspAddOrdersMenuItems @OrderID, @MenuItemID, 30
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		RETURN
	END CATCH
END
GO

EXEC uspAddRollbackScenario -- unsuccessful
EXEC uspAddCommitScenario

SELECT * FROM LogTable

SELECT * FROM Orders
SELECT * FROM MenuItems
SELECT * FROM OrdersMenuItems

DELETE FROM OrdersMenuItems WHERE Quantity = 30
DELETE FROM Orders WHERE OrderDate = '2008-11-11' 
DELETE FROM MenuItems WHERE ItemName = 'pizza' and Price = 10
