using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class SIF_BOOKING_PAKET_D
    {
        public string Kd_Cabang { get; set; }

        public string No_Paket { get; set; }

        public string No_seq { get; set; }

        public string Kd_Stok { get; set; }

        public decimal Qty { get; set; }

        public decimal harga { get; set; }

        public string Kd_satuan { get; set; }

        public string Keterangan { get; set; }

        public decimal? potongan { get; set; }

        public decimal? potongan_total { get; set; }

        public string Status { get; set; }

        public string departemen { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Programe_name { get; set; }

        public string kd_parent { get; set; }

        public string No { get; set; }

        public string set { get; set; }

        public string ambil_bonus { get; set; }

        public string Deskripsi { get; set; }

    }
}
