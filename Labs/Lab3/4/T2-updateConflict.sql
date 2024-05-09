use Restaurant
go

SET TRAN ISOLATION LEVEL SNAPSHOT
BEGIN TRAN

WAITFOR DELAY '00:00:05'
BEGIN TRAN

--T1 has now updated and obtained a lock on this table
--trying to update the same row will receive error 3960
UPDATE Customers SET Address = 'Cluj Napoca 2, Romania' WHERE CID = 1
COMMIT TRAN

