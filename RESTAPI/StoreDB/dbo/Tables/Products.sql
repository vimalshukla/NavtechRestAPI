CREATE TABLE [dbo].[Products] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (50)  NOT NULL,
    [Code]          VARCHAR (50)  NOT NULL,
    [Details]       VARCHAR (500) NOT NULL,
    [Price]         FLOAT (53)    NOT NULL,
    [StockQuantity] INT           NOT NULL,
    [StatusId]      INT           NOT NULL,
    [CreatedDate]   DATETIME      NOT NULL,
    [ModifiedDate]  DATETIME      NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC)
);

