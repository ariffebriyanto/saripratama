using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ERP.Domain.Base;
using ERP.Web;
using ERP.Web.Controllers;
using ERP.Web.Utils;
using IFA.Domain.Models;
using IFA.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace IFA.Web.Controllers
{
    public class DOController : BaseController
    {
        public DOController(FactoryClass factoryClass,
          IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }

        #region "DO"
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<KasirVM> result = new List<KasirVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            string salesID = "";

            if (RoleName == "PENJUALAN" || RoleName == "BUAT UAT" )
            {
                response = await client.CallAPIGet("Helper/GetKasir");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<KasirVM>>(response.Message);

                    var x = result.Where(w => w.Kode_Pegawai == PegawaiID).FirstOrDefault();
                    if (x != null)
                    {
                        salesID = x.Kode_Sales;
                    }
                }
            }

            ViewBag.salesID = salesID;
            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Create(string id = "", string mode = "")
        {
            IEnumerable<KasirVM> result = new List<KasirVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            var JenisUsaha = claimsIdentity.FindFirst("JenisUsaha").Value;
            var Telp = claimsIdentity.FindFirst("Telp").Value;
            var WA = claimsIdentity.FindFirst("WA").Value;
            var akses_penjualan = claimsIdentity.FindFirst("akses_penjualan").Value;

            string salesID = "";

            if (RoleName == "PENJUALAN" || RoleName == "BUAT UAT" || RoleName == "SPV")
            {
                response = await client.CallAPIGet("Helper/GetKasir");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<KasirVM>>(response.Message);

                    var x = result.Where(w => w.Kode_Pegawai == PegawaiID).FirstOrDefault();
                    if (x != null)
                    {
                        salesID = x.Kode_Sales;
                    }
                }
            }

            ViewBag.salesID = salesID;
            ViewBag.akses_penjualan = akses_penjualan;

            if (id == "")
            {
                ViewBag.Mode = "NEW";
            }
            else
            {
                ViewBag.Mode = mode;
                ViewBag.Id = id;
            }
            ViewBag.BranchID = JenisUsaha;

            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetCustomer()
        {
            IEnumerable<CustomerVM> result = new List<CustomerVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Helper/GetCustomer");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<CustomerVM>>(response.Message);

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

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetPaket()
        {
            IEnumerable<PaketVM> result = new List<PaketVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Helper/GetPaket");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<PaketVM>>(response.Message);

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

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetPaketList(string no_paket)
        {
            IEnumerable<PaketVMList> result = new List<PaketVMList>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Helper/GetPaketList?no_paket=" + no_paket);
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<PaketVMList>>(response.Message);

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

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetAuth(string password)
        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
           
            IEnumerable<AuthVM> result = new List<AuthVM>();
           

            try
            {

                response = await client.CallAPIGet("Helper/GetAuthOTP?password=" + password);


                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<AuthVM>>(response.Message);
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

            return Ok(result);
        }


        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetHargaBarang()
        {
            IEnumerable<BarangHargaVM> result = new List<BarangHargaVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var JenisUsaha = claimsIdentity.FindFirst("JenisUsaha").Value;
            var akses_penjualan = claimsIdentity.FindFirst("akses_penjualan").Value;

            try
            {
                response = await client.CallAPIGet("Helper/GetHargaBarang?kdcabang=" + BranchID);
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<BarangHargaVM>>(response.Message);

                    foreach (var item in result)
                    {
                        if (JenisUsaha == "GROSIR" && akses_penjualan !="CASH")
                        {
                            item.Harga_Rupiah = item.harga_rupiah4;
                        }
                        item.Nama_Barang = item.Nama_Barang + "| Rp." + item.Harga_Rupiah.ToString("#, ##0") + "| " + item.stok;
                    }

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

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDOBarang(string id)
        {
            IEnumerable<SALES_SO_D> result = new List<SALES_SO_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("INV_Q/GetPO?no_po=" + id);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SALES_SO_D>>(response.Message);


            }
            //ViewBag.Mode = "EDIT";
            return Ok(result);
        }


        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetKasir()
        {
            IEnumerable<KasirVM> result = new List<KasirVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Helper/GetKasir");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<KasirVM>>(response.Message);

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

            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> Save(SALES_SO data, Boolean stok)
        {
            Response response = new Response();
            Response responseBooked = new Response();
            Response responseCekStok = new Response();

            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            bool alokasi = false;
            //bool alokasi1 = false;
            string jns_alokasi = "";

            data.Kd_Cabang = BranchID;
            //if (data.jenis_so != "BO PAKET" || data.jenis_so != "BOOKING ORDER")
            //{

            //}
            response = await client.CallAPIPost("DO/CekStok", data);
            if (response.Success == true || data.jenis_so == "BO PAKET"|| data.jenis_so == "BOOKING ORDER") // jika stok ga cukup=false
            { 
               
          

            //TAMBAHAN BERNAD
            var sif_setting = await client.CallAPIGet("Helper/GetSysSettingByKey/IsExecuted");
            var IsExecuted = JsonConvert.DeserializeObject<SIF_SETTING>(sif_setting.Message);
            while (IsExecuted.SYSVALUE == "true")
            {
                System.Threading.Thread.Sleep(1000);
                sif_setting = await client.CallAPIGet("Helper/GetSysSettingByKey/IsExecuted");
                IsExecuted = JsonConvert.DeserializeObject<SIF_SETTING>(sif_setting.Message);

            }

            var sif_settingUpdate = await client.CallAPIGet("Helper/GetSysSettingByKey/IsExecuted");
            var IsExecutedUpdate = JsonConvert.DeserializeObject<SIF_SETTING>(sif_setting.Message);
            IsExecutedUpdate.SYSVALUE = "true";
            await client.CallAPIPost("Helper/UpdateSysSetting", IsExecutedUpdate);

            var mode = "";
            int seqNo = 0;
            decimal qty = 0;
            string nodo = "";
            string kd_gudang = "";
            

           // bool check = false;

            var JenisUsaha = claimsIdentity.FindFirst("JenisUsaha").Value;
            var akses_penjualan = claimsIdentity.FindFirst("akses_penjualan").Value;

            
            data.booked = new List<SALES_BOOKED>();
            try
            {
               
                    var ret = await client.CallAPIGet("Helper/GetGudangFromCabang?kd_cabang=" + BranchID);
                    if (ret.Success)
                    {
                        kd_gudang = JsonConvert.DeserializeObject<string>(ret.Message);
                    }

                    data.Kd_Cabang = BranchID;
                    data.Last_Created_By = UserID;
                    data.Last_Create_Date = DateTime.Now;
                    data.Tipe_trans = "JPJ-KPT-01";
                    if (data.Jenis_sp == "CASH")
                    {
                        data.STATUS_DO = "TERKIRIM";
                        data.Tgl_Kirim_Marketing = DateTime.Now;
                        if (data.Atas_Nama.ToUpper() == "PLEASE SELECT")
                        {
                            data.Atas_Nama = "CASH";
                            data.Kd_Customer = "CASH";
                        }
                    }
                    else
                    {
                        if (data.jenis_so == "BO PAKET" || data.jenis_so == "BOOKING ORDER")
                        {
                            //if (data.jenis_so == "BO PAKET")
                            //{ }
                            data.STATUS_DO = data.jenis_so;

                            alokasi = true;

                            jns_alokasi = data.jenis_so;
                        }
                        else
                        {
                            data.STATUS_DO = "PERSIAPAN BARANG";
                            jns_alokasi = "PERSIAPAN BARANG";
                        }

                    }
                    data.pending = "";
                    data.Departement = "2";
                    data.Status = "OK";
                    //   data.Discount = 0;
                    data.Potongan = 0;
                    data.Flag_Ppn = data.Flag_Ppn;
                    data.PPn = data.PPn;

                    data.no_paket = data.no_paket;
                    data.Status_Simpan = "Y";
                    data.Program_Name = "SO_SPT";






                    foreach (var item in data.details.Where(x => x.kode_Barang != null && x.kode_Barang != string.Empty))
                    {
                        if (alokasi == true)
                        {

                            data.STATUS_DO = jns_alokasi;
                        }

                        item.Kd_Cabang = data.Kd_Cabang;
                        item.tipe_trans = data.Tipe_trans;
                        item.No_sp = data.No_sp;
                        item.No_seq = (seqNo + 1).ToString();
                        item.Kd_Stok = item.kode_Barang;
                        item.disc1 = item.disc1;
                        item.disc2 = item.disc2;
                        item.disc3 = item.disc3;
                        item.disc4 = item.disc4;
                        item.Kd_satuan = item.satuan;
                        item.Last_created_by = data.Last_Created_By;
                        item.Last_create_date = data.Last_Create_Date;
                        item.Deskripsi = item.nama_Barang;
                        item.Status_Simpan = data.Status_Simpan;
                        item.STATUS_DO = data.STATUS_DO;

                        item.kd_gudang = kd_gudang; // utk prc stok gudang
                        item.blthn = DateTime.Now.ToString("yyyyMM");

                        if (JenisUsaha == "TOKO" || akses_penjualan == "CASH")
                        {
                            item.potongan_total = item.potongan_total;
                            item.potongan = item.diskon * item.Qty;
                        }
                        else
                        {
                            item.potongan_total = item.potongan_total;
                            item.potongan = item.diskon;
                        }
                        item.Programe_name = data.No_sp;
                        if (item.flagbonus == true)
                        {
                            item.Bonus = "1";
                        }
                        else
                        {
                            item.Bonus = "0";
                        }

                        if (item.no_booked != Guid.Empty && item.no_booked != null)
                        {
                            responseBooked = await client.CallAPIGet("DO/GetInden?unix_id=" + item.no_booked + "&status=ALOKASI");

                            if (responseBooked.Success)
                            {
                                var result = JsonConvert.DeserializeObject<List<SALES_BOOKED>>(responseBooked.Message);
                                if (result.Count() > 0)
                                {
                                    result.FirstOrDefault().no_sp = data.No_sp;
                                    result.FirstOrDefault().Status = "DO";
                                    result.FirstOrDefault().Last_Updated_By = UserID;
                                    result.FirstOrDefault().Last_Update_Date = DateTime.Now;

                                    data.booked.Add(result.FirstOrDefault());
                                }
                            }
                        }

                        qty += item.Qty;
                        seqNo += 1;
                    }
                    data.Total_qty = qty;

                    response = await client.CallAPIPost("DO/Save", data);
                    nodo = response.Message;
                    response.Result = nodo;


                    if (response.Success && data.Jenis_sp == "CASH")
                    {
                        var invNo = await client.CallAPIGet("Helper/GetNoInv?refNo=" + response.Message);
                        string no = JsonConvert.DeserializeObject<string>(invNo.Message);
                        //  System.Threading.Thread.Sleep(1121);
                        response = await client.CallAPIGet("DO/JurnalSO?invNo=" + no + "&tipetrans=" + data.Tipe_trans);
                     
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
          
          }
            //TAMBAHAN BERNAD
            var sif_settingUpdate1 = await client.CallAPIGet("Helper/GetSysSettingByKey/IsExecuted");
            var IsExecutedUpdate1 = JsonConvert.DeserializeObject<SIF_SETTING>(sif_settingUpdate1.Message);
            IsExecutedUpdate1.SYSVALUE = "false";
            await client.CallAPIPost("Helper/UpdateSysSetting", IsExecutedUpdate1);
           
            return Ok(response);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveRetur(SALES_SO data)
        {
            Response response = new Response();
            Response responseBooked = new Response();

            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            data.Kd_Cabang = BranchID;
            response = await client.CallAPIPost("DO/CekStok", data);
            if (response.Success == true)
            {
                //TAMBAHAN BERNAD
                var sif_setting = await client.CallAPIGet("Helper/GetSysSettingByKey/IsExecuted");
            var IsExecuted = JsonConvert.DeserializeObject<SIF_SETTING>(sif_setting.Message);
            while (IsExecuted.SYSVALUE == "true")
            {
                System.Threading.Thread.Sleep(1000);
                sif_setting = await client.CallAPIGet("Helper/GetSysSettingByKey/IsExecuted");
                IsExecuted = JsonConvert.DeserializeObject<SIF_SETTING>(sif_setting.Message);

            }

            var sif_settingUpdate = await client.CallAPIGet("Helper/GetSysSettingByKey/IsExecuted");
            var IsExecutedUpdate = JsonConvert.DeserializeObject<SIF_SETTING>(sif_setting.Message);
            IsExecutedUpdate.SYSVALUE = "true";
            await client.CallAPIPost("Helper/UpdateSysSetting", IsExecutedUpdate);

            //var mode = "";
            int seqNo = 0;
            decimal qty = 0;
            string nodo = "";
            //var claimsIdentity = User.Identity as ClaimsIdentity;
            //var UserID = claimsIdentity.FindFirst("UserID").Value;
            //var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var JenisUsaha = claimsIdentity.FindFirst("JenisUsaha").Value;
            var akses_penjualan = claimsIdentity.FindFirst("akses_penjualan").Value;

            data.booked = new List<SALES_BOOKED>();
            string noret = "";
            string kd_gudang = "";
            var invNo= await client.CallAPIGet("Helper/GetNoInv?refNo=" + data.SP_REFF);
            string no= JsonConvert.DeserializeObject<string>(invNo.Message);
            var retNo = await client.CallAPIGet("Helper/GetNoTrans?prefix=SPRTR&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
            if (retNo.Success)
            {
                noret = JsonConvert.DeserializeObject<string>(retNo.Message);
            }
            var ret = await client.CallAPIGet("Helper/GetGudangFromCabang?kd_cabang=" + BranchID);
            if (ret.Success)
            {
                kd_gudang = JsonConvert.DeserializeObject<string>(ret.Message);
            }
            //System.Threading.Thread.Sleep(2000);
            try
            {
                //response.Success = true;

                var response1 = await client.CallAPIGet("DO/GetDO?no_po=" + data.SP_REFF);

                if (response1.Success)
                {
                    var result = JsonConvert.DeserializeObject<List<SALES_SO>>(response1.Message);
                    result.FirstOrDefault().JML_RP_TRANS = result.FirstOrDefault().JML_RP_TRANS * -1;
                    result.FirstOrDefault().JML_VALAS_TRANS = result.FirstOrDefault().JML_VALAS_TRANS * -1;
                    result.FirstOrDefault().Discount = result.FirstOrDefault().Discount * -1;
                    result.FirstOrDefault().Total_qty = result.FirstOrDefault().Total_qty * -1;
                    result.FirstOrDefault().Biaya = result.FirstOrDefault().Biaya * -1;
                    result.FirstOrDefault().PPn = result.FirstOrDefault().PPn * -1;
                    result.FirstOrDefault().STATUS_DO = "RETUR";
                    result.FirstOrDefault().blthn = DateTime.Now.ToString("yyyyMM");
                    result.FirstOrDefault().details = new List<SALES_SO_D>();
                    foreach (var item in result.FirstOrDefault().detailsvm)
                    {
                        var model = new SALES_SO_D();
                        model.blthn = DateTime.Now.ToString("yyyyMM");
                        model.Kd_Cabang = BranchID;
                        model.kd_gudang = kd_gudang;
                        model.Kd_satuan = item.satuan;
                        model.No_sp = data.SP_REFF;
                        model.Kd_Stok = item.kode_Barang;
                        model.Qty = item.qty * -1;
                        model.STATUS_DO = "RETUR";

                        result.FirstOrDefault().details.Add(model);
                    }
                  //  System.Threading.Thread.Sleep(2536);
                    response = await client.CallAPIPost("DO/UpdateRetur", result.FirstOrDefault());

                    var retur = new SALES_RETUR();
                    var returd = new List<SALES_RETUR_D>();

                    retur.No_retur = noret;
                    retur.Kd_Cabang = BranchID;
                    retur.Tgl_retur = DateTime.Now;
                    retur.No_ref1 = data.SP_REFF;
                    retur.No_ref2 = no;
                    retur.Kd_Customer = data.Kd_Customer;
                    retur.Kd_sales = result.FirstOrDefault().Kd_sales;
                    retur.Total_qty = result.FirstOrDefault().Total_qty;
                    retur.Nama_agen = result.FirstOrDefault().Atas_Nama;
                    retur.Keterangan = result.FirstOrDefault().Keterangan;
                    retur.jml_rp_trans = result.FirstOrDefault().JML_RP_TRANS;
                    retur.Jenis_Retur = result.FirstOrDefault().Jenis_sp;
                    retur.Tipe_trans = "JRR-KPT-01";
                    retur.flag_ppn = result.FirstOrDefault().Flag_Ppn;
                    retur.Status = "ENTRY";
                    retur.Program_Name = "frmReturDO";
                    retur.Last_Created_By = UserID;
                    retur.Last_Create_Date = DateTime.Now;
                    retur.details = new List<SALES_RETUR_D>();
                    seqNo = 0;
                    foreach (var item in result.FirstOrDefault().detailsvm)
                    {
                        var model = new SALES_RETUR_D();
                        seqNo += 1;
                        model.Kd_Cabang = BranchID;
                        model.No_retur = noret;
                        model.No_seq = seqNo;
                        model.tipe_trans = "JRR-KPT-01";
                        model.Kd_Stok = item.kode_Barang;
                        model.Nama = item.nama_Barang;
                        model.Qty = item.qty;
                        model.Kd_satuan = item.satuan;
                        model.harga = item.harga;
                        model.Status = "ENTRY";
                        model.Last_created_by = UserID;
                        model.Last_create_date = DateTime.Now;
                        model.Programe_name = "frmReturDO";
                        model.Total = item.qty * item.harga;
                        model.persediaan = 0;
                        model.qty_tarik = item.qty;
                        model.Jns_retur = retur.Jenis_Retur;
                        retur.details.Add(model);
                    }
                   // System.Threading.Thread.Sleep(2760);
                    response = await client.CallAPIPost("DO/SaveRetur", retur);

                }

                if (response.Success)
                {
                    if (data.No_sp == null)
                    {
                    
                        data.No_sp = nodo;
                    }
                    else
                    {
                        data.No_sp = data.No_sp;
                    }

                    data.Kd_Cabang = BranchID;
                    data.Last_Created_By = UserID;
                    data.Last_Create_Date = DateTime.Now;
                    data.Tipe_trans = "JPJ-KPT-01";

                    if (data.Jenis_sp == "CASH")
                    {
                        data.STATUS_DO = "TERKIRIM";
                        data.Tgl_Kirim_Marketing = DateTime.Now;
                        if (data.Atas_Nama.ToUpper() == "PLEASE SELECT")
                        {
                            data.Atas_Nama = "CASH";
                            data.Kd_Customer = "CASH";
                        }
                    }
                    else
                    {
                        data.STATUS_DO = "PERSIAPAN BARANG";
                    }
                    data.pending = "";
                    // data.Atas_Nama = "";
                    data.Departement = "2";
                    data.Status = "OK";
                    //data.Discount = 0;
                    data.Potongan = 0;
                    data.Status_Simpan = "Y";
                    data.Program_Name = "SO_SPT";

                    

                    foreach (var item in data.details)
                    {
                        item.blthn = DateTime.Now.ToString("yyyyMM");
                        item.kd_gudang = kd_gudang; // utk prc stok gudang
                        item.Kd_Cabang = data.Kd_Cabang;
                        item.tipe_trans = data.Tipe_trans;
                        item.No_sp = data.No_sp;
                        item.No_seq = (seqNo + 1).ToString();
                        item.Kd_Stok = item.kode_Barang;
                        item.Kd_satuan = item.satuan;
                        item.Last_created_by = data.Last_Created_By;
                        item.Last_create_date = data.Last_Create_Date;
                        item.Deskripsi = item.nama_Barang;
                        item.Status_Simpan = data.Status_Simpan;
                        item.STATUS_DO = data.STATUS_DO;
                        item.Programe_name = data.No_sp;
                        if (JenisUsaha == "TOKO" || akses_penjualan == "CASH")
                        {
                            item.potongan_total = item.diskon * item.Qty;
                            item.potongan = item.diskon * item.Qty;
                        }
                        else
                        {
                            item.potongan_total = item.diskon;
                            item.potongan = item.diskon;
                        }
                        if (item.flagbonus == true)
                        {
                            item.Bonus = "1";
                        }
                        else
                        {
                            item.Bonus = "0";
                        }

                        qty += item.Qty;
                        seqNo += 1;
                    }
                    data.Total_qty = qty;
                    //System.Threading.Thread.Sleep(2248);
                    response = await client.CallAPIPost("DO/Save", data);
                }




                if (response.Success && data.Jenis_sp == "CASH")
                {
                    // System.Threading.Thread.Sleep(1000);
                    //await client.CallAPIGet("DO/JurnalNota?no_sp=" + data.No_sp);
                    var invNo1 = await client.CallAPIGet("Helper/GetNoInv?refNo=" + data.No_sp);
                    string no1 = JsonConvert.DeserializeObject<string>(invNo1.Message);
                    response = await client.CallAPIGet("DO/JurnalSO?invNo=" + no1 + "&tipetrans=" + data.Tipe_trans);
                }

             

                if (response.Success)
                {
                    response.Result = data.No_sp;
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

            //TAMBAHAN BERNAD
            sif_settingUpdate = await client.CallAPIGet("Helper/GetSysSettingByKey/IsExecuted");
            IsExecutedUpdate = JsonConvert.DeserializeObject<SIF_SETTING>(sif_setting.Message);
            IsExecutedUpdate.SYSVALUE = "false";
            await client.CallAPIPost("Helper/UpdateSysSetting", IsExecutedUpdate);
          }
            return Ok(response);
        }


        public async Task<IActionResult> UpdateBO([FromBody] List<SALES_SO_D> data)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            Response response = new Response();
            Response responseinsert = new Response();
            Response responseupdate = new Response();
            Response responsecek = new Response();
            Response responsecekstokbo = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            ////foreach (MUSER_ROLE item in data)
            ////{
            ////    item.Rec_Stat = "1";
            ////    item.Nama_Pegawai = item.Nama_Pegawai;
            ////    item.Kode_Pegawai = item.Kode_Pegawai;
            ////    item.Last_Updated_By = UserID;
            ////    item.Last_Update_Date = DateTime.Now;
            ////}
            ///
            for(int i=0;i < data.Count; i++) { 
            if (data[i].alokasi > 0  )
            {
                if (data[i].tunda == true) // pending = y tidak perlu pengecekan
                {
                    try
                    {
                        response = await client.CallAPIPost("DO/UpdateBO", data);
                    }
                    catch (Exception e)
                    {

                    }
                }
                else
                {
                    string hasil = "";
                    string hasil1 = "";
                    string hasil2 = "";
                    int saldo = 0;

                    try
                    {
                        responsecekstokbo = await client.CallAPIGet("helper/CekBO?kdstok=" + data[i].Kd_Stok);
                        if (responsecekstokbo.Success)
                        {
                            hasil = JsonConvert.DeserializeObject<string>(responsecekstokbo.Message);

                            if (hasil != "0" && hasil != "")
                            {
                                // api update
                                try
                                {
                                    responseupdate = await client.CallAPIPost("DO/UpdateBO", data);
                                   // responseupdate = await client.CallAPIPost("DO/UpdateBOStokBoked", data);

                                }
                                catch (Exception h)
                                {

                                }
                            }
                            else
                            {
                                try
                                {
                                    //api insert
                                    responseinsert = await client.CallAPIPost("DO/SaveBOStokBoked", data);


                                }
                                catch (Exception u)
                                {

                                }

                            }

                            try
                            {
                                DateTime dt1 = DateTime.Now;
                                string strdate1 = dt1.ToString("yyyyMM", CultureInfo.InvariantCulture);


                                responsecek = await client.CallAPIGet("helper/CekSaldo?kdstok=" + data[i].Kd_Stok + "&bultah=" + strdate1);
                                if (responsecek.Success)
                                {
                                    hasil1 = JsonConvert.DeserializeObject<string>(responsecek.Message);
                                    hasil2 = hasil1.Replace(".", "");
                                    saldo = int.Parse(hasil2);
                                    if (saldo >= 0)
                                    {
                                        //jalankan api save
                                        try
                                        {
                                            response = await client.CallAPIPost("DO/UpdateBO", data);
                                        }
                                        catch (Exception e)
                                        {

                                        }
                                    }
                                    else
                                    {

                                        response.Success = false;
                                        response.Result = "failed";
                                        response.Message = "STOK TIDAK CUKUP";
                                    }
                                }

                            }
                            catch (Exception h)
                            {

                            }


                        }
                    }
                    catch (Exception u)
                    {
                            response.Success = false;
                            response.Result = "failed";
                            response.Message = "STOK TIDAK CUKUP";
                    }

                if (data[i].alokasi > data[i].stok)
                {
                    response.Success = false;
                    response.Result = "failed";
                    response.Message = data[i].nama_Barang.ToString() + "Stok Tidak Cukup";
                }
                }
               

            }
            else
            {
                if(data[i].alokasi <= 0)
                {
                    response.Success = false;
                    response.Result = "failed";
                    response.Message = data[i].nama_Barang.ToString() + "ALokasi Harus Lebih Dari 0";
                }
                //if (data[0].tunda == null )
                //{
                //    response.Success = false;
                //    response.Result = "failed";
                //    response.Message = data[i].nama_Barang.ToString() + "Harus Pilih Pending";
                //}
            }
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDO(FilterPURC_PO filterPO)
        {
            IEnumerable<SALES_SO> result = new List<SALES_SO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var RoleID = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            try
            { 
                if (UserID == "natali" || UserID == "hiong")
                {
                    response = await client.CallAPIGet("DO/GetByDO?no_po=" + filterPO.no_po + "&cb=" + BranchID + "&dt1=" + filterPO.DateFrom + "&dt2=" + filterPO.DateTo );
                }
                else
                {
                    response = await client.CallAPIGet("DO/GetDO?no_po=" + filterPO.no_po + "&dt1=" + filterPO.DateFrom + "&dt2=" + filterPO.DateTo + "&status_po=" + filterPO.status_po + "&cb=" + BranchID);
                }
                //response = await client.CallAPIGet("DO/GetDO?no_po=" + filterPO.no_po + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo + "&status_po=" + filterPO.status_po);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_SO>>(response.Message);

                    //if (RoleName == "PENJUALAN")
                    //{
                    //    response = await client.CallAPIGet("Helper/GetKasir");
                    //    if (response.Success)
                    //    {
                    //        var result1 = JsonConvert.DeserializeObject<List<KasirVM>>(response.Message);

                    //        var x = result1.Where(w => w.Kode_Pegawai == PegawaiID).FirstOrDefault();
                    //        if (x != null)
                    //        {
                    //           // result = result.Where(w => w.Kd_Cabang == BranchID && w.Kd_sales == x.Kode_Sales);
                    //        }
                    //    }
                    //}

                    if (result.Count() > 0)
                    {
                        result.FirstOrDefault().Tgl_spdesc = result.FirstOrDefault().Tgl_sp.ToString("dd MMMM yyyy");
                        result.FirstOrDefault().Tgl_Kirim_Marketingdesc = result.FirstOrDefault().Tgl_Kirim_Marketing.ToString("dd MMMM yyyy");
                    }
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

            ViewBag.Mode = "EDIT";
            return Ok(result);
        }
        public async Task<string> GetDOPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string barang)
        {
            List<SALES_SO> result = new List<SALES_SO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
       
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            try
            {
                response = await client.CallAPIGet("DO/GetDOPartial?skip=" + skip + "&take=" + take + "&pageSize=" + pageSize + "&page=" + page + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&sorting=" + sorting + "&filter=" + filter + "&kdcb=" + BranchID);
              


                if (response.Success)
                {

                    result = JsonConvert.DeserializeObject<List<SALES_SO>>(response.Message);
                   

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

            ViewBag.Mode = "EDIT";

            return JsonConvert.SerializeObject(new { total = response.Result, data = result });
            //  return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDO_mon(FilterPURC_PO filterPO)
        {
            IEnumerable<SALES_SO> result = new List<SALES_SO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var RoleID = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var kd_sales="";

            DateTime dt1 = filterPO.DateFrom.Value;
            DateTime dt2 = filterPO.DateTo.Value;
            string strdate1 = dt1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string strdate2 = dt2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            try
            {
                if (UserID == "natali" || UserID == "hiong")
                {
                    response = await client.CallAPIGet("DO/GetByDO?no_po=" + filterPO.no_po + "&cb=" + BranchID + "&dt1=" + strdate1 + "&dt2=" + strdate2);
                }
                else
                {
                    if (RoleName == "PENJUALAN") //|| RoleName == "SPV"
                    {
                        response = await client.CallAPIGet("Helper/GetKasir");
                        if (response.Success)
                        {
                            var result1 = JsonConvert.DeserializeObject<List<KasirVM>>(response.Message);

                            var x = result1.Where(w => w.Kode_Pegawai == PegawaiID).FirstOrDefault();
                            kd_sales = x.Kode_Sales;
                            
                        }
                    }
                    response = await client.CallAPIGet("DO/GetDO_mon?no_po=" + filterPO.no_po + "&dt1=" + strdate1 + "&dt2=" + strdate2 + "&status_po=" + kd_sales + "&cb=" + BranchID);
                }
                //response = await client.CallAPIGet("DO/GetDO?no_po=" + filterPO.no_po + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo + "&status_po=" + filterPO.status_po);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_SO>>(response.Message);

                   

                    if (result.Count() > 0)
                    {
                        result.FirstOrDefault().Tgl_spdesc = result.FirstOrDefault().Tgl_sp.ToString("dd MMMM yyyy");
                        result.FirstOrDefault().Tgl_Kirim_Marketingdesc = result.FirstOrDefault().Tgl_Kirim_Marketing.ToString("dd MMMM yyyy");
                    }
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

            ViewBag.Mode = "EDIT";
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetByDO(FilterPURC_PO filterPO)
        {
            IEnumerable<SALES_SO> result = new List<SALES_SO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;

            var RoleID = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            try
            {
                if (RoleName == "UAT")
                {
                    response = await client.CallAPIGet("DO/GetByDO?no_po=" + filterPO.no_po );
                }
                else
                {
                    response = await client.CallAPIGet("DO/GetByDO?no_po=" + filterPO.no_po + "&cb=" + BranchID);
                }
                //response = await client.CallAPIGet("DO/GetDO?no_po=" + filterPO.no_po + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo + "&status_po=" + filterPO.status_po);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_SO>>(response.Message);

                    if (RoleName == "PENJUALAN")
                    {
                        response = await client.CallAPIGet("Helper/GetKasir");
                        if (response.Success)
                        {
                            var result1 = JsonConvert.DeserializeObject<List<KasirVM>>(response.Message);

                            var x = result1.Where(w => w.Kode_Pegawai == PegawaiID).FirstOrDefault();
                            if (x != null)
                            {
                                result = result.Where(w => w.Kd_Cabang == BranchID);
                            }
                        }
                    }

                    if (result.Count() > 0)
                    {
                        result.FirstOrDefault().Tgl_spdesc = result.FirstOrDefault().Tgl_sp.ToString("dd MMMM yyyy");
                        result.FirstOrDefault().Tgl_Kirim_Marketingdesc = result.FirstOrDefault().Tgl_Kirim_Marketing.ToString("dd MMMM yyyy");
                    }
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

            ViewBag.Mode = "EDIT";
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetByCust(FilterPURC_PO filterPO)
        {
            IEnumerable<SALES_SO> result = new List<SALES_SO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;

            var RoleID = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            DateTime dt1 = filterPO.DateFrom.Value;
            DateTime dt2 = filterPO.DateTo.Value;
            string strdate1 = dt1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string strdate2 = dt2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            try
            {
                if (RoleName == "UAT")
                {
                    response = await client.CallAPIGet("DO/GetByCust?kd_cust=" + filterPO.kd_cust);
                }
                else
                {
                    response = await client.CallAPIGet("DO/GetByCust?kd_cust=" + filterPO.kd_cust + "&cb=" + BranchID + "&DateFrom=" + strdate1 + "&DateTo=" + strdate2);
                }
                //response = await client.CallAPIGet("DO/GetDO?no_po=" + filterPO.no_po + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo + "&status_po=" + filterPO.status_po);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_SO>>(response.Message);

                    if (RoleName == "PENJUALAN")
                    {
                        response = await client.CallAPIGet("Helper/GetKasir");
                        if (response.Success)
                        {
                            var result1 = JsonConvert.DeserializeObject<List<KasirVM>>(response.Message);

                            var x = result1.Where(w => w.Kode_Pegawai == PegawaiID).FirstOrDefault();
                            if (x != null)
                            {
                                result = result.Where(w => w.Kd_Cabang == BranchID);
                            }
                        }
                    }

                    if (result.Count() > 0)
                    {
                        result.FirstOrDefault().Tgl_spdesc = result.FirstOrDefault().Tgl_sp.ToString("dd MMMM yyyy");
                        result.FirstOrDefault().Tgl_Kirim_Marketingdesc = result.FirstOrDefault().Tgl_Kirim_Marketing.ToString("dd MMMM yyyy");
                    }
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

            ViewBag.Mode = "EDIT";
            return Ok(result);
        }

        //[Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        //public async Task<IActionResult> GetByStok(FilterPURC_PO filterPO)
        //{
        //    IEnumerable<SALES_SO> result = new List<SALES_SO>();
        //    ApiClient client = factoryClass.APIClientAccess();
        //    Response response = new Response();
        //    var claimsIdentity = User.Identity as ClaimsIdentity;

        //    var RoleID = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
        //    var RoleName = claimsIdentity.FindFirst("RoleName").Value;
        //    var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
        //    var BranchID = claimsIdentity.FindFirst("BranchID").Value;

        //    try
        //    {
        //        if (RoleName == "UAT")
        //        {
        //            response = await client.CallAPIGet("DO/GetByStok?kd_stok=" + filterPO.kd_stok);
        //        }
        //        else
        //        {
        //            response = await client.CallAPIGet("DO/GetByStok?kd_stok=" + filterPO.kd_stok + "&cb=" + BranchID);
        //        }
        //        //response = await client.CallAPIGet("DO/GetDO?no_po=" + filterPO.no_po + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo + "&status_po=" + filterPO.status_po);

        //        if (response.Success)
        //        {
        //            result = JsonConvert.DeserializeObject<List<SALES_SO>>(response.Message);

        //            if (RoleName == "PENJUALAN")
        //            {
        //                response = await client.CallAPIGet("Helper/GetKasir");
        //                if (response.Success)
        //                {
        //                    var result1 = JsonConvert.DeserializeObject<List<KasirVM>>(response.Message);

        //                    var x = result1.Where(w => w.Kode_Pegawai == PegawaiID).FirstOrDefault();
        //                    if (x != null)
        //                    {
        //                        result = result.Where(w => w.Kd_Cabang == BranchID);
        //                    }
        //                }
        //            }

        //            if (result.Count() > 0)
        //            {
        //                result.FirstOrDefault().Tgl_spdesc = result.FirstOrDefault().Tgl_sp.ToString("dd MMMM yyyy");
        //                result.FirstOrDefault().Tgl_Kirim_Marketingdesc = result.FirstOrDefault().Tgl_Kirim_Marketing.ToString("dd MMMM yyyy");
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        StackTrace st = new StackTrace(e, true);
        //        StackFrame frame = st.GetFrame(st.FrameCount - 1);
        //        string fileName = frame.GetFileName();
        //        string methodName = frame.GetMethod().Name;
        //        int line = frame.GetFileLineNumber();

        //        if (factoryClass.config.application != "development")
        //        {
        //            var path = Path.Combine(Startup.contentRoot, "appsettings.json");

        //            string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
        //            EmailErrorLog.SendEmail(emailbody, path);
        //        }
        //    }

        //    ViewBag.Mode = "EDIT";
        //    return Ok(result);
        //}

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDORetur(FilterPURC_PO filterPO)
        {
            IEnumerable<SALES_SO> result = new List<SALES_SO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var RoleID = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;

            try
            {
                response = await client.CallAPIGet("DO/GetDO?no_po=" + filterPO.no_po + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo + "&status_po=" + filterPO.status_po);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_SO>>(response.Message);

                    if (RoleName == "PENJUALAN")
                    {
                        response = await client.CallAPIGet("Helper/GetKasir");
                        if (response.Success)
                        {
                            var result1 = JsonConvert.DeserializeObject<List<KasirVM>>(response.Message);

                            var x = result1.Where(w => w.Kode_Pegawai == PegawaiID).FirstOrDefault();
                            if (x != null)
                            {
                                result = result.Where(w => w.Kd_Cabang == BranchID);
                            }
                        }
                    }

                    if (result.Count() > 0)
                    {
                        result.FirstOrDefault().Tgl_spdesc = result.FirstOrDefault().Tgl_sp.ToString("dd MMMM yyyy");
                        result.FirstOrDefault().Tgl_Kirim_Marketingdesc = result.FirstOrDefault().Tgl_Kirim_Marketing.ToString("dd MMMM yyyy");
                    }
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

            ViewBag.Mode = "EDIT";
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDOToko(string id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var Branch = claimsIdentity.FindFirst("Branch").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            IEnumerable<SALES_SO> result = new List<SALES_SO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            Response responseD = new Response();

            string sHTML = "";

            try
            {
                //response = await client.CallAPIGet("PURC_PO/GetPrintPO?no_po=" + id);

                //responseD = await client.CallAPIGet("PURC_PO/GetDetailPO?no_po=" + id);
                response = await client.CallAPIGet("DO/GetDO?no_po=" + id);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_SO>>(response.Message);

                    var response1 = await client.CallAPIGet("Helper/GetKasir");
                    List<KasirVM> result1 = new List<KasirVM>();
                    if (response.Success)
                    {
                        result1 = JsonConvert.DeserializeObject<List<KasirVM>>(response1.Message).Where(x => x.Kode_Pegawai == PegawaiID).ToList();

                    }

                    //head
                    sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: Dot Matrix; " +
                        " font-size: 12px; border-collapse: collapse; width: 100%;}th{border: 1px solid #dddddd; " +
                        " text-align: center; padding: 8px;}.borderGrid{border: 1px solid #dddddd; text-align: left; " +
                        " padding: 8px;}p{margin-block-start: 0em; margin-block-end: 0em; margin-bottom: 7px;}" +
                        " </style></head><body> " +
                        " <table style='margin-bottom: 2px;'> <tr style='border-bottom: black dashed 2px;'> " +
                        " <td style='width: 40%;border: 0px solid #dddddd;text-align:center;'> <p style='text-align:center;font-size:20px;font-weight:bold;'>" + Branch + "</p> " +
                        " <p>Jalan Kombes Pol M Duryat 7-9 Kota Sidoarjo</p><p>Telp: 081216327988 WA: 081216327988 </p></td></tr>" +
                        " </table> " +
                        " <table style='margin-bottom: 2px;'> <tr> " +
                        " <td style='text-align:center;font-size:20px;font-weight:bold;'>SURAT JALAN</td></tr></table> ";

                    sHTML += "<table ><tr> <td style='width: 100px;'>No</td><td >: " + id + "</td></tr>" +
                        "<tr> <td style='width: 100px;'>Tanggal</td><td >: " + result.FirstOrDefault().Last_Create_Date.ToString("dd MMM yyyy") + "</td></tr>" +
                        "<tr style='border-bottom: black dashed 2px;'> <td style='width: 100px;'>Gudang</td><td >: " + Branch + "</td></tr>" +
                        "</table>";
                    decimal totberat = 0;
                    foreach (var item in result.FirstOrDefault().detailsvm)
                    {
                        sHTML += "<table> <tr> <td style='width: 60%;'>" + item.nama_Barang + " (" + Math.Round(item.vol * item.qty, 2) + "kg)</td><td style='text-align:right;'>" + Decimal.ToInt32(item.qty) + " " + item.satuan + "</td></tr></table>";
                        totberat += Math.Round(item.vol * item.qty, 2);
                    }
                    sHTML += "<table >" +
                        "<tr style='border-top: black dashed 2px;'> " +
                        "<td style='width: 100px;'>Keterangan</td><td >: " + result.FirstOrDefault().Keterangan + "</td></tr>" +
                         "<tr style='border-bottom: black dashed 2px;'> " +
                        "<td style='width: 100px;'>Tot Berat</td><td >: " + totberat + "kg</td></tr>" +
                       "</table>";
                    sHTML += "<table style='margin-top: 10px;'> " +
                        "<tr> <td style='width: 50%;text-align:center;'>Tanda Terima</td><td style='text-align:center;'>Hormat Kami</td></tr><tr style=''> <td style='width: 50%;text-align:center;padding-top:50px;'valign='bottom'>Customer</td><td valign='bottom' style='text-align:center;'>" + result1.FirstOrDefault().Nama_Sales + "</td></tr>" +
                        "<tr> <td style='width: 50%;text-align:center;'>Mengetahui</td><td style='text-align:center;'>Dikirim</td></tr><tr style=''> <td style='width: 50%;text-align:center;padding-top:50px;'valign='bottom'>Checker</td><td valign='bottom' style='text-align:center;'>Sopir</td></tr>" +

                        "</table>";
                    //end
                    sHTML += " </body></html> ";
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


            ViewBag.Mode = "EDIT";
            return Ok(sHTML);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetIndenDO(Guid? id, string status = "", string kd_customer = "", string Kd_sales = "")
        {
            IEnumerable<SALES_BOOKED> result = new List<SALES_BOOKED>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            try
            {
                if(Kd_sales == string.Empty)
                {
                    response = await client.CallAPIGet("Helper/GetKasir");
                    if (response.Success)
                    {
                        var result1 = JsonConvert.DeserializeObject<List<KasirVM>>(response.Message);

                        var x = result1.Where(w => w.Kode_Pegawai == PegawaiID).FirstOrDefault();
                        if (x != null)
                        {
                            Kd_sales = x.Kode_Sales;
                        }
                    }
                }
                response = await client.CallAPIGet("DO/GetIndenList?id=" + id + "&status=" + status + "&kd_customer=" + kd_customer );

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_BOOKED>>(response.Message);
                    if (result.Count() > 0)
                    {
                        result.FirstOrDefault().tgl_indendesc = result.FirstOrDefault().tgl_inden.ToString("dd MMMM yyyy");
                    }
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

            ViewBag.Mode = "EDIT";
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetIndenDOCreate(Guid? id, string status = "", string kd_customer = "", string Kd_sales = "")
        {
            IEnumerable<SALES_BOOKED> result = new List<SALES_BOOKED>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            try
            {
                if (Kd_sales == string.Empty)
                {
                    response = await client.CallAPIGet("Helper/GetKasir");
                    if (response.Success)
                    {
                        var result1 = JsonConvert.DeserializeObject<List<KasirVM>>(response.Message);

                        var x = result1.Where(w => w.Kode_Pegawai == PegawaiID).FirstOrDefault();
                        if (x != null)
                        {
                            Kd_sales = x.Kode_Sales;
                        }
                    }
                }
                response = await client.CallAPIGet("DO/GetIndenCreate?id=" + id + "&status=" + status + "&kd_customer=" + kd_customer + "&Kd_sales=" + Kd_sales);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_BOOKED>>(response.Message);
                    if (result.Count() > 0)
                    {
                        result.FirstOrDefault().tgl_indendesc = result.FirstOrDefault().tgl_inden.ToString("dd MMMM yyyy");
                    }
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

            ViewBag.Mode = "EDIT";
            return Ok(result);
        }


        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDOLimit(string kd_customer = "")
        {
            IEnumerable<v_masalah> result = new List<v_masalah>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            try
            {
                
                response = await client.CallAPIGet("FIN_AR_LUNAS/GetOverdue?kd_cust=" + kd_customer );

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<v_masalah>>(response.Message);
                    
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

            ViewBag.Mode = "EDIT";
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDOKairos(string id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var Branch = claimsIdentity.FindFirst("Branch").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            IEnumerable<SALES_SO> result = new List<SALES_SO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            Response responseD = new Response();
            Response response2 = new Response();
            IEnumerable<CustomerVM> result2 = new List<CustomerVM>();

            string sHTML = "";

            try
            {
                response = await client.CallAPIGet("DO/GetDO?no_po=" + id);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_SO>>(response.Message);

                    var response1 = await client.CallAPIGet("Helper/GetKasir");
                    List<KasirVM> result1 = new List<KasirVM>();
                    if (response.Success)
                    {
                        result1 = JsonConvert.DeserializeObject<List<KasirVM>>(response1.Message).Where(x => x.Kode_Pegawai == PegawaiID).ToList();

                    }

                    response2 = await client.CallAPIGet("Helper/GetCustomer");
                    if (response2.Success)
                    {
                        result2 = JsonConvert.DeserializeObject<List<CustomerVM>>(response2.Message).Where(x => x.Kd_Customer == result.FirstOrDefault().Kd_Customer);

                    }
                    //head
                    sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: Dot Matrix; " +
                        " font-size: 12px; border-collapse: collapse; width: 100%;}th{border: 1px solid #dddddd; " +
                        " text-align: center; padding: 8px;}.borderGrid{border: 1px solid #dddddd; text-align: left; " +
                        " padding: 8px;}p{margin-block-start: 0em; margin-block-end: 0em; margin-bottom: 7px;}" +
                        " </style></head><body> " +
                        " <table style='margin-bottom: 2px;'> <tr style='border-bottom: black dashed 2px;'> " +
                        " <td style='width: 40%;border: 0px solid #dddddd;text-align:center;'> <p style='text-align:center;font-size:20px;font-weight:bold;'>" + Branch + "</p> " +
                        " <p>Jalan Kombes Pol M Duryat 7-9 Kota Sidoarjo</p><p>Telp: 081216327988 WA: 081216327988 </p></td></tr>" +
                        " </table> " +
                        " <table style='margin-bottom: 2px;'> <tr> " +
                        " <td style='text-align:center;font-weight:bold;'>DO BARANG</td></tr></table> ";

                    sHTML += "<table ><tr> <td style='width: 100px;'>No</td><td >: " + id + "</td></tr>" +
                        "<tr> <td style='width: 100px;'>Tanggal</td><td >: " + result.FirstOrDefault().Last_Create_Date.ToString("dd MMM yyyy HH:mm:ss") + "</td></tr>" +
                        "<tr> <td style='width: 100px;'>Kasir</td><td >: " + result1.FirstOrDefault().Nama_Sales + "</td></tr>" +
                        "<tr style='border-bottom: black dashed 2px;'> <td style='width: 100px;'>Gudang</td><td >: " + Branch + "</td></tr>" +
                        "</table>";

                    int totqty = 0;

                    foreach (var item in result.FirstOrDefault().detailsvm)
                    {
                        sHTML += "<table> <tr> <td style='width: 60%;'>" + item.nama_Barang + "</td><td style='text-align:right;'>" + Decimal.ToInt32(item.qty) + " " + item.satuan + "</td></tr></table>";
                        totqty += Decimal.ToInt32(item.qty);
                    }

                    sHTML += "<table style='margin-top: 10px;'><tr style='border-top: black dashed 2px;'> " +
                        "<td style='width: 100px;'>Kode Pel</td>" +
                        "<td >: " + result2.FirstOrDefault().Kd_Customer + "</td></tr><tr> " +
                        "<td style='width: 100px;'>Pelanggan</td>" +
                        "<td >: " + result2.FirstOrDefault().Nama_Customer + "</td></tr>" +
                        "<tr> <td style='width: 100px;'>No Telp</td>" +
                        "<td >: " + result2.FirstOrDefault().no_telepon1 + "</td></tr>" +
                        "<tr> <td style='width: 100px;'>Alamat</td>" +
                        "<td >: " + result2.FirstOrDefault().Alamat1 + "</td></tr>" +
                        "<tr> <td style='width: 100px;'>Tot Qty</td>" +
                        "<td >: " + totqty + "</td></tr>" +
                        "<tr> <td style='width: 100px;'>Tot Item</td>" +
                        "<td >: " + result.FirstOrDefault().detailsvm.Count() + "</td></tr>" +
                        "<tr style='border-bottom: black dashed 2px;'> <td style='width: 100px;'>Note</td>" +
                        "<td >: " + result.FirstOrDefault().Keterangan + "</td></tr></table>";

                    //end
                    sHTML += " </body></html> ";
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


            ViewBag.Mode = "EDIT";
            return Ok(sHTML);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDONota(string id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var Branch = claimsIdentity.FindFirst("Branch").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            IEnumerable<SALES_SO> result = new List<SALES_SO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            Response responseD = new Response();
            Response response2 = new Response();
            IEnumerable<CustomerVM> result2 = new List<CustomerVM>();

            string sHTML = "";

            try
            {
                response = await client.CallAPIGet("DO/GetDO?no_po=" + id);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_SO>>(response.Message);

                    var response1 = await client.CallAPIGet("Helper/GetKasir");
                    List<KasirVM> result1 = new List<KasirVM>();
                    if (response.Success)
                    {
                        result1 = JsonConvert.DeserializeObject<List<KasirVM>>(response1.Message).Where(x => x.Kode_Pegawai == PegawaiID).ToList();

                    }

                    response2 = await client.CallAPIGet("Helper/GetCustomer");
                    if (response2.Success)
                    {
                        result2 = JsonConvert.DeserializeObject<List<CustomerVM>>(response2.Message).Where(x => x.Kd_Customer == result.FirstOrDefault().Kd_Customer);

                    }
                    //head
                    sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: Dot Matrix; " +
                        " font-size: 12px; border-collapse: collapse; width: 100%;}th{border: 1px solid #dddddd; " +
                        " text-align: center; padding: 8px;}.borderGrid{border: 1px solid #dddddd; text-align: left; " +
                        " padding: 8px;}p{margin-block-start: 0em; margin-block-end: 0em; margin-bottom: 7px;}" +
                        " </style></head><body> " +
                        " <table style='margin-bottom: 2px;'> <tr style='border-bottom: black dashed 2px;'> " +
                        " <td style='width: 40%;border: 0px solid #dddddd;text-align:center;'> <p style='text-align:center;font-size:20px;font-weight:bold;'>" + Branch + "</p> " +
                        " <p>Jalan Kombes Pol M Duryat 7-9 Kota Sidoarjo</p><p>Telp: 081216327988 WA: 081216327988 </p></td></tr>" +
                        " </table> " +
                        " <table style='margin-bottom: 2px;'> <tr> " +
                        " <td style='text-align:center;font-size:20px;font-weight:bold;'>NOTA</td></tr></table> ";

                    sHTML += "<table >" +
                        "<tr> <td style='width: 100px;'>Tipe</td><td >: " + result.FirstOrDefault().Jenis_sp + "</td></tr>" +
                        "<tr> <td style='width: 100px;'>Pelanggan</td><td >: " + result.FirstOrDefault().Atas_Nama + "</td></tr>" +
                        "<tr> <td style='width: 100px;'>No</td><td >: " + id + "</td></tr>" +
                        "<tr> <td style='width: 100px;'>Tanggal</td><td >: " + result.FirstOrDefault().Last_Create_Date.ToString("dd MMM yyyy") + "</td></tr>" +
                        "<tr style='border-bottom: black dashed 2px;'> <td style='width: 100px;'>Gudang</td><td >: " + Branch + "</td></tr>" +
                        "</table>";

                    int totqty = 0;
                    int totberat = 0;
                    foreach (var item in result.FirstOrDefault().detailsvm)
                    {
                        sHTML += "<table> <tr> <td style='width: 60%;'>" + item.nama_Barang + " </td></tr><tr><td style=''>" + Decimal.ToInt32(item.qty) + " " + item.satuan + " X " + Decimal.ToInt32(item.harga).ToString("#,##0") + "</td><td style='text-align:right;'>" + Decimal.ToInt32(item.harga * item.qty).ToString("#,##0") + "</td></tr></table>";
                        totberat += Decimal.ToInt32(item.vol * item.qty);
                        totqty += Decimal.ToInt32(item.qty);
                    }

                    sHTML += "<table> " +
                        "<tr style='border-top: black dashed 2px;'> " +
                        "<td style='width: 60%;'>SUB TOTAL</td><td style='text-align:right;'>" + Decimal.ToInt32(result.FirstOrDefault().JML_RP_TRANS - result.FirstOrDefault().PPn ).ToString("#,##0") + "</td></tr>" +
                        "<tr> <td style='width: 60%;'>PPN</td><td style='text-align:right;'>" + Decimal.ToInt32(result.FirstOrDefault().PPn).ToString("#,##0") + "</td></tr>" +
                        "<tr> <td style='width: 60%;'>DISKON TOTAL</td><td style='text-align:right;'>0</td></tr>" +
                        "<tr> <td style='width: 60%;'>DP</td><td style='text-align:right;'>(" + Decimal.ToInt32(result.FirstOrDefault().dp).ToString("#,##0") + ")</td></tr>" +
                        "<tr> <td style='width: 60%;'>GRAND TOTAL</td><td style='text-align:right;'>" + Decimal.ToInt32(result.FirstOrDefault().JML_RP_TRANS - result.FirstOrDefault().dp).ToString("#,##0") + "</td></tr>" +
                        "</table>";

                    //sHTML += " <table> " +
                    //    "<tr> <td style='width: 100px;'>Tot Qty</td><td >: " + totqty + "</td></tr>" +
                    //    "<tr> <td style='width: 100px;'>Tot Item</td><td >: " + result.FirstOrDefault().detailsvm.Count() + "</td></tr>" +
                    //    "<tr> <td style='width: 100px;'>Tot Berat</td><td >: " + totberat + " kg</td></tr>" +
                    //    "</table> ";
                    sHTML += " <table> " +
                      "<tr style='border-bottom: black dashed 2px;'> <td style='width: 100px;'>Keterangan</td><td >: " + result.FirstOrDefault().Keterangan + "</td></tr>" +
                      "</table> ";
                    sHTML += "<table > " +
                   "<tr> <td style='width: 60%;text-align:center;'></td><td style='text-align:center;'>Hormat Kami</td></tr><tr style=''> <td style='width: 60%;text-align:center;padding-top:50px;'></td><td style='text-align:center;' valign='bottom'>" + result1.FirstOrDefault().Nama_Sales + "</td></tr></table>" +
                    "<table> <tr> <td style='text-align:center;'>TERIMA KASIH</td></tr><tr> <td style='text-align:center;'>Barang Yang Sudah Dibeli</td></tr><tr> <td style='text-align:center;'>Tidak Bisa Ditukar/Dikembalikan</td></tr></table>";
                    //end
                    sHTML += " </body></html> ";
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


            ViewBag.Mode = "EDIT";
            return Ok(sHTML);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> SaveCustomer(SIF_CUSTOMER data)
        {
            Response response = new Response();
            List<SALES_BOOKED> model = new List<SALES_BOOKED>();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            string no="";
            try
            {
                var retNo = await client.CallAPIGet("Helper/GetNoTrans?prefix=MCST&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                if (retNo.Success)
                {
                    no = JsonConvert.DeserializeObject<string>(retNo.Message);
                }
                data.Kd_Customer = no;
                data.Kd_Cabang = BranchID;
                data.Last_Created_By = UserID;
                data.Last_Create_Date = DateTime.Now;
                data.Rec_Stat = "Y";
                response = await client.CallAPIPost("Master/SaveCustomer", data);
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


            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDONotaNew(string id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var Branch = claimsIdentity.FindFirst("Branch").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            IEnumerable<SALES_SO> result = new List<SALES_SO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            Response responseD = new Response();
            Response response2 = new Response();
            IEnumerable<CustomerVM> result2 = new List<CustomerVM>();

            string sHTML = "";

            try
            {
                response = await client.CallAPIGet("DO/GetDO?no_po=" + id );
                if (response.Success == true)
                {
                    if (result.Count() > 0)
                    {
                        result = JsonConvert.DeserializeObject<List<SALES_SO>>(response.Message);
                        result.FirstOrDefault().branch = Branch;
                        result.FirstOrDefault().tanggaldesc = result.FirstOrDefault().Tgl_sp.ToString("dd MMM yyyy");
                    }
                  
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


            ViewBag.Mode = "EDIT";
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetKasir1()
        {
            IEnumerable<KasirVM> result = new List<KasirVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Helper/GetKasir");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<KasirVM>>(response.Message);

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

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetNotaSM(string id,string sj)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var Branch = claimsIdentity.FindFirst("Branch").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            var JenisUsaha = claimsIdentity.FindFirst("JenisUsaha").Value;
            IEnumerable<SALES_SO> result = new List<SALES_SO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            Response responseD = new Response();
            Response response2 = new Response();
            IEnumerable<CustomerVM> result2 = new List<CustomerVM>();

            string sHTML = "";

            try
            {
                response = await client.CallAPIGet("DO/GetNotaSM?no_po=" + id );
                if (response.Success == true)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_SO>>(response.Message);
                    result.FirstOrDefault().branch = Branch;
                    result.FirstOrDefault().tanggaldesc = result.FirstOrDefault().Tgl_sp.ToString("dd MMM yyyy");
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


            ViewBag.Mode = "EDIT";
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetPiutangCustomer(string kd_cust)
        {
            IEnumerable<PiutangSOVM> result = new List<PiutangSOVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("DO/GetPiutangCustomer?kd_cust=" + kd_cust);
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<PiutangSOVM>>(response.Message);
                    
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

            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDODetail(FilterPURC_PO filterPO)
        {
            IEnumerable<SALES_SO_D> result = new List<SALES_SO_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;

            var RoleID = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            DateTime dt1 = filterPO.DateFrom.Value;
            DateTime dt2 = filterPO.DateTo.Value;
            string strdate1 = dt1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string strdate2 = dt2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            try
            {
                response = await client.CallAPIGet("DO/GetDODetail?no_po=" + filterPO.no_po + "&dt1=" + strdate1 + "&dt2=" + strdate2 + "&status_po=" + filterPO.status_po + "&cb=" + BranchID);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_SO_D>>(response.Message);
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

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<string> GetBOPAKETPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime? DateFrom = null, DateTime? DateTo = null, string kd_cust = null, string no_po = null)
        {
            List<SALES_SO_D> result = new List<SALES_SO_D>();
            //IEnumerable<SALES_SO_D> result = new List<SALES_SO_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            
            var claimsIdentity = User.Identity as ClaimsIdentity;

            //var RoleID = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            //var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            //var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            //var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            //string strdate1 = "";
            //string strdate2 = "";

            //if (DateFrom != null  && DateTo != null)
            //{
            //    DateTime dt1 = DateFrom;
            //    DateTime dt2 = DateTo;
            //    strdate1 = dt1.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    strdate2 = dt2.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            //}
            

            try
            {

                response = await client.CallAPIGet("DO/GetBOPAKETPartial?skip=" + skip + "&take=" + take + "&pageSize=" + pageSize + "&page=" + page + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&sorting=" + sorting + "&filter=" + filter + "&kd_cust=" + kd_cust + "&no_po=" + no_po);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_SO_D>>(response.Message);
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

            ViewBag.Mode = "EDIT";

            return JsonConvert.SerializeObject(new { total = response.Result, data = result });
            //  return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetBOPAKET(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo=null, string status_po = null, string kd_cust = null, string cb = null)
        {
            
            IEnumerable<SALES_SO_D> result = new List<SALES_SO_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            var claimsIdentity = User.Identity as ClaimsIdentity;

            var RoleID = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            //string strdate1 = "";
            //string strdate2 = "";

            //if (DateFrom != null && DateTo != null)
            //{
            //    DateTime dt1 = DateFrom;
            //    DateTime dt2 = DateTo;
            //    strdate1 = dt1.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    strdate2 = dt2.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            //}


            try
            {

                response = await client.CallAPIGet("DO/GetBOPAKET?no_po=" + no_po + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo  + "&status_po=" + status_po + "&kd_cust=" + kd_cust + "&cb=" + BranchID);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_SO_D>>(response.Message);
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

            return Ok(result);
        }


        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Edit(SALES_SO data)
        {
            Response response = new Response();
            Response responseBooked = new Response();

            ApiClient client = factoryClass.APIClientAccess();
          //  var mode = "";
            int seqNo = 0;
            decimal qty = 0;
          //  string nodo = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var JenisUsaha = claimsIdentity.FindFirst("JenisUsaha").Value;
            var akses_penjualan = claimsIdentity.FindFirst("akses_penjualan").Value;
           
            data.Kd_Cabang = BranchID;
            response = await client.CallAPIPost("DO/CekStok", data);
            if (response.Success == true)
            {
                data.booked = new List<SALES_BOOKED>();
                string noret = "";
                // string kd_gudang = "";
                try
                {
                    //response.Success = true;

                    var response1 = await client.CallAPIGet("DO/GetDO?no_po=" + data.No_sp);

                    if (response1.Success)
                    {
                        var result = JsonConvert.DeserializeObject<List<SALES_SO>>(response1.Message).FirstOrDefault();
                        result.JML_RP_TRANS = data.JML_RP_TRANS;
                        result.JML_VALAS_TRANS = data.JML_RP_TRANS;
                        result.Discount = data.Discount;
                        result.Biaya = data.Biaya;
                        result.PPn = data.PPn;
                        result.STATUS_DO = "PERSIAPAN BARANG";
                        result.Last_Updated_By = UserID;
                        result.Last_Update_Date = DateTime.Now;
                        result.details = new List<SALES_SO_D>();
                        result.inc_ongkir = data.inc_ongkir;
                        foreach (var item in data.details.Where(x => x.kode_Barang != null && x.kode_Barang != string.Empty))
                        {
                            item.Kd_Cabang = result.Kd_Cabang;
                            item.tipe_trans = result.Tipe_trans;
                            item.No_sp = result.No_sp;
                            item.No_seq = (seqNo + 1).ToString();
                            item.Kd_Stok = item.kode_Barang;
                            item.Kd_satuan = item.satuan;
                            item.Last_created_by = result.Last_Created_By;
                            item.Last_create_date = result.Last_Create_Date;
                            item.Deskripsi = item.nama_Barang;
                            item.Status_Simpan = result.Status_Simpan;
                            item.STATUS_DO = result.STATUS_DO;

                            item.blthn = DateTime.Now.ToString("yyyyMM");

                            if (JenisUsaha == "TOKO" || akses_penjualan == "CASH")
                            {
                                item.potongan_total = item.diskon * item.Qty;
                                item.potongan = item.diskon * item.Qty;
                            }
                            else
                            {
                                item.potongan_total = item.diskon * item.Qty;
                                item.potongan = item.diskon;
                            }
                            item.Programe_name = result.No_sp;
                            if (item.flagbonus == true)
                            {
                                item.Bonus = "1";
                            }
                            else
                            {
                                item.Bonus = "0";
                            }

                            if (item.no_booked != Guid.Empty && item.no_booked != null)
                            {
                                responseBooked = await client.CallAPIGet("DO/GetInden?unix_id=" + item.no_booked);

                                if (responseBooked.Success)
                                {
                                    var result1 = JsonConvert.DeserializeObject<List<SALES_BOOKED>>(responseBooked.Message);
                                    if (result1.Count() > 0)
                                    {
                                        result1.FirstOrDefault().no_sp = result.No_sp;
                                        result1.FirstOrDefault().Status = "DO";
                                        result1.FirstOrDefault().Last_Updated_By = UserID;
                                        result1.FirstOrDefault().Last_Update_Date = DateTime.Now;

                                        data.booked.Add(result1.FirstOrDefault());
                                    }
                                }
                            }
                            result.details.Add(item);
                            qty += item.Qty;
                            seqNo += 1;
                        }
                        result.Total_qty = qty;
                        //System.Threading.Thread.Sleep(3163);
                        response = await client.CallAPIPost("DO/UpdateSO", result);
                    }

                    if (response.Success)
                    {
                        response.Result = data.No_sp;
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

            }
            return Ok(response);
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

               var ret = await client.CallAPIGet("DO/GetDO?no_po=" + data.No_sp );
                if (ret.Success)
                {
                    _so = JsonConvert.DeserializeObject<List<SALES_SO>>(ret.Message).FirstOrDefault();
                    _so.STATUS_DO = "BATAL";
                    _so.Program_Name = "Pembatalan SO";
                    //response = await client.CallAPIGet("DO/GetDODetail?no_po=" + filterPO.no_po
                    response = await client.CallAPIGet("DO/GetSOD?no_po=" + data.No_sp );

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
        #endregion

        #region "Inden"
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Inden(string id = "", string mode = "")
        {
            IEnumerable<KasirVM> result = new List<KasirVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;

            string salesID = "";

            if (RoleName == "PENJUALAN" || RoleName == "SPV")
            {
                response = await client.CallAPIGet("Helper/GetKasir");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<KasirVM>>(response.Message);

                    var x = result.Where(w => w.Kode_Pegawai == PegawaiID).FirstOrDefault();
                    if (x != null)
                    {
                        salesID = x.Kode_Sales;
                    }
                }
            }

            ViewBag.salesID = salesID;

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
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveInden(SALES_SO data)
        {
            Response response = new Response();
            List<SALES_BOOKED> model = new List<SALES_BOOKED>();
            ApiClient client = factoryClass.APIClientAccess();
            //var mode = "";
            //int seqNo = 0;
            //decimal qty = 0;
            //string nodo = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            Guid idDisplay = Guid.NewGuid();
            try
            {
                foreach (var item in data.details)
                {
                    SALES_BOOKED booked = new SALES_BOOKED();
                    booked.id = Guid.NewGuid();
                    booked.idDisplay = idDisplay;
                    booked.Kd_Cabang = BranchID;
                    booked.tgl_inden = data.Tgl_sp;
                    booked.Kd_Customer = data.Kd_Customer;
                    booked.Kd_sales = data.Kd_sales;
                    booked.Kd_Stok = item.kode_Barang;
                    booked.Nama_Barang = item.nama_Barang;
                    booked.Qty = item.Qty;
                    booked.harga = item.harga;
                    booked.total = item.Qty * item.harga;
                    booked.Kd_satuan = item.satuan;
                    booked.Keterangan = item.Keterangan;
                    booked.Status = "ENTRY";
                    booked.Last_Created_By = UserID;
                    booked.Last_Create_Date = DateTime.Now;
                    booked.dp_inden = data.dp_inden;

                    model.Add(booked);
                }

                response = await client.CallAPIPost("DO/SaveInden", model);

                if (response.Success)
                {
                    response.Result = idDisplay.ToString();
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


            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetInden(Guid? id)
        {
            IEnumerable<SALES_BOOKED> result = new List<SALES_BOOKED>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("DO/GetInden?id=" + id + "&status=ENTRY,ALOKASI");

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_BOOKED>>(response.Message);
                    if (result.Count() > 0)
                    {
                        result.FirstOrDefault().tgl_indendesc = result.FirstOrDefault().tgl_inden.ToString("dd MMMM yyyy");
                    }
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

            ViewBag.Mode = "EDIT";
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> DeleteInden(Guid id)
        {
            Response response = new Response();
            List<SALES_BOOKED> model = new List<SALES_BOOKED>();
            ApiClient client = factoryClass.APIClientAccess();
           
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            Guid idDisplay = Guid.NewGuid();
            try
            {
                response = await client.CallAPIGet("DO/GetInden?id=" + id + "&status=ENTRY,ALOKASI");

                if (response.Success)
                {
                    var result = JsonConvert.DeserializeObject<List<SALES_BOOKED>>(response.Message);
                    if (result.Count() > 0)
                    {
                        foreach (var item in result)
                        {
                            SALES_BOOKED booked = new SALES_BOOKED();
                            booked.id = item.id;
                            booked.idDisplay = item.idDisplay;
                            booked.Kd_Cabang = BranchID;
                            booked.tgl_inden = item.tgl_inden;
                            booked.Kd_Customer = item.Kd_Customer;
                            booked.Kd_sales = item.Kd_sales;
                            booked.Kd_Stok = item.Kd_Stok;
                            booked.Nama_Barang = item.Nama_Barang;
                            booked.Qty = item.Qty;
                            booked.harga = item.harga;
                            booked.total = item.Qty * item.harga;
                            booked.Kd_satuan = item.Kd_satuan;
                            booked.Keterangan = item.Keterangan;
                            booked.Status = "DELETE";
                            booked.Last_Updated_By = UserID;
                            booked.Last_Update_Date = DateTime.Now;
                            booked.no_sp = item.no_sp;

                            model.Add(booked);
                        }
                    }

                    response = await client.CallAPIPost("DO/UpdateInden", model);

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


            return Ok(response);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> EditInden(SALES_SO data)
        {
            Response response = new Response();
            List<SALES_BOOKED> model = new List<SALES_BOOKED>();
            ApiClient client = factoryClass.APIClientAccess();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            Guid idDisplay = Guid.NewGuid();
            try
            {
                response = await client.CallAPIGet("DO/GetInden?id=" + data.No_sp);

                if (response.Success)
                {
                    var result = JsonConvert.DeserializeObject<List<SALES_BOOKED>>(response.Message);
                    if (result.Count() > 0)
                    {
                        foreach (var item in result)
                        {
                            SALES_BOOKED booked = new SALES_BOOKED();
                            booked.id = item.id;
                            booked.idDisplay = item.idDisplay;
                            booked.Kd_Cabang = BranchID;
                            booked.tgl_inden = item.tgl_inden;
                            booked.Kd_Customer = item.Kd_Customer;
                            booked.Kd_sales = item.Kd_sales;
                            booked.Kd_Stok = item.Kd_Stok;
                            booked.Nama_Barang = item.Nama_Barang;
                            booked.Qty = item.Qty;
                            booked.harga = item.harga;
                            booked.total = item.Qty * item.harga;
                            booked.Kd_satuan = item.Kd_satuan;
                            booked.Keterangan = item.Keterangan;
                            booked.Status = "DELETE";
                            booked.Last_Updated_By = UserID;
                            booked.Last_Update_Date = DateTime.Now;
                            booked.no_sp = item.no_sp;
                            booked.dp_inden = item.dp_inden;

                            model.Add(booked);
                        }
                    }
                    response = await client.CallAPIPost("DO/UpdateInden", model);
                    model = new List<SALES_BOOKED>();
                    foreach (var item in data.details)
                    {
                        SALES_BOOKED booked = new SALES_BOOKED();
                        booked.id = Guid.NewGuid();
                        booked.idDisplay = idDisplay;
                        booked.Kd_Cabang = BranchID;
                        booked.tgl_inden = data.Tgl_sp;
                        booked.Kd_Customer = data.Kd_Customer;
                        booked.Kd_sales = data.Kd_sales;
                        booked.Kd_Stok = item.kode_Barang;
                        booked.Nama_Barang = item.nama_Barang;
                        booked.Qty = item.Qty;
                        booked.harga = item.harga;
                        booked.total = item.Qty * item.harga;
                        booked.Kd_satuan = item.satuan;
                        booked.Keterangan = item.Keterangan;
                        booked.Status = "ENTRY";
                        booked.Last_Created_By = UserID;
                        booked.Last_Create_Date = DateTime.Now;
                        booked.dp_inden = data.dp_inden;

                        model.Add(booked);
                    }

                    response = await client.CallAPIPost("DO/SaveInden", model);

                    if (response.Success)
                    {
                        response.Result = idDisplay.ToString();
                    }
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


            return Ok(response);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDPInden(string id)
        {
            IEnumerable<SALES_BOOKED> result = new List<SALES_BOOKED>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("DO/GetDPInden?id=" + id);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_BOOKED>>(response.Message);
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

            ViewBag.Mode = "EDIT";
            return Ok(result);
        }

        #endregion

        #region "Alokasi"
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Alokasi()
        {
            IEnumerable<KasirVM> result = new List<KasirVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;

            string salesID = "";

            return View();
        }


        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> alokasi_BO()
        {
            IEnumerable<KasirVM> result = new List<KasirVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;

            string salesID = "";

            return View();
        }

        public async Task<IActionResult> SaveAlokasi([FromBody] List<SALES_BOOKED> data)
        {
            Response response = new Response();
            List<SALES_BOOKED> model = new List<SALES_BOOKED>();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            try
            {
                foreach (var item in data)
                {
                    response = await client.CallAPIGet("DO/GetInden?unix_id=" + item.id);

                    if (response.Success)
                    {
                        var result = JsonConvert.DeserializeObject<List<SALES_BOOKED>>(response.Message);
                        if (result.Count() > 0)
                        {
                            foreach (var itemx in result)
                            {
                                SALES_BOOKED booked = new SALES_BOOKED();
                                booked.id = itemx.id;
                                booked.idDisplay = itemx.idDisplay;
                                booked.Kd_Cabang = BranchID;
                                booked.tgl_inden = itemx.tgl_inden;
                                booked.Kd_Customer = itemx.Kd_Customer;
                                booked.Kd_sales = itemx.Kd_sales;
                                booked.Kd_Stok = itemx.Kd_Stok;
                                booked.Nama_Barang = itemx.Nama_Barang;
                                booked.Qty = itemx.Qty;
                                booked.qty_Alokasi = item.qty_Alokasi;
                                booked.harga = itemx.harga;
                                booked.total = item.qty_Alokasi * itemx.harga;
                                booked.Kd_satuan = itemx.Kd_satuan;
                                booked.Keterangan = itemx.Keterangan;
                                booked.Last_Updated_By = UserID;
                                booked.Last_Update_Date = DateTime.Now;
                                booked.no_sp = itemx.no_sp;
                                booked.Status = "ALOKASI";
                                booked.alokasiLevel = "A";

                                model.Add(booked);
                            }
                        }
                    }
                }
                response = await client.CallAPIPost("DO/SaveAlokasi", model);

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


            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetIndenAlokasi()
        {
            IEnumerable<SALES_BOOKED> result = new List<SALES_BOOKED>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("DO/GetInden");

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_BOOKED>>(response.Message);
                    if (result.Count() > 0)
                    {
                        result.FirstOrDefault().tgl_indendesc = result.FirstOrDefault().tgl_inden.ToString("dd MMMM yyyy");
                    }
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

            ViewBag.Mode = "EDIT";
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetAlokasiBO()
        {
            IEnumerable<SALES_BOOKED> result = new List<SALES_BOOKED>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("DO/GetInden");

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SALES_BOOKED>>(response.Message);
                    if (result.Count() > 0)
                    {
                        result.FirstOrDefault().tgl_indendesc = result.FirstOrDefault().tgl_inden.ToString("dd MMMM yyyy");
                    }
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

            ViewBag.Mode = "EDIT";
            return Ok(result);
        }
        #endregion
    }
}