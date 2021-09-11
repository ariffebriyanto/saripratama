using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class SALES_RETUR_D
    {
    
        public string Kd_Cabang { get; set; }
       
        public string No_retur { get; set; }

        public string tipe_trans { get; set; }
     
        public decimal No_seq { get; set; }
      
        public string Kd_Stok { get; set; }

        public string Nama { get; set; }
  
        public decimal? Qty { get; set; }

        public string Kd_satuan { get; set; }

        public decimal qty_tarik { get; set; }

        public decimal? harga { get; set; }

        public string Jns_retur { get; set; }

        public string Jns_alasan { get; set; }

        public string Sts_ganti { get; set; }

        public string Keterangan { get; set; }

        public string Status { get; set; }

        public string departemen { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Programe_name { get; set; }

        public decimal? potongan { get; set; }

        public decimal? potongan_total { get; set; }

        public string nama_potongan { get; set; }

        public string lokasi { get; set; }

        public decimal? Total { get; set; }

        public decimal? persediaan { get; set; }

        public decimal? disc1 { get; set; }

        public decimal? disc2 { get; set; }

        public decimal? disc3 { get; set; }

        public decimal? disc4 { get; set; }

        public string Bonus { get; set; }

        public decimal? qty_nota { get; set; }
        public string nama_barang { get; set; }
       // public string kd_satuan { get; set; }
    }
}
