use Restaurant
go

-- set deadlock priority to high for transaction 2
-- tran 1 will be chosen as deadlock victim, as it has lower priority

SET DEADLOCK_PRIORITY HIGH 
BEGIN TRAN
--exclusive lock on table Orders
UPDATE Ingredients SET IngredientName = 'TRANSACTION 2' WHERE IngredientName = 'Salt'
WAITFOR DELAY '00:00:10'
UPDATE Customers SET Address = 'TRANSACTION 2' WHERE LastName = 'Doe'
COMMIT TRAN
--after some time tran 1 will be chosen as a deadlock victim and terminates with an error
--in tales Customers and Orders will be the values from tran 2 (this tran)

--UPDATE Customers SET Address = 'TG MURES' WHERE LastName = 'Doe'
--UPDATE Ingredients SET IngredientName = 'Salt' WHERE IID = 1