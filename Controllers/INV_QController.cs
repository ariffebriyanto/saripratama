using System;
using System.Collections.Generic;
using System.Linq;
using ERP.Api.Utils;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ERP.Domain.Base;
using IFA.Api.Repositories;
using IFA.Domain.Models;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using ERP.Api;
using System.Diagnostics;
using IFA.Domain.Utils;

namespace IFA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class INV_QController : ControllerBase
    {
        [HttpGet("GetNotrans")]
        public ActionResult<string> GetNotrans(string kd_cabang = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_QC> ListNotrans = new List<INV_QC>();

            try
            {
                ListNotrans = INV_QCRepo.GetNotransCbo(kd_cabang);
                var ListNoPO = ListNotrans.Select(s => new {
                    s.no_trans,
                    s.p_np,
                    s.Nama_Supplier,
                    s.no_po
                    
             
                }).ToList();
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


        [HttpGet("GetListPO")]
        public ActionResult<string> GetListPO(string kd_cabang = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_QC> ListNotrans = new List<INV_QC>();

            try
            {
                ListNotrans = INV_QCRepo.GetListPOCbo(kd_cabang);
                var ListNoPO = ListNotrans.Select(s => new {
                    s.no_po

                }).ToList();
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

        [HttpGet("GetPO")]
        public ActionResult<string> GetPO(string no_po = null)
        {
            var successReponse = new Response();
            IEnumerable<PURC_PO_D> ListPO = new List<PURC_PO_D>();

            try
            {
                ListPO = INV_QCRepo.GetPO(no_po);

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

        [HttpGet("GetQC")]
        public ActionResult<string> GetQC(string no_trans = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_QC> ListQCM = new List<INV_QC>();

            try
            {
                ListQCM = INV_QCRepo.GetSavedQC(no_trans);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListQCM);
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
        public ActionResult<string> GetDetailQC(string no_trans = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_QC> ListQC = new List<INV_QC>();

            try
            {
                ListQC = INV_QCRepo.GetSavedQC(no_trans);

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


        [HttpGet("GetMonitoringQC")]
        public ActionResult<string> GetMonitoringQC(string kd_stok = null, string DateFrom = null, string DateTo = null)
        {
            var successReponse = new Response();
            IEnumerable<INV_QC> ListQC = new List<INV_QC>();

            try
            {
                //ListQC = INV_QCRepo.GetMonitoringQC(kd_stok,DateFrom, DateTo);
                ListQC = INV_QCRepo.GetMonitoringQC(kd_stok, DateFrom, DateTo);

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

        [HttpGet("GetDetailPO")]
        public async Task<ActionResult<string>> GetDetailPO(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null)
        {
            var successReponse = new Response();
            IEnumerable<PURC_PO_D> ListPO = new List<PURC_PO_D>();

            try
            {
                ListPO = await PUPR_PO_DRepo.GetPODetail(no_po, DateFrom, DateTo, status_po);

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
       

        [HttpPost("SaveQC")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> SaveQC([FromForm] PURC_PO data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                await INV_QC_MRepo.SaveQC_M(data, trans);
                if (data.podetail != null)
                {
                    foreach (PURC_PO_D podetail in data.podetail)
                    {
                        await INV_QCRepo.SaveQCDetail(podetail, trans);
                        //  INV_QCRepo.UpdateStatPO(podetail, trans, conn);

                    }
                }
                await INV_QCRepo.SPStatusPO(data.no_trans, data.no_ref, trans, conn);
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

        [HttpPost("UpdateQC")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> UpdateQC([FromForm] PURC_PO data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                INV_QC_MRepo.UpdateQC_M(data, trans);
                if (data.podetail != null)
                {
                    INV_QCRepo.DeleteQCDetail(data.podetail[0].no_po, trans);
                    foreach (PURC_PO_D podetail in data.podetail)
                    {
                        await INV_QCRepo.SaveQCDetail(podetail, trans);
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

        [HttpGet("GenPONumber")]
        public async Task<ActionResult<string>> GenPONumber(string prefix = null, DateTime? DateFrom = null)
        {
            var successReponse = new Response();


            try
            {
                string nopo = await PURC_PORepo.GenPONumber(prefix, DateFrom);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(nopo);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetKartuStok")]
        public ActionResult<string> GetKartuStok(string kd_stok, string bulan, string tahun, string kd_cabang)
        {
            var successReponse = new Response();
            List<V_StokGudang> StokGudangHDR = new List<V_StokGudang>();
            List<vy_saldocard> StokGudangDTL = new List<vy_saldocard>();
            vy_profile profile = new vy_profile();
            int seq = 0;
            string blth = "";
            decimal qtySisa = 0;
            //decimal saldo_awal = 0;
            bool filterTahun = false;
            try
            {
                if (bulan != string.Empty && bulan != null)
                {
                    blth = tahun + bulan;
                }
                else
                {
                    blth = tahun;
                    filterTahun = true;
                }

                //StokGudangHDR = INV_QCRepo.GetKartuStok(kd_stok, blth, filterTahun);
                StokGudangHDR = INV_QCRepo.GetKartuStok(kd_stok, blth,kd_cabang, filterTahun);

                if (filterTahun == true)
                {
                    StokGudangHDR.FirstOrDefault().bultah = tahun;
                }

                profile = INV_QCRepo.getProfile();
                StokGudangHDR.FirstOrDefault().profile = new vy_profile();
                StokGudangHDR.FirstOrDefault().profile.nama = profile.nama;
                StokGudangHDR.FirstOrDefault().profile.alamat = profile.alamat;
                StokGudangHDR.FirstOrDefault().profile.propinsi = profile.propinsi;
                StokGudangHDR.FirstOrDefault().profile.kota = profile.kota;
                StokGudangHDR.FirstOrDefault().profile.telp1 = profile.telp1;
                StokGudangHDR.FirstOrDefault().profile.fax1 = profile.fax1;

                StokGudangDTL = INV_QCRepo.GetKartuStokDetail(kd_stok, blth, filterTahun);

                if (StokGudangDTL.Count > 0)
                {
                    StokGudangHDR.FirstOrDefault().ListSaldo = new List<vy_saldocard>();
                    foreach (vy_saldocard detail in StokGudangDTL)
                    {
                        if (seq == 0)
                        {
                            qtySisa = StokGudangHDR.FirstOrDefault().awal_qty_onstok;
                            
                            seq += 1;
                        }
                        detail.qty_sisa = (qtySisa + detail.qty_in) - detail.qty_out;
                        //detail.awal_qty_onstok= StokGudangHDR.FirstOrDefault().awal_qty_onstok;
                        qtySisa = detail.qty_sisa;
                        StokGudangHDR.FirstOrDefault().ListSaldo.Add(detail);
                    }
                }

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(StokGudangHDR);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                var path = Path.Combine(Startup.contentRoot, "appsettings.json"); 
                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);

                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetPrintQC")]
        public ActionResult<string> GetPrintQC(string no_qc = null)
        {
            var successReponse = new Response();
            PrintQC ListQC = new PrintQC();

            try
            {
                ListQC = INV_QCRepo.GetPrintQC(no_qc);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListQC);
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

        [HttpGet("GetKartuStokGudang")]
        public async Task<ActionResult<string>> GetKartuStokGudang(string kd_stok, string bulan, string tahun, string gudang,string kd_cabang)
        {
            var successReponse = new Response();
            List<V_StokGudang> StokGudangHDR = new List<V_StokGudang>();
            List<vy_saldocard> StokGudangDTL = new List<vy_saldocard>();
            vy_profile profile = new vy_profile();
            int seq = 0;
            string blth = "";
            decimal qtySisa = 0;
            bool filterTahun = false;
            try
            {
                if (bulan != string.Empty && bulan != null)
                {
                    blth = tahun + bulan;
                }
                else
                {
                    blth = tahun;
                    filterTahun = true;
                }
                kd_cabang = await HelperRepo.GetCabangFromGudang(gudang);
                StokGudangHDR = INV_QCRepo.GetKartuStok(kd_stok, blth,kd_cabang, filterTahun);

                if (filterTahun == true)
                {
                    StokGudangHDR.FirstOrDefault().bultah = tahun;
                }

                profile = INV_QCRepo.getProfile();
                StokGudangHDR.FirstOrDefault().profile = new vy_profile();
                StokGudangHDR.FirstOrDefault().profile.nama = profile.nama;
                StokGudangHDR.FirstOrDefault().profile.alamat = profile.alamat;
                StokGudangHDR.FirstOrDefault().profile.propinsi = profile.propinsi;
                StokGudangHDR.FirstOrDefault().profile.kota = profile.kota;
                StokGudangHDR.FirstOrDefault().profile.telp1 = profile.telp1;
                StokGudangHDR.FirstOrDefault().profile.fax1 = profile.fax1;

                StokGudangDTL = INV_QCRepo.GetKartuStokDetailGudang(kd_stok, blth, filterTahun, gudang);

                if (StokGudangDTL.Count > 0)
                {
                    StokGudangHDR.FirstOrDefault().ListSaldo = new List<vy_saldocard>();
                    foreach (vy_saldocard detail in StokGudangDTL)
                    {
                        if (seq == 0)
                        {
                            qtySisa = StokGudangHDR.FirstOrDefault().awal_qty_onstok;
                            seq += 1;
                        }
                        detail.qty_sisa = (qtySisa + detail.qty_in) - detail.qty_out;
                        detail.awal_qty_onstok = StokGudangHDR.FirstOrDefault().awal_qty_onstok;
                        qtySisa = detail.qty_sisa;
                        StokGudangHDR.FirstOrDefault().ListSaldo.Add(detail);
                    }
                }

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(StokGudangHDR);
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