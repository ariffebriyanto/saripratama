using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using IFA.Domain.Models;
using IFA.Domain.Utils;
using ERP.Web.Utils;
using Microsoft.AspNetCore.Http;
using System.IO;
using ERP.Domain.Base;
using Newtonsoft.Json;

namespace ERP.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(FactoryClass factoryClass,
          IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
       // [Authorize(Roles = "Admin, User, UAT")]
        public IActionResult Index()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            // alternatively
            // claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;

            // get some claim by type
            Auth auth = new Auth();
            try
            {
                if(claimsIdentity.Claims.Count() > 0)
                {
                    var UserID = claimsIdentity.FindFirst("UserID").Value;
                    var RoleID = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
                    var RoleName = claimsIdentity.FindFirst("RoleName").Value;
                    var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
                    var NamaPegawai = claimsIdentity.FindFirst(ClaimTypes.Name).Value;
                    var BranchID = claimsIdentity.FindFirst("BranchID").Value;
                    var Branch = claimsIdentity.FindFirst("Branch").Value;
                    var Alamat = claimsIdentity.FindFirst("Alamat").Value;

                    auth.Alamat = Alamat;
                    auth.Branch = Branch;
                    auth.BranchID = BranchID;
                    auth.NamaPegawai = NamaPegawai;
                    auth.PegawaiID = PegawaiID;
                    auth.RoleName = RoleName;
                    auth.RoleID = RoleID;
                    auth.UserID = UserID;
                }
                else
                {
                    return RedirectToAction("Login", "Account");
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
            
         
            return View(auth);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetStatPO()
        {
            IEnumerable<DashboardVM> result = new List<DashboardVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Master/GetStatPO");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<DashboardVM>>(response.Message);

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
        public async Task<IActionResult> GetStatMTS()
        {
            IEnumerable<DashboardVM> result = new List<DashboardVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Master/GetStatMTS");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<DashboardVM>>(response.Message);

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
        public async Task<IActionResult> GetStatPIU()
        {
            IEnumerable<DashboardVM> result = new List<DashboardVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Master/GetStatPIU");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<DashboardVM>>(response.Message);

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
        public async Task<IActionResult> GetStatDUE()
        {
            IEnumerable<DashboardVM> result = new List<DashboardVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Master/GetStatDUE");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<DashboardVM>>(response.Message);

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
        public async Task<IActionResult> GetStatBooked()
        {
            IEnumerable<DashboardVM> result = new List<DashboardVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Master/GetStatBooked");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<DashboardVM>>(response.Message);

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


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
