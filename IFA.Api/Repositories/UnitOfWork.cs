using ERP.Api.Interface;
using ERP.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(SolutionConfig solutionConfig)
        {
            this.SolutionConfig = solutionConfig;
            this.AppConnection = new ApplicationConnection(this.SolutionConfig.ConnectionString);
        }

        private SolutionConfig SolutionConfig { get; set; }

        private IApplicationConnection AppConnection { get; set; }

        public void BeginTransaction()
        {
            this.AppConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            this.AppConnection.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            this.AppConnection.RollbackTransaction();
        }

        public void Dispose()
        {
            this.AppConnection.Dispose();
        }
    }
}
