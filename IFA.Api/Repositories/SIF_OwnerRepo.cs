using Dapper;
using ERP.Api.Utils;
using ERP.Domain.Base;
using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Api.Repositories
{
    public class SIF_OwnerRepo
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
        public static IEnumerable<SIF_Owner> Get_Owner(string Kd_Owner = "")
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT *  " +
                    " FROM SIF_Owner WITH(NOLOCK) WHERE Nama_Owner<>'' AND Kd_Owner<>'' ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@kode_Owner", Kd_Owner);

                if (Kd_Owner != string.Empty && Kd_Owner != null)
                {
                    sql += " AND Kd_Owner=@kode_Owner ";
                }

                sql += " ORDER BY Last_Create_Date ASC, CASE WHEN Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC ";

                var res = con.Query<SIF_Owner>(sql, param);

                return res;
            }
        }
        public static List<SIF_Owner> GetALL_Owner()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT * from SIF_Owner";
                    //" JP.nm_buku_besar as nm_rek_persediaan,B.rek_hpp,BB.nm_buku_besar as nm_rek_hpp,B.Rec_Stat,B.rek_penjualan1," +
                    //" P1.nm_buku_besar as nm_rek_penjualan1,B.rek_penjualan2,P2.nm_buku_besar as nm_rek_penjualan2,B.rek_retur1," +
                    //" R1.nm_buku_besar as nm_rek_retur1,B.rek_retur2,R2.nm_buku_besar as nm_rek_retur2,B.rek_bonus1,B1.nm_buku_besar as nm_rek_bonus1," +
                    //" B.rek_bonus2,B2.nm_buku_besar as nm_rek_bonus2,B.kd_kategori,K.nama as nama_kategori,B.kd_sub_kategori,SK.nama as nama_sub_kategori" +
                    //" FROM SIF_Barang B WITH(NOLOCK) LEFT OUTER JOIN SIF_buku_besar P1 WITH (NOLOCK) ON P1.kd_buku_besar = B.rek_penjualan1" +
                    //" LEFT OUTER JOIN SIF_buku_besar P2  WITH (NOLOCK)  ON P2.kd_buku_besar = B.rek_penjualan2 LEFT OUTER JOIN SIF_buku_besar R1 ON R1.kd_buku_besar = B.rek_retur1" +
                    //" LEFT OUTER JOIN SIF_buku_besar R2  WITH (NOLOCK)  ON R2.kd_buku_besar = B.rek_retur2" +
                    //" LEFT OUTER JOIN SIF_buku_besar JP WITH (NOLOCK) ON JP.kd_buku_besar = B.rek_persediaan" +
                    //" LEFT OUTER JOIN SIF_buku_besar BB WITH (NOLOCK) ON BB.kd_buku_besar = B.rek_hpp" +
                    //" LEFT OUTER JOIN SIF_buku_besar B1 WITH (NOLOCK) ON B1.kd_buku_besar = B.rek_bonus1" +
                    //" LEFT OUTER JOIN SIF_buku_besar B2 WITH (NOLOCK) ON B2.kd_buku_besar = B.rek_bonus2" +
                    //" LEFT OUTER JOIN SIF_kategori K ON K.kd_kategori = B.kd_kategori" +
                    //" LEFT OUTER JOIN SIF_sub_kategori SK WITH (NOLOCK) ON SK.kd_sub_kategori = B.kd_sub_kategori" +
                    //" WHERE B.Rec_Stat = 'Y'";
                DynamicParameters param = new DynamicParameters();

                sql += " ORDER BY Nama_Owner ";

                var res = con.Query<SIF_Owner>(sql, param, null, true, 36000);

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


        public static int SaveOwner(SIF_Owner data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF_Owner   (Kd_Owner, Kd_Cabang, Nama_Owner,Alamat_Owner, Last_Created_By, Last_Create_Date) " +
                    "VALUES(@Kd_Owner, @Kd_Cabang, @Nama_Owner,@Alamat_Owner, @Last_Created_By, @Last_Create_Date);";
            param = new DynamicParameters();
            param.Add("@Kd_Owner", data.Kd_Owner);
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Nama_Owner", data.Nama_Owner);
            param.Add("@Alamat_Owner", data.Alamat_Owner);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
          

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }


        public static int UpdateOwner(SIF_Owner data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE SIF_Owner SET  Kd_Cabang=@Kd_Cabang,Nama_Owner=@Nama_Owner,Alamat_Owner=@Alamat_Owner," +
                    "Last_Updated_By=@Last_Updated_By, Last_Update_Date=@Last_Update_Date " +
                    " WHERE Kd_Owner=@Kd_Owner;";
            param = new DynamicParameters();
            param.Add("@Kd_Owner", data.Kd_Owner);
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Nama_Owner", data.Nama_Owner);
            param.Add("@Alamat_Owner", data.Alamat_Owner);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@Last_Update_Date", DateTime.Now);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static SIF_Owner assignData(SIF_Owner data)
        {
            SIF_Owner SIF_BrgCont = new SIF_Owner();
            SIF_BrgCont.Kd_Owner = data.Kd_Owner;
            SIF_BrgCont.Kd_Cabang = data.Kd_Cabang;
            SIF_BrgCont.Nama_Owner = data.Nama_Owner;
            SIF_BrgCont.Alamat_Owner = data.Alamat_Owner;
            
            SIF_BrgCont.Last_Created_By = "SYSTEM";
            SIF_BrgCont.Last_Create_Date = DateTime.Now;

            return SIF_BrgCont;
        }

        public static int DeleteOwner(SIF_Owner data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE FROM SIF_Owner   WHERE Kd_Owner=@Kd_Owner;";
            param = new DynamicParameters();
            param.Add("@Kd_Owner", data.Kd_Owner);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static int SaveListOwner(SIF_Owner data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF_Owner(Kd_Cabang,Nama_Owner,Alamat_Owner, " +
                    "Last_Created_By,Last_Create_Date) " +
                    "VALUES(@Kd_Cabang, @Nama_Owner,@Alamat_Owner, " +
                    "@Last_Created_By,@Last_Create_Date);";
            param = new DynamicParameters();
            
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Nama_Owner", data.Nama_Owner);
            param.Add("@Alamat_Owner", data.Alamat_Owner);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
           
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }
        public static Response getCountPO(DateTime DateFrom, DateTime DateTo, string filterquery = "", string sortingquery = "", string owner = "", int harga = 0, string pet = "")
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT count(*) as Result " +
                   " from SIF_Owner a WITH(NOLOCK) " +
                     " LEFT JOIN SIF.DBO.SIF_PET b WITH(NOLOCK) ON a.KD_Owner = b.KD_Owner";


                DynamicParameters param = new DynamicParameters();
                param.Add("@pet", pet);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@harga", harga);
                param.Add("@owner", owner);

                if (pet != string.Empty && pet != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " b.Nama_Pet like '%" + @pet + "%' ";
                }
                if (owner != string.Empty && owner != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " a.Nama_Owner like '%" + @owner + "%'  ";
                }
                if (DateFrom != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " a.Last_Create_Date >= @DateFrom ";
                }

                if (DateTo != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " a.Last_Create_Date <= @DateTo ";
                }

                if (harga > 0)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " b.harga_pet >= 0 AND b.harga_pet <= @harga ";
                }


                sql += filter;
                // sql += " GROUP BY PO.no_po ";

                var res = con.Query<Response>(sql, param, null, true, 36000);

                return res.FirstOrDefault();
            }
        }

        public async static Task<List<SIF_Owner>> GetOwnerPartial(DateTime DateFrom, DateTime DateTo, string filterquery = "", string sortingquery = "", string owner = "", int harga = 0, string pet = "")
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                
                string sql = "SELECT a.* from SIF_Owner a" +
                " Left Join SIF_Pet b on a.KD_Owner = b.KD_Owner";
               


                DynamicParameters param = new DynamicParameters();
                param.Add("@pet", pet);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@harga", harga);
                param.Add("@owner", owner);


                if (pet != string.Empty && pet != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " b.Nama_Pet like '%" + @pet + "%' ";
                }
                if (owner != string.Empty && owner != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " a.Nama_Owner like '%" + @owner + "%'  ";
                }
                if (DateFrom != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " a.Last_Create_Date >= @DateFrom ";
                }

                if (DateTo != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " a.Last_Create_Date <= @DateTo ";
                }

                if (harga > 0)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " b.harga_pet >= 0 AND b.harga_pet <= @harga ";
                }


                sql += filter;

                
                    sql += " ORDER BY a.Last_Create_Date DESC ";
               


                var res = con.Query<SIF_Owner>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }


    }
}
