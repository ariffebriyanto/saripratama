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
    public class Fin_AR_LunasController : BaseController
    {
        public Fin_AR_LunasController(FactoryClass factoryClass,
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

        public async Task<IActionResult> SaveNOTA(FIN_NOTA_LUNAS data, int jml_titipangr , decimal jml_bayargiro,int jml_titipantf , decimal jml_bayartf)
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
                response = await client.CallAPIGet("Helper/GetNoTrans?prefix=JKM&transdate=" + DateTime.Now + "&kdcabang=" + BranchID);
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
            data.jml_giro = data.jml_giro;
            data.jml_transfer = data.jml_transfer;
            data.jml_tunai = data.jml_tunai;
            data.thnbln = DateTime.Now.ToString("yyyyMM");
            data.jml_tagihan = data.jml_tagihan;
            data.Jns_bayar = data.Jns_bayar;
            data.keterangan = data.keterangan;
            data.jml_rp_trans = data.jml_rp_trans;
            data.jml_val_trans = data.jml_val_trans;
           
            data.kd_kartu = data.kd_kartu;
            data.tgl_trans = DateTime.Now;
            //if (data.Jns_bayar == "01")
            //{
                //data.jml_titipangr = jml_titipan1;
                data.no_giro = data.no_giro;
                //data.tipe_trans_gr = "JKM-KPT-01";
                data.jns_giro_trans = "01";
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
                data.tipe_trans = "JKM-KPT-06";
               // data.jns_giro_trans = "";
                data.jml_bayar = data.jml_bayar;
            // data.jml_bayar_tf = data.jml_bayar;
            //}


            //detail

            decimal totaltagihan = 0;
            for (int i = 0; i <= data.detail.Count() - 1; i++)
            {
                data.detail[i].Kd_Cabang = BranchID;
                data.detail[i].prev_no_inv = data.detail[i].no_trans;
                data.detail[i].no_trans = data.no_trans;
                data.detail[i].no_seq = i + 1;
                data.detail[i].kd_kartu = data.kd_kartu;
                //if (data.Jns_bayar == "01")
                //{
                //    data.detail[i].tipe_trans = "JKM-KPT-01";
                //}
                //else
                //{
                  data.detail[i].tipe_trans = "JKM-KPT-06";
                //}
                data.detail[i].jml_pembulatan = data.detail[i].jml_pembulatan;
                data.detail[i].jml_bayar = data.detail[i].jml_bayar;
                data.detail[i].jml_tagihan = data.detail[i].jml_tagihan;
                totaltagihan += Convert.ToDecimal(data.detail[i].jml_tagihan);
                data.detail[i].jml_diskon = data.detail[i].jml_diskon;
                data.detail[i].subtotal = data.detail[i].subtotal;
                data.detail[i].Last_created_by = UserID;
                data.detail[i].Last_create_date = DateTime.Now;


            }

            data.jml_tagihan = totaltagihan;

            if (mode == "NEW")
            {
                response = await client.CallAPIPost("FIN_AR_LUNAS/SaveNota", data);
                
            }

            if (response.Success)
            {
                response.Result = transno;
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, Kasir, FIN")]
        public IActionResult MonAR()
        {
            return View();
        }

        [Authorize(Roles = "Admin, User, UAT, FIN")]
        public async Task<IActionResult> GetMonMutasiAR(Filter filterkb)
        {
            IEnumerable<FIN_NOTA_LUNAS> result = new List<FIN_NOTA_LUNAS>();
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            // var tipe_trans = "JKK-KBB-03";
            //response = await client.CallAPIGet("FIN_AR_LUNAS/GetMonNota?id=" + filterkb.id + "&cb=" + BranchID + "&DateFrom=" + filterkb.DateFrom + "&DateTo=" + filterkb.DateTo);
            response = await client.CallAPIGet("FIN_AR_LUNAS/GetMonNota?id=" + filterkb.id + "&cb=" + BranchID );

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_NOTA_LUNAS>>(response.Message);

            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, FIN")]
        public async Task<IActionResult> GetMonMutasiARD(Filter filterkb)
        {
            IEnumerable<FIN_NOTA_LUNAS_D> result = new List<FIN_NOTA_LUNAS_D>();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            Response response = new Response();

            // var tipe_trans = "JKK-KBB-03";
            //response = await client.CallAPIGet("FIN_AR_LUNAS/GetMonNota?id=" + filterkb.id + "&cb=" + BranchID + "&DateFrom=" + filterkb.DateFrom + "&DateTo=" + filterkb.DateTo);
            response = await client.CallAPIGet("FIN_AR_LUNAS/GetMonNotaD?id=" + filterkb.id + "&cb=" + BranchID );

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_NOTA_LUNAS_D>>(response.Message);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetPrintAR(string id)
        {
           // PrintTerima result = new PrintTerima();
            IEnumerable<FIN_NOTA_LUNAS> result = new List<FIN_NOTA_LUNAS>();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            IEnumerable<FIN_NOTA_LUNAS_D> resultD = new List<FIN_NOTA_LUNAS_D>();
            ApiClient client = factoryClass.APIClientAccess();
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            Response response = new Response();
            Response responseD = new Response();

            string sHTML = "";


            response = await client.CallAPIGet("FIN_AR_LUNAS/GetMonNota?id=" + id + "&cb=" + BranchID);

            responseD = await client.CallAPIGet("FIN_AR_LUNAS/GetMonNotaD?id=" + id + "&cb=" + BranchID);


            if (response.Success && responseD.Success)
            {
                result = JsonConvert.DeserializeObject<List<FIN_NOTA_LUNAS>>(response.Message);
                resultD = JsonConvert.DeserializeObject<List<FIN_NOTA_LUNAS_D>>(responseD.Message);

                //head
                sHTML = "<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}td, th{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}} .noBorder{border: 0px solid #dddddd;}.paddingtd{padding: 0;padding-bottom:2px;padding-left:8px;padding-top:2px;}.paddingth{padding: 0;padding-bottom:8px;padding-left:8px;}.textCenter{text-align: center;}.textRight{text-align: right;}</style></head> ";
                
                foreach (FIN_NOTA_LUNAS ar in result)
                {

                    //company profile
                    sHTML += " <body>Pelunasan AR<table style='margin-bottom: 20px; '><tr ><td style='width:40%; border: 0px solid #dddddd;' ><h2>" + ar.no_trans + "</h2><p>" + ar.no_ref1 + "</p><p>Tanggal Transaksi: " + ar.tgl_trans + "</p></td></tr> ";

                    //jenis pembayaran
                    var jns1 = "";
                    if (ar.Jns_bayar == "01")
                    {
                        jns1 = "Giro";
                    }else if (ar.Jns_bayar == "04")
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
                foreach (FIN_NOTA_LUNAS_D detail in resultD)
                {
                    sHTML += " <tr><td class='textCenter'> " + detail.prev_no_inv + " </td> <td class='textRight'> " + string.Format("{0:#,0.00}", detail.jml_tagihan) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.jml_bayar) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.jml_diskon) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.jml_pembulatan) + " </td><td class='textRight'> " + string.Format("{0:#,0.00}", detail.pendp_lain) + " </td><td class='textCenter'> " + string.Format("{0:#,0.00}", detail.subtotal) + " </td></tr> ";
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
