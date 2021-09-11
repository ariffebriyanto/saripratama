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
    public class SIF_BarangController : ControllerBase
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

        [HttpGet("Get_Barang")]
        public ActionResult<string> Get_Barang(string Kode_Barang = "")
        {
            var successReponse = new Response();
            IEnumerable<SIF_Barang> ListSIFBarang = new List<SIF_Barang>();

            try
            {
                ListSIFBarang = SIF_BarangRepo.Get_Barang(Kode_Barang);

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
        [HttpGet("GetALL_Barang")]
        public ActionResult<string> GetALL_Barang()
        {
            var successReponse = new Response();
            List<SIF_Barang> ListSIFBarang = new List<SIF_Barang>();

            try
            {
                ListSIFBarang = SIF_BarangRepo.GetALL_Barang();

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

        [HttpPost("SaveSIFBrg")]
        public ActionResult<Response> SaveSIFBrg([FromForm] SIF_Barang data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                data = SIF_BarangRepo.assignData(data);
                SIF_BarangRepo.SaveBrg(data, trans);

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

        [HttpPost("UpdateSIFBrg")]
        public ActionResult<Response> UpdateSIFBrg([FromForm] List<SIF_Barang> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach(SIF_Barang item in data)
                {
                    SIF_BarangRepo.UpdateBrg(item, trans);

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


        [HttpGet("GetBarangGudang")]
        public ActionResult<string> GetBarangGudang()
        {
            IEnumerable<SIF_Barang> ListBarang = new List<SIF_Barang>();
            var successReponse = new Response();


            try
            {
                ListBarang = SIF_BarangRepo.GetALL_Barang();
                var BarangList = ListBarang.Select(s => new {
                    s.Kode_Barang,
                    s.Nama_Barang
                }).ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(BarangList);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetBarangMobile/{id}")]
        public ActionResult<string> GetBarangMobile(string id)
        {
            IEnumerable<SIF_Barang> ListBarang = new List<SIF_Barang>();
            var successReponse = new Response();
            try
            {
                ListBarang = SIF_BarangRepo.GetALL_BarangMobile(id);
                var BarangList = ListBarang.Select(s => new {
                    s.Kode_Barang,
                    s.Nama_Barang
                }).ToList();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(BarangList);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetSIFBukuBesarCbo")]
        public async Task<ActionResult<string>> GetSIFBukuBesarCbo()
        {
            var successReponse = new Response();
            IEnumerable<SIF_PersediaanCbo> ListSIFPersediaan = new List<SIF_PersediaanCbo>();

            try
            {
                ListSIFPersediaan = await HelperRepo.GetSIFBukuBesarCbo();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListSIFPersediaan);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetSIFKategoriCbo")]
        public async Task<ActionResult<string>> GetSIFKategoriCbo()
        {
            var successReponse = new Response();
            IEnumerable<SIF_PersediaanCbo> ListSIFPersediaan = new List<SIF_PersediaanCbo>();

            try
            {
                ListSIFPersediaan =await HelperRepo.GetSIFBukuBesarCbo();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListSIFPersediaan);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetrekPenjualan2Cbo")]
        public async Task<ActionResult<string>> GetrekPenjualan2Cbo()
        {
            var successReponse = new Response();
            IEnumerable<rek_penjualan2Cbo> ListSIFPersediaan = new List<rek_penjualan2Cbo>();

            try
            {
                ListSIFPersediaan = await HelperRepo.Getrek_penjualan2Cbo();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListSIFPersediaan);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("DeleteBrg")]
        public ActionResult<Response> DeleteBrg([FromForm] List<SIF_Barang> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Barang item in data)
                {
                    SIF_BarangRepo.UpdateBrg(item, trans);
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

        [HttpPost("SaveListBarang")]
        public ActionResult<Response> SaveListBarang([FromForm] List<SIF_Barang> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Barang item in data)
                {
                    SIF_BarangRepo.SaveListBarang(item, trans);

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