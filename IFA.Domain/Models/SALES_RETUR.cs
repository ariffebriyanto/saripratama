using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class SALES_RETUR
    {
        [Required]
        public string Kd_Cabang { get; set; }
        [Required]
        public string No_retur { get; set; }

        public string Tipe_trans { get; set; }

        public DateTime? Tgl_retur { get; set; }

        public string No_ref1 { get; set; }

        public string No_ref2 { get; set; }

        public string Kd_Customer { get; set; }

        public string Nama_agen { get; set; }

        public string Nama_Paket { get; set; }

        public DateTime? Tgl_tarik { get; set; }

        public string Kd_sales { get; set; }

        public string Keterangan { get; set; }
        [Required]
        public decimal? Total_qty { get; set; }

        public string Status { get; set; }

        public string departemen { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public string Jenis_Retur { get; set; }

        public int? CetakKe { get; set; }

        public string isPrinted { get; set; }

        public string No_ref3 { get; set; }

        public decimal? jml_rp_trans { get; set; }

        public string flag_ppn { get; set; }
        public List<SALES_RETUR_D> details { get; set; }

    }

    public class InvRetur
    {
        public string no_ref2 { get; set; }
        public string nm_cust { get; set; }
        public string no_inv { get; set; }
        public string atas_nama { get; set; }
        public string jenis_sp { get; set; } //alamat
        public string alamat { get; set; } //alamat
        public string Kd_Customer { get; set; }
    }

    public class InvReturDtl
    {
        public string No_sp { get; set; }

        public string Jenis_sp { get; set; }

        public string Kd_Stok { get; set; }

        public string Kd_satuan { get; set; }

        public string Nama_Barang { get; set; }

        public decimal Qty { get; set; }

        public decimal? qty_nota { get; set; }

        public decimal qty_retur { get; set; }

        public string no_inv { get; set; }

        public string no_jurnal { get; set; }

        public decimal? harga { get; set; }

        public decimal nilai_hpp { get; set; }

        public decimal? total { get; set; }

        public string Kd_Customer { get; set; }
        public string Atas_Nama { get; set; }

        public decimal? qty_total { get; set; }

        public string Keterangan { get; set; }

        public decimal? disc1 { get; set; }

        public decimal? disc2 { get; set; }

        public decimal? disc3 { get; set; }

        public decimal? disc4 { get; set; }

        public decimal? potongan { get; set; }

        public decimal? potongan_total { get; set; }

        public string Bonus { get; set; }
        public decimal retur_total { get; set; }
        public decimal qty_available { get; set; }

    }

}
