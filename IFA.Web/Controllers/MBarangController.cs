using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Domain.Base;
using IFA.Domain.Models;
using ERP.Web.Controllers;
using ERP.Web.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using OfficeOpenXml;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace IFA.Web.Controllers
{
    public class MBarangController :  BaseController
    {
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Index()
        {
            ApiClient client = factoryClass.APIClientAccess();

            Response response = new Response();

            response = await client.CallAPIGet("SIF_Barang/GetBarangGudang");

            if (response.Success)
            {
                ViewBag.BarangList = response.Message;

            }
            response = await client.CallAPIGet("SIF_Barang/GetSIFBukuBesarCbo");
            if (response.Success)
            {
                ViewBag.PersediaanList = response.Message;

            }
            response = await client.CallAPIGet("SIF_Barang/GetrekPenjualan2Cbo");
            if (response.Success)
            {
                ViewBag.Penjualan2List = response.Message;

            }
            response = await client.CallAPIGet("SIF_Satuan/GetSatuanALL");
            if (response.Success)
            {
                ViewBag.Satuan = response.Message;

            }

            return View();
        }

        #region Kota

       
        public MBarangController(FactoryClass factoryClass,
           IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<SIF_Barang> result = new List<SIF_Barang>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("SIF_Barang/GetALL_Barang");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Barang>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<PartialViewResult> EditBrg(string id = null)
        {
            SIF_Barang model = new SIF_Barang();

            ApiClient client = factoryClass.APIClientAccess();
            if (id != null)
            {
                var response = await client.CallAPIGet("SIF_Barang/Get_Barang?kode_Barang=" + id);

                if (response.Success)
                {
                    model = JsonConvert.DeserializeObject<List<SIF_Barang>>(response.Message).FirstOrDefault();
                }
            }

            return PartialView(model);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> SaveBrg(SIF_Barang data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            data.Kd_Cabang = "1";

            data.Rec_Stat = "Y";
            response = await client.CallAPIPost("SIF_Barang/SaveSIFBrg", data);

            return Ok(response);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> UpdateKota(SIF_Barang data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            data.Rec_Stat = "Y";
            data.Kd_Cabang = "1";

            response = await client.CallAPIPost("SIF_Barang/UpdateSIFBrg", data);

            return Ok(response);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> DeleteBrg([FromBody] List<SIF_Barang> data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            foreach (SIF_Barang item in data)
            {
                item.Rec_Stat = "N";
                item.Last_Updated_By = UserID;
                item.Last_Update_Date = DateTime.Now;

            }

            response = await client.CallAPIPost("SIF_Barang/DeleteBrg", data);

            return Ok(response);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> UpdateBrg([FromBody] List<SIF_Barang> data)
    
        {
           
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
          
            response = await client.CallAPIPost("SIF_Barang/UpdateSIFBrg", data);

            return Ok(response);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> DeleteBarang([FromBody] List<SIF_Barang> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("SIF_Barang/UpdateSIFBrg", data);

            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> SavelistBarang([FromBody] List<SIF_Barang> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            var Kode_Barang = "";
            var retNo = await client.CallAPIGet("Helper/GetKdBarang");
            if (retNo.Success)
            {
                Kode_Barang = JsonConvert.DeserializeObject<string>(retNo.Message);
            }

            foreach (SIF_Barang item in data)
            {
                item.Kd_Cabang = BranchID;
                item.Kode_Barang = Kode_Barang;
                item.kd_jenis = "0";
                item.kd_ukuran = "0";
                item.kd_merk = "00";
                item.kd_kain = "0";
                item.tipe_stok = "0";
                item.Last_Created_By = UserID;
                item.Last_Create_Date = DateTime.Now;
                item.Kd_Depart = "2";
                item.kd_jns_persd = "1";
                item.kd_tipe = "0";
                item.kd_sub_tipe = "0";
                item.lokasi = "00001";
                item.Rec_Stat = "Y";
            }
            response = await client.CallAPIPost("SIF_Barang/SaveListBarang", data);

            return Ok(response);
        }
        #endregion
    }
}