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
    public class MPetController : BaseController
    {
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> Index()
        {
            ApiClient client = factoryClass.APIClientAccess();

            Response response = new Response();

            response = await client.CallAPIGet("SIF_Owner/GetOwnerGudang");

            if (response.Success)
            {
                ViewBag.OwnerList = response.Message;

            }

          




            return View();
        }

        #region Kota


        public MPetController(FactoryClass factoryClass,
           IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<SIF_Pet> result = new List<SIF_Pet>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("SIF_Pet/GetALL_Pet");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Pet>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<PartialViewResult> EditPet(string id = null)
        {
            SIF_Pet model = new SIF_Pet();

            ApiClient client = factoryClass.APIClientAccess();
            if (id != null)
            {
                var response = await client.CallAPIGet("SIF_Pet/Get_Pet?KD_Pet=" + id);

                if (response.Success)
                {
                    model = JsonConvert.DeserializeObject<List<SIF_Pet>>(response.Message).FirstOrDefault();
                }
            }

            return PartialView(model);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> SavePet(SIF_Pet data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            data.Kd_Cabang = "1";


            response = await client.CallAPIPost("SIF_Pet/SaveSIFPet", data);

            return Ok(response);
        }

        public async Task<IActionResult> UpdatePet([FromBody] List<SIF_Owner> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("SIF_Pet/UpdateSIFPet", data);

            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> DeletePet([FromBody] List<SIF_Pet> data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var UserID = claimsIdentity.FindFirst("UserID").Value;
            var BranchID = claimsIdentity.FindFirst("BranchID").Value;
            var RoleName = claimsIdentity.FindFirst("RoleName").Value;
            var PegawaiID = claimsIdentity.FindFirst("PegawaiID").Value;
            foreach (SIF_Pet item in data)
            {

                item.Last_Updated_By = UserID;
                item.Last_Update_Date = DateTime.Now;

            }

            response = await client.CallAPIPost("SIF_Pet/DeletePet", data);

            return Ok(response);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> UpdateOwner([FromBody] List<SIF_Pet> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("SIF_Pet/UpdateSIFOwner", data);

            return Ok(response);
        }
        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> DeleteBarang([FromBody] List<SIF_Pet> data)

        {

            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();

            response = await client.CallAPIPost("SIF_Pet/UpdateSIFBrg", data);

            return Ok(response);
        }

        [Authorize(Roles = "Admin, User, UAT, SPV, LOGISTIK, PENJUALAN")]
        public async Task<IActionResult> SavelistPet([FromBody] List<SIF_Pet> data)

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

            foreach (SIF_Pet item in data)
            {
                item.Kd_Cabang = BranchID;
                item.Last_Created_By = UserID;
                item.Last_Create_Date = DateTime.Now;

            }
            response = await client.CallAPIPost("SIF_Pet/SaveListPet", data);

            return Ok(response);
        }
        #endregion
    }
}