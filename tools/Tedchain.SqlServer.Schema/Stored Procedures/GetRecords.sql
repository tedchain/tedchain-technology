CREATE PROCEDURE [Tedchain].[GetRecords]
    @instance INT,
    @ids [Tedchain].[IdTable] READONLY
AS
    SET NOCOUNT ON;

    SELECT [Key], [Value], [Version]
    FROM [Tedchain].[Records]
    INNER JOIN @ids AS Ids ON Records.[Key] = Ids.[Id]
    WHERE Records.[Instance] = @instance;

RETURN;
