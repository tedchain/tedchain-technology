CREATE TABLE [Tedchain].[Transactions]
(
    [Id] BIGINT NOT NULL IDENTITY,
    [Instance] INT NOT NULL,
    [TransactionHash] BINARY(32) NOT NULL,
    [MutationHash] BINARY(32) NOT NULL,
    [RawData] VARBINARY(MAX) NOT NULL,

    CONSTRAINT [PK_Transactions]
    PRIMARY KEY ([Id]),
)
GO

CREATE NONCLUSTERED INDEX [IX_Transactions_Id]
ON [Tedchain].[Transactions] ([Instance], [Id])
GO

CREATE NONCLUSTERED INDEX [IX_Transactions_TransactionHash]
ON [Tedchain].[Transactions] ([Instance], [TransactionHash])
GO

CREATE NONCLUSTERED INDEX [IX_Transactions_MutationHash]
ON [Tedchain].[Transactions] ([Instance], [MutationHash])
GO