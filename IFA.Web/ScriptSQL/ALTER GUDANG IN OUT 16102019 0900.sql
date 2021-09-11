ALTER TABLE [INV].[dbo].[INV_GUDANG_OUT]
ADD [gudang_tujuan] varchar(32) NULL,
[gudang_asal] varchar(32) NULL
GO

ALTER TABLE [INV].[dbo].[INV_GUDANG_IN]
ADD [gudang_tujuan] varchar(32) NULL,
[gudang_asal] varchar(32) NULL
GO
