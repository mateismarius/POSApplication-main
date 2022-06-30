CREATE PROCEDURE [dbo].[spLogIn]
	@userId int
AS

BEGIN TRY
	INSERT INTO [dbo].[Loggs] (UserId) VALUES (@userId)
	UPDATE [dbo].[User] SET  IsActive = 1 WHERE Id = @userId 
END TRY

BEGIN CATCH
    
 
-- Transaction uncommittable
    IF (XACT_STATE()) = -1
      ROLLBACK TRANSACTION
 
-- Transaction committable
    IF (XACT_STATE()) = 1
      COMMIT TRANSACTION
  END CATCH