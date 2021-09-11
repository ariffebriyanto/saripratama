using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class FIN_PEMBELIAN
    {
        public string Kd_cabang { get; set; }

        public string tipe_trans { get; set; }

        public string no_inv { get; set; }

        public DateTime? tgl_inv { get; set; }

        public string no_ref1 { get; set; }

        public string no_ref2 { get; set; }

        public string no_ref3 { get; set; }

        public string thnbln { get; set; }

        public string kd_supplier { get; set; }

        public string nm_supplier { get; set; }

        public string almt_agen { get; set; }

        public string kd_valuta { get; set; }

        public decimal? kurs_valuta { get; set; }

        public decimal? jml_val_trans { get; set; }

        public decimal? jml_rp_trans { get; set; }

        public string flag_ppn { get; set; }

        public decimal? jml_val_ppn { get; set; }

        public decimal? jml_rp_ppn { get; set; }

        public string no_fakt_pajak { get; set; }

        public decimal? jml_diskon { get; set; }

        public decimal jml_tagihan { get; set; }

        public decimal? jml_bayar { get; set; }

        public decimal? jml_akhir { get; set; }

        public decimal? jml_akhir_tagihan { get; set; }


        public DateTime? tgl_jth_tempo { get; set; }

        public DateTime? tgl_posting { get; set; }

        public string no_posting { get; set; }

        public DateTime? tgl_batal { get; set; }

        public string prev_no_inv { get; set; }

        public string keterangan { get; set; }

        public string kd_buku_besar { get; set; }

        public string sts_jur { get; set; }

        public string no_jurnal { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Program_name { get; set; }

        public string status { get; set; }

        public DateTime? tgl_approve { get; set; }

        public string ket_approve { get; set; }

        public string no_sj { get; set; }

        public string sts_tagihan { get; set; }

        public decimal? ongkir { get; set; }

        public decimal? jml_bayar_pending { get; set; }
        public string nama { get; set; }
        public string alamat { get; set; }
        public string telp { get; set; }
        public string wa { get; set; }
        

        public List<FIN_PEMBELIAN_D> detail { get; set; }

    }

}
