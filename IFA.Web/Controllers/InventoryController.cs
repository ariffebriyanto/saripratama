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
    public class InventoryController : BaseController
    {
        public InventoryController(FactoryClass factoryClass,
           IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Create(string id = "", string mode = "")
        {
            INV_GUDANG_IN terima = new INV_GUDANG_IN();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            Response response = new Response();

            response = await client.CallAPIGet("INV_Q/GetNotrans?kd_cabang=" + BranchID);

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
            ViewBag.BranchUser = BranchID.ToString().PadLeft(6, '0').Trim();
            return View(terima);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDetailQC(string id)
        {

            IEnumerable<INV_QC_M> result = new List<INV_QC_M>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            string kd_gudang = "";

            var ret = await client.CallAPIGet("Helper/GetGudangFromCabang?kd_cabang=" + BranchID);
            if (ret.Success)
            {
                kd_gudang = JsonConvert.DeserializeObject<string>(ret.Message);
            }
            response = await client.CallAPIGet("GUDANG_IN/GetQc?no_trans=" + id + "&kd_gudang=" + kd_gudang);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_QC_M>>(response.Message);
            }


            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveTerimaBarang(INV_GUDANG_IN data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var mode = "";
            string no_trans = "";
            data.no_ref = data.no_ref;
            data.no_qc = data.no_qc;
            data.tipe_trans = "JPP-KUT-01";
            data.penyerah = data.penyerah;
            data.keterangan = data.keterangan;
            data.blthn = DateTime.Now.ToString("yyyyMM");
            data.Last_Create_Date = DateTime.Now;
            data.tgl_trans = data.tgl_trans;
            data.kode_gudang = data.kode_gudang;
            data.jml_qtypo = data.jml_qtypo;
            data.jml_qtyin = data.jml_qtyin;

           

            if (data.p_np == "Y")
            {
                if (data.no_trans == null)
                {
                    mode = "NEW";
                    response = await client.CallAPIGet("Helper/GetNoTrans?prefix=JPPP&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                    if (response.Success)
                    {
                        no_trans = JsonConvert.DeserializeObject<string>(response.Message);
                       
                    }
                    data.no_trans = no_trans;

                }

            }
            else  
            {
                if (data.no_trans == null)
                {
                    mode = "NEW";
                    response = await client.CallAPIGet("Helper/GetNoTrans?prefix=JPPNP&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                    if (response.Success)
                    {
                        no_trans = JsonConvert.DeserializeObject<string>(response.Message);

                    }
                    data.no_trans = no_trans;

                }
            }
            
            data.Kd_Cabang = BranchID;

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


            List <PURC_PO_D> PODetailNew = new List<PURC_PO_D>();

            for (int i = 0; i <= data.gddetail.Count() - 1; i++)
            {
                PURC_PO_D model = new PURC_PO_D();
                model.kd_stok = data.gddetail[i].kd_stok;
                model.Kd_Cabang = BranchID;
                model.harga = data.gddetail[i].harga;
                model.no_po = data.no_ref;
                model.qty_order = data.gddetail[i].qty_order;
                model.no_seq = data.gddetail[i].no_seq;
                PODetailNew.Add(model);

                data.gddetail[i].Kd_Cabang = BranchID;
                data.gddetail[i].tipe_trans = "JPU-KUT-01";
                data.gddetail[i].no_seq = data.gddetail[i].no_seq;
                data.gddetail[i].kd_stok = data.gddetail[i].kd_stok;
                data.gddetail[i].no_qc = data.no_qc;
                data.gddetail[i].no_ref = data.no_ref;
                data.gddetail[i].keterangan = data.gddetail[i].keterangan;
                data.gddetail[i].kd_satuan = data.gddetail[i].kd_satuan;
                data.gddetail[i].kd_ukuran = data.gddetail[i].kd_ukuran;
                data.gddetail[i].no_trans = data.no_trans;
                data.gddetail[i].qty_sisa = data.gddetail[i].qty_sisa;
                data.gddetail[i].qty_in = data.gddetail[i].qty_qc_pass;
                data.gddetail[i].qty_order = data.gddetail[i].qty_order;
                data.gddetail[i].harga = data.gddetail[i].harga;
                data.gddetail[i].rp_trans = data.gddetail[i].rp_trans;
                data.gddetail[i].gudang_asal = data.gddetail[i].gudang_asal;
                data.gddetail[i].gudang_tujuan = data.kode_gudang;
                data.gddetail[i].kd_buku_besar = data.gddetail[i].kd_buku_besar;
                data.gddetail[i].kd_buku_biaya = data.gddetail[i].kd_buku_biaya;
                data.gddetail[i].blthn = DateTime.Now.ToString("yyyyMM");
                data.gddetail[i].Last_Created_By = UserID;
                data.gddetail[i].Last_Create_Date = DateTime.Now;


            }

            PO.podetail = PODetailNew;

            PURC_PO POExist = new PURC_PO();
            List<PURC_PO_D> PODetailExist = new List<PURC_PO_D>();
            response = await client.CallAPIGet("PURC_PO/GetPOPenerimaan?no_po=" + data.no_ref);

            if (response.Success)
            {
                POExist = JsonConvert.DeserializeObject<PURC_PO>(response.Message);
            }

            response = await client.CallAPIGet("PURC_PO/GetDetailPOPenerimaan?no_po=" + data.no_ref );

                if (response.Success)
                {
                PODetailExist = JsonConvert.DeserializeObject<List<PURC_PO_D>>(response.Message);
                }


            var itemsToUpdate = from ExistPO in PODetailExist
                                join NewPO in PODetailNew
                                on new { ExistPO.no_po,ExistPO.kd_stok,ExistPO.no_seq} equals new { NewPO.no_po,NewPO.kd_stok,NewPO.no_seq}
                                select new
                                {
                                    ExistPO,
                                    NewPO
                                };

            foreach (var item in itemsToUpdate)
            {
                // Update the product:
                var getdeiskon = item.ExistPO.Diskon4??0;
                item.ExistPO.harga = item.NewPO.harga;
                item.ExistPO.jml_diskon = (item.NewPO.harga * item.ExistPO.qty) * (getdeiskon/100);
                item.ExistPO.qty_order= item.NewPO.qty_order;
                item.ExistPO.total = (item.NewPO.harga * item.NewPO.qty_order)- item.ExistPO.jml_diskon;
                item.ExistPO.harga_new = item.NewPO.harga;
                item.ExistPO.total_new = (item.NewPO.harga * item.NewPO.qty_order) - item.ExistPO.jml_diskon;
                var gudangdetail = data.gddetail.Where(w => w.kd_stok == item.ExistPO.kd_stok).FirstOrDefault();
                if(gudangdetail != null)
                {
                    item.ExistPO.qty_kirim = gudangdetail.qty_qc_pass;
                    item.ExistPO.qty_sisa = gudangdetail.qty_order - gudangdetail.qty_in;
                }
                // TODO: Update the stock items if required
            }


            List<PURC_PO_D> FinalResult = itemsToUpdate.Select(x => x.ExistPO).ToList();
            if  (FinalResult.Count!=0)
            {
                POExist.detailPO = FinalResult;
            }
           
            
            data.gddetail.RemoveAll(x => x.qty_in == 0);

            if (mode == "NEW")
            {
                data.Last_Created_By = UserID;
                data.Last_Create_Date = DateTime.Now;
              
                try
                {

                    response = await client.CallAPIPost("GUDANG_IN/SaveTerimaBarang", data);
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
                    await client.CallAPIPost("GUDANG_IN/UpdatePO",POExist);
                    INV_GUDANG_IN GdList = new INV_GUDANG_IN();
                    GdList.no_trans = data.no_trans;
                    //diubah di exec saat save biar sekali pangil api (y2k)
                    //await client.CallAPIPost("GUDANG_IN/InsertNota_Beli", GdList);
                    //await client.CallAPIPost("GUDANG_IN/SP_Status_PO", data);
                    response.Result = no_trans;
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

        [Authorize(Roles = "Admin, User, UAT, PENJUALAN, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetPeriode()
        {
            IEnumerable<Periode_Buku> result = new List<Periode_Buku>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            response = await client.CallAPIGet("Helper/GetPeriodeBuku");
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<Periode_Buku>>(response.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, PENJUALAN, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetBarang()
        {
            IEnumerable<SIF_BarangGudangCbo> result = new List<SIF_BarangGudangCbo>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            response = await client.CallAPIGet("SIF_Barang/GetBarangGudang");
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_BarangGudangCbo>>(response.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, PENJUALAN, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Pembatalan(string id)
        {
            IEnumerable<INV_GUDANG_OUT> data = new List<INV_GUDANG_OUT>();
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            INV_GUDANG_OUT gd_out = new INV_GUDANG_OUT();
            //IEnumerable<INV_GUDANG_OUT> gd = new List<INV_GUDANG_OUT>();
            List<INV_GUDANG_OUT_D> detail = new List<INV_GUDANG_OUT_D>();

            var cb = BranchID;
            var by = UserID;


            try
            {
                response = await client.CallAPIGet("INV_GUDANG_OUT/GetGudangOut?no_trans=" + id);

                if (response.Success)
                {
                    gd_out = JsonConvert.DeserializeObject<List<INV_GUDANG_OUT>>(response.Message).FirstOrDefault();
                    gd_out.Last_Updated_By = UserID;
                    //gd_out.detail.
                    //sj. = "BATAL";
                    //sj.Program_name = "Pembatalan SJK";
                    //response = await client.CallAPIGet("SALES_SJ/GetDtlSJ_del?no_sj=" + id);
                    detail = gd_out.detail.ToList();

                    if (response.Success)
                    {
                        //detil = JsonConvert.DeserializeObject<List<INV_GUDANG_OUT_D>>(response.Message);
                        if (detail != null && detail.Count() > 0)
                        {
                            //data.FirstOrDefault().tgl_transdesc = data.FirstOrDefault().tgl_trans.ToString("dd MMMM yyyy");

                           // detail = gd_out.detail.FirstOrDefault();
                            //foreach (var item in detail)
                            //{
                            //    gd_out.detail.Add(item);
                            //}
                            response = await client.CallAPIPost("INV_GUDANG_OUT/Pembatalan", gd_out);
                           // response = await client.CallAPIPost("SALES_SJ/PembatalanSJK", sj);
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
                    response.Success = false;
                    response.Message = "Data Tidak Ditemukan";
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
        public async Task<IActionResult> StokGudang()
        {
            ApiClient client = factoryClass.APIClientAccess();

            Response response = new Response();

            response = await client.CallAPIGet("SIF_Barang/GetBarangGudang");

            if (response.Success)
            {
                ViewBag.BarangList = response.Message;

            }

            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, PENJUALAN, SPV, LOGISTIK")]
        public async Task<IActionResult> GetStokGudang(string Kode_Barang, string blnthn)
        {
            IEnumerable<V_MonStok> result = new List<V_MonStok>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            if (blnthn == null)
            {
                response = await client.CallAPIGet("Helper/GetBlthn");
                if (response.Success)
                {
                    blnthn = JsonConvert.DeserializeObject<string>(response.Message);
                }

                
            }

            if (blnthn != null)
            {
                response = await client.CallAPIGet("GUDANG_IN/GetstokGudang?Kode_Barang=" + Kode_Barang + "&blnthn=" + blnthn + "&cb=" + BranchID);
            }

             
            
            

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<V_MonStok>>(response.Message);
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, PENJUALAN, SPV, LOGISTIK")]
        public async Task<IActionResult> GetDetailStokGudang(string Kode_Barang,string blnthn)
        {
            IEnumerable<V_MonStokDetail> result = new List<V_MonStokDetail>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("GUDANG_IN/GetDetailstokGudang?Kode_Barang=" + Kode_Barang + "&blnthn=" + blnthn);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<V_MonStokDetail>>(response.Message);
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult StokAllGudang()
        {
         
            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, PENJUALAN, SPV, LOGISTIK")]
        public async Task<IActionResult> GetStokAllGudang(string Kode_Barang, string blnthn)
        {
            IEnumerable<StokAllGudang> result = new List<StokAllGudang>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("GUDANG_IN/GetStokAllGudang?Kode_Barang=" + Kode_Barang + "&blnthn=" + blnthn);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<StokAllGudang>>(response.Message);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetTerima(string id,DateTime? DateFrom= null,DateTime? DateTo=null,string program_name=null)
        {
            IEnumerable<INV_GUDANG_IN> result = new List<INV_GUDANG_IN>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            response = await client.CallAPIGet("GUDANG_IN/GetTerima?no_trans=" + id +"&DateFrom=" + DateFrom + "&DateTo=" + DateTo +"&kd_cabang="+BranchID+"&program_name="+program_name);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_GUDANG_IN>>(response.Message);
                if (result.Count() > 0)
                {
                    result.FirstOrDefault().tgl_transdesc = result.FirstOrDefault().tgl_trans.ToString("dd MMMM yyyy");
                }
                   



            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetDetailTerima(string id, DateTime? DateFrom=null, DateTime? DateTo=null)
        {
            IEnumerable<INV_GUDANG_IN_D> result = new List<INV_GUDANG_IN_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("GUDANG_IN/GetDetailTerima?no_trans=" + id + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_GUDANG_IN_D>>(response.Message);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> StokOpname(string id = "", string mode = "")
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

            string kd_gudang = "";
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var ret = await client.CallAPIGet("Helper/GetGudangFromCabang?kd_cabang=" + BranchID);
            if (ret.Success)
            {
                kd_gudang = JsonConvert.DeserializeObject<string>(ret.Message);
            }
            ViewBag.Gudang = kd_gudang;

            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetSatuan()
        {
            IEnumerable<SIF_Satuan> result = new List<SIF_Satuan>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = await client.CallAPIGet("SIF_Satuan/GetSIFSatuanCbo");
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Satuan>>(response.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveOpname(INV_OPNAME data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            string transno = "";
            var mode = "";


            if (data.no_trans == null)
            {
                mode = "NEW";
                response = await client.CallAPIGet("Helper/GetNoTrans?prefix=OPN&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                if (response.Success)
                {
                    transno = JsonConvert.DeserializeObject<string>(response.Message);
                }
                data.no_trans = transno;
            }
            else
            {
                transno = data.no_trans;
            }

            data.Kd_Cabang = BranchID;
            data.bultah = DateTime.Now.ToString("yyyyMM");
            data.Last_Created_By = UserID;
            data.Last_Create_Date = DateTime.Now;

            for (int i = 0; i <= data.opnamedtl.Count() - 1; i++)
            {
                data.opnamedtl[i].Kd_Cabang = BranchID;
                data.opnamedtl[i].no_trans = data.no_trans;
                data.opnamedtl[i].no_seq = i + 1;
                data.opnamedtl[i].bultah = DateTime.Now.ToString("yyyyMM");
                data.opnamedtl[i].tgl_trans = DateTime.Now.Date;
                data.opnamedtl[i].Last_Created_By = UserID;
                data.opnamedtl[i].Last_Create_Date = DateTime.Now;

                //dummy
                data.opnamedtl[i].kode_gudang = data.gudang;
                // data.opnamedtl[i].qty_data = 0;
                data.opnamedtl[i].nilai_rata = 0;
                data.opnamedtl[i].nilai_manual = 0;
                data.opnamedtl[i].total = 0;

            }

            if (mode == "NEW")
            {
                try
                {
                    response = await client.CallAPIPost("INV_OPNAME/SaveOpname", data);
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
                response.Result = transno;
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetBarangSaldo(string cb=null)
        {
            IEnumerable<SIF_BarangCbo> result = new List<SIF_BarangCbo>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
           // string BranchID = "";

            ////var UserID = claimsIdentity.FindFirst("UserID").Value;
            //if (cb == null)
            //{
            //    BranchID = claimsIdentity.FindFirst("BranchID").Value;

            //}
            //else
            //{
            //    var ret = await client.CallAPIGet("Helper/GetCabangFromGudang?kd_gudang=" + cb);
            //    if (ret.Success)
            //    {
            //        BranchID = JsonConvert.DeserializeObject<string>(ret.Message);
            //    }
            //}

            response = await client.CallAPIGet("SIF_Barang/GetSIFBarangCboSaldoGudang?cb=" + cb);
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_BarangCbo>>(response.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetOpname(INV_OPNAME data)
        {
            IEnumerable<INV_OPNAME> result = new List<INV_OPNAME>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();

            response = await client.CallAPIGet("INV_OPNAME/GetOpname?no_trans=" + data.no_trans);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_OPNAME>>(response.Message);

                if (result.Count() > 0)
                {
                    result.FirstOrDefault().tgl_transdesc = result.FirstOrDefault().tgl_trans.ToString("dd MMMM yyyy");
                }
            }


            ViewBag.Mode = "EDIT";
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult MonitoringOpname()
        {
          
            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetMonOpname()
        {
            IEnumerable<INV_OPNAME> result = new List<INV_OPNAME>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            response = await client.CallAPIGet("INV_OPNAME/GetMonOpname?kd_cabang=" + BranchID);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_OPNAME>>(response.Message);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetMonOpnameDTL()
        {
            IEnumerable<INV_OPNAME_DTL> result = new List<INV_OPNAME_DTL>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            response = await client.CallAPIGet("INV_OPNAME/GetMonOpnameDTL?kd_cabang=" + BranchID);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_OPNAME_DTL>>(response.Message);
            }
            return Ok(result);
        }


        [Authorize(Roles = "Admin, User, UAT, PENJUALAN, SPV, LOGISTIK")]
        public async Task<IActionResult> GetGudang(string filter = null)
        {
            IEnumerable<SIF_Gudang> result = new List<SIF_Gudang>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            response = await client.CallAPIGet("Helper/GetGudang");
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Gudang>>(response.Message);
                if(filter != null)
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result.Where(w => w.Kd_Cabang == BranchID).ToList();
                    }
                }
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, PENJUALAN, SPV, LOGISTIK")]
        public async Task<IActionResult> GetGudangDefaultByCabang(string filter = null)
        {
            IEnumerable<SIF_Gudang> result = new List<SIF_Gudang>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            response = await client.CallAPIGet("Helper/GetGudangDefaultByCabang/?cabang=" + BranchID);
            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Gudang>>(response.Message);
                if (filter != null)
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result.Where(w => w.Kd_Cabang == BranchID).ToList();
                    }
                }
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult MutasiCabang(string id = "", string mode = "")
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

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveMutasiCabang(INV_GUDANG_OUT data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            string transno = "";
            var mode = "";

            //string gd_asal = "";

            //var ret = await client.CallAPIGet("Helper/GetGudangFromCabang?kd_cabang=" + BranchID);
            //if (ret.Success)
            //{
            //    gd_asal = JsonConvert.DeserializeObject<string>(ret.Message);
            //}



            if (data.no_trans == null)
            {
                mode = "NEW";
                response = await client.CallAPIGet("Helper/GetNoTrans?prefix=BK&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                if (response.Success)
                {
                    transno = JsonConvert.DeserializeObject<string>(response.Message);
                }
                data.no_trans = transno;
            }
            else
            {
                transno = data.no_trans;
            }
            //data.gudang = kd_gudang;
            data.Kd_Cabang = BranchID;
            data.tipe_trans = "JPB-KUT-02";
            data.tgl_trans = DateTime.Now;
            data.tgl_posting = DateTime.Now;
            data.no_posting = transno;
            data.jml_rp_trans = 0;
            data.blthn = DateTime.Now.ToString("yyyyMM");
            data.Last_Created_By = UserID;
            data.Last_Create_Date = DateTime.Now;
            //data.sudah_sj = 1;
            //data.gudang_tujuan=

            for (int i = 0; i <= data.detail.Count() - 1; i++)
            {
                data.detail[i].Kd_Cabang = BranchID;
                data.detail[i].no_trans = data.no_trans;
                data.detail[i].no_seq = i + 1;
                data.detail[i].tipe_trans = "JPB-KUT-02";
                data.detail[i].qty_sisa = 0;
                data.detail[i].qty_order = data.detail[i].qty_out;
                data.detail[i].blthn = DateTime.Now.ToString("yyyyMM");
                data.detail[i].no_ref = transno;
                data.detail[i].gudang_asal = data.gudang_asal;
                //data.detail[i].gudang_tujuan = "EXP01";// set ke gudang sementara expedisi mobil truk
                data.detail[i].gudang_tujuan = data.gudang_tujuan;

                data.detail[i].kd_buku_besar = data.detail[i].rek_persediaan;
                data.detail[i].rp_trans = data.detail[i].harga * data.detail[i].qty_out;

                data.detail[i].Last_Created_By = UserID;
                data.detail[i].Last_Create_Date = DateTime.Now;
                data.detail[i].Program_Name = "Frm_BMB";


            }


            //if (mode == "NEW")
            //{
            //    response = await client.CallAPIPost("INV_GUDANG_OUT/MoveGudang_OUT", data);
            //}

            if (mode == "NEW")
            {
                response = await client.CallAPIPost("INV_GUDANG_OUT/SaveGUDANG_OUT", data);
            }

            if (response.Success)
            {
                response.Result = transno;
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult MMutasiCabang()
        {
            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetMutasiGudang(INV_GUDANG_OUT data)
        {
            IEnumerable<INV_GUDANG_OUT> result = new List<INV_GUDANG_OUT>();
            ApiClient client = factoryClass.APIClientAccess();

            Response response = new Response();

            response = await client.CallAPIGet("INV_GUDANG_OUT/GetGudangOut?no_trans=" + data.no_trans);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_GUDANG_OUT>>(response.Message);

                if (result.Count() > 0)
                {
                    result.FirstOrDefault().tgl_transdesc = result.FirstOrDefault().tgl_trans.ToString("dd MMMM yyyy");
                }
            }


            ViewBag.Mode = "EDIT";
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetMonMutasiGudang(string id, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            IEnumerable<INV_GUDANG_OUT> result = new List<INV_GUDANG_OUT>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            var tipe_trans = "JPB-KUT-02";
            response = await client.CallAPIGet("INV_GUDANG_OUT/GetGudangOut?no_trans=" + id + "&tipe_trans=" + tipe_trans + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&cb=" + BranchID);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_GUDANG_OUT>>(response.Message);
               
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetMonMutasiGudangD(DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            IEnumerable<INV_GUDANG_OUT_D> result = new List<INV_GUDANG_OUT_D>();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            Response response = new Response();
            INV_GUDANG_OUT data = new INV_GUDANG_OUT();
            data.tipe_trans = "JPB-KUT-02";

            response = await client.CallAPIGet("INV_GUDANG_OUT/GetGudangOutD?tipe_trans=" + data.tipe_trans + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&cb=" + BranchID);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_GUDANG_OUT_D>>(response.Message);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult MutasiBebas(string id = "", string mode = "")
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
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public IActionResult TerimaBebasIndex()
        {
            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> TerimaBebas(string id = "", string mode = "")
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            INV_GUDANG_IN terima = new INV_GUDANG_IN();
            ApiClient client = factoryClass.APIClientAccess();

            
            Response response = new Response();

            response = await client.CallAPIGet("Helper/GetGudang");
            if (response.Success)
            {
                ViewBag.GudangList = response.Message;

            }
            response = null;
            response = await client.CallAPIGet("SIF_Satuan/GetSIFSatuanCbo?kode_barang=");
            if (response.Success)
            {
                ViewBag.SatuanList = response.Message;

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

            ViewBag.BranchUser = BranchID.ToString().PadLeft(6, '0').Trim();
            return View(terima);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetBarangCbo()
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
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<IActionResult> SaveTerimaBebas(INV_GUDANG_IN data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var mode = "";
            string no_trans = "";
            string gd_login = "";

            var ret = await client.CallAPIGet("Helper/GetGudangFromCabang?kd_cabang=" + BranchID);
            if (ret.Success)
            {
                gd_login = JsonConvert.DeserializeObject<string>(ret.Message);
            }


            data.tipe_trans = "JPB-KUT-02";
            data.Program_Name = "FRM_TRMBEBAS";
        
            data.keterangan = data.keterangan;
            data.blthn = DateTime.Now.ToString("yyyyMM");
            data.Last_Create_Date = DateTime.Now;
            data.tgl_trans = data.tgl_trans;
            data.kode_gudang = gd_login;


                    response = await client.CallAPIGet("Helper/GetNoTrans?prefix=BM&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
                    if (response.Success)
                    {
                        no_trans = JsonConvert.DeserializeObject<string>(response.Message);

                    }
           data.no_trans = no_trans;

          
        

            data.Kd_Cabang = BranchID;
            data.gddetail.RemoveAll(x => x.qty_in == 0);
            for (int i = 0; i <= data.gddetail.Count() - 1; i++)
            {

                data.gddetail[i].Kd_Cabang = BranchID;
                data.gddetail[i].tipe_trans = data.tipe_trans;
                data.gddetail[i].no_seq = i + 1;
                data.gddetail[i].kd_stok = data.gddetail[i].kd_stok;
                data.gddetail[i].Program_Name = data.Program_Name;
                data.gddetail[i].rek_persediaan = data.gddetail[i].rek_persediaan;
                data.gddetail[i].kd_buku_besar = data.gddetail[i].rek_persediaan;
                data.gddetail[i].gudang_tujuan = data.kode_gudang; // 00000
                data.gddetail[i].keterangan = data.gddetail[i].keterangan;
                data.gddetail[i].kd_satuan = data.gddetail[i].kd_satuan;
                data.gddetail[i].no_trans = data.no_trans;
                data.gddetail[i].harga = data.gddetail[i].harga;
                data.gddetail[i].qty_in = data.gddetail[i].qty_in;
                data.gddetail[i].rp_trans = data.gddetail[i].rp_trans;
                data.gddetail[i].gudang_tujuan = gd_login;
                data.gddetail[i].blthn = DateTime.Now.ToString("yyyyMM");
                data.gddetail[i].Last_Created_By = UserID;
                data.gddetail[i].Last_Create_Date = DateTime.Now;


            }
                response = await client.CallAPIPost("GUDANG_IN/SaveTerimaBarang", data);
            if (response.Success)
            {
                response.Result = no_trans;
            }

            return Ok(response);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetPrintTerima(string id)
        {
            PrintTerima result = new PrintTerima();
            IEnumerable<INV_GUDANG_IN_D> resultD = new List<INV_GUDANG_IN_D>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            Response responseD = new Response();

            string sHTML = "";

          
                response = await client.CallAPIGet("GUDANG_IN/GetPrintTerima?no_trans=" + id);

                responseD = await client.CallAPIGet("GUDANG_IN/GetDetailTerima?no_trans=" + id);


                if (response.Success && responseD.Success)
                {
                    result = JsonConvert.DeserializeObject<PrintTerima>(response.Message);
                    resultD = JsonConvert.DeserializeObject<List<INV_GUDANG_IN_D>>(responseD.Message);

                    //head
                    sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}td, th{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}} .noBorder{border: 0px solid #dddddd;}.paddingtd{padding: 0;padding-bottom:2px;padding-left:8px;padding-top:2px;}.paddingth{padding: 0;padding-bottom:8px;padding-left:8px;}.textCenter{text-align: center;}.textRight{text-align: right;}</style></head> ";

                    //company profile
                    sHTML += " <body><table style='margin-bottom: 20px; '><tr ><td style='width:40%; border: 0px solid #dddddd;' ><h2>" + result.Cabang + " ("+ result.atas_nama + ")</h2><p>" + result.AlamatCabang + "</p><p>" + result.Telp1 + "</p><p>" + result.Kota + "</p></td><td style='width: 20%;border: 0px solid #dddddd;'></td><td style='width: 40%;border: 0px solid #dddddd;'><h2>Terima Barang</h2><p>PO No: " + result.PONumber + "</p><p>Tanggal Terima: " + result.Tanggaltrans+ "</p></td></tr> ";

                    //supplier profile
                    sHTML += " <tr class='headerTable '> <th style='width:40%;' class='noBorder '> SUPPLIER </th> <th style='width: 20%;' class='noBorder '></th> <th style='width: 40%;' class='noBorder '> ALAMAT PENGIRIMAN </th> </tr><tr > <td style='width:40%;' class='noBorder paddingtd'> " + result.Nama_Supplier + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> " + result.POKeterangan + " </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> " + result.AlamatSupplier + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> Telp No: " + result.No_Telepon1 + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr><tr ><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr></table> ";

                    //UserDetail
                  

                    //Notes
                    sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='noBorder'> NOTES </th> </tr><tr> <td class='noBorder paddingtd'> " + result.Keterangan + " </td></tr></table> ";

                    //headerDetail
                    sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='textCenter'> BARANG </th> <th class='textCenter'>SATUAN</th> <th class='textCenter'> QTY PO </th> <th class='textCenter'> QTY TERIMA </th> <th class='textCenter'> LOKASI SIMPAN </th> </tr> ";

                    //detail
                    foreach (INV_GUDANG_IN_D detail in resultD)
                    {
                        sHTML += " <tr> <td> " + detail.Nama_Barang + " </td><td class='textCenter'>" + detail.kd_satuan + "</td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.qty_order) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.qty_in) + " </td><td> " + detail.gudang_tujuan + " </td></tr> ";
                    }

                    //total
                  

                    //endDetail
                    sHTML += " </table> ";

                    //end
                    sHTML += " </body></html> ";
                }
            ViewBag.Mode = "EDIT";
            return Ok(sHTML);
        }

        public IActionResult KalkulasiStok(string kd_stok)
        {
            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> PostKalkulasi(string id)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            KalkulasiStokVM data = new KalkulasiStokVM();
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            string transno = "";
            var mode = "";
            string kd_gudang = "";

            var ret = await client.CallAPIGet("Helper/GetGudangFromCabang?kd_cabang=" + BranchID);
            if (ret.Success)
            {
                kd_gudang = JsonConvert.DeserializeObject<string>(ret.Message);
            }
            try
            {
                data.kd_gudang = kd_gudang;
                data.kd_cabang = BranchID;
                data.kd_stok = id;
                data.blthn = DateTime.Now.ToString("yyyyMM");
                response = await client.CallAPIPost("Helper/PostKalkulasi", data);
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
        public IActionResult ResetBooked()
        {
            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> ResetBooking(string id)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            KalkulasiStokVM data = new KalkulasiStokVM();
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            string transno = "";
            var mode = "";
            try
            {
                data.kd_cabang = BranchID;
                data.kd_stok = id;
                data.blthn = DateTime.Now.ToString("yyyyMM");
                response = await client.CallAPIPost("Helper/ResetBooking", data);
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

        public async Task<string> GetTerimaPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string barang)
        {
            List<INV_GUDANG_IN> result = new List<INV_GUDANG_IN>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            response = await client.CallAPIGet("GUDANG_IN/GetTerimaPartial?skip=" + skip + "&take=" + take + "&pageSize=" + pageSize + "&page=" + page + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&sorting=" + sorting + "&filter=" + filter + "&barang=" + barang);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<INV_GUDANG_IN>>(response.Message);
                //if (result.Count() > 0)
                //{
                //    result.FirstOrDefault().tgl_transdesc = result.FirstOrDefault().tgl_trans.ToString("dd MMMM yyyy");
                //}
            }
            return JsonConvert.SerializeObject(new { total = response.Result, data = result });
        }

        public async Task<string> GetStokGudangPartial(string blnthn, string sorting, string filter, int skip, int take, int pageSize, int page, string barang)
        {
            IEnumerable<V_MonStok> result = new List<V_MonStok>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            //if (blnthn == null)
            //{
            //    response = await client.CallAPIGet("Helper/GetBlthn");
            //    if (response.Success)
            //    {
            //        blnthn = JsonConvert.DeserializeObject<string>(response.Message);
            //    }


            //}

            if (blnthn != null)
            {
                response = await client.CallAPIGet("GUDANG_IN/GetstokGudangPartial?blnthn=" + blnthn + "&cb=" + BranchID + "&sorting=" + sorting + "&filter=" + filter + "&skip=" + skip + "&take=" + take + "&pageSize=" + pageSize + "&barang=" + barang );
            }

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<V_MonStok>>(response.Message);
            }
            //  return Ok(result);
            return JsonConvert.SerializeObject(new { total = response.Result, data = result });
        }
    }
}