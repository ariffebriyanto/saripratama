using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class V_MonStok
    {
    public int nomor { get; set; }
    public string kd_stok { get; set; }

    public string Kd_Satuan { get; set; }

    public string Nama_Barang { get; set; }

    public string Kode_Barang { get; set; }

    public string spek_brg { get; set; }

    public string tipe_stok { get; set; }

    public string Kd_Depart { get; set; }

    public string kd_jns_persd { get; set; }

    public string kd_jenis { get; set; }

    public string kd_merk { get; set; }

    public string kd_tipe { get; set; }

    public string kd_sub_tipe { get; set; }

    public string kd_ukuran { get; set; }

    public string kd_kain { get; set; }

    public decimal stok_min { get; set; }

    public string no_urut { get; set; }

    public decimal? stok_max_av { get; set; }

    public decimal? lead_time { get; set; }

    public decimal? tinggi { get; set; }

    public decimal? lebar { get; set; }

    public decimal? panjang { get; set; }

    public decimal? awal_qty_onstok { get; set; }

    public decimal? qty_onstok_in { get; set; }

    public decimal? qty_onstok_out { get; set; }

    public decimal? akhir_qty_onstok { get; set; }

    public decimal? akhir_qty_prod { get; set; }

    public decimal? akhir_qty_kony { get; set; }

    public decimal? qty_available_asli { get; set; }

    public decimal? qty_available { get; set; }

    public decimal akhir_booked { get; set; } //qty_akhir_expedisi
        public decimal qty_akhir_expedisi { get; set; } //

        public string bultah { get; set; }

    public string Nama_Persediaan { get; set; }
    public string Nama_Merk { get; set; }
    public string Nama_Jenis { get; set; }
    public string Nama_Tipe { get; set; }
    public string Status_Stok { get; set; }

    public string kategori { get; set; }
    public string sub_kategori { get; set; }
  


    }

}
