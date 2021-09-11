using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class SIF_buku_pusat
    {
        public string Kd_Cabang { get; set; }

        public string kd_buku_pusat { get; set; }

        public string nm_buku_pusat { get; set; }

        public string grup_header { get; set; }

        public string tipe_rek { get; set; }

        public string grup_level1 { get; set; }

        public string grup_level2 { get; set; }

        public string grup_level3 { get; set; }

        public string kd_departemen { get; set; }

        public string Rec_Stat { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

    }

}
