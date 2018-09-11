using System.Data.SqlClient;

namespace kfe.Infrastructure.DataAccess.SqlExecutor
{
    public class SqlExecutorFactory : IDbExecutorFactory
    {
        public IDbExecutor CreateExecutor(IConnectionStringBuilder connectionStringBuilder)
        {

            var dbConnection = new SqlConnection(connectionStringBuilder.Build());
            dbConnection.Open();
            return new SqlExecutor(dbConnection);
        }

    }
}
