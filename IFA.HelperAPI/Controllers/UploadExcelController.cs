using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IFA.HelperAPI.Controllers
{
    public class UploadExcelController : ApiController
    {
        
        [Route("ImportBarang")]
        [HttpPost]
        public bool ImportBarang()
        {



            var FileUpload = HttpContext.Current.Request.Files.Count > 0 ?
            HttpContext.Current.Request.Files[0] : null;

            string conString = string.Empty;
            string filename = FileUpload.FileName;
            string targetpath = HttpContext.Current.Server.MapPath("~/Content/Upload/");
            FileUpload.SaveAs(targetpath + filename);
            string pathToExcelFile = targetpath + filename;
            string extension = Path.GetExtension(filename);

            switch (extension)
            {
                case ".xls": //Excel 97-03
                    conString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    break;

            }
            conString = string.Format(conString, FileUpload);
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();

                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[2] {
                new DataColumn("Barang", typeof(string)),
                new DataColumn("Harga",typeof(decimal)) });

                DataTable dt = new DataTable();

                dt.Columns.AddRange(new DataColumn[4] {
                    new DataColumn("Batch", typeof(Guid)),
                    new DataColumn("ID", typeof(Guid)),
                new DataColumn("Barang", typeof(string)),
                new DataColumn("Harga",typeof(decimal)) });

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                }
                excel_con.Close();
                Guid Batch = Guid.NewGuid();
                for (int i = 0; i <= dtExcelData.Rows.Count - 1; i++)
                {
                    Guid id = Guid.NewGuid();
                    DataRow dr = dt.NewRow();
                    dr[0] = Batch;
                    dr[1] = id;
                    dr[2] = dtExcelData.Rows[i][0];
                    dr[3] = dtExcelData.Rows[i][1];

                    dt.Rows.Add(dr);
                }

                string consString = System.Configuration.ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.SIF_BARANGTEMP";

                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Batch", "Batch");
                        sqlBulkCopy.ColumnMappings.Add("ID", "ID");
                        sqlBulkCopy.ColumnMappings.Add("Barang", "Barang");
                        sqlBulkCopy.ColumnMappings.Add("Harga", "Harga");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
                return true;
            }
        }

    }
}
