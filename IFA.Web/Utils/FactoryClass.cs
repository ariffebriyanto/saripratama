using ERP.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Utils
{
    public class FactoryClass
    {
        public readonly SolutionConfig config;

        public FactoryClass(SolutionConfig config)
        {
            this.config = config;
        }

        //public OVPAPIAccess NewOVPAPIAccess()
        //{
        //    return new OVPAPIAccess(config);
        //}

        //public CoreAPIAccess NewCoreAPIAccess()
        //{
        //    return new CoreAPIAccess(config);
        //}

        //public EmployeeAPIAccess NewEmployeeAPIAccess()
        //{
        //    return new EmployeeAPIAccess(config);
        //}

        public ApiClient APIClientAccess()
        {
            return new ApiClient(config);
        }

        public string GetExcelSamplingFolder()
        {
            return config.ExcelSamplingFolder;
        }
    }
}
