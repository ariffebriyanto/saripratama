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
using IFA.Domain.Helpers;
using IFA.Domain.Models;
using IFA.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IFA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KasirController : ControllerBase
    {
        [HttpGet("GetKartu")]
        public async Task<ActionResult<string>> GetKartu()
        {
            IEnumerable<SIF_Pegawai> ListData = new List<SIF_Pegawai>();
            var successReponse = new Response();


            try
            {
                ListData = await KasirRepo.GetKartu();
                var SelectList = ListData.Select(s => new {
                    s.stat,
                    s.nama,
                    s.kode
                }).ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(SelectList);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetRekKas")]
        public async Task<ActionResult<string>> GetRekKas()
        {
            IEnumerable<SIF_buku_besar> ListData = new List<SIF_buku_besar>();
            var successReponse = new Response();


            try
            {
                ListData = await HelperRepo.GetRekKas();
                var SelectList = ListData.Select(s => new {
                    s.kd_buku_besar,
                    s.nm_buku_besar
                   
                }).ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(SelectList);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetRekGL")]
        public async Task<ActionResult<string>> GetRekGL()
        {
            IEnumerable<SIF_buku_besar> ListData = new List<SIF_buku_besar>();
            var successReponse = new Response();


            try
            {
                ListData = await HelperRepo.GetRekGL();
                var SelectList = ListData.Select(s => new {
                    s.kd_buku_besar,
                    s.nm_buku_besar

                }).ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(SelectList);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }
        [HttpGet("GetRekBP")]
        public async Task<ActionResult<string>> GetRekBP()
        {
            IEnumerable<SIF_buku_pusat> ListData = new List<SIF_buku_pusat>();
            var successReponse = new Response();


            try
            {
                ListData = await HelperRepo.GetRekBP();
                var SelectList = ListData.Select(s => new {
                    s.kd_buku_pusat,
                    s.nm_buku_pusat

                }).ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(SelectList);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("SaveKasBon")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveKasBon([FromForm] FIN_KAS_BON_H data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                await KasirRepo.SaveKasBon(data, trans);
                if (data.detail != null)
                {
                    await KasirRepo.DelKasBonDetail(data.nomor, trans);
                    foreach (FIN_KAS_BON gddetail in data.detail)
                    {
                        await KasirRepo.SaveKasBonDetail(gddetail, trans);
                        //await INV_GUDANG_IN_Repo.SPStokIn(gddetail.Kd_Cabang, gddetail.blthn, gddetail.kd_stok, gddetail.kd_satuan, Math.Abs(gddetail.qty_in), trans, conn);
                        //move from exp -> gudang utama
                        //INV_GUDANG_IN_Repo.SPGudangIn(gddetail.Kd_Cabang, gddetail.blthn, gddetail.kd_stok, gddetail.kd_satuan, gddetail.qty_in, gddetail.gudang_tujuan, trans, conn);
                        //await GUDANG_OUTRepo.SPGudangOut(gddetail.Kd_Cabang, gddetail.blthn, gddetail.kd_stok, gddetail.kd_satuan, gddetail.qty_in, gddetail.gudang_asal, trans, conn);
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

        [HttpGet("GetKasBon")]
        public async Task<ActionResult<string>> GetKasBon(string id = null, DateTime? DateFrom = null, DateTime? DateTo = null, string stat = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<FIN_KAS_BON_H> ListData = new List<FIN_KAS_BON_H>();
            IEnumerable<FIN_KAS_BON> ListDetail = new List<FIN_KAS_BON>();

            try
            {
                if (id != null)
                {
                    ListData = await KasirRepo.GetKasBon(id, DateFrom, DateTo, stat, barang, cb);
                    ListDetail = await KasirRepo.GetKasBonD(id, DateFrom, DateTo, stat, barang, cb);

                    if (ListDetail != null || ListDetail.Count() != 0)
                    {
                        ListData.FirstOrDefault().detail = new List<FIN_KAS_BON>();

                        foreach (FIN_KAS_BON dtl in ListDetail)
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

        [HttpGet("GetMonKasBon")]
        public async Task<ActionResult<string>> GetMonKasBon(string id = null, DateTime? DateFrom = null, DateTime? DateTo = null, string stat = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<FIN_KAS_BON_H> ListData = new List<FIN_KAS_BON_H>();
            //IEnumerable<FIN_KAS_BON> ListDetail = new List<FIN_KAS_BON>();

            try
            {
                if (id != null)
                {
                    ListData = await KasirRepo.GetKasBon(id, DateFrom, DateTo, stat, barang, cb);
                   // ListDetail = await KasirRepo.GetKasBonD(id, DateFrom, DateTo, stat, barang, cb);

                    //if (ListDetail != null || ListDetail.Count() != 0)
                    //{
                    //    ListData.FirstOrDefault().detail = new List<FIN_KAS_BON>();

                    //    foreach (FIN_KAS_BON dtl in ListDetail)
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
        [HttpGet("GetKasBonDTL")]
        public async Task<ActionResult<string>> GetKasBonDTL(string id = null, DateTime? dt1 = null, DateTime? dt2 = null, string stat = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<FIN_KAS_BON> ListDTL = new List<FIN_KAS_BON>();

            try
            {

                ListDTL = await KasirRepo.GetKasBonD(id, dt1, dt2, stat, barang, cb);

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

        [HttpPost("Delete")]
        public async Task<ActionResult<Response>> Delete([FromForm] FIN_KAS_BON_H data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            string blthn = DateTime.Now.ToString("yyyyMM");
            try
            {
                await KasirRepo.Delete(data.nomor, data.Last_Updated_By, trans);
                await KasirRepo.DeleteDTL(data.nomor, data.Last_Updated_By, trans);
                //foreach (var item in data.details.Where(x => x.Kd_Stok != null && x.Kd_Stok != string.Empty))
                //{
                //    await HelperRepo.SP_rilis_booked(item.Kd_Cabang, blthn, item.Kd_Stok, item.Kd_satuan, item.Qty, trans, conn);

                //}
                //await DORepo.UpdateStatSOD(data.No_sp, data.Last_Updated_By, trans);

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