using Dapper;
using ERP.Api.Utils;
using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Api.Repositories
{
    public class PUPR_PO_DRepo
    {
        public static async Task<int> DeletePODetail(string id, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE FROM [PURC].[dbo].[PURC_PO_D]  WHERE no_po=@no_po;";
            param = new DynamicParameters();
            param.Add("@no_po", id);
           

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SavePODetail(PURC_PO_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [PURC].[dbo].[PURC_PO_D]  (Kd_Cabang,tipe_trans,no_po,no_seq,kd_stok,spek_brg,kd_satuan,satuan_beli," +
                    " qty,harga,harga_raw,prosen_diskon,diskon2,diskon3,jml_diskon,total,keterangan,tgl_kirim,qty_kirim,qty_sisa,Last_Create_Date," + 
                    " Last_Created_By,Program_Name,jasa,inv_stat,flag_bonus,Diskon4,Bonus,harga_new,diskon1_new,diskon2_new," +
                    " diskon3_new,diskon4_new,total_new,jml_diskon_new, last_price) " +
                    " VALUES(@Kd_Cabang,@tipe_trans,@no_po,@no_seq,@kd_stok,@spek_brg,@kd_satuan,@satuan_beli,@qty,@harga,@harga_raw,@prosen_diskon," +
                    " @diskon2,@diskon3,@jml_diskon,@total,@keterangan,@tgl_kirim,@qty_kirim,@qty_sisa,@Last_Create_Date,@Last_Created_By," +
                    " @Program_Name,@jasa,@inv_stat,@flag_bonus,@Diskon4,@Bonus,@harga_new,@diskon1_new," +
                    " @diskon2_new,@diskon3_new,@diskon4_new,@total_new,@jml_diskon_new, @last_price);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_po", data.no_po);
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@spek_brg", data.spek_brg);
            param.Add("@kd_satuan", data.kd_satuan);
            param.Add("@satuan_beli", data.satuan_beli);
            param.Add("@qty", data.qty);
            param.Add("@harga", data.harga);
            param.Add("@tgl_kirim", data.tgl_kirim);
            param.Add("@harga_raw", data.harga_raw);
            param.Add("@prosen_diskon", data.prosen_diskon);
            param.Add("@diskon2", data.diskon2);
            param.Add("@diskon3", data.diskon3);
            param.Add("@jml_diskon", data.jml_diskon);
            param.Add("@total", data.total);
            param.Add("@keterangan", data.keterangan);
            param.Add("@tgl_kirim", data.tgl_kirim);
            param.Add("@qty_kirim", data.qty_kirim);
            param.Add("@qty_sisa", data.qty_sisa);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@jasa", data.jasa);
            param.Add("@inv_stat", data.inv_stat);
            param.Add("@flag_bonus", data.flag_bonus);
            param.Add("@Diskon4", data.Diskon4);
            param.Add("@Bonus", data.Bonus);
            param.Add("@harga_new", data.harga_new);
            param.Add("@diskon1_new", data.diskon1_new);
            param.Add("@diskon2_new", data.diskon2_new);
            param.Add("@diskon3_new", data.diskon3_new);
            param.Add("@diskon4_new", data.diskon4_new);
            param.Add("@total_new", data.total_new);
            param.Add("@jml_diskon_new", data.jml_diskon_new);
            param.Add("@last_price", data.last_price);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public async static Task<IEnumerable<PURC_PO_D>> GetPODetail(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT D.jml_diskon, D.keterangan,D.kd_stok, D.kd_satuan,D.no_po,  D.no_seq, D.qty, D.harga, D.total, D.prosen_diskon, D.diskon2, D.diskon3, D.Diskon4, B.Nama_Barang AS nama_barang, S.Nama_Satuan AS satuan, D.last_price, case when Bonus='T' THEN 'false' ELSE 'true' END AS Bonus, D.tgl_kirim, D.qty_kirim, D.qty_sisa   " +
                            " FROM PURC.DBO.PURC_PO H WITH (NOLOCK) " +
                            " INNER JOIN PURC.DBO.PURC_PO_D D WITH (NOLOCK) ON D.no_po = H.no_po " +
                            " left outer  JOIN SIF.dbo.SIF_Barang B WITH (NOLOCK) ON D.kd_stok = B.Kode_Barang " +
                            " left outer  JOIN SIF.dbo.SIF_Satuan S WITH (NOLOCK) ON D.kd_satuan = S.Kode_Satuan ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@status_po", status_po);
                param.Add("@barang", barang);

                if (no_po != string.Empty && no_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " H.no_po = @no_po ";
                }
                if (barang != string.Empty && barang != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " D.kd_stok = @barang ";
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
                    filter += " H.tgl_po >= @DateFrom ";
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
                    filter += " H.tgl_po <= @DateTo ";
                }

                sql += filter;

                var res = con.Query<PURC_PO_D>(sql, param);

                return res;
            }
        }

        public async static Task<PURC_PO> GetPOPenerimaan(string no_po = null)
        {
            using (var con = DataAccess.GetConnection())
            {

                string sql = "SELECT * FROM PURC.DBO.PURC_PO  WITH (NOLOCK) where no_po = @no_po";


                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);

                var res = con.Query<PURC_PO>(sql, param);

                return res.FirstOrDefault();
            }
        }
        public async static Task<IEnumerable<PURC_PO_D>> GetDetailPOPenerimaan(string no_po = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                
                string sql = "SELECT * FROM PURC.DBO.PURC_PO_D WITH (NOLOCK) where no_po = @no_po";
                           

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
           
                var res = con.Query<PURC_PO_D>(sql, param);

                return res;
            }
        }
        public async static Task<IEnumerable<PURC_PO_D>> GetPODtlCbg(string kd_cabang = null, string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT hg.Harga_Rupiah harga1,hg.Harga_Rupiah4 harga4, D.jml_diskon, D.keterangan,D.kd_stok, D.kd_satuan,D.no_po, D.no_seq, D.qty, D.harga, D.total, D.prosen_diskon, D.diskon2, D.diskon3, D.Diskon4, B.Nama_Barang AS nama_barang, S.Nama_Satuan AS satuan, D.last_price  " +
                            " FROM PURC.DBO.PURC_PO H WITH (NOLOCK) " +
                            " INNER JOIN PURC.DBO.PURC_PO_D D WITH (NOLOCK) ON D.no_po = H.no_po " +
                            " left outer  JOIN SIF.dbo.SIF_Barang B WITH (NOLOCK) ON D.kd_stok = B.Kode_Barang " +
                            " left outer  JOIN SIF.dbo.SIF_Satuan S WITH (NOLOCK) ON D.kd_satuan = S.Kode_Satuan " +
                            " left outer  JOIN SIF.dbo.SIF_Harga hg WITH (NOLOCK) ON D.kd_stok = Hg.Kode_Barang and hg.Kd_Cabang=@kd_cabang ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", "%" + no_po + "%");
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@status_po", status_po);
                param.Add("@barang", barang);
                param.Add("@kd_cabang", kd_cabang);

                if (no_po != string.Empty && no_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " H.no_po LIKE @no_po ";
                }
                if (barang != string.Empty && barang != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " D.kd_stok = @barang ";
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
                    filter += " H.tgl_po >= @DateFrom ";
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
                    filter += " H.tgl_po <= @DateTo ";
                }

                sql += filter;

                var res = con.Query<PURC_PO_D>(sql, param);

                return res;
            }
        }


        public async static Task<IEnumerable<PURC_PO_D>> GetDetailApprovalPO(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT D.no_po, D.no_seq, D.qty, D.harga, D.total, D.prosen_diskon, D.diskon2, D.diskon3, D.Diskon4, B.Nama_Barang AS nama_barang, S.Nama_Satuan AS satuan, D.last_price,D.keterangan  " +
                            " FROM PURC.DBO.PURC_PO H WITH (NOLOCK) " +
                            " INNER JOIN PURC.DBO.PURC_PO_D D WITH (NOLOCK) ON D.no_po = H.no_po " +
                            " left outer  JOIN SIF.dbo.SIF_Barang B WITH (NOLOCK) ON D.kd_stok = B.Kode_Barang " +
                            " left outer  JOIN SIF.dbo.SIF_Satuan S WITH (NOLOCK) ON D.kd_satuan = S.Kode_Satuan ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@status_po", status_po);

                if (no_po != string.Empty && no_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " H.no_po LIKE CONCAT('%',@no_po,'%') ";
                }

                if (status_po != string.Empty && status_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " H.rec_stat =@status_po AND ISNULL(H.status_po,'') <> 'BATAL' ";
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
                    filter += " H.tgl_po >= @DateFrom ";
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
                    filter += " H.tgl_po <= @DateTo ";
                }

                sql += filter;

                var res = con.Query<PURC_PO_D>(sql, param);

                return res;
            }
        }

        public static IEnumerable<PURC_PO> GetReturInv(string kd_cabang)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT a.no_po ,b.Nama_Supplier ,j.no_jur, a.kd_supplier  " +
                    "from sif.dbo.SIF_Supplier b WITH (NOLOCK) left join purc.dbo.purc_po a WITH (NOLOCK) on a.kd_supplier=b.Kode_Supplier " +
                    "left join FIN.dbo.FIN_JURNAL_D j WITH (NOLOCK) on a.no_po=j.no_ref1 " +
                    "where a.no_po is not null and j.no_jur is not null and j.Kd_Cabang=@kdcabang and datediff(day, a.Last_Create_Date,getdate()) <=365 and datediff(day, a.Last_Create_Date,getdate()) >=0 and a.Last_Create_Date is not null " +
                    " group by a.no_po ,b.Nama_Supplier ,j.no_jur, a.kd_supplier, a.Last_Create_Date " +
                    " order by a.Last_Create_Date desc"; 

                DynamicParameters param = new DynamicParameters();
                param.Add("@kdcabang", kd_cabang);

                var res = con.Query<PURC_PO>(sql, param);

                return res;
            }
        }

        public static IEnumerable<vBarangPOTerpilih> GetPORetur(string no_po)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select * from PURC.dbo.vBarangPOTerpilih where  no_po=@no_po";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);

                var res = con.Query<vBarangPOTerpilih>(sql, param);

                return res;
            }
        }

        public static async Task<int> SaveReturPODetail(PURC_RETUR_BELI_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [PURC].[dbo].[PURC_RETUR_BELI_D]  (Kd_Cabang, no_retur, no_seq, kd_stok, " +
                " satuan, qty, qty_sisa, harga, total, keterangan, rec_stat, Last_Create_Date, Last_Created_By, " +
                " Last_Update_Date, Last_Updated_By, Program_Name, Disk1, Disk2, Disk3, Disk4,jml_diskon, Bonus)" +
                " VALUES (@Kd_Cabang,@no_retur,@no_seq,@kd_stok,@satuan,@qty,@qty_sisa,@harga,@total,@keterangan," +
                " @rec_stat,@Last_Create_Date,@Last_Created_By,@Last_Update_Date,@Last_Updated_By,@Program_Name," +
                " @Disk1,@Disk2,@Disk3,@Disk4,@jml_diskon,@Bonus)";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@no_retur", data.no_retur);
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@satuan", data.satuan);
            param.Add("@qty", data.qty);
            param.Add("@qty_sisa", data.qty_sisa);
            param.Add("@harga", data.harga);
            param.Add("@total", data.total);
            param.Add("@keterangan", data.keterangan);
            param.Add("@rec_stat", data.rec_stat);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@Disk1", data.Disk1);
            param.Add("@Disk2", data.Disk2);
            param.Add("@Disk3", data.Disk3);
            param.Add("@Disk4", data.Disk4);
            param.Add("@jml_diskon", data.jml_diskon);
            param.Add("@Bonus", data.Bonus);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static IEnumerable<PURC_RETUR_BELI> GetRetur(string no_retur)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT Kd_Cabang, no_retur, tanggal, no_po, no_ref, no_ref1, kd_supplier, keterangan, " +
                    "jml_rp_trans, rec_stat, Last_Create_Date, Last_Created_By, Last_Update_Date, Last_Updated_By, " +
                    "Program_Name, jumlah_cetak, flag_ppn, Bonus " +
                    "FROM PURC.dbo.PURC_RETUR_BELI WITH (NOLOCK)" +
                    "WHERE no_retur=@no_retur";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_retur", no_retur);

                var res = con.Query<PURC_RETUR_BELI>(sql, param);

                return res;
            }
        }

        public static IEnumerable<PURC_RETUR_BELI> GetReturList(DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT R.*, Nama_Supplier "+ 
                            "FROM[PURC].[dbo].[PURC_RETUR_BELI] R "+
                            "INNER JOIN SIF.DBO.SIF_SUPPLIER S ON S.Kode_Supplier = R.kd_supplier ";

                DynamicParameters param = new DynamicParameters();

                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);

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
                    filter += " R.Last_Create_Date >= @DateFrom ";
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
                    filter += " R.Last_Create_Date <= @DateTo ";
                }

                sql += filter;

                sql += " ORDER BY R.Last_Create_Date DESC";

                var res = con.Query<PURC_RETUR_BELI>(sql, param);

                return res;
            }
        }

        public static IEnumerable<PURC_RETUR_BELI_D> GetReturDetail(string no_retur)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT D.Kd_Cabang, no_retur, no_seq, kd_stok, satuan, qty, qty_sisa, harga, total, D.keterangan, " +
                    " D.rec_stat, D.Last_Create_Date, D.Last_Created_By, D.Last_Update_Date, D.Last_Updated_By, D.Program_Name, " +
                    " Disk1, Disk2, Disk3, Disk4,jml_diskon, Bonus,B.Nama_Barang " +
                    " FROM PURC.dbo.PURC_RETUR_BELI_D D WITH (NOLOCK) " +
                    " INNER JOIN SIF.DBO.SIF_BARANG B WITH (NOLOCK) ON D.kd_stok=B.kode_barang  " +
                    " WHERE no_retur=@no_retur";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_retur", no_retur);

                var res = con.Query<PURC_RETUR_BELI_D>(sql, param);

                return res;
            }
        }

        public async static Task<PURC_PO_D> GetLastPO(string kd_stok)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT TOP 1 D.jml_diskon, D.keterangan,D.kd_stok, D.kd_satuan,D.no_po, D.no_seq, D.qty, D.harga, D.total, D.prosen_diskon, D.diskon2, D.diskon3, D.Diskon4, B.Nama_Barang AS nama_barang, S.Nama_Satuan AS satuan, D.last_price  " +
                            " FROM PURC.DBO.PURC_PO H WITH (NOLOCK) " +
                            " INNER JOIN PURC.DBO.PURC_PO_D D WITH (NOLOCK) ON D.no_po = H.no_po " +
                            " left outer  JOIN SIF.dbo.SIF_Barang B WITH (NOLOCK) ON D.kd_stok = B.Kode_Barang " +
                            " left outer  JOIN SIF.dbo.SIF_Satuan S WITH (NOLOCK) ON D.kd_satuan = S.Kode_Satuan ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_stok", kd_stok);
             

              

                sql += "WHERE D.kd_stok=@kd_stok ORDER BY H.Last_Create_Date DESC";

                var res = con.Query<PURC_PO_D>(sql, param);

                return res.FirstOrDefault();
            }
        }

        public static int SPFIN_AUTOMAN_JURNALReturPO(string no_retur, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@tipe_trans", "JPB-KUT-03");
                param.Add("@no_inv", no_retur);

                return conn.Execute("FIN.dbo.FIN_AUTOMAN_JURNAL", param, trans, 36000, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }
    }
}
