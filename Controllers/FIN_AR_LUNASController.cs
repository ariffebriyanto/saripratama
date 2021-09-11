using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ERP.Api;
using ERP.Api.Utils;
using ERP.Domain.Base;
using IFA.Api.Repositories;
using IFA.Domain.Models;
using IFA.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IFA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FIN_AR_LUNASController : ControllerBase
    {
        [HttpGet("GetInvoice")]
       
        public async Task<ActionResult<string>> GetInvoice(string cb = null, string kdcust = null,string kdvaluta=null )
        {
            var successReponse = new Response();
            IEnumerable<FIN_NOTA> ListHeader = new List<FIN_NOTA>();
            //IEnumerable<SALES_SO_DVM> ListPOD = new List<SALES_SO_DVM>();

            try
            {
                ListHeader = await FIN_LunasRepo.GetInvoice(cb, kdcust, kdvaluta);
             
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListHeader);
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

        [HttpGet("GetOverdue")]

        public async Task<ActionResult<string>> GetOverdue(string kd_cust = null)
        {
            var successReponse = new Response();
            IEnumerable<v_masalah> ListHeader = new List<v_masalah>();
            //IEnumerable<SALES_SO_DVM> ListPOD = new List<SALES_SO_DVM>();

            try
            {
                ListHeader = await FIN_LunasRepo.GetOverdue(kd_cust);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListHeader);
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

        [HttpPost("SaveNota")]
        //[RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveNota([FromForm] FIN_NOTA_LUNAS data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            var no_trans = data.no_trans;
            try
            {
                await FIN_LunasRepo.Save(data, trans);
                if (data.detail != null)
                {
                    await FIN_LunasRepo.DelNotaDetail(data.no_trans, trans);
                    foreach (FIN_NOTA_LUNAS_D gddetail in data.detail)
                    {
                        gddetail.prev_no_inv = gddetail.prev_no_inv;
                        await FIN_LunasRepo.SaveDetail(gddetail, trans);


                        // await INV_GUDANG_IN_Repo.SPStokIn(gddetail.Kd_Cabang, gddetail.blthn, gddetail.kd_stok, gddetail.kd_satuan, Math.Abs(gddetail.qty_in), trans, conn);
                        //move from exp -> gudang utama
                        //INV_GUDANG_IN_Repo.SPGudangIn(gddetail.Kd_Cabang, gddetail.blthn, gddetail.kd_stok, gddetail.kd_satuan, gddetail.qty_in, gddetail.gudang_tujuan, trans, conn);
                        //await GUDANG_OUTRepo.SPGudangOut(gddetail.Kd_Cabang, gddetail.blthn, gddetail.kd_stok, gddetail.kd_satuan, gddetail.qty_in, gddetail.gudang_asal, trans, conn);
                    }
                    if (data.giro != null)
                    {
                        foreach (FIN_GIRO getgiro in data.giro)
                        {
                            await FIN_LunasRepo.SaveGiro(getgiro, no_trans, trans);



                            // await INV_GUDANG_IN_Repo.SPStokIn(gddetail.Kd_Cabang, gddetail.blthn, gddetail.kd_stok, gddetail.kd_satuan, Math.Abs(gddetail.qty_in), trans, conn);
                            //move from exp -> gudang utama
                            //INV_GUDANG_IN_Repo.SPGudangIn(gddetail.Kd_Cabang, gddetail.blthn, gddetail.kd_stok, gddetail.kd_satuan, gddetail.qty_in, gddetail.gudang_tujuan, trans, conn);
                            //await GUDANG_OUTRepo.SPGudangOut(gddetail.Kd_Cabang, gddetail.blthn, gddetail.kd_stok, gddetail.kd_satuan, gddetail.qty_in, gddetail.gudang_asal, trans, conn);
                        }
                    }
                }
                //await GUDANG_OUTRepo.UpdateGudang(data.Kd_Cabang, data.no_ref, trans);
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

        [HttpGet("GetNota")]
        public async Task<ActionResult<string>> GetNota(string id = null, DateTime? dt1 = null, DateTime? dt2 = null, string stat = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<FIN_NOTA_LUNAS> ListData = new List<FIN_NOTA_LUNAS>();
            IEnumerable<FIN_NOTA_LUNAS_D> ListDetail = new List<FIN_NOTA_LUNAS_D>();

            try
            {
                if (id != null)
                {
                    ListData = await FIN_LunasRepo.GetNota(id, dt1, dt2, stat, barang, cb);
                    ListDetail = await FIN_LunasRepo.GetNotaD(id, dt1, dt2, stat, barang, cb);

                    if (ListDetail != null || ListDetail.Count() != 0)
                    {
                        ListData.FirstOrDefault().detail = new List<FIN_NOTA_LUNAS_D>();

                        foreach (FIN_NOTA_LUNAS_D dtl in ListDetail)
                        {
                            ListData.FirstOrDefault().detail.Add(dtl);
                        }
                    }

                }

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);
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

        [HttpGet("GetMonNota")]
        public async Task<ActionResult<string>> GetMonNota(string id = null, DateTime? dt1 = null, DateTime? dt2 = null, string stat = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<FIN_NOTA_LUNAS> ListData = new List<FIN_NOTA_LUNAS>();
           // IEnumerable<FIN_NOTA_LUNAS_D> ListDetail = new List<FIN_NOTA_LUNAS_D>();

            try
            {
                if (id != null)
                {
                    ListData = await FIN_LunasRepo.GetNota(id, dt1, dt2, stat, barang, cb);
                    //ListDetail = await FIN_LunasRepo.GetNotaD(id, dt1, dt2, stat, barang, cb);

                    //if (ListDetail != null || ListDetail.Count() != 0)
                    //{
                    //    ListData.FirstOrDefault().detail = new List<FIN_NOTA_LUNAS_D>();

                    //    foreach (FIN_NOTA_LUNAS_D dtl in ListDetail)
                    //    {
                    //        ListData.FirstOrDefault().detail.Add(dtl);
                    //    }
                    //}

                }

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);
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
        [HttpGet("GetMonNotaD")]
        public async Task<ActionResult<string>> GetMonNotaD(string id = null, DateTime? dt1 = null, DateTime? dt2 = null, string stat = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<FIN_NOTA_LUNAS_D> ListDTL = new List<FIN_NOTA_LUNAS_D>();

            try
            {

                ListDTL = await FIN_LunasRepo.GetNotaD(id, dt1, dt2, stat, barang, cb);

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

    }
}