use Restaurant
go

--SELECT * FROM Customers
--SELECT * FROM Ingredients

BEGIN TRAN
--exclusive lock on table Customers
UPDATE Customers SET Address = 'TRANSACTION 1' WHERE LastName = 'Doe'
WAITFOR DELAY '00:00:10'
--this transaction will be blocked because transaction 2 has already blocked our lock on table Orders
UPDATE Ingredients SET IngredientName = 'TRANSACTION 1' WHERE IngredientName = 'Salt'
COMMIT TRAN

--UPDATE Customers SET Address = 'TG MURES' WHERE LastName = 'Doe'
--UPDATE Ingredients SET IngredientName = 'Salt' WHERE IID = 1