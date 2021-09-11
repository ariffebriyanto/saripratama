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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IFA.Web.Controllers
{
    public class BookingPaket : BaseController
    {
        public BookingPaket(FactoryClass factoryClass,
          IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        #region "Monitoring"
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetPaketMontoring(string id = null)
        {
            IEnumerable<SIF_BOOKING_PAKET> result = new List<SIF_BOOKING_PAKET>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("BookingPaket/GetPaket");

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SIF_BOOKING_PAKET>>(response.Message);
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

        #region "Create"
        public IActionResult Create(string id = "", string mode = "")
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

        public async Task<IActionResult> GetHargaBarang()
        {
            IEnumerable<BarangHargaVM> result = new List<BarangHargaVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            try
            {
                response = await client.CallAPIGet("Helper/GetHargaBarang?kdcabang=" + BranchID);
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<BarangHargaVM>>(response.Message);
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

        public async Task<IActionResult> Save(SIF_BOOKING_PAKET info)
        {
            Response response = new Response();

            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            int seq = 0;
            SIF_BOOKING_PAKET data = new SIF_BOOKING_PAKET();

            string nopaket = "";

            try
            {
                decimal totalQty = 0;
                decimal totalHarga = 0;

                data.Kd_Cabang = BranchID;
                data.No_Paket = info.No_Paket;
                data.Tgl_Paket = info.Tgl_Paket;
                data.Nama_Paket = info.Nama_Paket;
                data.Tgl_Akhir_Paket = info.Tgl_Akhir_Paket;
                data.Status_Aktif = info.Status_Aktif;

                data.Departement = "2";

                data.Program_Name = "FrmMasterBookingPaket";
                data.Biaya = 0;
                data.Status = "OK";

                if (info.No_Paket != "" && info.No_Paket != null)
                {
                    data.Last_Update_Date = DateTime.Now;
                    data.Last_Updated_By = UserID;
                    data.Last_Create_Date = DateTime.Now;
                    data.Last_Created_By = UserID;
                }
                else
                {
                    data.Last_Create_Date = DateTime.Now;
                    data.Last_Created_By = UserID;
                }
                data.details = new List<SIF_BOOKING_PAKET_D>();
                foreach (var item in info.details)
                {
                    if (item.Kd_Stok != "" && item.Kd_Stok != null)
                    {
                        SIF_BOOKING_PAKET_D model = new SIF_BOOKING_PAKET_D();
                        model.No_Paket = data.No_Paket;
                        model.Kd_Cabang = data.Kd_Cabang;
                        model.No_seq = (seq + 1).ToString();
                        model.Kd_Stok = item.Kd_Stok;
                        model.Qty = item.Qty;
                        model.harga = item.harga;
                        model.Kd_satuan = item.Kd_satuan;
                        model.Keterangan = item.Keterangan;
                        model.departemen = "2";
                        model.Last_create_date = DateTime.Now;
                        model.Last_created_by = UserID;
                        model.Programe_name = "FrmMasterBookingPaket";
                        model.Deskripsi = item.Deskripsi;
                        totalQty += item.Qty;
                        totalHarga += item.Qty * item.harga;
                        seq += 1;
                        data.details.Add(model);
                    }

                }
                data.Harga_Paket = totalHarga;
                data.Total_qty = totalQty;

                response = await client.CallAPIPost("BookingPaket/Save", data);
                nopaket = response.Message;
                if (response.Success)
                {
                    response.Result = nopaket;
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

        public async Task<IActionResult> GetPaket(string id = null)
        {
            SIF_BOOKING_PAKET result = new SIF_BOOKING_PAKET();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("BookingPaket/GetPaketbyID?id=" + id);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<SIF_BOOKING_PAKET>(response.Message);
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
