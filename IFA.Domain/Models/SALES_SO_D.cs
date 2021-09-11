using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class SALES_SO_D
    {
     
        public string Kd_Cabang { get; set; }

        public string jns_paket { get; set; }

        public string tipe_trans { get; set; }
    
        public string No_sp { get; set; }
  
        public string No_seq { get; set; }
     
        public string Kd_Stok { get; set; }
      
        public decimal Qty { get; set; }

        public decimal? QtyCetak { get; set; }

        public decimal? Qty_book { get; set; }

        public decimal? PriceList { get; set; }

        public decimal? harga { get; set; }

        public decimal? vol { get; set; }

        public DateTime? tgl_kirim { get; set; }

        public DateTime? tgl_inden { get; set; }


        public string prioritas { get; set; }

        public string Kd_satuan { get; set; }

        public string Jns_service { get; set; }

        public decimal? qty_prod { get; set; }

        public decimal? stok { get; set; }

        public decimal? qty_kirim { get; set; }

        public decimal? Qty_sisa { get; set; }

        public string Keterangan { get; set; }

        public decimal? disc1 { get; set; }

        public decimal? disc2 { get; set; }

        public decimal? disc3 { get; set; }

        public decimal? disc4 { get; set; }

        public decimal? disc5 { get; set; }

        public decimal? potongan { get; set; }

        public decimal? potongan_total { get; set; }

        public string Status { get; set; }

        public string departemen { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Programe_name { get; set; }

        public string kd_parent { get; set; }

        public string No { get; set; }

        public string set_paket { get; set; }

        public string ambil_bonus { get; set; }

        public string Deskripsi { get; set; }

        public decimal? key_bonus { get; set; }

        public string nama_potongan { get; set; }

        public int? thnbuat { get; set; }

        public DateTime? Tgl_Kirim_Marketing { get; set; }

        public int? Nomor_Bonus { get; set; }

        public string Bom_Service { get; set; }

        public int? qty_batal { get; set; }

        public string jenis_so { get; set; }

        public string Status_Inspeksi { get; set; }

        public string reff { get; set; }

        public string CONFIRMED { get; set; }

        public decimal? BIAYA_SERVICE { get; set; }

        public string KOMPLAIN { get; set; }

        public string NO_REFF { get; set; }

        public short? sudah_qc { get; set; }

        public string Bonus { get; set; }

        public string pending { get; set; }

        public string No_Paket { get; set; }

        public string Status_Simpan { get; set; }

        public string sudahPO { get; set; }

        public string STATUS_DO { get; set; }

        public string isClosed { get; set; }

        public decimal? QtyLastCetak { get; set; }
        public string kode_Barang { get; set; }
        public string nama_Barang { get; set; }

        public decimal total_qty { get; set; }
        public string nama_Sales { get; set; }

        public string nama_Customer { get; set; }
        public string satuan { get; set; }
        public string total { get; set; }
        public bool flagbonus { get; set; }
        public Guid no_booked { get; set; }
        public decimal? diskon { get; set; }
        public string blthn { get; set; }
        public string kd_gudang { get; set; }
        public bool tunda { get; set; }

        public decimal qty_order { get; set; }
        public decimal qty_retur { get; set; }
        public decimal qty_sisa_krm { get; set; }
        public decimal qty_awal { get; set; }
        public decimal alokasi { get; set; }
        public string qty_available { get; set; }
        public decimal qty_alokasi { get; set; }
        public decimal qty_alocated { get; set; }


    }

    public class SALES_SO_DVM
    {
        public string kode_Barang { get; set; }
        public string nama_Barang { get; set; }
        public string satuan { get; set; }
        public decimal stok { get; set; }
        public decimal qty { get; set; }
        public decimal qty_awal { get; set; }
        public decimal harga { get; set; }
        public decimal total { get; set; }
        public string keterangan { get; set; }
        public bool flagbonus { get; set; }
        public decimal vol { get; set; }
        public decimal diskon { get; set; }
        public decimal disc1 { get; set; }
        public decimal disc2 { get; set; }
        public decimal disc3 { get; set; }

        public decimal disc4 { get; set; }
        public decimal qty_out { get; set; }
        public decimal ongkir { get; set; }
        public string No_sp { get; set; }


    }
}
