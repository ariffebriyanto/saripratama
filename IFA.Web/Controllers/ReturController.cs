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

namespace IFA.Web.Controllers
{
    public class ReturController : BaseController
    {
        public ReturController(FactoryClass factoryClass,
        IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult Create(string id = "", string mode = "")
        {
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;

            if (id == "")
            {
                ViewBag.Mode = "NEW";
            }
            else
            {
                ViewBag.Mode = mode;
                ViewBag.Id = id;
            }
            ViewBag.BranchID = BranchID;
            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult Index(string id = "", string mode = "")
        {
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;

            if (id == "")
            {
                ViewBag.Mode = "NEW";
            }
            else
            {
                ViewBag.Mode = mode;
                ViewBag.Id = id;
            }
            ViewBag.BranchID = BranchID;
            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetReturInv()
        {
            IEnumerable<InvRetur> result = new List<InvRetur>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            
            try
            {
                response = await client.CallAPIGet("Retur/GetReturInv?kd_cabang=" + BranchID);
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<InvRetur>>(response.Message);
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
        public async Task<IActionResult> GetDODetails(string id)
        {
            IEnumerable<InvReturDtl> result = new List<InvReturDtl>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Retur/GetDODetails?id=" + id);
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<InvReturDtl>>(response.Message);
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
        public async Task<IActionResult> Save(List<InvReturDtl> data)
        {
            Response response = new Response();
            Response responseBooked = new Response();
            IEnumerable<KasirVM> result = new List<KasirVM>();
            ApiClient client = factoryClass.APIClientAccess();
            var mode = "";
            int seqNo = 0;
            decimal qty = 0;
            string nodo = "";
            string noret = "";
            string salesID = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            SALES_RETUR retur = new SALES_RETUR();
            try
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

                var retNo = await client.CallAPIGet("Helper/GetNoTrans?prefix=SPRTR&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                if (retNo.Success)
                {
                    noret = JsonConvert.DeserializeObject<string>(retNo.Message);
                }
                retur.No_retur = noret;
                retur.Kd_Cabang = BranchID;
                retur.Tgl_retur = DateTime.Now;
                retur.No_ref1 = data.FirstOrDefault().No_sp;
                retur.No_ref2 = data.FirstOrDefault().no_inv;
                retur.Kd_Customer = data.FirstOrDefault().Kd_Customer;
                retur.Kd_sales = salesID;
                retur.Total_qty = data.Sum(s=> s.qty_retur);
                retur.Nama_agen = data.FirstOrDefault().Atas_Nama;
               // retur.Keterangan = result.FirstOrDefault().Keterangan;
                retur.jml_rp_trans = data.Sum(s=>s.retur_total);
                retur.Jenis_Retur = data.FirstOrDefault().Jenis_sp;
                retur.Tipe_trans = "JRR-KPT-01";
                retur.flag_ppn = "Y";
                retur.Status = "ENTRY";
                retur.Program_Name = "frmReturDO";
                retur.Last_Created_By = UserID;
                retur.Last_Create_Date = DateTime.Now;
                retur.details = new List<SALES_RETUR_D>();
                seqNo = 0;
                foreach (var item in data)
                {
                    var model = new SALES_RETUR_D();
                    seqNo += 1;
                    model.Kd_Cabang = BranchID;
                    model.No_retur = noret;
                    model.No_seq = seqNo;
                    model.tipe_trans = "JRR-KPT-01";
                    model.Kd_Stok = item.Kd_Stok;
                    model.Nama = item.Nama_Barang;
                    model.Qty = item.Qty;
                    model.Kd_satuan = item.Kd_satuan;
                    model.harga = item.harga;
                    model.Status = "ENTRY";
                    model.Last_created_by = UserID;
                    model.Last_create_date = DateTime.Now;
                    model.Programe_name = "frmReturDO";
                    model.Total = item.qty_retur * item.harga;
                    model.persediaan = 0;
                    model.qty_tarik = item.qty_retur;
                    model.Jns_retur = item.Jenis_sp;
                    retur.details.Add(model);
                }
                response = await client.CallAPIPost("DO/SaveRetur", retur);

                //var invNo1 = await client.CallAPIGet("Helper/GetNoInv?refNo=" + retur.No_retur);
                //string no1 = JsonConvert.DeserializeObject<string>(invNo1.Message);
                response = await client.CallAPIGet("DO/JurnalSO?invNo=" + noret + "&tipetrans=" + retur.Tipe_trans);

                if (response.Success)
                {
                    response.Result = retur.No_retur;
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
        public async Task<IActionResult> Save2(SALES_RETUR retur)
        {
            Response response = new Response();
            Response responseBooked = new Response();
            IEnumerable<KasirVM> result = new List<KasirVM>();
            ApiClient client = factoryClass.APIClientAccess();
            var mode = "";
            int seqNo = 0;
            decimal qty = 0;
            string nodo = "";
            string noret = "";
            string salesID = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
     //       SALES_RETUR retur = new SALES_RETUR();
            try
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

                var retNo = await client.CallAPIGet("Helper/GetNoTrans?prefix=SPRTR&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                if (retNo.Success)
                {
                    noret = JsonConvert.DeserializeObject<string>(retNo.Message);
                }
                retur.No_retur = noret;
                retur.Kd_Cabang = BranchID;
                retur.Tgl_retur = DateTime.Now;
                retur.Tgl_tarik = DateTime.Now;
                retur.Total_qty = retur.details.Sum(x => x.qty_tarik);
                retur.Tipe_trans = "JRR-KPT-01";
                retur.flag_ppn = "Y";
                retur.Status = "ENTRY";
                retur.Program_Name = "frmReturDO";
                retur.Jenis_Retur = "RETUR NON CASH";
                retur.Last_Created_By = UserID;
                retur.Last_Create_Date = DateTime.Now;
               // retur.details = new List<SALES_RETUR_D>();
                seqNo = 0;
                foreach (var item in retur.details)
                {
                    //var model = new SALES_RETUR_D();
                    seqNo += 1;
                    item.Kd_Cabang = BranchID;
                    item.No_retur = noret;
                    item.No_seq = seqNo;
                    item.tipe_trans = "JRR-KPT-01";
                    item.Status = "ENTRY";
                    item.Programe_name = "frmReturDO";
                    item.Jns_retur = "RETUR NON CASH";
                    item.persediaan = item.harga;
                    item.Keterangan = "RETUR NON CASH";
                   // item. = 0;
                    //model.Kd_Stok = item.Kd_Stok;
                    //model.Nama = item.Nama;
                    item.qty_nota = item.Qty;
                    //model.Kd_satuan = item.Kd_satuan;
                    //model.harga = item.harga;

                    item.Last_created_by = UserID;
                    item.Last_create_date = DateTime.Now;

                    item.Total = item.qty_tarik * item.harga;

                    item.qty_tarik = item.qty_tarik;
                    item.Jns_retur = item.Jns_retur;
                   // retur.details.Add(model);
                }
                response = await client.CallAPIPost("DO/SaveRetur", retur);

                //var invNo1 = await client.CallAPIGet("Helper/GetNoInv?refNo=" + retur.No_retur);
                //string no1 = JsonConvert.DeserializeObject<string>(invNo1.Message);
                response = await client.CallAPIGet("DO/JurnalSO?invNo=" + noret + "&tipetrans=" + retur.Tipe_trans);

                if (response.Success)
                {
                    response.Result = retur.No_retur;
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
        public async Task<IActionResult> GetMonRetur(string id, DateTime? DateFrom = null, DateTime? DateTo = null, string barang = null)
        {
            IEnumerable<SALES_RETUR> result = new List<SALES_RETUR>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var kdcabang = claimsIdentity.FindFirst("BranchID").Value;

            response = await client.CallAPIGet("RETUR/GetMonRetur?kdcb=" + kdcabang + "&no_trans =" + id + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&kd_stok=" + barang);


            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SALES_RETUR>>(response.Message);


            }
            //ViewBag.Mode = "EDIT";
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetMonReturDetil(string id, DateTime? DateFrom = null, DateTime? DateTo = null, string barang = null)
        {
            IEnumerable<SALES_RETUR_D> result = new List<SALES_RETUR_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var kdcabang = claimsIdentity.FindFirst("BranchID").Value;

            response = await client.CallAPIGet("RETUR/GetMonReturD?kdcb=" + kdcabang + "&no_trans =" + id + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&kd_stok=" + barang);


            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SALES_RETUR_D>>(response.Message);


            }
            //ViewBag.Mode = "EDIT";
            return Ok(result);
        }
    }
}