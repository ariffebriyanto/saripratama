using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace IFA.Domain.Models
{
    public class FIN_BELI_LUNAS_D
    {
        [Required]
        public string Kd_Cabang { get; set; }
        [Required]
        public string tipe_trans { get; set; }

        public string no_trans { get; set; }

        public int no_seq { get; set; }
        [Required]
        public string prev_no_inv { get; set; }

        public decimal? jml_tagihan { get; set; }

        public decimal? jml_piut { get; set; }

        public decimal? jml_ppn { get; set; }

        public decimal? jml_pph { get; set; }

        public decimal? jml_diskon { get; set; }

        public decimal? jml_admin { get; set; }

        public decimal? pendp_lain { get; set; }

        public decimal? biaya_lain { get; set; }

        public decimal? jml_pembulatan { get; set; }

        public decimal jml_bayar { get; set; }

        public string kd_buku_besar { get; set; }

        public string kd_kartu { get; set; }

        public string keterangan { get; set; }

        public string status { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Programe_name { get; set; }

    }
}
