TRUNCATE TABLE [KLibDBTest].[dbo].[Notes];

ALTER TABLE [KLibDBTest].[dbo].[Notes]
DROP CONSTRAINT [FK_Notes_Users];

TRUNCATE TABLE [KLibDBTest].[dbo].[Users];

ALTER TABLE [KLibDBTest].[dbo].[Notes]
ADD CONSTRAINT [FK_Notes_Users] FOREIGN KEY(UserId) REFERENCES [KLibDBTest].[dbo].[Users](Id);