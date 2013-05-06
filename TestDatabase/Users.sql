CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(50) NULL, 
    [DisplayName] NVARCHAR(50) NULL, 
    [Password] VARCHAR(50) NULL, 
    [CreatedDate] DATETIME NULL DEFAULT SYSDATETIME(), 
    [Status] CHAR(1) NULL DEFAULT 'A', 
    [ModifiedAt] DATETIME NULL 
)
