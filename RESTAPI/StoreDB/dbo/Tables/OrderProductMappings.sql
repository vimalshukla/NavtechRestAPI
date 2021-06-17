CREATE TABLE [dbo].[OrderProductMappings] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [OrderId]      INT      NOT NULL,
    [ProductId]    INT      NOT NULL,
    [Quantity]     INT      NOT NULL,
    [CreatedDate]  DATETIME NOT NULL,
    [ModifiedDate] DATETIME NOT NULL,
    [StatusId]     INT      NOT NULL,
    CONSTRAINT [PK_OrderProductMappings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderProductMapping_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[OrderDetails] ([Id]),
    CONSTRAINT [FK_OrderProductMapping_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_OrderProductMapping_Order]
    ON [dbo].[OrderProductMappings]([OrderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_OrderProductMapping_Product]
    ON [dbo].[OrderProductMappings]([ProductId] ASC);

