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
    public class SIF_SatuanController : ControllerBase
    {
        [HttpGet("GetSIFSatuanCbo")]
        public ActionResult<string> GetSIFSatuanCbo(string kode_barang=null)
        {
            var successReponse = new Response();
            List<SIF_Satuan> ListSIFSatuan = new List<SIF_Satuan>();

            try
            {
                ListSIFSatuan = SIF_SatuanRepo.GetSIFSatuanCbo(kode_barang);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListSIFSatuan);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetSatuanALL")]
        public ActionResult<string> GetSatuanALL()
        {
            var successReponse = new Response();
            List<SIF_Satuan> ListSIFSatuan = new List<SIF_Satuan>();

            try
            {
                ListSIFSatuan = SIF_SatuanRepo.GetSatuanALL();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListSIFSatuan);
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