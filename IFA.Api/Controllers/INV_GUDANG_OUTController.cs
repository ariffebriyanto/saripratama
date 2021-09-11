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

namespace IFA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class INV_GUDANG_OUTController : ControllerBase
    {
        [HttpGet("GetGudangOut")]
        public async Task<ActionResult<string>> GetGudangOut(string no_trans = null, string tipe_trans = null, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
        {
            var successReponse = new Response();    
            IEnumerable<INV_GUDANG_OUT> ListOut = new List<INV_GUDANG_OUT>();
            IEnumerable<INV_GUDANG_OUT_D> ListDTL = new List<INV_GUDANG_OUT_D>();
            try
            {
                if (no_trans != null)
                {
                    ListOut = await GUDANG_OUTRepo.getGudang(no_trans);
                    ListDTL = await GUDANG_OUTRepo.getGudangDetail(no_trans);

                    if (ListDTL != null)
                    {
                        ListOut.FirstOrDefault().detail = new List<INV_GUDANG_OUT_D>();

                        foreach (INV_GUDANG_OUT_D detail in ListDTL)
                        {
                            ListOut.FirstOrDefault().detail.Add(detail);

                        }
                    }

                }
                if (no_trans == null && tipe_trans != null)
                {
                    ListOut = await GUDANG_OUTRepo.getMonMutasi(no_trans,tipe_trans, DateFrom, DateTo, cb);
                }

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListOut);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetGudangOutD")]
        public async Task<ActionResult<string>> GetGudangOutD(string tipe_trans = null, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_GUDANG_OUT_D> ListDTL = new List<INV_GUDANG_OUT_D>();
            try
            {
                 ListDTL = await GUDANG_OUTRepo.getGudangDetailD(tipe_trans, DateFrom, DateTo, cb);
                
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListDTL);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }
        [HttpPost("SaveGUDANG_OUT")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveGUDANG_OUT([FromForm] INV_GUDANG_OUT data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
          //  data.gudang_asal= await HelperRepo.GetGudangFromCabang(data.Kd_Cabang);
            try
            {
               await GUDANG_OUTRepo.SaveGudangOut(data, trans);
                if (data.detail != null)
                {
                   await GUDANG_OUTRepo.DeleteDetail(data.detail[0].no_trans, trans);
                    foreach (INV_GUDANG_OUT_D detail in data.detail)
                    {
                       await GUDANG_OUTRepo.SaveDetail(detail, trans);
                       await GUDANG_OUTRepo.SPGudangKeluarBebas(detail, trans, conn);
                       await GUDANG_OUTRepo.SPStokKeluarBebas(detail, trans, conn);
                    }
                }

                await GUDANG_OUTRepo.SPJurnal(data, trans, conn);

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
        [HttpGet("GetDsSiapKirim")]
        public async Task<ActionResult<string>> GetDsSiapKirim(string no_trans = null, string kd_cabang = null, string blthn = null)
        {
            var successReponse = new Response();
            IEnumerable<PROD_rcn_krm_D> ListTerima = new List<PROD_rcn_krm_D>();

            try
            {
                ListTerima = await GUDANG_OUTRepo.GetDsSiapKirim(no_trans, kd_cabang, blthn);
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

        [HttpPost("Pembatalan")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> Pembatalan([FromForm] INV_GUDANG_OUT data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            string blthn = DateTime.Now.ToString("yyyyMM");
            string gudang_tujuan = await HelperRepo.GetGudangFromCabang(data.Kd_Cabang);
            string notran = await HelperRepo.GetNoTrans("BMSJ", DateTime.Now, data.Kd_Cabang);
            string noref = "";

            try
            {
                // if (data.Program_name == "Pembatalan SJK")
                //{ 
                if (data.detail != null)
                {
                    noref = data.no_trans;
                    data.no_ref = noref;
                    data.no_trans = notran;

                    foreach (INV_GUDANG_OUT_D sjdetail in data.detail)
                    {
                        sjdetail.no_trans = notran;
                        sjdetail.no_ref = noref;

                            sjdetail.Program_Name = "Pembatalan Mutasi Out";
                            await INV_GUDANG_IN_Repo.SPStokIn(sjdetail.Kd_Cabang, blthn, sjdetail.kd_stok, sjdetail.kd_satuan, sjdetail.qty_out, trans, conn);
                            await INV_GUDANG_IN_Repo.InsertStokDtl_in(sjdetail, gudang_tujuan, trans);
                       
                    }
                    await INV_GUDANG_IN_Repo.InsertStok_In(data, trans);
                }
                // }
                //await SalesSJ_Repo.BatalSJK(data, trans);
                await GUDANG_OUTRepo.DELGudang(data.Kd_Cabang, noref,data.Last_Updated_By, trans);// g perlu d delete karena sudah di inser di in, jd misal out sj 10.. di batalin maka in lg 10 di kartu stok
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
    }
}