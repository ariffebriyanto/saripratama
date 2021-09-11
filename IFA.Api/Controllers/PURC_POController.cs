using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ERP.Api;
using ERP.Api.Utils;
using ERP.Domain.Base;
using IFA.Api.Repositories;
using IFA.Api.Utils;
using IFA.Domain.Models;
using IFA.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;

namespace IFA.Api.Controllers
{
    [Route("api/[controller]")]
  //  [ApiController]
    public class PURC_POController : ControllerBase
    {
        [HttpGet("GetPO")]
        public async Task<ActionResult<string>> GetPO(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null)
        {
            var successReponse = new Response();
            IEnumerable<PURC_PO> ListPO = new List<PURC_PO>();

            try
            {
              
                ListPO = await PURC_PORepo.GetPO(no_po, DateFrom, DateTo, status_po, barang);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetPOPartial")]
        public async Task<ActionResult<string>> GetPOPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string barang)
        {
            var successReponse = new Response();
            List<PURC_PO> ListPO = new List<PURC_PO>();

            try
            {
                var filterquery = "";
                var sortingquery = "";
                if (filter != null && filter != "" && filter != "null")
                {
                    var filtermodel = JsonConvert.DeserializeObject<KendoGridParameterParser.Models.Filter>(filter);

                    filterquery = FilterExtension.RecursiveFilterExpressionBuilder(filtermodel);

                }

                if (sorting != null && sorting != "" && sorting != "null")
                {
                    var sortingmodel = JsonConvert.DeserializeObject<List<SortDescription>>(sorting);
                    if (sortingmodel.Count != 0)
                    {
                        sortingquery = SortingExtension.SortExpressionBuilder(sortingmodel);
                        sortingquery= string.Format(" ORDER BY {0}", sortingquery);
                    }

                }
                int seq = pageSize + skip;
                ListPO = await PURC_PORepo.GetPOPartial(DateFrom, DateTo, filterquery, sortingquery, barang, seq);

                var query = ListPO.AsEnumerable().Skip(skip).Take(pageSize);

                successReponse.Success = true;
                successReponse.Result = PURC_PORepo.getCountPO(DateFrom, DateTo, filterquery, barang, seq).Result;
                successReponse.Message = JsonConvert.SerializeObject(query.ToList());
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetReturListPartial")]
        public async Task<ActionResult<string>> GetReturListPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string barang)
        {
            var successReponse = new Response();
            try
            {
                var filterquery = "";
                var sortingquery = "";
                if (filter != null && filter != "" && filter != "null")
                {
                    var filtermodel = JsonConvert.DeserializeObject<KendoGridParameterParser.Models.Filter>(filter);

                    filterquery = FilterExtension.RecursiveFilterExpressionBuilder(filtermodel);

                }

                if (sorting != null && sorting != "" && sorting != "null")
                {
                    var sortingmodel = JsonConvert.DeserializeObject<List<SortDescription>>(sorting);
                    if (sortingmodel.Count != 0)
                    {
                        sortingquery = SortingExtension.SortExpressionBuilder(sortingmodel);
                        sortingquery = string.Format(" ORDER BY {0}", sortingquery);
                    }

                }
                int seq = pageSize + skip;
                var res = await PURC_PORepo.GetReturListPartial(DateFrom, DateTo, filterquery, sortingquery, barang, seq);

                var query = res.AsEnumerable().Skip(skip).Take(pageSize);

                successReponse.Success = true;
                successReponse.Result = PURC_PORepo.GetCountReturListPartial(DateFrom, DateTo, filterquery, barang, seq).Result;
                successReponse.Message = JsonConvert.SerializeObject(query.ToList());
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetLastPOMonitoring")]
        public async Task<ActionResult<string>> GetLastPOMonitoring(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null)
        {
            var successReponse = new Response();
            IEnumerable<v_last_purc> ListPO = new List<v_last_purc>();

            try
            {

                ListPO = await PURC_PORepo.GetPOLast(no_po, DateFrom, DateTo, status_po, barang);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetLastPOPartialMonitoring")]
        public async Task<ActionResult<string>> GetLastPOPartialMonitoring(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string barang)
        {
            var successReponse = new Response();
            IEnumerable<v_last_purc> ListPO = new List<v_last_purc>();

            try
            {

                var filterquery = "";
                var sortingquery = "";
                if (filter != null && filter != "" && filter != "null")
                {
                    var filtermodel = JsonConvert.DeserializeObject<KendoGridParameterParser.Models.Filter>(filter);

                    filterquery = FilterExtension.RecursiveFilterExpressionBuilder(filtermodel);

                }

                if (sorting != null && sorting != "" && sorting != "null")
                {
                    var sortingmodel = JsonConvert.DeserializeObject<List<SortDescription>>(sorting);
                    if (sortingmodel.Count != 0)
                    {
                        sortingquery = SortingExtension.SortExpressionBuilder(sortingmodel);
                        sortingquery = string.Format(" ORDER BY {0}", sortingquery);
                    }

                }
                int seq = pageSize + skip;
                ListPO = await PURC_PORepo.GetPOLastPartial(DateFrom, DateTo, filterquery, sortingquery, barang, seq);

                var query = ListPO.AsEnumerable().Skip(skip).Take(pageSize);

                successReponse.Success = true;
                successReponse.Result = PURC_PORepo.GetCountPOLastPartial(DateFrom, DateTo, filterquery, barang, seq).Result;
                successReponse.Message = JsonConvert.SerializeObject(query.ToList());
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetPOMobile")]
        public async Task<ActionResult<string>> GetPOMobile(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null)
        {
            var successReponse = new Response();
            IEnumerable<PURC_PO> ListPO = new List<PURC_PO>();

            try
            {

                ListPO = await PURC_PORepo.GetPOMobile(no_po, DateFrom, DateTo, status_po, barang);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }


        [HttpGet("GetPrintPO")]
        public async Task<ActionResult<string>> GetPrintPO(string no_po = null)
        {
            var successReponse = new Response();
            PrintPO ListPO = new PrintPO();

            try
            {
                ListPO = await PURC_PORepo.GetPrintPO(no_po);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetDetailPO")]
        public async Task<ActionResult<string>> GetDetailPO(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null)
        {
            var successReponse = new Response();
            IEnumerable<PURC_PO_D> ListPO = new List<PURC_PO_D>();

            try
            {
                ListPO = await PUPR_PO_DRepo.GetPODetail(no_po, DateFrom, DateTo, status_po, barang);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }


        [HttpGet("GetPOPenerimaan")]
        public async Task<ActionResult<string>> GetPOPenerimaan(string no_po = null)
        {
            var successReponse = new Response();
            PURC_PO ListPO = new PURC_PO();

            try
            {
                ListPO = await PUPR_PO_DRepo.GetPOPenerimaan(no_po);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetDetailPOPenerimaan")]
        public async Task<ActionResult<string>> GetDetailPOPenerimaan(string no_po = null)
        {
            var successReponse = new Response();
            IEnumerable<PURC_PO_D> ListPO = new List<PURC_PO_D>();

            try
            {
                ListPO = await PUPR_PO_DRepo.GetDetailPOPenerimaan(no_po);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetPODtlCbg")]
        public async Task<ActionResult<string>> GetPODtlCbg(string kd_cabang = null, string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null)
        {
            var successReponse = new Response();
            IEnumerable<PURC_PO_D> ListPO = new List<PURC_PO_D>();

            try
            {
                ListPO = await PUPR_PO_DRepo.GetPODtlCbg(kd_cabang,no_po, DateFrom, DateTo, status_po, barang);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("SavePO")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SavePO([FromForm] PURC_PO data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
               await PURC_PORepo.SavePO(data, trans);
                if (data.podetail != null)
                {
                    await PUPR_PO_DRepo.DeletePODetail(data.podetail[0].no_po, trans);
                    foreach (PURC_PO_D podetail in data.podetail)
                    {
                        await PUPR_PO_DRepo.SavePODetail(podetail, trans);
                    }
                }

                if(data.listdpm != null)
                {
                    foreach (PURC_DPM_D dpm in data.listdpm)
                    {
                        PURC_DPM_DRepo.UpdateNo_PO(dpm, trans);
                    }
                }
                if (Startup.application != "development")
                {
                    if (data.status_po == "ENTRY")
                    {
                        approvalNotificaton(data);
                    }
                }
                    
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = "success";

                //all save success -> commit set true
                DataAccess.CloseTransaction(conn, trans, true);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                //ada fail -> rollback
                DataAccess.CloseTransaction(conn, trans, false);

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return successReponse;
        }

        [HttpPost("UpdatePO")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> UpdatePO([FromForm] PURC_PO data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
               await PURC_PORepo.UpdatePO(data, trans);
                if (data.podetail != null)
                {
                    await PUPR_PO_DRepo.DeletePODetail(data.podetail[0].no_po, trans);
                    foreach (PURC_PO_D podetail in data.podetail)
                    {
                        await PUPR_PO_DRepo.SavePODetail(podetail, trans);
                    }
                }
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = "success";

                //all save success -> commit set true
                DataAccess.CloseTransaction(conn, trans, true);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                //ada fail -> rollback
                DataAccess.CloseTransaction(conn, trans, false);

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return successReponse;
        }

        [HttpGet("GenPONumber")]
        public async Task<ActionResult<string>> GenPONumber(string prefix = null, DateTime? DateFrom = null)
        {
            var successReponse = new Response();
           

            try
            {
                string nopo= await PURC_PORepo.GenPONumber(prefix, DateFrom);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(nopo);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetApprovalPO")]
        public async Task<ActionResult<string>> GetApprovalPO(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null)
        {
            var successReponse = new Response();
            IEnumerable<PURC_PO> ListPO = new List<PURC_PO>();

            try
            {
               
                ListPO = await PURC_PORepo.GetApprovalPO(no_po, DateFrom, DateTo, status_po);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetDetailApprovalPO")]
        public async Task<ActionResult<string>> GetDetailApprovalPO(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null)
        {
            var successReponse = new Response();
            IEnumerable<PURC_PO_D> ListPO = new List<PURC_PO_D>();

            try
            {
                ListPO = await PUPR_PO_DRepo.GetDetailApprovalPO(no_po, DateFrom, DateTo, status_po);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("SaveApprovalPO")]
        public async Task<ActionResult<Response>> SaveApprovalPO([FromForm] List<PURC_PO> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try

            {
                foreach (PURC_PO detail in data)
                {
                   await PURC_PORepo.UpdatePO(detail, trans);
                    if (detail.rec_stat== "APPROVE")
                    {
                       await PURC_PORepo.SPLast_Price(detail, trans, conn);
                    }
                }

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = "success";

                //all save success -> commit set true
                DataAccess.CloseTransaction(conn, trans, true);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                //ada fail -> rollback
                DataAccess.CloseTransaction(conn, trans, false);

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return successReponse;
        }

        [HttpPost("UpdateApprovalMobile")]
        public async Task<ActionResult<string>> UpdateApprovalMobile([FromBody] POMASTERApprovalVM data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            IEnumerable<PURC_PO> ListPO = new List<PURC_PO>();
            try
            {
                ListPO = await PURC_PORepo.GetPO(data.ID);

                foreach (PURC_PO detail in ListPO)
                {
                    detail.rec_stat = data.StatusDesc;
                    detail.status_po = data.StatusDesc;
                    detail.Last_Updated_By = data.UserID;
                    detail.Last_Update_Date = DateTime.Now;

                    if (detail.rec_stat == "APPROVE")
                    {
                        detail.status_po = "OPEN";
                        detail.user_approve = data.UserID;
                        detail.tgl_approve = DateTime.Now;
                        detail.ket_batal = "";
                    }
                    else if (detail.rec_stat == "REJECT")
                    {
                        detail.status_po = "BATAL";
                    }

                   await PURC_PORepo.UpdatePO(detail, trans);
                    if (detail.rec_stat == "APPROVE")
                    {
                       await PURC_PORepo.SPLast_Price(detail, trans, conn);
                    }
                }

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = "success";

                //all save success -> commit set true
                DataAccess.CloseTransaction(conn, trans, true);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                //ada fail -> rollback
                DataAccess.CloseTransaction(conn, trans, false);

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return JsonConvert.SerializeObject(successReponse);
        }

        private void approvalNotificaton(PURC_PO header)
        {
            pushnotif obj = new pushnotif();
            obj.title = "PO Approval";

            AuthRepo repo = new AuthRepo();

            string role = "";
            obj.body = "Waiting Approval, PO No: " + header.no_po;
            role = "UAT";
            obj.landing_page = "po-approval";

            IEnumerable<AuthenticationVM> usermodel = repo.getTokenByRole(role);
            if (usermodel != null && usermodel.Count() > 0)
            {
                foreach (var user in usermodel)
                {
                    if(user.token !=null && user.token != string.Empty)
                    {
                        obj.to = user.token;
                        PushNotification pushNotification = new PushNotification(obj);
                    }
                }
            }
        }

        [HttpPost("PembatalanPO")]
        public async Task<ActionResult<Response>> PembatalanPO([FromForm] PURC_PO data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            string no_po = "";
            try
            {
                //notrns = data.gudangin.FirstOrDefault().no_trans;
                no_po = data.no_po;
                var kdcb = data.gudangin.FirstOrDefault().Kd_Cabang;
                await PURC_PORepo.UpdatePO(data, trans);
                if (data.gudangin != null && data.gudangin.Count()>0)
                {
                 
                    foreach(var item in data.gudangin)
                    {
                        await GUDANG_OUTRepo.SPStokOut(item.Kd_Cabang, item.blthn, item.kd_stok, item.kd_satuan, item.qty_in, trans, conn);
                       // await GUDANG_OUTRepo.SPGudangOut(item.Kd_Cabang, DateTime.Now.ToString("yyyyMM"), item.kd_stok, item.kd_satuan, item.qty_in, item.gudang_tujuan, trans, conn);
                    }
                }

                await INV_GUDANG_IN_Repo.DelGudangIN(no_po, trans);
                await INV_GUDANG_IN_Repo.DelGudangINDtl(no_po, trans);
                await INV_GUDANG_IN_Repo.DelQC(no_po, trans);
                await INV_GUDANG_IN_Repo.DelQC_detail(no_po, trans);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = "success";

                //all save success -> commit set true
                DataAccess.CloseTransaction(conn, trans, true);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                //ada fail -> rollback
                DataAccess.CloseTransaction(conn, trans, false);

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return successReponse;
        }

        [HttpGet("GetReturInv")]
        public string GetReturInv(string kd_cabang)
        {
            var successReponse = new Response();
            try
            {
                var res = PUPR_PO_DRepo.GetReturInv(kd_cabang);
                var list = res.Select(x => new
                {
                    x.no_po,
                    x.Nama_Supplier,
                    x.no_jur,
                    x.kd_supplier
                });
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(list);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetPORetur")]
        public string GetPORetur(string no_po)
        {
            var successReponse = new Response();
            try
            {
                var res = PUPR_PO_DRepo.GetPORetur(no_po);
                successReponse.Success  = true;
                successReponse.Result   = "success";
                successReponse.Message  = JsonConvert.SerializeObject(res);
            }
            catch (Exception e)
            {
                successReponse.Success  = false;
                successReponse.Result   = "failed";
                successReponse.Message  = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("SaveReturPO")]
        public async Task<ActionResult<Response>> SaveReturPO([FromForm] PURC_RETUR_BELI data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                await PURC_PORepo.SaveReturPO(data, trans);
                if (data.detail != null)
                {
                    foreach (PURC_RETUR_BELI_D item in data.detail.Where(w => w.qty > 0))
                    {
                       await PUPR_PO_DRepo.SaveReturPODetail(item, trans);
                     
                    }
                }


                // await PUPR_PO_DRepo.SPFIN_AUTOMAN_JURNALReturPO(data.no_retur, trans, conn);
                Thread t = new Thread(() => JurnalReturPO(data.no_retur));
                t.Start();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = "success";

                //all save success -> commit set true
                DataAccess.CloseTransaction(conn, trans, true);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                //ada fail -> rollback
                DataAccess.CloseTransaction(conn, trans, false);

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return successReponse;
        }


        public void JurnalReturPO(string no_trans)
        {
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);

            try
            {
                PUPR_PO_DRepo.SPFIN_AUTOMAN_JURNALReturPO(no_trans, trans, conn);
                DataAccess.CloseTransaction(conn, trans, true);
            }
            catch (Exception e)
            {
                DataAccess.CloseTransaction(conn, trans, false);
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }
        }


        [HttpGet("GetRetur")]
        public string GetRetur(string no_retur)
        {
            var successReponse = new Response();
            try
            {
                var res = PUPR_PO_DRepo.GetRetur(no_retur).FirstOrDefault();
                
                var detail = PUPR_PO_DRepo.GetReturDetail(no_retur);
                res.detail = new List<PURC_RETUR_BELI_D>();
                res.detail = detail.ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(res);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetLastPO")]
        public async Task<ActionResult<string>> GetLastPO(string kd_barang)
        {
            var successReponse = new Response();
            PURC_PO_D ListPO = new PURC_PO_D();

            try
            {

                ListPO = await PUPR_PO_DRepo.GetLastPO(kd_barang);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetReturList")]
        public string GetReturList(DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var successReponse = new Response();
            try
            {
                var res = PUPR_PO_DRepo.GetReturList(DateFrom, DateTo);
                
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(res);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

       

    }
}