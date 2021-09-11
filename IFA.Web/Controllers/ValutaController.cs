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
    public class ValutaController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public ValutaController(FactoryClass factoryClass,
         IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }

        public async Task<IActionResult> GetValuta()
        {
            IEnumerable<SIF_Valuta> result = new List<SIF_Valuta>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Helper/GetValuta");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SIF_Valuta>>(response.Message);

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