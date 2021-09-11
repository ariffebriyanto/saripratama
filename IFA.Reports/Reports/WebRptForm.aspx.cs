using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stimulsoft.Report;

namespace PPIC.Reports
{
    public partial class WebRptForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["q"] != null)
            {
                Guid uid = new Guid((string)Request.QueryString["q"]);
                StiReport report = new StiReport();
                report.Load(Server.MapPath("~/Reports/DO.mrt"));
                report.Compile();
                report.Render();
                StiWebViewer1.Report = report;
            }
            else if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"] == "labarugi")
                {
                    if (Request.QueryString["bln"] != null && Request.QueryString["thn"] != null && Request.QueryString["val"] != null)
                    {
                        string uid = Request.QueryString["q"].toString();
                        StiReport report = new StiReport();
                        report.Load(Server.MapPath("~/Reports/repLabaRugi.mrt"));
                        report.Compile();
                        report["bln"] = Request.QueryString["bln"].ToString();
                        report["thn"] = Request.QueryString["thn"].ToString();
                        report["val"] = Request.QueryString["val"].ToString();
                        report["bulan"] = Request.QueryString["bln"].ToString();
                        report["tahun"] = Request.QueryString["thn"].ToString();
                        report["valuta"] = Request.QueryString["val"].ToString();
                        report.Render();
                        StiWebViewer1.Report = report;
                    }
                        
                }
                    
            }
        }
    }
}