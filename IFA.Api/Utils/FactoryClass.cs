using ERP.Api.Interface;
using ERP.Domain.Objects;
using ERP.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Api.Utils
{
    public class FactoryClass
    {
        public readonly SolutionConfig config;

        public FactoryClass(SolutionConfig config)
        {
            this.config = config;
        }

        public IUnitOfWork NewUnitOfWork()
        {
            return new UnitOfWork(config);
        }

    }
}
