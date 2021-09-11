using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class PURC_PO
    {
       
        public string Kd_Cabang { get; set; }

        public string tipe_trans { get; set; }
      
        public string no_po { get; set; }
        //false,"Tanggal PO harus diisi","", DataType.Date
        [Required(AllowEmptyStrings =false,ErrorMessage ="Tanggal PO Harus Diisi")]
        public DateTime tgl_po { get; set; }

        public string no_pr { get; set; }

        public string no_trans { get; set; } // nambah buat linked ke qc, yayak
        public string sj_supplier { get; set; }
        public string blthn { get; set; }
        public DateTime tgl_trans { get; set; }

        public string no_ref { get; set; }

        public string no_qc { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Supplier Harus Dipilih")]
        public string kd_supplier { get; set; }

        public string kd_valuta { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Kurs Harus Diisi")]
        public decimal kurs_valuta { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Tanggal Kirim Harus Diisi")]
        public DateTime tgl_kirim { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tanggal Bayar Harus Diisi")]
        public DateTime tgl_jth_tempo { get; set; }

        public decimal? qty_total { get; set; }
        public decimal? qty_order { get; set; }

        public decimal jml_val_trans { get; set; }

        public decimal? jml_rp_trans { get; set; }

        public string flag_diskon { get; set; }

        public string flag_ppn { get; set; }

        public decimal jml_ppn { get; set; }

        public decimal? prosen_diskon { get; set; }

        public decimal? jml_diskon { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Alamat Kirim Harus Diisi")]
        [DataType(DataType.MultilineText)]
        public string keterangan { get; set; }

        public string rec_stat { get; set; }

        public int lama_bayar { get; set; }

        public DateTime? tgl_bayar { get; set; }

        public string term_bayar { get; set; }

        public DateTime? tgl_approve { get; set; }

        public string user_approve { get; set; }

        public string ket_approve { get; set; }

        public DateTime? tgl_batal { get; set; }

        public string ket_batal { get; set; }

        public DateTime? tgl_cetak { get; set; }

        public decimal? jml_cetak { get; set; }

        public string sts_ctk_ulang { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public string status_po { get; set; }

        public string flag_tagih { get; set; }

        public decimal? total { get; set; }

        public decimal ongkir { get; set; }

        public string isClosed { get; set; }

        public string Nama_Supplier { get; set; }
        public List<PURC_PO_D> podetail { get; set; }
        public List<PURC_DPM_D> listdpm { get; set; }

        public string tgl_podesc { get; set; }
        public string tgl_kirimdesc { get; set; }
        public string tgl_jth_tempodesc { get; set; }
        public DateTime Tgl_Diperlukan { get; set; }
        public string atas_nama { get; set; }
        public string kop_surat { get; set; }
        public string alamat_custom { get; set; }
        public string param_po { get; set; }
        public string no_jur { get; set; }
        public string stuffbarang { get; set; }
        public string lok_simpan { get; set; }
        public string p_np { get; set; }
        public string kode_gudang { get; set; }
        public string penyerah { get; set; }

        public List<INV_GUDANG_IN_D> gudangin { get; set; }
        public List<PURC_PO_D> detailPO { get; set; }
    }
    public class FilterPURC_PO
    {
        public string no_po { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string status_po { get; set; }
        public string barang { get; set; }
        public string kd_stok { get; set; }
        public string kd_cust { get; set; }

    }

    public class filterPOPartial
    {
       
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public int pageSize { get; set; }
        public int page { get; set; }

    }

    public class PrintPO
    {
        public string Cabang { get; set; }
        public string Telp1 { get; set; }
        public string Kota { get; set; }
        public string AlamatCabang { get; set; }
        public string PUNumber { get; set; }
        public string TanggalPO { get; set; }
        public string StatusPO { get; set; }
        public string AlamatPengiriman { get; set; }
        public string TanggalJatuhTempo { get; set; }
        public string TanggalKirim { get; set; }
        public string NamaSupplier { get; set; }
        public string AlamatSupplier { get; set; }
        public string TelpSupplier { get; set; }
        public string RequestBy { get; set; }
        public string ApprovedBy { get; set; }
        public string Notes { get; set; }
        public decimal PPN { get; set; }
        public decimal Ongkir { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public string atas_nama { get; set; }
        public string kop_surat { get; set; }
        public string alamat_custom { get; set; }
    }

    public class POMASTERApprovalVM
    {
        public string ID { get; set; }
        public string StatusDesc { get; set; }
        public string UserID { get; set; }
    }
    public class SOAmountVM
    {
        public string cabang { get; set; }
        public decimal amount { get; set; }
    }
}
