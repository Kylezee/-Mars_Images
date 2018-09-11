using Dapper;
using System.Collections.Generic;
using System.Data;

namespace kfe.Infrastructure.DataAccess.SqlExecutor
{
    public class SqlExecutor : IDbExecutor
    {
        private readonly IDbConnection _dbConnection;

        public SqlExecutor(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Dispose()
        {
            _dbConnection.Dispose();
        }

        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.Execute(
                sql,
                param,
                transaction,
                commandTimeout,
                commandType);
        }

        public IEnumerable<dynamic> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.Query(
                 sql,
                 param,
                 transaction,
                 buffered,
                 commandTimeout,
                 commandType);
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.Query<T>(
                 sql,
                 param,
                 transaction,
                 buffered,
                 commandTimeout,
                 commandType);
        }

        public T QuerySingle<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.QuerySingle<T>(
                sql,
                param,
                transaction,
                commandTimeout,
                commandType);
        }

    }
}
