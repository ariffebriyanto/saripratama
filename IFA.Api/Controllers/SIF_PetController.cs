using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ERP.Api;
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
    public class SIF_PetController : ControllerBase
    {
        [HttpGet("GetSIFBarangCbo")]
        public ActionResult<string> GetSIFBarangCbo()
        {
            var successReponse = new Response();
            List<SIF_BarangCbo> ListSIFBarang = new List<SIF_BarangCbo>();

            try
            {
                ListSIFBarang = SIF_BarangRepo.GetSIFBarangCbo();

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

        [HttpGet("GetSIFBarangCB")]
        public ActionResult<string> GetSIFBarangCB()
        {
            var successReponse = new Response();
            List<SIF_BarangCbo> ListSIFBarang = new List<SIF_BarangCbo>();

            try
            {
                ListSIFBarang = SIF_BarangRepo.GetSIFBarangCB();

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

        [HttpGet("GetSIFBarangCboSaldo")]
        public ActionResult<string> GetSIFBarangCboSaldo(string cb)
        {
            var successReponse = new Response();
            List<SIF_BarangCbo> ListSIFBarang = new List<SIF_BarangCbo>();

            try
            {
                ListSIFBarang = SIF_BarangRepo.GetSIFBarangCboSaldo(cb);

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

        [HttpGet("GetSIFBarangCboSaldoGudang")]
        public ActionResult<string> GetSIFBarangCboSaldoGudang(string cb)
        {
            var successReponse = new Response();
            List<SIF_BarangCbo> ListSIFBarang = new List<SIF_BarangCbo>();

            try
            {
                ListSIFBarang = SIF_BarangRepo.GetSIFBarangCboSaldoGudang(cb);

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




        [HttpGet("Get_Pet")]
        public ActionResult<string> Get_Pet(string Kd_Pet = "")
        {
            var successReponse = new Response();
            IEnumerable<SIF_Pet> ListSIFBarang = new List<SIF_Pet>();

            try
            {
                ListSIFBarang = SIF_PetRepo.Get_Pet(Kd_Pet);

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
        [HttpGet("GetALL_Pet")]
        public ActionResult<string> GetALL_Pet()
        {
            var successReponse = new Response();
            List<SIF_Pet> ListSIFBarang = new List<SIF_Pet>();

            try
            {
                ListSIFBarang = SIF_PetRepo.GetALL_Pet();

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

        [HttpPost("SaveSIFPet")]
        public ActionResult<Response> SaveSIFPet([FromForm] SIF_Pet data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                data = SIF_PetRepo.assignData(data);
                SIF_PetRepo.SavePet(data, trans);

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

        [HttpPost("UpdateSIFPet")]
        public ActionResult<Response> UpdateSIFPet([FromForm] List<SIF_Pet> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Pet item in data)
                {
                    SIF_PetRepo.UpdatePet(item, trans);

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


        [HttpGet("GetOwnerGudang")]
        public ActionResult<string> GetOwnerGudang()
        {
            IEnumerable<SIF_Pet> ListOwner = new List<SIF_Pet>();
            var successReponse = new Response();


            try
            {
                ListOwner = SIF_PetRepo.GetALL_Pet();
                var OwnerList = ListOwner.Select(s => new {
                    s.Kd_Pet,
                    s.Nama_Pet
                }).ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(OwnerList);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

       

        [HttpPost("DeletePet")]
        public ActionResult<Response> DeletePet([FromForm] List<SIF_Pet> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Pet item in data)
                {
                    SIF_PetRepo.DeletePet(item, trans);
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

        [HttpPost("SaveListPet")]
        public ActionResult<Response> SaveListPet([FromForm] List<SIF_Pet> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Pet item in data)
                {
                    SIF_PetRepo.SaveListPet(item, trans);

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

        [HttpGet("GetDetailMPO")]
        public async Task<ActionResult<string>> GetDetailMPO(int kd_owner=0,string pet=null, DateTime? DateFrom = null, DateTime? DateTo = null, int harga=0, string owner = null)
        {
            var successReponse = new Response();
            IEnumerable<SIF_Pet> ListPO = new List<SIF_Pet>();

            try
            {
                ListPO = await SIF_PetRepo.GetPetDetail(kd_owner,pet, DateFrom, DateTo, harga, owner);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListPO);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();


            }
            return JsonConvert.SerializeObject(successReponse);
        }

    }
}