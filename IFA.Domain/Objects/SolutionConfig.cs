using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Domain.Objects
{
    public class SolutionConfig
    {
        public bool IsDevelopment { get; set; }
        public string ConnectionString { get; set; }
        public string AzureStorageConnection { get; set; }
        public string AzureContainerName { get; set; }
        public string Url_FileFolder { get; set; }
        public string Url_Web { get; set; }
        public string Url_OVPAPI { get; set; }
        public string Url_CoreAPI { get; set; }
        public string Url_EmployeeAPI { get; set; }
        public string FolderLocationSaveRequest { get; set; }
        public string FolderLocationSaveCredit { get; set; }
        public string ImpUser { get; set; }
        public string impPassword { get; set; }
        public string impDomain { get; set; }
        public string impServerIP { get; set; }
        public string impFolder { get; set; }
        public string FolderLocationSaveMassMaintenance { get; set; }
        public string FolderLocationSaveMassMaintenanceToDB { get; set; }
        public string ExcelSamplingFolder { get; set; }

        public string SIF_API_Server { get; set; }
        public string namaCompany { get; set; }
        public string namaApp { get; set; }
        public string dateServer { get; set; }
        public string application { get; set; }

    }

}
