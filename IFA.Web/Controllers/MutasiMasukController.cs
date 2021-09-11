using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ERP.Domain.Base;
using ERP.Web.Utils;
using IFA.Domain.Helpers;
using IFA.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ERP.Web.Controllers;
using System.Globalization;

using System.Diagnostics;
using ERP.Web;
using System.IO;
using IFA.Domain.Utils;


namespace IFA.Web.Controllers
{
    public class MutasiMasukController : BaseController
    {
        public MutasiMasukController(FactoryClass factoryClass,
         IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> Index(string id = "", string mode = "")
        {
            ApiClient client = factoryClass.APIClientAccess();

            Response response = new Response();


            response = await client.CallAPIGet("GUDANG_IN/GetListMtsOut");

            if (response.Success)
            {
                ViewBag.NoTrans = response.Message;

            }
            if (id == "")
            {
                ViewBag.Mode = "NEW";
            }
            else
            {
                ViewBag.Mode = mode;
                ViewBag.Id = id;
            }

            response = null;
            response = await client.CallAPIGet("Helper/GetGudang");
            if (response.Success)
            {
                ViewBag.GudangList = response.Message;

            }

            //response = null;
            //response = await client.CallAPIGet("SIF_Supplier/GetSIF_Supplier");

            //if (response.Success)
            //{
            //    ViewBag.Supplier = response.Message;

            //}
            //response = null;
            //response = await client.CallAPIGet("SIF_Satuan/GetSIFSatuanCbo");
            //if (response.Success)
            //{
            //    ViewBag.Satuan = response.Message;

            //}
            //response = null;
            //response = await client.CallAPIGet("SIF_Barang/GetSIFBarangCbo");
            //if (response.Success)
            //{
            //    ViewBag.Barang = response.Message;

            //}
            return View();
        }

        public IActionResult MonMtsMasuk()
        {
            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetMTSDetail(FilterGudangIn filterIn)
        {
            IEnumerable<INV_GUDANG_IN_D> result = new List<INV_GUDANG_IN_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("GUDANG_IN/GetDetailTerima?&DateFrom=" + filterIn.DateFrom + "&DateTo=" + filterIn.DateTo );

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_GUDANG_IN_D>>(response.Message);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetPO(FilterPURC_PO filterPO)
        {
            IEnumerable<PURC_PO> result = new List<PURC_PO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("PURC_PO/GetPO?no_po=" + filterPO.no_po + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo + "&status_po=" + filterPO.status_po);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<PURC_PO>>(response.Message);
                result.FirstOrDefault().tgl_podesc = result.FirstOrDefault().tgl_po.ToString("dd MMMM yyyy");
                result.FirstOrDefault().tgl_kirimdesc = result.FirstOrDefault().tgl_kirim.ToString("dd MMMM yyyy");
                result.FirstOrDefault().tgl_jth_tempodesc = result.FirstOrDefault().tgl_jth_tempo.ToString("dd MMMM yyyy");

            }


            //ViewBag.Mode = "EDIT";
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")] //[Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetMts(string id, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            IEnumerable<INV_GUDANG_OUT_D> result = new List<INV_GUDANG_OUT_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var kdcabang = claimsIdentity.FindFirst("BranchID").Value;
            response = await client.CallAPIGet("GUDANG_IN/GetMts?no_trans=" + id + "&kdcabang=" + kdcabang);
            //response = await client.CallAPIGet("GUDANG_IN/GetMTS_In?kdcb=" + kdcabang + "&no_trans =" + id + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo);


            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_GUDANG_OUT_D>>(response.Message);


            }
            //ViewBag.Mode = "EDIT";
            return Ok(result);
        }


        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetMts_IN(string id, DateTime? DateFrom = null, DateTime? DateTo = null,string barang = null)
        {
            IEnumerable<INV_GUDANG_IN> result = new List<INV_GUDANG_IN>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var kdcabang = claimsIdentity.FindFirst("BranchID").Value;

            response = await client.CallAPIGet("GUDANG_IN/GetMTS_In?kdcb=" + kdcabang + "&no_trans =" + id + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo+ "&kd_stok=" + barang);


            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_GUDANG_IN>>(response.Message);


            }
            //ViewBag.Mode = "EDIT";
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetdataMts(string id)
        {
            

            IEnumerable<INV_GUDANG_IN_D> result = new List<INV_GUDANG_IN_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var kdcabang = claimsIdentity.FindFirst("BranchID").Value;

            response = await client.CallAPIGet("GUDANG_IN/GetdataMts?no_trans=" + id);

            if (response.Success)
            {

                result = JsonConvert.DeserializeObject<List<INV_GUDANG_IN_D>>(response.Message);


            }
            //ViewBag.Mode = "EDIT";
            return Ok(result);

        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveMTS(INV_GUDANG_IN data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var mode = "";
            string no_trans = "";
            string blthn = "";
            string kd_gudang = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            if (data.no_trans == null)
            {
                mode = "NEW";
                //response = await client.CallAPIGet("Helper/GenNoTrans?prefix=QC&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                //response = await client.CallAPIGet("Helper/GetNoTrans?prefix=BMI&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                response = await client.CallAPIGet("Helper/GetNoTransx?prefix=BMI&kdcabang=" + BranchID);
                if (response.Success)
                {
                    no_trans = JsonConvert.DeserializeObject<string>(response.Message);
                }
                data.no_trans = no_trans;
            }
            else
            {
                no_trans = data.no_trans;
            }

            response = await client.CallAPIGet("Helper/GetGudangFromCabang?kd_cabang=" + BranchID);
            if (response.Success)
            {
                kd_gudang = JsonConvert.DeserializeObject<string>(response.Message);
            }

            if (data.blthn == null)
            {
                mode = "NEW";
                response = await client.CallAPIGet("Helper/GetBlthn");
                if (response.Success)
                {
                    blthn = JsonConvert.DeserializeObject<string>(response.Message);
                }
                data.blthn = blthn;
            }
            else
            {
                blthn = data.blthn;
            }


            //var claimsIdentity = User.Identity as ClaimsIdentity;
            //var UserID = claimsIdentity.FindFirst("UserID").Value;
            //var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            data.Kd_Cabang = BranchID;
            data.kode_gudang = data.gddetail.FirstOrDefault().gudang_tujuan;
            data.tipe_trans = "JPB-KUT-02";
            data.jml_rp_trans = data.jml_rp_trans;
            //data.jml_rp_trans = data.jml_rp_trans - data.jml_ppn - data.ongkir;
            //asal
            if (data.keterangan == null)
            {
                data.keterangan = "Penerimaan Mutasi ";
            }
            data.gddetail.RemoveAll(x => x.qty_in == 0);
            for (int i = 0; i <= data.gddetail.Count() - 1; i++)
            {
               
                data.gddetail[i].no_trans = data.no_trans;
                data.gddetail[i].no_seq = i + 1;
                data.gddetail[i].Kd_Cabang = data.Kd_Cabang;
                data.gddetail[i].blthn = data.blthn;
                //data.gddetail[i].gudang_asal = "EXP01";
                //data.gddetail[i].gudang_tujuan = kd_gudang;

                data.gddetail[i].tipe_trans = data.tipe_trans;
                data.gddetail[i].Last_Created_By = UserID;
                data.gddetail[i].Last_Create_Date = DateTime.Now;
            }
            if (mode == "NEW")
            {
                data.Last_Created_By = UserID;
                data.Last_Create_Date = DateTime.Now;
                response = await client.CallAPIPost("GUDANG_IN/SaveMTS", data);
            }
            else
            {
                data.Last_Updated_By = UserID;
                data.Last_Update_Date = DateTime.Now;
                response = await client.CallAPIPost("GUDANG_IN/UpdateMTS", data);

            }
            if (response.Success)
            {
                response.Result = no_trans;
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public IActionResult LaporanKartuStok()
        {
            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetBarang()
        {
            IEnumerable<SIF_BarangCbo> result = new List<SIF_BarangCbo>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("SIF_Barang/GetSIFBarangCbo");
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_BarangCbo>>(response.Message);

            }
            return Ok(result);
        }

        public async Task<IActionResult> Pembatalan(string id)
        {
            IEnumerable<INV_GUDANG_IN> data = new List<INV_GUDANG_IN>();
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            INV_GUDANG_IN gd_out = new INV_GUDANG_IN();
            //IEnumerable<INV_GUDANG_OUT> gd = new List<INV_GUDANG_OUT>();
            List<INV_GUDANG_IN_D> detail = new List<INV_GUDANG_IN_D>();

            var cb = BranchID;
            var by = UserID;


            try
            {
                response = await client.CallAPIGet("GUDANG_IN/MtsIn??kdcb=" + BranchID + "&no_trans=" + id);

                if (response.Success)
                {
                    gd_out = JsonConvert.DeserializeObject<List<INV_GUDANG_IN>>(response.Message).FirstOrDefault();
                    gd_out.Last_Updated_By = UserID;
                    //gd_out.detail.
                    //sj. = "BATAL";
                    //sj.Program_name = "Pembatalan SJK";
                    //response = await client.CallAPIGet("SALES_SJ/GetDtlSJ_del?no_sj=" + id);
                    detail = gd_out.gddetail.ToList();

                    if (response.Success)
                    {
                        //detil = JsonConvert.DeserializeObject<List<INV_GUDANG_OUT_D>>(response.Message);
                        if (detail != null && detail.Count() > 0)
                        {
                            //data.FirstOrDefault().tgl_transdesc = data.FirstOrDefault().tgl_trans.ToString("dd MMMM yyyy");

                            // detail = gd_out.detail.FirstOrDefault();
                            //foreach (var item in detail)
                            //{
                            //    gd_out.detail.Add(item);
                            //}
                            response = await client.CallAPIPost("INV_GUDANG_IN/Pembatalan", gd_out);
                            // response = await client.CallAPIPost("SALES_SJ/PembatalanSJK", sj);
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "Data Tidak Ditemukan";
                            response.Result = "failed";
                            return Ok(response);
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Data Tidak Ditemukan";
                        response.Result = "failed";
                        return Ok(response);
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Data Tidak Ditemukan";
                    response.Result = "failed";
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (factoryClass.config.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }
            }


            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult GetTahun()
        {
            List<Shared> result = new List<Shared>();
            ApiClient client = factoryClass.APIClientAccess();
            for (int i = 5; i > 0; i--)
            {
                Shared tahun = new Shared();
                tahun.key = DateTime.Now.AddYears(-i).Year.ToString();
                tahun.value = DateTime.Now.AddYears(-i).Year.ToString();
                result.Add(tahun);
            }
            for (int i = 0; i < 6; i++)
            {
                Shared tahun = new Shared();
                tahun.key = DateTime.Now.AddYears(i).Year.ToString();
                tahun.value = DateTime.Now.AddYears(i).Year.ToString();
                result.Add(tahun);
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public IActionResult GetBulan()
        {
            List<Shared> result = new List<Shared>();
            ApiClient client = factoryClass.APIClientAccess();
            string[] names = DateTimeFormatInfo.CurrentInfo.MonthNames;
            for (int i = 0; i < names.Length - 1; i++)
            {
                Shared bulan = new Shared();
                if (i + 1 <= 9)
                {
                    bulan.key = "0" + (i + 1).ToString();
                }
                else
                {
                    bulan.key = (i + 1).ToString();
                }
                bulan.value = names[i];
                result.Add(bulan);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetDataStokHeader(string kd_stok, string bulan, string tahun)
        {
            IEnumerable<V_StokGudang> result = new List<V_StokGudang>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("INV_Q/GetKartuStok?kd_stok=" + kd_stok + "&bulan=" + bulan + "&tahun=" + tahun);
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<V_StokGudang>>(response.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetPrintKartuStok(string kd_stok, string bulan, string tahun)
        {
            List<V_StokGudang> result = new List<V_StokGudang>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            string sHTML = "";

            decimal sumQtyIn = 0;
            decimal sumQtyOut = 0;
            decimal sumQtySisa = 0;

            response = await client.CallAPIGet("INV_Q/GetKartuStok?kd_stok=" + kd_stok + "&bulan=" + bulan + "&tahun=" + tahun);
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<V_StokGudang>>(response.Message);
                //profile
                sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}th{border: 1px solid #dddddd; text-align: center; padding: 8px;}.borderGrid{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}}</style></head><body><table style='margin-bottom: 20px;'><tr style='border-bottom: black solid 5px;'><td style='width: 40%;border: 0px solid #dddddd;' ><h2>" + result.FirstOrDefault().profile.nama + "</h2><p>" + result.FirstOrDefault().profile.alamat + "</p><p>telp: " + result.FirstOrDefault().profile.telp1 + "</p><p>fax: " + result.FirstOrDefault().profile.fax1 + "</p><p>" + result.FirstOrDefault().profile.kota + " - " + result.FirstOrDefault().profile.propinsi + "</p></td></tr></table><table style='margin-bottom: 20px;'><tr><td style='text-align:right;'>Periode:" + result.FirstOrDefault().bultah + "<td></tr><tr><td style='text-align:center;font-size:20px;font-weight:bold;'>KARTU STOK<td></tr></table>";

                //header
                sHTML += "<table style='margin-bottom: 20px;'><tr><td style='width: 100px;font-size: 14px;'>Nama Barang:<td><td style='font-size: 14px;font-weight: bold !important;'>" + result.FirstOrDefault().Nama_Barang + "<td></tr><tr><td style='font-size: 14px;'>Kode Barang:<td><td style='font-size: 14px;font-weight: bold !important;'>" + result.FirstOrDefault().Kode_Barang + "<td><td style='font-size: 14px;text-align: right;'>Saldo Awal:<td><td style='font-size: 14px;font-weight: bold !important;text-align: right;width: 60px;'>" + result.FirstOrDefault().awal_qty_onstok + "<td></tr></table>";

                //header table
                sHTML += "<table> <tr class='headerTable'> <th>Tanggal</th><th>No Bukti</th> <th>Nama Toko/Gudang</th> <th>Keterangan</th><th>Qty Masuk</th> <th>Qty Keluar</th><th>Qty Sisa</th> </tr>";

                //detail
                foreach (vy_saldocard detail in result.FirstOrDefault().ListSaldo)
                {
                    sHTML += " <tr> <td class='borderGrid' style='min-width:100px'>" + detail.Tanggal.ToString("dd MMMM yyyy") + "</td><td class='borderGrid'>" + detail.no_trans + "</td><td class='borderGrid'>" + detail.Atas_Nama + "</td><td class='borderGrid'>" + detail.keterangan + "</td><td class='borderGrid' style='text-align: right;'>" + detail.qty_in + "</td><td class='borderGrid' style='text-align: right;'>" + detail.qty_out + "</td><td class='borderGrid' style='text-align: right;'>" + detail.qty_sisa + "</td></tr>";
                    sumQtyIn += detail.qty_in;
                    sumQtyOut += detail.qty_out;
                }
                sumQtySisa = (sumQtyIn + result.FirstOrDefault().awal_qty_onstok) - sumQtyOut;
                //summary
                sHTML += " <tr> <td colspan='4' class='borderGrid' style='text-align: right;font-weight: 700;'>TOTAL</td><td class='borderGrid' style='text-align: right;font-weight: 700;'>" + sumQtyIn + "</td><td class='borderGrid' style='text-align: right;font-weight: 700;'>" + sumQtyOut + "</td><td class='borderGrid' style='text-align: right;font-weight: 700;'>" + sumQtySisa + "</td></tr> ";

                //end table
                sHTML += " </table> ";

                //end
                sHTML += " </body></html> ";
            }
            return Ok(sHTML);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetDetailMts(FilterGudangIn filterMn)
        {
            IEnumerable<INV_GUDANG_IN_D> result = new List<INV_GUDANG_IN_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            //DateTime dt1 = DateTime.ParseExact(filterMn.DateFrom.ToString(), "dd/MM/yyyy HH:mm tt", CultureInfo.InvariantCulture);
            //DateTime dt2 = DateTime.ParseExact(filterMn.DateTo.ToString(), "dd/MM/yyyy HH:mm tt", CultureInfo.InvariantCulture);
            DateTime dt1 = filterMn.DateFrom.Value;
            DateTime dt2 = filterMn.DateTo.Value;
            string strdate1 = dt1.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            string strdate2 = dt2.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //date1.par
            try
            {
                response = await client.CallAPIGet("GUDANG_IN/GetMonitoringMts?kdcb=" + BranchID + "&no_trans=" + filterMn.id + "&kd_stok=" + filterMn.barang + "&DateFrom=" + filterMn.DateFrom + "&DateTo=" + filterMn.DateTo);
                //response = await client.CallAPIPost("GUDANG_IN/GetMonitoringMts",filterMn);
               // response = await client.CallAPIPost("INV_GUDANG_IN/Pembatalan", gd_out);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<INV_GUDANG_IN_D>>(response.Message);
                }
            }
            catch (Exception e)
            {
                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (factoryClass.config.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }
            }


            //if (response.Success)
            //{
            //    result = JsonConvert.DeserializeObject<List<INV_QC>>(response.Message);
            //}
            return Ok(result);
        }

        public async Task<string> GetMts_INPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string barang)
        {
            IEnumerable<INV_GUDANG_IN> result = new List<INV_GUDANG_IN>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var kdcabang = claimsIdentity.FindFirst("BranchID").Value;

            response = await client.CallAPIGet("GUDANG_IN/GetMts_INPartial?kdcb=" + kdcabang + "&skip=" + skip + "&take=" + take + "&pageSize=" + pageSize + "&page=" + page + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&sorting=" + sorting + "&filter=" + filter + "&barang=" + barang);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_GUDANG_IN>>(response.Message);


            }
            return JsonConvert.SerializeObject(new { total = response.Result, data = result });
        }
    }
}