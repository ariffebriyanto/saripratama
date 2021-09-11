using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    
        public class INV_GUDANG_IN
        {  
            [Required]
            public string Kd_Cabang { get; set; }
           [Required]
            public string no_trans { get; set; }

            public string tipe_trans { get; set; }

            public DateTime tgl_trans { get; set; }

            public string no_qc { get; set; }

            public string no_ref { get; set; }

            public string penyerah { get; set; }

            public string keterangan { get; set; }

            public string status { get; set; }

            public decimal jml_rp_trans { get; set; }

            public decimal? jml_ppn { get; set; }

            public string blthn { get; set; }

            public DateTime? tgl_posting { get; set; }

            public string no_posting { get; set; }

            public string no_jurnal { get; set; }

            public DateTime? Last_Create_Date { get; set; }

            public string Last_Created_By { get; set; }

            public DateTime? Last_Update_Date { get; set; }

            public string Last_Updated_By { get; set; }

            public string Program_Name { get; set; }

            public string kode_gudang { get; set; }

            public string sj_supplier { get; set; }

            public int? cetak_ke { get; set; }

            public string cetak_ulang { get; set; }

            public string kd_customer { get; set; }

            public List<INV_GUDANG_IN_D> gddetail { get; set; }

            public string p_np { get; set; }

            public string Nama_Supplier { get; set; }

            public string tgl_transdesc { get; set; }

            public string Nama_Gudang { get; set; }

        public decimal jml_qtyin { get; set; }
        public decimal jml_qtypo { get; set; }
        public string gudang_asal { get; set; }
        public string gudang_tujuan { get; set; }
        public string alamat { get; set; }
        public string telp { get; set; }
        public string wa { get; set; }


    }

    public class FilterGudangIn
    {
        public string barang { get; set; }
        public string id { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string kdcb { get; set; }
       

    }
    public class PrintTerima
    {
        public string AlamatCabang { get; set; }
        public string no_trans { get; set; }
        public string Cabang { get; set; }
        public string Telp1 { get; set; }
        public string Kota { get; set; }
        public string PONumber { get; set; }
        public string Tanggaltrans { get; set; }
        public string Nama_Supplier { get; set; }
        public string AlamatSupplier { get; set; }
        public string Keterangan { get; set; }
        public string atas_nama { get; set; }
        public string POKeterangan { get; set; }
        public string No_Telepon1 { get; set; }

    }

    public class StokAllGudang
    {
        public string bultah { get; set; }
        public string kd_stok { get; set; }
        public string Nama_Barang { get; set; }
        public string kd_satuan { get; set; }
        public string Bangil { get; set; }
        public string Kombes_Sidoarjo { get; set; }
        public string Lamongan { get; set; }
        public string Lingkar_timur { get; set; }
        public string Kairos { get; set; }
       
    }




}
