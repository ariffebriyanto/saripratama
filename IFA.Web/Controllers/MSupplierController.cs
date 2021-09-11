using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Domain.Base;
using ERP.Domain.Models;
using ERP.Web.Controllers;
using ERP.Web.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IFA.Web.Controllers
{
    public class MSupplierController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        #region Kota
      

        public MSupplierController(FactoryClass factoryClass,
           IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        public async Task<IActionResult> GetKota()
        {
            IEnumerable<SIF_Kota> result = new List<SIF_Kota>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("SIF_Supplier/GetSIFKota");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Kota>>(response.Message);
            }
            return Ok(result);
        }

        public async Task<PartialViewResult> EditKota(string id = null)
        {
            SIF_Kota model = new SIF_Kota();

            ApiClient client = factoryClass.APIClientAccess();
            if (id != null)
            {
                var response = await client.CallAPIGet("SIF_Kota/GetSIFKota?kode_kota=" + id);

                if (response.Success)
                {
                    model = JsonConvert.DeserializeObject<List<SIF_Kota>>(response.Message).FirstOrDefault();
                }
            }

            return PartialView(model);
        }

        public async Task<IActionResult> SaveKota(SIF_Kota data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            data.Kd_Cabang = "1";

            data.Rec_Stat = "Y";
            response = await client.CallAPIPost("SIF_Kota/SaveSIFKota", data);

            return Ok(response);
        }
        public async Task<IActionResult> UpdateKota(SIF_Kota data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            data.Rec_Stat = "Y";
            data.Kd_Cabang = "1";

            response = await client.CallAPIPost("SIF_Kota/UpdateSIFKota", data);

            return Ok(response);
        }
        public async Task<IActionResult> DeleteKota(string id)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            SIF_Kota data = new SIF_Kota();
            data.Kode_Kota = id;
            data.Rec_Stat = "N";
            data.Kd_Cabang = "1";

            response = await client.CallAPIPost("SIF_Kota/UpdateSIFKota", data);

            return Ok(response);
        }

        #endregion
    }
}