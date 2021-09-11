using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class Shared
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class PaketVM
    {
        public string no_paket { get; set; }
        public string nama_paket{ get; set; }
      

    }

    public class PaketVMList
    {
        public string no_paket { get; set; }
        public string nama_paket { get; set; }
        public string kd_stok { get; set; }
        public int qty { get; set; }
        public decimal harga { get; set; }
        public decimal potongan_total { get; set; }
       public decimal total { get; set; }

        public string kd_satuan { get; set; }
        public string status { get; set; }
        public string deskripsi { get; set; }



    }

    public class AuthVM
    {
        public string password { get; set; }
      


    }

    public class CustomerVM
    {
        public string Nama_Customer { get; set; }
        public string nama_customer2 { get; set; }
        public string Kd_Customer { get; set; }
        public string Alamat1 { get; set; }
        public string Kota1 { get; set; }
        public string Alamat2 { get; set; }
        public string Kota2 { get; set; }
        public string Kd_Wilayah { get; set; }
        public string Kode_Area { get; set; }
        public decimal CreditLimit { get; set; }
        public int jatuh_tempo { get; set; }
        public int jatuh_tempo2 { get; set; }
        public string sts_group { get; set; }
        public string no_telepon1 { get; set; }
        public string Rec_Stat { get; set; }
        public decimal saldo_limit { get; set; }


    }

    public class BarangHargaVM
    {
        public string Kd_Satuan { get; set; }
        public string Nama_Barang { get; set; }
        public string Harga_Dollar { get; set; }
        public decimal Harga_Rupiah { get; set; }
        public decimal harga_rupiah2 { get; set; }
        public decimal harga_rupiah3 { get; set; }
        public decimal harga_rupiah4 { get; set; }
        public string Kode_Barang { get; set; }
        public int isset { get; set; }
        public decimal stok { get; set; }
        public decimal vol { get; set; }
        public int batas1 { get; set; }
        public int batas2 { get; set; }
        public int batas3 { get; set; }
        public string nama_cabang { get; set; }

    }

    public class KasirVM
    {
        public string Nama_Sales { get; set; }
        public string Kode_Sales { get; set; }
        public string Kode_Pegawai { get; set; }

    }

    public class SaldoVM
    {
        public int Saldo_Awal { get; set; }
        public string kd_rekening { get; set; }
        public string kd_valuta { get; set; }
        public string tahun { get; set; }

        public string bulan { get; set; }

        



    }

    public class SaldoVM1
    {
        
        public string no { get; set; }
        public string no_jur { get; set; }
        public DateTime tgl_trans { get; set; }

        public DateTime tgl_posting { get; set; }
        public string tipe_desc { get; set; }

        public string no_ref1 { get; set; }
        public string no_ref3 { get; set; }
        public string nama { get; set; }

        public string keterangan { get; set; }

        public string kd_buku_besar { get; set; }

        public string nm_buku_besar { get; set; }

        public decimal saldo_val_debet { get; set; }

        public decimal saldo_val_kredit { get; set; }



    }



    public class Kendaraan
    {
        public string Kode_Kendaraan { get; set; }
        public string Nama_Kendaraan { get; set; }
       

    }

    public class SIF_Harga
    {
        public string no_trans { get; set; }
        public string Kd_Cabang { get; set; }

        public string Kode_Barang { get; set; }

        public DateTime Start_Date { get; set; }

        public DateTime? End_Date { get; set; }

        public decimal? Harga_Dollar { get; set; }

        public string Rec_Stat { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public string kd_merk { get; set; }

        public string kd_tipe { get; set; }

        public string keterangan { get; set; }

        public decimal? Harga_Awal { get; set; }

        public decimal? Diskon { get; set; }

        public decimal? Harga_Rupiah { get; set; }

        public decimal? Harga_Rupiah2 { get; set; }

        public decimal? Harga_Rupiah3 { get; set; }

        public decimal? Harga_Rupiah4 { get; set; }

        public int? qty_harga1_min { get; set; }

        public int? qty_harga2_min { get; set; }

        public int? qty_harga3_min { get; set; }

        public int? qty_harga4_min { get; set; }

        public int? qty_harga1_max { get; set; }

        public int? qty_harga2_max { get; set; }

        public int? qty_harga3_max { get; set; }

        public int? qty_harga4_max { get; set; }

        public string nama_Barang { get; set; }
        public decimal? Harga_RupiahOld { get; set; }
        public decimal? Harga_RupiahOld2 { get; set; }
        public decimal? Harga_RupiahOld3 { get; set; }


        public string Selisih { get; set; }
        public string Selisih2 { get; set; }
        public string Selisih3 { get; set; }

    }

    public class pushnotif
    {
        public string title { get; set; }
        public string body { get; set; }
        public string landing_page { get; set; }
        public string to { get; set; }
    }

    public class SIF_Valuta_VM
    {
        public string Kode_Valuta { get; set; }
        public string Nama_Valuta { get; set; }
    }

    public class SIF_Gen_Reff_D_VM
    {
        public string Id_Data { get; set; }
        public string Desc_Data { get; set; }
    }

    public class SIF_Bank_VM
    {
        public string kd_bank { get; set; }
        public string nama_bank { get; set; }
    }

    public class DashboardVM
    {
        public int no { get; set; }
        public string desc1 { get; set; }
        public string desc2 { get; set; }
        public string desc3 { get; set; }
        public decimal total { get; set; }


    }

    public class Filter
    {
        public string cb { get; set; } //kode cabang
        public string id { get; set; } ////kode trans
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string stat { get; set; } //biasanya Y or N
        public string kd_brg { get; set; }
        public string TipeTrans { get; set; }

    }

    public class SortDescription
    {
        public string field { get; set; }
        public string dir { get; set; }
    }

    public class FilterContainer
    {
        public List<FilterDescription> filters { get; set; }
        public string logic { get; set; }
    }

    public class FilterDescription
    {
        public string @operator { get; set; }
        public string field { get; set; }
        public string value { get; set; }
    }

}
