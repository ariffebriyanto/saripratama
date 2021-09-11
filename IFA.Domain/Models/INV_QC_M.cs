using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class INV_QC_M
    {
        [Required]
        public string Kd_Cabang { get; set; }
        [Required]
        public string no_trans { get; set; }

        public DateTime? tgl_trans { get; set; }

        public string no_ref { get; set; }

        public byte? trx_stat { get; set; }

        public string keterangan { get; set; }

        public decimal? jml_rp_trans { get; set; }

        public string p_np { get; set; }

        public decimal? jml_ppn { get; set; }

        public string blthn { get; set; }

        public string sj_supplier { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public int? cetak_ke { get; set; }

        public string cetak_ulang { get; set; }

        public string penyerah { get; set; }

        public decimal? qty { get; set; }

        public string kd_stok { get; set; }
        public string kd_satuan { get; set; }
        public string kd_buku_besar { get; set; }
        public string Nama_Barang { get; set; }
        public string qty_po { get; set; }
        public string harga { get; set; }
        public decimal qty_qc_pass { get; set; }
        public decimal qty_order { get; set; }
        public string gudang_asal { get; set; }

        public int? no_seq { get; set; }



        public string gudang_tujuan { get; set; }
        public List<INV_QC> qcdetail { get; set; }


    }
    public class PrintQC
    {
        public string Cabang { get; set; }
        public string Telp1 { get; set; }
        public string Kota { get; set; }
        public string AlamatCabang { get; set; }
        public string no_trans { get; set; }
        public string no_po { get; set; }
        public DateTime tgl_trans { get; set; }
        public string doc_status { get; set; }
        public string sj_supplier { get; set; }
        public string NamaSupplier { get; set; }
        public string AlamatSupplier { get; set; }
      public  string AlamatPengiriman { get; set; }
        public string TelpSupplier { get; set; }

        public string keterangan { get; set; }
        public decimal qty_order { get; set; }
        public decimal qty_qc_pass { get; set; }
        public decimal qty_sisa { get; set; }
         
        public decimal SubTotal { get; set; } //SubTotal
        public decimal qty_qc { get; set; }
       
    }

}
