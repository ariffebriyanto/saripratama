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
    [ApiController]
    public class DOController : ControllerBase
    {
        [HttpGet("GetDO_mon")]
        public async Task<ActionResult<string>> GetDO_mon(string no_po = null, string dt1 = null,string dt2 = null, string status_po = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SO> ListPO = new List<SALES_SO>();
            IEnumerable<SALES_SO_DVM> ListPOD = new List<SALES_SO_DVM>();

            try
            {

                ListPO = await DORepo.GetDO_mon(no_po, dt1, dt2, status_po, barang, cb);
                if (no_po != null)
                {
                    ListPOD = await DORepo.GetDODetail(no_po, dt1, dt2, status_po, barang, cb);

                    if (ListPOD != null && ListPOD.Count() > 0)
                    {
                        ListPO.FirstOrDefault().detailsvm = new List<SALES_SO_DVM>();
                        foreach (var item in ListPOD)
                        {
                            ListPO.FirstOrDefault().detailsvm.Add(item);
                        }
                    }
                }


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

        [HttpGet("GetDO")]
        public async Task<ActionResult<string>> GetDO(string no_po = null, string dt1 = null, string dt2 = null, string status_po = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SO> ListPO = new List<SALES_SO>();
            IEnumerable<SALES_SO_DVM> ListPOD = new List<SALES_SO_DVM>();

            try
            {

                ListPO = await DORepo.GetDO(no_po, dt1, dt2, status_po, barang, cb);
                if (no_po != null)
                {
                    ListPOD = await DORepo.GetDODetail(no_po, dt1, dt2, status_po, barang, cb);

                    if (ListPOD != null && ListPOD.Count() > 0)
                    {
                        if (ListPO != null && ListPO.Count() > 0)
                        {
                            ListPO.FirstOrDefault().detailsvm = new List<SALES_SO_DVM>();
                        }
                            
                        foreach (var item in ListPOD)
                        {
                            if (ListPO != null && ListPO.Count() > 0)
                            {
                                ListPO.FirstOrDefault().detailsvm.Add(item);
                            }
                        }
                    }
                }


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

        [HttpGet("GetDOPartial")]
        public async Task<ActionResult<string>> GetDOPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string kdcb)
        {
            var successReponse = new Response();
            List<SALES_SO> ListPO = new List<SALES_SO>();

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
                ListPO = await DORepo.GetDOPartial(DateFrom, DateTo, kdcb, filterquery, sortingquery, seq);

                var query = ListPO.AsEnumerable().Skip(skip).Take(pageSize);

                successReponse.Success = true;
                successReponse.Result = DORepo.getCountDO(DateFrom, DateTo, filterquery, kdcb, seq).Count().ToString();
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


        [HttpGet("GetByDO")]
        public async Task<ActionResult<string>> GetByDO(string no_po = null, string cb = null, string dt1 = null, string dt2 = null, string status_po = null, string barang = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SO> ListPO = new List<SALES_SO>();
            IEnumerable<SALES_SO_DVM> ListPOD = new List<SALES_SO_DVM>();

            try
            {

                ListPO = await DORepo.GetByDO(no_po, dt1, dt2, cb);
                if (no_po != null || dt1 != null || dt2 != null)
                {
                    ListPOD = await DORepo.GetByDODetail(no_po, dt1, dt2, status_po, barang, cb);

                    if (ListPOD != null && ListPOD.Count() > 0)
                    {
                        ListPO.FirstOrDefault().detailsvm = new List<SALES_SO_DVM>();
                        foreach (var item in ListPOD)
                        {
                            ListPO.FirstOrDefault().detailsvm.Add(item);
                        }
                    }
                }


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
        [HttpGet("GetByCust")]
        public async Task<ActionResult<string>> GetByCust(string kd_cust = null, string cb = null, string DateFrom = null, string DateTo = null, string status_po = null, string barang = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SO> ListPO = new List<SALES_SO>();
            IEnumerable<SALES_SO_DVM> ListPOD = new List<SALES_SO_DVM>();

            try
            {

                ListPO = await DORepo.GetByCust(kd_cust, DateFrom, DateTo, cb);
                var no_no = ListPO.FirstOrDefault().No_sp;
                if (kd_cust != null || DateFrom != null || DateTo != null)
                {
                    ListPOD = await DORepo.GetByDODetail(no_no, DateFrom, DateTo, status_po, barang, cb);

                    if (ListPOD != null && ListPOD.Count() > 0)
                    {
                        ListPO.FirstOrDefault().detailsvm = new List<SALES_SO_DVM>();
                        foreach (var item in ListPOD)
                        {
                            ListPO.FirstOrDefault().detailsvm.Add(item);
                        }
                    }
                }


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

        //[HttpGet("GetByStok")]
        //public async Task<ActionResult<string>> GetByStok(string kd_stok = null, string cb = null, DateTime? dt1 = null, DateTime? dt2 = null, string status_po = null, string barang = null)
        //{
        //    var successReponse = new Response();
        //    IEnumerable<SALES_SO> ListPO = new List<SALES_SO>();
        //    IEnumerable<SALES_SO_DVM> ListPOD = new List<SALES_SO_DVM>();

        //    try
        //    {

        //        ListPO = await DORepo.GetByStok(kd_stok, dt1, dt2, cb);
        //        if (kd_stok != null || dt1 != null || dt2 != null)
        //        {
        //            ListPOD = await DORepo.GetByStokDetail(kd_stok, dt1, dt2, status_po, barang, cb);

        //            if (ListPOD != null && ListPOD.Count() > 0)
        //            {
        //                ListPO.FirstOrDefault().detailsvm = new List<SALES_SO_DVM>();
        //                foreach (var item in ListPOD)
        //                {
        //                    ListPO.FirstOrDefault().detailsvm.Add(item);
        //                }
        //            }
        //        }


        //        successReponse.Success = true;
        //        successReponse.Result = "success";
        //        successReponse.Message = JsonConvert.SerializeObject(ListPO);
        //    }
        //    catch (Exception e)
        //    {
        //        successReponse.Success = false;
        //        successReponse.Result = "failed";
        //        successReponse.Message = e.Message;

        //        StackTrace st = new StackTrace(e, true);
        //        StackFrame frame = st.GetFrame(st.FrameCount - 1);
        //        string fileName = frame.GetFileName();
        //        string methodName = frame.GetMethod().Name;
        //        int line = frame.GetFileLineNumber();

        //        if (Startup.application != "development")
        //        {
        //            var path = Path.Combine(Startup.contentRoot, "appsettings.json");

        //            string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
        //            EmailErrorLog.SendEmail(emailbody, path);
        //        }

        //    }
        //    return JsonConvert.SerializeObject(successReponse);
        //}

        [HttpGet("GetDODetail")]
        public async Task<ActionResult<string>> GetDODetail(string no_po = null, string dt1 = null, string dt2 = null, string status_po = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SO_DVM> ListPOD = new List<SALES_SO_DVM>();
            try
            {
                ListPOD = await DORepo.GetDODetail(no_po, dt1, dt2, status_po, barang, cb);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPOD);
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

        [HttpGet("GetSOD")]
        public async Task<ActionResult<string>> GetSOD(string no_po = null, DateTime? dt1 = null, DateTime? dt2 = null, string status_po = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SO_D> ListPOD = new List<SALES_SO_D>();
            try
            {
                ListPOD = await DORepo.GetSO_D(no_po, dt1, dt2, status_po, barang, cb);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPOD);
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


        [HttpPost("UpdateBO")]
        public async Task<ActionResult<Response>>  UpdateBO([FromForm] List<SALES_SO_D> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SALES_SO_D item in data)
                {
                    if (item.alokasi > 0)
                    {
                        await DORepo.UpdateBO(item, trans);
                    }
                }
                // data = SIF_PegawaiRepo.assignData(data);


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
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return successReponse;
        }

        [HttpPost("SaveBOStokBoked")]
        public async Task<ActionResult<Response>> SaveBOStokBoked([FromForm] List<SALES_SO_D> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SALES_SO_D item in data)
                {
                    
                    await DORepo.SaveBOStokBoked(item, trans);

                }
                // data = SIF_PegawaiRepo.assignData(data);


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
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return successReponse;
        }


        [HttpPost("UpdateBOStokBoked")]
        public async Task<ActionResult<Response>> UpdateBOStokBoked([FromForm] List<SALES_SO_D> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SALES_SO_D item in data)
                {
                    await DORepo.UpdateBOStokBoked(item, trans);

                }
                // data = SIF_PegawaiRepo.assignData(data);


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
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return successReponse;
        }

        [HttpGet("GetBOPAKET")]
        public async Task<ActionResult<string>> GetBOPAKET(string no_po = null, DateTime? dt1 = null, DateTime? dt2 = null, string status_po = null, string kd_cust = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SO_D> ListPOD = new List<SALES_SO_D>();
            try
            {
               
                ListPOD = await DORepo.GetBOPAKET(no_po, dt1, dt2, status_po, kd_cust, cb);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPOD);
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


        [HttpGet("GetBOPAKETPartial")]
        public async Task<ActionResult<string>> GetBOPAKETPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime? DateFrom=null, DateTime? DateTo=null, string kd_cust=null, string no_po=null)
        {
            var successReponse = new Response();
            List<SALES_SO_D> ListPOD = new List<SALES_SO_D>();
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
                ListPOD = await DORepo.GetBOPAKETPartial(DateFrom, DateTo, filterquery, sortingquery, kd_cust, seq, no_po);

                var query = ListPOD.AsEnumerable().Skip(skip).Take(pageSize);

                successReponse.Success = true;
                successReponse.Result = DORepo.getCountAlokasi(DateFrom, DateTo, filterquery, sortingquery, kd_cust, seq, no_po).Count().ToString();
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

        [HttpGet("GetNotaSM")]
        public async Task<ActionResult<string>> GetNotaSM(string no_po = null, DateTime? dt1 = null, DateTime? dt2 = null, string status_po = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SO> ListPO = new List<SALES_SO>();
            IEnumerable<SALES_SO_DVM> ListPOD = new List<SALES_SO_DVM>();

            try
            {
                //cetak nota sementara
                ListPO = await DORepo.GetNotaSM(no_po, dt1, dt2, status_po, barang, cb);
                if (no_po != null)
                {
                    ListPOD = await DORepo.GetNotaSMDetail(no_po, dt1, dt2, status_po, barang, cb);

                    if (ListPOD != null && ListPOD.Count() > 0)
                    {
                        ListPO.FirstOrDefault().detailsvm = new List<SALES_SO_DVM>();
                        foreach (var item in ListPOD)
                        {
                            ListPO.FirstOrDefault().detailsvm.Add(item);
                        }
                    }
                }


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

        [HttpGet("GetDORetur")]
        public async Task<ActionResult<string>> GetDORetur(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SO> ListPO = new List<SALES_SO>();
            IEnumerable<SALES_SO_DVM> ListPOD = new List<SALES_SO_DVM>();

            try
            {

                ListPO = await DORepo.GetDORetur(no_po, DateFrom, DateTo, status_po, barang);
                if (no_po != null)
                {
                    ListPOD = await DORepo.GetDODetailRetur(no_po, DateFrom, DateTo, status_po, barang);

                    if (ListPOD != null)
                    {
                        ListPO.FirstOrDefault().detailsvm = new List<SALES_SO_DVM>();
                        foreach (var item in ListPOD)
                        {
                            ListPO.FirstOrDefault().detailsvm.Add(item);
                        }
                    }
                }


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

        [HttpGet("GetPiutangCustomer")]
        public async Task<ActionResult<string>> GetPiutangCustomer(string kd_cust = null)
        {
            var successReponse = new Response();
            IEnumerable<PiutangSOVM> ListPO = new List<PiutangSOVM>();

            try
            {
                ListPO = await DORepo.GetPiutangCustomer(kd_cust);

                //if(ListPO != null && ListPO.Count() > 0)
                //{
                //    foreach(var item in ListPO)
                //    {
                //        item.total = ListPO.Sum(s => s.jml_akhir);
                //    }
                //}

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


        [HttpPost("Save")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> Save([FromForm] SALES_SO data)
        {
            var successReponse = new Response();


            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            string nodo = data.No_sp;
            try
            {
                ////sudah di generate di web
                if (data.No_sp != null && data.No_sp != string.Empty)
                {
                    // data.No_sp = nodo;
                    nodo = data.No_sp;
                }
                else
                {
                    // System.Threading.Thread.Sleep(700);
                    nodo = await HelperRepo.GetNoTransNew("SPSPRT", DateTime.Now, data.Kd_Cabang, trans, conn);
                    data.stat_save = 0;
                    data.No_sp = nodo;

                }
                //System.Threading.Thread.Sleep(1120);
                await DORepo.DeleteDetail(data, trans); //jika ada maka delete
                await DORepo.Save(data, trans);
                // System.Threading.Thread.Sleep(2748);
                foreach (var item in data.details.Where(x => x.kode_Barang != null && x.kode_Barang != string.Empty))
                {
                    item.No_sp = nodo;
                    await DORepo.SaveDetail(item, trans);
                     

                }

                if (data.Jenis_sp.ToUpper() == "CASH")
                {
                    await DORepo.SPFIN_INS_NOTA_JUAL_LANGSUNG(data, trans, conn);

                    foreach (var item in data.details.Where(x => x.kode_Barang != null && x.kode_Barang != string.Empty))
                    {
                        if (item.Qty > 0)
                        {
                            //sementara saat akunting posting blm jalan
                            await GUDANG_OUTRepo.SPStokOut(item.Kd_Cabang, item.blthn, item.Kd_Stok, item.Kd_satuan, item.Qty, trans, conn);
                            //SP gudang dtl Out, 
                            await GUDANG_OUTRepo.SPGudangOut(item.Kd_Cabang, item.blthn, item.Kd_Stok, item.Kd_satuan, item.Qty, item.kd_gudang, trans, conn);
                        }
                    }

                }
                else //jika non cash maka booked dan limit
                     //jika (data.Jenis_sp == "NON CASH")
                {
                    foreach (var item in data.details.Where(x => x.kode_Barang != null && x.kode_Barang != string.Empty))
                    {
                        if (item.Qty > 0)
                        {
                            await HelperRepo.SPBooked_in(item.Kd_Cabang, DateTime.Now.ToString("yyyyMM"), item.Kd_Stok, item.Kd_satuan, item.Qty, trans, conn);
                        }    
                    }
                    await DORepo.UpdateLimit(data.Kd_Customer, data.JML_VALAS_TRANS ?? 0, trans);
                }

                if (data.booked != null && data.booked.Count() > 0)
                {
                    foreach (var item in data.booked)
                    {
                        await DORepo.SaveDetailBooked(item, trans);
                    }
                }





                //  throw new Exception("Test");

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = nodo;

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

        [HttpPost("SaveRencanaKirim")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveRencanaKirim([FromForm] PROD_rcn_krm data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                await PROD_rcn_krmRepo.SaveRcnKrm(data, trans);
                if (data.rcnkrmDetail != null)
                {
                    await PROD_rcn_krm_D_Repo.DeleteRcnKirimDetail(data.rcnkrmDetail[0].no_trans, trans);
                    foreach (PROD_rcn_krm_D rcnkrmDetail in data.rcnkrmDetail)
                    {
                        await PROD_rcn_krm_D_Repo.SavercnkirimDetail(rcnkrmDetail, trans);
                        string no_sp = rcnkrmDetail.no_sp;
                        await PROD_rcn_krmRepo.UpdateSOKirim(no_sp, trans, conn);

                        string res = await DORepo.SaveSJ(rcnkrmDetail.no_trans, rcnkrmDetail.no_sp, rcnkrmDetail.kd_cabang, rcnkrmDetail.last_created_by, 2, trans, conn);
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
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return successReponse;
        }

        [HttpPost("UpdateRetur")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> UpdateRetur([FromForm] SALES_SO data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                await DORepo.UpdateRetur(data, trans);

                foreach (var item in data.details)
                {
                    await DORepo.UpdateDetailRetur(item, trans);
                }

                foreach (var item in data.details)
                {
                    //sementara saat akunting posting blm jalan
                    //Decimal QTY = item.Qty;
                    await INV_GUDANG_IN_Repo.SPStokIn(item.Kd_Cabang, item.blthn, item.Kd_Stok, item.Kd_satuan, Math.Abs(item.Qty), trans, conn);
                    await INV_GUDANG_IN_Repo.SPGudangIn(item.Kd_Cabang, item.blthn, item.Kd_Stok, item.Kd_satuan, Math.Abs(item.Qty), item.kd_gudang, trans, conn);
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

        [HttpPost("UpdateSO")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> UpdateSO([FromForm] SALES_SO data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                await DORepo.UpdateDO(data, trans);
                await DORepo.DeleteDetail(data, trans);
                foreach (var item in data.details)
                {
                    await DORepo.SaveDetail(item, trans);
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

        [HttpPost("DeleteSO")]
        public async Task<ActionResult<Response>> DeleteSO([FromForm] SALES_SO data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            string blthn = DateTime.Now.ToString("yyyyMM");
            try
            {
                await DORepo.Delete(data.No_sp, data.Last_Updated_By, trans);
                foreach (var item in data.details.Where(x => x.Kd_Stok != null && x.Kd_Stok != string.Empty))
                {
                    await HelperRepo.SP_rilis_booked(item.Kd_Cabang, blthn, item.Kd_Stok, item.Kd_satuan, item.Qty, trans, conn);

                }
                await DORepo.UpdateStatSOD(data.No_sp, data.Last_Updated_By, trans);

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

        //SaveInden
        [HttpPost("SaveInden")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveInden([FromForm] List<SALES_BOOKED> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (var item in data)
                {
                    await DORepo.SaveInden(item, trans);
                 //   await HelperRepo.SPBooked_in(item.Kd_Cabang, DateTime.Now.ToString("yyyyMM"), item.Kd_Stok, item.Kd_satuan, item.Qty, trans, conn);
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

        [HttpGet("GetInden")]
        public async Task<ActionResult<string>> GetInden(Guid? id, string status = "", string kd_customer = "", string Kd_sales = "", Guid? unix_id = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_BOOKED> ListPO = new List<SALES_BOOKED>();

            try
            {
                if (status != "")
                {
                    string[] statusSpl = status.Split(",");
                    var statusTemp = "";
                    foreach (var item in statusSpl)
                    {
                        statusTemp += "'" + item + "',";
                    }
                    status = statusTemp.Remove(statusTemp.Length - 1);
                }
                ListPO = await DORepo.GetInden(id, status, kd_customer, Kd_sales, unix_id);

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

        [HttpGet("GetIndenCreate")]
        public async Task<ActionResult<string>> GetIndenCreate(Guid? id, string status = "", string kd_customer = "", string Kd_sales = "", Guid? unix_id = null)
        {
            var successReponse = new Response();
            List<SALES_BOOKED> ListPO = new List<SALES_BOOKED>();
            List<SALES_BOOKED> ListPOTemp = new List<SALES_BOOKED>();
            List<string> idExist = new List<string>();
            try
            {
                if (status != "")
                {
                    string[] statusSpl = status.Split(",");
                    var statusTemp = "";
                    foreach (var item in statusSpl)
                    {
                        statusTemp += "'" + item + "',";
                    }
                    status = statusTemp.Remove(statusTemp.Length - 1);
                }
                ListPO = await DORepo.GetInden(id, status, kd_customer, Kd_sales, unix_id);
                foreach (var item in ListPO)
                {
                    var level = "";
                    if (item.alokasiLevel != "A" && item.alokasiLevel != "" && item.alokasiLevel != null)
                    {
                        if (item.alokasiLevel == "B")
                        {
                            level = "'A'";
                        }
                        else if (item.alokasiLevel == "C")
                        {
                            level = "'A','B'";
                        }
                        else if (item.alokasiLevel == "D")
                        {
                            level = "'A','B','C'";
                        }
                        var listExist = await DORepo.checkLevel(item.Kd_Stok, level);
                        if (listExist.Count() > 0)
                        {
                            idExist.Add(item.id.ToString());
                        }
                    }
                }

                if (idExist.Count()>0)
                {
                    foreach (var item in idExist)
                    {
                        ListPO.RemoveAll(x => x.id.ToString() == item);
                    }
                }

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


        [HttpGet("GetDPInden")]
        public async Task<ActionResult<string>> GetDPInden(string id)
        {
            var successReponse = new Response();
            IEnumerable<SALES_BOOKED> ListPO = new List<SALES_BOOKED>();

            try
            {

                ListPO = await DORepo.GetDPInden(id);

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
        [HttpGet("GetIndenList")]
        public async Task<ActionResult<string>> GetIndenList(Guid? id, string status = "", string kd_customer = "", string Kd_sales = "", Guid? unix_id = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_BOOKED> ListPO = new List<SALES_BOOKED>();

            try
            {
                if (status != "")
                {
                    string[] statusSpl = status.Split(",");
                    var statusTemp = "";
                    foreach (var item in statusSpl)
                    {
                        statusTemp += "'" + item + "',";
                    }
                    status = statusTemp.Remove(statusTemp.Length - 1);
                }

                ListPO = await DORepo.GetIndenList(id, status, kd_customer, Kd_sales, unix_id);

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


        [HttpPost("Ceksalesbooked")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> Ceksalesbooked([FromForm] SALES_SO data)
        //public async Task<ActionResult<Response>> Save([FromForm] SALES_SO data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            int stat = 0; //0=OK ; kalo 1=stok kirang
            var kdcb = data.Kd_Cabang;
            try
            {
                // data.podetail.RemoveAll(x => x.qty_qc_pass == 0);
                data.details.RemoveAll(x => x.no_booked != Guid.Empty); //|| x.no_booked != null
                foreach (var item in data.details)
                {

                    int cek_min = await DORepo.CekStok(kdcb, item.kode_Barang, item.Qty - item.qty_awal, DateTime.Now.ToString("yyyyMM"), trans);
                    if (cek_min < 0)
                    {
                        stat = 1;
                        successReponse.Success = false;
                        successReponse.Result = "failed";
                        successReponse.Message = item.nama_Barang + " Stok tidak Cukup!! " + cek_min;
                    }
                    //await HelperRepo.SPBooked_in(item.Kd_Cabang, DateTime.Now.ToString("yyyyMM"), item.Kd_Stok, item.Kd_satuan, item.Qty * -1, trans, conn);
                }

                if (stat == 0)
                {
                    successReponse.Success = true;
                    successReponse.Result = "success";
                    successReponse.Message = "success";
                }

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




        [HttpPost("CekStok")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> CekStok([FromForm] SALES_SO data)
        //public async Task<ActionResult<Response>> Save([FromForm] SALES_SO data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            int stat = 0; //0=OK ; kalo 1=stok kirang
            var kdcb = data.Kd_Cabang;
            try
            {
                // data.podetail.RemoveAll(x => x.qty_qc_pass == 0);
                data.details.RemoveAll(x => x.no_booked != Guid.Empty ); //|| x.no_booked != null
                foreach (var item in data.details)
                {
                    
                    int cek_min = await DORepo.CekStok(kdcb,item.kode_Barang, item.Qty- item.qty_awal, DateTime.Now.ToString("yyyyMM"), trans);
                    if (cek_min < 0 )
                    {
                        stat = 1;
                        successReponse.Success = false;
                        successReponse.Result = "failed";
                        successReponse.Message = item.nama_Barang +" Stok tidak Cukup!! "+ cek_min ;
                    }
                    //await HelperRepo.SPBooked_in(item.Kd_Cabang, DateTime.Now.ToString("yyyyMM"), item.Kd_Stok, item.Kd_satuan, item.Qty * -1, trans, conn);
                }

                if (stat== 0)
                {
                    successReponse.Success = true;
                    successReponse.Result = "success";
                    successReponse.Message = "success";
                }
                

                

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

        [HttpPost("UpdateInden")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> UpdateInden([FromForm] List<SALES_BOOKED> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (var item in data)
                {
                    await DORepo.UpdateInden(item, trans);
                  //  await HelperRepo.SPBooked_in(item.Kd_Cabang, DateTime.Now.ToString("yyyyMM"), item.Kd_Stok, item.Kd_satuan, item.Qty * -1, trans, conn);
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

        [HttpPost("SaveAlokasi")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveAlokasi([FromForm] List<SALES_BOOKED> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (var item in data)
                {
                    await DORepo.UpdateInden(item, trans);
                 //   await HelperRepo.SPBooked_in(item.Kd_Cabang, DateTime.Now.ToString("yyyyMM"), item.Kd_Stok, item.Kd_satuan, item.qty_Alokasi - item.Qty, trans, conn);
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

        [HttpPost("SaveRetur")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveRetur([FromForm] SALES_RETUR data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            string blth = DateTime.Now.ToString("yyyyMM");
            string gudang_tujuan = await HelperRepo.GetGudangFromCabang(data.Kd_Cabang);
            string nosp = data.No_ref1;
            //string notran="";
            //decimal jm = data.details.Sum(x => x.qty_tarik);
            //if (jm > 0)
            //{
            //    notran = await HelperRepo.GetNoTrans("BMSJ", DateTime.Now, data.Kd_Cabang);
            //    data.Keterangan = notran;
            //}

            try
            {
                await DORepo.SaveRetur(data, trans);

                foreach (var item in data.details)
                {
                    await DORepo.SaveReturDetail(item, trans);
                  //  await INV_GUDANG_IN_Repo.SPStokIn(item.Kd_Cabang, blth, item.Kd_Stok, item.Kd_satuan, item.qty_tarik * -1, trans, conn);
                    await INV_GUDANG_IN_Repo.InsertGudangInDetil(item.Kd_Cabang, data.No_retur, data.Tipe_trans, data.No_ref1, item.No_seq, item.Kd_Stok, item.Kd_satuan, item.Last_created_by, data.Keterangan, item.Qty ?? 0, item.harga ?? 0, item.qty_tarik, gudang_tujuan, trans);
                    await DORepo.UpdateStatRetur(nosp, item.Kd_Stok, item.qty_tarik, item.Last_created_by, trans);
                }

                //System.Threading.Thread.Sleep(1360);
                await DORepo.SPFIN_AUTOMAN_JURNAL(data, trans, conn);
                await INV_GUDANG_IN_Repo.InsertGudangIn(data.Kd_Cabang, data.Tipe_trans, data.No_retur, data.No_retur, DateTime.Now, data.jml_rp_trans ?? 0,
           data.Last_Created_By, data.Keterangan, blth, data.Program_Name, data.Nama_agen, DateTime.Now, data.Last_Created_By,
           data.No_ref1, gudang_tujuan, data.Kd_Customer, trans);
                // await INV_GUDANG_IN_Repo.SPStokIn(data.Kd_Cabang, blth, data.Kd_Stok, data.Kd_satuan, Math.Abs(data.Qty), trans, conn);


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

        [HttpGet("JurnalSO")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> JurnalSO(string invNo = "", string tipetrans = "")
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {

                await DORepo.SPFIN_CATALOG_MAKEJUR(invNo, tipetrans, trans, conn);

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

        [HttpGet("JurnalNota")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> JurnalNota(string no_sp = "", string tipetrans = "")
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                await DORepo.SPFIN_INS_NOTA_JUAL_LANGSUNG2(no_sp, trans, conn);
                //await DORepo.SPFIN_CATALOG_MAKEJUR(invNo, tipetrans, trans, conn);

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

        [HttpGet("PostingSO")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> PostingSO(string no_jurnal)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                DateTime vtgl = DateTime.Now;
                string res = await DORepo.SPFIN_POSTING(no_jurnal, vtgl, trans, conn);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(res); ;

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

        [HttpGet("GetAmountSO")]
        public async Task<ActionResult<string>> GetAmountSO()
        {
            var successReponse = new Response();
            IEnumerable<SOAmountVM> ListPO = new List<SOAmountVM>();

            try
            {

                ListPO = await DORepo.GetAmountSO();

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

    }
}