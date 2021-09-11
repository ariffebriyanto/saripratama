USE [INV]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [NonClusteredIndex-20191018-133926]    Script Date: 10/18/2019 4:12:09 PM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20191018-133926] ON [dbo].[INV_STOK_SALDO]
(
	[bultah] ASC
)
INCLUDE([kd_stok],[tinggi],[lebar],[panjang],[awal_qty_onstok],[akhir_qty_onstok],[akhir_qty_prod],[akhir_qty_kony],[qty_available],[qty_onstok_in],[qty_onstok_out]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


USE [PROD]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [NonClusteredIndex-20191018-135253]    Script Date: 10/18/2019 4:12:33 PM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20191018-135253] ON [dbo].[PROD_rcn_krm_d]
(
	[no_dpb] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
