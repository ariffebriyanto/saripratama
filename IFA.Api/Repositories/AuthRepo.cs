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
    public class AuthRepo
    {
        public static Auth getAuthLogin(Auth auth)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT MUSER.akses_penjualan, MUSER.userid as UserID, MUSER.passwd as Password, MROLE.IDROLE as RoleID, MROLE.NAMA as RoleName, SIF_Pegawai.Kode_Pegawai as PegawaiID, SIF_Pegawai.Nama_Pegawai as NamaPegawai, SIF_cabang.kd_cabang as BranchID, SIF_cabang.nama AS Branch, SIF_cabang.alamat  as Alamat, " +
                            " SIF_cabang.keterangan AS JenisUsaha, SIF_cabang.fax1 as Telp, SIF_cabang.fax2 as WA  " +
                            " FROM MUSER " +
                            " INNER JOIN MUSER_ROLE ON MUSER_ROLE.IDUSER = MUSER.userid " +
                            " INNER JOIN MROLE ON MROLE.IDROLE = MUSER_ROLE.IDROLE " +
                            " INNER JOIN SIF_Pegawai ON SIF_Pegawai.Kode_Pegawai = MUSER.nama " +
                            " INNER JOIN SIF_cabang ON SIF_cabang.kd_cabang = SIF_Pegawai.Kd_Cabang " +
                            " WHERE MUSER.rec_stat = 'Y' AND userid = @UserID AND  passwd=@Password  ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", auth.UserID);
                param.Add("@Password", auth.Password);



                var res = con.Query<Auth>(sql, param);

                return res.FirstOrDefault();
            }
        }

        public static IEnumerable<Auth> getAuthOTP(string pass)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select Val_kode1 as [Password] from SIF_Gen_Reff_D where Ref_role='SIF' and id_ref_file='OTP' AND id_ref_data='OTP'" +
                    "and Val_kode1=@otp ";


                DynamicParameters param = new DynamicParameters();
              
                param.Add("@otp", pass);



                var res = con.Query<Auth>(sql, param);

                return res;
            }
        }

        public static AuthenticationVM getAuthLoginMobile(AuthenticationVM auth)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT MUSER.userid as userID, MUSER.passwd as Password, MROLE.IDROLE as role, MROLE.NAMA as RoleName, SIF_Pegawai.Kode_Pegawai as employeeID, SIF_Pegawai.Nama_Pegawai as userDisplay, SIF_Pegawai.Nama_Pegawai as name, SIF_cabang.kd_cabang as areaID, SIF_cabang.nama AS areaDesc, SIF_cabang.alamat  as Alamat,  SIF_cabang.keterangan AS JenisUsaha, SIF_cabang.fax1 as Telp, SIF_cabang.fax2 as WA, token, IMEI   " +
                            " FROM MUSER " +
                            " INNER JOIN MUSER_ROLE ON MUSER_ROLE.IDUSER = MUSER.userid " +
                            " INNER JOIN MROLE ON MROLE.IDROLE = MUSER_ROLE.IDROLE " +
                            " INNER JOIN SIF_Pegawai ON SIF_Pegawai.Kode_Pegawai = MUSER.nama " +
                            " INNER JOIN SIF_cabang ON SIF_cabang.kd_cabang = SIF_Pegawai.Kd_Cabang " +
                            " WHERE MUSER.rec_stat = 'Y' AND userid = @UserID AND passwd = @Password  ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", auth.userID);
                param.Add("@Password", auth.password);



                var res = con.Query<AuthenticationVM>(sql, param);

                return res.FirstOrDefault();
            }
        }

        public static int UpdateUserToken(AuthenticationVM data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [SIF].[dbo].[MUSER] WITH(ROWLOCK) SET token=@token, IMEI=@IMEI, " +
                " Last_update_date=@Last_update_date, Last_updated_by=@Last_updated_by WHERE userID=@userID;";
            param = new DynamicParameters();
            param.Add("@token", data.token);
            param.Add("@IMEI", data.IMEI);
            param.Add("@Last_update_date", DateTime.Now);
            param.Add("@Last_updated_by", data.userID);
            param.Add("@userID", data.userID);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public IEnumerable<AuthenticationVM> getTokenByRole(string  role)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT token,USERID, IDROLE AS role FROM MUSER M " +
                    " INNER JOIN MUSER_ROLE R ON M.userid = R.IDUSER " +
                    " WHERE IDROLE = @role ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@role", role);
                var res = con.Query<AuthenticationVM>(sql, param);

                return res.ToList();
            }
        }
    }
}
