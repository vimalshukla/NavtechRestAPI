CREATE TABLE [dbo].[CustomerDetails] (
    [FullName]     NVARCHAR (100) NOT NULL,
    [Email]        NVARCHAR (50)  NOT NULL,
    [Mobile]       NVARCHAR (20)  NOT NULL,
    [CreatedDate]  DATETIME       NOT NULL,
    [ModifiedDate] DATETIME       NOT NULL,
    [StatusId]     INT            NOT NULL,
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_CustomerDetails] PRIMARY KEY CLUSTERED ([ID] ASC)
);

