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
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;

namespace IFA.Web.Controllers
{
    public class Fin_AP_LunasController : BaseController
    {
        public Fin_AP_LunasController(FactoryClass factoryClass,
          IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, FIN")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, FIN")]
        public async Task<IActionResult> Create(string id = "", string mode = "")
        {

            ApiClient client = factoryClass.APIClientAccess();

            Response response = new Response();
            if (id == "")
            {
                ViewBag.Mode = "NEW";
            }
            else
            {
                ViewBag.Mode = mode;
                ViewBag.Id = id;
            }
            response = await client.CallAPIGet("SIF_Barang/GetBarangGudang");

            if (response.Success)
            {
                ViewBag.BarangList = response.Message;

            }


            response = await client.CallAPIGet("SIF_Satuan/GetSatuanALL");
            if (response.Success)
            {
                ViewBag.Satuan = response.Message;

            }
            response = await client.CallAPIGet("Helper/GetRekBank");
            if (response.Success)
            {
                ViewBag.banktujuan = response.Message;

            }
            response = await client.CallAPIGet("Helper/GetJenisGiro");
            if (response.Success)
            {
                ViewBag.JenisGiro = response.Message;

            }

            response = await client.CallAPIGet("Helper/GetDivisi");
            if (response.Success)
            {
                ViewBag.Divisi = response.Message;

            }

            response = await client.CallAPIGet("Helper/GetKartuGiro");
            if (response.Success)
            {
                ViewBag.KartuGiro = response.Message;

            }

            response = await client.CallAPIGet("Helper/GetBankAsal");
            if (response.Success)
            {
                ViewBag.BankAsal = response.Message;

            }

            return View();
        }
       
        public async Task<IActionResult> SaveNOTA(FIN_BELI_LUNAS data, int jml_titipangr, decimal jml_bayargiro, int jml_titipantf, decimal jml_bayartf)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var mode = "";
            string transno = "";

            if (data.no_trans == null)
            {
                mode = "NEW";
                response = await client.CallAPIGet("Helper/GetNoTrans?prefix=JKK&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
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

            data.Last_created_by = UserID;
            data.Last_create_date = DateTime.Now;
            data.kurs_valuta = 1;
            data.kd_valuta = "IDR";
            data.no_trans = transno;
            data.Kd_cabang = BranchID;

            data.jml_val_trans = data.jml_val_trans;
            data.jml_rp_trans = data.jml_rp_trans;
            data.thnbln = DateTime.Now.ToString("yyyyMM");
           // data.jml_tagihan = data.jml_tagihan;
            data.Jns_bayar = data.Jns_bayar;
            data.keterangan = data.keterangan;
            data.jml_rp_trans = data.jml_rp_trans;
            data.jml_val_trans = data.jml_val_trans;
           
            data.kd_kartu = data.kd_kartu;
            data.kartu = data.kd_kartu;
            data.tgl_trans = DateTime.Now;
            //if (data.Jns_bayar == "01")


            //{
            //data.jml_titipangr = jml_titipan1;
            //data.no_giro = data.no_giro;
            //data.tipe_trans_gr = "JKM-KPT-01";
           // data.jns_giro_trans = "01";
            //data.jml_bayargr = jml_bayargiro;

            //}
            //else
            //{
            //if (data.Jns_bayar == "04")//transfer
            //{v 
            //    data.kd_bank = data.kd_bank;
            //}
            //else if (data.Jns_bayar == "03")//tunai
            //{
            //    data.kd_bank = 0;
            //}
            // data.jml_titipan_tf = data.jml_titipantf;
            data.kd_bank = data.kd_bank;

            data.jml_titipan = data.jml_titipan;
            // data.no_giro = "";
           // data.tipe_trans = "JKM-KPT-06";
            // data.jns_giro_trans = "";
            data.jml_bayar = data.jml_bayar;
            // data.tipe_trans = "JKK-KUT-" + data.Jns_bayar;
            data.tipe_trans = "JKK-KUT-03";
            data.jml_giro = data.jml_giro; 
            data.jml_transfer = data.jml_transfer;
            data.jml_tunai = data.jml_tunai;
            decimal totaltagihan = 0;
            for (int i = 0; i <= data.detail.Count() - 1; i++)
            {
                data.detail[i].Kd_Cabang = BranchID;
                data.detail[i].prev_no_inv = data.detail[i].no_trans;
                data.detail[i].no_trans = data.no_trans;
                data.detail[i].no_seq = i + 1;
                data.detail[i].kd_kartu = data.kd_kartu;
                data.detail[i].tipe_trans = "JKK-KUT-03";
                data.detail[i].jml_pembulatan = data.detail[i].jml_pembulatan;
                data.detail[i].jml_bayar = data.detail[i].jml_bayar;
                data.detail[i].jml_tagihan = data.detail[i].jml_tagihan;
                totaltagihan += Convert.ToDecimal(data.detail[i].jml_tagihan);
                data.detail[i].jml_diskon = data.detail[i].jml_diskon;
                data.detail[i].Last_created_by = UserID;
                data.detail[i].Last_create_date = DateTime.Now;


            }

            data.jml_tagihan = totaltagihan;
            if (mode == "NEW")
            {
                response = await client.CallAPIPost("FIN_AP/SaveAP", data);

            }

            if (response.Success)
            {
                response.Result = transno;
            }

            return Ok(response);
        }


        public async Task<IActionResult> SaveFINAR(INV_GUDANG_OUT data)
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
                //data.detail[i].gudang_asal = gd_asal;
                data.detail[i].gudang_tujuan = "EXP01";
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

        [Authorize(Roles = "Admin, User, UAT, Kasir, FIN")]
        public IActionResult MonAP()
        {
            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, FIN")]
        public async Task<IActionResult> GetMonMutasiAP(Filter filterkb)
        {
            IEnumerable<FIN_BELI_LUNAS> result = new List<FIN_BELI_LUNAS>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            // var tipe_trans = "JKK-KBB-03";
            //response = await client.CallAPIGet("Kasir/GetKasBon?id=" + filterkb.id + "&cb=" + BranchID + "&DateFrom=" + filterkb.DateFrom + "&DateTo=" + filterkb.DateTo);
            response = await client.CallAPIGet("FIN_AP/GetMonAP?id=" + filterkb.id + "&cb=" + BranchID );

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_BELI_LUNAS>>(response.Message);

            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, FIN")]
        public async Task<IActionResult> GetMonMutasiAPD(Filter filterkb)
        {
            IEnumerable<FIN_BELI_LUNAS_D> result = new List<FIN_BELI_LUNAS_D>();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            Response response = new Response();

            // var tipe_trans = "JKK-KBB-03";
            // response = await client.CallAPIGet("Kasir/GetKasBonD?id=" + filterkb.id + "&cb=" + BranchID + "&DateFrom=" + filterkb.DateFrom + "&DateTo=" + filterkb.DateTo);
            response = await client.CallAPIGet("FIN_AP/GetMonAP_D?id=" + filterkb.id + "&cb=" + BranchID);

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_BELI_LUNAS_D>>(response.Message);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetPrintAP(string id)
        {
            // PrintTerima result = new PrintTerima();
            IEnumerable<FIN_BELI_LUNAS> result = new List<FIN_BELI_LUNAS>();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            IEnumerable<FIN_BELI_LUNAS_D> resultD = new List<FIN_BELI_LUNAS_D>();
            ApiClient client = factoryClass.APIClientAccess();
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            Response response = new Response();
            Response responseD = new Response();

            string sHTML = "";


            response = await client.CallAPIGet("FIN_AP/GetMonAP?id=" + id + "&cb=" + BranchID);

            responseD = await client.CallAPIGet("FIN_AP/GetMonAP_D?id=" + id + "&cb=" + BranchID);


            if (response.Success && responseD.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_BELI_LUNAS>>(response.Message);
                resultD = JsonConvert.DeserializeObject<List<FIN_BELI_LUNAS_D>>(responseD.Message);

                //head
                sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}td, th{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}} .noBorder{border: 0px solid #dddddd;}.paddingtd{padding: 0;padding-bottom:2px;padding-left:8px;padding-top:2px;}.paddingth{padding: 0;padding-bottom:8px;padding-left:8px;}.textCenter{text-align: center;}.textRight{text-align: right;}</style></head> ";

                foreach (FIN_BELI_LUNAS ar in result)
                {

                    //company profile
                    sHTML += " <body>Pelunasan AP<table style='margin-bottom: 20px; '><tr ><td style='width:40%; border: 0px solid #dddddd;' ><h2>" + ar.no_trans + "</h2><p>" + ar.no_ref1 + "</p><p>Tanggal Transaksi: " + ar.tgl_trans + "</p></td></tr> ";

                    //jenis pembayaran
                    var jns1 = "";
                    if (ar.Jns_bayar == "01")
                    {
                        jns1 = "Giro";
                    }
                    else if (ar.Jns_bayar == "04")
                    {
                        jns1 = "Transfer";
                    }
                    else
                    {
                        jns1 = "Tunai";
                    }
                    sHTML += " <tr class='headerTable '> <th style='width:40%;' class='noBorder '> Pembayaran </th> <th style='width: 20%;' class='noBorder '></th> <th style='width: 40%;' class='noBorder '> Jenis Pembayaran </th> </tr><tr > <td style='width:40%;' class='noBorder paddingtd'> " + jns1 + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> " + ar.jml_bayar + " </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> " + ar.kd_kartu + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr><tr > <td style='width:40%;' class='noBorder paddingtd'> Valuta: " + ar.kd_valuta + " </td><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr><tr ><td style='width: 20%;' class='noBorder paddingtd'></td><td style='width: 40%;' class='noBorder paddingtd'> </td></tr></table> ";
                    //Notes
                    sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='noBorder'> NOTES </th> </tr><tr> <td class='noBorder paddingtd'> " + ar.keterangan + " </td></tr></table> ";

                }



                //UserDetail




                //headerDetail
                sHTML += " <table style='margin-bottom: 20px;'> <tr class='headerTable '> <th class='textCenter'> No Invoice </th> <th class='textCenter'>Jumlah Tagihan</th><th class='textCenter'>Jumlah Bayar</th> <th class='textCenter'> Jumlah Diskon </th> <th class='textCenter'> Jumlah Pembulatan </th> <th class='textCenter'> Pendapatan Lain</th> <th class='textCenter'> Sub Total</th> </tr> ";

                //detail
                foreach (FIN_BELI_LUNAS_D detail in resultD)
                {
                    var subtotalku = detail.jml_tagihan - detail.jml_bayar - detail.jml_diskon - detail.jml_pembulatan - detail.pendp_lain;
                    sHTML += " <tr><td class='textCenter'> " + detail.prev_no_inv + " </td> <td class='textRight'> " + string.Format("{0:#,0.00}", detail.jml_tagihan) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.jml_bayar) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.jml_diskon) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.jml_pembulatan) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.pendp_lain) + " </td><td class='textCenter'> " + string.Format("{0:#,0.00}", subtotalku) + " </td></tr> ";
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
    }
}
