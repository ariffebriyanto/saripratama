using Dapper;
using ERP.Api.Utils;
using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Api.Repositories
{
    public class SIF_BarangRepo
    {
        public static List<SIF_BarangCbo> GetSIFBarangCbo()
        {
            using (var con = DataAccess.GetConnection())
            {
                //string sql = "SELECT Kode_Barang,Nama_Barang, kd_jenis, B.Kd_Satuan,  " +
                //    " (SELECT TOP 1 harga FROM PURC.DBO.PURC_PO_D D WITH(NOLOCK) " +
                //    " INNER JOIN PURC.DBO.PURC_PO PO  WITH(NOLOCK) ON PO.no_po = D.no_po AND PO.rec_stat = 'APPROVE' " +
                //    " WHERE D.kd_stok = B.Kode_Barang " +
                //    " ORDER BY tgl_approve DESC " +
                //    " ) AS last_price " +
                //    " FROM SIF_Barang B WITH(NOLOCK) WHERE B.Rec_Stat='Y' AND not (kd_jns_persd in ('5','4')) AND Nama_Barang<>'' AND Kode_Barang<>'' ";

                string sql = "SELECT Kode_Barang,Nama_Barang, kd_jenis, B.Kd_Satuan, P.Last_Price " +
                   " FROM SIF_Barang B WITH(NOLOCK) " +
                   " LEFT OUTER JOIN PURC.dbo.PURC_PO_D_PRICE P WITH(NOLOCK) ON B.Kode_Barang=P.KD_STOK " +
                   " WHERE B.Rec_Stat='Y' AND not (kd_jns_persd in ('5','4')) AND Nama_Barang<>'' AND Kode_Barang<>'' ";

                DynamicParameters param = new DynamicParameters();

                sql += " ORDER BY Nama_Barang ";

                var res = con.Query<SIF_BarangCbo>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public static List<SIF_BarangCbo> GetSIFBarangCB()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT Kode_Barang,Nama_Barang, kd_jenis, Kd_Satuan,rek_persediaan " +
                    " FROM SIF_Barang WITH(NOLOCK) WHERE Rec_Stat='Y' AND not (kd_jns_persd in ('5','4')) AND Nama_Barang<>'' AND Kode_Barang<>'' ";


                DynamicParameters param = new DynamicParameters();

                sql += " ORDER BY Nama_Barang ";

                var res = con.Query<SIF_BarangCbo>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public static List<SIF_BarangCbo> GetSIFBarangCboSaldo(string kdcabang)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "  SELECT DISTINCT Kode_Barang,Nama_Barang, B.kd_jenis, B.Kd_Satuan, isnull (stok.akhir_qty_onstok,0) as qty_data, rek_persediaan, stok.nilai_rata as harga  " +
  "FROM SIF.DBO.SIF_Barang B WITH(NOLOCK) " +
  "INNER JOIN INV.dbo.INV_STOK_SALDO as stok WITH (NOLOCK) on stok.kd_stok = B.Kode_Barang " +
  "WHERE Rec_Stat = 'Y' AND not(B.kd_jns_persd in ('5','4')) AND Nama_Barang<>'' AND Kode_Barang<>'' " +
  "AND stok.Kd_Cabang = @kdcabang AND stok.bultah = (select SUBSTRING(CONVERT(varchar, GETDATE(), 112), 1, 6) )  " +
  " AND stok.bultah = (select SUBSTRING(CONVERT(varchar, GETDATE(), 112), 1, 6))";


                DynamicParameters param = new DynamicParameters();
                param.Add("@kdcabang", kdcabang);
                sql += " ORDER BY Nama_Barang ";

                var res = con.Query<SIF_BarangCbo>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public static List<SIF_BarangCbo> GetSIFBarangCboSaldoGudang(string kdcabang)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "   SELECT DISTINCT Kode_Barang,Nama_Barang, B.kd_jenis, B.Kd_Satuan, isnull (stok.akhir_qty,0) as qty_data, rek_persediaan, 0 as harga  " +
                    " FROM SIF.DBO.SIF_Barang B WITH(NOLOCK) " +
                    " INNER JOIN INV.dbo.INV_STOK_GUDANG as stok WITH(NOLOCK) on stok.kd_stok = B.Kode_Barang " +
                    " WHERE Rec_Stat = 'Y' AND not(B.kd_jns_persd in ('5','4')) AND Nama_Barang<>'' AND Kode_Barang<>'' AND stok.kode_gudang = @kdcabang " +
                    " AND stok.bultah = (select SUBSTRING(CONVERT(varchar, GETDATE(), 112), 1, 6) )   AND stok.bultah = (select SUBSTRING(CONVERT(varchar, GETDATE(), 112), 1, 6)) ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@kdcabang", kdcabang);
                sql += " ORDER BY Nama_Barang ";

                var res = con.Query<SIF_BarangCbo>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }
        public static IEnumerable<SIF_Barang> Get_Barang(string Kode_Barang = "")
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT Kd_Cabang,Kode_Barang,Nama_Barang,Kd_Satuan,Keterangan, Rec_Stat,Last_Create_Date,Last_Created_By,Last_Update_Date,Last_Updated_By,Program_Name  " +
                    " FROM SIF_Barang WITH(NOLOCK) WHERE Rec_Stat='Y' AND not (kd_jns_persd in ('5','4')) AND Nama_Barang<>'' AND Kode_Barang<>'' ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@kode_Barang", Kode_Barang);

                if (Kode_Barang != string.Empty && Kode_Barang != null)
                {
                    sql += " AND Kode_Barang=@kode_Barang ";
                }

                sql += " ORDER BY Last_Create_Date ASC, CASE WHEN Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC ";

                var res = con.Query<SIF_Barang>(sql, param);

                return res;
            }
        }
        public static List<SIF_Barang> GetALL_Barang()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT B.Kd_Cabang,B.Kode_Barang,B.Kd_Satuan,B.Nama_Barang,B.nm_jual,isnull(B.berat,0) berat,B.rek_persediaan," +
                    " JP.nm_buku_besar as nm_rek_persediaan,B.rek_hpp,BB.nm_buku_besar as nm_rek_hpp,B.Rec_Stat,B.rek_penjualan1," +
                    " P1.nm_buku_besar as nm_rek_penjualan1,B.rek_penjualan2,P2.nm_buku_besar as nm_rek_penjualan2,B.rek_retur1," +
                    " R1.nm_buku_besar as nm_rek_retur1,B.rek_retur2,R2.nm_buku_besar as nm_rek_retur2,B.rek_bonus1,B1.nm_buku_besar as nm_rek_bonus1," +
                    " B.rek_bonus2,B2.nm_buku_besar as nm_rek_bonus2,B.kd_kategori,K.nama as nama_kategori,B.kd_sub_kategori,SK.nama as nama_sub_kategori" +
                    " FROM SIF_Barang B WITH(NOLOCK) LEFT OUTER JOIN SIF_buku_besar P1 WITH (NOLOCK) ON P1.kd_buku_besar = B.rek_penjualan1" +
                    " LEFT OUTER JOIN SIF_buku_besar P2  WITH (NOLOCK)  ON P2.kd_buku_besar = B.rek_penjualan2 LEFT OUTER JOIN SIF_buku_besar R1 ON R1.kd_buku_besar = B.rek_retur1" +
                    " LEFT OUTER JOIN SIF_buku_besar R2  WITH (NOLOCK)  ON R2.kd_buku_besar = B.rek_retur2" +
                    " LEFT OUTER JOIN SIF_buku_besar JP WITH (NOLOCK) ON JP.kd_buku_besar = B.rek_persediaan" +
                    " LEFT OUTER JOIN SIF_buku_besar BB WITH (NOLOCK) ON BB.kd_buku_besar = B.rek_hpp" +
                    " LEFT OUTER JOIN SIF_buku_besar B1 WITH (NOLOCK) ON B1.kd_buku_besar = B.rek_bonus1" +
                    " LEFT OUTER JOIN SIF_buku_besar B2 WITH (NOLOCK) ON B2.kd_buku_besar = B.rek_bonus2" +
                    " LEFT OUTER JOIN SIF_kategori K ON K.kd_kategori = B.kd_kategori" +
                    " LEFT OUTER JOIN SIF_sub_kategori SK WITH (NOLOCK) ON SK.kd_sub_kategori = B.kd_sub_kategori" +
                    " WHERE B.Rec_Stat = 'Y'";
                DynamicParameters param = new DynamicParameters();

                sql += " ORDER BY Nama_Barang ";

                var res = con.Query<SIF_Barang>(sql, param, null, true,36000);

                return res.ToList();
            }
        }

        public static List<SIF_Barang> GetALL_BarangMobile(string nama_barang)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT B.Kd_Cabang,B.Kode_Barang,B.Kd_Satuan,B.Nama_Barang,B.nm_jual,B.berat,B.rek_persediaan," +
                    " JP.nm_buku_besar as nm_rek_persediaan,B.rek_hpp,BB.nm_buku_besar as nm_rek_hpp,B.Rec_Stat,B.rek_penjualan1," +
                    " P1.nm_buku_besar as nm_rek_penjualan1,B.rek_penjualan2,P2.nm_buku_besar as nm_rek_penjualan2,B.rek_retur1," +
                    " R1.nm_buku_besar as nm_rek_retur1,B.rek_retur2,R2.nm_buku_besar as nm_rek_retur2,B.rek_bonus1,B1.nm_buku_besar as nm_rek_bonus1," +
                    " B.rek_bonus2,B2.nm_buku_besar as nm_rek_bonus2,B.kd_kategori,K.nama as nama_kategori,B.kd_sub_kategori,SK.nama as nama_sub_kategori" +
                    " FROM SIF_Barang B WITH(NOLOCK) LEFT OUTER JOIN SIF_buku_besar P1 ON P1.kd_buku_besar = B.rek_penjualan1" +
                    " LEFT OUTER JOIN SIF_buku_besar P2 WITH (NOLOCK)  ON P2.kd_buku_besar = B.rek_penjualan2 LEFT OUTER JOIN SIF_buku_besar R1 ON R1.kd_buku_besar = B.rek_retur1" +
                    " LEFT OUTER JOIN SIF_buku_besar R2 WITH (NOLOCK)  ON R2.kd_buku_besar = B.rek_retur2" +
                    " LEFT OUTER JOIN SIF_buku_besar JP WITH (NOLOCK)  ON JP.kd_buku_besar = B.rek_persediaan" +
                    " LEFT OUTER JOIN SIF_buku_besar BB WITH (NOLOCK)  ON BB.kd_buku_besar = B.rek_hpp" +
                    " LEFT OUTER JOIN SIF_buku_besar B1 WITH (NOLOCK)  ON B1.kd_buku_besar = B.rek_bonus1" +
                    " LEFT OUTER JOIN SIF_buku_besar B2 WITH (NOLOCK)  ON B2.kd_buku_besar = B.rek_bonus2" +
                    " LEFT OUTER JOIN SIF_kategori K WITH (NOLOCK)  ON K.kd_kategori = B.kd_kategori" +
                    " LEFT OUTER JOIN SIF_sub_kategori SK WITH (NOLOCK)  ON SK.kd_sub_kategori = B.kd_sub_kategori" +
                    " WHERE B.Rec_Stat = 'Y'";
                DynamicParameters param = new DynamicParameters();
                param.Add("@nama_barang", nama_barang);
                sql += " AND B.nama_barang LIKE CONCAT('%',@nama_barang,'%') ";


                sql += " ORDER BY Nama_Barang ";

                var res = con.Query<SIF_Barang>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }


        public static int SaveBrg(SIF_Barang data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF_Barang   (Kode_Barang, Kd_Cabang, Nama_Barang, ,Kd_Satuan,Keterangan, Last_Created_By, Last_Create_Date, Rec_Stat) " +
                    "VALUES(@Kode_Kota, @Kd_Cabang, @Nama_Kota,@Kd_Satuan, @Keterangan, @Last_Created_By, @Last_Create_Date, @Rec_Stat);";
            param = new DynamicParameters();
            param.Add("@Kode_Barang", data.Kode_Barang);
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Nama_Barang", data.Nama_Barang);
            param.Add("@Kd_Satuan", data.Kd_Satuan);
            param.Add("@Keterangan", data.Kd_Satuan);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Rec_Stat", data.Rec_Stat);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }


        public static int UpdateBrg(SIF_Barang data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE SIF_Barang   SET  Kd_Cabang=@Kd_Cabang,Nama_Barang=@Nama_Barang,nm_jual=@nm_jual,Kd_Satuan=@Kd_Satuan,rek_penjualan1=@rek_penjualan1,rek_penjualan2=@rek_penjualan2," +
                    " rek_persediaan=@rek_persediaan,rek_hpp=@rek_hpp,rek_retur1=@rek_retur1,rek_retur2=@rek_retur2,rek_bonus1=@rek_bonus1,rek_bonus2=@rek_bonus2,Keterangan=@Keterangan," +
                    "berat=@berat,Rec_Stat=@Rec_Stat, Last_Updated_By=@Last_Updated_By, Last_Update_Date=@Last_Update_Date " +
                    " WHERE Kode_Barang=@kode_Barang;";
            param = new DynamicParameters();
            param.Add("@kode_Barang", data.Kode_Barang);
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Nama_Barang", data.Nama_Barang);
            param.Add("@nm_jual", data.nm_jual);
            param.Add("@Kd_Satuan", data.Kd_Satuan);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@rek_persediaan", data.rek_persediaan);
            param.Add("@rek_penjualan1", data.rek_penjualan1);
            param.Add("@rek_penjualan2", data.rek_penjualan2);
            param.Add("@rek_hpp", data.rek_hpp);
            param.Add("@rek_bonus1", data.rek_bonus1);
            param.Add("@rek_bonus2", data.rek_bonus2);
            param.Add("@rek_retur1", data.rek_retur1);
            param.Add("@rek_retur2", data.rek_retur2);
            param.Add("@berat", data.berat);
            param.Add("@berat", data.berat);
            param.Add("@Rec_Stat", data.Rec_Stat);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@Last_Update_Date", DateTime.Now);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static SIF_Barang assignData(SIF_Barang data)
        {
            SIF_Barang SIF_BrgCont = new SIF_Barang();
            SIF_BrgCont.Kode_Barang = data.Kode_Barang;
            SIF_BrgCont.Kd_Cabang = data.Kd_Cabang;
            SIF_BrgCont.Nama_Barang = data.Nama_Barang;
            SIF_BrgCont.Kd_Satuan = data.Kd_Satuan;
            SIF_BrgCont.Rec_Stat = data.Rec_Stat;

            SIF_BrgCont.Last_Created_By = "SYSTEM";
            SIF_BrgCont.Last_Create_Date = DateTime.Now;

            return SIF_BrgCont;
        }

        public static int DeleteBrg(SIF_Barang data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE FROM SIF_Barang   WHERE Kode_Barang=@Kode_Barang;";
            param = new DynamicParameters();
            param.Add("@Kode_Barang", data.Kode_Barang);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static int SaveListBarang(SIF_Barang data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF_Barang   (Kode_Barang,Kd_Cabang,Nama_Barang,nm_jual,Kd_Satuan,rek_penjualan1,rek_penjualan2,rek_persediaan,rek_hpp, " +
                    "rek_retur1,rek_retur2,rek_bonus1,rek_bonus2,Keterangan,kd_jenis,kd_ukuran,kd_merk,kd_kain,tipe_stok,Last_Created_By,Last_Create_Date,Rec_Stat,Kd_Depart,kd_jns_persd,kd_tipe,kd_sub_tipe,lokasi,berat) " +
                    "VALUES(@Kode_Barang, @Kd_Cabang, @Nama_Barang,@nm_jual, @Kd_Satuan, @rek_penjualan1, @rek_penjualan2, @rek_persediaan,@rek_hpp,@rek_retur1,@rek_retur2, " +
                    "@rek_bonus1,@rek_bonus2,@Keterangan,@kd_jenis,@kd_ukuran,@kd_merk,@kd_kain,@tipe_stok,@Last_Created_By,@Last_Create_Date,@Rec_Stat,@Kd_Depart,@kd_jns_persd,@kd_tipe,@kd_sub_tipe,@lokasi,@berat);";
            param = new DynamicParameters();
            param.Add("@Kode_Barang", data.Kode_Barang);
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Nama_Barang", data.Nama_Barang);
            param.Add("@nm_jual", data.nm_jual);
            param.Add("@Kd_Satuan", data.Kd_Satuan);
            param.Add("@rek_penjualan1", data.rek_penjualan1);
            param.Add("@rek_penjualan2", data.rek_penjualan2);
            param.Add("@rek_persediaan", data.rek_persediaan);
            param.Add("@rek_hpp", data.rek_hpp);
            param.Add("@rek_retur1", data.rek_retur1);
            param.Add("@rek_retur2", data.rek_retur2);
            param.Add("@rek_bonus1", data.rek_bonus1);
            param.Add("@rek_bonus2", data.rek_bonus2);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@kd_jenis", data.kd_jenis);
            param.Add("@kd_ukuran", data.kd_ukuran);
            param.Add("@kd_merk", data.kd_merk);
            param.Add("@kd_kain", data.kd_kain);
            param.Add("@tipe_stok", data.tipe_stok);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Rec_Stat", "Y");
            param.Add("@Kd_Depart", data.Kd_Depart);
            param.Add("@kd_jns_persd", data.kd_jns_persd);
            param.Add("@kd_tipe", data.kd_tipe);
            param.Add("@kd_sub_tipe", data.kd_sub_tipe);
            param.Add("@lokasi", data.lokasi);
            param.Add("@berat", data.berat);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

       

    }
}
