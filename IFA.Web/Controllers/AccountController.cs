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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ERP.Domain.Objects;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace IFA.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(FactoryClass factoryClass,
         IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }

        
        public async Task<IActionResult> Login([FromServices] IConfiguration config)
        {
            //var nmApp = 
            var nameApp = Startup.namaApp;
            var cpy = Startup.namaCompany;
            ViewBag.namaApp = nameApp; 
            ViewBag.namaCompany = cpy;
            
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            Auth auth = new Auth();

            auth.UserID = userName;
            auth.Password = password;

            response = await client.CallAPIPost("Auth/GetAuthLogin", auth);
            auth = new Auth();
            if (response.Success)
            {
                auth = JsonConvert.DeserializeObject<Auth>(response.Message);
            }
            else
            {
                auth = null;
            }

            ClaimsIdentity identity = null;
            bool isAuthenticated = false;

            if (auth != null)
            {
                string akses_penjualan = "";
                if(auth.akses_penjualan != null)
                {
                    akses_penjualan = auth.akses_penjualan;
                }
                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, auth.NamaPegawai),
                    new Claim(ClaimTypes.Role, auth.RoleID),
                    new Claim("UserID", auth.UserID),
                    new Claim("RoleName", auth.RoleName),
                    new Claim("PegawaiID", auth.PegawaiID),
                    new Claim("BranchID", auth.BranchID),
                    new Claim("Branch", auth.Branch),
                    new Claim("Alamat", auth.Alamat),
                    new Claim("RoleID", auth.RoleID),
                    new Claim("Password", auth.Password),
                    new Claim("JenisUsaha", auth.JenisUsaha),
                    new Claim("Telp", auth.Telp),
                    new Claim("WA", auth.WA),
                    new Claim("akses_penjualan", auth.akses_penjualan)


                }, CookieAuthenticationDefaults.AuthenticationScheme);

                isAuthenticated = true;
            }

            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
            TempData["Message"] = "Authentication Fail";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAuthOTP(string password)
        {
            
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            Auth authOTP = new Auth();

            //auth.UserID = userName;
            authOTP.Password = password;

            response = await client.CallAPIPost("Auth/GetAuthOTP", authOTP);
            authOTP = new Auth();
            if (response.Success)
            {
                authOTP = JsonConvert.DeserializeObject<Auth>(response.Message);
            }
            else
            {
                authOTP = null;
            }

            
            return View();
        }
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult ChangeCabang()
        {
            return View();
        }
        public async Task<IActionResult> UpdatePasswordUser(MUSER data)
        {
            Response response = new Response();
            List<SALES_BOOKED> model = new List<SALES_BOOKED>();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var Password = claimsIdentity.FindFirst("Password").Value;

            try
            {
                if (Password != data.oldPassword)
                {
                    response.Success = false;
                    response.Result = "failed";
                    response.Message = "Password Anda Salah";
                    return Ok(response);
                }
                data.userid = UserID;
                response = await client.CallAPIPost("Helper/UpdatePasswordUser", data);
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

        public async Task<IActionResult> UpdateCabang(MUSER data)
        {
            Response response = new Response();
            List<SALES_BOOKED> model = new List<SALES_BOOKED>();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var Password = claimsIdentity.FindFirst("Password").Value;

            try
            {
                if (Password != data.oldPassword)
                {
                    response.Success = false;
                    response.Result = "failed";
                    response.Message = "Password Anda Salah";
                    return Ok(response);
                }
                data.userid = UserID;
                response = await client.CallAPIPost("Helper/UpdateCabang", data);
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

        public IActionResult UserGuide()
        {
            return View();
        }

        public ActionResult UserGuidePenjualan()
        {
            string fileName = "Penjualan88_" + Guid.NewGuid() + ".pdf";
            string filePath = @"C:\88\UserGuide\88_Penjualan.pdf";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/force-download", fileName);

        }

        public ActionResult UserGuideNonCash()
        {
            string fileName = "Penjualan88_" + Guid.NewGuid() + ".pdf";
            string filePath = @"C:\88\UserGuide\noncash.pdf";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/force-download", fileName);

        }

        public ActionResult UserGuidePrint()
        {
            string fileName = "Penjualan88_" + Guid.NewGuid() + ".pdf";
            string filePath = @"C:\88\UserGuide\print.pdf";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/force-download", fileName);

        }


        public ActionResult UserGuideRetur()
        {
            string fileName = "Penjualan88_" + Guid.NewGuid() + ".pdf";
            string filePath = @"C:\88\UserGuide\retur.pdf";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/force-download", fileName);

        }

        public ActionResult UserGuideOpname()
        {
            string fileName = "Penjualan88_" + Guid.NewGuid() + ".pdf";
            string filePath = @"C:\88\UserGuide\opname.pdf";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/force-download", fileName);

        }

        public ActionResult UserGuideMTS_out()
        {
            string fileName = "Penjualan88_" + Guid.NewGuid() + ".pdf";
            string filePath = @"C:\88\UserGuide\mutasi_out.pdf";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/force-download", fileName);

        }

        public ActionResult UserGuideMTS_in()
        {
            string fileName = "Penjualan88_" + Guid.NewGuid() + ".pdf";
            string filePath = @"C:\88\UserGuide\mutasi_in.pdf";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/force-download", fileName);

        }


        public ActionResult UserGuideCekStok()
        {
            string fileName = "Penjualan88_" + Guid.NewGuid() + ".pdf";
            string filePath = @"C:\88\UserGuide\CekStok.pdf";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/force-download", fileName);

        }

        public ActionResult UserGuideAllCabang()
        {
            string fileName = "Penjualan88_" + Guid.NewGuid() + ".pdf";
            string filePath = @"C:\88\UserGuide\all_cabang.pdf";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/force-download", fileName);

        }

        public ActionResult UserGuideLapSales()
        {
            string fileName = "Penjualan88_" + Guid.NewGuid() + ".pdf";
            string filePath = @"C:\88\UserGuide\lap_jual.pdf";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/force-download", fileName);

        }
    }
}