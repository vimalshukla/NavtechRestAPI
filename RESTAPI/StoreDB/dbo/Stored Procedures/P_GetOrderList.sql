CREATE PROCEDURE [P_GetOrderList]
 @Page INT,
 @Size INT,
 @Orderby nvarchar(50), 
 @Direction nvarchar(10)
-- @totalrow INT  OUTPUT
AS
BEGIN
    DECLARE @Offset INT
    DECLARE @Newsize INT
    DECLARE @SqlQuery NVARCHAR(MAX)

    IF(@Page=0)
      BEGIN
        SET @Offset = @Page
        SET @Newsize = @Size
       END
    ELSE 
      BEGIN
        SET @Offset = @Page*@Size
        SET @Newsize = @Size-1
      END
    SET NOCOUNT ON
    SET @SqlQuery = '
     WITH OrderedSet AS
    (
      select ord.Id as OrderId,ord.TotalAmount ,cd.FullName as CustomerName,cd.Email,os.Status as OrderStatus,pt.Mode as PaymentMode,ROW_NUMBER() OVER (ORDER BY ord.['+@Orderby+'] ' + @Direction + ') AS ''Index'' from [SimpleStore].[dbo].[OrderDetails] ord join CustomerDetails cd on ord.CustomerId=cd.Id 
join  OrderStatus os on os.Id=ord.Status join PaymentModes pt on pt.Id =ord.PaymentModeId 
    )
	
   SELECT * FROM OrderedSet WHERE [Index] BETWEEN ' + CONVERT(NVARCHAR(12), @Offset) + ' AND ' + CONVERT(NVARCHAR(12), (@Offset + @Newsize)) 
   
   EXECUTE (@SqlQuery)
END