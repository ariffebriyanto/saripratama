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
using IFA.Api.Utils;
using IFA.Domain.Models;
using IFA.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace IFA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FIN_JURNALController : ControllerBase
    {
        [HttpPost("SaveJurnal")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveJurnal([FromForm] FIN_JURNAL data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                await FIN_JurnalRepo.Save(data, trans);
                if (data.detail != null)
                {
                    await FIN_JurnalRepo.DelDetail(data.no_jur, trans);
                    foreach (FIN_JURNAL_D gddetail in data.detail)
                    {
                        await FIN_JurnalRepo.SaveDetail(gddetail, trans);
                        // await INV_GUDANG_IN_Repo.SPStokIn(gddetail.Kd_Cabang, gddetail.blthn, gddetail.kd_stok, gddetail.kd_satuan, Math.Abs(gddetail.qty_in), trans, conn);
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

        [HttpGet("GetMonTransaksiHarianPartial")]
        public async Task<ActionResult<string>> GetMonTransaksiHarianPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime? DateFrom = null, DateTime? DateTo = null, string kd_buku_besar = null, string kd_valuta = null)
        {
            var successReponse = new Response();
            List<SaldoVM1> ListPO = new List<SaldoVM1>();

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
                ListPO = await FIN_JurnalRepo.GetMonTransaksiHarianPartial(DateFrom, DateTo, filterquery, sortingquery, kd_buku_besar, kd_valuta, seq);

                var query = ListPO.AsEnumerable().Skip(skip).Take(pageSize);

                successReponse.Success = true;
                successReponse.Result = FIN_JurnalRepo.getCountMonTransaksiHarianPartial(DateFrom, DateTo, filterquery, kd_buku_besar, kd_valuta, seq).Result;
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


        [HttpGet("GetMonTransaksiJurnalPartial")]
        public async Task<ActionResult<string>> GetMonTransaksiJurnalPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime? DateFrom = null, DateTime? DateTo = null, string tipe_trans = null, string kd_valuta = null, string cek_post = null)
        {
            var successReponse = new Response();
            List<FIN_JURNAL> ListPO = new List<FIN_JURNAL>();

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
                ListPO = await FIN_JurnalRepo.GetMonTransaksiJurnalPartial(DateFrom, DateTo, filterquery, sortingquery, tipe_trans, kd_valuta, seq,cek_post);

                var query = ListPO.AsEnumerable().Skip(skip).Take(pageSize);

                successReponse.Success = true;
                successReponse.Result = FIN_JurnalRepo.getCountMonTransaksiJurnalPartial(DateFrom, DateTo, filterquery, tipe_trans, kd_valuta, seq,cek_post).Result;
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

        [HttpGet("GetMonTransaksiJurnalDetail")]
        public async Task<ActionResult<string>> GetMonTransaksiJurnalDetail(string no_jur = null)
        {
            var successReponse = new Response();
            IEnumerable<FIN_JURNAL_D> ListPO = new List<FIN_JURNAL_D>();

            try
            {
                ListPO = await FIN_JurnalRepo.GetMonTransaksiJurnalDetail(no_jur);

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


        [HttpGet("GetSaldo")]
        public async Task<ActionResult<string>> GetSaldo(string kd_rekening, string kd_valuta, string tahun, string bulan)
        {
            var successReponse = new Response();
            IEnumerable<SaldoVM> ListData = new List<SaldoVM>();


            try
            {
                if (kd_rekening != null || tahun != null && bulan != null)
                {
                    ListData = await FIN_JurnalRepo.GetSaldo(kd_rekening, kd_valuta, tahun, bulan);
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


        [HttpGet("GetJurnal")]
        public async Task<ActionResult<string>> GetJurnal(string id = null, DateTime? DateFrom = null, DateTime? DateTo = null, string stat = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<FIN_JURNAL> ListData = new List<FIN_JURNAL>();
            IEnumerable<FIN_JURNAL_D> ListDetail = new List<FIN_JURNAL_D>();

            try
            {
                if (id != null || DateFrom != null && DateTo != null)
                {
                    ListData = await FIN_JurnalRepo.GetJurnal(id, DateFrom, DateTo, stat, barang, cb);
                    ListDetail = await FIN_JurnalRepo.GetJurnalDTL(id, DateFrom, DateTo, stat, barang, cb);

                    if (ListDetail != null || ListDetail.Count() != 0 )
                    {
                        ListData.FirstOrDefault().detail = new List<FIN_JURNAL_D>();

                        foreach (FIN_JURNAL_D dtl in ListDetail)
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

        [HttpGet("GetMonJurnal")]
        public async Task<ActionResult<string>> GetMonJurnal(string id = null, DateTime? DateFrom = null, DateTime? DateTo = null, string stat = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<FIN_JURNAL> ListData = new List<FIN_JURNAL>();
            IEnumerable<FIN_JURNAL_D> ListDetail = new List<FIN_JURNAL_D>();

            try
            {
                if (id != null || DateFrom != null && DateTo != null)
                {
                    ListData = await FIN_JurnalRepo.GetJurnal(id, DateFrom, DateTo, stat, barang, cb);
                    //ListDetail = await FIN_JurnalRepo.GetJurnalDTL(id, DateFrom, DateTo, stat, barang, cb);

                    //if (ListDetail != null || ListDetail.Count() != 0)
                    //{
                    //    ListData.FirstOrDefault().detail = new List<FIN_JURNAL_D>();

                    //    foreach (FIN_JURNAL_D dtl in ListDetail)
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

        [HttpGet("GetJurnalD")]
        public async Task<ActionResult<string>> GetJurnalD(string id = null, DateTime? dt1 = null, DateTime? dt2 = null, string stat = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<FIN_JURNAL_D> ListDTL = new List<FIN_JURNAL_D>();

            try
            {

                ListDTL = await FIN_JurnalRepo.GetJurnalDTL(id, dt1, dt2, stat, barang, cb);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListDTL);
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

        [HttpGet("GetJurnalDTL")]
        public async Task<ActionResult<string>> GetDODetail(string id = null, DateTime? dt1 = null, DateTime? dt2 = null, string stat = null, string barang = null, string cb = null)
        {
            var successReponse = new Response();
            IEnumerable<FIN_JURNAL_D> ListDTL = new List<FIN_JURNAL_D>();
            try
            {
                ListDTL = await FIN_JurnalRepo.GetJurnalDTL(id, dt1, dt2, stat, barang, cb);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListDTL);
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

        [HttpGet("GetJurnalPending")]
        public async Task<ActionResult<string>> GetJurnalPending()
        {
            var successReponse = new Response();
            IEnumerable<FIN_JURNAL> ListDTL = new List<FIN_JURNAL>();
            try
            {
                ListDTL = await FIN_JurnalRepo.GetJurnalPending();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListDTL);
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

        [HttpGet("GetJurnalDetailPending")]
        public async Task<ActionResult<string>> GetJurnalDetailPending(string nojur)
        {
            var successReponse = new Response();
            IEnumerable<FIN_JURNAL_D> ListDTL = new List<FIN_JURNAL_D>();
            try
            {
                ListDTL = await FIN_JurnalRepo.GetJurnalDetailPending(nojur);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListDTL);
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
        [HttpGet("PostingJurnal")]
        public async Task<ActionResult<string>> PostingJurnal(string nojur)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                string[] nojurSplit = nojur.Split(";");
                foreach(var item in nojurSplit)
                {
                    string no_posting = await FIN_JurnalRepo.PostingJurnal(item, DateTime.Now, trans, conn);
                }
                //ListDTL = await FIN_JurnalRepo.GetJurnalPending();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = "success";
                DataAccess.CloseTransaction(conn, trans, true);
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
                DataAccess.CloseTransaction(conn, trans, false);
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetPiutangUsaha")]
        public async Task<ActionResult<string>> GetPiutangUsaha(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime tanggal, string kd_cust, string no_trans, string tipe)
        {
            var successReponse = new Response();
            List<FIN_PIUTANG_USAHA_Header> ListPO = new List<FIN_PIUTANG_USAHA_Header>();

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
                ListPO = await FIN_JurnalRepo.GetPiutangUsaha(tanggal, filterquery, sortingquery, kd_cust, no_trans, tipe, seq);

                var query = ListPO.AsEnumerable().Skip(skip).Take(pageSize);

                var x = await FIN_JurnalRepo.GetPiutangUsahaCount(tanggal, filterquery, sortingquery, kd_cust, no_trans, tipe, seq);

                successReponse.Success = true;
                successReponse.Result = x.Count().ToString();
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

        [HttpGet("GetPiutangUsahaDetail")]
        public async Task<ActionResult<string>> GetPiutangUsahaDetail(string kd_cust, DateTime tanggal, string no_trans)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            IEnumerable<FIN_PIUTANG_USAHA_Detail> ListDTL = new List<FIN_PIUTANG_USAHA_Detail>();
            try
            {
                ListDTL = await FIN_JurnalRepo.GetPiutangUsahaDetail(kd_cust, tanggal, no_trans);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListDTL); 
                DataAccess.CloseTransaction(conn, trans, true);
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
                DataAccess.CloseTransaction(conn, trans, false);
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetPenjualan")]
        public async Task<ActionResult<string>> GetPenjualan(string no_inv)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            IEnumerable<FIN_PIUTANG_USAHA_Penjualan> ListDTL = new List<FIN_PIUTANG_USAHA_Penjualan>();
            try
            {
                ListDTL = await FIN_JurnalRepo.GetPenjualan(no_inv);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListDTL);
                DataAccess.CloseTransaction(conn, trans, true);
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
                DataAccess.CloseTransaction(conn, trans, false);
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetHutangUsaha")]
        public async Task<ActionResult<string>> GetHutangUsaha(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime tanggal, string kd_cust, string no_trans, string tipe)
        {
            var successReponse = new Response();
            List<FIN_HUTANG_USAHA_Header> ListPO = new List<FIN_HUTANG_USAHA_Header>();

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
                ListPO = await FIN_JurnalRepo.GetHutangUsaha(tanggal, filterquery, sortingquery, kd_cust, no_trans, tipe, seq);

                var query = ListPO.AsEnumerable().Skip(skip).Take(pageSize);

                var x = await FIN_JurnalRepo.GetHutangUsahaCount(tanggal, filterquery, sortingquery, kd_cust, no_trans, tipe, seq);

                successReponse.Success = true;
                successReponse.Result = x.Count().ToString();
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

        [HttpGet("GetHutangUsahaDetail")]
        public async Task<ActionResult<string>> GetHutangUsahaDetail(string kd_cust, DateTime tanggal, string no_trans)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            IEnumerable<FIN_HUTANG_USAHA_Detail> ListDTL = new List<FIN_HUTANG_USAHA_Detail>();
            try
            {
                ListDTL = await FIN_JurnalRepo.GetHutangUsahaDetail(kd_cust, tanggal, no_trans);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListDTL);
                DataAccess.CloseTransaction(conn, trans, true);
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
                DataAccess.CloseTransaction(conn, trans, false);
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetPembelian")]
        public async Task<ActionResult<string>> GetPembelian(string no_inv)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            IEnumerable<FIN_HUTANG_USAHA_Penjualan> ListDTL = new List<FIN_HUTANG_USAHA_Penjualan>();
            try
            {
                ListDTL = await FIN_JurnalRepo.GetPembelian(no_inv);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListDTL);
                DataAccess.CloseTransaction(conn, trans, true);
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
                DataAccess.CloseTransaction(conn, trans, false);
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }
            return JsonConvert.SerializeObject(successReponse);
        }
    }
}
