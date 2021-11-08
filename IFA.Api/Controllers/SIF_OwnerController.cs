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
using ERP.Api;

using System.Diagnostics;
using System.IO;

using IFA.Api.Utils;

using IFA.Domain.Utils;

using System.Linq.Dynamic.Core;


namespace IFA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SIF_OwnerController : ControllerBase
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

        [HttpGet("Get_Owner")]
        public ActionResult<string> Get_Owner(string Kd_Owner = "")
        {
            var successReponse = new Response();
            IEnumerable<SIF_Owner> ListSIFBarang = new List<SIF_Owner>();

            try
            {
                ListSIFBarang = SIF_OwnerRepo.Get_Owner(Kd_Owner);

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
        [HttpGet("GetALL_Owner")]
        public ActionResult<string> GetALL_Owner()
        {
            var successReponse = new Response();
            List<SIF_Owner> ListSIFBarang = new List<SIF_Owner>();

            try
            {
                ListSIFBarang = SIF_OwnerRepo.GetALL_Owner();

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

        [HttpPost("SaveSIFOwner")]
        public ActionResult<Response> SaveSIFOwner([FromForm] SIF_Owner data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                data = SIF_OwnerRepo.assignData(data);
                SIF_OwnerRepo.SaveOwner(data, trans);

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

        [HttpPost("UpdateSIFOwner")]
        public ActionResult<Response> UpdateSIFOwner([FromForm] List<SIF_Owner> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Owner item in data)
                {
                    SIF_OwnerRepo.UpdateOwner(item, trans);

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
            IEnumerable<SIF_Owner> ListOwner = new List<SIF_Owner>();
            var successReponse = new Response();


            try
            {
                ListOwner = SIF_OwnerRepo.GetALL_Owner();
                var OwnerList = ListOwner.Select(s => new {
                    s.Kd_Owner,
                    s.Nama_Owner
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

        [HttpPost("DeleteOwner")]
        public ActionResult<Response> DeleteOwner([FromForm] List<SIF_Owner> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Owner item in data)
                {
                    SIF_OwnerRepo.DeleteOwner(item, trans);
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

        [HttpPost("SaveListOwner")]
        public ActionResult<Response> SaveListOwner([FromForm] List<SIF_Owner> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Owner item in data)
                {
                    SIF_OwnerRepo.SaveListOwner(item, trans);

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

        [HttpGet("GetOwnerPartial")]
        public async Task<ActionResult<string>> GetOwnerPartial(string sorting, string filter, int skip, int take, int pageSize, int page, DateTime DateFrom, DateTime DateTo, string owner,int harga,string pet)
        {
            var successReponse = new Response();
            List<SIF_Owner> ListPO = new List<SIF_Owner>();

            try
            {
                var filterquery = "";
                var sortingquery = "";
                if (filter != null && filter != "" && filter != "null")
                {
                    var filtermodel = JsonConvert.DeserializeObject<KendoGridParameterParser.Models.Filter>(filter);

                    filterquery = FilterExtension.RecursiveFilterExpressionBuilder(filtermodel);

                }

                if (sorting != null && sorting != "" && sorting != "null")
                {
                    var sortingmodel = JsonConvert.DeserializeObject<List<SortDescription>>(sorting);
                    if (sortingmodel.Count != 0)
                    {
                        sortingquery = SortingExtension.SortExpressionBuilder(sortingmodel);
                        sortingquery = string.Format(" ORDER BY {0}", sortingquery);
                    }

                }
                int seq = pageSize + skip;
                ListPO = await SIF_OwnerRepo.GetOwnerPartial(DateFrom, DateTo, filterquery, sortingquery, owner,harga,pet);

                var query = ListPO.AsEnumerable().Skip(skip).Take(pageSize);

                successReponse.Success = true;
                successReponse.Result = SIF_OwnerRepo.getCountPO(DateFrom, DateTo, filterquery, sortingquery, owner, harga,pet).Result;
                successReponse.Message = JsonConvert.SerializeObject(query.ToList());
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

                if (Startup.application != "development")
                {
                    var path = Path.Combine(Startup.contentRoot, "appsettings.json");

                    string emailbody = EmailErrorLog.createHtml(e.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }

            }
            return JsonConvert.SerializeObject(successReponse);
        }

    }
}