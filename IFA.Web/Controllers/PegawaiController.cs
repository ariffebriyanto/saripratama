using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Domain.Base;
using ERP.Domain.Models;
using ERP.Web.Controllers;
using ERP.Web.Utils;
using IFA.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IFA.Web.Controllers
{
    public class PegawaiController : BaseController
    {

        public async Task<IActionResult> Index()
        {
            ApiClient client = factoryClass.APIClientAccess();
            Response response = new Response();
            response = null;
            response = await client.CallAPIGet("Helper/GetCabangALL");
            if (response.Success)
            {
                ViewBag.GudangList = response.Message;

            }
            return View();
        }
        public PegawaiController(FactoryClass factoryClass,
           IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        public async Task<IActionResult> GetPegawai()
        {
            IEnumerable<SIF_Pegawai> result = new List<SIF_Pegawai>();
            ApiClient client = factoryClass.APIClientAccess();

            var response = await client.CallAPIGet("SIF_Pegawai/GetPegawaiALL");

            if (response.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Pegawai>>(response.Message);
            }

            
            return Ok(result);
        }

        public async Task<PartialViewResult> Edit(string id = null)
        {
            SIF_Pegawai model = new SIF_Pegawai();

            ApiClient client = factoryClass.APIClientAccess();
            if (id != null)
            {
                var response = await client.CallAPIGet("SIF_Pegawai/GetSIF_Pegawai?kode_pegawai=" + id);

                if (response.Success)
                {
                    model = JsonConvert.DeserializeObject<List<SIF_Pegawai>>(response.Message).FirstOrDefault();
                }
            }

            return PartialView(model);
        }
        public async Task<IActionResult> UpdatePegawai(SIF_Pegawai data)
        {
            Response response = new Response();
            ApiClient client = factoryClass.APIClientAccess();
            data.Rec_Stat = "Y";
            data.Kd_Cabang = "1";

            response = await client.CallAPIPost("SIF_Pegawai/UpdateSIFPegawai", data);

            return Ok(response);
        }
    }
}