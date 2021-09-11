using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class SIF_Kendaraan
    {
        public string Kd_Cabang { get; set; }

        public string Kode_Kendaraan { get; set; }

        public string Nama_Kendaraan { get; set; }

        public string Keterangan { get; set; }

        public double? Kapasitas { get; set; }

        public double? Kapasitas_m3 { get; set; }

        public string No_Polisi { get; set; }

        public string Rec_Stat { get; set; }

        public string jns_kendaraan { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

    }
}
