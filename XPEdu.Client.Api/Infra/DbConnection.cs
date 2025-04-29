using MySqlConnector;
using System.Data;

namespace XPEdu.Client.Api.Infra
{
    public static class DbConnection
    {
        public static IDbConnection GetConnection()
        {
            return new MySqlConnection("Server=localhost;Database=xpedu;User=root;Password=admin123;");
        }
    }
}
