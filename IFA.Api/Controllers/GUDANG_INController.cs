using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Api.Utils;
using ERP.Domain.Base;
using IFA.Api.Repositories;
using IFA.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using ERP.Api;
using System.IO;
using IFA.Domain.Utils;
using System.Threading;
using IFA.Api.Utils;

namespace IFA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GUDANG_INController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet("GetQC")]
        public ActionResult<string> GetQC(string no_trans,string kd_gudang=null)
        {
            var successReponse = new Response();
            IEnumerable<INV_QC_M> ListPO = new List<INV_QC_M>();

            try
            {
                ListPO = INV_QC_MRepo.GetQC(no_trans,kd_gudang); /*--, DateFrom, DateTo, status_po);*/

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetDetailQC")]
        public async Task<ActionResult<string>> GetDetail(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_QC> ListQC_D = new List<INV_QC>();

            try
            {
                ListQC_D =await INV_QCRepo.GetDetailQC(no_po); /* , DateFrom, DateTo, status_po);*/

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListQC_D);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }
        [HttpPost("SaveTerimaBarang")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveTerimaBarang([FromForm] INV_GUDANG_IN data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            var no_po = data.no_ref;
            data.gddetail.RemoveAll(x => x.qty_in == 0);
            try
            {
                await INV_GUDANG_IN_Repo.SaveGudangIn(data, trans);
                if (data.gddetail != null)
                {
                    await INV_GUDANG_IN_D_Repo.DeleteGudangDetail(data.gddetail[0].no_trans, trans);
                    foreach (INV_GUDANG_IN_D gddetail in data.gddetail)
                    {
                        await INV_GUDANG_IN_D_Repo.SaveGudangDetail(gddetail, trans);
                        //await INV_GUDANG_IN_D_Repo.Stprc_saldo(gddetail, trans);
                        //await INV_GUDANG_IN_D_Repo.Stprc_gudangIn(gddetail, trans);
                    }
                }
                //await INV_GUDANG_IN_Repo.InsertNota_Beli(data.no_trans, trans, conn);

                Thread t = new Thread(() => InsertNotaBeliThread(data.no_trans));
                t.Start();
                await INV_GUDANG_IN_Repo.SPStatusPO(data.no_ref, trans, conn);
                await INV_GUDANG_IN_Repo.SPStatusQC(data.no_ref,data.no_qc, trans, conn);
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

       

        public void InsertNotaBeliThread(string no_trans)
        {
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);

            try
            {
                INV_GUDANG_IN_Repo.InsertNota_BeliThread(no_trans, trans, conn);
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

        [HttpPost("Pembatalan")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> Pembatalan([FromForm] INV_GUDANG_IN data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            string blthn = DateTime.Now.ToString("yyyyMM");
            string gudang_tujuan = await HelperRepo.GetGudangFromCabang(data.Kd_Cabang);
            string notran = await HelperRepo.GetNoTrans("BKR", DateTime.Now, data.Kd_Cabang);
            string noref = "";

            try
            {
                // if (data.Program_name == "Pembatalan SJK")
                //{ 
                if (data.gddetail != null)
                {
                    noref = data.no_trans;
                    data.no_ref = noref;
                    data.no_trans = notran;

                    foreach (INV_GUDANG_IN_D sjdetail in data.gddetail)
                    {
                        sjdetail.no_trans = notran;
                        sjdetail.no_qc = noref;

                        sjdetail.Program_Name = "Pembatalan Mutasi";
                        await GUDANG_OUTRepo.SPStokOut(sjdetail.Kd_Cabang, blthn, sjdetail.kd_stok, sjdetail.kd_satuan, sjdetail.qty_in, trans, conn);
                        await GUDANG_OUTRepo.InsertStokDtl_out(sjdetail, gudang_tujuan, trans);

                    }
                    await GUDANG_OUTRepo.InsertStok_out(data, trans);
                }
                // }
                //await SalesSJ_Repo.BatalSJK(data, trans);
                await INV_GUDANG_IN_Repo.UpdateGudang(data.Kd_Cabang, noref, trans);// g perlu d delete karena sudah di inser di in, jd misal out sj 10.. di batalin maka in lg 10 di kartu stok
                                                                                // await GUDANG_OUTRepo.UpdateGudangDtl(data.Kd_Cabang,data.no_trans, trans);
                                                                                //await SalesSJ_Repo.DelSPM(data, trans);
                                                                                //await SalesSJ_Repo.UpdateSO_BATAL(data, trans); //UpdateSO_TERKIRIM
                                                                                //await SalesSJ_Repo.UpdateSO_D_BATAL(data, trans); //UpdateSO_TERKIRIM
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

        [HttpGet("GetTerima")]
        public async Task<ActionResult<string>> GetTerima(string no_trans,DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null,string kd_cabang=null,string program_name=null)
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_IN> ListTerima = new List<INV_GUDANG_IN>();

            try
            {
                if(program_name == "FRM_TRMBEBAS")
                {
                    ListTerima = await INV_GUDANG_IN_Repo.GetGudangInBebas(no_trans, DateFrom, DateTo, status_po, kd_cabang);
                }
                else
                {
                    ListTerima =await INV_GUDANG_IN_Repo.GetGudangIn(no_trans, DateFrom, DateTo, status_po, kd_cabang);

                }
               

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListTerima);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }


        [HttpGet("GetTerimaPartial")]
        public async Task<ActionResult<string>> GetTerimaPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string barang)
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_IN> ListTerima = new List<INV_GUDANG_IN>();

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

                ListTerima = await INV_GUDANG_IN_Repo.GetGudangInPartial(DateFrom, DateTo, filterquery, sortingquery, barang, seq);

                var query = ListTerima.AsEnumerable().Skip(skip).Take(pageSize);


                successReponse.Success = true;
                successReponse.Result = INV_GUDANG_IN_Repo.GetCountGudang(DateFrom, DateTo, filterquery, barang, seq).Count().ToString();
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


        [HttpGet("GetDetailTerima")]
        public async Task<ActionResult<string>> GetDetailTerima(string no_trans, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_IN_D> ListTerima = new List<INV_GUDANG_IN_D>();

            try
            {
                ListTerima = await INV_GUDANG_IN_D_Repo.GetDetGudangIn(no_trans, DateFrom, DateTo,status_po);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListTerima);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetDetailTerimaView")]
        public async Task<ActionResult<string>> GetDetailTerimaView(string no_trans, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_IN_D> ListTerima = new List<INV_GUDANG_IN_D>();

            try
            {
                ListTerima = await INV_GUDANG_IN_D_Repo.GetDetGudangInView(no_trans, DateFrom, DateTo, status_po);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListTerima);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }


        [HttpGet("GetstokGudang")]
        public async Task<ActionResult<string>> GetstokGudang(string Kode_Barang, string blnthn,string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<V_MonStok> ListTerima = new List<V_MonStok>();

            try
            {
                ListTerima = await INV_GUDANG_IN_Repo.GetMonStok(Kode_Barang, blnthn, cb);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListTerima);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetDetailstokGudang")]
        public async Task<ActionResult<string>> GetDetailstokGudang(string Kode_Barang,string blnthn)
        {
            var successReponse = new Response();
            IEnumerable<V_MonStokDetail> ListDetailstokGudang = new List<V_MonStokDetail>();

            try
            {
                if(blnthn == null || blnthn == string.Empty)
                {
                    blnthn += DateTime.Now.Year.ToString();

                    if (DateTime.Now.Month.ToString().Count() == 1)
                    {
                        blnthn += "0" + DateTime.Now.Month.ToString();
                    }
                    else
                    {
                        blnthn += DateTime.Now.Month.ToString();
                    }
                }
                ListDetailstokGudang = await INV_GUDANG_IN_Repo.GetMonStokDetail(Kode_Barang,blnthn);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListDetailstokGudang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetStokAllGudang")]
        public async Task<ActionResult<string>> GetStokAllGudang(string Kode_Barang, string blnthn)
        {
            var successReponse = new Response();
            IEnumerable<StokAllGudang> ListDetailstokGudang = new List<StokAllGudang>();

            try
            {
                if (blnthn == null || blnthn == string.Empty)
                {
                    blnthn += DateTime.Now.Year.ToString();

                    if (DateTime.Now.Month.ToString().Count() == 1)
                    {
                        blnthn += "0" + DateTime.Now.Month.ToString();
                    }
                }
                ListDetailstokGudang = await INV_GUDANG_IN_Repo.GetStokAllGudang(Kode_Barang, blnthn);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListDetailstokGudang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("UpdateQC")]
        public async Task<ActionResult<Response>> UpdateQC([FromForm] INV_QC_M data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                await INV_GUDANG_IN_Repo.UpdateQC(data, trans);
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
            //decimal? GrandTotal = 0;
            //decimal? TesGrandTotal = 0;
            //decimal? PPN = 0;


            try
            {
                //if (data.flag_ppn == "Y")
                //{
                //    TesGrandTotal = data.detailPO.Sum(x => x.total);
                //    PPN = (data.detailPO.Sum(x => x.total) * 0.01m);
                //    GrandTotal = data.detailPO.Sum(x => x.total) - (data.detailPO.Sum(x => x.total) * 0.01m);
                //}
                //else
                //{
                //  //  System.Threading.Thread.Sleep(300);
                //    GrandTotal = data.detailPO.Sum(x => x.total);
                //}

                //data.jml_rp_trans = GrandTotal;

                //await INV_GUDANG_IN_Repo.UpdateGrandTotalPO(data, trans);
                foreach (PURC_PO_D item in data.detailPO)
                {
                    await INV_GUDANG_IN_Repo.UpdatePODetail(item, trans);
                    //await INV_GUDANG_IN_Repo.UpdateQCDetail(item, trans);
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



        [HttpGet("GetListMtsOut")]
        public async Task<ActionResult<string>> GetListMtsOut()
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_OUT> ListNotrans = new List<INV_GUDANG_OUT>();

            try
            {
                ListNotrans = await INV_GUDANG_IN_Repo.GetMtsOutCbo();
                var ListNoPO = ListNotrans.Select(s => new {s.no_trans }).ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListNoPO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetMts")]
        public async Task<ActionResult<string>> GetMts(string no_trans = null,string kdcabang= null)
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_OUT_D> ListMts = new List<INV_GUDANG_OUT_D>();

            try
            {
                ListMts = await GUDANG_OUTRepo.getSavedGudang(no_trans, kdcabang);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListMts);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }
        [HttpGet("GetdataMts")]
        public async Task<ActionResult<string>> GetdataMts(string no_trans = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_IN_D> ListMts = new List<INV_GUDANG_IN_D>();

            try
            {
                ListMts =await INV_GUDANG_IN_Repo.getGudangDetail(no_trans);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListMts);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("InsertNota_Beli")]
        public async Task<ActionResult<Response>> InsertNota_Beli([FromForm] string no_trans)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                await INV_GUDANG_IN_Repo.InsertNota_Beli(no_trans, trans, conn);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = "success";
                DataAccess.CloseTransaction(conn, trans, true);
                //all save success -> commit set true

            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;
                DataAccess.CloseTransaction(conn, trans, false);
                //ada fail -> rollback
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

        [HttpPost("SP_Status_PO")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SP_Status_PO([FromForm] INV_GUDANG_IN data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                await INV_GUDANG_IN_Repo.SPStatusPO(data.no_ref, trans, conn);

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

        [HttpPost("SaveMTS")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveMTS([FromForm] INV_GUDANG_IN data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                await INV_GUDANG_IN_Repo.SaveGudangIn(data, trans);
                if (data.gddetail != null)
                {
                    foreach (INV_GUDANG_IN_D gddetail in data.gddetail)
                    {
                        await INV_GUDANG_IN_D_Repo.SaveGudangDetail(gddetail, trans);
                        await INV_GUDANG_IN_Repo.SPStokIn(gddetail.Kd_Cabang, gddetail.blthn, gddetail.kd_stok, gddetail.kd_satuan, Math.Abs(gddetail.qty_in), trans, conn);
                        //move from exp -> gudang utama
                       await INV_GUDANG_IN_Repo.SPGudangIn(gddetail.Kd_Cabang, gddetail.blthn, gddetail.kd_stok, gddetail.kd_satuan, gddetail.qty_in, gddetail.gudang_tujuan, trans, conn);
                        //await GUDANG_OUTRepo.SPGudangOut(gddetail.Kd_Cabang, gddetail.blthn, gddetail.kd_stok, gddetail.kd_satuan, gddetail.qty_in, gddetail.gudang_asal, trans, conn);
                    }
                }
               await GUDANG_OUTRepo.UpdateGudang(data.Kd_Cabang, data.no_ref, trans);
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

        [HttpPost("UpdateMTS")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> UpdateMTS([FromForm] INV_GUDANG_IN data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                await INV_GUDANG_IN_Repo.UpdateGudangIn(data, trans);
                if (data.gddetail != null)
                {
                    await INV_GUDANG_IN_D_Repo.DeleteGudangDetail(data.gddetail[0].no_trans, trans);
                    foreach (INV_GUDANG_IN_D gddetail in data.gddetail)
                    {
                        await INV_GUDANG_IN_D_Repo.SaveGudangDetail(gddetail, trans);
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

        [HttpGet("GetMonitoringMts")]
        public async Task<ActionResult<string>> GetMonitoringMts(string kdcb, string no_trans = null, string kd_stok = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_IN_D> ListQC = new List<INV_GUDANG_IN_D>();

            try
            {
                //ListQC = INV_QCRepo.GetMonitoringQC(kd_stok,DateFrom, DateTo);
                ListQC = await INV_GUDANG_IN_D_Repo.GetMonitoringMts(kdcb,no_trans,kd_stok, DateFrom, DateTo);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListQC);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetMTS_In")]
        public async Task<ActionResult<string>> GetMTS_In(string kdcb, string no_trans = null, DateTime? DateFrom = null, DateTime? DateTo = null,string kd_stok = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_IN> ListH = new List<INV_GUDANG_IN>();

            try
            {
                //ListQC = INV_QCRepo.GetMonitoringQC(kd_stok,DateFrom, DateTo);
                ListH = await INV_GUDANG_IN_Repo.GetMTS_In(kdcb , no_trans, DateFrom, DateTo, kd_stok);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListH);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }
        [HttpGet("GetPrintTerima")]
        public async Task<ActionResult<string>> GetPrintTerima(string no_trans = null)
        {
            var successReponse = new Response();
            PrintTerima ListTerima = new PrintTerima();

            try
            {
                ListTerima = await INV_GUDANG_IN_Repo.GetPrintTerima(no_trans);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListTerima);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("getGudangDetailByNoPO")]
        public async Task<ActionResult<string>> getGudangDetailByNoPO(string no_po)
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_IN_D> ListTerima = new List<INV_GUDANG_IN_D>();

            try
            {
                ListTerima = await INV_GUDANG_IN_Repo.getGudangDetailByNoPO(no_po);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListTerima);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetstokGudangPartial")]
        public async Task<ActionResult<string>> GetstokGudangPartial(string blnthn, string cb, string sorting, string filter, int skip, int take, int pageSize, int page, string barang)
        {
            var successReponse = new Response();
            IEnumerable<V_MonStok> ListTerima = new List<V_MonStok>();

            try
            {
                var filterquery = "";
                var sortingquery = "";
                if (filter != null && filter != "" && filter != "null")
                {
                    var filtermodel = JsonConvert.DeserializeObject<KendoGridParameterParser.Models.Filter>(filter);

                    filterquery = FilterExtension.RecursiveFilterExpressionBuilder(filtermodel);
                    if (barang != null && barang != "")
                    {
                        filterquery += " AND Nama_Barang LIKE '%" + barang + "%' ";
                    }
                }
                else if(barang != null && barang != "")
                {
                    filterquery = " Nama_Barang LIKE '%" + barang + "%' ";
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

                ListTerima = await INV_GUDANG_IN_Repo.GetMonStokPartial(blnthn, cb, filterquery, sortingquery, seq);
                var query = ListTerima.AsEnumerable().Skip(skip).Take(pageSize);
                successReponse.Success = true;
                successReponse.Result = INV_GUDANG_IN_Repo.GetCountMonStokPartial(blnthn, cb, filterquery, sortingquery, seq).Count().ToString();
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

        [HttpGet("GetMts_INPartial")]
        public async Task<ActionResult<string>> GetMts_INPartial(string kdcb, string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string barang)
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_IN> ListH = new List<INV_GUDANG_IN>();

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
                ListH = await INV_GUDANG_IN_Repo.GetMTS_InPartial(kdcb,DateFrom, DateTo, filterquery, sortingquery, barang, seq);

                var query = ListH.AsEnumerable().Skip(skip).Take(pageSize);

                successReponse.Success = true;
                successReponse.Result = INV_GUDANG_IN_Repo.GetCountMTS_InPartial(kdcb,DateFrom, DateTo, filterquery, barang, seq).Count().ToString();
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
    }
}