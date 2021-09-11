using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using ERP.Web;
using IFA.Domain.Utils;

using ERP.Domain.Base;
using ERP.Web.Utils;
using IFA.Domain.Helpers;
using IFA.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ERP.Web.Controllers;
using System.Globalization;

namespace IFA.Web.Controllers
{
    public class QCController : BaseController
    {
        public QCController(FactoryClass factoryClass,
         IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Index(string id = "", string mode = "")
        {
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            response = await client.CallAPIGet("INV_Q/GetListPO?kd_cabang=" + BranchID);


            if (response.Success)
            {
                ViewBag.NoTrans = response.Message;

            }

            response = null;
            response = await client.CallAPIGet("Helper/GetGudangDefaultByCabang/?cabang=" + BranchID);
            if (response.Success)
            {
                ViewBag.GudangList = response.Message;

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

            //response = null;
            //response = await client.CallAPIGet("Helper/GetGudang");
            //if (response.Success)
            //{
            //    ViewBag.GudangList = response.Message;

            //}

            response = null;
            response = await client.CallAPIGet("SIF_Barang/GetSIFBarangCB");
            if (response.Success)
            {
                ViewBag.Barang = response.Message;

            }
            return View();
        }

        public IActionResult MonitoringQC()
        {
            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetPODetail(FilterPURC_PO filterPO)
        {
            IEnumerable<PURC_PO_D> result = new List<PURC_PO_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("PURC_PO/GetDetailPO?no_po=" + filterPO.no_po + "&DateFrom=" + filterPO.DateFrom + "&DateTo=" + filterPO.DateTo + "&status_po=" + filterPO.status_po);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<PURC_PO_D>>(response.Message);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetdataQC(string id)
        {
            IEnumerable<INV_QC> result = new List<INV_QC>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("INV_Q/GetQC?no_trans=" + id);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_QC>>(response.Message);


            }
            //ViewBag.Mode = "EDIT";
            return Ok(result);

        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetQC(string id)
        {
            IEnumerable<PURC_PO_D> result = new List<PURC_PO_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("INV_Q/GetPO?no_po=" + id);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<PURC_PO_D>>(response.Message);


            }
            //ViewBag.Mode = "EDIT";
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDetailQC(FilterGudangIn filterMn)
        {
            IEnumerable<INV_QC> result = new List<INV_QC>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            //DateTime dt1 = DateTime.ParseExact(filterMn.DateFrom.ToString(), "dd/MM/yyyy HH:mm tt", CultureInfo.InvariantCulture);
            //DateTime dt2 = DateTime.ParseExact(filterMn.DateTo.ToString(), "dd/MM/yyyy HH:mm tt", CultureInfo.InvariantCulture);
            DateTime dt1 = filterMn.DateFrom.Value;
            DateTime dt2 = filterMn.DateTo.Value;
            string strdate1 = dt1.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            string strdate2 = dt2.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //date1.par
            try
            {
                response = await client.CallAPIGet("INV_Q/GetMonitoringQC?kd_stok=" + filterMn.barang + "&DateFrom=" + strdate1 + "&DateTo=" + strdate2);
                //response = await client.CallAPIGet("INV_Q/GetMonitoringQC?kd_stok=" + filterMn.barang);

                if (response.Success)
                {
                    result = JsonConvert.DeserializeObject<List<INV_QC>>(response.Message);
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


            //if (response.Success)
            //{
            //    result = JsonConvert.DeserializeObject<List<INV_QC>>(response.Message);
            //}
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveQC(PURC_PO data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var mode = "";
            string no_trans = "";
            string blthn = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            if (data.no_trans == null)
            {
                mode = "NEW";
                response = await client.CallAPIGet("Helper/GetNoTransx?prefix=QC&kdcabang=" + BranchID);
                if (response.Success)
                {
                    no_trans = JsonConvert.DeserializeObject<string>(response.Message);
                }
                data.no_trans = no_trans;
            }
            else
            {
                no_trans = data.no_trans;
            }

            if (data.blthn == null)
            {
                mode = "NEW";
                response = await client.CallAPIGet("Helper/GetBlthn");
                if (response.Success)
                {
                    blthn = JsonConvert.DeserializeObject<string>(response.Message);
                }
                data.blthn = blthn;
            }
            else
            {
                blthn = data.blthn;
            }

            data.Kd_Cabang = BranchID;
            data.kurs_valuta = 1;
            data.tipe_trans = "JPP-KUT-01";
            data.total = data.jml_rp_trans;
            data.jml_rp_trans = data.jml_rp_trans - data.jml_ppn - data.ongkir;
            data.kurs_valuta = 1;
            data.tgl_kirim = DateTime.Now;
            data.tgl_jth_tempo = DateTime.Now;
            //if (data.keterangan == null)
            //{
            //    data.keterangan = "QC " + BranchID;
            //}
            data.podetail.RemoveAll(x => x.qty_qc_pass == 0);
            for (int i = 0; i <= data.podetail.Count() - 1; i++)
            {
                data.podetail[i].no_trans = data.no_trans;
                data.podetail[i].Kd_Cabang = data.Kd_Cabang;
                data.podetail[i].tipe_trans = data.tipe_trans;
                data.podetail[i].no_seq = i + 1;
                data.podetail[i].tgl_trans = data.tgl_trans;
                data.podetail[i].kd_stok = data.podetail[i].kd_stok;
                data.podetail[i].kd_satuan = data.podetail[i].kd_satuan;
                data.podetail[i].spek_brg = data.podetail[i].spek_brg;
                data.podetail[i].qty_order = data.podetail[i].qty_order;
                data.podetail[i].qty_qc_pass = data.podetail[i].qty_order - data.podetail[i].qty_sisa;
                data.podetail[i].qty_qc_unpass = data.podetail[i].qty_sisa;
                data.podetail[i].qty_sisa = data.podetail[i].qty_sisa;
                data.podetail[i].blthn = blthn;
                data.podetail[i].Bonus = data.podetail[i].Bonus;
                data.podetail[i].harga = data.podetail[i].harga;
                data.podetail[i].gudang_tujuan = data.podetail[i].lokasi;
                data.podetail[i].kd_buku_besar = data.podetail[i].kd_buku_besar;
                data.podetail[i].kd_buku_biaya = data.podetail[i].kd_buku_biaya;
                data.podetail[i].Last_Created_By = UserID;
                data.podetail[i].Last_Create_Date = DateTime.Now;
            }
            data.qty_total = data.podetail.Select(i => i.qty_qc_pass).Sum();
            if (mode == "NEW")
            {
                data.Last_Created_By = UserID;
                data.Last_Create_Date = DateTime.Now;
                response = await client.CallAPIPost("INV_Q/SaveQC", data);
            }
            else
            {
                data.Last_Updated_By = UserID;
                data.Last_Update_Date = DateTime.Now;
                response = await client.CallAPIPost("INV_Q/UpdateQC", data);

            }


            INV_GUDANG_IN gudangin = new INV_GUDANG_IN();
            string no_transIN = "";
            gudangin.no_ref = data.no_ref;
            gudangin.no_qc = no_trans;
            gudangin.tipe_trans = "JPP-KUT-01";
            gudangin.penyerah = data.penyerah;
            gudangin.keterangan = data.keterangan;
            gudangin.blthn = DateTime.Now.ToString("yyyyMM");
            gudangin.Last_Create_Date = DateTime.Now;
            gudangin.tgl_trans = data.tgl_trans;
            gudangin.kode_gudang = data.kode_gudang;
            gudangin.jml_qtypo = data.podetail.Sum(c => c.qty_order) ?? 0;
            gudangin.jml_qtyin = data.podetail.Sum(c => c.qty_qc_pass) ?? 0;

            if (data.p_np == "Y")
            {
                mode = "NEW";
                response = await client.CallAPIGet("Helper/GetNoTrans?prefix=JPPP&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                if (response.Success)
                {
                    no_transIN = JsonConvert.DeserializeObject<string>(response.Message);

                }
                gudangin.no_trans = no_transIN;

            }
            else
            {
                mode = "NEW";
                response = await client.CallAPIGet("Helper/GetNoTrans?prefix=JPPNP&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                if (response.Success)
                {
                    no_transIN = JsonConvert.DeserializeObject<string>(response.Message);

                }
                gudangin.no_trans = no_transIN;
            }

            gudangin.Kd_Cabang = BranchID;

            PURC_PO PO = new PURC_PO();
            PO.no_po = data.no_ref;
            PO.kd_supplier = "";
            PO.kurs_valuta = 0;
            PO.keterangan = data.keterangan;
            PO.tgl_po = DateTime.Now;
            PO.tgl_kirim = DateTime.Now;
            PO.tgl_bayar = DateTime.Now;
            PO.tgl_jth_tempo = DateTime.Now;
            PO.Tgl_Diperlukan = DateTime.Now;

            List<PURC_PO_D> PODetailNew = new List<PURC_PO_D>();
            gudangin.gddetail = new List<INV_GUDANG_IN_D>();

            for (int i = 0; i <= data.podetail.Count() - 1; i++)
            {
                PURC_PO_D model = new PURC_PO_D();
                model.kd_stok = data.podetail[i].kd_stok;
                model.Kd_Cabang = BranchID;
                model.harga = data.podetail[i].harga;
                model.no_po = gudangin.no_ref;
                model.qty_order = data.podetail[i].qty_order;
                model.no_seq = data.podetail[i].no_seq;
                PODetailNew.Add(model);

                INV_GUDANG_IN_D detail = new INV_GUDANG_IN_D();
                detail.Kd_Cabang = BranchID;
                detail.tipe_trans = "JPU-KUT-01";
                detail.no_seq = data.podetail[i].no_seq;
                detail.kd_stok = data.podetail[i].kd_stok;
                detail.no_qc = gudangin.no_qc;
                detail.no_ref = gudangin.no_ref;
                detail.keterangan = data.podetail[i].keterangan;
                detail.kd_satuan = data.podetail[i].kd_satuan;
                detail.kd_ukuran = "0";
                detail.no_trans = gudangin.no_trans;
                detail.qty_sisa = data.podetail[i].qty_sisa;
                detail.qty_in = data.podetail[i].qty_qc_pass ?? 0;
                detail.qty_order = data.podetail[i].qty_order;
                detail.harga = data.podetail[i].harga;
                detail.rp_trans = data.podetail[i].harga;
                detail.gudang_asal = "00000";
                detail.gudang_tujuan = gudangin.kode_gudang;
                detail.kd_buku_besar = data.podetail[i].kd_buku_besar;
                detail.kd_buku_biaya = data.podetail[i].kd_buku_biaya;
                detail.blthn = DateTime.Now.ToString("yyyyMM");
                detail.Last_Created_By = UserID;
                detail.Last_Create_Date = DateTime.Now;

                gudangin.gddetail.Add(detail);
            }

            PO.podetail = PODetailNew;

            PURC_PO POExist = new PURC_PO();
            List<PURC_PO_D> PODetailExist = new List<PURC_PO_D>();
            response = await client.CallAPIGet("PURC_PO/GetPOPenerimaan?no_po=" + data.no_ref);


            if (response.Success)
            {
                POExist = JsonConvert.DeserializeObject<PURC_PO>(response.Message);
            }

            response = await client.CallAPIGet("PURC_PO/GetDetailPOPenerimaan?no_po=" + data.no_ref);

            if (response.Success)
            {
                PODetailExist = JsonConvert.DeserializeObject<List<PURC_PO_D>>(response.Message);
            }


            var itemsToUpdate = from ExistPO in PODetailExist
                                join NewPO in PODetailNew
                                on new { ExistPO.no_po, ExistPO.kd_stok, ExistPO.no_seq } equals new { NewPO.no_po, NewPO.kd_stok, NewPO.no_seq }
                                select new
                                {
                                    ExistPO,
                                    NewPO
                                };

            foreach (var item in itemsToUpdate)
            {
                // Update the product:
                var getdeiskon = item.ExistPO.Diskon4 ?? 0;
                item.ExistPO.harga = item.NewPO.harga;
                item.ExistPO.jml_diskon = (item.NewPO.harga * item.ExistPO.qty) * (getdeiskon / 100);
                item.ExistPO.qty_order = item.NewPO.qty_order;
                item.ExistPO.total = (item.NewPO.harga * item.NewPO.qty_order) - item.ExistPO.jml_diskon;
                item.ExistPO.harga_new = item.NewPO.harga;
                item.ExistPO.total_new = (item.NewPO.harga * item.NewPO.qty_order) - item.ExistPO.jml_diskon;
                var gudangdetail = gudangin.gddetail.Where(w => w.kd_stok == item.ExistPO.kd_stok).FirstOrDefault();
                if (gudangdetail != null)
                {
                    item.ExistPO.qty_kirim = gudangdetail.qty_qc_pass;
                    item.ExistPO.qty_sisa = gudangdetail.qty_order - gudangdetail.qty_in;
                }
                // TODO: Update the stock items if required
            }


            List<PURC_PO_D> FinalResult = itemsToUpdate.Select(x => x.ExistPO).ToList();
            if (FinalResult.Count != 0)
            {
                POExist.detailPO = FinalResult;
            }


            gudangin.gddetail.RemoveAll(x => x.qty_in == 0);

            if (mode == "NEW")
            {
                gudangin.Last_Created_By = UserID;
                gudangin.Last_Create_Date = DateTime.Now;

                try
                {

                    response = await client.CallAPIPost("GUDANG_IN/SaveTerimaBarang", gudangin);
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

            if (response.Success)
            {
                try
                {
                    await client.CallAPIPost("GUDANG_IN/UpdatePO", POExist);
                    INV_GUDANG_IN GdList = new INV_GUDANG_IN();
                    GdList.no_trans = no_transIN;
                   
                    response.Result = no_transIN;
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

            //if (response.Success)
            //{
            //    response.Result = no_transIN;
            //}

            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult LaporanKartuStok()
        {
            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetBarang()
        {
            IEnumerable<SIF_BarangCbo> result = new List<SIF_BarangCbo>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("SIF_Barang/GetSIFBarangCB");
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_BarangCbo>>(response.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult GetTahun()
        {
            List<Shared> result = new List<Shared>();
            ApiClient client = factoryClass.APIClientAccess();
            for (int i = 0; i <= 10; i++)
            {
                Shared tahun = new Shared();
                tahun.key = DateTime.Now.AddYears(-i).Year.ToString();
                tahun.value = DateTime.Now.AddYears(-i).Year.ToString();
                result.Add(tahun);
            }
            //for (int i = 0; i < 6; i++)
            //{
            //    Shared tahun = new Shared();
            //    tahun.key = DateTime.Now.AddYears(i).Year.ToString();
            //    tahun.value = DateTime.Now.AddYears(i).Year.ToString();
            //    result.Add(tahun);
            //}
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult GetBulan()
        {
            List<Shared> result = new List<Shared>();
            ApiClient client = factoryClass.APIClientAccess();
            string[] names = DateTimeFormatInfo.CurrentInfo.MonthNames;
            for (int i = 0; i < names.Length - 1; i++)
            {
                Shared bulan = new Shared();
                if (i + 1 <= 9)
                {
                    bulan.key = "0" + (i + 1).ToString();
                }
                else
                {
                    bulan.key = (i + 1).ToString();
                }
                bulan.value = names[i];
                result.Add(bulan);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDataStokHeader(string kd_stok, string bulan, string tahun)
        {
            IEnumerable<V_StokGudang> result = new List<V_StokGudang>();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            Response response = new Response();
            response = await client.CallAPIGet("INV_Q/GetKartuStok?kd_stok=" + kd_stok + "&bulan=" + bulan + "&tahun=" + tahun + "&kd_cabang=" + BranchID);
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<V_StokGudang>>(response.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetPrintQC(string id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            PrintQC result = new PrintQC();
            IEnumerable<INV_QC> resultD = new List<INV_QC>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            Response responseD = new Response();

            string sHTML = "";

            try
            {
                response = await client.CallAPIGet("INV_Q/GetPrintQC?no_qc=" + id);

                responseD = await client.CallAPIGet("INV_Q/GetDetailQC?no_trans=" + id);


                if (response.Success && responseD.Success)
                {
                    result = JsonConvert.DeserializeObject<PrintQC>(response.Message);
                    resultD = JsonConvert.DeserializeObject<List<INV_QC>>(responseD.Message);

                    //head
                    sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}td, th{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}} .noBorder{border: 0px solid #dddddd;}.paddingtd{padding: 0;padding-bottom:2px;padding-left:8px;padding-top:2px;}.paddingth{padding: 0;padding-bottom:8px;padding-left:8px;}.textCenter{text-align: center;}.textRight{text-align: right;}</style></head> ";

                    //company profile
                    sHTML += " <body><table style='margin-bottom: 20px; '><tr ><td style='width:40%; border: 0px solid #dddddd;' ><h2>" + result.Cabang + "</h2><p>" + result.AlamatCabang + "</p><p>" + result.Telp1 + "</p><p>" + result.Kota + "</p></td><td style='width: 20%;border: 0px solid #dddddd;'></td><td style='width: 40%;border: 0px solid #dddddd;'><h2>Inseksi PO Kedatangan</h2><p>PO No: " + result.no_po + "</p><p>Tanggal QC: " + result.tgl_trans + "</p></td></tr> ";

                    //supplier profile
                    sHTML += " <tr class='headerTable '> <th style='width:40%;' class='noBorder '> SUPPLIER </th> <th style='width: 20%;' class='noBorder '></th> <th style='width: 40%;' class='noBorder '> </th> </tr><tr > <td style='width:40%;' class='noBorder paddingtd'> " + result.NamaSupplier + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> " + result.AlamatPengiriman + " </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> " + result.AlamatSupplier + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> Telp No: " + result.TelpSupplier + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'>  </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr></table> ";

                    //UserDetail
                    sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='noBorder'> TANGGAL KIRIM </th>  </tr>" +
                        " <tr> <td class='noBorder paddingtd'> " + result.tgl_trans.ToString("dd MMMM yyyy") + " </td></tr></table> ";

                    //Notes
                    //sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='noBorder'> NOTES </th> </tr><tr> <td class='noBorder paddingtd'> " + result.Notes + " </td></tr></table> ";

                    //headerDetail
                    sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='textCenter'> BARANG </th> <th class='textCenter'>SATUAN</th> <th class='textCenter'> QTY </th>  </tr> ";

                    //detail
                    foreach (INV_QC detail in resultD)
                    {
                        sHTML += " <tr> <td> " + detail.nama_barang + " </td><td class='textCenter'>" + detail.kd_satuan + "</td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.qty) + " </td></tr> ";
                    }

                    //total
                    //sHTML += " <tr> <td class='textRight noBorder' colspan='5'> SUBTOTAL </td><td class='textRight'> " + string.Format("{0:#,0.00}", result.SubTotal) + "</td></tr><tr> <td class='textRight noBorder' colspan='5'> ONGKOS KIRIM </td><td class='textRight'> " + string.Format("{0:#,0.00}", result.Ongkir) + "</td></tr><tr> <td class='textRight noBorder' colspan='5'> PPN </td><td class='textRight'> " + string.Format("{0:#,0.00}", result.PPN) + "</td></tr><tr> <th class='textRight noBorder' colspan='5'> GRAND TOTAL </th> <th class='textRight'> " + string.Format("{0:#,0.00}", result.GrandTotal) + "</th> </tr> ";

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


        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetPrintKartuStok(string kd_stok, string bulan, string tahun)
        {
            List<V_StokGudang> result = new List<V_StokGudang>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            string sHTML = "";

            decimal sumQtyIn = 0;
            decimal sumQtyOut = 0;
            decimal sumQtySisa = 0;
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            response = await client.CallAPIGet("INV_Q/GetKartuStok?kd_stok=" + kd_stok + "&bulan=" + bulan + "&tahun=" + tahun + "&kd_cabang=" + BranchID);
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<V_StokGudang>>(response.Message);
                //profile
                sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}th{border: 1px solid #dddddd; text-align: center; padding: 8px;}.borderGrid{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}}</style></head><body><table style='margin-bottom: 20px;'><tr style='border-bottom: black solid 5px;'><td style='width: 40%;border: 0px solid #dddddd;' ><h2>" + result.FirstOrDefault().profile.nama + "</h2><p>" + result.FirstOrDefault().profile.alamat + "</p><p>telp: " + result.FirstOrDefault().profile.telp1 + "</p><p>fax: " + result.FirstOrDefault().profile.fax1 + "</p><p>" + result.FirstOrDefault().profile.kota + " - " + result.FirstOrDefault().profile.propinsi + "</p></td></tr></table><table style='margin-bottom: 20px;'><tr><td style='text-align:right;'>Periode:" + result.FirstOrDefault().bultah + "</td></tr><tr><td style='text-align:center;font-size:20px;font-weight:bold;'>KARTU STOK</td></tr></table>";

                //header
                sHTML += "<table style='margin-bottom: 20px;'><tr><td style='width: 100px;font-size: 14px;'>Nama Barang:</td><td style='font-size: 14px;font-weight: bold !important;'>" + result.FirstOrDefault().Nama_Barang + "</td></tr><tr><td style='font-size: 14px;'>Kode Barang:</td><td style='font-size: 14px;font-weight: bold !important;'>" + result.FirstOrDefault().Kode_Barang + "</td><td style='font-size: 14px;text-align: right;'>Saldo Awal:</td><td style='font-size: 14px;font-weight: bold !important;text-align: right;width: 60px;'>" + result.FirstOrDefault().awal_qty_onstok + "</td></tr></table>";

                //header table
                sHTML += "<table> <tr class='headerTable'> <th>Tanggal</th><th>No Bukti</th> <th>Nama Toko/Gudang</th> <th>Keterangan</th><th>Qty Masuk</th> <th>Qty Keluar</th><th>Qty Sisa</th> </tr>";

                //detail
                foreach (vy_saldocard detail in result.FirstOrDefault().ListSaldo)
                {
                    sHTML += " <tr> <td class='borderGrid' style='min-width:100px'>" + detail.Tanggal.ToString("dd MMMM yyyy") + "</td><td class='borderGrid'>" + detail.no_trans + "</td><td class='borderGrid'>" + detail.Atas_Nama + "</td><td class='borderGrid'>" + detail.keterangan + "</td><td class='borderGrid' style='text-align: right;'>" + detail.qty_in + "</td><td class='borderGrid' style='text-align: right;'>" + detail.qty_out + "</td><td class='borderGrid' style='text-align: right;'>" + detail.qty_sisa + "</td></tr>";
                    sumQtyIn += detail.qty_in;
                    sumQtyOut += detail.qty_out;
                }
                sumQtySisa = (sumQtyIn + result.FirstOrDefault().awal_qty_onstok) - sumQtyOut;
                //summary
                sHTML += " <tr> <td colspan='4' class='borderGrid' style='text-align: right;font-weight: 700;'>TOTAL</td><td class='borderGrid' style='text-align: right;font-weight: 700;'>" + sumQtyIn + "</td><td class='borderGrid' style='text-align: right;font-weight: 700;'>" + sumQtyOut + "</td><td class='borderGrid' style='text-align: right;font-weight: 700;'>" + sumQtySisa + "</td></tr> ";

                //end table
                sHTML += " </table> ";

                //end
                sHTML += " </body></html> ";
            }
            return Ok(sHTML);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult LaporanKartuStokGudang()
        {
            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDataStokHeaderGudang(string kd_stok, string bulan, string tahun, string gudang = null)
        {
            IEnumerable<V_StokGudang> result = new List<V_StokGudang>();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;

            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            Response response = new Response();
            string kd_gudang = "";



            if (gudang == null || gudang == string.Empty)
            {
                var ret = await client.CallAPIGet("Helper/GetGudangFromCabang?kd_cabang=" + BranchID);
                if (ret.Success)
                {
                    kd_gudang = JsonConvert.DeserializeObject<string>(ret.Message);
                }
                gudang = kd_gudang;
            }

            response = await client.CallAPIGet("INV_Q/GetKartuStokGudang?kd_stok=" + kd_stok + "&bulan=" + bulan + "&tahun=" + tahun + "&gudang=" + gudang);
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<V_StokGudang>>(response.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetPrintKartuStokGudang(string kd_stok, string bulan, string tahun, string gudang, string gudangName)
        {
            List<V_StokGudang> result = new List<V_StokGudang>();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            Response response = new Response();
            string kd_gudang = "";


            if (string.IsNullOrEmpty(gudang))
            {
                var ret = await client.CallAPIGet("Helper/GetGudangFromCabang?kd_cabang=" + BranchID);
                if (ret.Success)
                {
                    kd_gudang = JsonConvert.DeserializeObject<string>(ret.Message);
                }
                gudang = kd_gudang;
            }


            if (gudangName == null || gudangName == string.Empty)
            {
                var ret2 = await client.CallAPIGet("Helper/GetNamaGudang?kd_cabang=" + BranchID);
                if (ret2.Success)
                {
                    kd_gudang = JsonConvert.DeserializeObject<string>(ret2.Message);
                }
                gudangName = kd_gudang;
            }

            string sHTML = "";

            decimal sumQtyIn = 0;
            decimal sumQtyOut = 0;
            decimal sumQtySisa = 0;

            response = await client.CallAPIGet("INV_Q/GetKartuStokGudang?kd_stok=" + kd_stok + "&bulan=" + bulan + "&tahun=" + tahun + "&gudang=" + gudang + "&kd_cabang=" + BranchID);
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<V_StokGudang>>(response.Message);
                //profile
                sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}th{border: 1px solid #dddddd; text-align: center; padding: 8px;}.borderGrid{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}}</style></head><body><table style='margin-bottom: 20px;'><tr style='border-bottom: black solid 5px;'><td style='width: 40%;border: 0px solid #dddddd;' ><h2>" + result.FirstOrDefault().profile.nama + "</h2><p>" + result.FirstOrDefault().profile.alamat + "</p><p>telp: " + result.FirstOrDefault().profile.telp1 + "</p><p>fax: " + result.FirstOrDefault().profile.fax1 + "</p><p>" + result.FirstOrDefault().profile.kota + " - " + result.FirstOrDefault().profile.propinsi + "</p></td></tr></table><table style='margin-bottom: 20px;'><tr><td style='text-align:right;'>Periode:" + result.FirstOrDefault().bultah + "</td></tr><tr><td style='text-align:center;font-size:20px;font-weight:bold;'>KARTU STOK</td></tr></table>";

                //header
                sHTML += "<table style='margin-bottom: 20px;'><tr><td style='width: 100px;font-size: 14px;'>Gudang:</td><td style='font-size: 14px;font-weight: bold !important;'> " + gudangName + "</td></tr><tr><td style='width: 100px;font-size: 14px;'>Nama Barang:</td><td style='font-size: 14px;font-weight: bold !important;'>" + result.FirstOrDefault().Nama_Barang + "</td></tr><tr><td style='font-size: 14px;'>Kode Barang:</td><td style='font-size: 14px;font-weight: bold !important;'>" + result.FirstOrDefault().Kode_Barang + "</td><td style='font-size: 14px;text-align: right;'>Saldo Awal:</td><td style='font-size: 14px;font-weight: bold !important;text-align: right;width: 60px;'>" + result.FirstOrDefault().awal_qty_onstok + "</td></tr></table>";

                //header table
                sHTML += "<table> <tr class='headerTable'> <th>Tanggal</th><th>No Bukti</th> <th>Nama Toko/Gudang</th> <th>Keterangan</th><th>Qty Masuk</th> <th>Qty Keluar</th><th>Qty Sisa</th> </tr>";

                //detail
                foreach (vy_saldocard detail in result.FirstOrDefault().ListSaldo)
                {
                    sHTML += " <tr> <td class='borderGrid' style='min-width:100px'>" + detail.Tanggal.ToString("dd MMMM yyyy") + "</td><td class='borderGrid'>" + detail.no_trans + "</td><td class='borderGrid'>" + detail.Atas_Nama + "</td><td class='borderGrid'>" + detail.keterangan + "</td><td class='borderGrid' style='text-align: right;'>" + detail.qty_in + "</td><td class='borderGrid' style='text-align: right;'>" + detail.qty_out + "</td><td class='borderGrid' style='text-align: right;'>" + detail.qty_sisa + "</td></tr>";
                    sumQtyIn += detail.qty_in;
                    sumQtyOut += detail.qty_out;
                }
                sumQtySisa = (sumQtyIn + result.FirstOrDefault().awal_qty_onstok) - sumQtyOut;
                //summary
                sHTML += " <tr> <td colspan='4' class='borderGrid' style='text-align: right;font-weight: 700;'>TOTAL</td><td class='borderGrid' style='text-align: right;font-weight: 700;'>" + sumQtyIn + "</td><td class='borderGrid' style='text-align: right;font-weight: 700;'>" + sumQtyOut + "</td><td class='borderGrid' style='text-align: right;font-weight: 700;'>" + sumQtySisa + "</td></tr> ";

                //end table
                sHTML += " </table> ";

                //end
                sHTML += " </body></html> ";
            }
            return Ok(sHTML);
        }
    }
}