using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class FIN_PEMBELIAN_D
    {
        public string Kd_Cabang { get; set; }

        public string tipe_trans { get; set; }

        public string no_inv { get; set; }

        public int no_seq { get; set; }

        public string kd_stok { get; set; }

        public string satuan { get; set; }

        public decimal? qty { get; set; }

        public decimal? harga { get; set; }

        public decimal? jml_diskon { get; set; }

        public decimal? jml_val_trans { get; set; }

        public decimal? jml_rp_trans { get; set; }

        public string kd_buku_besar { get; set; }

        public string keterangan { get; set; }

        public string status { get; set; }

        public string kd_divisi { get; set; }

        public string prev_no_inv { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Programe_name { get; set; }

    }

}
