CREATE TABLE [dbo].[OrderDetails] (
    [Id]            INT        IDENTITY (1, 1) NOT NULL,
    [TotalAmount]   FLOAT (53) NOT NULL,
    [CreatedDate]   DATETIME   NOT NULL,
    [ModifiedDate]  DATETIME   NOT NULL,
    [Status]        INT        NOT NULL,
    [CustomerId]    INT        NOT NULL,
    [PaymentModeId] INT        NOT NULL,
    CONSTRAINT [PK_OrdersDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Order_PaymentMode] FOREIGN KEY ([PaymentModeId]) REFERENCES [dbo].[PaymentModes] ([Id]),
    CONSTRAINT [FK_OrderDeatils_CustomerDetails] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[CustomerDetails] ([ID]),
    CONSTRAINT [FK_OrderDeatils_OrderStatus] FOREIGN KEY ([Status]) REFERENCES [dbo].[OrderStatus] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Order_CustomerDetails]
    ON [dbo].[OrderDetails]([CustomerId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_OrderDeatils_OrderStatus]
    ON [dbo].[OrderDetails]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Order_PaymentType]
    ON [dbo].[OrderDetails]([PaymentModeId] ASC);

