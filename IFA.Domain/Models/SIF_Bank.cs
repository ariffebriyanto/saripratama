using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class SIF_Bank
    {
        public string Kd_Cabang { get; set; }

        public decimal kd_bank { get; set; }

        public string nama_bank { get; set; }

        public string alamat { get; set; }

        public string Kd_Bk_Besar { get; set; }

        public string no_rekening { get; set; }

        public string Rec_Stat { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

    }

}
