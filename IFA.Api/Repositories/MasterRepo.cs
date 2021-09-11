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
    public class MasterRepo
    {
        public static async Task<int> SaveCustomer(SIF_CUSTOMER data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF_CUSTOMER   (Kd_Cabang, Kd_Customer, Nama_Customer, Tgl_Lahir, " +
                " Alamat1, Kota1, No_Telepon1, nama_customer2, Alamat2, Kota2, No_Telepon2, No_Fax, nm_npwp, " +
                " No_NPWP, Email, Jenis_Usaha, Kontak_Person, Grade, Kd_Wilayah, Kode_Area, Keterangan, " +
                " Norek_Bank_Rupiah, Norek_Bank_Valas, Limit_Piutang_Rupiah, Limit_Piutang_Valas, Rec_Stat, " +
                " sts_group, Attribute1, Attribute2, Attribute3, Attribute4, Attribute5, Last_Create_Date, " +
                " Last_Created_By, Program_Name, jatuh_tempo, " +
                " jatuh_tempo2, flag_overlimit) " +
                    "VALUES(@Kd_Cabang,@Kd_Customer,@Nama_Customer,@Tgl_Lahir,@Alamat1,@Kota1,@No_Telepon1,@nama_customer2,@Alamat2,@Kota2,@No_Telepon2,@No_Fax,@nm_npwp,@No_NPWP,@Email,@Jenis_Usaha,@Kontak_Person,@Grade,@Kd_Wilayah,@Kode_Area,@Keterangan,@Norek_Bank_Rupiah,@Norek_Bank_Valas,@Limit_Piutang_Rupiah,@Limit_Piutang_Valas,@Rec_Stat,@sts_group,@Attribute1,@Attribute2,@Attribute3,@Attribute4,@Attribute5,@Last_Create_Date,@Last_Created_By,@Program_Name,@jatuh_tempo,@jatuh_tempo2,@flag_overlimit);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Kd_Customer", data.Kd_Customer);
            param.Add("@Nama_Customer", data.Nama_Customer);
            param.Add("@Tgl_Lahir", data.Tgl_Lahir);
            param.Add("@Alamat1", data.Alamat1);
            param.Add("@Kota1", data.Kota1);
            param.Add("@No_Telepon1", data.No_Telepon1);
            param.Add("@nama_customer2", data.nama_customer2);
            param.Add("@Alamat2", data.Alamat2);
            param.Add("@Kota2", data.Kota2);
            param.Add("@No_Telepon2", data.No_Telepon2);
            param.Add("@No_Fax", data.No_Fax);
            param.Add("@nm_npwp", data.nm_npwp);
            param.Add("@No_NPWP", data.No_NPWP);
            param.Add("@Email", data.Email);
            param.Add("@Jenis_Usaha", data.Jenis_Usaha);
            param.Add("@Kontak_Person", data.Kontak_Person);
            param.Add("@Grade", data.Grade);
            param.Add("@Kd_Wilayah", data.Kd_Wilayah);
            param.Add("@Kode_Area", data.Kode_Area);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@Norek_Bank_Rupiah", data.Norek_Bank_Rupiah);
            param.Add("@Norek_Bank_Valas", data.Norek_Bank_Valas);
            param.Add("@Limit_Piutang_Rupiah", data.Limit_Piutang_Rupiah);
            param.Add("@Limit_Piutang_Valas", data.Limit_Piutang_Valas);
            param.Add("@Rec_Stat", data.Rec_Stat);
            param.Add("@sts_group", data.sts_group);
            param.Add("@Attribute1", data.Attribute1);
            param.Add("@Attribute2", data.Attribute2);
            param.Add("@Attribute3", data.Attribute3);
            param.Add("@Attribute4", data.Attribute4);
            param.Add("@Attribute5", data.Attribute5);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@jatuh_tempo", data.jatuh_tempo);
            param.Add("@jatuh_tempo2", data.jatuh_tempo2);
            param.Add("@flag_overlimit", data.flag_overlimit);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> SaveBukuBesar(SIF_buku_besar data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF_CUSTOMER   (Kd_Cabang, Kd_Customer, Nama_Customer, Tgl_Lahir, " +
                " Alamat1, Kota1, No_Telepon1, nama_customer2, Alamat2, Kota2, No_Telepon2, No_Fax, nm_npwp, " +
                " No_NPWP, Email, Jenis_Usaha, Kontak_Person, Grade, Kd_Wilayah, Kode_Area, Keterangan, " +
                " Norek_Bank_Rupiah, Norek_Bank_Valas, Limit_Piutang_Rupiah, Limit_Piutang_Valas, Rec_Stat, " +
                " sts_group, Attribute1, Attribute2, Attribute3, Attribute4, Attribute5, Last_Create_Date, " +
                " Last_Created_By, Program_Name, jatuh_tempo, " +
                " jatuh_tempo2, flag_overlimit) " +
                    "VALUES(@Kd_Cabang,@Kd_Customer,@Nama_Customer,@Tgl_Lahir,@Alamat1,@Kota1,@No_Telepon1,@nama_customer2,@Alamat2,@Kota2,@No_Telepon2,@No_Fax,@nm_npwp,@No_NPWP,@Email,@Jenis_Usaha,@Kontak_Person,@Grade,@Kd_Wilayah,@Kode_Area,@Keterangan,@Norek_Bank_Rupiah,@Norek_Bank_Valas,@Limit_Piutang_Rupiah,@Limit_Piutang_Valas,@Rec_Stat,@sts_group,@Attribute1,@Attribute2,@Attribute3,@Attribute4,@Attribute5,@Last_Create_Date,@Last_Created_By,@Program_Name,@jatuh_tempo,@jatuh_tempo2,@flag_overlimit);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@grup_header", data.grup_header);
            param.Add("@grup_header", data.grup_header);
            param.Add("@cflow", data.cflow);
            param.Add("@pola_entry", data.pola_entry);
            param.Add("@tipe_rek", data.tipe_rek);
            param.Add("@kd_valuta", data.kd_valuta);
            param.Add("@nama_customer2", data.grup_level1);
            param.Add("@grup_level1", data.grup_level2);
            param.Add("@grup_level3", data.grup_level3);
            param.Add("@grup_level4", data.grup_level4);
            param.Add("@Rec_Stat", "Y");
            param.Add("@Last_Created_By", data.Last_Created_By);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> SaveRole(MUSER_ROLE data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF.dbo.MUSER_ROLE (IDUSER,IDROLE) " +
                    "VALUES(@IDUSER,@IDROLE);";
            param = new DynamicParameters();
            param.Add("@IDUSER", data.IDROLE);
            param.Add("@IDROLE", data.IDROLE);




            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> SaveSupplier(SIF_CUSTOMER data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF_Supplier   (Kd_Cabang, Kode_Supplier, Nama_Supplier, " +
                " Alamat1, Kota1, No_Telepon1, Last_Create_Date, Last_Created_By, Program_Name,Rec_Stat ) " +
                    "VALUES(@Kd_Cabang,@Kd_Customer,@Nama_Customer,@Alamat1,@Kota1,@No_Telepon1,@Last_Create_Date,@Last_Created_By,'Master_Supplier','Y');";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Kd_Customer", data.Kd_Customer);
            param.Add("@Nama_Customer", data.Nama_Customer);

            param.Add("@Alamat1", data.Alamat1);
            param.Add("@Kota1", data.Kota1);
            param.Add("@No_Telepon1", data.No_Telepon1);

            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateRole(MUSER_ROLE data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE SIF.dbo.MUSER_ROLE  SET  IDROLE=@IDROLE WHERE IDUSER=@IDUSER;";
            param = new DynamicParameters();
            param.Add("@IDROLE", data.IDROLE);
            param.Add("@IDUSER", data.IDUSER);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<IEnumerable<SIF_buku_besar>> GetSIFBukuBesar()
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                //string filter = "";
                string sql = "SELECT B.Kd_Cabang,B.kd_buku_besar,B.nm_buku_besar ,B.grup_header,B.cflow,B.pola_entry,B.tipe_rek,B.kd_valuta,V.Nama_Valuta, " +
                    "B.grup_level1,B.grup_level2,B.grup_level3,B.grup_level4,B.Rec_Stat,B.Last_Create_Date,B.Last_Created_By,B.Last_Update_Date,B.Last_Updated_By,B.Program_Name,B.div1,B.div2,B.div3,B.div4,B.div5 " +
                    "FROM [SIF].[dbo].[SIF_buku_besar] B WITH (NOLOCK) INNER JOIN[SIF].[dbo].[SIF_Valuta] V WITH (NOLOCK) ON V.Kode_Valuta = B.kd_valuta where kd_buku_besar <> ''";
                var res = con.Query<SIF_buku_besar>(sql, param);

                return res;
            }
        }

        public static async Task<IEnumerable<SIF_Sopir>> GetSopir()
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                //string filter = "";
                string sql = "SELECT Kd_Cabang,Kode_Sopir,Kd_pegawai,Stat_job,Nama_Sopir,Keterangan,Rec_Stat,Last_Create_Date,Last_Created_By,Last_Update_Date,Last_Updated_By,Program_Name " +
                    "FROM [SIF].[dbo].[SIF_Sopir] ";
                var res = con.Query<SIF_Sopir>(sql, param);

                return res;
            }
        }

        public static async Task<IEnumerable<SIF_Kendaraan>> GetKendaraan()
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                //string filter = "";
                string sql = "SELECT Kd_Cabang,Kode_Kendaraan,Nama_Kendaraan,Keterangan,Kapasitas,Kapasitas_m3,No_Polisi,Rec_Stat,jns_kendaraan,Last_Create_Date,Last_Created_By,Last_Update_Date,Last_Updated_By,Program_Name " +
                    "FROM [SIF].[dbo].[SIF_Kendaraan] B WITH (NOLOCK) ";
                var res = con.Query<SIF_Kendaraan>(sql, param);

                return res;
            }
        }

        public static async Task<IEnumerable<SIF_Harga>> GetSIFHarga(string Kd_cabang)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                //string filter = "";
                string sql = "select h.Kode_Barang,b.Nama_Barang,h.[Start_Date],h.Harga_Rupiah,h.Harga_Rupiah as Harga_RupiahOld,h.Harga_Rupiah2,h.Harga_Rupiah2 as Harga_RupiahOld2,h.Harga_Rupiah3,h.Harga_Rupiah3 as Harga_RupiahOld3, h.Harga_Rupiah4,h.qty_harga1_min, " +
                             "h.qty_harga2_min,h.qty_harga3_min from SIF.dbo.SIF_Harga h WITH (NOLOCK) inner join SIF.dbo.SIF_barang b WITH (NOLOCK) on h.Kode_Barang = b.Kode_Barang " +
                             "where h.Kd_Cabang = @Kd_Cabang order by b.Nama_Barang ASC";

                param = new DynamicParameters();
                param.Add("@Kd_Cabang", Kd_cabang);

                var res = con.Query<SIF_Harga>(sql, param);
                return res;
            }
        }

        public static async Task<int> UpdateSIFHarga(SIF_Harga data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update [SIF].[dbo].[SIF_Harga] set Harga_Rupiah = @Harga_Rupiah, Harga_Rupiah2 = @Harga_Rupiah2 , Harga_Rupiah3=@Harga_Rupiah3,Harga_Rupiah4=@Harga_Rupiah4, " +
                    "Start_Date = @Start_Date ,qty_harga1_min=@qty_harga1_min,qty_harga2_min = @qty_harga2_min,qty_harga3_min=@qty_harga3_min," +
                    "Last_Update_Date=getdate(),Last_Updated_By=@Last_Updated_By where " +
                    "Kode_Barang = @Kode_Barang";
            param = new DynamicParameters();

            param.Add("@Kode_Barang", data.Kode_Barang);
            param.Add("@Start_Date", data.Start_Date);
            param.Add("@Harga_Rupiah", data.Harga_Rupiah);
            param.Add("@Harga_Rupiah2", data.Harga_Rupiah2);
            param.Add("@Harga_Rupiah3", data.Harga_Rupiah3);
            param.Add("@Harga_Rupiah4", data.Harga_Rupiah4);
            param.Add("@Kode_Barang", data.Kode_Barang);
            param.Add("@Start_Date", data.Start_Date);
            param.Add("@qty_harga1_min", data.qty_harga1_min);
            param.Add("@qty_harga2_min", data.qty_harga2_min);
            param.Add("@qty_harga3_min", data.qty_harga3_min);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            //param.Add("@qty_harga3_min", data.Last_Update_Date);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<IEnumerable<CustomerVM>> GetCustomerMobile()
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                //string filter = "";
                string sql = "select TOP 30 Kd_Cabang, Kd_Customer, Nama_Customer, Tgl_Lahir, Alamat1, Kota1, No_Telepon1, nama_customer2, Alamat2, Kota2, No_Telepon2, No_Fax, nm_npwp, No_NPWP, Email, Jenis_Usaha, Kontak_Person, Grade, " +
                    " Kd_Wilayah, Kode_Area, Keterangan, Norek_Bank_Rupiah, Norek_Bank_Valas, Limit_Piutang_Rupiah, Limit_Piutang_Valas, Rec_Stat, sts_group, Attribute1, Attribute2, Attribute3, Attribute4, Attribute5, Last_Create_Date, " +
                    " Last_Created_By, Last_Update_Date, Last_Updated_By, Program_Name, jatuh_tempo, jatuh_tempo2, flag_overlimit " +
                    " FROM SIF_Customer where rec_stat='Y' and Kd_Customer <> 'INTERNAL' " +
                             " ORDER BY Nama_Customer ASC";

                param = new DynamicParameters();

                var res = con.Query<CustomerVM>(sql, param);
                return res;
            }
        }

        public static async Task<IEnumerable<CustomerVM>> GetCustomer()
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                //string filter = "";
                string sql = "select Kd_Cabang, Kd_Customer, Nama_Customer,saldo_limit, Tgl_Lahir, Alamat1, Kota1, No_Telepon1, nama_customer2, Alamat2, Kota2, No_Telepon2, No_Fax, nm_npwp, No_NPWP, Email, Jenis_Usaha, Kontak_Person, Grade, " +
                    " Kd_Wilayah, Kode_Area, Keterangan, Norek_Bank_Rupiah, Norek_Bank_Valas, Limit_Piutang_Rupiah as creditLimit, Limit_Piutang_Valas, Rec_Stat, sts_group, Attribute1, Attribute2, Attribute3, Attribute4, Attribute5, Last_Create_Date, " +
                    " Last_Created_By, Last_Update_Date, Last_Updated_By, Program_Name, jatuh_tempo, jatuh_tempo2, flag_overlimit " +
                    " FROM SIF_Customer WITH (NOLOCK) where rec_stat='Y' and Kd_Customer <> 'INTERNAL' " +
                             " ORDER BY Nama_Customer ASC";

                param = new DynamicParameters();

                var res = con.Query<CustomerVM>(sql, param);
                return res;
            }
        }

        public static async Task<int> UpdateCustomer(CustomerVM data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE SIF_Customer   SET Nama_Customer=@Nama_Customer,Alamat1 = @Alamat1,No_Telepon1=@No_Telepon1, Limit_Piutang_Rupiah=@creditLimit, Limit_Piutang_Valas=@creditLimit,Rec_Stat=@Rec_Stat,jatuh_tempo=@jatuh_tempo,saldo_limit=@saldo_limit " +
                    " WHERE Kd_Customer=@Kd_Customer;";
            param = new DynamicParameters();
            param.Add("@Kd_Customer", data.Kd_Customer);
            param.Add("@Nama_Customer", data.Nama_Customer);
            param.Add("@Alamat1", data.Alamat1);
            param.Add("@No_Telepon1", data.no_telepon1);
            param.Add("@creditLimit", data.CreditLimit);
            param.Add("@jatuh_tempo", data.jatuh_tempo);
            param.Add("@saldo_limit", data.saldo_limit);
            param.Add("@Rec_Stat", data.Rec_Stat);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateBukuBesar(SIF_buku_besar data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE SIF.dbo.SIF_buku_besar   SET Kd_Cabang=@Kd_Cabang,grup_header = @grup_header,grup_header=@grup_header, cflow=@cflow, pola_entry=@pola_entry,tipe_rek=@tipe_rek,kd_valuta=@kd_valuta,grup_level1=@grup_level1,grup_level2=@grup_level2,grup_level4=@grup_level4,Rec_Stat=@Rec_Stat,Last_Updated_By=@Last_Updated_By,Last_Update_Date=GETDATE() " +
                    " WHERE kd_buku_besar=@kd_buku_besar;";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@kd_buku_besar", data.kd_buku_besar);
            param.Add("@nm_buku_besar", data.nm_buku_besar);
            param.Add("@grup_header", data.grup_header);
            param.Add("@cflow", data.cflow);
            param.Add("@pola_entry", data.pola_entry);
            param.Add("@tipe_rek", data.tipe_rek);
            param.Add("@kd_valuta", data.kd_valuta);
            param.Add("@nama_customer2", data.grup_level1);
            param.Add("@grup_level1", data.grup_level2);
            param.Add("@grup_level3", data.grup_level3);
            param.Add("@grup_level4", data.grup_level4);
            param.Add("@Rec_Stat", data.Rec_Stat);
            param.Add("@Last_Created_By", data.Last_Updated_By);



            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> DeleteCustomer(CustomerVM data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE FROM SIF.dbo.SIF_Customer  WHERE Kd_Customer=@Kd_Customer;";
            param = new DynamicParameters();
            param.Add("@Kd_Customer", data.Kd_Customer);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> DeletePegawai(string kd_peg, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE SIF.dbo.SIF_Pegawai SET Rec_Stat='N' WHERE Kode_Pegawai=@Kode_Pegawai;";
            param = new DynamicParameters();
            param.Add("@Kode_Pegawai", kd_peg);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> DeleteSALES(string kd_peg, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE SIF.dbo.SIF_Sales SET Rec_Stat='N' WHERE Kode_Pegawai=@Kode_Pegawai;";
            param = new DynamicParameters();
            param.Add("@Kode_Pegawai", kd_peg);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> DeleteUserRole(string kd_peg, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE SIF.dbo.MUSER_ROLE WHERE IDUSER=@Kode_Pegawai;";
            param = new DynamicParameters();
            param.Add("@Kode_Pegawai", kd_peg);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveListCustomer(CustomerVM data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();

            Query = "INSERT INTO SIF_Customer   (Kd_Customer,Nama_Customer,Alamat1,No_Telepon1,Limit_Piutang_Valas,Limit_Piutang_Rupiah,Rec_Stat,jatuh_tempo,saldo_limit) " +
                    "VALUES(@Kd_Customer,@Nama_Customer,@Alamat1,@No_Telepon1,@creditLimit,@creditLimit,@Rec_Stat,@jatuh_tempo,@creditLimit);";
            param = new DynamicParameters();
            param.Add("@Kd_Customer", data.Kd_Customer);
            param.Add("@Nama_Customer", data.Nama_Customer);
            param.Add("@Alamat1", data.Alamat1);
            param.Add("@No_Telepon1", data.no_telepon1);
            param.Add("@creditLimit", data.CreditLimit);
            param.Add("@jatuh_tempo", data.jatuh_tempo);
            param.Add("@saldo_limit", data.saldo_limit);
            param.Add("@Rec_Stat", "Y");
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<IEnumerable<SIF_Supplier>> GetSupplier()
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();

                string sql = "SELECT Kd_Cabang,Kode_Supplier,Nama_Supplier,Alamat1,Kota1,No_Telepon1,Rec_Stat,Last_Create_Date,Last_Created_By,Last_Update_Date,Last_Updated_By,Program_Name " +
                             "FROM[SIF].[dbo].[SIF_Supplier] ORDER BY Nama_Supplier ASC";

                param = new DynamicParameters();

                var res = con.Query<SIF_Supplier>(sql, param);
                return res;
            }
        }
        public static async Task<List<MUSER_ROLE>> GetRole()
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();

                string sql = "SELECT * FROM SIF.dbo.MUSER_ROLE";

                param = new DynamicParameters();

                var res = con.Query<MUSER_ROLE>(sql, param);
                return res.ToList();
            }
        }

        public static async Task<int> UpdateSupplier(SIF_Supplier data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE SIF_Supplier   SET Nama_Supplier=@Nama_Customer,Alamat1 = @Alamat1,No_Telepon1=@No_Telepon1 " +
                    " WHERE Kode_Supplier=@Kd_Supplier;";
            param = new DynamicParameters();
            param.Add("@Kd_Supplier", data.Kode_Supplier);
            param.Add("@Nama_Supplier", data.Nama_Supplier);
            param.Add("@Alamat1", data.Alamat1);
            param.Add("@No_Telepon1", data.No_Telepon1);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> DeleteSupplier(SIF_Supplier data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE FROM SIF_Supplier   WHERE Kode_Supplier=@Kd_Supplier;";
            param = new DynamicParameters();
            param.Add("@Kd_Supplier", data.Kode_Supplier);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveListSupplier(SIF_Supplier data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF_Supplier   (Kd_Cabang, Kode_Supplier, Nama_Supplier, " +
                " Alamat1, Kota1, No_Telepon1, Last_Create_Date, Last_Created_By, Program_Name,Rec_Stat ) " +
                    "VALUES(@Kd_Cabang,@Kd_Customer,@Nama_Customer,@Alamat1,@Kota1,@No_Telepon1,@Last_Create_Date,@Last_Created_By,'Master_Supplier','Y');";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Kd_Customer", data.Kode_Supplier);
            param.Add("@Nama_Customer", data.Nama_Supplier);

            param.Add("@Alamat1", data.Alamat1);
            param.Add("@Kota1", data.Kota1);
            param.Add("@No_Telepon1", data.No_Telepon1);

            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateSopir(SIF_Supplier data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE SIF_Supplier SET Nama_Supplier=@Nama_Customer,Alamat1 = @Alamat1,No_Telepon1=@No_Telepon1 " +
                    " WHERE Kode_Supplier=@Kd_Supplier;";
            param = new DynamicParameters();
            param.Add("@Kd_Supplier", data.Kode_Supplier);
            param.Add("@Nama_Supplier", data.Nama_Supplier);
            param.Add("@Alamat1", data.Alamat1);
            param.Add("@No_Telepon1", data.No_Telepon1);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> DeleteSopir(SIF_Sopir data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE FROM SIF.dbo.SIF_Sopir WHERE Kode_Sopir=@Kode_Sopir;";
            param = new DynamicParameters();
            param.Add("@Kode_Sopir", data.Kode_Sopir);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveListSopir(SIF_Sopir data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF.dbo.SIF_Sopir  (Kd_Cabang,Kode_Sopir,Kd_pegawai,Stat_job,Nama_Sopir,Keterangan,Rec_Stat,Last_Create_Date,Last_Created_By,Program_Name ) " +
                    "VALUES(@Kd_Cabang,@Kode_Sopir,@Kd_pegawai,@Stat_job,@Nama_Sopir,@Keterangan,'Y',GETDATE(),@Last_Created_By,@Program_Name);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Kode_Sopir", data.Kode_Sopir);
            param.Add("@Kd_pegawai", data.Kd_pegawai);

            param.Add("@Nama_Sopir", data.Nama_Sopir);
            param.Add("@Stat_job", data.Stat_job);
            param.Add("@Keterangan", data.Keterangan);

            param.Add("@Program_Name", data.Program_Name);
            param.Add("@Last_Created_By", data.Last_Created_By);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateKendaraan(SIF_Kendaraan data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE SIF.dbo.SIF_Kendaraan  SET Nama_Kendaraan=@Nama_Kendaraan,Keterangan = @Keterangan,No_Polisi=@No_Polisi " +
                    " WHERE Kode_Kendaraan=@Kode_Kendaraan;";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Kode_Kendaraan", data.Kode_Kendaraan);
            param.Add("@Nama_Kendaraan", data.Nama_Kendaraan);

            param.Add("@Keterangan", data.Keterangan);
            param.Add("@Kapasitas", data.Kapasitas);
            param.Add("@Kapasitas_m3", data.Kapasitas_m3);
            param.Add("@jns_kendaraan", data.jns_kendaraan);
            param.Add("@No_Polisi", data.No_Polisi);
            param.Add("@Rec_Stat", data.Rec_Stat);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> DeleteKendaraan(SIF_Kendaraan data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE FROM SIF.dbo.SIF_Kendaraan WHERE Kode_Supplier=@Kode_Kendaraan;";
            param = new DynamicParameters();
            param.Add("@Kode_Kendaraan", data.Kode_Kendaraan);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveListKendaraan(SIF_Kendaraan data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF.dbo.SIF_Kendaraan (Kd_Cabang,Kode_Kendaraan,Nama_Kendaraan,Keterangan,Kapasitas,Kapasitas_m3,No_Polisi,Rec_Stat,jns_kendaraan,Last_Create_Date,Last_Created_By,Program_Name ) " +
                    "VALUES(@Kd_Cabang,@Kode_Kendaraan,@Nama_Kendaraan,@Keterangan,@Kapasitas,@Kapasitas_m3,@No_Polisi,@Rec_Stat,@jns_kendaraan,GETDATE(),@Last_Created_By,@Program_Name);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Kode_Kendaraan", data.Kode_Kendaraan);
            param.Add("@Nama_Kendaraan", data.Nama_Kendaraan);

            param.Add("@Keterangan", data.Keterangan);
            param.Add("@Kapasitas", data.Kapasitas);
            param.Add("@Kapasitas_m3", data.Kapasitas_m3);
            param.Add("@jns_kendaraan", data.jns_kendaraan);
            param.Add("@No_Polisi", data.No_Polisi);
            param.Add("@Rec_Stat", data.Rec_Stat);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
    }
}
