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
    public class PURC_DPM_DRepo
    {
        public static IEnumerable<PURC_DPM_D> GetPURC_DPM_D(string No_DPM="", string status = "")
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT D.Kd_Cabang,tipe_trans,D.no_po,No_DPM,No_Seq,D.Kd_Stok,Satuan,D.spek_brg,Qty,Qty_PR,Qty_received,Qty_sisa,D.Keterangan,D.rec_stat,no_csp,Tgl_Diperlukan,D.Last_Create_Date,D.Last_Created_By,D.Last_Update_Date,D.Last_Updated_By,D.Program_Name, B.Nama_Barang, CASE WHEN D.rec_stat ='ENTRY' THEN D.Qty ELSE Qty_PR END AS qty_approve, CASE WHEN D.rec_stat = 'ENTRY' THEN '' ELSE D.rec_stat END AS status_approve, last_price, C.nama AS cabang  " +
                    " FROM[PURC].[dbo].[PURC_DPM_D] D WITH(NOLOCK) " +
                    " INNER JOIN[SIF].[dbo].[SIF_BARANG] B WITH(NOLOCK) ON B.Kode_Barang=D.Kd_Stok  " +
                    " LEFT OUTER JOIN [PURC].[dbo].[PURC_PO_D_PRICE] P WITH(NOLOCK) ON B.Kode_Barang=P.Kd_Stok " +
                    " INNER JOIN [SIF].[dbo].[SIF_cabang] C WITH(NOLOCK) ON C.kd_cabang=D.Kd_Cabang  ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@No_DPM", No_DPM);
                param.Add("@rec_stat", status);

                if (No_DPM != string.Empty && No_DPM != null)
                {
                    if (filter== "")
                    {
                        filter = " WHERE ";
                    }
                    else
                    {
                        filter = " AND ";
                    }
                    sql += filter + "  No_DPM=@No_DPM ";
                }

                if (status != string.Empty && status != null)
                {
                    if (filter == "")
                    {
                        filter = " WHERE ";
                    }
                    else
                    {
                        filter = " AND ";
                    }
                    sql += filter + "  D.rec_stat=@rec_stat ";
                }
                sql += " ORDER BY D.Last_Create_Date DESC, CASE WHEN D.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC ";

                var res = con.Query<PURC_DPM_D>(sql, param);

                return res;
            }
        }
        public static int SavePURC_DPM_D(PURC_DPM_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [PURC].[dbo].[PURC_DPM_D]  (Kd_Cabang,tipe_trans,No_DPM,No_Seq,Kd_Stok,Satuan,spek_brg,Qty,Qty_PR,Qty_received,Qty_sisa,Keterangan,rec_stat,no_csp,Tgl_Diperlukan,Last_Create_Date,Last_Created_By) " +
                    "VALUES(@Kd_Cabang,@tipe_trans,@No_DPM,@No_Seq,@Kd_Stok,@Satuan,@spek_brg,@Qty,@Qty_PR,@Qty_received,@Qty_sisa,@Keterangan,@rec_stat,@no_csp,@Tgl_Diperlukan,@Last_Create_Date,@Last_Created_By);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@No_DPM", data.No_DPM);
            param.Add("@No_Seq", data.No_Seq);
            param.Add("@Kd_Stok", data.Kd_Stok);
            param.Add("@Satuan", data.Satuan);
            param.Add("@spek_brg", data.spek_brg);
            param.Add("@Qty", data.Qty);
            param.Add("@Qty_PR", data.Qty_PR);
            param.Add("@Qty_received", data.Qty_received);
            param.Add("@Qty_sisa", data.Qty_sisa);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@rec_stat", data.rec_stat);
            param.Add("@no_csp", data.no_csp);
            param.Add("@Tgl_Diperlukan", data.Tgl_Diperlukan);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static int UpdatePURC_DPM_D(PURC_DPM_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [PURC].[dbo].[PURC_DPM_D]  SET Kd_Cabang=@Kd_Cabang, tipe_trans=@tipe_trans,  No_Seq=@No_Seq, " +
                " Kd_Stok=@Kd_Stok, Satuan=@Satuan, spek_brg=@spek_brg, Qty=@Qty, Qty_PR=@Qty_PR, Qty_received=@Qty_received, Qty_sisa=@Qty_sisa, Keterangan=@Keterangan, " +
                " rec_stat=@rec_stat, no_csp=@no_csp, Tgl_Diperlukan=@Tgl_Diperlukan, Last_Update_Date=@Last_Update_Date, Last_Updated_By=@Last_Updated_By " +
                " WHERE No_DPM=@No_DPM;";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@No_DPM", data.No_DPM);
            param.Add("@No_Seq", data.No_Seq);
            param.Add("@Kd_Stok", data.Kd_Stok);
            param.Add("@Satuan", data.Satuan);
            param.Add("@spek_brg", data.spek_brg);
            param.Add("@Qty", data.Qty);
            param.Add("@Qty_PR", data.Qty_PR);
            param.Add("@Qty_received", data.Qty_received);
            param.Add("@Qty_sisa", data.Qty_sisa);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@rec_stat", data.rec_stat);
            param.Add("@no_csp", data.no_csp);
            param.Add("@Tgl_Diperlukan", data.Tgl_Diperlukan);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }


        public static int UpdateNo_PO(PURC_DPM_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [PURC].[dbo].[PURC_DPM_D]  SET no_po=@no_po, rec_stat='PURCHASE', Last_Update_Date=@Last_Update_Date, Last_Updated_By=@Last_Updated_By " +
                    "WHERE No_DPM=@No_DPM ;";
            param = new DynamicParameters();
            param.Add("@No_DPM", data.No_DPM);
            param.Add("@no_po", data.no_po);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static int UpdateApproval(PURC_DPM_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [PURC].[dbo].[PURC_DPM_D]  SET Qty_PR=@Qty_PR, Qty_sisa=@Qty_sisa, rec_stat=@rec_stat, Last_Update_Date=@Last_Update_Date, Last_Updated_By=@Last_Updated_By " +
                    "WHERE No_DPM=@No_DPM ;";
            param = new DynamicParameters();
            param.Add("@No_DPM", data.No_DPM);
            param.Add("@Qty_PR", data.Qty_PR);
            param.Add("@Qty_sisa", data.Qty_sisa);
            param.Add("@rec_stat", data.rec_stat);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

    }
}
