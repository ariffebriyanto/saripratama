using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace IFA.Domain.Models
{
    public class FIN_NOTA_LUNAS
    {
        [Required]
        public string Kd_cabang { get; set; }
        [Required]
        public string tipe_trans { get; set; }
        [Required]
        public string no_trans { get; set; }

        public DateTime? tgl_trans { get; set; }

        public string no_ref1 { get; set; }

        public string no_ref2 { get; set; }

        public string no_ref3 { get; set; }

        public string thnbln { get; set; }
        [Required]
        public string kd_kartu { get; set; }

        public string kd_valuta { get; set; }

        public decimal? kurs_valuta { get; set; }

        public decimal? jml_val_trans { get; set; }

        public decimal? jml_rp_trans { get; set; }

        public decimal? jml_tagihan { get; set; }

        public decimal? jml_bayar { get; set; }

        public string Jns_bayar { get; set; }

        public string jns_giro_trans { get; set; }

        public string no_giro { get; set; }

        public decimal? kd_bank { get; set; }

        public DateTime? tgl_posting { get; set; }

        public string no_posting { get; set; }

        public DateTime? tgl_batal { get; set; }

        public string keterangan { get; set; }

        public string status { get; set; }

        public string kd_buku_besar { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Program_name { get; set; }

        public string no_do { get; set; }

        public decimal? jml_titipan { get; set; }

        public decimal? jml_giro { get; set; }

        public decimal? jml_transfer { get; set; }

        public decimal? jml_tunai { get; set; }

        public string no_batal { get; set; }

        public string nama { get; set; }
        public string alamat { get; set; }
        public string telp { get; set; }
        public string wa { get; set; }
        public List<FIN_NOTA_LUNAS_D> detail { get; set; }
        public List<FIN_GIRO> giro { get; set; }

    }
}
