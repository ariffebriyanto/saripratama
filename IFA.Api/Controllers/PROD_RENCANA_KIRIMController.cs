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
using IFA.Api.Utils;
using System.Diagnostics;
using ERP.Api;
using System.IO;
using IFA.Domain.Utils;

namespace IFA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PROD_RENCANA_KIRIMController : ControllerBase
    {
        [HttpGet("GetRcnKrm")]
        public async Task<ActionResult<string>> GetRcnKrm(string kd_cabang, string kode_sales, string no_sp = null)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            IEnumerable<PRODV_MON_SO> ListMonSO = new List<PRODV_MON_SO>();

            try
            {
                ListMonSO = await PROD_rcn_krmRepo.Getrcnkirim(kd_cabang, kode_sales, no_sp);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListMonSO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("SaveRencanaKirim")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveRencanaKirim([FromForm] PROD_rcn_krm data)
        {
            var ListSO = data.rcnkrmDetail.GroupBy(item => item.no_sp).Select(group => group.First()).ToList();
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
                        //   await PROD_rcn_krm_D_Repo.UpdateSODetail(rcnkrmDetail, trans);
                        string no_sp = rcnkrmDetail.no_sp;
                        await PROD_rcn_krmRepo.UpdateSOKirim(no_sp, trans, conn);


                    }
                }

                //sebelumnya di detilnya update dulu di each ya cong, y

                //foreach (var item in ListSO)
                //{

                //    await HelperRepo.UpdateGranTotalSO(item.kd_cabang,item.no_sp, trans);
                //}


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

        //Update Status DO saat rencana kirim

        [HttpPost("UpdateSOStatus")]
        public async Task<ActionResult<Response>> UpdateSOStatus([FromForm] List<PROD_rcn_krm_D> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (PROD_rcn_krm_D item in data)
                {
                    await PROD_rcn_krmRepo.UpdateSOStatus(item, trans);
                    await PROD_rcn_krmRepo.UpdateSOStatusDetail(item, trans);
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

        // Pembatalan Rencana Kirim
        [HttpPost("Deletercnkirim")]
        public async Task<ActionResult<Response>> Deletercnkirim([FromForm] PROD_rcn_krm data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {

                await PROD_rcn_krmRepo.Deletercnkirim(data, trans);
                await PROD_rcn_krmRepo.DeletercnkirimDetail(data, trans);


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

        //Pending

        //[HttpPost("UpdateSODStatus")]
        //public ActionResult<Response> UpdateSODStatus([FromForm] PRODV_MON_SO data)
        //{
        //    var successReponse = new Response();
        //    var conn = DataAccess.GetConnection();
        //    var trans = DataAccess.OpenTransaction(conn);
        //    try
        //    {
        //        PROD_rcn_krmRepo.UpdateSODStatus(data ,trans);
        //        successReponse.Success = true;
        //        successReponse.Result = "success";
        //        successReponse.Message = "success";

        //        //all save success -> commit set true
        //        DataAccess.CloseTransaction(conn, trans, true);
        //    }
        //    catch (Exception e)
        //    {
        //        successReponse.Success = false;
        //        successReponse.Result = "failed";
        //        successReponse.Message = e.Message;

        //        //ada fail -> rollback
        //        DataAccess.CloseTransaction(conn, trans, false);
        //    }
        //    finally
        //    {
        //        //close transaction -> commit success or fail
        //        DataAccess.DisposeConnectionAndTransaction(conn, trans);
        //    }

        //    return successReponse;
        //}

        [HttpGet("GetMonRcnKirimPartial")]
        public async Task<ActionResult<string>> GetMonRcnKirimPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime? DateFrom=null, DateTime? DateTo=null, string no_sp=null, string kd_customer=null)
        {
            var successReponse = new Response();
            List<PROD_rcn_krm> ListPO = new List<PROD_rcn_krm>();

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
                ListPO = await PROD_rcn_krmRepo.GetMonRcnKirimPartial(DateFrom, DateTo, filterquery, sortingquery, no_sp,kd_customer, seq);

                var query = ListPO.AsEnumerable().Skip(skip).Take(pageSize);

                successReponse.Success = true;
                successReponse.Result = PROD_rcn_krmRepo.getCountMonRcnKirim(DateFrom, DateTo, filterquery, no_sp, kd_customer, seq).Result;
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

        [HttpGet("GetMonRcnKirim")]
        public ActionResult<string> GetMonRcnKirim(string no_sp = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var successReponse = new Response();
            IEnumerable<PROD_rcn_krm> ListRcnKirim = new List<PROD_rcn_krm>();

            try
            {
                ListRcnKirim = PROD_rcn_krmRepo.GetMonRcnKirim(no_sp, DateFrom, DateTo);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListRcnKirim);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetDetRcnKirim")]
        public ActionResult<string> GetDetRcnKirim(string no_trans=null,DateTime? DateFrom = null, DateTime? DateTo = null, string kd_customer=null)
        {
            var successReponse = new Response();
            IEnumerable<PROD_rcn_krm_D> ListRcnKirim = new List<PROD_rcn_krm_D>();

            try
            {
                ListRcnKirim = PROD_rcn_krmRepo.GetDetRcnKirim(no_trans, DateFrom, DateTo,kd_customer);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListRcnKirim);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetrcnkirimDetail")]
        public ActionResult<string> GetrcnkirimDetail(string no_trans)
        {
            var successReponse = new Response();
            IEnumerable<PRODV_MON_SO> ListRcnKirim = new List<PRODV_MON_SO>();

            try
            {
                ListRcnKirim = PROD_rcn_krm_D_Repo.GetrcnkirimDetail(no_trans);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListRcnKirim);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetNorcnkirim")]
        public ActionResult<string> GetNorcnkirim(string kd_cabang)
        {
            var successReponse = new Response();
            IEnumerable<PROD_rcn_krm> ListNotrans = new List<PROD_rcn_krm>();

            try
            {
                ListNotrans = PROD_rcn_krmRepo.GetNorcnkirimCbo(kd_cabang);
                var ListNoPO = ListNotrans.Select(s => new
                {
                    s.no_trans
                }).ToList();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListNotrans);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("SaveSiapKirim")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveSiapKirim([FromForm] INV_GUDANG_OUT data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);

            var cekGd = await GUDANG_OUTRepo.CekGudangOut(data.no_ref, trans);
            if (cekGd == 0)
            {


                try
                {
                    await GUDANG_OUTRepo.SaveGudangOut(data, trans);
                    if (data.detail != null)
                    {
                        await GUDANG_OUTRepo.DeleteSiapKirimDetail(data.detail[0].no_trans, trans);


                        foreach (INV_GUDANG_OUT_D detail in data.detail)
                        {
                            await GUDANG_OUTRepo.SaveDetail(detail, trans);
                            await HelperRepo.SPBooked_out(detail.Kd_Cabang, detail.blthn, detail.kd_stok, detail.kd_satuan, detail.qty_out, trans, conn);
                            await GUDANG_OUTRepo.SPStokOut(detail.Kd_Cabang, detail.blthn, detail.kd_stok, detail.kd_satuan, detail.qty_out, trans, conn);
                            await PROD_rcn_krmRepo.UpdateSODStatus(detail, trans);
                            await GUDANG_OUTRepo.SPGudangKeluarBebas(detail, trans, conn);
                        }
                    }
                    if (data.detail != null)
                    {
                        var so_sj = data.detail.GroupBy(x => x.no_sp).Select(x => x);
                        string cek = "";
                        int jml = 0;
                        foreach (var group in so_sj)
                        {

                            var groupKey = group.Key;
                            foreach (var groupedItem in group)
                            {

                                if (groupKey != cek)
                                {
                                    jml += 1;
                                    await SalesSJ_Repo.SaveSJ(groupedItem.no_ref, groupedItem.no_sp, groupedItem.Kd_Cabang, groupedItem.Last_Created_By, groupedItem.no_trans, 2, trans, conn);
                                    await SalesSJ_Repo.UpdateSO_KIRIM(groupedItem.no_sp, groupedItem.Kd_Cabang, groupedItem.Last_Updated_By, trans); //UpdateSO_TERKIRIM
                                    cek = groupedItem.no_sp;
                                }

                            }

                        }
                    }
                    //ssdsd
                    string no_ref = data.no_ref;
                    await PROD_rcn_krmRepo.InvStat(no_ref, trans);

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
            }
            else
            {

                successReponse.Success = false;
                successReponse.Result = "No Rcn Sudah di Proses";
                successReponse.Message = "No Rcn Sudah di Proses";
            }
            return successReponse;
        }

        [HttpGet("GetSiapKirim")]
        public ActionResult<string> GetSiapKirim(string no_trans)
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_OUT> ListRcnKirim = new List<INV_GUDANG_OUT>();

            try
            {
                ListRcnKirim = PROD_rcn_krmRepo.GetMonSiapKirim(no_trans);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListRcnKirim);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetMonSiapKirim")]
        public ActionResult<string> GetMonSiapKirim(string no_trans)
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_OUT> ListRcnKirim = new List<INV_GUDANG_OUT>();

            try
            {
                ListRcnKirim = PROD_rcn_krmRepo.GetMonSiapKirim(no_trans).OrderByDescending(x => x.tgl_trans).ThenByDescending(X => X.no_trans);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListRcnKirim);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetMonSiapKirimD")]
        public ActionResult<string> GetMonSiapKirimD(string no_trans)
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_OUT_D> ListMonSiapKirimD = new List<INV_GUDANG_OUT_D>();

            try
            {
                ListMonSiapKirimD = PROD_rcn_krmRepo.GetMonSiapKirimD(no_trans);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListMonSiapKirimD);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetPrintSiapKirim")]
        public ActionResult<string> GetPrintSiapKirim(string no_trans = null)
        {
            var successReponse = new Response();
            INV_GUDANG_OUT ListTerima = new INV_GUDANG_OUT();

            try
            {
                ListTerima = PROD_rcn_krmRepo.GetPrintSiapKirim(no_trans);

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

        [HttpGet("GetCetakDPB")]
        public async Task<ActionResult<string>> GetCetakDPB(DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var successReponse = new Response();
            List<CETAK_DPB> model = new List<CETAK_DPB>();

            try
            {
                model = await PROD_rcn_krmRepo.GetCetakDPB(DateFrom, DateTo);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(model);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("SaveCetakDPB")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveCetakDPB([FromForm] inputcetakDPB data)
        {
            var successReponse = new Response();
            List<CETAK_DPB> model = new List<CETAK_DPB>();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                string dateInspeksi = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                foreach (var item in data.details)
                {
                    await DORepo.UpdateCetakDPB(item, dateInspeksi, trans);

                }

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = dateInspeksi;
                DataAccess.CloseTransaction(conn, trans, true);
                //successReponse.Message = "04/10/2018 9:48:44";
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;
                DataAccess.CloseTransaction(conn, trans, false);

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