-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proses_isClosed](@xno_po varchar(25),@no_qc varchar(25))
AS
BEGIN
	 declare @no_po varchar(100)
	 declare @kd_cus varchar(100)
	 declare @kd_stok varchar(100)
	 declare @disc1 varchar(100)
	 declare @disc2 varchar(100)
	 declare @nama_disc varchar(100)
	 declare @kd_merk varchar(100)
	 declare @isClosed varchar(100)
	 declare @no_seq varchar(100)
	 declare @akhire numeric(18,3)
	 declare @temp money
     declare @no integer, @tgl_edk datetime
	  declare @status varchar(100)
	 
     
    select @status= case when sum(pod.qty)<=sum(qd.qty_qc_pass) then 'Y' else 'T' END 
    from PURC.dbo.purc_po po INNER join PURC.dbo.PURC_PO_D pod on po.no_po=pod.no_po
inner JOIN inv.dbo.INV_QC_M q on po.no_po=q.no_ref
inner join INV.dbo.INV_QC qd on q.no_trans=qd.no_trans
where q.no_trans=@no_qc and po.no_po=@xno_po
    -- PRINT @status;
     
     if @status = 'Y'
     begin
     	set @isClosed = 'Y'
     end 
    
     else 
     begin
     	set @isClosed = 'T'
     end
     
     update PURC_PO
     set isclosed=@isClosed
     where no_po=@xno_po
     
     
     

END