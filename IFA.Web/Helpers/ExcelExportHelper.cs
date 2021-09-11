using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Web.Helpers
{
    public class ExcelExportHelper
    {
        public static string ExcelContentType { get { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; } }

        public static DataTable ToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); 
            }

            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }

                table.Rows.Add(values);
            }
            return table;
        }

        public static byte[] ExportExcel(DataTable dt)
        {
            byte[] result = null;
            ExcelPackage pck = new ExcelPackage();

            using (pck)
            {
                ExcelWorksheet wsDt = pck.Workbook.Worksheets.Add("Sheet1");
                wsDt.Cells["A1"].LoadFromDataTable(dt, true, TableStyles.None);
                int colindex = 1;
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.DataType == typeof(DateTime))
                    {
                        wsDt.Column(colindex).Style.Numberformat.Format = "dd/MM/yyyy";
                    }
                    else if (col.DataType == typeof(decimal))
                    {
                        wsDt.Column(colindex).Style.Numberformat.Format = "#,##0.00";
                    }
                    else if (col.DataType == typeof(int))
                    {
                        wsDt.Column(colindex).Style.Numberformat.Format = "#";
                    }
                    wsDt.Column(colindex).AutoFit();

                    colindex++;
                }

                result = pck.GetAsByteArray(); 

            }
            return result;
        }

        public static byte[] ExportExcel<T>(List<T> data)
        {
            return ExportExcel(ToDataTable<T>(data));
        }
    }
}
