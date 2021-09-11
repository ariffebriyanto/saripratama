using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class SIF_Barang
    {
        public string Kd_Cabang { get; set; }

        public string Kode_Barang { get; set; }

        public string Kd_Satuan { get; set; }

        public string Nama_Barang { get; set; }

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

        public string no_urut { get; set; }

        public string lokasi { get; set; }

        public decimal? stok_min { get; set; }

        public decimal? stok_max_av { get; set; }

        public decimal? lead_time { get; set; }

        public string Keterangan { get; set; }

        public string rek_penjualan1 { get; set; }

        public string rek_penjualan2 { get; set; }

        public string rek_persediaan { get; set; }

        public string rek_biaya { get; set; }

        public string rek_pusat_biaya { get; set; }

        public string rek_hpp { get; set; }

        public string Rec_Stat { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public string rek_retur1 { get; set; }

        public string rek_retur2 { get; set; }

        public string rek_bonus1 { get; set; }

        public string rek_bonus2 { get; set; }

        public string kd_barang_bom { get; set; }

        public decimal? tinggi { get; set; }

        public decimal? lebar { get; set; }

        public decimal? panjang { get; set; }

        public decimal? volume { get; set; }

        public decimal? rata_pakai { get; set; }

        public string nm_jual { get; set; }

        public string update_kode { get; set; }

        public decimal? Stok_Min_J8 { get; set; }

        public decimal? Stok_Min_B2 { get; set; }

        public decimal? Stok_Min_Klampis { get; set; }
        public decimal? harga { get; set; }
        public decimal? berat { get; set; }


        //add master barang
        public string nm_rek_persediaan { get; set; }
        public string nm_rek_hpp { get; set; }
        public string nm_rek_penjualan1 { get; set; }
        public string nm_rek_penjualan2 { get; set; }
        public string nm_rek_retur1 { get; set; }
        public string nm_rek_retur2 { get; set; }
        public string nm_rek_bonus1 { get; set; }
        public string nm_rek_bonus2 { get; set; }



    }
    public class SIF_BarangCbo
    {
        public string Kode_Barang { get; set; }
        public string Nama_Barang { get; set; }
        public string kd_jenis { get; set; }
        public string Kd_Satuan { get; set; }
        public decimal? qty_data { get; set; }
        public string rek_persediaan { get; set; }
        public decimal? harga { get; set; }
        public decimal last_price { get; set; }
        
    }

    public class SIF_BarangGudangCbo
    {
        public string Kode_Barang { get; set; }
        public string Nama_Barang { get; set; }

    }
    public class SIF_PersediaanCbo
    {
        public string rek_persediaan { get; set; }
        public string nm_rek_persediaan { get; set; }

    }
    public class rek_penjualan2Cbo
    {
        public string rek_penjualan { get; set; }
        public string nm_rek_penjualan2 { get; set; }

    }

    public class SIF_BkBesarCbo
    {
        public string kd_buku_besar { get; set; }
        public string nm_buku_besar { get; set; }

    }

    public class SIF_kategori
    {
        public string kd_buku_besar { get; set; }
        public string nm_buku_besar { get; set; }

    }
}
