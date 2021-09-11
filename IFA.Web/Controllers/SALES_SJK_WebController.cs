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
using System.IO;
using ERP.Web;
using IFA.Domain.Utils;

namespace IFA.Api.Controllers
{
    public class SALES_SJK_WebController : BaseController
    {

        public SALES_SJK_WebController(FactoryClass factoryClass,
           IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Index(string id = "", string mode = "")
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("SALES_SJ/GetListSJK?kdcabang="+ BranchID);

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
            response = await client.CallAPIGet("SIF_Barang/GetSIFBarangCB");
            if (response.Success)
            {
                ViewBag.Barang = response.Message;

            }
            return View();      
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetSJ(string id)
        {
            IEnumerable<SALES_SJ_D> result = new List<SALES_SJ_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("SALES_SJ/GetSJ?no_sj=" + id);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SALES_SJ_D>>(response.Message);


            }
            //ViewBag.Mode = "EDIT";
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveSJK(SALES_SJ data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
          
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            data.Kd_cabang = BranchID;
            data.Last_updated_by = UserID;
            data.Tgl_balik = DateTime.Now;
            data.tipe_trans = "JPJ-KPT-01";
            var sp = data.sjdetail.FirstOrDefault().No_sp;
            for (int i = 0; i <= data.sjdetail.Count() - 1; i++)
            {
                data.sjdetail[i].Kd_cabang = data.Kd_cabang;
                data.sjdetail[i].Last_updated_by = UserID;
            }
            try
            {
               
             response = await client.CallAPIPost("SALES_SJ/SaveSJK", data);

                //if (response.Success)
                //{
                //    //System.Threading.Thread.Sleep(1000);
                //    //await DORepo.SPFIN_INS_NOTA_JUAL_LANGSUNG2(data.No_sp, trans, conn);
                //    await client.CallAPIGet("DO/JurnalNota?no_sp=" + data.No_sp);
                //    var invNo = await client.CallAPIGet("Helper/GetNoInv?refNo=" + data.No_sp);
                //    string no = JsonConvert.DeserializeObject<string>(invNo.Message);
                //    response = await client.CallAPIGet("DO/JurnalSO?invNo=" + no + "&tipetrans=" + data.tipe_trans);
                //}
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

        public async Task<IActionResult> editSJ(string id = "", string mode = "")
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("SALES_SJ/GetListSJK?kdcabang=" + BranchID);

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
            response = await client.CallAPIGet("SIF_Barang/GetSIFBarangCB");
            if (response.Success)
            {
                ViewBag.Barang = response.Message;

            }
            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult>saveEditSJ(SALES_SJ data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            data.Kd_cabang = BranchID;
            data.Last_updated_by = UserID;
            data.Tgl_balik = DateTime.Now;
            data.tipe_trans = "JPJ-KPT-01";

            for (int i = 0; i <= data.sjdetail.Count() - 1; i++)
            {
                data.sjdetail[i].Kd_cabang = data.Kd_cabang;
                data.sjdetail[i].Last_updated_by = UserID;
                data.sjdetail[i].no_sj2 = data.no_sj2;
            }
            try
            {

                response = await client.CallAPIPost("SALES_SJ/editSJ", data);

                //if (response.Success)
                //{
                //    System.Threading.Thread.Sleep(1000);
                //    // await DORepo.SPFIN_INS_NOTA_JUAL_LANGSUNG2(data.No_sp, trans, conn);
                //    await client.CallAPIGet("DO/JurnalNota?no_sp=" + data.No_sp);
                //    var invNo = await client.CallAPIGet("Helper/GetNoInv?refNo=" + data.No_sp);
                //    string no = JsonConvert.DeserializeObject<string>(invNo.Message);
                //    response = await client.CallAPIGet("DO/JurnalSO?invNo=" + no + "&tipetrans=" + data.tipe_trans);
                //}
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
                var ret = await client.CallAPIGet("SALES_SJ/GetMonSJ_del?no_sj=" + id + "&jns_sj=SJK"); 
                if (ret.Success)
                {
                    sj = JsonConvert.DeserializeObject<List<SALES_SJ>>(ret.Message).FirstOrDefault();
                    sj.status_sj = "BATAL";
                    sj.Program_name = "Pembatalan SJK";
                    sj.Last_updated_by = UserID;
                    sj.Last_update_date = DateTime.Now;
                    response = await client.CallAPIGet("SALES_SJ/GetDtlSJ_del?no_sj=" + id + "&jns_sj=SJK");

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
