using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class PURC_DPM_D
    {
        public string Kd_Cabang { get; set; }

        public string tipe_trans { get; set; }

        public string no_po { get; set; }

        public string No_DPM { get; set; }

        public int No_Seq { get; set; }

        public string Kd_Stok { get; set; }

        public string Satuan { get; set; }

        public string spek_brg { get; set; }

        public decimal Qty { get; set; }

        public decimal? Qty_PR { get; set; }

        public decimal? Qty_received { get; set; }

        public decimal? Qty_sisa { get; set; }

        public string Keterangan { get; set; }

        public string rec_stat { get; set; }

        public string no_csp { get; set; }

        public DateTime Tgl_Diperlukan { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }
        public List<SIF_Satuan> ListSatuan { get; set; }
        public List<SIF_BarangCbo> ListBarang { get; set; }
        public string Nama_Barang { get; set; }

        public decimal? qty_approve { get; set; }
        public string status_approve { get; set; }
        public decimal last_price { get; set; }
        public string cabang { get; set; }
        public List<dpmDetail> dpmdetail { get; set; }


    }

    public class dpmDetail
    {
        public string kd_Stok { get; set; }
        public string kd_satuan { get; set; }
        public string Satuan { get; set; }
        public string nama_barang { get; set; }
        public decimal Qty { get; set; }
        public string Tgl_Diperlukan { get; set; }
        public string no_dpm { get; set; }


    }

}
