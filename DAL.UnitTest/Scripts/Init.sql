SET IDENTITY_INSERT [KLibDBTest].[dbo].[Users] ON;

INSERT INTO [KLibDBTest].[dbo].[Users]
           ([Id]
		   ,[UserName]
           ,[DisplayName]
           ,[Password]
           ,[CreatedDate]
           ,[Status])
     VALUES
	 (1, 'user1', 'User 1', 'password', SYSDATETIME(), 'A'),
	 (2, 'user2', 'User 2', 'password', SYSDATETIME(), 'L');

SET IDENTITY_INSERT [KLibDBTest].[dbo].[Users] OFF;

SET IDENTITY_INSERT [KLibDBTest].[dbo].[Notes] ON;

INSERT INTO [KLibDBTest].[dbo].[Notes]
           ([Id]
		   ,[UserId]
           ,[Text]
           ,[CreatedDate]
           ,[Title]
           ,[Status])
     VALUES
     (1, 1, 'This is the detail of the note 1', SYSDATETIME(), 'Note 1 User 1', 'A'),
	 (2, 2, 'This is the detail of the note 2', SYSDATETIME(), 'Note 2 User 2', 'A'),
	 (3, 2, 'This is the detail of the note 3', SYSDATETIME(), 'Note 3 User 2', 'A'),
	 (4, 1, 'This is the detail of the note 4', SYSDATETIME(), 'Note 4 User 1', 'A'),
	 (5, 1, 'This is the detail of the note 5', SYSDATETIME(), 'Note 5 User 1', 'A'),
	 (6, 2, 'This is the detail of the note 6', SYSDATETIME(), 'Note 6 User 2', 'D'),
	 (7, 2, 'This is the detail of the note 7', SYSDATETIME(), 'Note 7 User 2', 'D'),
	 (8, 2, 'This is the detail of the note 8', SYSDATETIME(), 'Note 8 User 2', 'A');

SET IDENTITY_INSERT [KLibDBTest].[dbo].[Notes] OFF;