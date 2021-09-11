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
    public class SIF_PegawaiController : ControllerBase
    {
        [HttpGet("GetSIF_Pegawai")]
        public ActionResult<string> GetSIF_Pegawai(string kode_pegawai = "")
        {
            var successReponse = new Response();
            List<SIF_Pegawai> ListSIFPegawai = new List<SIF_Pegawai>();

            try
            {
                ListSIFPegawai = SIF_PegawaiRepo.GetSIFPegawaiCbo(kode_pegawai);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListSIFPegawai);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }
        //[HttpPost("SavePegawai")]
        //public ActionResult<Response> SavePegawai([FromForm] SIF_Pegawai data)
        //{
        //    var successReponse = new Response();
        //    var conn = DataAccess.GetConnection();
        //    var trans = DataAccess.OpenTransaction(conn);
        //    try
        //    {

        //        SIF_PegawaiRepo.SavePegawai(data, trans);

        //        successReponse.Success = true;
        //        successReponse.Result = "success";
        //        successReponse.Message = "success";

        //        //all save success -> commit set true
        //        DataAccess.CloseTransaction(conn, trans, true);
        //    }
        //    catch (Exception e)
        //    {
        //        successReponse.Success = false;
        //        successReponse.Result = "failed";
        //        successReponse.Message = e.Message;

        //        //ada fail -> rollback
        //        DataAccess.CloseTransaction(conn, trans, false);
        //    }
        //    finally
        //    {
        //        //close transaction -> commit success or fail
        //        DataAccess.DisposeConnectionAndTransaction(conn, trans);
        //    }

        //    return successReponse;
        //}

        [HttpPost("SavePegawai")]
        public async Task<ActionResult<Response>> SavePegawai([FromForm] List<SIF_Pegawai> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);

            try
            {
                string KD_SALES = await HelperRepo.GetNoTransNew("MSALES", DateTime.Now, data.FirstOrDefault().Kd_Cabang, trans, conn);
                foreach (SIF_Pegawai item in data)
                {

                    await SIF_PegawaiRepo.SavePegawai(item, trans);
                    await SIF_PegawaiRepo.SaveSales(item, KD_SALES, trans);
                    await SIF_PegawaiRepo.SaveMUSER(item, trans);  //SaveMUSER
                    await SIF_PegawaiRepo.SaveUserRole(item, trans);

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

        [HttpGet("GetPegawaiALL")]
        public ActionResult<string> GetPegawaiALL()
        {
            var successReponse = new Response();
            List<SIF_Pegawai> ListSIFPegawai = new List<SIF_Pegawai>();

            try
            {
                ListSIFPegawai = SIF_PegawaiRepo.GetSIFPegawaiCbo();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListSIFPegawai);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("DeletePegawai")]
        public async Task<ActionResult<Response>> DeletePegawai([FromForm] List<SIF_Pegawai> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Pegawai item in data)
                {
                    await MasterRepo.DeletePegawai(item.Kode_Pegawai, trans);
                    await HelperRepo.DeleteMUSER(item.Kode_Pegawai, trans);
                    await MasterRepo.DeleteSALES(item.Kode_Pegawai, trans);
                    await MasterRepo.DeleteUserRole(item.userlogin, trans);
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
        [HttpPost("UpdateSIFPegawai")]
        public async Task<ActionResult<Response>> UpdateSIFPegawai([FromForm] List<SIF_Pegawai> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Pegawai item in data)
                {
                    await SIF_PegawaiRepo.UpdatePegawai(item, trans);

                }
                // data = SIF_PegawaiRepo.assignData(data);


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