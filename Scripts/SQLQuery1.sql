truncate table OAuthTokens

select * from [TimeActivity]
drop table [TimeActivity]
CREATE TABLE [dbo].[TimeActivity] (
[id] [int] IDENTITY(1,1) NOT NULL,
    [RealmId]  INT          NOT NULL,
    [Employee] VARCHAR (50) NULL,
    [Customer] VARCHAR (50) NULL,
    [Item]     VARCHAR (50) NULL,
    [Date]     VARCHAR (50) NULL,
    [Hours]    VARCHAR (50) NULL,
    [QboId]    INT          NULL,
	[Invoice_QboId]    INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)