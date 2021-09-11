using Dapper;
using ERP.Api.Utils;
using IFA.Domain.Helpers;
using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Api.Repositories
{
    public class HelperRepo
    {
        public static async Task<string> GetNoTrans(string prefix, DateTime transdate, string kdcabang)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters _params = new DynamicParameters();
                _params.Add("@vkd_bukti", prefix, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@vtgl_trans", transdate, dbType: DbType.Date, direction: ParameterDirection.Input);
                _params.Add("@kd_cabang", kdcabang, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@vno_trans", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
                var result = con.Execute("[dbo].[sifp_get_no_trans]", _params, null, null, CommandType.StoredProcedure);
                var retVal = _params.Get<string>("@vno_trans");


                // var res = con.Query<PURC_PO>(sql, param);

                return retVal;
            }
        }
        public static async Task<string> GetNoTransNew(string prefix, DateTime transdate, string kdcabang, SqlTransaction trans, SqlConnection conn)
        {
            DynamicParameters _params = new DynamicParameters();
            _params.Add("@vkd_bukti", prefix, dbType: DbType.String, direction: ParameterDirection.Input);
            _params.Add("@vtgl_trans", transdate, dbType: DbType.Date, direction: ParameterDirection.Input);
            _params.Add("@kd_cabang", kdcabang, dbType: DbType.String, direction: ParameterDirection.Input);
            _params.Add("@vno_trans", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
            var result = conn.Execute("[dbo].[sifp_get_no_trans]", _params, trans, null, CommandType.StoredProcedure);
            var retVal = _params.Get<string>("@vno_trans");


            // var res = con.Query<PURC_PO>(sql, param);

            return retVal;
        }
        public static async Task<string> GetNoTransx(string prefix, string kdcabang)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters _params = new DynamicParameters();
                _params.Add("@vkd_bukti", prefix, dbType: DbType.String, direction: ParameterDirection.Input);
                //_params.Add("@vtgl_trans", transdate, dbType: DbType.Date, direction: ParameterDirection.Input);
                _params.Add("@kd_cabang", kdcabang, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@vno_trans", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
                var result = con.Execute("[dbo].[sifp_get_no_transx]", _params, null, 36000, CommandType.StoredProcedure);
                var retVal = _params.Get<string>("@vno_trans");


                // var res = con.Query<PURC_PO>(sql, param);

                return retVal;
            }
        }
        public static string GenBlthn()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select SUBSTRING(CONVERT(varchar,GETDATE(),112),1,6)as blthn";
                var res = con.Query<string>(sql).FirstOrDefault();
                return res;
            }
        }
        public static async Task<IEnumerable<DashboardVM>> GetStatPO()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select c.Nama_Gudang as desc1,  COUNT(p.no_po) as total from PURC.dbo.PURC_PO p  WITH(NOLOCK) " +
 " LEFT join SIF.dbo.SIF_Gudang c  WITH(NOLOCK) on c.Kode_Gudang = p.gudang_tujuan " +
 " where p.status_po = 'OPEN' group by c.Nama_Gudang";
                var res = con.Query<DashboardVM>(sql);

                return res;
            }
        }

        public static async Task<IEnumerable<DashboardVM>> GetStatMTS()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = " SELECT c.nama as desc1,sg2.Nama_Gudang desc2,count(no_trans) as total " +
  "FROM[INV].[dbo].[INV_GUDANG_OUT] g WITH(NOLOCK) " +
  "inner join SIF.dbo.SIF_cabang c  WITH(NOLOCK) on c.kd_cabang = g.Kd_Cabang " +
  "LEFT join SIF.dbo.SIF_Gudang sg2   WITH(NOLOCK) on sg2.Kode_Gudang = g.gudang_tujuan " +
  "where isnull(g.rec_stat,'Y')<> 'N' AND g.tipe_trans = 'JPB-KUT-02' " +
  "and g.sudah_sj <> 1 and g.tgl_trans < DATEADD(DAY, -7, GETDATE()) " +
  "group by c.nama ,sg2.Nama_Gudang  order by total DESC";
                var res = con.Query<DashboardVM>(sql);

                return res;
            }
        }

        public static async Task<IEnumerable<DashboardVM>> GetStatPIU()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select 'Customer' as desc1, count (c.Kd_Customer) as total from SIF.dbo.SIF_Customer c where c.saldo_limit >= 100000 and c.saldo_limit > 1  ";
                var res = con.Query<DashboardVM>(sql);

                return res;
            }
        }
        public static async Task<IEnumerable<DashboardVM>> GetStatDUE()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select 'Customer' as desc1  ,0 as total";
                var res = con.Query<DashboardVM>(sql);

                return res;
            }
        }

        public static async Task<IEnumerable<DashboardVM>> GetStatBooked()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT top 20 g.Kd_Cabang as desc1,g.bultah desc2,b.Nama_Barang desc3, g.akhir_booked total    " +
   " FROM[INV].[dbo].[INV_STOK_SALDO] g WITH(NOLOCK) " +
   " inner join SIF.dbo.SIF_Barang b  WITH(NOLOCK) on g.kd_stok = b.Kode_Barang " +
   " where g.bultah = (select SUBSTRING(CONVERT(varchar, GETDATE(), 112), 1, 6) as blthn ) " +
   " order by g.akhir_booked desc   ";
                var res = con.Query<DashboardVM>(sql);

                return res;
            }
        }
        public static string GetNoInv(string refNo)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT no_inv FROM [FIN].[dbo].[FIN_NOTA] WITH(NOLOCK) WHERE no_ref1=@no_ref1";
                DynamicParameters param = new DynamicParameters();
                param.Add("@no_ref1", refNo);
                var res = con.Query<string>(sql, param).FirstOrDefault();
                return res;
            }
        }

        public static string CekBO(string kdstok)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select count(1) from sales.dbo.sales_booked where kd_stok=@kd_stok";
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_stok", kdstok);
                var res = con.Query<string>(sql, param).FirstOrDefault();
                return res;
            }
        }

        public static string CekSaldo(string kdstok, string bultah)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select top 1 isnull([qty_available],0)-isnull((select qty from sales.dbo.sales_booked where kd_stok=@kd_stok),0) from [INV].[dbo].[INV_STOK_SALDO] where kd_stok=@kd_stok and bultah=@bultah";
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_stok", kdstok);
                param.Add("@bultah", bultah);
                var res = con.Query<string>(sql, param).FirstOrDefault();
                return res;
            }
        }
        public static string GetNoJur(string refNo)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT no_jur FROM [FIN].[dbo].[FIN_JURNAL] WITH(NOLOCK) WHERE no_ref1=@no_ref1";
                DynamicParameters param = new DynamicParameters();
                param.Add("@no_ref1", refNo);
                var res = con.Query<string>(sql, param).FirstOrDefault();
                return res;
            }
        }
        public static IEnumerable<SIF_Gudang> GetGudang()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select Kd_Cabang, Kode_Gudang,Nama_Gudang from [SIF].[dbo].[SIF_Gudang] where Kode_Gudang not in ('00000','EXP01') ";
                var res = con.Query<SIF_Gudang>(sql);

                return res;
            }
        }
        public static IEnumerable<Periode_Buku> GetPeriodeBuku()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT thn_buku + bln_buku AS 'thnbln', nama_bulan + ' ' + thn_buku as 'nama' FROM SIF.dbo.SIF_Periode_Buku";
                var res = con.Query<Periode_Buku>(sql);

                return res;
            }
        }
        public static IEnumerable<CustomerVM> GetCustomer()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select Nama_Customer+' '+no_telepon1 as Nama_Customer,nama_customer2,no_telepon1, Kd_Customer, Alamat1,Kota1,Alamat2,Kota2, Kd_Wilayah , Kode_Area, limit_piutang_rupiah CreditLimit,isnull(sts_group,'T') sts_group from SIF.dbo.SIF_Customer where rec_stat='Y' and Kd_Customer <> 'INTERNAL' order by Nama_Customer";
                var res = con.Query<CustomerVM>(sql);

                return res;
            }
        }

        public static IEnumerable<PaketVM> GetPaket()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = " select no_paket,nama_paket from SIF_BOOKING_PAKET where CONVERT(VARCHAR, GETDATE(), 23) <= Tgl_Akhir_Paket order by nama_paket";
                var res = con.Query<PaketVM>(sql);

                return res;
            }
        }

        public static async Task<IEnumerable<PaketVMList>> GetPaketList(string no_paket)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                string sql = " select b.*, b.harga * b.qty as total from SIF_BOOKING_PAKET a inner join SIF_BOOKING_PAKET_D b on a.no_paket = b.no_paket and a.kd_cabang = b.kd_cabang  where CONVERT(VARCHAR, GETDATE(), 23) <= a.Tgl_Akhir_Paket and b.no_paket=@nopaket order by a.nama_paket";
                param.Add("@nopaket", no_paket);
                var res = con.Query<PaketVMList>(sql,param);

                return res;
            }
        }


        public static async Task<IEnumerable<AuthVM>> GetAuthOTP(string password)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                //string sql = "select b.Kd_Satuan, b.Nama_Barang+'| Rp'+ CAST(h.Harga_Rupiah as varchar(16))+'| '+ CAST(isnull([qty_available],0) as varchar(12))   as Nama_Barang  , h.Harga_Dollar ,h.Harga_Rupiah,h.harga_rupiah2, " +
                //string sql = "select b.berat as vol, b.Kd_Satuan, b.Nama_Barang , h.Harga_Dollar ,h.Harga_Rupiah,h.harga_rupiah2,  " +
                //    " h.harga_rupiah3,h.harga_rupiah4,b.Kode_Barang , case B.kd_jenis when '038' then 1 when '039' then 1 else 0 end as isset, isnull([qty_available],0) AS stok,  " +
                //    "  h.qty_harga1_min as  batas1, h.qty_harga2_min as batas2, h.qty_harga3_min as batas3  " +
                //    " from SIF.dbo.SIF_Barang as b " +
                //    " inner join sif.dbo.sif_harga as h on b.Kode_Barang=h.Kode_Barang and h.Kd_Cabang=@kdcabang " +
                //    " LEFT OUTER JOIN [INV].[dbo].[INV_STOK_SALDO] S ON H.Kode_Barang=S.kd_stok " +
                //    " inner join ( select kode_barang, max(start_date) start_date from sif.dbo.sif_harga group by kode_barang ) h2 " +
                //    " on h2.Kode_Barang=h.Kode_Barang and h2.Start_Date = h.Start_Date  where b.rec_stat='Y' and b.kd_jns_persd in ('1','2','3')  and getdate() >= h.start_date  " +
                //    " and s.Kd_Cabang=@kdcabang " +
                //    "GROUP BY s.bultah, b.berat, b.Kd_Satuan, b.Nama_Barang , h.Harga_Dollar ,h.Harga_Rupiah,h.harga_rupiah2,   h.harga_rupiah3,h.harga_rupiah4,b.Kode_Barang , B.kd_jenis , [qty_available],    h.qty_harga1_min, h.qty_harga2_min , h.qty_harga3_min " +
                //    " HAVING s.bultah = (select TOP 1 SUBSTRING(CONVERT(varchar, max(s.bultah), 112), 1, 6) as blthn) " +
                //    " order by Nama_Barang";
                string sql = "select Val_kode1 as [Password] from SIF_Gen_Reff_D where Ref_role='SIF' and id_ref_file='OTP' AND id_ref_data='OTP'" +
                    "and Val_kode1=@otp ";
                param.Add("@otp", password);
               

                var res = con.Query<AuthVM>(sql, param);

                return res;
            }
        }
        public static async Task<IEnumerable<BarangHargaVM>> GetHargaBarang(string kdcabang)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                //string sql = "select b.Kd_Satuan, b.Nama_Barang+'| Rp'+ CAST(h.Harga_Rupiah as varchar(16))+'| '+ CAST(isnull([qty_available],0) as varchar(12))   as Nama_Barang  , h.Harga_Dollar ,h.Harga_Rupiah,h.harga_rupiah2, " +
                //string sql = "select b.berat as vol, b.Kd_Satuan, b.Nama_Barang , h.Harga_Dollar ,h.Harga_Rupiah,h.harga_rupiah2,  " +
                //    " h.harga_rupiah3,h.harga_rupiah4,b.Kode_Barang , case B.kd_jenis when '038' then 1 when '039' then 1 else 0 end as isset, isnull([qty_available],0) AS stok,  " +
                //    "  h.qty_harga1_min as  batas1, h.qty_harga2_min as batas2, h.qty_harga3_min as batas3  " +
                //    " from SIF.dbo.SIF_Barang as b " +
                //    " inner join sif.dbo.sif_harga as h on b.Kode_Barang=h.Kode_Barang and h.Kd_Cabang=@kdcabang " +
                //    " LEFT OUTER JOIN [INV].[dbo].[INV_STOK_SALDO] S ON H.Kode_Barang=S.kd_stok " +
                //    " inner join ( select kode_barang, max(start_date) start_date from sif.dbo.sif_harga group by kode_barang ) h2 " +
                //    " on h2.Kode_Barang=h.Kode_Barang and h2.Start_Date = h.Start_Date  where b.rec_stat='Y' and b.kd_jns_persd in ('1','2','3')  and getdate() >= h.start_date  " +
                //    " and s.Kd_Cabang=@kdcabang " +
                //    "GROUP BY s.bultah, b.berat, b.Kd_Satuan, b.Nama_Barang , h.Harga_Dollar ,h.Harga_Rupiah,h.harga_rupiah2,   h.harga_rupiah3,h.harga_rupiah4,b.Kode_Barang , B.kd_jenis , [qty_available],    h.qty_harga1_min, h.qty_harga2_min , h.qty_harga3_min " +
                //    " HAVING s.bultah = (select TOP 1 SUBSTRING(CONVERT(varchar, max(s.bultah), 112), 1, 6) as blthn) " +
                //    " order by Nama_Barang";
                    string sql = "select b.berat as vol, b.Kd_Satuan, b.Nama_Barang , h.Harga_Dollar ,h.Harga_Rupiah,h.harga_rupiah2,  " +
                       " h.harga_rupiah3,h.harga_rupiah4,b.Kode_Barang , case B.kd_jenis when '038' then 1 when '039' then 1 else 0 end as isset, isnull([QTY_AVAILABLE],0) AS stok,  " +
                       "  h.qty_harga1_min as  batas1, h.qty_harga2_min as batas2, h.qty_harga3_min as batas3  " +
                       " from SIF.dbo.SIF_Barang as b WITH(NOLOCK) " +
                       " inner join sif.dbo.sif_harga as h WITH(NOLOCK) on b.Kode_Barang=h.Kode_Barang and h.Kd_Cabang='" + @kdcabang + "' " +
                       " LEFT OUTER JOIN [INV].[dbo].[INV_STOK_SALDO] S WITH(NOLOCK) ON H.Kode_Barang=S.kd_stok " +
                       " inner join ( select kode_barang, max(start_date) start_date from sif.dbo.sif_harga group by kode_barang ) h2 " +
                       " on h2.Kode_Barang=h.Kode_Barang and h2.Start_Date = h.Start_Date  where b.rec_stat='Y' and b.kd_jns_persd in ('1','2','3')  and getdate() >= h.start_date  " +
                       " and s.Kd_Cabang='" + @kdcabang + "' and s.bultah=(select SUBSTRING(CONVERT(varchar,GETDATE(),112),1,6) as blthn)" +
                       " order by Nama_Barang";
                param.Add("@kdcabang", kdcabang);
               

                var res = con.Query<BarangHargaVM>(sql, param);

                return res;
            }
        }
        public static async Task<IEnumerable<BarangHargaVM>> GetHargaBarangMobile(string Kode_Barang)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                //string sql = "select b.Kd_Satuan, b.Nama_Barang+'| Rp'+ CAST(h.Harga_Rupiah as varchar(16))+'| '+ CAST(isnull([qty_available],0) as varchar(12))   as Nama_Barang  , h.Harga_Dollar ,h.Harga_Rupiah,h.harga_rupiah2, " +
                string sql = "select DISTINCT C.nama AS nama_cabang, b.Kd_Satuan, b.Nama_Barang , h.Harga_Dollar ,h.Harga_Rupiah,h.harga_rupiah2,   h.harga_rupiah3,h.harga_rupiah4,b.Kode_Barang " +
                        " from SIF.dbo.SIF_Barang as b " +
                        " inner join sif.dbo.sif_harga as h on b.Kode_Barang = h.Kode_Barang " +
                        " INNER JOIN SIF.dbo.SIF_CABANG C ON H.KD_CABANG = B.Kd_Cabang " +
                        " inner join(select kode_barang, max(start_date) start_date from sif.dbo.sif_harga group by kode_barang) h2 on h2.Kode_Barang = h.Kode_Barang and h2.Start_Date = h.Start_Date " +
                        " where b.rec_stat = 'Y' and b.kd_jns_persd in ('1', '2', '3')  and getdate() >= h.start_date AND B.Kode_Barang=@Kode_Barang" +
                        " order by Nama_Barang, nama";
                param.Add("@Kode_Barang", Kode_Barang);

                var res = con.Query<BarangHargaVM>(sql, param);

                return res;
            }
        }
        public static async Task<IEnumerable<KasirVM>> GetKasir()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select Nama_Sales, Kode_Sales, Kode_Pegawai from SIF.dbo.SIF_Sales where rec_stat='Y'";
                var res = con.Query<KasirVM>(sql);

                return res;
            }
        }
        public static async Task<IEnumerable<Kendaraan>> GetKendaraan()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select Kode_Kendaraan, Nama_Kendaraan from SIF.dbo.SIF_Kendaraan where rec_stat='Y'";
                var res = con.Query<Kendaraan>(sql);

                return res;
            }
        }
        public static async Task<IEnumerable<SIF_PersediaanCbo>> GetSIFBukuBesarCbo()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select kd_buku_besar as rek_persediaan,nm_buku_besar as nm_rek_persediaan from SIF.dbo.SIF_buku_besar where rec_stat='Y'";
                var res = con.Query<SIF_PersediaanCbo>(sql);

                return res;
            }
        }
        public static async Task<IEnumerable<rek_penjualan2Cbo>> Getrek_penjualan2Cbo()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select kd_buku_besar as rek_penjualan2,nm_buku_besar as nm_rek_penjualan2 from SIF.dbo.SIF_buku_besar where rec_stat='Y'";
                var res = con.Query<rek_penjualan2Cbo>(sql);

                return res;
            }
        }
        public static async Task<int> UpdatePasswordUser(MUSER data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [SIF].[dbo].[MUSER] SET passwd=@passwd WHERE userid=@userid;";
            param = new DynamicParameters();
            param.Add("@userid", data.userid);
            param.Add("@passwd", data.passwd);
            
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateCabang(MUSER data, SqlTransaction trans, SqlConnection conn)
        {

            DynamicParameters param = new DynamicParameters();
            param.Add("@userid", data.userid);
            param.Add("@kd_cabang", data.kd_cabang);

            return conn.Execute("[SIF].[dbo].[SP_PINDAH_CABANG]", param, trans, null, CommandType.StoredProcedure);
                        
        }
        public static async Task<int> DeleteMUSER(string userid, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE [SIF].[dbo].[MUSER]  WHERE nama=@userid;";
            param = new DynamicParameters();
            param.Add("@userid", userid);
          

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<string> GetGudangFromCabang(string kd_cabang)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select Kode_Gudang from [SIF].[dbo].[SIF_Gudang] WHERE kd_cabang=@kd_cabang";
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                var res = con.Query<string>(sql, param).FirstOrDefault();
                return res;
            }
        }

        public static async Task<List<SIF_Gudang>> GetGudangDefaultByCabang(string kd_cabang)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select * from [SIF].[dbo].[SIF_Gudang] WHERE kd_cabang=@kd_cabang AND DIVISI='1 '";
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                var res = con.Query<SIF_Gudang>(sql, param);
                return res.ToList();
            }
        }

        public static string GetNamaGudang(string kd_cabang)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select Nama_Gudang from [SIF].[dbo].[SIF_Gudang] WHERE kd_cabang=@kd_cabang";
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                var res = con.Query<string>(sql, param).FirstOrDefault();
                return res;
            }
        }
        public static async Task<string> GetKdCbFromNmGudang(string nm_gudang)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select kd_cabang from [SIF].[dbo].[SIF_Gudang] WHERE Nama_Gudang=@nm_gudang";
                DynamicParameters param = new DynamicParameters();
                param.Add("@nm_gudang", nm_gudang);
                var res = con.Query<string>(sql, param).FirstOrDefault();
                return res;
            }
        }
        public static async Task<string> GetCabangFromGudang(string kd_gudang)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select kd_cabang from [SIF].[dbo].[SIF_Gudang] WHERE Kode_Gudang=@kd_gudang";
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_gudang", kd_gudang);
                var res = con.Query<string>(sql, param).FirstOrDefault();
                return res;
            }
        }
        public static string GetKdBarang()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT MAX(CAST([Kode_Barang] as int)) + 1 FROM [SIF].[dbo].[SIF_Barang]";
                var res = con.Query<string>(sql).FirstOrDefault();
                return res;
            }
        }
        public static async Task<int> SPKalkulasi(KalkulasiStokVM data, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                // param.Add("@kdcabang", data.Kd_Cabang);
                param.Add("@kdstok", data.kd_stok);
                param.Add("@kd_cabang", data.kd_cabang);
                param.Add("@tahbul", data.blthn);
                param.Add("@LanjutBulanDepan", 1);

                return conn.Execute("[INV].[dbo].[hitungulangStokSaldo2]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> UpdateGranTotalSO(string kdcb, string no_sp, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = " update s set s.JML_RP_TRANS = sd.jml + ISNULL(s.Biaya,0), s.JML_VALAS_TRANS = sd.jml + ISNULL(s.Biaya,0)"+
"  FROM SALES.dbo.SALES_SO s " +
" left join(select No_sp, sum (((isnull(harga,0)*Qty) - (Qty*isnull(potongan, 0)))) as jml from sales.dbo.SALES_SO_D GROUP BY NO_SP ) sd " +
" on s.No_sp = sd.No_sp " +
 "where s.Kd_Cabang = @kdcb and s.No_sp =@no_sp ";
            param = new DynamicParameters();
            param.Add("@kdcb", kdcb);
            param.Add("@no_sp", no_sp);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> ResetBooking(KalkulasiStokVM data,SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();

            string filter = "";
            Query = " update INV.dbo.inv_stok_saldo set qty_booked_in=0 ,qty_booked_out=0, qty_in_expedisi=0,qty_out_expedisi=0 ";
            param = new DynamicParameters();
            param.Add("@kdcb", data.kd_cabang);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@blthn", data.blthn);

            if (data.kd_stok != string.Empty && data.kd_stok != null)
            {

                filter += " where Kd_Cabang=@kdcb and kd_stok=@kd_stok and bultah=@blthn ";
            }
            else
            {
                filter += " where Kd_Cabang=@kdcb and bultah=@blthn ";
            }
            Query += filter;
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> KalkulasiSTOK(KalkulasiStokVM data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();

            string filter = "";
            Query = " update s " +
            "set s.qty_onstok_in = isnull(v.qty_in, 0), " +
            "s.qty_onstok_out = isnull(v.qty_out, 0) " +
            "FROM " +
            "(select g.Kode_Gudang as gudang, SD.kd_stok, SD.bultah, SD.qty_onstok_in, SD.qty_onstok_out from inv.dbo.INV_STOK_SALDO SD " +
             "INNER JOIN SIF.DBO.SIF_GUDANG G ON SD.KD_CABANG = G.KD_CABANG AND " +
            "g.Kode_Gudang = @kd_gudang and SD.bultah = @blthn) s " +
             "left JOIN " +
            "(select gudang, kd_stok, bultah, sum(qty_in) qty_in, sum(qty_out) qty_out from AMERICAN_REPORT.DBO.[vy_saldocard] " +
            "where gudang = @kd_gudang and bultah = @blthn " +
             "group by kd_stok, bultah, gudang) v " +
             " on s.kd_stok = v.kd_stok and s.bultah = v.bultah and s.gudang = v.gudang " +
            "where s.gudang = @kd_gudang and s.bultah = @blthn ";
 //--and s.kd_stok = '21014'  ";
            param = new DynamicParameters();
            param.Add("@kd_gudang", data.kd_gudang);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@blthn", data.blthn);

            if (data.kd_stok != string.Empty && data.kd_stok != null)
            {

                filter += " and s.kd_stok=@kd_stok ";
            }
           
            Query += filter;
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }


        public static async Task<int> SPBooked_in(string kd_cabang, string blthn, string kd_stok, string kd_satuan, decimal qty, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                param.Add("@bultah", blthn);
                param.Add("@kd_stok", kd_stok);
                param.Add("@kd_satuan", kd_satuan);
                param.Add("@booked_in", qty);
       

                return await conn.ExecuteAsync("[INV].[dbo].[inv_stok_saldo_booked_in]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }
        public static async Task<int> SP_rilis_booked(string kd_cabang, string blthn, string kd_stok, string kd_satuan, decimal qty_out, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                param.Add("@bultah", blthn);
                param.Add("@kd_stok", kd_stok);
                param.Add("@kd_satuan", kd_satuan);
                param.Add("@booked_out", qty_out);
                return await conn.ExecuteAsync("[INV].[dbo].[sp_rilis_booked_out]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> SPBooked_out(string kd_cabang, string blthn, string kd_stok, string kd_satuan, decimal qty_out, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                param.Add("@bultah", blthn);
                param.Add("@kd_stok", kd_stok);
                param.Add("@kd_satuan", kd_satuan);
                param.Add("@booked_out", qty_out);
                return await conn.ExecuteAsync("[INV].[dbo].[inv_stok_saldo_booked_out]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> SPExpdc_out(string kd_cabang, string blthn, string kd_stok, decimal qty_out, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                param.Add("@bultah", blthn);
                param.Add("@kd_stok", kd_stok);
            
                param.Add("@qty_out", qty_out);
                return await conn.ExecuteAsync("[INV].[dbo].[inv_stok_expdc_out]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }
        public static async Task<IEnumerable<SIF_Gen_Reff_D>> GetJnsBayar()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select Id_Data,Desc_Data from sif.dbo.SIF_Gen_Reff_D where Id_Ref_data='JNSBYR' and Id_Data <> '00' ";
                var res = con.Query<SIF_Gen_Reff_D>(sql);

                return res;
            }
        }

        public static async Task<IEnumerable<SIF_Gen_Reff_D>> GetJnsJurnal()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select * from SIF.dbo.SIF_Gen_Reff_D where Id_Ref_Data = 'JNSJUR' ";
                var res = con.Query<SIF_Gen_Reff_D>(sql);

                return res;
            }
        }

        public static IEnumerable<SIF_Cabang> GetCabang(string kdcb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select TOP 1 * from [SIF].[dbo].[SIF_cabang] order by kd_cabang ";
                var res = con.Query<SIF_Cabang>(sql);

                return res;
            }
        }

        public static IEnumerable<SIF_Cabang> GetCabangALL()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select kd_cabang,nama from [SIF].[dbo].[SIF_cabang] where rec_stat='Y' order by kd_cabang ";
                var res = con.Query<SIF_Cabang>(sql);

                return res;
            }
        }
        public static async Task<IEnumerable<SIF_buku_besar>> GetRekKas()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT kd_buku_besar, nm_buku_besar FROM SIF.dbo.SIF_buku_besar WITH (NOLOCK) " +
                                                     "WHERE grup_header='D' AND div1='Y' and " +
                                                     "kd_buku_besar in (Select d.Val_kode1  " +
                                                     "From SIF.dbo.SIF_Gen_Reff_D d WITH (NOLOCK) " +
                                                     "where d.Id_Ref_Data = 'KKL')";
                var res = con.Query<SIF_buku_besar>(sql);

                return res;
            }
        }
        public static async Task<IEnumerable<SIF_buku_besar>> GetRekGL()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT kd_buku_besar, nm_buku_besar FROM SIF.dbo.SIF_buku_besar WITH (NOLOCK) WHERE grup_header='D' AND div1='Y'  and " +
                              " kd_buku_besar not like '101%'";
                var res = con.Query<SIF_buku_besar>(sql);

                return res;
            }
        }

        public static async Task<IEnumerable<SIF_buku_besar>> PolaEntry(string id)
        {
            using (var con = DataAccess.GetConnection())
            {
                string Query = "";
                DynamicParameters param = new DynamicParameters();
                Query = "select nm_buku_besar, pola_entry from SIF.dbo.SIF_buku_besar WITH (NOLOCK) where kd_buku_besar=@id";
                param = new DynamicParameters();
                param.Add("@id", id);
                var res = con.Query<SIF_buku_besar>(Query, param, null, true, 36000);


                return res;
            }
        }
        public static async Task<IEnumerable<SIF_buku_pusat>> GetRekBP()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT kd_buku_pusat, nm_buku_pusat FROM SIF.dbo.SIF_buku_pusat WITH (NOLOCK) ";
                             
                var res = con.Query<SIF_buku_pusat>(sql);

                return res;
            }
        }

        public static async Task<IEnumerable<SIF_Bank>> GetRekBank()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select kd_bank, nama_bank from SIF.dbo.SIF_Bank WITH (NOLOCK)";
                var res = con.Query<SIF_Bank>(sql);

                return res;
            }
        }
        public static async Task<IEnumerable<SIF_Valuta>> GetValuta()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select Kode_Valuta,Nama_Valuta from sif.dbo.SIF_valuta";
                var res = con.Query<SIF_Valuta>(sql);

                return res;
            }
        }
        public static async Task<IEnumerable<FIN_GIRO>> GetGiro(string kdcb, string kd_customer = null, string nomor = null)

        {
            using (var con = DataAccess.GetConnection())
            {

                string Query = "";
                DynamicParameters param = new DynamicParameters();

                string filter = "";
                Query = " select A.nomor,  B.nama_bank, isnull(BA.nama_bank_asal,'')nama_bank_asal, A.tgl_jth_tempo, A.jml_trans, isnull(A.keterangan,'') keterangan, '' cek_giro , 0.00 jml_trans_terpilih, A.kd_bank  " +
"from FIN.dbo.FIN_GIRO A " +
"left join SIF.dbo.SIF_Bank B on B.kd_bank = A.kd_bank " +
"left join (Select G.Val_kode1 kd_bank_asal,G.Desc_Data nama_bank_asal " +
"from SIF.dbo.SIF_Gen_Reff_D G " +
"where G.Id_Ref_File = 'BANKASAL') as BA " +
"on BA.kd_bank_asal = A.bank_asal " +
"where kartu = ' & lookcust.EditValue & ' and A.jns_giro = '01' " +
"and A.kd_valuta = 'IDR' and A.jns_trans = 'JUAL' and A.tipe_trans = 'JRR-KPT-10' " +
"and A.nomor not in (select distinct isnull(L.no_giro,'kosong') " +
"from FIN.dbo.FIN_NOTA_LUNAS_D_GIRO L where L.status in ('DITERIMA', 'CLERARING', 'CAIR'))  " +
"and A.[status] = 'DITERIMA'  ";
                param = new DynamicParameters();
                param.Add("@kdcb", kdcb);
                param.Add("@kd_customer", kd_customer);
                param.Add("@nomor", nomor);

                if (kd_customer != string.Empty && kd_customer != null && nomor != string.Empty && nomor != null)
                {

                    filter += " where A.kartu=@kd_customer and A.nomor=@nomor and A.Kd_Cabang=@kdcb";
                }

                if (kd_customer != string.Empty && kd_customer != null )
                {

                    filter += " where A.kartu=@kd_customer and A.Kd_Cabang=@kdcb";
                }

                Query += filter;
                var res = con.Query<FIN_GIRO>(Query, param, null, true, 36000);

                return res;
            }

           
        }

        public static async Task<IEnumerable<FIN_GIRO>> GetGiroBeli(string kdcb, string kdsup = null, string nomor = null)

        {
            using (var con = DataAccess.GetConnection())
            {

                string Query = "";
                DynamicParameters param = new DynamicParameters();

                string filter = "";
                Query = " select  a.Kd_Cabang,a.kartu, a.jns_giro,a.nomor, a.keterangan, a.tgl_jth_tempo, a.jml_trans, b.Nama_Valuta, a.kurs_valuta from FIN.dbo.FIN_GIRO a, SIF.dbo.SIF_Valuta b where a.kd_valuta = b.Kode_Valuta " +
"AND a.kartu = @kdsupp " +
"and a.jns_giro = '01' " +
" and a.jns_trans = 'BELI' and a.tipe_trans = 'JRR-KUT-01' and a.status LIKE 'KELUAR%' and a.nomor not in (select distinct isnull(kd_giro,'kosong') from FIN.dbo.FIN_BELI_LUNAS) AND NOT(a.no_jur IS NULL OR a.no_jur = '') ORDER BY a.nomor ";
                param = new DynamicParameters();
                param.Add("@kdcb", kdcb);
                param.Add("@kdsup", kdsup);
                param.Add("@nomor", nomor);

                if (kdsup != string.Empty && kdsup != null && nomor != string.Empty && nomor != null)
                {

                    filter += " where A.kartu=@kd_customer and A.nomor=@nomor and A.Kd_Cabang=@kdcb";
                }

                if (kdsup != string.Empty && kdsup != null)
                {

                    filter += " where A.kartu=@kd_customer and A.Kd_Cabang=@kdcb";
                }

                Query += filter;
                var res = con.Query<FIN_GIRO>(Query, param, null, true, 36000);

                return res;
            }


        }
        public static async Task<SIF_SETTING> GetSysSettingByKey(string key)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT ID, SYSKEY, SYSVALUE FROM SIF_SETTING WITH(NOLOCK) WHERE SYSKEY=@key";
                DynamicParameters param = new DynamicParameters();
                param.Add("@key", key);
                var res = con.Query<SIF_SETTING>(sql, param);

                return res.FirstOrDefault();
            }
        }

        public static async Task<int> UpdateSysSetting(SIF_SETTING data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [SIF].[dbo].[SIF_SETTING] SET SYSVALUE=@SYSVALUE WHERE SYSKEY=@SYSKEY;";
            param = new DynamicParameters();
            param.Add("@SYSVALUE", data.SYSVALUE);
            param.Add("@SYSKEY", data.SYSKEY);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<IEnumerable<SIF_Gen_Reff_D>> GetJenisGiro()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT *,d.Id_Data 'kd_jns_giro', d.Desc_Data 'jns_giro' FROM SIF.dbo.SIF_Gen_Reff_D d WHERE d.Id_Ref_Data = 'JNSBYR' AND d.Id_Data IN('01','02')";
                var res = await con.QueryAsync<SIF_Gen_Reff_D>(sql);

                return res;
            }
        }

        public static async Task<IEnumerable<SIF_Departemen>> GetDivisi()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT * FROM SIF.dbo.SIF_Departemen f WHERE f.Kd_Departemen in ('2', '3')";
                var res = await con.QueryAsync<SIF_Departemen>(sql);

                return res;
            }
        }

        public static async Task<IEnumerable<SIF_Gen_Reff_D>> GetBankAsal()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT * FROM SIF.dbo.SIF_Gen_Reff_D WHERE Id_Ref_Data = 'BANKASAL'";
                var res = await con.QueryAsync<SIF_Gen_Reff_D>(sql);

                return res;
            }
        }

        public static async Task<IEnumerable<SIF_Bank>> GetBankTujuan()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT * FROM SIF.dbo.SIF_Bank";
                var res = await con.QueryAsync<SIF_Bank>(sql);

                return res;
            }
        }

        public static async Task<IEnumerable<v_kartu_csv>> GetKartuGiro()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select kode, nama from FIN.dbo.v_kartu_cst";
                var res = await con.QueryAsync<v_kartu_csv>(sql);

                return res;
            }
        }
        public static async Task<IEnumerable<SIF_CUSTOMER>> GetKartu()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT * FROM SIF.dbo.SIF_CUSTOMER";
                var res = await con.QueryAsync<SIF_CUSTOMER>(sql);

                return res;
            }
        }

        public static async Task<IEnumerable<SIF_ALAMAT_KIRIM>> GetAlamatKirim()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT * FROM SIF.dbo.SIF_ALAMAT_KIRIM";
                var res = await con.QueryAsync<SIF_ALAMAT_KIRIM>(sql);

                return res;
            }
        }
    }
}
