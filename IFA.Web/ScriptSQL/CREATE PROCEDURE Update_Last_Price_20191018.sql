-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Update_last_Price 
	-- Add the parameters for the stored procedure here
	@no_po varchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;
	DECLARE @kd_stok VARCHAR(22)
	DECLARE @harga DECIMAL(18,2)

	DECLARE db_cursor CURSOR FOR 
		SELECT kd_stok, harga 
		FROM PURC_PO_D 
		WHERE no_po=@no_po 

		OPEN db_cursor  
		FETCH NEXT FROM db_cursor INTO @kd_stok, @harga  

		WHILE @@FETCH_STATUS = 0  
		BEGIN  
			  if exists(Select 1 From PURC_PO_D_PRICE Where kd_stok = @kd_stok)
				  BEGIN
					update PURC_PO_D_PRICE set last_price = @harga, no_po=@no_po where kd_stok = @kd_stok
				   END
				ELSE
				  BEGIN
					   INSERT INTO PURC_PO_D_PRICE(kd_stok, no_po, last_price)
					   VALUES(@kd_stok, @no_po, @harga)
				   END

			  FETCH NEXT FROM db_cursor INTO @kd_stok, @harga 
		END 

		CLOSE db_cursor  
	DEALLOCATE db_cursor 
END
GO


--EXEC Update_last_Price '1/00002/POM/0/20191017'