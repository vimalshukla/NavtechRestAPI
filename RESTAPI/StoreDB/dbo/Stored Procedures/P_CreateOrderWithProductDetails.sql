

CREATE PROCEDURE [dbo].[P_CreateOrderWithProductDetails]
( 

	@OrderProductsMapping [OrderedProducts] readonly,
	@CustomerId		int,
	@OrderStatusId	int,
	@TotalAmount	int,
	@PaymentModeId	int
)


AS
BEGIN --1
    DECLARE 
    @IsRunSuccess			bit=0,
    @CurrentDate	DateTime=getdate() , 
    @ErrorMessage	nvarchar(2000),
	@OrderId int,
	@InActiveStatusId		int=4,
	@IsValidaData bit=1

	--Check Customer Exist
	If Not Exists(select 1 from CustomerDetails where Id=@CustomerId and StatusId<>@InActiveStatusId)
	Begin 
		SET @ErrorMessage ='Customer does not Exist.'		
		SET @IsValidaData=0
		SELECT @IsRunSuccess Result ,@ErrorMessage ErrorMessage
		RETURN
	End

	--Chaeck Valid Order Status
	If Not Exists(select 1 from OrderStatus where Id=@OrderStatusId)
	Begin 
		SET @ErrorMessage ='Invalid Order Status'
		SET @IsValidaData=0
		SELECT @IsRunSuccess Result ,@ErrorMessage ErrorMessage
		RETURN
	End

	--Chaeck Valid Payment Mode
	If Not Exists(select 1 from PaymentModes where Id=@PaymentModeId )
	Begin 
		SET @ErrorMessage ='Invalid Payment Mode ID'
		SET @IsValidaData=0
		SELECT @IsRunSuccess Result ,@ErrorMessage ErrorMessage
		RETURN
	End


			-- Begin Order Creation.
	DECLARE 
	@ProductCursor cursor,
	@ProductId	int,
	@Quantity	int,
	@RemainingStock int=0,
	@CurrentStock int

BEGIN
    SET @ProductCursor = CURSOR FOR
    select ProductId,Quantity  from @OrderProductsMapping    
    OPEN @ProductCursor 
    FETCH NEXT FROM @ProductCursor 
    INTO  @ProductId,@Quantity

    WHILE @@FETCH_STATUS = 0
    BEGIN
		
	If Not Exists(select 1 from Products where Id=@ProductId and StatusId<>@InActiveStatusId)
		Begin 
			SET @ErrorMessage ='Product does not Exist.'
			SET @IsValidaData=0
			SELECT @IsRunSuccess Result ,@ErrorMessage ErrorMessage
			RETURN
		End
	Else If Exists(Select 1 from Products where Id=@ProductId and StockQuantity>=@Quantity and StatusId!=@InActiveStatusId)
		Begin
				Select @CurrentStock= StockQuantity from Products where Id=@ProductId and StockQuantity>@Quantity and StatusId!=@InActiveStatusId
				Set @RemainingStock=@CurrentStock - @Quantity;

				Update Products Set StockQuantity=@RemainingStock where Id=@ProductId

	End
	Else		
		Begin
				SET @ErrorMessage ='Product is either out of stock or the order quantity cannnot be fulfilled.'
				SET @IsValidaData=0
				RETURN
		End

      FETCH NEXT FROM @ProductCursor 
      INTO  @ProductId,@Quantity
    END; 

    CLOSE @ProductCursor ;
    DEALLOCATE @ProductCursor;
END;

	IF @IsValidaData=1
	Begin

		BEGIN TRANSACTION 
		BEGIN TRY
    
			Insert into  [SimpleStore].[dbo].[OrderDetails] (CustomerId,TotalAmount,[Status],PaymentModeId,CreatedDate,ModifiedDate)
			Values (@CustomerId,@TotalAmount,@OrderStatusId,@PaymentModeId,@CurrentDate,@CurrentDate)

	
	       Select top  1 @OrderId =Id from  [OrderManagement].[dbo].[Order] where CustomerId=@CustomerId and Status!= @InActiveStatusId order by id desc
	       
		   Insert into OrderProductMappings select @OrderId,[ProductId],[Quantity],@CurrentDate,@CurrentDate,1 from @OrderProductsMapping  

		   SET @IsRunSuccess=1; 
			COMMIT TRANSACTION  
 
		END TRY

		BEGIN CATCH
		    
			SET @ErrorMessage ='The following error has occurred:  ' + ERROR_MESSAGE()
			ROLLBACK TRANSACTION 	
				
		END CATCH 
	
	End 
END

 SELECT @IsRunSuccess Result ,@ErrorMessage ErrorMessage