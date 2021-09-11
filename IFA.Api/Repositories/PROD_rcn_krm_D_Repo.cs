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
    public class PROD_rcn_krm_D_Repo
    {


        public static async Task<int> DeleteRcnKirimDetail(string id, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE FROM [PROD].[dbo].[PROD_rcn_krm_d]   WHERE no_trans=@no_trans;";
            param = new DynamicParameters();
            param.Add("@no_trans", id);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SavercnkirimDetail(PROD_rcn_krm_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [PROD].[dbo].[PROD_rcn_krm_d]   (Kd_Cabang,kd_departemen,tanggal,no_trans,no_sp,no_sp_dtl,no_seq,kd_barang,jumlah," +
                    " no_dpb,kd_customer) " +
                    " VALUES(@Kd_Cabang,@kd_departemen,@tanggal,@no_trans,@no_sp,@no_sp_dtl,@no_seq,@kd_barang,@jumlah,@no_dpb,@kd_customer);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.kd_cabang);
            param.Add("@kd_departemen", data.kd_departemen);
            param.Add("@tanggal", data.tanggal);
            param.Add("@no_trans", data.no_trans);
            param.Add("@no_sp", data.no_sp);
            param.Add("@no_sp_dtl", data.no_sp_dtl);
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_barang", data.kd_barang);
            param.Add("@jumlah", data.jumlah);
            param.Add("@no_dpb", data.no_dpb);
            param.Add("@kd_customer", data.kd_customer);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateSODetail(PROD_rcn_krm_D data, SqlTransaction trans)
        {
            int res = 0;

            string Query = "";
            DynamicParameters param = new DynamicParameters();

            Query = "update sales.dbo.sales_so_d set QTY=@Qty where no_sp=@no_sp and Kd_Stok=@Kd_Stok";
            param = new DynamicParameters();
            param.Add("@no_sp", data.no_sp);
            param.Add("@Kd_Stok", data.kd_barang);
            param.Add("@Qty", data.jumlah);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }


        public static IEnumerable<PRODV_MON_SO> GetrcnkirimDetail(string no_trans)
        {
            using (var con = DataAccess.GetConnection())
            {
              
                string sql = "select b.Nama_Barang, '' kd_kenek,v.*,v.qty_sisa_krm + d.jumlah as sisa , d.no_trans,d.jumlah, d.jml_indeks, ISNULL(d.jml_m3,0),d.no_dpb,ISNULL(b.berat,0)as berat,ISNULL(b.berat*d.jumlah,0) as Totberat " +
                    " from PROD.dbo.PROD_rcn_krm_d d left join PROD.dbo.PRODV_MON_SO v on v.No_sp = d.no_sp and  v.No_seq_d = d.no_sp_dtl And v.No_sp_box = d.no_seq " +
                    " left join SIF.dbo.SIF_Barang b on v.Kd_Stok = b.Kode_Barang where no_trans=@no_trans order by v.no_sp, v.no_seq_d, v.No_sp_box ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);
                var res = con.Query<PRODV_MON_SO>(sql, param);

                return res;
            }

        }

    }
}
