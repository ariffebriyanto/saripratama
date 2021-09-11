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
    public class KasirRepo
    {
        public static async Task<IEnumerable<SIF_Pegawai>> GetKartu()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select a.stat, a.nama, a.kode from (select CASE WHEN SUBSTRING(kode_pegawai,1,1) = 'P' THEN 'Pegawai' ELSE 'Supir' END stat, nama_pegawai nama, kode_pegawai kode from sif.dbo.SIF_Pegawai WITH (NOLOCK) where not Kode_Pegawai in (select Kd_pegawai from sif.dbo.SIF_sopir WITH (NOLOCK)) AND Rec_stat = 'Y' union all select CASE WHEN SUBSTRING(kode_sopir,1,1) = 'P' THEN 'Pegawai' ELSE 'Supir' END stat, nama_sopir nama, kode_sopir kode from sif.dbo.SIF_sopir WITH (NOLOCK) where Rec_stat = 'Y') a order by a.nama";
                var res = con.Query<SIF_Pegawai>(sql);

                return res;
            }
        }
       

        public static async Task<int> DelKasBonDetail(string nomor, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE [FIN].[dbo].[FIN_KAS_BON]  " +
                " WHERE nomor=@nomor;";
            //Query = "UPDATE [SALES].[dbo].[SALES_SO] SET STATUS_DO='BATAL',Last_Update_Date=GETDATE(),Last_Updated_By=@Last_Updated_By  " +
            //    " WHERE No_sp=@No_sp;";
            param = new DynamicParameters();
            param.Add("@nomor", nomor);
           // param.Add("@Last_Updated_By", data.Last_Updated_By);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<IEnumerable<FIN_KAS_BON_H>> GetKasBon(string id = null, DateTime? DateFrom = null, DateTime? DateTo = null, string stat = null, string barang = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT  C.alamat, C.nama,C.fax1 as telp, C.fax2 as wa,* FROM FIN.dbo.FIN_KAS_BON_H S WITH (NOLOCK)" +
                    " INNER JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON S.Kd_Cabang = C.kd_cabang  ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@id", id);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@barang", barang);
                param.Add("@kd_cabang", cb);
                param.Add("@stat", stat);

                if (id != string.Empty && id != null)
                {

                    filter += " WHERE S.Kd_Cabang=@kd_cabang and S.nomor LIKE CONCAT('%',@id,'%')  ";
                    //filter += " WHEREKd_cabang @no_po AND S.STATUS_DO<>'BATAL' ";
                }
                else
                {
                    if (DateFrom != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE S.tgl_trans >= @DateFrom ";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                        // filter += " AND Tgl_sp >= @DateFrom ";
                    }

                    if (DateTo != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE  S.tgl_trans <= @DateTo";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                        filter += "AND S.tgl_trans <= @DateTo ";
                    }

                }

                //{
                //    filter += " AND ";
                //}
                filter += " AND DATEDIFF(day, S.tgl_trans, getdate()) <=30 ";

                sql += filter;

                sql += " ORDER BY S.Last_Create_Date DESC ";


                var res = con.Query<FIN_KAS_BON_H>(sql, param, null, true, 36000);

                return res;
            }
        }
        public static async Task<IEnumerable<FIN_KAS_BON>> GetKasBonD(string id = null, DateTime? DateFrom = null, DateTime? DateTo = null, string stat = null, string barang = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT * FROM FIN.dbo.FIN_KAS_BON WITH (NOLOCK) ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@id", id);
                param.Add("@stat", stat);
                param.Add("@kd_cabang", cb);

                if (id != string.Empty && id != null)
                {

                    filter += " WHERE Kd_Cabang=@kd_cabang and nomor LIKE CONCAT('%',@id,'%')  ";
                    //filter += " WHEREKd_cabang @no_po AND S.STATUS_DO<>'BATAL' ";
                }
                else
                {
                    if (DateFrom != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE tgl_trans >= @DateFrom ";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                        // filter += " AND Tgl_sp >= @DateFrom ";
                    }

                    if (DateTo != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE  tgl_trans <= @DateTo";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                        filter += "AND tgl_trans <= @DateTo ";
                    }

                }

                if (filter == null)
                {
                    filter += " AND DATEDIFF(day, tgl_trans, getdate()) <=30 ";
                }
       

                sql += filter;

                sql += " ORDER BY Last_Create_Date DESC ";

                var res = con.Query<FIN_KAS_BON>(sql, param, null, true, 36000);

                return res;
            }
        }


        public static async Task<int> SaveKasBon(FIN_KAS_BON_H data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [FIN].[dbo].[FIN_KAS_BON_H]  (Kd_Cabang,tipe_trans,nomor,no_trans,tgl_trans,total_trans,Last_Created_By,keterangan,rec_stat,Last_Create_Date) " +
                    "VALUES(@Kd_Cabang,@tipe_trans,@nomor,@nomor,@tgl_trans,@total_trans,@Last_Created_By,@keterangan,@rec_stat,GETDATE());";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@nomor", data.nomor);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@total_trans", data.total_trans);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@keterangan", data.keterangan);
            
            param.Add("@rec_stat", "Y");
           


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveKasBonDetail(FIN_KAS_BON data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [FIN].[dbo].[FIN_KAS_BON]  (Kd_Cabang,tipe_trans,nomor,no_seq,kd_kartu,tgl_trans,jml_trans,kd_buku_besar,kd_valuta,kurs_valuta,Last_Created_By,keterangan,Last_Create_Date,rec_stat) " +
                    "VALUES(@Kd_Cabang,@tipe_trans,@nomor,@no_seq,@kd_kartu,@tgl_trans,@jml_trans,@kd_buku_besar,@kd_valuta,@kurs_valuta,@Last_Created_By,@keterangan,GETDATE(),@rec_stat);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@nomor", data.nomor);
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_kartu", data.kd_kartu);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@jml_trans", data.jml_trans);
            param.Add("@kd_buku_besar", data.kd_buku_besar);
            param.Add("@kd_valuta", data.kd_valuta);
            param.Add("@kurs_valuta", data.kurs_valuta);
          
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@keterangan", data.keterangan);

            param.Add("@rec_stat", "Y");



            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> Delete(string id, string Last_Updated_By, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            //Query = "DELETE [SALES].[dbo].[SALES_SO] WITH(ROWLOCK) " +
            //    " WHERE No_sp=@No_sp;";
            Query = "UPDATE [FIN].[dbo].[FIN_KAS_BON_H] SET rec_stat='T',Last_Update_Date=GETDATE(),Last_Updated_By=@Last_Updated_By  " +
                " WHERE nomor=@id;";
            param = new DynamicParameters();
            param.Add("@id", id);
            param.Add("@Last_Updated_By", Last_Updated_By);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> DeleteDTL(string id, string Last_Updated_By, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            //Query = "DELETE [SALES].[dbo].[SALES_SO] WITH(ROWLOCK) " +
            //    " WHERE No_sp=@No_sp;";
            Query = "UPDATE [FIN].[dbo].[FIN_KAS_BON] SET rec_stat='T',Last_Update_Date=GETDATE(),Last_Updated_By=@Last_Updated_By  " +
                " WHERE nomor=@id;";
            param = new DynamicParameters();
            param.Add("@id", id);
            param.Add("@Last_Updated_By", Last_Updated_By);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
    }
}
