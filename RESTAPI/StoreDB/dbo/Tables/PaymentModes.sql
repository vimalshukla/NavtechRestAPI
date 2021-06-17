CREATE TABLE [dbo].[PaymentModes] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Mode]     VARCHAR (20) NOT NULL,
    [StatusId] INT          NOT NULL,
    CONSTRAINT [PK_PaymentModes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

