CREATE TABLE [dbo].[OrderStatus] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Status] VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);

