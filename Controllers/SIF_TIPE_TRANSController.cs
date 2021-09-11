using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Api.Utils;
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
    public class SIF_TIPE_TRANSController : ControllerBase
    {
        [HttpGet("GetTipeTrans")]
        public async Task<ActionResult<string>> GetTipeTrans()
        {
            IEnumerable<SIF_TIPE_TRANS> ListModel = new List<SIF_TIPE_TRANS>();
            var successReponse = new Response();

            try
            {
                ListModel = await SIF_TIPE_TRANSRepo.GetTipeTrans();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListModel);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetTipeTransMon")]
        public async Task<ActionResult<string>> GetTipeTransMon(string kd_jurnal = null)
        {
            IEnumerable<SIF_TIPE_TRANSVMON> ListModel = new List<SIF_TIPE_TRANSVMON>();
            var successReponse = new Response();

            try
            {
                ListModel = await SIF_TIPE_TRANSRepo.GetTipeTransMon(kd_jurnal);
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListModel);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("Update")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> Update([FromForm] SIF_TIPE_TRANSVM data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (var item in data.details)
                {
                    await SIF_TIPE_TRANSRepo.Update(item, data.updateby, data.updatedate, trans);
                }

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = "success";

                //all save success -> commit set true
                DataAccess.CloseTransaction(conn, trans, true);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                //ada fail -> rollback
                DataAccess.CloseTransaction(conn, trans, false);
            }
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return successReponse;
        }

    }
}
