using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class FIN_JURNAL_D
    {
        public string Kd_Cabang { get; set; }

        public string tipe_trans { get; set; }

        public string no_jur { get; set; }

        public int no_seq { get; set; }

        public string kd_buku_besar { get; set; }

        public string kartu { get; set; }

        public string kd_buku_pusat { get; set; }

        public DateTime? tgl_valuta { get; set; }

        public string kd_valuta { get; set; }

        public decimal? kurs_valuta { get; set; }

        public decimal? saldo_val_debet { get; set; }

        public decimal? saldo_val_kredit { get; set; }

        public decimal? saldo_rp_debet { get; set; }

        public decimal? saldo_rp_kredit { get; set; }

        public string keterangan { get; set; }

        public string status { get; set; }

        public string prev_no_inv { get; set; }

        public string kd_obyek { get; set; }

        public string no_ref1 { get; set; }

        public string no_ref2 { get; set; }

        public string no_ref3 { get; set; }

        public decimal? val_ref1 { get; set; }

        public decimal? harga { get; set; }

        public decimal? tinggi { get; set; }

        public decimal? lebar { get; set; }

        public decimal? panjang { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Programe_name { get; set; }
        public string rekening { get; set; }
        public string barang { get; set; }

        public string nm_buku_besar { get; set; }

        public string nm_buku_pusat { get; set; }

        public string Nama_Barang { get; set; }


    }
}
