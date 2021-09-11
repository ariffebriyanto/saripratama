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
    public class AuthController : ControllerBase
    {
        [HttpPost("GetAuthLogin")]
        public ActionResult<string> GetAuthLogin([FromForm] Auth auth)
        {
            var successReponse = new Response();
            Auth ListUser = new Auth();

            try
            {
                ListUser = AuthRepo.getAuthLogin(auth);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListUser);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("GetAuthOTP")]
        public ActionResult<string> GetAuthOTP([FromForm] Auth auth)
        {
            var successReponse = new Response();
            Auth ListUser = new Auth();
            IEnumerable<Auth> dataList = new List<Auth>();
            //List<Auth> dataList = new List<Auth>();

            try
            {
                dataList = AuthRepo.getAuthOTP(auth.Password);
                if (dataList.Count() > 0)
                {
                    successReponse.Success = true;
                    successReponse.Result = "success";
                }
                else
                {
                    successReponse.Success = false;
                    successReponse.Result = "failed";
                }

            
                //successReponse.Message = JsonConvert.SerializeObject(ListUser);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }


        [HttpPost("GetAuthLoginMobile")]
        public ActionResult<string> GetAuthLoginMobile([FromBody]AuthenticationVM auth)
        {
            var successReponse = new Response();
            AuthenticationVM ListUser = new AuthenticationVM();
            try
            {
                ListUser = AuthRepo.getAuthLoginMobile(auth);

                successReponse.Success = true;
                successReponse.Result = "success";
                successReponse.Message = JsonConvert.SerializeObject(ListUser);
            }
            catch (Exception e)
            {
                successReponse.Success = false;
                successReponse.Result = "failed";
                successReponse.Message = e.Message;

            }
            return JsonConvert.SerializeObject(successReponse);
        }

        [HttpPost("PostUpdateUserToken")]
        public ActionResult<Response> PostUpdateUserToken([FromBody] AuthenticationVM data)
        {
            var successReponse = new Response();
            var conn = DataAccess.GetConnection();
            var trans = DataAccess.OpenTransaction(conn);
            try
            {
                AuthRepo.UpdateUserToken(data, trans);


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
    }
}