using System.Data;

namespace ReportGenius.Infrastructure.Database.Connection
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
