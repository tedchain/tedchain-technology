CREATE PROCEDURE [Tedchain].[GetLastTransaction]
    @instance INT
AS
    SET NOCOUNT ON;

    SELECT TOP(1) Transactions.[TransactionHash]
    FROM [Tedchain].[Transactions]
    WHERE Transactions.[Instance] = @instance
    ORDER BY Transactions.[Id] DESC;

RETURN;
