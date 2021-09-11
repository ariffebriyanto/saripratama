using Dapper;
using ERP.Api.Utils;
using ERP.Domain.Base;
using IFA.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace IFA.Api.Repositories
{
    public class FINGiroRepo
    {
        public static IEnumerable<FIN_GIRO> GetFINGiro()
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                //string filter = "";
                string sql = "SELECT a.*, c.Nama_Valuta, d.Desc_Data as jenis_giro, "+
                            "e.Desc_Data as nama_bank_asal, g.nama_bank, "+
                            "f.Nama_Departemen, cs.nama_customer " +
                            "FROM FIN.dbo.FIN_GIRO a "+
                            "left join fin.dbo.fin_jurnal b on a.no_jur = b.no_jur "+
                            "inner join SIF.dbo.SIF_Valuta c on a.kd_valuta = c.Kode_Valuta "+
                            "inner join SIF.dbo.SIF_Gen_Reff_D d on a.jns_giro = d.Id_Data and d.Id_Ref_Data = 'JNSBYR' AND d.Id_Data IN('01','02') "+
                            "inner join SIF.DBO.SIF_Gen_Reff_D e on a.bank_asal = e.Id_data and e.Id_Ref_Data = 'BANKASAL' "+
                            "inner join SIF.dbo.SIF_Departemen f on a.divisi = f.Kd_Departemen and f.Kd_Departemen in ('2', '3') "+
                            "inner join SIF.dbo.SIF_Bank g on a.kd_bank = g.kd_bank "+
                            "inner join SIF.dbo.SIF_Customer cs on a.kartu = cs.kd_customer " +
                            "WHERE a.jns_trans = 'JUAL' and a.status = 'DITERIMA' " +
                            "and isnull(b.no_posting,'') = ''" +
                            " ORDER BY a.Last_Create_Date DESC ";
                var res = con.Query<FIN_GIRO>(sql, param);

                return res;
            }
        }
        public static List<FIN_GIRO> GetALL_Giro()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT a.*, C.Desc_Data, D.Desc_Data AS jns_giro_desc, E.nama_departemen, F.nama_bank, g.nama_customer      " +
                                                " FROM FIN.dbo.FIN_GIRO a " +
                                               " left join fin.dbo.fin_jurnal b on a.no_jur = b.no_jur " +
                                               " INNER JOIN SIF.dbo.SIF_Gen_Reff_D C ON A.bank_asal=C.Id_Data AND  Id_Ref_Data = 'BANKASAL'" +
                                               " INNER JOIN SIF.dbo.SIF_Gen_Reff_D d ON A.jns_giro=D.Id_Data AND d.Id_Ref_Data = 'JNSBYR' AND d.Id_Data IN ('01','02')" +
                                               " INNER JOIN SIF.dbo.SIF_Departemen E ON A.divisi=E.Kd_Departemen AND E.Kd_Departemen in ('2', '3')" +
                                               " INNER JOIN SIF.dbo.SIF_Bank F WITH (NOLOCK) ON A.kd_bank=F.kd_bank" +
                                               " INNER JOIN SIF.dbo.SIF_CUSTOMER G WITH(NOLOCK) ON A.kartu=g.Kd_Customer " +
                                               " WHERE a.jns_trans = 'JUAL' and a.status = 'DITERIMA' " +
                                               " and isnull(b.no_posting,'') = ''";
                DynamicParameters param = new DynamicParameters();

                sql += " ORDER BY a.tgl_trans";

                var res = con.Query<FIN_GIRO>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public static List<FIN_GIRO> GetALL_GiroTemp()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT a.*, C.Desc_Data, D.Desc_Data AS jns_giro_desc, E.nama_departemen, F.nama_bank, g.nama_customer      " +
                                                " FROM FIN.dbo.FIN_GIRO a " +
                                               " left join fin.dbo.fin_jurnal b on a.no_jur = b.no_jur " +
                                               " INNER JOIN SIF.dbo.SIF_Gen_Reff_D C ON A.bank_asal=C.Id_Data AND  Id_Ref_Data = 'BANKASAL'" +
                                               " INNER JOIN SIF.dbo.SIF_Gen_Reff_D d ON A.jns_giro=D.Id_Data AND d.Id_Ref_Data = 'JNSBYR' AND d.Id_Data IN ('01','02')" +
                                               " INNER JOIN SIF.dbo.SIF_Departemen E ON A.divisi=E.Kd_Departemen AND E.Kd_Departemen in ('2', '3')" +
                                               " INNER JOIN SIF.dbo.SIF_Bank F WITH (NOLOCK) ON A.kd_bank=F.kd_bank" +
                                               " INNER JOIN SIF.dbo.SIF_CUSTOMER G WITH(NOLOCK) ON A.kartu=g.Kd_Customer " +
                                               " WHERE a.jns_trans = 'JUAL' and a.status = 'DITERIMA' " +
                                               " and isnull(b.no_posting,'') = '' and isnull(a.no_ref,'') = ''";
                DynamicParameters param = new DynamicParameters();

                sql += " ORDER BY a.tgl_trans";

                var res = con.Query<FIN_GIRO>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public static IEnumerable<FIN_GIRO> Get_Giro(string nomor = "")
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT a.* FROM FIN.dbo.FIN_GIRO a " +
                                               "left join fin.dbo.fin_jurnal b on a.no_jur = b.no_jur " +
                                               "WHERE a.jns_trans = 'JUAL' and a.status = 'DITERIMA' " +
                                               "and isnull(b.no_posting,'') = ''";


                DynamicParameters param = new DynamicParameters();
                param.Add("@kode_Barang", nomor);

                if (nomor != string.Empty && nomor != null)
                {
                    sql += " AND a.nomor=@kode_Barang ";
                }

                sql += " ORDER BY a.tgl_trans ";

                var res = con.Query<FIN_GIRO>(sql, param);

                return res;
            }
        }

        public static int SaveListGiro(FIN_GIRO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO FIN.dbo.FIN_GIRO   (Kd_Cabang, nomor, jns_trans, divisi, tipe_trans, jns_giro, tgl_trans, kd_bank, " +
                " kartu, tgl_jth_tempo, kd_valuta, kurs_valuta, jml_trans, keterangan, status, no_jur, Last_Create_Date, " +
                " Last_Created_By, Last_Update_Date, Last_Updated_By, Program_Name, bank_asal) " +
                    "VALUES(@Kd_Cabang,@nomor,@jns_trans,@divisi,@tipe_trans,@jns_giro,@tgl_trans,@kd_bank,@kartu,@tgl_jth_tempo,@kd_valuta,@kurs_valuta,@jml_trans,@keterangan,@status,@no_jur,@Last_Create_Date,@Last_Created_By,@Last_Update_Date,@Last_Updated_By,@Program_Name,@bank_asal);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@nomor", data.nomor);
            param.Add("@jns_trans", data.jns_trans);
            param.Add("@divisi", data.divisi);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@jns_giro", data.jns_giro);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@kd_bank", data.kd_bank);
            param.Add("@kartu", data.kartu);
            param.Add("@tgl_jth_tempo", data.tgl_jth_tempo);
            param.Add("@kd_valuta", data.kd_valuta);
            param.Add("@kurs_valuta", data.kurs_valuta);
            param.Add("@jml_trans", data.jml_trans);
            param.Add("@keterangan", data.keterangan);
            param.Add("@status", data.status);
            param.Add("@no_jur", data.no_jur);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@bank_asal", data.bank_asal);
            param.Add("@Program_Name", data.Program_Name);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static int UpdateFINGIRO(FIN_GIRO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE FIN.dbo.FIN_GIRO   SET  Kd_Cabang=@Kd_Cabang,nomor=@nomor,jns_trans=@jns_trans,divisi=@divisi,tipe_trans=@tipe_trans,jns_giro=@jns_giro," +
                    " tgl_trans=@tgl_trans,kd_bank=@kd_bank,kartu=@kartu,tgl_jth_tempo=@tgl_jth_tempo,jml_trans=@jml_trans,keterangan=@keterangan,status=@status," +
                    " no_jur=@no_jur,bank_asal=@bank_asal, Last_Updated_By=@Last_Updated_By, Last_Update_Date=@Last_Update_Date " +
                    " WHERE nomor=@nomor;";
            param = new DynamicParameters();
           
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@nomor", data.nomor);
            param.Add("@jns_trans", data.jns_trans);
            param.Add("@divisi", data.divisi);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@jns_giro", data.jns_giro);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@kd_bank", data.kd_bank);
            param.Add("@kartu", data.kartu);
            param.Add("@tgl_jth_tempo", data.tgl_jth_tempo);
            param.Add("@kd_valuta", data.kd_valuta);
            param.Add("@kurs_valuta", data.kurs_valuta);
            param.Add("@jml_trans", data.jml_trans);
            param.Add("@keterangan", data.keterangan);
            param.Add("@status", data.status);
            param.Add("@no_jur", data.no_jur);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@bank_asal", data.bank_asal);
          

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static int DeleteGiro(FIN_GIRO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE FROM FIN.dbo.FIN_GIRO    WHERE Kode_Barang=@Kode_Barang;";
            param = new DynamicParameters();
            param.Add("@nomor", data.nomor);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveGiro(FIN_GIRO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO FIN.dbo.FIN_GIRO   (Kd_Cabang, nomor, jns_trans, divisi, tipe_trans, jns_giro, tgl_trans, kd_bank, " +
                " kartu, tgl_jth_tempo, kd_valuta, kurs_valuta, jml_trans, keterangan, status, no_jur, Last_Create_Date, " +
                " Last_Created_By, Last_Update_Date, Last_Updated_By, Program_Name, bank_asal) " +
                    "VALUES(@Kd_Cabang,@nomor,@jns_trans,@divisi,@tipe_trans,@jns_giro,@tgl_trans,@kd_bank,@kartu,@tgl_jth_tempo,@kd_valuta,@kurs_valuta,@jml_trans,@keterangan,@status,@no_jur,@Last_Create_Date,@Last_Created_By,@Last_Update_Date,@Last_Updated_By,@Program_Name,@bank_asal);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@nomor", data.nomor);
            param.Add("@jns_trans", data.jns_trans);
            param.Add("@divisi", data.divisi);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@jns_giro", data.jns_giro);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@kd_bank", data.kd_bank);
            param.Add("@kartu", data.kartu);
            param.Add("@tgl_jth_tempo", data.tgl_jth_tempo);
            param.Add("@kd_valuta", data.kd_valuta);
            param.Add("@kurs_valuta", data.kurs_valuta);
            param.Add("@jml_trans", data.jml_trans);
            param.Add("@keterangan", data.keterangan);
            param.Add("@status", data.status);
            param.Add("@no_jur", data.no_jur);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@bank_asal", data.bank_asal);
            param.Add("@Program_Name", data.Program_Name);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
    }
}
