USE [Test];

if OBJECT_ID(N'Users', N'U') IS NOT NULL
BEGIN
    DROP TABLE [Users];
END

CREATE TABLE [Users]
(
    [Id]   INT          IDENTITY(1, 1) NOT NULL,
    [Name] NVARCHAR(20) CHECK([Name] != N'') NOT NULL,
    [Age]  INT          CHECK(Age >= 0) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
);

GO