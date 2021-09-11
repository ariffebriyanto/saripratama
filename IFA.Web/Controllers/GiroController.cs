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
    public class GiroController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public GiroController(FactoryClass factoryClass,
        IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }

        #region "Penerimaan Giro"
        public async Task<IActionResult> GetGiro(string kdcb, string kdcust, string nomor)
        {
            IEnumerable<FIN_GIRO> result = new List<FIN_GIRO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Helper/GetGiro?cb=" + kdcb + "&kdcustomer=" + kdcust + "&nomor=" + nomor);
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<FIN_GIRO>>(response.Message);

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

        public async Task<IActionResult> GetGiroBeli(string kdsup)
        {
            IEnumerable<FIN_GIRO> result = new List<FIN_GIRO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;


            try
            {
                response = await client.CallAPIGet("Helper/GetGiroBeli?cb=" + BranchID + "&kdsup=" + kdsup );
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<FIN_GIRO>>(response.Message);

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

        public async Task<IActionResult> listgiro()
        {
            IEnumerable<FIN_GIRO> result = new List<FIN_GIRO>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("FIN_GIRO/GetFINGiro");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_GIRO>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<IActionResult> GetJenisGiro()
        {
            IEnumerable<SIF_Gen_Reff_D> result = new List<SIF_Gen_Reff_D>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("Helper/GetJenisGiro");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Gen_Reff_D>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<IActionResult> GetDivisi()
        {
            IEnumerable<SIF_Departemen> result = new List<SIF_Departemen>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("Helper/GetDivisi");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Departemen>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<IActionResult> GetBankAsal()
        {
            IEnumerable<SIF_Gen_Reff_D> result = new List<SIF_Gen_Reff_D>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("Helper/GetBankAsal");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Gen_Reff_D>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<IActionResult> GetBankTujuan()
        {
            IEnumerable<SIF_Bank> result = new List<SIF_Bank>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("Helper/GetBankTujuan");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Bank>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<IActionResult> GetValuta()
        {
            IEnumerable<SIF_Valuta> result = new List<SIF_Valuta>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("Helper/GetValuta");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Valuta>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<IActionResult> GetKartu()
        {
            IEnumerable<SIF_CUSTOMER> result = new List<SIF_CUSTOMER>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("Helper/GetKartu");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_CUSTOMER>>(response.Message);
            }
            return Ok(result);
        }


        public async Task<IActionResult> Save(FIN_GIRO info)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var JenisUsaha = claimsIdentity.FindFirst("JenisUsaha").Value;
            var akses_penjualan = claimsIdentity.FindFirst("akses_penjualan").Value;

            info.Last_Created_By = UserID;
            info.Kd_Cabang = BranchID;
            info.Last_Create_Date = DateTime.Now;
            info.jns_trans = "JUAL";
            info.tipe_trans = "JRR-KPT-10";
            info.status = "DITERIMA";
            info.kartu = info.kartu;


            response = await client.CallAPIPost("FIN_Giro/SaveGiro", info);

            return Ok(response);
        }
        #endregion

        #region "monitoring Giro"
        public IActionResult MonitoringGiro()
        {
            return View();
        }

        #endregion
    }
}