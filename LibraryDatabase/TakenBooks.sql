CREATE TABLE [dbo].[TakenBooks]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [BookId] INT NOT NULL, 
    [IsReturned] BIT NOT NULL
)
