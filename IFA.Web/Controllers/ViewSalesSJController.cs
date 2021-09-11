using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ERP.Domain.Base;
using ERP.Web;
using ERP.Web.Controllers;
using ERP.Web.Utils;
using IFA.Domain.Helpers;
using IFA.Domain.Models;
using IFA.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using System.Globalization;

namespace IFA.Web.Controllers
{
    public class ViewSalesSJController : BaseController
    {
        public ViewSalesSJController(FactoryClass factoryClass,
         IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult Index()
        {
           
            return View();
        }


        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult CetakSJ()
        {

            return View();
        }
        //public IActionResult editSJ()
        //{

        //    return View();
        //}
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult ReportSJK()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var JenisUsaha = claimsIdentity.FindFirst("JenisUsaha").Value;

            //var UserID = claimsIdentity.FindFirst("UserID").Value;
            //var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            //var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            //var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            
            //var Telp = claimsIdentity.FindFirst("Telp").Value;
            //var WA = claimsIdentity.FindFirst("WA").Value;
            //var akses_penjualan = claimsIdentity.FindFirst("akses_penjualan").Value;

            ViewBag.BranchID = JenisUsaha;
            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult MonSJ()
        {

            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDPB()
        {
            IEnumerable<SALES_SJ> result = new List<SALES_SJ>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
           

            try
            {

                response = await client.CallAPIGet("SALES_SJ/GetListDPB");

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_SJ>>(response.Message);
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


            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveSJ([FromBody] List<SALES_SJ> data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            try
            {
                for (int i = 0; i <= data.Count() - 1; i++)
                {
                    
                    data[i].Kd_cabang = BranchID;
                    data[i].Last_created_by = UserID;
                   
                }
                response = await client.CallAPIPost("SALES_SJ/SaveSJ", data);
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
                sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}th{border: 1px solid #dddddd; text-align: center; padding: 8px;}.borderGrid{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}}</style></head><body><table style='margin-bottom: 20px;'><tr style='border-bottom: black solid 5px;'><td style='width: 40%;border: 0px solid #dddddd;' ><h2>" + result.FirstOrDefault().profile.nama + "</h2><p>" + result.FirstOrDefault().profile.alamat + "</p><p>telp: " + result.FirstOrDefault().profile.telp1 + "</p><p>fax: " + result.FirstOrDefault().profile.fax1 + "</p><p>" + result.FirstOrDefault().profile.kota + " - " + result.FirstOrDefault().profile.propinsi + "</p></td></tr></table><table style='margin-bottom: 20px;'><tr><td style='text-align:right;'>Periode:" + result.FirstOrDefault().bultah + "</td></tr><tr><td style='text-align:center;font-size:20px;font-weight:bold;'>KARTU STOK</td></tr></table>";

                //header
                sHTML += "<table style='margin-bottom: 20px;'><tr><td style='width: 100px;font-size: 14px;'>Nama Barang:</td><td style='font-size: 14px;font-weight: bold !important;'>" + result.FirstOrDefault().Nama_Barang + "</td></tr><tr><td style='font-size: 14px;'>Kode Barang:</td><td style='font-size: 14px;font-weight: bold !important;'>" + result.FirstOrDefault().Kode_Barang + "</td><td style='font-size: 14px;text-align: right;'>Saldo Awal:</td><td style='font-size: 14px;font-weight: bold !important;text-align: right;width: 60px;'>" + result.FirstOrDefault().awal_qty_onstok + "</td></tr></table>";

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
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult LaporanKartuStokGudang()
        {
            return View();
        }

        //public async Task<IActionResult> GetSJ(string id, DateTime? DateFrom = null, DateTime? DateTo = null, string program_name = null)
        //{
        //    IEnumerable<SALES_SJ_D> result = new List<SALES_SJ_D>();
        //    ApiClient client = factoryClass.APIClientAccess();
        //    Response response = new Response();
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var UserID = claimsIdentity.FindFirst("UserID").Value;
        //    var BranchID = claimsIdentity.FindFirst("BranchID").Value;

        //    response = await client.CallAPIGet("GUDANG_IN/GetSJ");

        //    if (response.Success)
        //    {
        //        result = JsonConvert.DeserializeObject<List<SALES_SJ_D>>(response.Message);


        //    }
        //    ViewBag.Mode = "EDIT";
        //    return Ok(result);
        //}
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetSJ(string id, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            IEnumerable<SALES_SJ> result = new List<SALES_SJ>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            response = await client.CallAPIGet("SALES_SJ/GetMonSJ?no_sj=" + id + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&cb=" + BranchID );

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SALES_SJ>>(response.Message);
                if (result.Count() > 0)
                {
                    result.FirstOrDefault().tgl_transdesc = result.FirstOrDefault().TglSJ.ToString("dd MMMM yyyy");
                }
            }
            return Ok(result);
        }
        public async Task<string> GetSJPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string barang)
        {
            List<SALES_SJ> result = new List<SALES_SJ>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;

            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            try
            {
                response = await client.CallAPIGet("SALES_SJ/GetSJPartial?skip=" + skip + "&take=" + take + "&pageSize=" + pageSize + "&page=" + page + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&sorting=" + sorting + "&filter=" + filter + "&kdcb=" + BranchID);



                if (response.Success)
                {

                    result = JsonConvert.DeserializeObject<List<SALES_SJ>>(response.Message);


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

            ViewBag.Mode = "EDIT";

            return JsonConvert.SerializeObject(new { total = response.Result, data = result });
            //  return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetAll_SJ(string id, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            IEnumerable<SALES_SJ> result = new List<SALES_SJ>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            response = await client.CallAPIGet("SALES_SJ/GetAll_SJ?no_sj=" + id + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&cb=" + BranchID);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SALES_SJ>>(response.Message);
                if (result.Count() > 0)
                {
                    result.FirstOrDefault().tgl_transdesc = result.FirstOrDefault().TglSJ.ToString("dd MMMM yyyy");
                }
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDetailSJ(string id, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            IEnumerable<SALES_SJ_D> result = new List<SALES_SJ_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            response = await client.CallAPIGet("SALES_SJ/GetDtlSJ?no_sj=" + id + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&cb=" + BranchID);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SALES_SJ_D>>(response.Message);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetSJK(string id, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            IEnumerable<SALES_SJ> result = new List<SALES_SJ>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            response = await client.CallAPIGet("SALES_SJ/GetMonSJK?no_sj=" + id + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&cb=" + BranchID);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SALES_SJ>>(response.Message);
                if (result.Count() > 0)
                {
                    result.FirstOrDefault().tgl_transdesc = result.FirstOrDefault().TglSJ.ToString("dd MMMM yyyy");
                }
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetAll_SJdtl(string id, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            IEnumerable<SALES_SJ_D> result = new List<SALES_SJ_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            response = await client.CallAPIGet("SALES_SJ/GetAll_SJdtl?no_sj=" + id + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&cb=" + BranchID);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SALES_SJ_D>>(response.Message);
                
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDetailSJK(string id, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            IEnumerable<SALES_SJ_D> result = new List<SALES_SJ_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            response = await client.CallAPIGet("SALES_SJ/GetDtlSJK?no_sj=" + id + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&cb=" + BranchID);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SALES_SJ_D>>(response.Message);
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Pembatalan(string id)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            SALES_SJ sj = new SALES_SJ();
            List<SALES_SJ_D> detail = new List<SALES_SJ_D>();

            var cb = BranchID;
            var by = UserID;


            try
            {
                var ret = await client.CallAPIGet("SALES_SJ/GetMonSJ_del?no_sj=" + id + "&jns_sj=SJ"); 
                if (ret.Success)
                {
                    sj = JsonConvert.DeserializeObject<List<SALES_SJ>>(ret.Message).FirstOrDefault();
                    sj.status_sj = "BATAL";
                    sj.Program_name = "Pembatalan SJ";
                    response = await client.CallAPIGet("SALES_SJ/GetDtlSJ_del?no_sj=" + id + "&jns_sj=SJ"); 

                    if (response.Success)
                    {
                        detail = JsonConvert.DeserializeObject<List<SALES_SJ_D>>(response.Message);
                        if (detail != null && detail.Count() > 0)
                        {
                            sj.sjdetail = new List<SALES_SJ_D>();
                            foreach (var item in detail)
                            {
                                sj.sjdetail.Add(item);
                            }
                            response = await client.CallAPIPost("SALES_SJ/PembatalanSJK", sj);
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
                    ret.Success = false;
                    ret.Message = "Data Tidak Ditemukan";
                    ret.Result = "failed";
                    return Ok(ret);
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
    }
}