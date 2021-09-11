CREATE PROCEDURE [dbo].[sifp_get_no_transx]
(@vkd_bukti varchar(10),@kd_cabang varchar(2), @vno_trans varchar(100) output)
WITH EXEC AS CALLER
AS
BEGIN
begin

  /*** vreset berisi : 'HARI', 'BLN', 'THN', 'ALL'***/
  /*** digunakan untuk reset no sequence berdasarkan parameter vreset***/

  declare @vkd_cabang char(2),
  		  @vkd_departemen varchar(10),
          @vperiode char(8),
          @hexperiode char(4),
          @vnomor  varchar(20),
          @vreset varchar(10),
          @vawalan  varchar(10),
          @vtengah  varchar(10),
          @vakhiran  varchar(10),
          @vlast_create_date date,   
          @vlast_created_by varchar(30),
          @vlast_update_date date,
          @vtgl_trans date,
          @vlast_updated_by varchar(30),
          @vrec_stat char(1)
  declare @vada int,
          @va numeric

  begin
    set @vrec_stat = 'S'
    set @vlast_create_date = (select getdate())
    set @vlast_update_date = (select getdate())
    set @vtgl_trans  = (select getdate())
    set @vlast_created_by = 'BRUN'
    set @vlast_updated_by = 'BRUN'
    set @vkd_departemen   = '01'
	set @vkd_cabang =@kd_cabang
  end

  begin
    select @vreset = reset_periode,
    		@vawalan = awalan,
            @vtengah = tengah,
            @vakhiran  = akhiran
      from sif.dbo.sif_no_trans_param 
     where kd_bukti = @vkd_bukti
     
  end	
   
  begin  
    if @vreset='HARI' 
    	begin
            set @vperiode = (select cast(YEAR(@vtgl_trans) as varchar) + 
            				right( '0' + cast(MONTH(@vtgl_trans) as varchar),2) + 
                            right( '0' + cast(DAY(@vtgl_trans) as varchar),2))
        end
    else if @vreset='BLN' 
    	begin
        	set @vperiode = (select cast(YEAR(@vtgl_trans) as varchar) + 
            				right( '0' + cast(MONTH(@vtgl_trans) as varchar),2))
            if @vkd_bukti='SPSPRT'
            	begin
                set @hexperiode = (select UPPER(RIGHT(sys.fn_varbintohexstr(CONVERT(VARBINARY(1), YEAR(@vtgl_trans)%100)),2)) + 
                	CHAR(MONTH(@vtgl_trans)+64))
                end
      	end
    else if @vreset='THN' 
    	begin
        	set @vperiode = (select cast(YEAR(@vtgl_trans) as varchar))
      	end
    else if @vreset='ALL' 
    	begin
        	set @vperiode = 'ALL'
      	end        
  	end
  end
 
  begin
    select @va = isnull(count(*),0) 
      from sif.dbo.sif_no_trans_param 
     where kd_bukti = @vkd_bukti
    if @va = 0
    begin
      --print 'Hubungi Admin Untuk Isi Parameter No Transaksi'
      --raiserror('Oh no a fatal error', 20, -1) with log      
      RAISERROR (50005, -- Message id.
           10, -- Severity,
           1, -- State,
           N'Hubungi Admin Untuk Isi Parameter No Transaksi')
    end
  end

  begin
  	select @vada = isnull(count(*),0)
  	  from sif.dbo.sif_no_trans 
     where sif_no_trans.kd_bukti = @vkd_bukti
       and sif_no_trans.periode = @vperiode
	   and kd_cabang =@vkd_cabang
  end

 
  if @vada = 0 

      begin
        insert into sif.dbo.sif_no_trans(kd_cabang,					kd_departemen,                    
                                          kd_bukti,					reset,				periode,
                                          awalan,					tengah,				akhiran,
                                          nomor,						
                                          rec_stat,           		last_create_date,   last_created_by,  
                                          last_update_date,			last_updated_by,    program_name)
        						  values (@vkd_cabang,				@vkd_departemen,                    
                                          @vkd_bukti,				@vreset,			@vperiode,
                                          @vawalan,					@vtengah,			@vakhiran,
                                          1,						
                                          @vrec_stat,           	@vlast_create_date,   @vlast_created_by,  
                                          @vlast_update_date,		@vlast_updated_by,    'get_no_trans')
   	  end
  else
      begin
		update sif.dbo.sif_no_trans
           set nomor = nomor + 1
         where kd_bukti = @vkd_bukti
           and periode  = @vperiode and kd_cabang =@vkd_cabang
	  end     
  
  begin
  	select @vperiode = isnull(periode,''),
           @vawalan = isnull(awalan,''),
           @vtengah = isnull(tengah,''),
           @vakhiran = isnull(akhiran,''),
           @vnomor = isnull(nomor,0)
      from sif.dbo.sif_no_trans
     where kd_bukti = @vkd_bukti
       and periode  = @vperiode
	   and kd_cabang =@vkd_cabang
  end
  
  if @vreset = 'ALL'
  	begin
      set @vnomor = (SELECT RIGHT(REPLICATE('0', 5) + CONVERT(VARCHAR, @vnomor), 5))
      set @vno_trans = @vkd_cabang + '/' + @vawalan + @vtengah + @vakhiran +@vnomor
      set @vno_trans = REPLACE(@vno_trans,' ','')
    end
  ELSE
  	begin
      set @vnomor = (SELECT RIGHT(REPLICATE('0', 5) + CONVERT(VARCHAR, @vnomor), 5))
      set @vno_trans =  @vkd_cabang + '/' + @vnomor + @vawalan + @vtengah + @vakhiran +'/'+  @vperiode
      if @vkd_bukti='SPSPRT'
      	begin
	        set @vno_trans = @vkd_cabang + '/' + @vnomor + '/' + @hexperiode + @vawalan + @vtengah + @vakhiran
        end
      set @vno_trans = REPLACE(@vno_trans,' ','')
    end
  
END