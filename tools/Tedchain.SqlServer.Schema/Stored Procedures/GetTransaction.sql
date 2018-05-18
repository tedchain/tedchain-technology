CREATE PROCEDURE [Tedchain].[GetTransaction]
    @instance INT,
    @mutationHash BINARY(32)
AS
    SET NOCOUNT ON;

    SELECT [RawData]
    FROM [Tedchain].[Transactions]
    WHERE Transactions.[Instance] = @instance AND Transactions.[MutationHash] = @mutationHash;

RETURN;
