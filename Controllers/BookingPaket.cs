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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IFA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingPaketController : ControllerBase
    {
        [HttpPost("Save")]
        [RequestFormLimits(ValueCountLimit = 214748364)]
        public async Task<ActionResult<Response>> Save([FromForm] SIF_BOOKING_PAKET data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {

                if (data.No_Paket == "" || data.No_Paket == null)
                {
                    data.No_Paket = await HelperRepo.GetNoTransNew("MDP", DateTime.Now, data.Kd_Cabang, trans, conn);
                    foreach (var item in data.details)
                    {
                        item.No_Paket = data.No_Paket;
                    }
                    await BookingPaketRepo.Insert(data, trans);
                }
                else
                {
                    await BookingPaketRepo.Update(data, trans);
                }

                await BookingPaketRepo.DeleteDetail(data, trans);
                foreach (var item in data.details)
                {
                    await BookingPaketRepo.SaveDetail(item, trans);
                }
                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = data.No_Paket;

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

        [HttpGet("GetPaketbyID")]
        public async Task<ActionResult<string>> GetPaketbyID(string id)
        {
            var successReponse = new Response();
            SIF_BOOKING_PAKET result = new SIF_BOOKING_PAKET();
            IEnumerable<SIF_BOOKING_PAKET_D>  detail = new List<SIF_BOOKING_PAKET_D>();

            try
            {
                result = await BookingPaketRepo.GetPaketByID(id);
                if (result != null)
                {
                    result.Tgl_Paketdesc = result.Tgl_Paket?.ToString("dd MMMM yyyy");
                    result.Tgl_Akhir_Paketdesc = result.Tgl_Akhir_Paket?.ToString("dd MMMM yyyy");

                    detail = await BookingPaketRepo.GetDODetailByID(id);

                    if (detail != null && detail.Count() > 0)
                    {
                        result.details = new List<SIF_BOOKING_PAKET_D>();
                        foreach (var item in detail)
                        {
                            result.details.Add(item);
                        }
                    }
                }


                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(result);
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

        [HttpGet("GetPaket")]
        public async Task<ActionResult<string>> GetPaket()
        {
            var successReponse = new Response();
            IEnumerable<SIF_BOOKING_PAKET> result = new List<SIF_BOOKING_PAKET>();
            IEnumerable<SIF_BOOKING_PAKET_D> detail = new List<SIF_BOOKING_PAKET_D>();

            try
            {
                result = await BookingPaketRepo.GetPaket();
                if (result != null)
                {
                    foreach(var item in result)
                    {
                        item.Tgl_Paketdesc = item.Tgl_Paket?.ToString("dd MMMM yyyy");
                        item.Tgl_Akhir_Paketdesc = item.Tgl_Akhir_Paket?.ToString("dd MMMM yyyy");
                    }
                   
                }


                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(result);
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
