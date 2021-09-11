using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Domain.Base;
using IFA.Api.Repositories;
using IFA.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IFA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SIF_Jenis_BarangController : ControllerBase
    {
        [HttpGet("GetSIFJenisBarangCbo")]
        public ActionResult<string> GetSIFJenisBarangCbo()
        {
            var successReponse = new Response();
            List<SIF_Jenis_Barang> ListJenis = new List<SIF_Jenis_Barang>();

            try
            {
                ListJenis = SIF_Jenis_BarangRepo.GetSIFJenisBarangCbo();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListJenis);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }
    }
}