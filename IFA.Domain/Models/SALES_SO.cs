using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class SALES_SO
    {
        //[Required]
        public string Kd_Cabang { get; set; }
    
        public string No_sp { get; set; }
       
        public string Tipe_trans { get; set; }
        public DateTime Tgl_sp { get; set; }
        public string Kd_Customer { get; set; }
        public string Atas_Nama { get; set; }
        public string Nama_pnrm { get; set; }
        public string Almt_pnrm { get; set; }
        public DateTime? Tgl_Kirim { get; set; }
        public string Kd_sales { get; set; }
        public string Keterangan { get; set; }
        public string Flag_Ppn { get; set; }
        public decimal PPn { get; set; }
        public decimal? Total_qty { get; set; }
        public string Departement { get; set; }
        public string Status { get; set; }
        public DateTime Last_Create_Date { get; set; }
        public string Last_Created_By { get; set; }
        public DateTime? Last_Update_Date { get; set; }
        public string Last_Updated_By { get; set; }
        public string Program_Name { get; set; }
        public string Jenis_sp { get; set; }
        public decimal JML_RP_TRANS { get; set; }
        public string Valas { get; set; }
        public decimal? JML_VALAS_TRANS { get; set; }
        public decimal? Kurs { get; set; }
        public string Alasan { get; set; }
        public decimal? Biaya { get; set; }
        public DateTime Tgl_Kirim_Marketing { get; set; }
        public string Kode_Wilayah { get; set; }
        public string isClosed { get; set; }
        public string media { get; set; }

        public string isPrinted { get; set; }
        public int? CetakKe { get; set; }
        public string desc_promo { get; set; }
        public string No_Telp { get; set; }
        public TimeSpan? JamKirim { get; set; }
        public string sts_centi { get; set; }
        public DateTime? tgl_lahir_umum { get; set; }
        public string Confirmed { get; set; }
        public string old_num { get; set; }
        public string SP_REFF { get; set; }
        public DateTime? TGL_BARANG_MASUK { get; set; }
        public DateTime? TGL_SERAH_FORM { get; set; }
        public string KOTA { get; set; }
        public string Bonus { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Potongan { get; set; }
        public string Alamat_Tarik { get; set; }
        public string SP_REFF2 { get; set; }
        public string pending { get; set; }
        public string STATUS_DO { get; set; }
        public string FLAG { get; set; }

        public string Flag_Paket { get; set; }

        public int? Qty_Paket { get; set; }

        public string Kd_Paket { get; set; }

        public string Jatuh_Tempo { get; set; }

        public string Status_Simpan { get; set; }

        public string JnsSales { get; set; }
        public List<SALES_SO_D> details { get; set; }
        public string Tgl_spdesc { get; set; }
        public string Tgl_Kirim_Marketingdesc { get; set; }
        public List<SALES_SO_DVM> detailsvm { get; set; }
        public decimal? subtotal { get; set; }
        public string sales { get; set; }
        public List<SALES_BOOKED> booked { get; set; }
        public decimal dp { get; set; }
        public string branch { get; set; }
        public string tanggaldesc { get; set; }
        public string blthn { get; set; }
        public string alamat { get; set; }
        public string telp { get; set; }
        public string wa { get; set; }
        public string nama { get; set; }
        public string inc_ongkir { get; set; }
        public int stat_save { get; set; }
        public decimal dp_inden { get; set; }

        public string jenis_so { get; set; }
        public string no_paket { get; set; }
    }

    public class PiutangSOVM
    {
        public string no_inv { get; set; }
        public DateTime tgl_jatuh_tempo { get; set; }
        public int hari_jatuh_tempo { get; set; }
        public int parameter_date { get; set; }
        public decimal jml_akhir { get; set; }
        public decimal total { get; set; }
        public decimal saldo_limit { get; set; }

    }

}
