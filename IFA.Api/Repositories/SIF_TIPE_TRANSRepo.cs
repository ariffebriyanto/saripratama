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
    public class SIF_TIPE_TRANSRepo
    {
        public static async Task<IEnumerable<SIF_TIPE_TRANS>> GetTipeTrans()
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                //string filter = "";
                string sql = "SELECT kd_cabang, kd_subsis, kd_jurnal, tipe_trans, tipe_desc, accronim_desc, auto_manual, CASE WHEN auto_posting IS NULL THEN 'M' ELSE auto_posting END auto_posting, flag_pajak, flag_materai, attribut1, attribut2, status, last_create_date, last_created_by, last_update_date, last_updated_by, program_name, kd_dept " +
                    "FROM [SIF].[dbo].[SIF_TIPE_TRANS] B WITH (NOLOCK) ";
                var res = con.Query<SIF_TIPE_TRANS>(sql, param);

                return res;
            }
        }

        public static async Task<IEnumerable<SIF_TIPE_TRANSVMON>> GetTipeTransMon(string kd_jurnal = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                //string filter = "";
                string sql = "select A.* from SIF.dbo.SIF_TIPE_TRANS A,SIF.dbo.SIF_Gen_Reff_D B  " +
        "  where A.kd_jurnal = B.Id_Data " +
        "  and B.Id_Ref_Data = 'JNSJUR' " +
        "  and B.Id_Data = @kd_jurnal ";
                param = new DynamicParameters();
                param.Add("@kd_jurnal", kd_jurnal);

                var res = con.Query<SIF_TIPE_TRANSVMON>(sql, param);

                return res;
            }
        }

        public static async Task<int> Update(SIF_TIPE_TRANS data, string update_by, DateTime update_date, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();

            Query = "UPDATE [SIF].[dbo].[SIF_TIPE_TRANS] WITH(ROWLOCK) SET auto_posting=@auto_posting, last_updated_by=@last_updated_by, last_update_date=@last_update_date" +
                "  WHERE tipe_trans=@tipe_trans";
            param = new DynamicParameters();
            param.Add("@auto_posting", data.auto_posting);
            param.Add("@last_updated_by", update_by);
            param.Add("@last_update_date", update_date);
            param.Add("@tipe_trans", data.tipe_trans);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

    }
}
