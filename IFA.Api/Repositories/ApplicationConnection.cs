using Dapper;
using ERP.Api.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Api.Repositories
{
    public class ApplicationConnection : IApplicationConnection
    {
        public ApplicationConnection(string connectionString)
        {
            this.Connection = new SqlConnection(connectionString);
            this.Connection.Open();
        }

        public IDbConnection Connection { get; private set; }

        public IDbTransaction Transaction { get; private set; }

        public void BeginTransaction()
        {
            if (this.Transaction == null)
            {
                this.Transaction = this.Connection.BeginTransaction();
            }
            else
            {
                throw new Exception("Another transaction already began.");
            }
        }

        public void CommitTransaction()
        {
            if (this.Transaction == null)
            {
                throw new Exception("No transaction has began.");
            }
            else
            {
                this.Transaction.Commit();
            }
        }

        public void RollbackTransaction()
        {
            if (this.Transaction == null)
            {
                throw new Exception("No transaction has began.");
            }
            else
            {
                this.Transaction.Rollback();
            }
        }

        public void Dispose()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Dispose();
            }
            if (this.Connection != null)
            {
                this.Connection.Dispose();
            }
        }

        public IDbCommand CreateCommand()
        {
            IDbCommand result = this.Connection.CreateCommand();
            if (this.Transaction != null)
            {
                result.Transaction = this.Transaction;
            }
            return result;
        }

        public IEnumerable<dynamic> Query(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return this.Connection.Query(sql, param, this.Transaction, buffered, commandTimeout, commandType);
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return this.Connection.Query<T>(sql, param, this.Transaction, buffered, commandTimeout, commandType);
        }

        public int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return this.Connection.Execute(sql, param, this.Transaction, commandTimeout, commandType); ;
        }
    }
}
