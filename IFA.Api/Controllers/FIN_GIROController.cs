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
    public class FIN_GIROController : ControllerBase
    {
        [HttpGet("GetFINGiro")]
        public ActionResult<string> GetFINGiro()
        {
            var successReponse = new Response();
            IEnumerable<FIN_GIRO> ListSIFBukuBesar = new List<FIN_GIRO>();

            try
            {
                ListSIFBukuBesar = FINGiroRepo.GetFINGiro();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListSIFBukuBesar);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetALL_Giro")]
        public ActionResult<string> GetALL_Giro()
        {
            var successReponse = new Response();
            List<FIN_GIRO> ListSIFBarang = new List<FIN_GIRO>();

            try
            {
                ListSIFBarang = FINGiroRepo.GetALL_Giro();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListSIFBarang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetALL_GiroTemp")]
        public ActionResult<string> GetALL_GiroTemp()
        {
            var successReponse = new Response();
            List<FIN_GIRO> ListSIFBarang = new List<FIN_GIRO>();

            try
            {
                ListSIFBarang = FINGiroRepo.GetALL_GiroTemp();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListSIFBarang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("Get_Giro")]
        public ActionResult<string> Get_Giro(string nomor = "")
        {
            var successReponse = new Response();
            IEnumerable<FIN_GIRO> ListSIFBarang = new List<FIN_GIRO>();

            try
            {
                ListSIFBarang = FINGiroRepo.Get_Giro(nomor);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListSIFBarang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("SaveListGiro")]
        public ActionResult<Response> SaveListGiro([FromForm] List<FIN_GIRO> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (FIN_GIRO item in data)
                {
                    FINGiroRepo.SaveListGiro(item, trans);

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

        [HttpPost("UpdateFINGIRO")]
        public ActionResult<Response> UpdateFINGIRO([FromForm] List<FIN_GIRO> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (FIN_GIRO item in data)
                {
                    FINGiroRepo.UpdateFINGIRO(item, trans);

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

        [HttpPost("DeleteGiro")]
        public ActionResult<Response> DeleteGiro([FromForm] List<FIN_GIRO> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (FIN_GIRO item in data)
                {
                    FINGiroRepo.DeleteGiro(item, trans);

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

        [HttpPost("SaveGiro")]
        public async Task<ActionResult<Response>> SaveGiro([FromForm] FIN_GIRO data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {

                await FINGiroRepo.SaveGiro(data, trans);

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