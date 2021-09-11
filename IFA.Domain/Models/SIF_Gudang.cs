using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class SIF_Gudang
    {
        public string Kd_Cabang { get; set; }

        public string Kode_Gudang { get; set; }

        public string Nama_Gudang { get; set; }

        public string Keterangan { get; set; }

        public string Rec_Stat { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public string divisi { get; set; }

    }

}
