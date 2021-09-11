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
using IFA.Domain.Models;
using IFA.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IFA.Web.Controllers
{
    public class FinanceController : BaseController
    {
        public FinanceController(FactoryClass factoryClass,
         IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        #region "PostingJurnal"
        public IActionResult PostingJurnal()
        {
            return View();
        }

        public IActionResult GL()
        {
            return View();
        }

        public IActionResult Jurnal()
        {
            return View();
        }

        public async Task<IActionResult> GetJurnal()
        {
            IEnumerable<FIN_JURNAL> result = new List<FIN_JURNAL>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var RoleID = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            try
            {
                response = await client.CallAPIGet("FIN_JURNAL/GetJurnalPending");

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<FIN_JURNAL>>(response.Message);
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
       
        public async Task<string> GetMonTransaksiHarianPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime? DateFrom = null, DateTime? DateTo = null, string kd_buku_besar = null, string kd_valuta = null)
        {
            List<SaldoVM1> result = new List<SaldoVM1>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("FIN_JURNAL/GetMonTransaksiHarianPartial?skip=" + skip + "&take=" + take + "&pageSize=" + pageSize + "&page=" + page + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&sorting=" + sorting + "&filter=" + filter + "&kd_buku_besar=" + kd_buku_besar + "&kd_valuta=" + kd_valuta);
                //  response = await client.CallAPIPost("PURC_PO/GetPOPartial", filterPO);


                if (response.Success)
                {

                    result = JsonConvert.DeserializeObject<List<SaldoVM1>>(response.Message);
                    //if (result.Count() > 0)
                    //{
                    //    result.FirstOrDefault().tgl_podesc = result.FirstOrDefault().tgl_po.ToString("dd MMMM yyyy");
                    //    result.FirstOrDefault().tgl_kirimdesc = result.FirstOrDefault().tgl_kirim.ToString("dd MMMM yyyy");
                    //    result.FirstOrDefault().tgl_jth_tempodesc = result.FirstOrDefault().tgl_jth_tempo.ToString("dd MMMM yyyy");
                    //}

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

        public async Task<string> GetMonTransaksiJurnalPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime? DateFrom = null, DateTime? DateTo = null, string tipe_trans = null, string kd_valuta = null, string cek_post = null)
        {
            List<FIN_JURNAL> result = new List<FIN_JURNAL>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("FIN_JURNAL/GetMonTransaksiJurnalPartial?skip=" + skip + "&take=" + take + "&pageSize=" + pageSize + "&page=" + page + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&sorting=" + sorting + "&filter=" + filter + "&tipe_trans=" + tipe_trans + "&kd_valuta=" + kd_valuta + "&cek_post=" + cek_post);
                //  response = await client.CallAPIPost("PURC_PO/GetPOPartial", filterPO);


                if (response.Success)
                {

                    result = JsonConvert.DeserializeObject<List<FIN_JURNAL>>(response.Message);
                    //if (result.Count() > 0)
                    //{
                    //    result.FirstOrDefault().tgl_podesc = result.FirstOrDefault().tgl_po.ToString("dd MMMM yyyy");
                    //    result.FirstOrDefault().tgl_kirimdesc = result.FirstOrDefault().tgl_kirim.ToString("dd MMMM yyyy");
                    //    result.FirstOrDefault().tgl_jth_tempodesc = result.FirstOrDefault().tgl_jth_tempo.ToString("dd MMMM yyyy");
                    //}

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

        
        public async Task<IActionResult> GetMonTransaksiJurnalDetail(FIN_JURNAL filterPO)
        {
            IEnumerable<FIN_JURNAL_D> result = new List<FIN_JURNAL_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("FIN_JURNAL/GetMonTransaksiJurnalDetail?no_jur=" + filterPO.no_jur);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<FIN_JURNAL_D>>(response.Message);
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

        public async Task<IActionResult> GetSaldo(SaldoVM info)
        {
            IEnumerable<SaldoVM> result = new List<SaldoVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var RoleID = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            try
            {
                response = await client.CallAPIGet("FIN_JURNAL/GetSaldo?kd_rekening=" + info.kd_rekening + "&kd_valuta=" + info.kd_valuta + "&tahun=" + info.tahun + "&bulan=" + info.bulan);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SaldoVM>>(response.Message);
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

        public async Task<IActionResult> GetJurnalDetail(string nojur)
        {
            IEnumerable<FIN_JURNAL_D> result = new List<FIN_JURNAL_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var RoleID = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            try
            {
                response = await client.CallAPIGet("FIN_JURNAL/GetJurnalDetailPending?nojur=" + nojur);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<FIN_JURNAL_D>>(response.Message);
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

        public async Task<IActionResult> SavePostingJurnal(string nojur)
        {
            Response response = new Response();
            Response responseBooked = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
         
            try
            {
                response = await client.CallAPIGet("FIN_JURNAL/PostingJurnal?nojur=" + nojur);
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
        #endregion

        #region "Monitoring Piutang Usaha"
        public IActionResult MonPiutangUsaha()
        {
            return View();
        }

        public async Task<string> GetPiutangUsaha(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime tanggal, string kd_cust, string no_trans, string tipe)
        {
            List<FIN_PIUTANG_USAHA_Header> result = new List<FIN_PIUTANG_USAHA_Header>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("FIN_JURNAL/GetPiutangUsaha?skip=" + skip + "&take=" + take + "&pageSize=" + pageSize + "&page=" + page + "&tanggal=" + tanggal + "&kd_cust=" + kd_cust + "&sorting=" + sorting + "&filter=" + filter + "&no_trans=" + no_trans + "&tipe=" + tipe);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<FIN_PIUTANG_USAHA_Header>>(response.Message);
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

        public async Task<IActionResult> GetPiutangUsahaDetail(string kd_cust, DateTime tanggal, string no_trans)
        {
            IEnumerable<FIN_PIUTANG_USAHA_Detail> result = new List<FIN_PIUTANG_USAHA_Detail>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
         

            try
            {
                response = await client.CallAPIGet("FIN_JURNAL/GetPiutangUsahaDetail/?kd_cust=" + kd_cust + "&tanggal=" + tanggal + "&no_trans=" + no_trans);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<FIN_PIUTANG_USAHA_Detail>>(response.Message);
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

        public async Task<IActionResult> GetPenjualan(string no_inv)
        {
            IEnumerable<FIN_PIUTANG_USAHA_Penjualan> result = new List<FIN_PIUTANG_USAHA_Penjualan>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();


            try
            {
                response = await client.CallAPIGet("FIN_JURNAL/GetPenjualan/?no_inv=" + no_inv);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<FIN_PIUTANG_USAHA_Penjualan>>(response.Message);
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

        #endregion


        #region "Monitoring Hutang Usaha"
        public IActionResult MonHutangUsaha()
        {
            return View();
        }

        public async Task<string> GetHutangUsaha(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime tanggal, string kd_cust, string no_trans, string tipe)
        {
            List<FIN_HUTANG_USAHA_Header> result = new List<FIN_HUTANG_USAHA_Header>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("FIN_JURNAL/GetHutangUsaha?skip=" + skip + "&take=" + take + "&pageSize=" + pageSize + "&page=" + page + "&tanggal=" + tanggal + "&kd_cust=" + kd_cust + "&sorting=" + sorting + "&filter=" + filter + "&no_trans=" + no_trans + "&tipe=" + tipe);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<FIN_HUTANG_USAHA_Header>>(response.Message);
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

        public async Task<IActionResult> GetHutangUsahaDetail(string kd_cust, DateTime tanggal, string no_trans)
        {
            IEnumerable<FIN_HUTANG_USAHA_Detail> result = new List<FIN_HUTANG_USAHA_Detail>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();


            try
            {
                response = await client.CallAPIGet("FIN_JURNAL/GetHutangUsahaDetail/?kd_cust=" + kd_cust + "&tanggal=" + tanggal + "&no_trans=" + no_trans);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<FIN_HUTANG_USAHA_Detail>>(response.Message);
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

        public async Task<IActionResult> GetPembelian(string no_inv)
        {
            IEnumerable<FIN_HUTANG_USAHA_Penjualan> result = new List<FIN_HUTANG_USAHA_Penjualan>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();


            try
            {
                response = await client.CallAPIGet("FIN_JURNAL/GetPembelian/?no_inv=" + no_inv);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<FIN_HUTANG_USAHA_Penjualan>>(response.Message);
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

        #endregion
    }
}