using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class SIF_Sopir
    {
        public string Kd_Cabang { get; set; }

        public string Kode_Sopir { get; set; }

        public string Kd_pegawai { get; set; }

        public string Stat_job { get; set; }

        public string Nama_Sopir { get; set; }

        public string Keterangan { get; set; }

        public string Rec_Stat { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

    }
}
