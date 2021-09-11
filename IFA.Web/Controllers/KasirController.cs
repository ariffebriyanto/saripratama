using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using ERP.Domain.Base;
using ERP.Web.Controllers;
using ERP.Web.Utils;
using IFA.Domain.Helpers;
using IFA.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using System.Diagnostics;
using ERP.Web;
using System.IO;
using IFA.Domain.Utils;

namespace IFA.Web.Controllers
{
    public class KasirController : BaseController
    {
        public KasirController(FactoryClass factoryClass,    
           IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }

        [Authorize(Roles = "Admin, User, UAT")]
        public IActionResult Index(string id = "", string mode = "")
        {
            if (id == "")
            {
                ViewBag.Mode = "NEW";
            }
            else
            {
                ViewBag.Mode = mode;
                ViewBag.Id = id;
            }

            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, FIN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveKasBon(FIN_KAS_BON_H data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            string transno = "";
            var mode = "";

            if (data.nomor == null)
            {
                mode = "NEW";
                response = await client.CallAPIGet("Helper/GetNoTrans?prefix=KBN&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                if (response.Success)
                {
                    transno = JsonConvert.DeserializeObject<string>(response.Message);
                }
                data.nomor = transno;
            }
            else
            {
                transno = data.nomor;
            }
            //data.gudang = kd_gudang;
            data.Kd_Cabang = BranchID;
            data.tipe_trans = "JKK-KBB-03";
            data.tgl_trans = DateTime.Now;
            //data.tgl_posting = DateTime.Now;
            //data.nomor = transno;
            data.total_trans = data.total_trans;
            //data.kd_valuta = "IDR";
            //data.kurs_valuta = 1;
            data.Last_Created_By = UserID;
            data.Last_Create_Date = DateTime.Now;
            //data.sudah_sj = 1;
            //data.gudang_tujuan=
            
            for (int i = 0; i <= data.detail.Count() - 1; i++)
            {
                
                data.detail[i].Kd_Cabang = BranchID;
                data.detail[i].tipe_trans = "JKK-KBB-03";
                data.detail[i].tgl_trans = DateTime.Now;
                data.detail[i].nomor = transno;
                data.detail[i].no_seq = i+1;
                data.detail[i].jml_trans = data.detail[i].jml_trans;
                data.detail[i].kurs_valuta = data.detail[i].kurs_valuta;
                data.detail[i].kd_valuta = data.detail[i].kd_valuta;
                data.detail[i].kd_kartu = data.detail[i].kd_kartu;
                data.detail[i].Last_Created_By = UserID;
                data.detail[i].Last_Create_Date = DateTime.Now;
            //    data.detail[i].Program_Name = "Frm_BMB";


            }

            if (mode == "NEW")
            {
                response = await client.CallAPIPost("kasir/SaveKasBon", data);
            }

            if (response.Success)
            {
                response.Result = transno;
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, Kasir, FIN")]
        public IActionResult MonKasBank()
        {
            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, Kasir, FIN")]
        public IActionResult MonKasBon()
        {
            return View();
        }

        public async Task<IActionResult> GetKasbon(FIN_KAS_BON_H data)
        {
            IEnumerable<FIN_KAS_BON_H> result = new List<FIN_KAS_BON_H>();
            ApiClient client = factoryClass.APIClientAccess();
      
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            Response response = new Response();

            var strku = "Kasir/GetKasBon?id=" + data.nomor + "&cb=" + BranchID + "";

            response = await client.CallAPIGet(strku);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_KAS_BON_H>>(response.Message);

               
            }


            ViewBag.Mode = "EDIT";
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT")]
        public async Task<IActionResult> GetMonMutasiKasBon(Filter filterkb)
        {
            IEnumerable<FIN_KAS_BON_H> result = new List<FIN_KAS_BON_H>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

           // var tipe_trans = "JKK-KBB-03";
            response = await client.CallAPIGet("Kasir/GetKasBon?id=" + filterkb.id + "&cb=" + BranchID + "&DateFrom=" + filterkb.DateFrom + "&DateTo=" + filterkb.DateTo);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_KAS_BON_H>>(response.Message);

            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT")]
        public async Task<IActionResult> GetMonMutasiKasBonD(Filter filterkb)
        {
            IEnumerable<FIN_KAS_BON> result = new List<FIN_KAS_BON>();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            Response response = new Response();

            // var tipe_trans = "JKK-KBB-03";
            response = await client.CallAPIGet("Kasir/GetKasBonD?id=" + filterkb.id + "&cb=" + BranchID + "&DateFrom=" + filterkb.DateFrom + "&DateTo=" + filterkb.DateTo);


            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_KAS_BON>>(response.Message);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, FIN")]
        public async Task<IActionResult> PegawaiKasBon()
        {
            IEnumerable<SIF_Pegawai> result = new List<SIF_Pegawai>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            response = await client.CallAPIGet("Kasir/GetKartu");
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Pegawai>>(response.Message);

            }
            return Ok(result);
        }


        [Authorize(Roles = "Admin, User, UAT")]
        public IActionResult KasMasukKeluar(string id = "", string mode = "")
        {
            if (id == "")
            {
                ViewBag.Mode = "NEW";
            }
            else
            {
                ViewBag.Mode = mode;
                ViewBag.Id = id;
            }

            return View();
        }

        [Authorize(Roles = "Admin, User, UAT")]
        public async Task<IActionResult> Rekeningbank()
        {
            IEnumerable<SIF_Bank> result = new List<SIF_Bank>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            response = await client.CallAPIGet("Helper/GetRekBank");
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Bank>>(response.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT")]
        public async Task<IActionResult> Rekeningkas()
        {
            IEnumerable<SIF_buku_besar> result = new List<SIF_buku_besar>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            response = await client.CallAPIGet("Helper/GetRekKas");
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_buku_besar>>(response.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT")]
        public async Task<IActionResult> RekGl()
        {
            IEnumerable<SIF_buku_besar> result = new List<SIF_buku_besar>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            response = await client.CallAPIGet("Helper/GetRekGL");
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_buku_besar>>(response.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT")]
        public async Task<IActionResult> BukuPusat()
        {
            IEnumerable<SIF_buku_pusat> result = new List<SIF_buku_pusat>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            response = await client.CallAPIGet("Helper/GetRekBP");
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_buku_pusat>>(response.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveKeluarMasuk(FIN_JURNAL data , string rek_attribute1)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            string transno = "";
            var mode = "";
            var jenisatr = rek_attribute1;
            

            if (data.no_jur == null)
            {
                mode = "NEW";
                string strjenis = "";

                if (jenisatr.Contains("JKK"))
                {
                    strjenis = "JKK";
                }
                else
                {
                    strjenis = "JKM";
                }
               
                response = await client.CallAPIGet("Helper/GetNoTrans?prefix=" + strjenis + "&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                if (response.Success)
                {
                    transno = JsonConvert.DeserializeObject<string>(response.Message);
                }
                data.no_jur = transno;
            }
            else
            {
                transno = data.no_jur;
            }
            //data.gudang = kd_gudang;
            data.kd_kartu = data.kd_kartu;
            data.Kd_cabang = BranchID;
            data.nama = data.nama;
            data.no_ref1 = data.no_ref1;
            data.alamat = data.alamat;
            data.tipe_trans = jenisatr;
            data.tgl_trans = DateTime.Now;
            data.tgl_posting = DateTime.Now;
            data.keterangan = data.keterangan;
            data.jml_rp_trans = data.jml_rp_trans;
            data.jml_val_trans = data.jml_val_trans;
            data.kd_valuta = "IDR";
            data.kurs_valuta = 1;
            data.thnbln = DateTime.Now.ToString("yyyyMM");
            data.Last_created_by = UserID;
            data.Last_create_date = DateTime.Now;
   

            for (int i = 0; i <= data.detail.Count() - 1; i++)
            {
                data.detail[i].Kd_Cabang = BranchID;
                data.detail[i].no_jur= data.no_jur;
                data.detail[i].kartu = data.detail[i].kd_buku_besar;
                data.detail[i].kd_buku_besar = data.rek_attribute2;
                data.detail[i].kd_buku_pusat = data.detail[i].kd_buku_pusat;
                data.detail[i].keterangan = data.detail[i].keterangan;
                data.detail[i].no_seq = i + 1;
                data.detail[i].kd_valuta = "IDR";
                data.detail[i].kurs_valuta = 1;
                if (jenisatr.Contains("JKK"))
                {
                    data.detail[i].saldo_rp_debet = data.detail[i].saldo_val_debet;
                    data.detail[i].saldo_val_debet = data.detail[i].saldo_val_debet;
                    data.detail[i].saldo_rp_kredit = 0;
                    data.detail[i].saldo_val_kredit = 0;
                }
                else
                {
                    data.detail[i].saldo_rp_debet = 0;
                    data.detail[i].saldo_val_debet = 0;
                    data.detail[i].saldo_rp_kredit = data.detail[i].saldo_val_kredit;
                    data.detail[i].saldo_val_kredit = data.detail[i].saldo_val_kredit;
                }
                data.detail[i].tipe_trans = jenisatr;

                data.detail[i].Last_created_by = UserID;
                data.detail[i].Last_create_date = DateTime.Now;
            }


            //if (mode == "NEW")
            //{
            //    response = await client.CallAPIPost("INV_GUDANG_OUT/MoveGudang_OUT", data);
            //}

            if (mode == "NEW")
            {
                response = await client.CallAPIPost("FIN_JURNAL/SaveJurnal", data);
            }

            if (response.Success)
            {
                response.Result = transno;
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveJurnalRupa(FIN_JURNAL data, string rek_attribute1)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            string transno = "";
            var mode = "";
            var jenisatr = "JRR-KBB-01";


            if (data.no_jur == null)
            {
                mode = "NEW";
                string strjenis = "JRR";

                //if (jenisatr.Contains("JKK"))
                //{
                //    strjenis = "JKK";
                //}
                //else
                //{
                //    strjenis = "JKM";
                //}

                response = await client.CallAPIGet("Helper/GetNoTrans?prefix=" + strjenis + "&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                if (response.Success)
                {
                    transno = JsonConvert.DeserializeObject<string>(response.Message);
                }
                data.no_jur = transno;
            }
            else
            {
                transno = data.no_jur;
            }
            //data.gudang = kd_gudang;
            data.kd_kartu = data.kd_kartu;
            data.Kd_cabang = BranchID;
            data.nama = data.nama;
            data.no_ref1 = data.no_ref1;
            data.alamat = data.alamat;
            data.tipe_trans = jenisatr;
            data.tgl_trans = DateTime.Now;
            data.tgl_posting = DateTime.Now;
            data.keterangan = data.keterangan;
            data.jml_rp_trans = data.jml_rp_trans;
            data.jml_val_trans = data.jml_val_trans;
            data.kd_valuta = "IDR";
            data.kurs_valuta = 1;
            data.thnbln = DateTime.Now.ToString("yyyyMM");
            data.Last_created_by = UserID;
            data.Last_create_date = DateTime.Now;

            decimal totaldebet = 0;
            decimal totalkredit = 0;

            for (int i = 0; i <= data.detail.Count() - 1; i++)
            {
                data.detail[i].Kd_Cabang = BranchID;
                data.detail[i].no_jur = data.no_jur;
                data.detail[i].kartu = data.detail[i].kd_buku_besar;
                //data.detail[i].kd_buku_besar = data.rek_attribute2;
                data.detail[i].kd_buku_besar = "00000";
                data.detail[i].kd_buku_pusat = data.detail[i].kd_buku_pusat;
                data.detail[i].keterangan = data.detail[i].keterangan;
                data.detail[i].no_seq = i + 1;
                data.detail[i].kd_valuta = "IDR";
                data.detail[i].kurs_valuta = 1;
               // if (jenisatr.Contains("JKK"))
                //{
                    data.detail[i].saldo_rp_debet = data.detail[i].saldo_val_debet;
                    data.detail[i].saldo_val_debet = data.detail[i].saldo_val_debet;
                    
               // }
               // else
               // {
                   
                    //data.detail[i].saldo_rp_kredit = data.detail[i].saldo_val_kredit;
                    //data.detail[i].saldo_val_kredit = data.detail[i].saldo_val_kredit;
               // }
                data.detail[i].tipe_trans = jenisatr;

                data.detail[i].Last_created_by = UserID;
                data.detail[i].Last_create_date = DateTime.Now;
                totaldebet += Convert.ToDecimal(data.detail[i].saldo_val_debet);
                totalkredit += Convert.ToDecimal(data.detail[i].saldo_val_kredit);
            }


            //if (mode == "NEW")
            //{
            //    response = await client.CallAPIPost("INV_GUDANG_OUT/MoveGudang_OUT", data);
            //}
            if (totaldebet == totalkredit)
            {
                if (mode == "NEW")
                {
                    response = await client.CallAPIPost("FIN_JURNAL/SaveJurnal", data);
                }

                if (response.Success)
                {
                    response.Result = transno;

                }

            }
            else
            {
                response.Result = "tidaksama";
            }
                

            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT")]
        public async Task<IActionResult> GetKeluarMasuk(FIN_JURNAL data)
        {
            IEnumerable<FIN_JURNAL> result = new List<FIN_JURNAL>();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            Response response = new Response();
            var strku = "FIN_JURNAL/GetJurnal?id=" + data.no_jur + "&cb=" + BranchID + "";
            response = await client.CallAPIGet(strku.ToString());
            //response = await client.CallAPIGet("FIN_JURNAL/GetJurnal?id=1/00001JKK/200607&cb=1");


            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_JURNAL>>(response.Message);


            }


            ViewBag.Mode = "EDIT";
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT")]
        public async Task<IActionResult> GetMonMutasiKasBank(Filter filterkb)
        {
            IEnumerable<FIN_JURNAL> result = new List<FIN_JURNAL>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            //var tipe_trans = "JKK-KBB-03";
            response = await client.CallAPIGet("FIN_JURNAL/GetJurnal?id=" + filterkb.id + "&cb=" + BranchID + "&DateFrom=" + filterkb.DateFrom + "&DateTo=" + filterkb.DateTo);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_JURNAL>>(response.Message);

            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT")]
        public async Task<IActionResult> GetMonMutasiKasBankD(Filter filterkb)
        {
            IEnumerable<FIN_JURNAL_D> result = new List<FIN_JURNAL_D>();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            Response response = new Response();

            response = await client.CallAPIGet("FIN_JURNAL/GetJurnalD?id=" + filterkb.id + "&cb=" + BranchID + "&DateFrom=" + filterkb.DateFrom + "&DateTo=" + filterkb.DateTo);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_JURNAL_D>>(response.Message);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Delete(SALES_SO data)
        {
            Response response = new Response();
            Response responseBooked = new Response();

            ApiClient client = factoryClass.APIClientAccess();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var JenisUsaha = claimsIdentity.FindFirst("JenisUsaha").Value;
            var akses_penjualan = claimsIdentity.FindFirst("akses_penjualan").Value;
            SALES_SO _so = new SALES_SO();
            List<SALES_SO_D> _dtl = new List<SALES_SO_D>();
            data.booked = new List<SALES_BOOKED>();


            data.Kd_Cabang = BranchID;
            data.Last_Updated_By = UserID;
            try
            {
                //response = await client.CallAPIGet("DO/GetDO?no_po=" + filterPO.no_po

                var ret = await client.CallAPIGet("DO/GetDO?no_po=" + data.No_sp);
                if (ret.Success)
                {
                    _so = JsonConvert.DeserializeObject<List<SALES_SO>>(ret.Message).FirstOrDefault();
                    _so.STATUS_DO = "BATAL";
                    _so.Program_Name = "Pembatalan SO";
                    //response = await client.CallAPIGet("DO/GetDODetail?no_po=" + filterPO.no_po
                    response = await client.CallAPIGet("DO/GetSOD?no_po=" + data.No_sp);

                    if (response.Success)
                    {
                        _dtl = JsonConvert.DeserializeObject<List<SALES_SO_D>>(response.Message);
                        if (_dtl != null && _dtl.Count() > 0)
                        {
                            _so.details = new List<SALES_SO_D>();
                            foreach (var item in _dtl)
                            {
                                _so.details.Add(item);
                            }
                            response = await client.CallAPIPost("DO/DeleteSO", _so);
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "Data Tidak Ditemukan";
                            response.Result = "failed";
                            return Ok(response);
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Data Tidak Ditemukan";
                        response.Result = "failed";
                        return Ok(response);
                    }
                }
                else
                {
                    ret.Success = false;
                    ret.Message = "Data Tidak Ditemukan";
                    ret.Result = "failed";
                    return Ok(ret);
                }
            }
            catch (Exception e)
            {
                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (factoryClass.config.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }
            }

            //try
            //{
            //        response = await client.CallAPIPost("DO/DeleteSO", data);

            //}
            //catch (Exception e)
            //{
            //    StackTrace st = new StackTrace(e, true);
            //    StackFrame frame = st.GetFrame(st.FrameCount - 1);
            //    string fileName = frame.GetFileName();
            //    string methodName = frame.GetMethod().Name;
            //    int line = frame.GetFileLineNumber();

            //    if (factoryClass.config.application != "development")
            //    {
            //        var path = Path.Combine(Startup.contentRoot, "appsettings.json");

            //        string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
            //        EmailErrorLog.SendEmail(emailbody, path);
            //    }
            //}


            return Ok(response);
        }

        public IActionResult JurnalRupa(string id = "", string mode = "")
        {
            if (id == "")
            {
                ViewBag.Mode = "NEW";
            }
            else
            {
                ViewBag.Mode = mode;
                ViewBag.Id = id;
            }

            return View();
        }


    }
}