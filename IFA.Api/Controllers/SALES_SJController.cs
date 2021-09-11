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
using System.Text.RegularExpressions;

namespace IFA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SALES_SJController : ControllerBase
    {
    

        [HttpGet("GetListSJK")]
        public ActionResult<string> GetListSJK(string kdcabang)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SJ> List_SJK = new List<SALES_SJ>();

            try
            {
                List_SJK = SalesSJ_Repo.GetListSJK(kdcabang);
                var ListNoSJ = List_SJK.Select(s => new {
                    s.no_sj

                }).ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListNoSJ);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetSJ")]
        public async Task<ActionResult<string>> GetSJ(string no_sj = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SJ_D> List_SJK = new List<SALES_SJ_D>();

            try
            {
                List_SJK = await SalesSJ_Repo.GetSJ(no_sj);
                //var ListNoSJ = List_SJK.Select(s => new {
                //    s.No_sp

                //}).ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(List_SJK);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }


        [HttpGet("GetMonSJ")]
        public async Task<ActionResult<string>> GetMonSJ(string no_sj, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SJ> List_SJK = new List<SALES_SJ>();

            try
            {
                List_SJK = await SalesSJ_Repo.GetMonSJ(no_sj, DateFrom, DateTo, cb);
              
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(List_SJK);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetSJPartial")]
        public async Task<ActionResult<string>> GetSJPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string kdcb)
        {
            var successReponse = new Response();
            List<SALES_SJ> ListPO = new List<SALES_SJ>();

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
                //List_SJK = await SalesSJ_Repo.GetAll_SJ(no_sj, DateFrom, DateTo, cb);
                ListPO = await SalesSJ_Repo.GetSJPartial(DateFrom, DateTo, kdcb, filterquery, sortingquery, seq);

                var query = ListPO.AsEnumerable().Skip(skip).Take(pageSize);

                successReponse.Success = true;
                successReponse.Result = SalesSJ_Repo.getCountSJ(DateFrom, DateTo, filterquery, kdcb, seq).Count().ToString();
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

        [HttpGet("GetAll_SJ")]
        public async Task<ActionResult<string>> GetAll_SJ(string no_sj, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SJ> List_SJK = new List<SALES_SJ>();

            try
            {
                List_SJK = await SalesSJ_Repo.GetAll_SJ(no_sj, DateFrom, DateTo, cb);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(List_SJK);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }


        [HttpGet("GetDtlSJ")]
        public ActionResult<string> GetDtlSJ(string no_sj, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
            {
            var successReponse = new Response();
            IEnumerable<SALES_SJ_D> ListResult = new List<SALES_SJ_D>();
            try
            {
                ListResult = SalesSJ_Repo.GetDtlSJ(no_sj, DateFrom, DateTo, cb);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListResult);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetMonSJK")]
        public async Task<ActionResult<string>> GetMonSJK(string no_sj, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SJ> List_SJK = new List<SALES_SJ>();

            try
            {
                List_SJK = await SalesSJ_Repo.GetMonSJK(no_sj, DateFrom, DateTo, cb);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(List_SJK);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetDtlSJK")]
        public ActionResult<string> GetDtlSJK(string no_sj, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SJ_D> ListResult = new List<SALES_SJ_D>();
            try
            {
                ListResult = SalesSJ_Repo.GetDtlSJK(no_sj, DateFrom, DateTo, cb);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListResult);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetMonSJ_del")]
        public async Task<ActionResult<string>> GetMonSJ_del(string no_sj, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null,string jns_sj =null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SJ> List_SJK = new List<SALES_SJ>();

            try
            {
                List_SJK = await SalesSJ_Repo.GetMonSJ_del(no_sj, DateFrom, DateTo, cb, jns_sj);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(List_SJK);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetDtlSJ_del")]
        public ActionResult<string> GetDtlSJ_del(string no_sj, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null, string jns_sj = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SJ_D> ListResult = new List<SALES_SJ_D>();
            try
            {
                ListResult = SalesSJ_Repo.GetDtlSJ_del(no_sj, DateFrom, DateTo, cb, jns_sj);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListResult);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }
        [HttpGet("GetAll_SJdtl")]
        public ActionResult<string> GetAll_SJdtl(string no_sj, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_SJ_D> ListResult = new List<SALES_SJ_D>();
            try
            {
                ListResult = SalesSJ_Repo.GetAll_SJdtl(no_sj, DateFrom, DateTo, cb);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListResult);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }


        [HttpPost("SaveSJ")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveSJ([FromForm] List<SALES_SJ> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SALES_SJ sj in data)
                {
                    await SalesSJ_Repo.SaveSJ(sj.no_krm, sj.No_sp,sj.Kd_cabang,sj.Last_created_by,sj.No_Gudang_Out,2, trans, conn);
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

        [HttpPost("SaveSJK")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveSJK([FromForm] SALES_SJ data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            string blthn = DateTime.Now.ToString("yyyyMM");
            string notran = "";
            string gudang_tujuan = await HelperRepo.GetGudangFromCabang(data.Kd_cabang);
            decimal jm = data.sjdetail.Sum(x => x.Qty_balik);
            if (jm > 0)
            {
                // notran = await HelperRepo.GetNoTrans("BMSJ", DateTime.Now, data.Kd_cabang);
               // data.Keterangan = notran;
            }
            System.Threading.Thread.Sleep(1700);
            try
            {
               await SalesSJ_Repo.SaveSJK(data, trans);
                if (data.sjdetail != null)
                {
                    foreach (SALES_SJ_D sjdetail in data.sjdetail)
                    {
                        sjdetail.no_gdin = notran;
                        await SalesSJ_Repo.SaveSJKDetail(sjdetail, trans);
                        //sudah d exec d sj gd out
                       // await GUDANG_OUTRepo.SPStokOut(sjdetail.Kd_cabang, blthn, sjdetail.Kd_stok, sjdetail.Kd_satuan, sjdetail.qty_out, trans, conn);
                        // tidak perlu expedisi await HelperRepo.SPExpdc_out(sjdetail.Kd_cabang, blthn, sjdetail.Kd_stok, sjdetail.qty_out, trans, conn);
                        //tidak perlu misal kirim 100 balek 20 ttp out 100 ntr saat kembali akan ins gudang in lg 20, total 80
                       // await SalesSJ_Repo.UpdateGudangSJ(sjdetail.Kd_cabang,data.No_Gudang_Out, sjdetail.Kd_stok,sjdetail.no_seq, sjdetail.qty_out, trans);//Update Gd
                        await SalesSJ_Repo.UpdateSODetail(sjdetail, trans);
                        //if (sjdetail.Qty_balik > 0)
                        //{
                        //    await INV_GUDANG_IN_Repo.InsertGudangSJKDetil(sjdetail, gudang_tujuan, trans);
                        //    await INV_GUDANG_IN_Repo.SPStokIn(sjdetail.Kd_cabang, blthn, sjdetail.Kd_stok, sjdetail.Kd_satuan, sjdetail.Qty_balik, trans, conn);
                        //}
                        //  INV_QCRepo.UpdateStat(podetail, trans, conn);

                    }
                    //if (jm > 0 )
                    //{
                    //    await INV_GUDANG_IN_Repo.InsertGudangSJK(data, trans);
                    //}
                }
                await SalesSJ_Repo.prodp_upd_krm_balik(data.no_dpb, data.No_sp,data.Kd_cabang, trans, conn);
              
                //await SalesSJ_Repo.CloseSp( data.sjdetail.FirstOrDefault().No_sp, trans, conn);
                await SalesSJ_Repo.UpdateSO_TERKIRIM(data, trans); //UpdateSO_TERKIRIM
                await HelperRepo.UpdateGranTotalSO(data.Kd_cabang, data.No_sp, trans);// recalc grad total
                                                                                      // await DORepo.SPFIN_INS_NOTA_JUAL_LANGSUNG2(data.No_sp, trans, conn); // SP cereate jurnal
                await DORepo.SPFIN_INS_NOTA_JUAL(data, trans, conn); // SP cereate jurnal

                // await HelperRepo.
                // string inv =  HelperRepo.GetNoInv(data.No_sp);
                //await DORepo.SPFIN_CATALOG_MAKEJUR(inv, "JPJ-KPT-01", trans, conn);
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
        [HttpPost("editSJ")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> editSJ([FromForm] SALES_SJ data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
           // string blthn = DateTime.Now.ToString("yyyyMM");
            //string notran = "";
            //string gudang_tujuan = await HelperRepo.GetGudangFromCabang(data.Kd_cabang);
            //decimal jm = data.sjdetail.Sum(x => x.Qty_balik);
            //if (jm > 0)
            //{
            //    notran = await HelperRepo.GetNoTrans("BMSJ", DateTime.Now, data.Kd_cabang);
            //    data.Keterangan = notran;
            //}
            //System.Threading.Thread.Sleep(1700);
            try
            {
                await SalesSJ_Repo.Update_SJ(data, trans);
                if (data.sjdetail != null)
                {
                    foreach (SALES_SJ_D sjdetail in data.sjdetail)
                    {
                        //sjdetail.no_gdin = notran;
                        await SalesSJ_Repo.Update_SJD(sjdetail, trans);
                        //sudah d exec d sj gd out
                        //// await GUDANG_OUTRepo.SPStokOut(sjdetail.Kd_cabang, blthn, sjdetail.Kd_stok, sjdetail.Kd_satuan, sjdetail.qty_out, trans, conn);
                        //// tidak perlu expedisi await HelperRepo.SPExpdc_out(sjdetail.Kd_cabang, blthn, sjdetail.Kd_stok, sjdetail.qty_out, trans, conn);
                        ////tidak perlu misal kirim 100 balek 20 ttp out 100 ntr saat kembali akan ins gudang in lg 20, total 80
                        //// await SalesSJ_Repo.UpdateGudangSJ(sjdetail.Kd_cabang,data.No_Gudang_Out, sjdetail.Kd_stok,sjdetail.no_seq, sjdetail.qty_out, trans);//Update Gd
                        //await SalesSJ_Repo.UpdateSODetail(sjdetail, trans);
                        //if (sjdetail.Qty_balik > 0)
                        //{
                        //    await INV_GUDANG_IN_Repo.InsertGudangSJKDetil(sjdetail, gudang_tujuan, trans);
                        //    await INV_GUDANG_IN_Repo.SPStokIn(sjdetail.Kd_cabang, blthn, sjdetail.Kd_stok, sjdetail.Kd_satuan, sjdetail.Qty_balik, trans, conn);
                        //}
                        ////  INV_QCRepo.UpdateStat(podetail, trans, conn);

                    }
                    //if (jm > 0)
                    //{
                    //    await INV_GUDANG_IN_Repo.InsertGudangSJK(data, trans);
                    //}
                }
                //await SalesSJ_Repo.prodp_upd_krm_balik(data.no_dpb, data.No_sp, data.Kd_cabang, trans, conn);

                ////await SalesSJ_Repo.CloseSp( data.sjdetail.FirstOrDefault().No_sp, trans, conn);
                //await SalesSJ_Repo.UpdateSO_TERKIRIM(data, trans); //UpdateSO_TERKIRIM
                //await HelperRepo.UpdateGranTotalSO(data.Kd_cabang, data.No_sp, trans);// recalc grad total
                //                                                                      //await DORepo.SPFIN_INS_NOTA_JUAL_LANGSUNG2(data.No_sp, trans, conn);
                //                                                                      // await HelperRepo.
                //                                                                      // string inv =  HelperRepo.GetNoInv(data.No_sp);
                //                                                                      //await DORepo.SPFIN_CATALOG_MAKEJUR(inv, "JPJ-KPT-01", trans, conn);
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

        [HttpPost("PembatalanSJK")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> PembatalanSJK([FromForm] SALES_SJ data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            string blthn = DateTime.Now.ToString("yyyyMM");
            string gudang_tujuan = await HelperRepo.GetGudangFromCabang(data.Kd_cabang);
            string notran = await HelperRepo.GetNoTrans("BMSJ", DateTime.Now, data.Kd_cabang);

            try
            {
             // if (data.Program_name == "Pembatalan SJK")
                //{ 
                if (data.sjdetail != null)
                {
                    data.Keterangan = notran;
                    foreach (SALES_SJ_D sjdetail in data.sjdetail)
                    {
                        sjdetail.no_gdin = notran;
                        if (data.Program_name == "Pembatalan SJK")
                        {
                            sjdetail.Program_name = "Pembatalan SJK";

                            await INV_GUDANG_IN_Repo.SPStokIn(sjdetail.Kd_cabang, blthn, sjdetail.Kd_stok, sjdetail.Kd_satuan, sjdetail.qty_out, trans, conn);
                            await INV_GUDANG_IN_Repo.InsertGudangSJKDetil(sjdetail, gudang_tujuan, trans);
                        }
                        else // sj qty kirim
                        {
                            await HelperRepo.SPExpdc_out(sjdetail.Kd_cabang, blthn, sjdetail.Kd_stok, sjdetail.Qty_kirim, trans, conn); // stok expedisi di keluarkan
                            await INV_GUDANG_IN_Repo.SPStokIn(sjdetail.Kd_cabang, blthn, sjdetail.Kd_stok, sjdetail.Kd_satuan, sjdetail.Qty_kirim, trans, conn);
                            await INV_GUDANG_IN_Repo.InsertGudangSJDetil(sjdetail, gudang_tujuan, trans);
                        }
                    }
                    await INV_GUDANG_IN_Repo.InsertGudangSJK(data, trans);
                }
             // }
                await SalesSJ_Repo.BatalSJK(data, trans);
                await DORepo.Delete(data.No_sp, data.Last_updated_by, trans);
                await DORepo.UpdateStatSOD(data.No_sp,data.Last_updated_by, trans);// g perlu d delete ka
                await SalesSJ_Repo.DelSPM(data, trans);
                await SalesSJ_Repo.UpdateSO_BATAL(data, trans); //UpdateSO_TERKIRIM
                await SalesSJ_Repo.UpdateSO_D_BATAL(data, trans); //UpdateSO_TERKIRIM
                //await SalesSJ_Repo.CloseSp(data.sjdetail.FirstOrDefault().No_sp, trans, conn);
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
    }
}