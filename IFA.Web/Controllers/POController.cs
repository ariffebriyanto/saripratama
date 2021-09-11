using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

using ERP.Domain.Base;
using ERP.Web;
using ERP.Web.Controllers;
using ERP.Web.Utils;
using IFA.Domain.Helpers;
using IFA.Domain.Models;
using IFA.Domain.Utils;
using IFA.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Table;
//kajdkajdkjasdksadasdasd

namespace IFA.Web.Controllers
{
    public class POController : BaseController
    {
        public POController(FactoryClass factoryClass,
           IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> Create(string id = "", string mode = "")
        {
          
            PURC_PO po = new PURC_PO();
            ApiClient client = factoryClass.APIClientAccess();
            List<SIF_Supplier> listSatuan = new List<SIF_Supplier>();
            List<SIF_Satuan> listSupplier = new List<SIF_Satuan>();

            Response response = new Response();
            try {

                response = await client.CallAPIGet("SIF_Supplier/GetSIF_Supplier");

                if (response.Success)
                {
                    ViewBag.Supplier = response.Message;

                }
                response = null;
                response = await client.CallAPIGet("SIF_Satuan/GetSIFSatuanCbo");
                if (response.Success)
                {
                    ViewBag.Satuan = response.Message;

                }
                response = null;
                response = await client.CallAPIGet("SIF_Barang/GetSIFBarangCbo");
                if (response.Success)
                {
                    ViewBag.Barang = response.Message;

                }
                if (id == "")
                {
                    ViewBag.Mode = "NEW";
                }
                else
                {
                    ViewBag.Mode = mode;
                    ViewBag.Id = id;
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
            
            return View(po);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetPO(FilterPURC_PO filterPO)
        {
            IEnumerable<PURC_PO> result = new List<PURC_PO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                //  response = await client.CallAPIGet("PURC_PO/GetPO?no_po=" + filterPO.no_po + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo + "&status_po=" + filterPO.status_po + "&barang=" + filterPO.barang);
                  response = await client.CallAPIGet("PURC_PO/GetPO?no_po=" + filterPO.no_po + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo + "&status_po=" + filterPO.status_po + "&barang=" + filterPO.barang);


                if (response.Success)
                {

                    result = JsonConvert.DeserializeObject<List<PURC_PO>>(response.Message);
                    if (result.Count() > 0)
                    {
                        result.FirstOrDefault().tgl_podesc = result.FirstOrDefault().tgl_po.ToString("dd MMMM yyyy");
                        result.FirstOrDefault().tgl_kirimdesc = result.FirstOrDefault().tgl_kirim.ToString("dd MMMM yyyy");
                        result.FirstOrDefault().tgl_jth_tempodesc = result.FirstOrDefault().tgl_jth_tempo.ToString("dd MMMM yyyy");
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
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetPODetail(FilterPURC_PO filterPO)
        {
            IEnumerable<PURC_PO_D> result = new List<PURC_PO_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("PURC_PO/GetDetailPO?no_po=" + filterPO.no_po + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo + "&status_po=" + filterPO.status_po + "&barang=" + filterPO.barang);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<PURC_PO_D>>(response.Message);
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
        public async Task<IActionResult> SaveCustomer(SIF_CUSTOMER data)
        {
            Response response = new Response();
            List<SALES_BOOKED> model = new List<SALES_BOOKED>();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            string no = "";
            try
            {
                var retNo = await client.CallAPIGet("Helper/GetNoTrans?prefix=MSUP&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                if (retNo.Success)
                {
                    no = JsonConvert.DeserializeObject<string>(retNo.Message);
                }
                data.Kd_Customer = no;
                data.Kd_Cabang = BranchID;
                data.Last_Created_By = UserID;
                data.Last_Create_Date = DateTime.Now;
                data.Rec_Stat = "Y";
                response = await client.CallAPIPost("Master/SaveSupplier", data);
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

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SavePO(PURC_PO data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var mode = "";
            string nopo = "";
            data.listdpm = new List<PURC_DPM_D>();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            try
            {
                if (data.no_po == null)
                {
                    mode = "NEW";
                    response = await client.CallAPIGet("Helper/GetNoTrans?prefix=PO-MP&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                    if (response.Success)
                    {
                        nopo = JsonConvert.DeserializeObject<string>(response.Message);
                    }
                    else
                    {
                        response = await client.CallAPIGet("Helper/GetNoTransx?prefix=PO-MP&kdcabang=" + BranchID);
                        nopo = JsonConvert.DeserializeObject<string>(response.Message);
                    }
                    data.no_po = nopo;
                }
                else
                {
                    nopo = data.no_po;
                }


                data.Kd_Cabang = BranchID;

                data.tipe_trans = "JPP-KUT-01";
                data.total = data.jml_rp_trans;
                data.jml_rp_trans = data.jml_rp_trans - data.jml_ppn - data.ongkir;
                data.isClosed = "T";
                data.status_po = "ENTRY";
                data.rec_stat = "ENTRY";
                data.qty_total = data.podetail.Count();
                data.tgl_jth_tempo = DateTime.Now.AddDays(data.lama_bayar);
                for (int i = 0; i <= data.podetail.Count() - 1; i++)
                {
                    data.podetail[i].no_po = data.no_po;
                    data.podetail[i].Kd_Cabang = data.Kd_Cabang;
                    data.podetail[i].tipe_trans = data.tipe_trans;
                    data.podetail[i].no_seq = i + 1;
                    data.podetail[i].tgl_kirim = data.tgl_kirim;
                    data.podetail[i].qty_kirim = data.podetail[i].qty;
                    data.podetail[i].inv_stat = 0;
                    if(data.podetail[i].Bonus.ToUpper() == "TRUE")
                    {
                        data.podetail[i].flag_bonus = "Y";
                        data.podetail[i].Bonus = "Y";
                    }
                    else
                    {
                        data.podetail[i].flag_bonus = "T";
                        data.podetail[i].Bonus = "T";
                    }
                 
                    data.podetail[i].harga_new = data.podetail[i].harga;
                    data.podetail[i].diskon4_new = data.podetail[i].Diskon4;
                    data.podetail[i].jml_diskon = data.podetail[i].harga * data.podetail[i].qty - data.podetail[i].total;

                    data.podetail[i].diskon1_new = data.podetail[i].prosen_diskon;
                    data.podetail[i].diskon2_new = data.podetail[i].diskon2;
                    data.podetail[i].diskon3_new = data.podetail[i].diskon3;
                    data.podetail[i].diskon4_new = data.podetail[i].Diskon4;
                    data.podetail[i].total_new = data.podetail[i].total;
                    data.podetail[i].qty_sisa = 0;
                    data.podetail[i].jml_diskon_new = data.podetail[i].jml_diskon;
                    data.podetail[i].Last_Created_By = UserID;
                    data.podetail[i].Last_Create_Date = DateTime.Now;
                    if (mode == "NEW")
                    {
                        if (data.podetail[i].pdm != null)
                        {
                            string[] pdmSplit = data.podetail[i].pdm.Split(";");
                            for (int j = 0; j <= pdmSplit.Length - 1; j++)
                            {
                                if (pdmSplit[j] != "")
                                {
                                    PURC_DPM_D dpm = new PURC_DPM_D();
                                    dpm.no_po = nopo;
                                    dpm.No_DPM = pdmSplit[j];
                                    dpm.Last_Updated_By = UserID;
                                    dpm.Last_Update_Date = DateTime.Now;
                                    data.listdpm.Add(dpm);
                                }
                            }
                        }
                    }
                }
                if (mode == "NEW")
                {
                    data.Last_Created_By = UserID;
                    data.Last_Create_Date = DateTime.Now;
                    response = await client.CallAPIPost("PURC_PO/SavePO", data);
                }
                else
                {
                    data.Last_Updated_By = UserID;
                    data.Last_Update_Date = DateTime.Now;
                    response = await client.CallAPIPost("PURC_PO/UpdatePO", data);
                }
                if (response.Success)
                {
                    response.Result = nopo;
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
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public IActionResult Approval()
        {
            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetApproval(FilterPURC_PO filterPO)
        {
            IEnumerable<PURC_PO> result = new List<PURC_PO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
               
                response = await client.CallAPIGet("PURC_PO/GetApprovalPO?no_po=" + filterPO.no_po + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo + "&status_po=" + filterPO.status_po);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<PURC_PO>>(response.Message);
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
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetDetailApprovalPO(FilterPURC_PO filterPO)
        {
            IEnumerable<PURC_PO_D> result = new List<PURC_PO_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("PURC_PO/GetDetailApprovalPO?no_po=" + filterPO.no_po + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo + "&status_po=" + filterPO.status_po);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<PURC_PO_D>>(response.Message);
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

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetPrintPO(string id, string kd_barang = null)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            PrintPO result = new PrintPO();
            IEnumerable<PURC_PO_D> resultD = new List<PURC_PO_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            Response responseD = new Response();

            string sHTML = "";

            try
            {
                if(kd_barang != null)
                {
                    var detail = await client.CallAPIGet("PURC_PO/GetLastPO?kd_barang=" + kd_barang);
                    var resultX = JsonConvert.DeserializeObject<PURC_PO_D>(detail.Message);
                    id = resultX.no_po;
                }

                response = await client.CallAPIGet("PURC_PO/GetPrintPO?no_po=" + id);

                responseD = await client.CallAPIGet("PURC_PO/GetDetailPO?no_po=" + id);


                if (response.Success && responseD.Success)
                {
                    result = JsonConvert.DeserializeObject<PrintPO>(response.Message);
                    resultD = JsonConvert.DeserializeObject<List<PURC_PO_D>>(responseD.Message);

                    //head
                    sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}td, th{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}} .noBorder{border: 0px solid #dddddd;}.paddingtd{padding: 0;padding-bottom:2px;padding-left:8px;padding-top:2px;}.paddingth{padding: 0;padding-bottom:8px;padding-left:8px;}.textCenter{text-align: center;}.textRight{text-align: right;}</style></head> ";

                    //company profile
                    sHTML += " <body><table style='margin-bottom: 20px; '><tr ><td style='width:40%; border: 0px solid #dddddd;' ><h2>" + result.Cabang + "</h2><p>&nbsp;</p><p>&nbsp; </p><p>&nbsp; </p></td><td style='width: 20%;border: 0px solid #dddddd;'></td><td style='width: 40%;border: 0px solid #dddddd;'><h2>PURCHASE ORDER</h2><p>PO No: " + result.PUNumber + "</p><p>Tanggal PO: " + result.TanggalPO + "</p><p>PO Status: " + result.StatusPO + "</p></td></tr> ";

                    //supplier profile
                    sHTML += " <tr class='headerTable '> <th style='width:40%;' class='noBorder '> SUPPLIER </th> <th style='width: 20%;' class='noBorder '></th> <th style='width: 40%;' class='noBorder '> ALAMAT PENGIRIMAN </th> </tr><tr > <td style='width:40%;' class='noBorder paddingtd'> " + result.NamaSupplier + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> " + result.AlamatPengiriman + " </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> " + result.AlamatSupplier + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> Telp No: " + result.TelpSupplier + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> Jatuh Tempo: " + result.TanggalJatuhTempo + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr></table> ";

                    //UserDetail
                    sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='noBorder'> TANGGAL KIRIM </th> <th class='noBorder'>REQUESTED BY</th> <th class='noBorder'> APPROVED BY </th> </tr><tr> <td class='noBorder paddingtd'> " + result.TanggalKirim + " </td><td class='noBorder paddingtd'> " + result.RequestBy + " </td><td class='noBorder paddingtd'> " + result.ApprovedBy + " </td></tr></table> ";

                    //Notes
                    sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='noBorder'> NOTES </th> </tr><tr> <td class='noBorder paddingtd'> " + result.Notes + " </td></tr></table> ";

                    //headerDetail
                    sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='textCenter'> BARANG </th> <th class='textCenter'>SATUAN</th> <th class='textCenter'> QTY </th> <th class='textCenter'> HARGA </th> <th class='textCenter'> DISC %1 </th><th class='textCenter'> DISC %2 </th><th class='textCenter'> DISC %3 </th><th class='textCenter'> DISC (Rp) </th> <th class='textCenter'> TOTAL </th> </tr> ";

                    //detail
                    foreach (PURC_PO_D detail in resultD)
                    {
                        sHTML += " <tr> <td> " + detail.nama_barang + " </td><td class='textCenter'>" + detail.satuan + "</td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.qty) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.harga) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.prosen_diskon) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.diskon2) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.diskon3) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.Diskon4) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.total) + " </td></tr> ";
                    }

                    //total
                    sHTML += " <tr> <td class='textRight noBorder' colspan='8'> SUBTOTAL </td><td class='textRight'> " + string.Format("{0:#,0.00}", result.SubTotal) + "</td></tr><tr> <td class='textRight noBorder' colspan='8'> ONGKOS KIRIM </td><td class='textRight'> " + string.Format("{0:#,0.00}", result.Ongkir) + "</td></tr><tr> <td class='textRight noBorder' colspan='8'> PPN </td><td class='textRight'> " + string.Format("{0:#,0.00}", result.PPN) + "</td></tr><tr> <th class='textRight noBorder' colspan='8'> GRAND TOTAL </th> <th class='textRight'> " + string.Format("{0:#,0.00}", result.GrandTotal) + "</th> </tr> ";

                    //endDetail
                    sHTML += " </table> ";

                    // ttd
                    sHTML += "<tr> <table style='margin-bottom: 20px;'> <tr> <th  class='textCenter noBorder '> Dibuat </th> <th  class='textCenter noBorder '> Diperiksa </th> <th class='textCenter noBorder '> Supplier </th></tr> ";
                    sHTML += " <tr> <td class='noBorder paddingtd'> <pre> </td><td class='noBorder paddingtd'> <pre>  </td><td class='noBorder paddingtd'><pre>  </td> ";
                    sHTML += " <tr> <td  class='textCenter noBorder paddingtd'>(" + UserID + ")</td><td  class='textCenter noBorder paddingtd'><pre>(           ) </td><td  class='textCenter noBorder paddingtd'> <pre>(           ) </td> ";
                    sHTML += "</table >";
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

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetPrintAkunting(string id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            PrintPO result = new PrintPO();
            IEnumerable<PURC_PO_D> resultD = new List<PURC_PO_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            Response responseD = new Response();

            string sHTML = "";

            try
            {
                response = await client.CallAPIGet("PURC_PO/GetPrintPO?no_po=" + id);

                responseD = await client.CallAPIGet("PURC_PO/GetPODtlCbg?no_po=" + id + "&kd_cabang=" + BranchID);


                if (response.Success && responseD.Success)
                {
                    result = JsonConvert.DeserializeObject<PrintPO>(response.Message);
                    resultD = JsonConvert.DeserializeObject<List<PURC_PO_D>>(responseD.Message);

                    //head
                    sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}td, th{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}} .noBorder{border: 0px solid #dddddd;}.paddingtd{padding: 0;padding-bottom:2px;padding-left:8px;padding-top:2px;}.paddingth{padding: 0;padding-bottom:8px;padding-left:8px;}.textCenter{text-align: center;}.textRight{text-align: right;}</style></head> ";

                    //company profile
                    sHTML += " <body><table style='margin-bottom: 20px; '><tr ><td style='width:40%; border: 0px solid #dddddd;' ><h2>" + result.Cabang + "</h2><p>" + result.AlamatCabang + "</p><p>" + result.Telp1 + "</p><p>" + result.Kota + "</p></td><td style='width: 20%;border: 0px solid #dddddd;'></td><td style='width: 40%;border: 0px solid #dddddd;'><h2>COPY PURCHASE ORDER</h2><p>PO No: " + result.PUNumber + "</p><p>Tanggal PO: " + result.TanggalPO + "</p><p>PO Status: " + result.StatusPO + "</p></td></tr> ";

                    //supplier profile
                    sHTML += " <tr class='headerTable '> <th style='width:40%;' class='noBorder '> SUPPLIER </th> <th style='width: 20%;' class='noBorder '></th> <th style='width: 40%;' class='noBorder '> ALAMAT PENGIRIMAN </th> </tr><tr > <td style='width:40%;' class='noBorder paddingtd'> " + result.NamaSupplier + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> " + result.AlamatPengiriman + " </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> " + result.AlamatSupplier + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> Telp No: " + result.TelpSupplier + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> Jatuh Tempo: " + result.TanggalJatuhTempo + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr></table> ";

                    //UserDetail
                    sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='noBorder'> TANGGAL KIRIM </th> <th class='noBorder'>REQUESTED BY</th> <th class='noBorder'> APPROVED BY </th> </tr><tr> <td class='noBorder paddingtd'> " + result.TanggalKirim + " </td><td class='noBorder paddingtd'> " + result.RequestBy + " </td><td class='noBorder paddingtd'> " + result.ApprovedBy + " </td></tr></table> ";

                    //Notes
                    sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='noBorder'> NOTES </th> </tr><tr> <td class='noBorder paddingtd'> " + result.Notes + " </td></tr></table> ";

                    //headerDetail
                    sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='textCenter'> BARANG </th> <th class='textCenter'>SATUAN</th> <th class='textCenter'> QTY </th> <th class='textCenter'> HARGA </th>  <th class='textCenter'> HARGA JUAL </th>  <th class='textCenter'> HARGA GROSIR </th> <th class='textCenter'> DISC (Rp) </th> <th class='textCenter'> TOTAL </th> </tr> ";

                    //detail
                    foreach (PURC_PO_D detail in resultD)
                    {
                        sHTML += " <tr> <td> " + detail.keterangan + " </td><td class='textCenter'>" + detail.satuan + "</td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.qty) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.harga) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.harga1) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.harga4) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.jml_diskon) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.total) + " </td></tr> ";
                    }

                    //total
                    sHTML += " <tr> <td class='textRight noBorder' colspan='7'> SUBTOTAL </td><td class='textRight'> " + string.Format("{0:#,0.00}", result.SubTotal) + "</td></tr><tr> <td class='textRight noBorder' colspan='7'> ONGKOS KIRIM </td><td class='textRight'> " + string.Format("{0:#,0.00}", result.Ongkir) + "</td></tr><tr> <td class='textRight noBorder' colspan='7'> PPN </td><td class='textRight'> " + string.Format("{0:#,0.00}", result.PPN) + "</td></tr><tr> <th class='textRight noBorder' colspan='7'> GRAND TOTAL </th> <th class='textRight'> " + string.Format("{0:#,0.00}", result.GrandTotal) + "</th> </tr> ";

                    //endDetail
                    sHTML += " </table> ";

                    // ttd
                    sHTML += "<tr> <table style='margin-bottom: 20px;'> <tr> <th  class='textCenter noBorder '> Dibuat </th> <th  class='textCenter noBorder '> Diperiksa </th> <th class='textCenter noBorder '> Akunting </th></tr> ";
                    sHTML += " <tr> <td class='noBorder paddingtd'> <pre> </td><td class='noBorder paddingtd'> <pre>  </td><td class='noBorder paddingtd'><pre>  </td> ";
                    sHTML += " <tr> <td  class='textCenter noBorder paddingtd'>(" + UserID + ")</td><td  class='textCenter noBorder paddingtd'><pre>(           ) </td><td  class='textCenter noBorder paddingtd'> <pre>(           ) </td> ";
                    sHTML += "</table >";
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

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetPrintMemo(string id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            PrintPO result = new PrintPO();
            IEnumerable<PURC_PO_D> resultD = new List<PURC_PO_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            Response responseD = new Response();

            string sHTML = "";

            try
            {
                response = await client.CallAPIGet("PURC_PO/GetPrintPO?no_po=" + id);

                responseD = await client.CallAPIGet("PURC_PO/GetDetailPO?no_po=" + id);


                if (response.Success && responseD.Success)
                {
                    result = JsonConvert.DeserializeObject<PrintPO>(response.Message);
                    resultD = JsonConvert.DeserializeObject<List<PURC_PO_D>>(responseD.Message);

                    //head
                    sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}td, th{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}} .noBorder{border: 0px solid #dddddd;}.paddingtd{padding: 0;padding-bottom:2px;padding-left:8px;padding-top:2px;}.paddingth{padding: 0;padding-bottom:8px;padding-left:8px;}.textCenter{text-align: center;}.textRight{text-align: right;}</style></head> ";

                    //company profile
                    sHTML += " <body><table style='margin-bottom: 20px; '><tr ><td style='width:40%; border: 0px solid #dddddd;' ><h2>" + result.Cabang + "</h2><p>" + result.AlamatCabang + "</p><p>" + result.Telp1 + "</p><p>" + result.Kota + "</p></td><td style='width: 20%;border: 0px solid #dddddd;'></td><td style='width: 40%;border: 0px solid #dddddd;'><h2>MEMO PURCHASE ORDER</h2><p>PO No: " + result.PUNumber + "</p><p>Tanggal PO: " + result.TanggalPO + "</p><p>PO Status: " + result.StatusPO + "</p></td></tr> ";

                    //supplier profile
                    sHTML += " <tr class='headerTable '> <th style='width:40%;' class='noBorder '> SUPPLIER </th> <th style='width: 20%;' class='noBorder '></th> <th style='width: 40%;' class='noBorder '>  </th> </tr><tr > <td style='width:40%;' class='noBorder paddingtd'> " + result.NamaSupplier + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'>  </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> " + result.AlamatSupplier + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> Telp No: " + result.TelpSupplier + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> Jatuh Tempo: " + result.TanggalJatuhTempo + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr></table> ";

                    //UserDetail
                    sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='noBorder'> TANGGAL MEMO </th> <th class='noBorder'>REQUESTED BY</th> <th class='noBorder'> APPROVED BY </th> </tr><tr> <td class='noBorder paddingtd'> " + DateTime.Now + " </td><td class='noBorder paddingtd'> " + result.RequestBy + " </td><td class='noBorder paddingtd'> " + result.ApprovedBy + " </td></tr></table> ";

                    //Notes
                    sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='noBorder'> NOTES </th> </tr><tr> <td class='noBorder paddingtd'> " + result.Notes + " </td></tr></table> ";

                    //headerDetail
                    sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='textCenter'> BARANG </th> <th class='textCenter'>SATUAN</th> <th class='textCenter'> QTY </th></tr> ";

                    //detail
                    foreach (PURC_PO_D detail in resultD)
                    {
                       // sHTML += " <tr> <td> " + detail.nama_barang + " </td><td class='textCenter'>" + detail.satuan + "</td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.qty) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.harga) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.jml_diskon) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.total) + " </td></tr> ";
                        sHTML += " <tr> <td> " + detail.keterangan + " </td><td class='textCenter'>" + detail.satuan + "</td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.qty) + " </td> ";
                    }

                    //total
                   // sHTML += " <tr> <td class='textRight noBorder' colspan='5'> SUBTOTAL </td><td class='textRight'> " + string.Format("{0:#,0.00}", result.SubTotal) + "</td></tr><tr> <td class='textRight noBorder' colspan='5'> ONGKOS KIRIM </td><td class='textRight'> " + string.Format("{0:#,0.00}", result.Ongkir) + "</td></tr><tr> <td class='textRight noBorder' colspan='5'> PPN </td><td class='textRight'> " + string.Format("{0:#,0.00}", result.PPN) + "</td></tr><tr> <th class='textRight noBorder' colspan='5'> GRAND TOTAL </th> <th class='textRight'> " + string.Format("{0:#,0.00}", result.GrandTotal) + "</th> </tr> ";

                    //endDetail
                    sHTML += " </table> ";
                  
                   // ttd
                    sHTML += "<tr> <table style='margin-bottom: 20px;'> <tr> <th  class='textCenter noBorder '> Dibuat </th> <th  class='textCenter noBorder '> Diperiksa </th> <th class='textCenter noBorder '> Driver </th></tr> ";
                    sHTML += " <tr> <td class='noBorder paddingtd'> <pre> </td><td class='noBorder paddingtd'> <pre>  </td><td class='noBorder paddingtd'><pre>  </td> ";
                    sHTML += " <tr> <td  class='textCenter noBorder paddingtd'>("+ UserID +")</td><td  class='textCenter noBorder paddingtd'><pre>(           ) </td><td  class='textCenter noBorder paddingtd'> <pre>(           ) </td> ";
                    sHTML += "</table >";
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

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public IActionResult RequestPO()
        {
            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetPORequest(string status)
        {
            IEnumerable<PURC_DPM_D> result = new List<PURC_DPM_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
             
                response = await client.CallAPIGet("PURC_DPM_D/GetPURC_DPM_D?No_DPM=&status=" + status);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<PURC_DPM_D>>(response.Message);
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
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveRequestPO(PURC_PO data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            string mode = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            List<PURC_DPM_D> List = new List<PURC_DPM_D>();
            try
            {
               
                if (data.podetail != null)
                {
                    if (data.podetail[0].no_dpm == null)
                    {
                        mode = "NEW";
                        
                    }
                    for (int i = 0; i <= data.podetail.Count - 1; i++)
                    {
                        PURC_DPM_D model = new PURC_DPM_D();
                        if (mode == "NEW")
                        {
                            model.No_DPM = DateTime.Now.ToString("yyyyMMddHHmmssfff") + i;
                            model.Last_Created_By = UserID;
                            model.Last_Create_Date = DateTime.Now;
                        }
                        else
                        {
                            model.No_DPM = data.podetail[i].no_dpm;
                            model.Last_Updated_By = UserID;
                            model.Last_Update_Date = DateTime.Now;
                        }
                        model.No_Seq = 1;
                        model.Qty_PR = 0;
                        model.Qty_received = 0;
                        model.Qty_sisa = data.podetail[i].qty;
                        model.Qty = data.podetail[i].qty;
                        model.rec_stat = "ENTRY";
                        model.no_po = "";
                        model.Tgl_Diperlukan = data.Tgl_Diperlukan;
                        model.Keterangan = data.keterangan;
                        model.Kd_Stok = data.podetail[i].kd_stok;
                        model.Satuan = data.podetail[i].kd_satuan;
                        model.Tgl_Diperlukan = data.Tgl_Diperlukan;
                       
                        model.Kd_Cabang = BranchID;
                        model.tipe_trans = "JPP-KUT-01";
                        List.Add(model);

                    }

                }


                if (mode == "NEW")
                {
                    foreach (PURC_DPM_D model in List)
                    {
                        response = await client.CallAPIPost("PURC_DPM_D/SavePURC_DPM_D", model);
                    }
                }
                else
                {
                    foreach (PURC_DPM_D model in List)
                    {
                        response = await client.CallAPIPost("PURC_DPM_D/EditPURC_DPM_D", model);
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
        public async Task<PartialViewResult> EditRequest(string id = null)
        {
            PURC_DPM_D model = new PURC_DPM_D();
            ApiClient client = factoryClass.APIClientAccess();

            try
            {
                if (id != null)
                {
                    var response = await client.CallAPIGet("PURC_DPM_D/GetPURC_DPM_D?No_DPM=" + id);

                    if (response.Success)
                    {
                        model = JsonConvert.DeserializeObject<List<PURC_DPM_D>>(response.Message).FirstOrDefault();
                        model.dpmdetail = new List<dpmDetail>();
                        dpmDetail detail = new dpmDetail();
                        detail.kd_Stok = model.Kd_Stok;
                        detail.Satuan = model.Satuan;
                        detail.kd_satuan = model.Satuan;
                        detail.nama_barang = model.Nama_Barang;
                        detail.Qty = model.Qty;
                        detail.Tgl_Diperlukan = model.Tgl_Diperlukan.ToString("dd MMMM yyyy");
                        detail.no_dpm = model.No_DPM;
                        model.dpmdetail.Add(detail);
                        ViewBag.DetailList= JsonConvert.SerializeObject(model.dpmdetail);
                    }
                }
                else
                {
                    dpmDetail detail = new dpmDetail();
                    model.No_DPM = "AUTO GENERATE";
                    model.rec_stat = "NEW";
                    ViewBag.DetailList = JsonConvert.SerializeObject(model.dpmdetail);
                }
                ViewBag.Mode = model.rec_stat;
                //var responseSatuan = await client.CallAPIGet("SIF_Satuan/GetSIFSatuanCbo");
                //if (responseSatuan.Success)
                //{
                //   model.ListSatuan = JsonConvert.DeserializeObject<List<SIF_Satuan>>(responseSatuan.Message);

                //}

                model.ListSatuan = new List<SIF_Satuan>();
                SIF_Satuan item = new SIF_Satuan();
                item.Kode_Satuan = "";
                item.Nama_Satuan = "Pilih Satuan";
                model.ListSatuan.Add(item);

               
                //var responseBarang = await client.CallAPIGet("SIF_Barang/GetSIFBarangCb");
                //if (responseBarang.Success)
                //{
                //    model.ListBarang = JsonConvert.DeserializeObject<List<SIF_BarangCbo>>(responseBarang.Message);

                //}
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

           
            return PartialView(model);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public IActionResult ApprovalRequestPO()
        {
            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveApprovalRequestPO([FromBody] List<PURC_DPM_D> data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            try
            {
                for (int i = 0; i <= data.Count() - 1; i++)
                {
                    if (data[i].status_approve != "REJECT")
                    {
                        data[i].Qty_PR = data[i].qty_approve;
                        data[i].Qty_sisa = data[i].qty_approve;
                    }
                    else
                    {
                        data[i].Qty_PR = 0;
                        data[i].Qty_sisa = 0;
                    }
                    data[i].rec_stat = data[i].status_approve;
                    data[i].Last_Updated_By = UserID;
                    data[i].Last_Update_Date = DateTime.Now;
                }
                response = await client.CallAPIPost("PURC_DPM_D/UpdateApprovalPURC_DPM_D", data);
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

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveApprovalPO([FromBody] List<PURC_PO> data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            List<PURC_PO> ListPO = new List<PURC_PO>();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            try
            {
                for (int i = 0; i <= data.Count() - 1; i++)
                {
                    if (data[i].rec_stat != "")
                    {
                        PURC_PO cont = new PURC_PO();
                        response = await client.CallAPIGet("PURC_PO/GetPO?no_po=" + data[i].no_po);

                        if (response.Success)
                        {
                            cont = JsonConvert.DeserializeObject<List<PURC_PO>>(response.Message).FirstOrDefault();
                        }

                        cont.rec_stat = data[i].rec_stat;

                        cont.ket_batal = data[i].ket_batal;
                        cont.Last_Update_Date = DateTime.Now;
                        cont.Last_Updated_By = UserID;

                        if (data[i].rec_stat == "APPROVE")
                        {
                            cont.status_po = "OPEN";
                            cont.user_approve = UserID;
                            cont.tgl_approve = DateTime.Now;
                            cont.ket_batal = "";
                        }
                        else if (data[i].rec_stat == "REJECT")
                        {
                            cont.status_po = "BATAL";
                        }
                        else if (data[i].rec_stat == "REVISE")
                        {
                            cont.status_po = "REVISE";
                        }

                        ListPO.Add(cont);
                    }

                }
                response = await client.CallAPIPost("PURC_PO/SaveApprovalPO", ListPO);
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
        public async Task<IActionResult> GetBarang()
        {
            List<SIF_BarangCbo> result = new List<SIF_BarangCbo>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("SIF_Barang/GetSIFBarangCbo");
                if (response.Success)
                {
                    
                    result = JsonConvert.DeserializeObject<List<SIF_BarangCbo>>(response.Message);
                    SIF_BarangCbo item = new SIF_BarangCbo();
                    item.Kode_Barang = "";
                    item.Nama_Barang = " ALL";
                    result.Add(item);

                    result = result.OrderBy(o => o.Nama_Barang).ToList();
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

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public IActionResult ReturnPO(string id = "", string mode = "")
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

        public async Task<IActionResult> GetReturInv()
        {
            IEnumerable<PURC_PO> result = new List<PURC_PO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            try
            {
                response = await client.CallAPIGet("PURC_PO/GetReturInv?kd_cabang=" + BranchID);
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<PURC_PO>>(response.Message);
                    var list = result.Select(x => new
                    {
                        x.no_po,
                        x.Nama_Supplier,
                        x.no_jur,
                        x.kd_supplier
                    });
                    return Ok(list);
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

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> GetReturPO(string no_po)
        {
            IEnumerable<vBarangPOTerpilih> result = new List<vBarangPOTerpilih>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            try
            {
                response = await client.CallAPIGet("PURC_PO/GetPORetur?no_po=" + no_po);
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<vBarangPOTerpilih>>(response.Message);
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
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> Saveretur(PURC_RETUR_BELI data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var mode = "";
            string nopo = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            int seq = 0;
            try
            {
                response = await client.CallAPIGet("Helper/GetNoTransx?prefix=RTR-M&kdcabang=" + BranchID);
                if (response.Success)
                {
                    nopo = JsonConvert.DeserializeObject<string>(response.Message);
                }
                
                data.Kd_Cabang = BranchID;
                data.Last_Create_Date = DateTime.Now;
                data.Last_Created_By = UserID;
                data.no_retur = nopo;
                data.jml_rp_trans = data.detail.Sum(s=>s.retur_total);
                data.rec_stat = "ENTRY";
                data.Program_Name = "frmReturBeliMaterial1";
                foreach (var dtl in data.detail)
                {
                    seq += 1;
                    dtl.Kd_Cabang = BranchID;
                    dtl.Last_Create_Date = DateTime.Now;
                    dtl.Last_Created_By = UserID;
                    dtl.no_retur = nopo;
                    dtl.no_seq = seq;
                    dtl.satuan = dtl.kd_satuan;
                    dtl.qty_sisa = dtl.qty - dtl.qty_retur;
                    dtl.qty = dtl.qty_retur;
                    dtl.total = dtl.qty_retur * dtl.harga;
                    dtl.rec_stat = "ENTRY";
                    dtl.Program_Name = "frmReturBeliMaterial1";
                }

                response = await client.CallAPIPost("PURC_PO/SaveReturPO", data);
                if (response.Success)
                {
                    response.Result = nopo;
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

        public async Task<IActionResult> GetRetur(string no_po)
        {
            PURC_RETUR_BELI result = new PURC_RETUR_BELI();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            try
            {
                response = await client.CallAPIGet("PURC_PO/GetRetur?no_retur=" + no_po);
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<PURC_RETUR_BELI>(response.Message);
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

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK")]
        public async Task<IActionResult> PembatalanPO(string id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            Response response = new Response();
            PURC_PO PO = new PURC_PO();
            // List<PURC_PO_D> detail = new List<PURC_PO_D>();
            ApiClient client = factoryClass.APIClientAccess();
            List<INV_GUDANG_IN_D> detail = new List<INV_GUDANG_IN_D>();
            try
            {
                response = await client.CallAPIGet("PURC_PO/GetPO?no_po=" + id);
                if (response.Success)
                {
                    PO = JsonConvert.DeserializeObject<List<PURC_PO>>(response.Message).FirstOrDefault();
                    PO.Last_Updated_By = "";
                    PO.Last_Update_Date = DateTime.Now;
                    PO.status_po = "BATAL";
                    PO.rec_stat = "BATAL";
                    PO.tgl_batal = DateTime.Now;
                    PO.ket_batal = "BATAL";
                    response = await client.CallAPIGet("GUDANG_IN/getGudangDetailByNoPO?no_po=" + id);

                    if (response.Success)
                    {
                        detail = JsonConvert.DeserializeObject<List<INV_GUDANG_IN_D>>(response.Message);
                        if (detail != null && detail.Count() > 0)
                        {
                            PO.gudangin = new List<INV_GUDANG_IN_D>();
                            foreach (var item in detail)
                            {
                                PO.gudangin.Add(item);
                            }
                            response = await client.CallAPIPost("PURC_PO/PembatalanPO", PO);
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "PO Tidak Ditemukan";
                            response.Result = "failed";
                            return Ok(response);
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "PO Tidak Ditemukan";
                        response.Result = "failed";
                        return Ok(response);
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "PO Tidak Ditemukan";
                    response.Result = "failed";
                    return Ok(response);
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
        public async Task<IActionResult> GetAlamatKirim()
        {
            IEnumerable<SIF_ALAMAT_KIRIM> result = new List<SIF_ALAMAT_KIRIM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            try
            {
                response = await client.CallAPIGet("Helper/GetAlamatKirim");
                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<SIF_ALAMAT_KIRIM>>(response.Message);

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

        public IActionResult ReturnList()
        {
            return View();
        }

        public async Task<IActionResult> GetReturList(FilterPURC_PO filterPO)
        {
            IEnumerable<PURC_RETUR_BELI> result = new List<PURC_RETUR_BELI>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("PURC_PO/GetReturList?DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo);

                if (response.Success)
                {

                    result = JsonConvert.DeserializeObject<List<PURC_RETUR_BELI>>(response.Message);
                    //if (result.Count() > 0)
                    //{
                    //    result.FirstOrDefault().tgl_podesc = result.FirstOrDefault().tgl_po.ToString("dd MMMM yyyy");
                    //    result.FirstOrDefault().tgl_kirimdesc = result.FirstOrDefault().tgl_kirim.ToString("dd MMMM yyyy");
                    //    result.FirstOrDefault().tgl_jth_tempodesc = result.FirstOrDefault().tgl_jth_tempo.ToString("dd MMMM yyyy");
                    //}

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

        public IActionResult LastPO()
        {
            return View();
        }

        public async Task<IActionResult> GetLastPO(FilterPURC_PO filterPO)
        {
            IEnumerable<v_last_purc> result = new List<v_last_purc>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("PURC_PO/GetLastPOMonitoring?no_po=" + filterPO.no_po + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo + "&status_po=" + filterPO.status_po + "&barang=" + filterPO.barang);

                if (response.Success)
                {

                    result = JsonConvert.DeserializeObject<List<v_last_purc>>(response.Message);
                    //if (result.Count() > 0)
                    //{
                    //    result.FirstOrDefault().tgl_podesc = result.FirstOrDefault().tgl_po.ToString("dd MMMM yyyy");
                    //    result.FirstOrDefault().tgl_kirimdesc = result.FirstOrDefault().tgl_kirim.ToString("dd MMMM yyyy");
                    //    result.FirstOrDefault().tgl_jth_tempodesc = result.FirstOrDefault().tgl_jth_tempo.ToString("dd MMMM yyyy");
                    //}

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

        public async Task<string> GetPOPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string barang)
        {
            List<PURC_PO> result = new List<PURC_PO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("PURC_PO/GetPOPartial?skip=" + skip + "&take=" + take + "&pageSize=" + pageSize + "&page=" + page + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&sorting=" + sorting + "&filter=" + filter + "&barang=" + barang);
              //  response = await client.CallAPIPost("PURC_PO/GetPOPartial", filterPO);


                if (response.Success)
                {

                    result = JsonConvert.DeserializeObject<List<PURC_PO>>(response.Message);
                    //if (result.Count() > 0)
                    //{
                    //    result.FirstOrDefault().tgl_podesc = result.FirstOrDefault().tgl_po.ToString("dd MMMM yyyy");
                    //    result.FirstOrDefault().tgl_kirimdesc = result.FirstOrDefault().tgl_kirim.ToString("dd MMMM yyyy");
                    //    result.FirstOrDefault().tgl_jth_tempodesc = result.FirstOrDefault().tgl_jth_tempo.ToString("dd MMMM yyyy");
                    //}

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

        public async Task<string> GetLastPOPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string barang)
        {
            IEnumerable<v_last_purc> result = new List<v_last_purc>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("PURC_PO/GetLastPOPartialMonitoring?skip=" + skip + "&take=" + take + "&pageSize=" + pageSize + "&page=" + page + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&sorting=" + sorting + "&filter=" + filter + "&barang=" + barang);

                if (response.Success)
                {

                    result = JsonConvert.DeserializeObject<List<v_last_purc>>(response.Message);

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
        }

        public async Task<string> GetReturListPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string barang)
        {
            IEnumerable<PURC_RETUR_BELI> result = new List<PURC_RETUR_BELI>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("PURC_PO/GetReturListPartial?skip=" + skip + "&take=" + take + "&pageSize=" + pageSize + "&page=" + page + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&sorting=" + sorting + "&filter=" + filter + "&barang=" + barang);

                if (response.Success)
                {

                    result = JsonConvert.DeserializeObject<List<PURC_RETUR_BELI>>(response.Message);
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
        }

        public async Task<FileResult> ExportExcelLastPO(DateTime DateFrom, DateTime DateTo)
        {
            List<v_last_purcdownload> result = new List<v_last_purcdownload>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("PURC_PO/GetLastPOMonitoring?DateFrom=" + DateFrom + "&DateTo=" + DateTo);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<v_last_purcdownload>>(response.Message);
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

            byte[] filecontent = ExcelExportHelper.ExportExcel(result);
            string excelName = $"PO Terakhir-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(filecontent, ExcelExportHelper.ExcelContentType, excelName);
        }

      
    }
}