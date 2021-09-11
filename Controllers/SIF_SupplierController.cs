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
    public class SIF_SupplierController : ControllerBase
    {
        [HttpGet("GetSIF_Supplier")]
        public ActionResult<string> GetSIF_Supplier()
        {
            var successReponse = new Response();
            List<SIF_Supplier> ListSIFKota = new List<SIF_Supplier>();

            try
            {
                ListSIFKota = SIF_SupplierRepo.GetSIFSupplierCbo();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListSIFKota);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetALL_Supplier")]
        public ActionResult<string> GetALL_Supplier()
        {
            var successReponse = new Response();
            List<SIF_Supplier> ListSIFKota = new List<SIF_Supplier>();

            try
            {
                ListSIFKota = SIF_SupplierRepo.GetAll_Supplier();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListSIFKota);
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