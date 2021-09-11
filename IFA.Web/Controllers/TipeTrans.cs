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
    public class TipeTransController : BaseController
    {
        public TipeTransController(FactoryClass factoryClass,
         IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<SIF_TIPE_TRANS> result = new List<SIF_TIPE_TRANS>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("SIF_TIPE_TRANS/GetTipeTrans");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_TIPE_TRANS>>(response.Message);
            }
            ViewBag.TipeTrans = response.Message;
            return View(result);
        }

        public async Task<IActionResult> Save(SIF_TIPE_TRANSVM info)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            List<SIF_TIPE_TRANS> data = new List<SIF_TIPE_TRANS>();
            try
            {
                info.updateby = UserID;
                info.updatedate = DateTime.Now;
                response = await client.CallAPIPost("SIF_TIPE_TRANS/Update", info);
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
