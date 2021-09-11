using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly FactoryClass factoryClass;
        protected IHttpContextAccessor httpContextAccessor { get; private set; }
        protected IDictionary<string, string> AuthenticationProperties { get; set; }

        public BaseController(
                FactoryClass factoryClass,
                IHttpContextAccessor httpContextAccessor
             )
        {
            this.factoryClass = factoryClass;
            this.httpContextAccessor = httpContextAccessor;
            //this.AuthenticationProperties = httpContextAccessor.HttpContext.AuthenticateAsync().Result.Properties.Items;


        }

        public string GetUserPropertyByKey(string key)
        {
            string result;
            AuthenticationProperties.TryGetValue(".Token." + key, out result);
            return result;
        }


        //public DateTime GetTodayDateTime()
        //{
        //    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(Startup.Configuration.GetSection("AppConfig").GetSection("TimeZoneID").Value);
        //    var dt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        //    DateTime dateReturn = TimeZoneInfo.ConvertTimeFromUtc(dt, cstZone);
        //    return dateReturn;
        //}

        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    ViewBag.DateTimeServer = this.GetTodayDateTime();
        //    base.OnActionExecuting(filterContext);
        //}
    }
}