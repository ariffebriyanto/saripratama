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
    public class ReturController : ControllerBase
    {
        [HttpGet("GetReturInv")]
        public string GetReturInv(string kd_cabang)
        {
            var successReponse = new Response();
            try
            {
                //var res = ReturRepo.GetReturInv(kd_cabang);
                var res = ReturRepo.GetReturInv2(kd_cabang);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(res);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetDODetails")]
        public string GetDODetails(string id)
        {
            var successReponse = new Response();
            try
            {
                var res = ReturRepo.GetDODetails2(id);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(res);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetMonRetur")]
        public async Task<ActionResult<string>> GetMonRetur(string kdcb, string no_trans = null, string kd_stok = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_RETUR> ListQC = new List<SALES_RETUR>();

            try
            {
                //ListQC = INV_QCRepo.GetMonitoringQC(kd_stok,DateFrom, DateTo);
                ListQC = await ReturRepo.GetMonRetur(kdcb, no_trans, kd_stok, DateFrom, DateTo);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListQC);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetMonReturD")]
        public async Task<ActionResult<string>> GetMonReturD(string kdcb, string no_trans = null, string kd_stok = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var successReponse = new Response();
            IEnumerable<SALES_RETUR_D> ListQC = new List<SALES_RETUR_D>();

            try
            {
                //ListQC = INV_QCRepo.GetMonitoringQC(kd_stok,DateFrom, DateTo);
                ListQC = await ReturRepo.GetMonReturD(kdcb, no_trans, kd_stok, DateFrom, DateTo);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListQC);
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