﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using ERP.Domain.Base;
using ERP.Web.Controllers;
using ERP.Web.Utils;
using IFA.Domain.Helpers;
using IFA.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using System.Diagnostics;
using ERP.Web;
using System.IO;
using IFA.Domain.Utils;

namespace IFA.Web.Controllers
{
    public class PelunasanPiutangController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public PelunasanPiutangController(FactoryClass factoryClass,
           IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }

        public async Task<IActionResult> GetMonMutasiGudang(string id, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            IEnumerable<INV_GUDANG_OUT> result = new List<INV_GUDANG_OUT>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            var tipe_trans = "JPB-KUT-02";
            response = await client.CallAPIGet("INV_GUDANG_OUT/GetGudangOut?no_trans=" + id + "&tipe_trans=" + tipe_trans + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&cb=" + BranchID);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_GUDANG_OUT>>(response.Message);

            }

            return Ok(result);
        }

        public async Task<IActionResult> GetInvoice(string kdsupplier, string kdvaluta)
        {
            IEnumerable<FIN_NOTA> result = new List<FIN_NOTA>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            if (kdsupplier != null && kdsupplier != "")
            {
                response = await client.CallAPIGet("FIN_AR_LUNAS/GetInvoice?cb=" + BranchID + "&kdsup=" + kdsupplier + "&kdvaluta=" + kdvaluta + "");
            }
            else
            {
                kdsupplier = "xxxxxxxxxxxxxyyxxxxxxxxxxx";
                response = await client.CallAPIGet("FIN_AR_LUNAS/GetInvoice?cb=" + BranchID + "&kdsup=" + kdsupplier + "&kdvaluta=" + kdvaluta + "");
            }
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_NOTA>>(response.Message);

            }

            return Ok(result);
        }

      
        public async Task<IActionResult> GetSupplier()
        {
            IEnumerable<SIF_Supplier> result = new List<SIF_Supplier>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("SIF_Supplier/GetALL_Supplier");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SIF_Supplier>>(response.Message);

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