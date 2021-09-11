using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Web.Controllers;
using ERP.Web.Utils;
using IFA.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IFA.Web.Controllers
{
    public class MSatuanController : BaseController
    {
        public MSatuanController(FactoryClass factoryClass,
         IHttpContextAccessor httpContextAccessor) : base(factoryClass, httpContextAccessor)
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetSatuanCbo(string kode_barang = null)
        {
            IEnumerable<SIF_Satuan> result = new List<SIF_Satuan>();
            ApiClient client = factoryClass.APIClientAccess();

            var responseSatuan = await client.CallAPIGet("SIF_Satuan/GetSIFSatuanCbo?kode_barang=" + kode_barang);
            if (responseSatuan.Success)
            {
                result = JsonConvert.DeserializeObject<List<SIF_Satuan>>(responseSatuan.Message);

            }
            return Ok(result);
        }
    }
}