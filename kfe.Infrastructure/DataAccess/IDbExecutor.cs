using System;
using System.Collections.Generic;
using System.Data;

namespace kfe.Infrastructure.DataAccess
{
    public interface IDbExecutor : IDisposable
    {
        int Execute(
           string sql,
           object param = null,
           IDbTransaction transaction = null,
           int? commandTimeout = default(int?),
           CommandType? commandType = default(CommandType?));

        IEnumerable<dynamic> Query(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?));

        IEnumerable<T> Query<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?));

        T QuerySingle<T>(
            string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null
            );
    }
}
