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
using IFA.Domain.Helpers;
using IFA.Domain.Models;
using IFA.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IFA.Web.Controllers
{
    public class ProduksiController : BaseController
    {
        public ProduksiController(FactoryClass factoryClass,
           IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Deletercnkirim(string id)
        {
            List<PROD_rcn_krm_D> result = new List<PROD_rcn_krm_D>();
            PROD_rcn_krm DelrcnKirim  = new PROD_rcn_krm();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            DelrcnKirim.no_trans = id;
            response = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetDetRcnKirim?no_trans=" + id);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<PROD_rcn_krm_D>>(response.Message);

            }

            for (int i = 0; i <= result.Count()- 1; i++)
            {
                result[i].last_updated_by = UserID;
   
            }


                var ListDO = result.GroupBy(item => new { item.no_sp,item.last_updated_by}).Select(group => new PROD_rcn_krm_D { no_sp = group.Key.no_sp,last_updated_by = group.Key.last_updated_by}).ToList();
          response = await client.CallAPIPost("PROD_RENCANA_KIRIM/UpdateSOStatus",ListDO);
          response = await client.CallAPIPost("PROD_RENCANA_KIRIM/Deletercnkirim", DelrcnKirim);

            return Ok(response);

        }


        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult>Create(string id = "", string mode = "")
        {
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            PROD_rcn_krm rcnkirim = new PROD_rcn_krm();
            response = null;
            response = await client.CallAPIGet("Helper/GetCboKenek");
            if (response.Success)
            {
                ViewBag.Kenek = response.Message;

            }
            response = await client.CallAPIGet("Helper/GetKendaraan");
            if (response.Success)
            {
                ViewBag.Kendaraan = response.Message;

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

            return View(rcnkirim);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult>Getrcnkrm(string id, string kd_sales)
        {
            IEnumerable<PRODV_MON_SO> result = new List<PRODV_MON_SO>();
            IEnumerable<KasirVM> GetSales = new List<KasirVM>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            string salesID = "";

            if (RoleName == "PENJUALAN" || RoleName == "BUAT UAT")
            {
                response = await client.CallAPIGet("Helper/GetKasir");
                if (response.Success)
                {
                    GetSales = JsonConvert.DeserializeObject<List<KasirVM>>(response.Message);

                    var x = GetSales.Where(w => w.Kode_Pegawai == PegawaiID).FirstOrDefault();
                    if (x != null)
                    {
                        salesID = x.Kode_Sales;
                    }
                 }
            }

            response = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetRcnKrm?kd_cabang=" + BranchID + "&kode_sales=" + salesID + "&no_sp=" + id);

                if (response.Success)
                {

                result = JsonConvert.DeserializeObject<List<PRODV_MON_SO>>(response.Message);
                }
            
          
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> Saverencanakrm(PROD_rcn_krm data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();


            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var mode = "";
            string no_trans = "";

            PROD_rcn_krm Header = new PROD_rcn_krm();
         
           
           
            Header.kd_cabang = BranchID;
            Header.last_created_by = UserID;
            Header.last_create_date = DateTime.Now;
            Header.tanggal = DateTime.Now;
            Header.kd_kenek = data.kd_kenek;
            Header.kd_sopir = data.kd_sopir;
            Header.kd_kendaraan = data.kd_kendaraan;
            Header.tipe_trans = "JPJ-KPT-01";
            Header.program_name = "frmRcnKrm_DPB2";
            if (Header.no_trans == null)
            {
                response = await client.CallAPIGet("Helper/GetNoTrans?prefix=PRODKRM&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);

                if (response.Success)
                {
                    Header.no_trans = JsonConvert.DeserializeObject<string>(response.Message);

                }
                else
                {
                    response = await client.CallAPIGet("Helper/GetNoTransx?prefix=PRODKRM&kdcabang=" + BranchID);
                    Header.no_trans = JsonConvert.DeserializeObject<string>(response.Message);
                }
            }

            List<PROD_rcn_krm_D> DetailList = new List<PROD_rcn_krm_D>();



            for (int i = 0; i <= data.rcnkrmDetailSO.Count - 1; i++)
            {

                PROD_rcn_krm_D model = new PROD_rcn_krm_D();
                model.kd_barang = data.rcnkrmDetailSO[i].Kd_Stok;
                model.kd_cabang = BranchID;
                model.last_created_by = UserID;
                model.no_trans = Header.no_trans;
                model.no_sp = data.rcnkrmDetailSO[i].No_sp;
                model.tanggal = DateTime.Now;
                model.last_created_by = UserID;
                model.no_seq = data.rcnkrmDetailSO[i].No_sp_box;
                model.no_sp_dtl = data.rcnkrmDetailSO[i].no_seq_d;
                model.jumlah = data.rcnkrmDetailSO[i].jumlah;
                model.kd_customer = data.rcnkrmDetailSO[i].Kd_Customer;

                DetailList.Add(model);

            }

            Header.rcnkrmDetail = DetailList;
            response = await client.CallAPIPost("PROD_RENCANA_KIRIM/SaveRencanaKirim", Header);
            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> Updaterencanakrm(PROD_rcn_krm data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();


            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
         

            PROD_rcn_krm Header = new PROD_rcn_krm();


            Header.no_trans = data.no_trans;
            Header.kd_cabang = BranchID;
            Header.last_updated_by = UserID;
            Header.last_update_date = DateTime.Now;
            Header.kd_kenek = data.kd_kenek;
            Header.kd_sopir = data.kd_sopir;
            Header.kd_kendaraan = data.kd_kendaraan;
         

            List<PROD_rcn_krm_D> DetailList = new List<PROD_rcn_krm_D>();



            for (int i = 0; i <= data.rcnkrmDetailSO.Count - 1; i++)
            {

                PROD_rcn_krm_D model = new PROD_rcn_krm_D();
                model.kd_barang = data.rcnkrmDetailSO[i].Kd_Stok;
                model.kd_cabang = BranchID;
                model.no_trans = Header.no_trans;
                model.no_sp = data.rcnkrmDetailSO[i].No_sp;
                model.tanggal = DateTime.Now;
                model.last_created_by = UserID;
                model.no_seq = data.rcnkrmDetailSO[i].No_sp_box;
                model.no_sp_dtl = data.rcnkrmDetailSO[i].no_seq_d;
                model.jumlah = data.rcnkrmDetailSO[i].jumlah;
                model.kd_customer = data.rcnkrmDetailSO[i].Kd_Customer;

                DetailList.Add(model);

            }

            Header.rcnkrmDetail = DetailList;
            response = await client.CallAPIPost("PROD_RENCANA_KIRIM/Updatercnkirim", Header);
            return Ok(response);
        }

      

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<string> GetMonrcnkrmPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime? DateFrom=null, DateTime? DateTo=null, string no_sp=null, string kd_customer=null)
        {
            List<PROD_rcn_krm> result = new List<PROD_rcn_krm>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            try
            {
                response = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetMonRcnKirimPartial?skip=" + skip + "&take=" + take + "&pageSize=" + pageSize + "&page=" + page + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&sorting=" + sorting + "&filter=" + filter + "&no_sp=" + no_sp + "&kd_customer=" + kd_customer);
                //  response = await client.CallAPIPost("PURC_PO/GetPOPartial", filterPO);


                if (response.Success)
                {

                    result = JsonConvert.DeserializeObject<List<PROD_rcn_krm>>(response.Message);
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



        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetMonrcnkrm(string no_trans, DateTime? DateFrom = null, DateTime? DateTo = null, string kd_customer=null)
        {
            IEnumerable<PROD_rcn_krm> result = new List<PROD_rcn_krm>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetMonRcnKirim?no_trans=" + no_trans + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&kd_customer=" + kd_customer);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<PROD_rcn_krm>>(response.Message);
                if (result.Count() > 0)
                {
                    result.FirstOrDefault().tgl_transdesc = result.FirstOrDefault().tanggal.ToString("dd MMMM yyyy");
                }

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDetrcnkrm(FilterProd_Krm_M filterPO)
        {


                IEnumerable<PROD_rcn_krm_D> result = new List<PROD_rcn_krm_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
           
            try
            {
                response = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetDetRcnKirim?no_trans=" + filterPO.no_trans + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<PROD_rcn_krm_D>>(response.Message);

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
        public async Task<IActionResult> GetrcnkirimDetail(string id)
        {
            IEnumerable<PRODV_MON_SO> result = new List<PRODV_MON_SO>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetrcnkirimDetail?no_trans=" + id);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<PRODV_MON_SO>>(response.Message);
               
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> DSiapkirim(string id = "", string mode = "")
        {
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            response = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetNorcnkirim?kd_cabang="+BranchID);

            //if (response.Success)
            //{
            //    ViewBag.cborcnkirim = response.Message;


            //}

            if (response.Success)
            {
                ViewBag.cborcnkirim = response.Message;

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

            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDsSiapKirim(string no_trans)
        {
            IEnumerable<PROD_rcn_krm_D> result = new List<PROD_rcn_krm_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            string blthn = DateTime.Now.ToString("yyyyMM");

            response = await client.CallAPIGet("INV_GUDANG_OUT/GetDsSiapKirim?no_trans=" + no_trans+"&kd_cabang="+BranchID+"&blthn="+ blthn);

            if (response.Success)
            {

                result = JsonConvert.DeserializeObject<List<PROD_rcn_krm_D>>(response.Message);
            }


            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveDSiapkrm(INV_GUDANG_OUT data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

         
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            List<SIF_Gudang> gudang_asal = new List<SIF_Gudang>();// HelperRepo.GetGudangFromCabang(data.Kd_Cabang);
            string gudang_tujuan = "EXP01";
            if (data.no_trans == null)
            {
                var ret = await client.CallAPIGet("Helper/GetGudangDefaultByCabang?cabang=" + BranchID);
                if (ret.Success)
                {
                    gudang_asal = JsonConvert.DeserializeObject<List<SIF_Gudang>>(ret.Message);
                }

                response = await client.CallAPIGet("Helper/GetNoTrans?prefix=BKE&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                if (response.Success)
                {
                    data.no_trans = JsonConvert.DeserializeObject<string>(response.Message);

                }
                else
                {
                    response = await client.CallAPIGet("Helper/GetNoTransx?prefix=BKE&kdcabang=" + BranchID);
                    data.no_trans = JsonConvert.DeserializeObject<string>(response.Message);
                }



            }
            data.tipe_trans = "JPB-KUT-01";
            data.tgl_trans = DateTime.Now;
            data.no_ref = data.no_ref;
            data.Last_Created_By = UserID;
            data.Kd_Cabang = BranchID;
            data.Last_Create_Date = DateTime.Now;
            data.gudang_asal = gudang_asal.FirstOrDefault().Kode_Gudang;
            data.gudang_tujuan = gudang_tujuan;

            for (int i = 0; i <= data.detail.Count() - 1; i++)
            {

                data.detail[i].Kd_Cabang = BranchID;
                data.detail[i].tipe_trans = "JPB-KUT-01";
                data.detail[i].no_ref2 = data.detail[i].no_sp;
                data.detail[i].no_seq = i + 1;
                data.detail[i].no_sp_dtl = data.detail[i].no_sp_dtl;
                data.detail[i].harga = data.detail[i].harga;
                data.detail[i].kd_stok = data.detail[i].kd_stok;
                data.detail[i].no_ref = data.no_ref;
                data.detail[i].no_trans = data.no_trans;
                data.detail[i].kd_stok = data.detail[i].kd_stok;
                data.detail[i].kd_buku_besar = data.detail[i].rek_persediaan;
                data.detail[i].gudang_asal = gudang_asal.FirstOrDefault().Kode_Gudang;
                data.detail[i].gudang_tujuan = gudang_tujuan;
                data.detail[i].blthn = DateTime.Now.ToString("yyyyMM");
                data.detail[i].Last_Created_By = UserID;
                data.detail[i].Last_Create_Date = DateTime.Now;


            }

            response = await client.CallAPIPost("PROD_RENCANA_KIRIM/SaveSiapKirim", data);
            response.Result = data.no_trans;


            return Ok(response);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult MSiapkirim()
        {
            return View();
        }


        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetSiapKirim(string id)
        {
            IEnumerable<INV_GUDANG_OUT> result = new List<INV_GUDANG_OUT>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetSiapKirim?no_trans=" + id);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_GUDANG_OUT>>(response.Message);
                if (result.Count() > 0)
                {
                    result.FirstOrDefault().tgl_transdesc = result.FirstOrDefault().tgl_trans.ToString("dd MMMM yyyy");
                }

            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetMonSiapkrm(string id)
        {
            IEnumerable<INV_GUDANG_OUT> result = new List<INV_GUDANG_OUT>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetMonSiapKirim?no_trans=" + id);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_GUDANG_OUT>>(response.Message);
                if (result.Count() > 0)
                {
                    result.FirstOrDefault().tgl_transdesc = result.FirstOrDefault().tgl_trans.ToString("dd MMMM yyyy");
                }

            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetMonSiapkrmD(string id)
        {
            IEnumerable<INV_GUDANG_OUT_D> result = new List<INV_GUDANG_OUT_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetMonSiapKirimD?no_trans=" + id);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_GUDANG_OUT_D>>(response.Message);
                if (result.Count() > 0)
                {
                   // result.FirstOrDefault().tgl_transdesc = result.FirstOrDefault().tgl_trans.ToString("dd MMMM yyyy");
                }

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> PrintSiapKirim(string id)
        {
            INV_GUDANG_OUT result = new INV_GUDANG_OUT();

            IEnumerable<INV_GUDANG_OUT_D> resultD = new List<INV_GUDANG_OUT_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            Response responseD = new Response();

            string sHTML = "";


            response = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetPrintSiapKirim?no_trans=" + id);

            responseD = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetMonSiapKirimD?no_trans=" + id);


            if (response.Success && responseD.Success)
            {
                resultD = JsonConvert.DeserializeObject<List<INV_GUDANG_OUT_D>>(responseD.Message);
                result = JsonConvert.DeserializeObject<INV_GUDANG_OUT>(response.Message);
               

                //head
                sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}td, th{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}} .noBorder{border: 0px solid #dddddd;}.paddingtd{padding: 0;padding-bottom:2px;padding-left:8px;padding-top:2px;}.paddingth{padding: 0;padding-bottom:8px;padding-left:8px;}.textCenter{text-align: center;}.textRight{text-align: right;}</style></head> ";

                //company profile
                sHTML += " <body><table style='margin-bottom: 20px; '><tr ><td style='width:40%; border: 0px solid #dddddd;' ><h2>" + result.no_trans + "</h2><p>Nama Supir : " + result.supir+ "</p></td><td style='width: 20%;border: 0px solid #dddddd;'></td><td style='width: 40%;border: 0px solid #dddddd;'><h2>BARANG SIAP KIRIM</h2><p>No Kendaraan: " + result.Nama_Kendaraan + "</p></td></tr> ";

                //supplier profile
               

                //UserDetail


                //Notes
                sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='noBorder'> NOTES </th> </tr><tr> <td class='noBorder paddingtd'> " + result.keterangan + " </td></tr></table> ";

                //headerDetail
                sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='textCenter'> NO PENJUALAN </th> <th class='textCenter'>NAMA CUSTOMER</th> <th class='textCenter'> NAMA BARANG </th> <th class='textCenter'> SATUAN </th> <th class='textCenter'> QTY ORDER </th> <th class='textCenter'> QTY OUT </th> </tr> ";

                //detail
                foreach (INV_GUDANG_OUT_D detail in resultD)
                {
                    sHTML += " <tr> <td> " + detail.no_sp + " </td><td class='textCenter'>" + detail.Nama_Customer + "</td><td class='textCenter'>" + detail.nama_Barang + "</td><td class='textCenter'>" + detail.kd_satuan + "</td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.jumlah) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.qty_out) + " </td></tr> ";
                }

                //total opopopo


                //endDetail
                sHTML += " </table> ";

                //end
                sHTML += " </body></html> ";
            }
            ViewBag.Mode = "EDIT";
            return Ok(sHTML);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> PrintrcnKirim(string id)
        {
            IEnumerable<PROD_rcn_krm> result = new List<PROD_rcn_krm>();

            IEnumerable<PROD_rcn_krm_D> resultD = new List<PROD_rcn_krm_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            Response responseD = new Response();

            string sHTML = "";


            response = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetMonRcnKirim?no_trans=" + id);

            responseD = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetrcnkirimDetail?no_trans=" + id);


            if (response.Success && responseD.Success)
            {
                resultD = JsonConvert.DeserializeObject<List<PROD_rcn_krm_D>>(responseD.Message);
                result = JsonConvert.DeserializeObject<List<PROD_rcn_krm>>(response.Message);

                PROD_rcn_krm model = new PROD_rcn_krm();

            if (result.Count() > 0)
                {
                    model.kd_kenek = result.FirstOrDefault().kd_kenek.ToString();
                    model.no_trans = result.FirstOrDefault().no_trans.ToString();
                }




                //head
                sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}td, th{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}} .noBorder{border: 0px solid #dddddd;}.paddingtd{padding: 0;padding-bottom:2px;padding-left:8px;padding-top:2px;}.paddingth{padding: 0;padding-bottom:8px;padding-left:8px;}.textCenter{text-align: center;}.textRight{text-align: right;}</style></head> ";

                //company profile
                sHTML += " <body><table style='margin-bottom: 20px; '><tr ><td style='width:40%; border: 0px solid #dddddd;' ><h2>" + model.no_trans + "</h2><p>Nama Supir : " + model.Nama_Supir + "</p></td><td style='width: 20%;border: 0px solid #dddddd;'></td><td style='width: 40%;border: 0px solid #dddddd;'><h2>BARANG SIAP KIRIM</h2><p>No Kendaraan: " + model.kd_kendaraan + "</p></td></tr> ";

                //supplier profile


                //UserDetail


                //Notes
                sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='noBorder'> NOTES </th> </tr><tr> <td class='noBorder paddingtd'> " + model.keterangan + " </td></tr></table> ";

                //headerDetail
                sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='textCenter'> NO PENJUALAN </th> <th class='textCenter'>NAMA CUSTOMER</th> <th class='textCenter'> NAMA BARANG </th> <th class='textCenter'> SATUAN </th> <th class='textCenter'> QTY ORDER </th> <th class='textCenter'> QTY OUT </th> </tr> ";

                //detail
                foreach (PROD_rcn_krm_D detail in resultD)
                {
                    sHTML += " <tr> <td> " + detail.no_trans + " </td><td class='textCenter'>" + detail.Nama_Barang + "</td><td class='textCenter'>" + detail.Nama_Barang + "</td><td class='textCenter'>" + detail.kd_satuan + "</td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.jumlah) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.qty_out) + " </td></tr> ";
                }

                //total opopopo


                //endDetail
                sHTML += " </table> ";

                //end
                sHTML += " </body></html> ";
            }
            ViewBag.Mode = "EDIT";
            return Ok(sHTML);
        }

        public async Task<IActionResult> CetakDPB()
        {
            return View();
        }
        public async Task<IActionResult> GetCetakDPB(FilterPURC_PO Filter)
        {
            IEnumerable<CETAK_DPB> result = new List<CETAK_DPB>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            string blthn = DateTime.Now.ToString("yyyyMM");

            response = await client.CallAPIGet("PROD_RENCANA_KIRIM/GetCetakDPB/?DateFrom="+ Filter.DateFrom + "&DateTo=" +Filter.DateTo);

            if (response.Success)
            {

                result = JsonConvert.DeserializeObject<List<CETAK_DPB>>(response.Message);
            }


            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveCetakDPB(inputcetakDPB data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            response = await client.CallAPIPost("PROD_RENCANA_KIRIM/SaveCetakDPB", data);
            //response = JsonConvert.DeserializeObject<List<PROD_rcn_krm_D>>(responseD.Message);
            return Ok(response);
        }
    }
}