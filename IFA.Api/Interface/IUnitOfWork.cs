using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Api.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        //OVP
        #region Request
        //IRequestDocumentErrorRepo RequestDocumentErrorRepo { get; }
        //IRequestDocumentRepo RequestDocumentRepo { get; }
        //IRequestRepo RequestRepo { get; }
        #endregion

        #region DMERequest
       // IDMERequestRepo dMERequestRepo { get; }
        //IDMEReverseRepo dMEReverseRepo { get; }
        #endregion

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
