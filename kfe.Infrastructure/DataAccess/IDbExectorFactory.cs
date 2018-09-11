namespace kfe.Infrastructure.DataAccess
{
    public interface IDbExecutorFactory
    {
        IDbExecutor CreateExecutor(IConnectionStringBuilder connectionStringBuilder);
    }
}
