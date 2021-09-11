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
    public class INV_OPNAMERepo
    {
        public static IEnumerable<INV_OPNAME> GetOpname(string no_trans = null, string kd_cabang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT Kd_Cabang, tgl_trans, no_trans, bultah, keterangan, no_jurnal, no_posting, Last_Create_Date, Last_Created_By, Last_Update_Date, Last_Updated_By, Program_Name  " +
                    " FROM [INV].[dbo].[INV_OPNAME] WITH (NOLOCK)   ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);
                param.Add("@kd_cabang", kd_cabang);

                if (no_trans != string.Empty && no_trans != null)
                {
                    sql += " WHERE no_trans=@no_trans ";
                }
                else if (kd_cabang != null)
                {
                    sql += " WHERE kd_cabang=@kd_cabang ";
                }

                sql += " ORDER BY Last_Create_Date DESC, CASE WHEN Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC ";

                var res = con.Query<INV_OPNAME>(sql, param);

                return res;
            }
        }

        public static IEnumerable<INV_OPNAME_DTL> GetOpnameDTL(string no_trans = null, string kd_cabang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT D.Kd_Cabang,no_trans,no_seq,kd_stok,D.kd_satuan,kode_gudang,D.kd_ukuran,D.kd_jns_persd,D.kd_jenis,D.spek_brg,qty_data,qty_opname,qty_selisih,persen,nilai_rata,nilai_manual,D.keterangan,D.Last_Create_Date,D.Last_Created_By,D.Last_Update_Date,D.Last_Updated_By,D.Program_Name,bultah,tgl_trans,total, B.Nama_Barang   " +
                            " FROM[INV].[dbo].[INV_OPNAME_DTL] D WITH (NOLOCK)  " +
                            " INNER JOIN SIF.dbo.SIF_BARANG B WITH (NOLOCK)  ON D.kd_stok = B.Kode_Barang  ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);
                param.Add("@kd_cabang", kd_cabang);
                if (no_trans != string.Empty && no_trans != null)
                {
                    sql += " WHERE no_trans=@no_trans ";
                }
                else if (kd_cabang != null)
                {
                    sql += " WHERE kd_cabang=@kd_cabang ";
                }
                sql += " ORDER BY Last_Create_Date DESC, CASE WHEN D.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC ";

                var res = con.Query<INV_OPNAME_DTL>(sql, param);

                return res;
            }
        }

        public static async Task<decimal> CekSaldoAwal(string kd_cabang, string kd_stok, string bultah, SqlTransaction trans)
        {

            decimal res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();



            Query = "select s.awal_qty_onstok from INV.dbo.INV_STOK_SALDO s WITH (NOLOCK) " +
                             " where s.kd_stok=@kd_stok and s.bultah=@bultah and s.Kd_Cabang=@kd_cabang";



            param = new DynamicParameters();
            param.Add("@kd_cabang", kd_cabang);
            param.Add("@kd_stok", kd_stok);
            param.Add("@bultah", bultah);

            res = trans.Connection.Query<decimal>(Query, param, transaction: trans).FirstOrDefault();
            //res = con.Query<V_StokGudang>(sql, param, null, true, 36000);
            return res;

        }

        public static async Task<int> CountRow(string gudang, string kd_stok, string bultah, SqlTransaction trans)
        {
            string sql;
            int result = 0;

            sql = "select  kd_stok from AMERICAN_REPORT.DBO.[vy_saldocard]  " +
            "where kd_stok ='" + kd_stok + "'  and gudang ='" + gudang + "' and bultah='" + bultah + "' ";


            using (
                SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);


                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string tes = "";
                            tes = dt.Rows[0][0].ToString();
                            if (dt.Rows[0][0].ToString() != "")
                            {
                                result = dt.Rows.Count;
                            }
                            else
                            {
                                result = 0;
                            }
                        }
                        else
                        {
                            result = 0;
                        }
                    }
                }
            }

            return result;
        }


        public static async Task<decimal> CekKartuStok(string gudang, string kd_stok, string bultah, SqlTransaction trans)
        {
            decimal res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "select (awal_qty_onstok + sum(qty_in)) - sum(qty_out) as qty_kartu from AMERICAN_REPORT.DBO.[vy_saldocard] " +
    " where kd_stok =@kd_stok  and gudang =@gudang and bultah=@bultah " +
    " group by kd_stok,awal_qty_onstok ";

            param = new DynamicParameters();
            param.Add("@gudang", gudang);
            param.Add("@kd_stok", kd_stok);
            param.Add("@bultah", bultah);

            res = trans.Connection.Query<decimal>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveOpname(INV_OPNAME data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_OPNAME]   (Kd_Cabang,tgl_trans,no_trans,bultah,keterangan,no_jurnal,no_posting,Last_Create_Date,Last_Created_By,Program_Name) " +
                    "VALUES(@Kd_Cabang,@tgl_trans,@no_trans,@bultah,@keterangan,@no_jurnal,@no_posting,@Last_Create_Date,@Last_Created_By,@Program_Name) ";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@no_trans", data.no_trans);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@bultah", data.bultah);
            param.Add("@keterangan", data.keterangan);
            param.Add("@no_jurnal", data.no_jurnal);
            param.Add("@no_posting", data.no_posting);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static int DeleteDetail(string id, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE FROM [INV].[dbo].[INV_OPNAME_DTL]   WHERE no_trans=@no_trans;";
            param = new DynamicParameters();
            param.Add("@no_trans", id);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveDetail(INV_OPNAME_DTL data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_OPNAME_DTL]   (Kd_Cabang, no_trans, no_seq, kd_stok, kd_satuan, kode_gudang, kd_ukuran, kd_jns_persd, kd_jenis, spek_brg, qty_data, qty_opname, qty_selisih, persen, nilai_rata, nilai_manual, keterangan, Last_Create_Date, " +
                    " Last_Created_By, Program_Name, bultah, tgl_trans, total) " +
                    " VALUES(@Kd_Cabang,@no_trans,@no_seq,@kd_stok,@kd_satuan,@kode_gudang,@kd_ukuran,@kd_jns_persd,@kd_jenis,@spek_brg,@qty_data, " +
                    " @qty_opname,@qty_selisih,@persen,@nilai_rata,@nilai_manual,@keterangan,@Last_Create_Date,@Last_Created_By,@Program_Name,@bultah,@tgl_trans,@total);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@no_trans", data.no_trans);
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@kd_satuan", data.kd_satuan);
            param.Add("@kode_gudang", data.kode_gudang);
            param.Add("@kd_ukuran", data.kd_ukuran);
            param.Add("@kd_jns_persd", data.kd_jns_persd);
            param.Add("@kd_jenis", data.kd_jenis);
            param.Add("@spek_brg", data.spek_brg);
            param.Add("@qty_data", data.qty_data);
            param.Add("@qty_opname", data.qty_opname);
            param.Add("@qty_selisih", data.qty_selisih);
            param.Add("@persen", data.persen);
            param.Add("@nilai_rata", data.nilai_rata);
            param.Add("@nilai_manual", data.nilai_manual);
            param.Add("@keterangan", data.keterangan);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@bultah", data.bultah);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@total", data.total);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SPOpname(string notrans, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@notrans", notrans);

                return conn.Execute("[INV].[dbo].[stok_opname]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

    }


}
