using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class FIN_NOTA_D
    {
        [Required]
        public string Kd_Cabang { get; set; }

        public string tipe_trans { get; set; }
        [Required]
        public string no_inv { get; set; }

        public string no_sj { get; set; }

        public string no_sp { get; set; }

        public int no_seq { get; set; }

        public string kd_stok { get; set; }

        public string satuan { get; set; }

        public decimal? Qty { get; set; }

        public decimal? harga { get; set; }

        public string Jns_service { get; set; }

        public decimal? disc1 { get; set; }

        public decimal? disc2 { get; set; }

        public decimal? disc3 { get; set; }

        public decimal? disc4 { get; set; }

        public decimal? disc5 { get; set; }

        public decimal? tot_diskon { get; set; }

        public decimal? jml_val_trans { get; set; }

        public decimal? jml_rp_trans { get; set; }

        public string kd_buku_besar { get; set; }

        public string keterangan { get; set; }

        public string status { get; set; }

        public string prev_no_inv { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Programe_name { get; set; }

        public decimal? tinggi { get; set; }

        public decimal? lebar { get; set; }

        public decimal? panjang { get; set; }

        public decimal? jml_ppn { get; set; }

        public string nama_barang { get; set; }

    }

}
