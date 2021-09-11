using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class FIN_KOMPENSASI
    {
        public string Kd_Cabang { get; set; }

        public string tipe_trans { get; set; }

        public string no_trans { get; set; }

        public int no_seq { get; set; }

        public string kd_buku_besar { get; set; }

        public string kd_kartu { get; set; }

        public decimal? jml_bayar { get; set; }

        public string keterangan { get; set; }

        public string status { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Programe_name { get; set; }

        public string kd_buku_pusat { get; set; }

    }

}
