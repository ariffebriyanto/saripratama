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
using IFA.Domain.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IFA.Api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        [HttpPost("SaveCustomer")]
        public async Task<ActionResult<Response>> SaveCustomer([FromForm] SIF_CUSTOMER data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {

                await MasterRepo.SaveCustomer(data, trans);

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
        [HttpPost("SaveBukuBesar")]
        public async Task<ActionResult<Response>> SaveBukuBesar([FromForm] SIF_buku_besar data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {

                await MasterRepo.SaveBukuBesar(data, trans);

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

        [HttpGet("GetStatPO")]
        public async Task<ActionResult<string>> GetStatPO()
        {
            IEnumerable<DashboardVM> ListGudang = new List<DashboardVM>();
            var successReponse = new Response();
            try
            {
                ListGudang = await HelperRepo.GetStatPO();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListGudang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }
        [HttpGet("GetStatBooked")]
        public async Task<ActionResult<string>> GetStatBooked()
        {
            IEnumerable<DashboardVM> ListGudang = new List<DashboardVM>();
            var successReponse = new Response();
            try
            {
                ListGudang = await HelperRepo.GetStatBooked();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListGudang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetStatMTS")]
        public async Task<ActionResult<string>> GetStatMTS()
        {
            IEnumerable<DashboardVM> ListGudang = new List<DashboardVM>();
            var successReponse = new Response();
            try
            {
                ListGudang = await HelperRepo.GetStatMTS();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListGudang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpGet("GetStatPIU")]
        public async Task<ActionResult<string>> GetStatPIU()
        {
            IEnumerable<DashboardVM> ListGudang = new List<DashboardVM>();
            var successReponse = new Response();
            try
            {
                ListGudang = await HelperRepo.GetStatPIU();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListGudang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }



        [HttpGet("GetStatDUE")]
        public async Task<ActionResult<string>> GetStatDUE()
        {
            IEnumerable<DashboardVM> ListGudang = new List<DashboardVM>();
            var successReponse = new Response();
            try
            {
                ListGudang = await HelperRepo.GetStatDUE();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListGudang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("SaveSupplier")]
        public async Task<ActionResult<Response>> SaveSupplier([FromForm] SIF_CUSTOMER data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {

                await MasterRepo.SaveSupplier(data, trans);

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

        [HttpGet("GetSIFBukuBesar")]
        public async Task<ActionResult<string>> GetSIFBukuBesar()
        {
            var successReponse = new Response();
            IEnumerable<SIF_buku_besar> ListSIFBukuBesar = new List<SIF_buku_besar>();

            try
            {
                ListSIFBukuBesar = await MasterRepo.GetSIFBukuBesar();

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

        [HttpGet("GetSopir")]
        public async Task<ActionResult<string>> GetSopir()
        {
            var successReponse = new Response();
            IEnumerable<SIF_Sopir> ListData = new List<SIF_Sopir>();

            try
            {
                ListData = await MasterRepo.GetSopir();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }
        [HttpGet("GetKendaraan")]
        public async Task<ActionResult<string>> GetKendaraan()
        {
            var successReponse = new Response();
            IEnumerable<SIF_Kendaraan> ListData = new List<SIF_Kendaraan>();

            try
            {
                ListData = await MasterRepo.GetKendaraan();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListData);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }
        [HttpGet("GetSIFHarga")]
        public async Task<ActionResult<string>> GetSIFHarga(string Kd_cabang)
        {
            var successReponse = new Response();
            IEnumerable<SIF_Harga> ListSIFBukuBesar = new List<SIF_Harga>();

            try
            {
                ListSIFBukuBesar = await MasterRepo.GetSIFHarga(Kd_cabang);

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

        [HttpPost("UpdateHarga")]
        public async Task<ActionResult<Response>> UpdateHarga([FromForm] List<SIF_Harga> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try

            {
                foreach (SIF_Harga item in data)
                {
                   
                       await MasterRepo.UpdateSIFHarga(item, trans);
                    
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
            finally
            {
                //close transaction -> commit success or fail
                DataAccess.DisposeConnectionAndTransaction(conn, trans);
            }

            return successReponse;
        }

        [HttpGet("GetCustomerMobile")]
        public async Task<ActionResult<string>> GetCustomerMobile()
        {
            IEnumerable<CustomerVM> ListGudang = new List<CustomerVM>();
            var successReponse = new Response();
            try
            {
                ListGudang = await MasterRepo.GetCustomerMobile();
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListGudang);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(ListGudang);
        }
        [HttpGet("GetCustomer")]
        public async Task<ActionResult<string>> GetCustomer()
        {
            var successReponse = new Response();
            IEnumerable<CustomerVM> ListCustomer = new List<CustomerVM>();

            try
            {
                ListCustomer = await MasterRepo.GetCustomer();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListCustomer);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("UpdateCustomer")]
        public async Task<ActionResult<Response>> UpdateCustomer([FromForm] List<CustomerVM> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (CustomerVM item in data)
                {
                    await MasterRepo.UpdateCustomer(item, trans);

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

        [HttpPost("UpdateBukuBesar")]
        public async Task<ActionResult<Response>> UpdateBukuBesar([FromForm] List<SIF_buku_besar> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_buku_besar item in data)
                {
                    await MasterRepo.UpdateBukuBesar(item, trans);

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

        [HttpPost("DeleteCustomer")]
        public async Task<ActionResult<Response>> DeleteCustomer([FromForm] List<CustomerVM> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (CustomerVM item in data)
                {
                    await MasterRepo.DeleteCustomer(item, trans);

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

       

        [HttpPost("SaveListCustomer")]
        public async Task<ActionResult<Response>> SaveListCustomer([FromForm] List<CustomerVM> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (CustomerVM item in data)
                {
                    await MasterRepo.SaveListCustomer(item, trans);

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
        [HttpGet("GetRole")]
        public async Task<ActionResult<string>> GetRole()
        {
            var successReponse = new Response();
            List<MUSER_ROLE> ListSIFPegawai = new List<MUSER_ROLE>();

            try
            {
                ListSIFPegawai = await MasterRepo.GetRole();

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
        [HttpPost("UpdateRole")]
        public async Task<ActionResult<Response>> UpdateRole([FromForm] List<MUSER_ROLE> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (MUSER_ROLE item in data)
                {
                    await MasterRepo.UpdateRole(item, trans);

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

        [HttpPost("SaveRole")]
        public async Task<ActionResult<Response>> SaveRole([FromForm] List<MUSER_ROLE> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);

            try
            {
                //'string KD_SALES = await HelperRepo.GetNoTransNew("MSALES", DateTime.Now, data.FirstOrDefault().Kd_Cabang, trans, conn);
                foreach (MUSER_ROLE item in data)
                {
                    //await SIF_PegawaiRepo.SavePegawai(item, trans);
                    //await SIF_PegawaiRepo.SaveSales(item, KD_SALES, trans);
                    await MasterRepo.SaveRole(item, trans);  //SaveMUSER

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

        [HttpGet("GetSupplier")]
        public async Task<ActionResult<string>> GetSupplier()
        {
            var successReponse = new Response();
            IEnumerable<SIF_Supplier> ListCustomer = new List<SIF_Supplier>();

            try
            {
                ListCustomer = await MasterRepo.GetSupplier();

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListCustomer);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("UpdateSupplier")]
        public async Task<ActionResult<Response>> UpdateSupplier([FromForm] List<SIF_Supplier> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Supplier item in data)
                {
                    await MasterRepo.UpdateSupplier(item, trans);

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

        [HttpPost("UpdateKendaraan")]
        public async Task<ActionResult<Response>> UpdateKendaraan([FromForm] List<SIF_Kendaraan> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Kendaraan item in data)
                {
                    await MasterRepo.UpdateKendaraan(item, trans);

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

        [HttpPost("DeleteSupplier")]
        public async Task<ActionResult<Response>> DeleteSupplier([FromForm] List<SIF_Supplier> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Supplier item in data)
                {
                    await MasterRepo.DeleteSupplier(item, trans);

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

        [HttpPost("DeleteKendaraan")]
        public async Task<ActionResult<Response>> DeleteKendaraan([FromForm] List<SIF_Kendaraan> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Kendaraan item in data)
                {
                    await MasterRepo.DeleteKendaraan(item, trans);

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

        [HttpPost("SaveListSupplier")]
        public async Task<ActionResult<Response>> SaveListSupplier([FromForm] List<SIF_Supplier> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Supplier item in data)
                {
                    await MasterRepo.SaveListSupplier(item, trans);

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

        [HttpPost("SaveListKendaraan")]
        public async Task<ActionResult<Response>> SaveListKendaraan([FromForm] List<SIF_Kendaraan> data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                foreach (SIF_Kendaraan item in data)
                {
                    await MasterRepo.SaveListKendaraan(item, trans);

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