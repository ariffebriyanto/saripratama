using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class SALES_SJ_D
    {
     
        public string Kd_cabang { get; set; }

        public string tipe_trans { get; set; }
        
        public string no_sj { get; set; }
        public string no_sj2 { get; set; }

        public decimal no_seq { get; set; }
       
        public string No_sp { get; set; }

        public string no_seq_kirim { get; set; }

        public string no_seq_box { get; set; }

        public string Kd_stok { get; set; }

        public string Kd_satuan { get; set; }
       
        public decimal Qty_kirim { get; set; }
     
        public decimal qty_out { get; set; }

        public decimal qty_sisa_out { get; set; }
        [Required]
        public decimal Qty_balik { get; set; }

        public string Keterangan { get; set; }

        public string Status { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Program_name { get; set; }

        public string Kirim_Ulang { get; set; } 
        public string nama_barang { get; set; }
        public string nama_cust { get; set; }
        public string alamat_cust { get; set; }
        public string No_Gudang_Out { get; set; }
        public string no_dpb { get; set; }
        public short? sudah_qc { get; set; }
        public string gudang_tujuan { get; set; }
        public string no_gdin { get; set; }

    }

    public class FilterSALES_SJ
    {
        public string no_sj { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string status_sj { get; set; }
        public string barang { get; set; }

    }

    public class PrintSJ
    {
        public string Cabang { get; set; }
        public string Telp1 { get; set; }
        public string Kota { get; set; }
        public string AlamatCabang { get; set; }
        public string SJNumber { get; set; }
        public string TanggalSJ { get; set; }
        public string StatusSJ { get; set; }
        public string AlamatPengiriman { get; set; }
        public string TanggalJatuhTempo { get; set; }
        public string TanggalKirim { get; set; }
        public string NamaCustomer { get; set; }
        public string AlamatCustomer { get; set; }
        public string TelpCustomer { get; set; }
        public string no_pol { get; set; }
        public string sopir { get; set; }
        public string Notes { get; set; }
        public decimal PPN { get; set; }
        public decimal Ongkir { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
