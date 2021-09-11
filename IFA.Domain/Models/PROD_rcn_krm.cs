using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    
        public class PROD_rcn_krm
    {
        public string kd_cabang { get; set; }

        public string kd_departemen { get; set; }

        public DateTime tanggal { get; set; }

        public string no_trans { get; set; }

        public string tipe_trans { get; set; }

        public string kd_kendaraan { get; set; }

        public string kd_sopir { get; set; }

        public string kd_kenek { get; set; }

        public decimal? jml_kapasitas { get; set; }

        public string rec_stat { get; set; }

        public string keterangan { get; set; }

        public DateTime? last_create_date { get; set; }

        public string last_created_by { get; set; }

        public DateTime? last_update_date { get; set; }

        public string last_updated_by { get; set; }

        public string program_name { get; set; }

        public int? cetak_ke { get; set; }

        public string cetak_ulang { get; set; }

        public string kd_ekspedisi { get; set; }

        public List<PROD_rcn_krm_D> rcnkrmDetail { get; set; }
        public List<PRODV_MON_SO> rcnkrmDetailSO { get; set; }

        //monitoring


        public string jenis_sp { get; set; }
        public string atas_nama { get; set; }
        public string no_sp { get; set; }

        //monitring revisi

        public string Nama_Supir { get; set; }
        public string Nama_kenek { get; set; }


        public string tgl_transdesc { get; set; }







    }

    public class FilterProd_Krm_M
    {
        public string no_trans { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string no_sp { get; set; }
        public string kd_cust { get; set; }
    }




}
