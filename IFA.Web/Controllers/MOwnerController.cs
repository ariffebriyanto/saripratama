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
    public class MOwnerController : BaseController
    {
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Index()
        {
            ApiClient client = factoryClass.APIClientAccess();

            Response response = new Response();

            response = await client.CallAPIGet("SIF_Owner/GetOwnerGudang");

            if (response.Success)
            {
                ViewBag.BarangList = response.Message;

            }
           
         
            

            return View();
        }

        #region Kota


        public MOwnerController(FactoryClass factoryClass,
           IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<SIF_Owner> result = new List<SIF_Owner>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("SIF_Owner/GetALL_Owner");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Owner>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<PartialViewResult> EditOwner(string id = null)
        {
            SIF_Owner model = new SIF_Owner();

            ApiClient client = factoryClass.APIClientAccess();
            if (id != null)
            {
                var response = await client.CallAPIGet("SIF_Owner/Get_Barang?kode_Barang=" + id);

                if (response.Success)
                {
                    model = JsonConvert.DeserializeObject<List<SIF_Owner>>(response.Message).FirstOrDefault();
                }
            }

            return PartialView(model);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> SaveOwner(SIF_Owner data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            data.Kd_Cabang = "1";

          
            response = await client.CallAPIPost("SIF_Owner/SaveSIFOwner", data);

            return Ok(response);
        }
       
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> DeleteOwner([FromBody] List<SIF_Owner> data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            foreach (SIF_Owner item in data)
            {
              
                item.Last_Updated_By = UserID;
                item.Last_Update_Date = DateTime.Now;

            }

            response = await client.CallAPIPost("SIF_Owner/DeleteOwner", data);

            return Ok(response);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> UpdateOwner([FromBody] List<SIF_Owner> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("SIF_Owner/UpdateSIFOwner", data);

            return Ok(response);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> DeleteBarang([FromBody] List<SIF_Owner> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("SIF_Owner/UpdateSIFBrg", data);

            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> SavelistOwner([FromBody] List<SIF_Owner> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;

            //var Kd_Owner = "";
            //var retNo = await client.CallAPIGet("Helper/GetKdOwner");
            //if (retNo.Success)
            //{
            //    Kd_Owner = JsonConvert.DeserializeObject<string>(retNo.Message);
            //}

            foreach (SIF_Owner item in data)
            {
                item.Kd_Cabang = BranchID;
                item.Last_Created_By = UserID;
                item.Last_Create_Date = DateTime.Now;
               
            }
            response = await client.CallAPIPost("SIF_Owner/SaveListOwner", data);

            return Ok(response);
        }
        #endregion
    }
}