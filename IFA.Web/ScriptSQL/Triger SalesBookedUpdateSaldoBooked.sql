-- =============================================
-- Author:		Yayak

-- Description:	utk update Booked dan Update QTY availabke yg bs di jual
-- =============================================

BEGIN
   Declare @kd_stok As varchar(22);
  Declare @kd_cabang As varchar(10);
  Declare @bultah As  Varchar(6);
  Declare @booked_in As numeric(10,4);
  
 Select @kd_cabang = i.Kd_cabang From inserted i;
 Select @kd_stok = i.kd_stok From inserted i;
 Select @booked_in = i.Qty From inserted i;
 select @bultah= SUBSTRING(CONVERT(varchar,GETDATE(),112),1,6)
  
  update INV.dbo.INV_STOK_SALDO 
    set qty_booked_in = qty_booked_in+@booked_in 	
    where bultah = @bultah 
    	and kd_stok = @kd_stok 
        and Kd_Cabang = @kd_cabang 
	
END