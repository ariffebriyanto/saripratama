using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class FIN_JURNAL
    {
        public string Kd_cabang { get; set; }

        public string tipe_trans { get; set; }

        public string no_jur { get; set; }

        public DateTime tgl_trans { get; set; }

        public string no_ref1 { get; set; }

        public string no_ref2 { get; set; }

        public string no_ref3 { get; set; }

        public string thnbln { get; set; }

        public string kd_kartu { get; set; }

        public string nama { get; set; }

        public string alamat { get; set; }

        public string kd_valuta { get; set; }

        public decimal kurs_valuta { get; set; }

        public decimal? jml_val_trans { get; set; }

        public int no { get; set; }

        public decimal? jml_rp_trans { get; set; }

        public string no_fakt_pajak { get; set; }

        public DateTime? tgl_posting { get; set; }

        public string no_posting { get; set; }

        public string keterangan { get; set; }

        public string rek_attribute1 { get; set; }

        public string rek_attribute2 { get; set; }

        public string cek_post { get; set; }

        public string verifikasi { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Program_name { get; set; }

        public int? jml_cetak { get; set; }

        public DateTime? tgl_cetak { get; set; }
        public string cabang { get; set; }
        public string tipe_desc { get; set; }
        public string nama_toko { get; set; }
        public string alamat_toko { get; set; }
        public string telp { get; set; }
        public string wa { get; set; }

        public string kartu { get; set; }
        public List<FIN_JURNAL_D> detail { get; set; }

    }

    public class FilterJurnal
    {
        public string no_jur { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string tipe_trans { get; set; }
      
    }

    public class vmonjurnal
    {
        public string Kd_cabang { get; set; }

        public string tipe_trans { get; set; }

        public string no_jur { get; set; }

        public DateTime tgl_trans { get; set; }

        public string no_ref1 { get; set; }

        public string no_ref2 { get; set; }

        public string no_ref3 { get; set; }

        public string thnbln { get; set; }

        public string kd_kartu { get; set; }

        public string nama { get; set; }

        public string alamat { get; set; }

        public string kd_valuta { get; set; }

        public decimal kurs_valuta { get; set; }

        public decimal? jml_val_trans { get; set; }

        public int no { get; set; }

        public decimal? jml_rp_trans { get; set; }

        public string no_fakt_pajak { get; set; }

        public DateTime? tgl_posting { get; set; }

        public string no_posting { get; set; }

        public string keterangan { get; set; }

        public string rek_attribute1 { get; set; }

        public string rek_attribute2 { get; set; }

        public string cek_post { get; set; }

        public string verifikasi { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Program_name { get; set; }

        public int? jml_cetak { get; set; }

        public DateTime? tgl_cetak { get; set; }
        public string cabang { get; set; }
        public string tipe_desc { get; set; }
        public string nama_toko { get; set; }
        public string alamat_toko { get; set; }
        public string telp { get; set; }
        public string wa { get; set; }

        public string kartu { get; set; }

        public List<FIN_JURNAL_D> detail { get; set; }

    }
}
