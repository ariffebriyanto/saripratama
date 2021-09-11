using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class SIF_BOOKING_PAKET
    {
        public string Kd_Cabang { get; set; }

        public string No_Paket { get; set; }

        public DateTime? Tgl_Paket { get; set; }

        public string Nama_Paket { get; set; }

        public DateTime? Tgl_Akhir_Paket { get; set; }

        public decimal? Harga_Paket { get; set; }

        public string Status_Aktif { get; set; }

        public decimal? Total_qty { get; set; }

        public string Departement { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public decimal? Biaya { get; set; }

        public string Status { get; set; }

        public string Tgl_Paketdesc { get; set; }
        public string Tgl_Akhir_Paketdesc { get; set; }
        public List<SIF_BOOKING_PAKET_D> details { get; set; }
    }
}
