use Restaurant
go

BEGIN TRAN
--exclusive lock on table Orders
UPDATE Ingredients SET IngredientName = 'TRANSACTION 2' WHERE IngredientName = 'Salt'
WAITFOR DELAY '00:00:10'
--this transaction will be blocked because transaction 1 has already blocked our lock on table Customers, so both transactions are blocked
UPDATE Customers SET Address = 'TRANSACTION 2' WHERE LastName = 'Doe'
COMMIT TRAN
--after some time tran 2 will be chosen as a deadlock victim and terminates with an error
--in tales Customers and Orders will be the values from tran 1