use Restaurant
go

INSERT INTO Customers VALUES ('Simona', 'Halep', 'simonahalep@tennis.com', 11111, 'Constanta')
BEGIN TRAN
WAITFOR DELAY '00:00:05'
UPDATE Customers
SET Address = 'BUCHAREST'
WHERE FirstName = 'Simona' and LastName = 'Halep'
COMMIT TRAN