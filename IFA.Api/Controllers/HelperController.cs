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
    public class HelperController : ControllerBase
    {
        [HttpGet("GetNoTrans")]
        public async Task<ActionResult<string>> GetNoTrans(string prefix, DateTime transdate, string kdcabang)
        {
            var successReponse = new Response();


            try
            {
                string nopo = await HelperRepo.GetNoTrans(prefix, transdate, kdcabang);

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

        [HttpGet("GetNoTransx")]
        public async Task<ActionResult<string>> GetNoTransx(string prefix, string kdcabang)
        {
            var successReponse = new Response();


            try
            {
                string nopo = await HelperRepo.GetNoTransx(prefix, kdcabang);

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

        [HttpGet("GetBlthn")]
        public ActionResult<string> GetBlthn()
        {
            var successReponse = new Response();


            try
            {
                string nopo = HelperRepo.GenBlthn();

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
        [HttpGet("GetGudang")]
        public ActionResult<string> GetGudang()
        {
            IEnumerable<SIF_Gudang> ListGudang = new List<SIF_Gudang>();
            var successReponse = new Response();


            try
            {
                ListGudang= HelperRepo.GetGudang();
                var GudangList = ListGudang.Select(s => new {
                    s.Kode_Gudang,
                    s.Nama_Gudang,
                    s.Kd_Cabang
                }).ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(GudangList);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetPeriodeBuku")]
        public ActionResult<string> GetPeriodeBuku()
        {
            IEnumerable<Periode_Buku> ListPeriode = new List<Periode_Buku>();
            var successReponse = new Response();


            try
            {
                ListPeriode = HelperRepo.GetPeriodeBuku();
                var PeriodeList = ListPeriode.Select(s => new {
                    s.thnbln,
                    s.nama
                }).ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(PeriodeList);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetCustomer")]
        public ActionResult<string> GetCustomer()
        {
            IEnumerable<CustomerVM> ListGudang = new List<CustomerVM>();
            var successReponse = new Response();
            try
            {
                ListGudang = HelperRepo.GetCustomer();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListGudang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetPaket")]
        public ActionResult<string> GetPaket()
        {
            IEnumerable<PaketVM> ListGudang = new List<PaketVM>();
            var successReponse = new Response();
            try
            {
                ListGudang = HelperRepo.GetPaket();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListGudang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetPaketList")]
        public async Task<ActionResult<string>> GetPaketList(string no_paket)
        {
            IEnumerable<PaketVMList> ListGudang = new List<PaketVMList>();
            var successReponse = new Response();
            try
            {
                ListGudang = await HelperRepo.GetPaketList(no_paket);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListGudang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetCabang")]
        public ActionResult<string> GetCabang(string kdcb = null)
        {
            IEnumerable<SIF_Cabang> ListData = new List<SIF_Cabang>();
            var successReponse = new Response();
            try
            {
                ListData = HelperRepo.GetCabang(kdcb);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetCabangALL")]
        public ActionResult<string> GetCabangALL()
        {
            IEnumerable<SIF_Cabang> ListData = new List<SIF_Cabang>();
            var successReponse = new Response();
            try
            {
                ListData = HelperRepo.GetCabangALL();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);
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

        [HttpGet("PolaEntry")]
        public async Task<ActionResult<string>> PolaEntry(string id)
        {
            IEnumerable<SIF_buku_besar> ListData = new List<SIF_buku_besar>();
            var successReponse = new Response();


            try
            {
                ListData = await HelperRepo.PolaEntry(id);
                var SelectList = ListData.Select(s => new {
                    s.kd_buku_besar,
                    s.pola_entry

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
        [HttpGet("GetHargaBarang")]
        public async Task<ActionResult<string>> GetHargaBarang(string kdcabang)
        {
            IEnumerable<BarangHargaVM> ListGudang = new List<BarangHargaVM>();
            var successReponse = new Response();
            try
            {
                ListGudang = await HelperRepo.GetHargaBarang(kdcabang);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListGudang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetAuthOTP")]
        public async Task<ActionResult<string>> GetAuthOTP(string password)
        {
            IEnumerable<AuthVM> ListGudang = new List<AuthVM>();
            var successReponse = new Response();
            try
            {
                ListGudang = await HelperRepo.GetAuthOTP(password);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListGudang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetHargaBarangMobile/{id}")]
        public async Task<ActionResult<string>> GetHargaBarangMobile(string id)
        {
            IEnumerable<BarangHargaVM> ListBarang = new List<BarangHargaVM>();
            var successReponse = new Response();
            try
            {
                ListBarang = await HelperRepo.GetHargaBarangMobile(id);

                var BarangList = ListBarang.Select(s => new {
                    s.Kode_Barang,
                    s.Nama_Barang,
                    s.nama_cabang,
                    s.Harga_Rupiah,
                    s.harga_rupiah2,
                    s.harga_rupiah3,
                    s.harga_rupiah4,
                    s.Kd_Satuan
                }).ToList();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(BarangList);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }
        [HttpGet("GetKasir")]
        public async Task<ActionResult<string>> GetKasir()
        {
            IEnumerable<KasirVM> ListGudang = new List<KasirVM>();
            var successReponse = new Response();
            try
            {
                ListGudang =await HelperRepo.GetKasir();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListGudang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetCboKenek")]
        public ActionResult<string> GetCboKenek()
        {
            IEnumerable<SIF_Pegawai> ListKenek = new List<SIF_Pegawai>();
            var successReponse = new Response();


            try
            {
                ListKenek = SIF_PegawaiRepo.GetSopir();
                var KenekList = ListKenek.ToList();
                //var KenekList = ListKenek.Where(x => x.Kode_Jabatan == "J00012").Select(s => new {
                //   s.Kode_Pegawai,
                //   s.Nama_Pegawai
                //}).ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(KenekList);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }


        [HttpGet("GetKendaraan")]
        public async Task<ActionResult<string>> GetKendaraan()
        {
            IEnumerable<Kendaraan> ListKendaraan = new List<Kendaraan>();
            var successReponse = new Response();
            try
            {
                ListKendaraan = await HelperRepo.GetKendaraan();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListKendaraan);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetNoInv")]
        public string GetNoInv(string refNo)
        {
            var successReponse = new Response();
            string nopo = "";
            try
            {
                nopo= HelperRepo.GetNoInv(refNo);
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


        [HttpGet("CekBO")]
        public string CekBO(string kdstok)
        {
            var successReponse = new Response();
            string nopo = "";
            try
            {
                nopo = HelperRepo.CekBO(kdstok);
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

        [HttpGet("CekSaldo")]
        public string CekSaldo(string kdstok, string bultah)
        {
            var successReponse = new Response();
            string nopo = "";
            try
            {
                nopo = HelperRepo.CekSaldo(kdstok,bultah);

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


        [HttpPost("UpdatePasswordUser")]
        public async Task<ActionResult<Response>> UpdatePasswordUser([FromForm] MUSER data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {

                await HelperRepo.UpdatePasswordUser(data, trans);

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

        [HttpPost("UpdateCabang")]
        public async Task<ActionResult<Response>> UpdateCabang([FromForm] MUSER data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {

                string no_ref = "";
                no_ref = await HelperRepo.GetCabangFromGudang(data.cabang_new);

                data.kd_cabang = no_ref;
                await HelperRepo.UpdateCabang(data, trans,conn);

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

        [HttpGet("GetGudangFromCabang")]
        public async Task<string> GetGudangFromCabang(string kd_cabang)
        {
            var successReponse = new Response();
           
            try
            {
                string no_ref = "";
                no_ref = await HelperRepo.GetGudangFromCabang(kd_cabang);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(no_ref);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetGudangDefaultByCabang")]
        public async Task<string> GetGudangDefaultByCabang(string cabang)
        {
            var successReponse = new Response();

            try
            {
                var model = await HelperRepo.GetGudangDefaultByCabang(cabang);
                var GudangList = model.Select(s => new {
                    s.Kode_Gudang,
                    s.Nama_Gudang,
                    s.Kd_Cabang
                }).ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(GudangList);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetCabangFromGudang")]
        public async Task<string> GetCabangFromGudang(string kd_gudang)
        {
            var successReponse = new Response();
            
            try
            {
                string no_ref = "";
                no_ref =await HelperRepo.GetCabangFromGudang(kd_gudang);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(no_ref);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetKdBarang")]
        public string GetKdBarang()
        {
            var successReponse = new Response();

            try
            {
                string no_ref = "";
                no_ref = HelperRepo.GetKdBarang();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(no_ref);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("PostKalkulasi")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<string> PostKalkulasi([FromForm]KalkulasiStokVM data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                //await HelperRepo.SPKalkulasi(data, trans, conn
                await HelperRepo.KalkulasiSTOK(data, trans);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = "Kalkulasi Sukses";

                DataAccess.CloseTransaction(conn, trans, true);
            }
            catch(Exception e)
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

        [HttpPost("ResetBooking")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<string> ResetBooking([FromForm]KalkulasiStokVM data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                await HelperRepo.ResetBooking(data,trans);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = "Reset Booked Sukses";

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

        [HttpPost("GetGiro")]
        //[RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<string> GetGiro(string kdcb, string kdcust=null,string nomor=null )
        {
            IEnumerable<FIN_GIRO> ListData = new List<FIN_GIRO>();
            var successReponse = new Response();
            try
            {
                await HelperRepo.GetGiro(kdcb, kdcust,nomor);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);

               // DataAccess.CloseTransaction(conn, trans, true);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                //ada fail -> rollback
                //DataAccess.CloseTransaction(conn, trans, false);

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
               // DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("GetGiroBeli")]
        //[RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<string> GetGiroBeli(string kdcb, string kdsup = null)
        {
            IEnumerable<FIN_GIRO> ListData = new List<FIN_GIRO>();
            var successReponse = new Response();
            try
            {
                await HelperRepo.GetGiroBeli(kdcb, kdsup);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);

                // DataAccess.CloseTransaction(conn, trans, true);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                //ada fail -> rollback
                //DataAccess.CloseTransaction(conn, trans, false);

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
                // DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetJnsBayar")]
        public async Task<ActionResult<string>> GetJnsBayar()
        {
            IEnumerable<SIF_Gen_Reff_D> ListData = new List<SIF_Gen_Reff_D>();
            var successReponse = new Response();


            try
            {
                ListData = await HelperRepo.GetJnsBayar();
                var SelectList = ListData.Select(s => new {
                    s.Id_Data,
                    s.Desc_Data
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

        [HttpGet("GetJnsJurnal")]
        public async Task<ActionResult<string>> GetJnsJurnal()
        {
            IEnumerable<SIF_Gen_Reff_D> ListData = new List<SIF_Gen_Reff_D>();
            var successReponse = new Response();


            try
            {
                ListData = await HelperRepo.GetJnsJurnal();
                var SelectList = ListData.Select(s => new {
                    s.Id_Data,
                    s.Desc_Data
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

        [HttpGet("GetRekBank")]
        public async Task<ActionResult<string>> GetRekBank()
        {
            IEnumerable<SIF_Bank> ListData = new List<SIF_Bank>();
            var successReponse = new Response();


            try
            {
                ListData = await HelperRepo.GetRekBank();
                var SelectList = ListData.Select(s => new {
                    s.kd_bank,
                    s.nama_bank
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
        [HttpGet("GetValuta")]
        public async Task<ActionResult<string>> GetValuta()
        {
            IEnumerable<SIF_Valuta> ListData = new List<SIF_Valuta>();
            var successReponse = new Response();


            try
            {
                ListData = await HelperRepo.GetValuta();
                var SelectList = ListData.Select(s => new {
                    s.Kode_Valuta,
                    s.Nama_Valuta
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

        [HttpGet("GetSysSettingByKey/{key}")]
        public async Task<ActionResult<string>> GetSysSettingByKey(string key)
        {
            SIF_SETTING ListData = new SIF_SETTING();
            var successReponse = new Response();

            try
            {
                ListData = await HelperRepo.GetSysSettingByKey(key);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("UpdateSysSetting")]
        public async Task<ActionResult<Response>> UpdateSysSetting([FromForm] SIF_SETTING data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {

                await HelperRepo.UpdateSysSetting(data, trans);

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

        [HttpGet("GetJenisGiro")]
        public async Task<ActionResult<string>> GetJenisGiro()
        {
            IEnumerable<SIF_Gen_Reff_D> ListData = new List<SIF_Gen_Reff_D>();
            var successReponse = new Response();

            try
            {
                ListData = await HelperRepo.GetJenisGiro();
               
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetDivisi")]
        public async Task<ActionResult<string>> GetDivisi()
        {
            IEnumerable<SIF_Departemen> ListData = new List<SIF_Departemen>();
            var successReponse = new Response();

            try
            {
                ListData = await HelperRepo.GetDivisi();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetBankAsal")]
        public async Task<ActionResult<string>> GetBankAsal()
        {
            IEnumerable<SIF_Gen_Reff_D> ListData = new List<SIF_Gen_Reff_D>();
            var successReponse = new Response();

            try
            {
                ListData = await HelperRepo.GetBankAsal();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetBankTujuan")]
        public async Task<ActionResult<string>> GetBankTujuan()
        {
            IEnumerable<SIF_Bank> ListData = new List<SIF_Bank>();
            var successReponse = new Response();

            try
            {
                ListData = await HelperRepo.GetBankTujuan();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }
        [HttpGet("GetKartu")]
        public async Task<ActionResult<string>> GetKartu()
        {
            IEnumerable<SIF_CUSTOMER> ListData = new List<SIF_CUSTOMER>();
            var successReponse = new Response();

            try
            {
                ListData = await HelperRepo.GetKartu();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetKartuGiro")]
        public async Task<ActionResult<string>> GetKartuGiro()
        {
            IEnumerable<v_kartu_csv> ListData = new List<v_kartu_csv>();
            var successReponse = new Response();

            try
            {
                ListData = await HelperRepo.GetKartuGiro();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetAlamatKirim")]
        public async Task<ActionResult<string>> GetAlamatKirim()
        {
            IEnumerable<SIF_ALAMAT_KIRIM> ListData = new List<SIF_ALAMAT_KIRIM>();
            var successReponse = new Response();

            try
            {
                ListData = await HelperRepo.GetAlamatKirim();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);
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