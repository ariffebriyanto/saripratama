using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class SIF_Pet
    {
        public string Kd_Cabang { get; set; }

        public string Kd_Pet { get; set; }

        public string Nama_Pet { get; set; }

        public string Jenis_Pet { get; set; }

        public string Harga_Pet { get; set; }

        public string JenisPet { get; set; }

        public string NamaOwner { get; set; }

        public string Kd_Owner { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }
    }
}
