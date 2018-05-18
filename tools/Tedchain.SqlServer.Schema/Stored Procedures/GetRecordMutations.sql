CREATE PROCEDURE [Tedchain].[GetRecordMutations]
    @instance INT,
    @recordKey VARBINARY(256)
AS
    SET NOCOUNT ON;

    SELECT Transactions.[MutationHash]
    FROM [Tedchain].[RecordMutations]
    INNER JOIN [Tedchain].[Transactions] ON RecordMutations.[TransactionId] = Transactions.[Id]
    WHERE RecordMutations.[Instance] = @instance AND RecordMutations.[RecordKey] = @recordKey;

RETURN;
