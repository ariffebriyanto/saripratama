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
    public class JenisBayarController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public JenisBayarController(FactoryClass factoryClass,
         IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }


        public async Task<IActionResult> GetTipeTrans()
        {
            IEnumerable<SIF_TIPE_TRANS> result = new List<SIF_TIPE_TRANS>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("SIF_TIPE_TRANS/GetTipeTrans");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SIF_TIPE_TRANS>>(response.Message);

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

        public async Task<IActionResult> GetTipeTransMon(SIF_TIPE_TRANSVMON tipe)
        {
            IEnumerable<SIF_TIPE_TRANSVMON> result = new List<SIF_TIPE_TRANSVMON>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("SIF_TIPE_TRANS/GetTipeTransMon?kd_jurnal=" + tipe.kd_jurnal);
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SIF_TIPE_TRANSVMON>>(response.Message);

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
        public async Task<IActionResult> GetJnsJurnal()
        {
            IEnumerable<SIF_Gen_Reff_D> result = new List<SIF_Gen_Reff_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Helper/GetJnsJurnal");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SIF_Gen_Reff_D>>(response.Message);

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

        public async Task<IActionResult> GetJnsBayar()
        {
            IEnumerable<SIF_Gen_Reff_D> result = new List<SIF_Gen_Reff_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Helper/GetJnsBayar");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SIF_Gen_Reff_D>>(response.Message);

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
    }
}