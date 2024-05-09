use Restaurant
go

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN
--inserted value is not visible yet
SELECT * FROM Ingredients
WAITFOR DELAY '00:00:04'
--we can see the inserted value
SELECT * FROM Ingredients
COMMIT TRAN

--DELETE FROM INGREDIENTS WHERE IngredientName = 'Pepper'