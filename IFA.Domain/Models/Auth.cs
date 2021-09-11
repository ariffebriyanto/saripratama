using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
   public class Auth
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string PegawaiID { get; set; }
        public string NamaPegawai { get; set; }
        public string BranchID { get; set; }
        public string Branch { get; set; }
        public string Alamat { get; set; }
        public string JenisUsaha { get; set; }
        public string Telp { get; set; }
        public string WA { get; set; }
        public string akses_penjualan { get; set; }

    }

    public class AuthenticationVM
    {
        public string userID { get; set; }
        public string userDisplay { get; set; }
        public string password { get; set; }
        public string employeeID { get; set; }
        public string name { get; set; }
        public string areaID { get; set; }
        public string areaDesc { get; set; }
        public string token { get; set; }
        public string role { get; set; }
        public string IMEI { get; set; }
    }
}
