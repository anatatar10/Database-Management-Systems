use Restaurant
go

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN
SELECT * FROM Customers
WAITFOR DELAY '00:00:05'
--see the value before update
SELECT * FROM Customers
COMMIT TRAN

--DELETE FROM CUSTOMERS WHERE LastName = 'Halep'