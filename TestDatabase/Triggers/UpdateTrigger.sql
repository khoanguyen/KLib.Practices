CREATE TRIGGER [UpdateTrigger]
	ON [dbo].[Users]
	FOR UPDATE
	AS
	BEGIN
		SET NOCOUNT ON
		UPDATE [dbo].[Users]
		SET [dbo].[Users].[ModifiedAt] = SYSDATETIME()
		FROM [dbo].[Users] u INNER JOIN inserted i ON u.Id = i.Id
	END
