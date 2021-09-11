using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Api.Interface
{
    public interface IApplicationConnection : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        IDbCommand CreateCommand();
        IEnumerable<dynamic> Query(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);
    }
}
