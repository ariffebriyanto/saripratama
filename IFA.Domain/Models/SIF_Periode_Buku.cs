using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
   public class SIF_Periode_Buku
    {
        public string Kd_Cabang { get; set; }

        public string thn_buku { get; set; }

        public string bln_buku { get; set; }

        public string nama_bulan { get; set; }

        public string attrib1 { get; set; }

        public string attrib2 { get; set; }

        public string status_close { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }
    }

    public class Periode_Buku
    {
        public string thnbln { get; set; }

        public string nama { get; set; }

    }
}
