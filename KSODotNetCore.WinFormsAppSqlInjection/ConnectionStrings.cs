using System.Data.SqlClient;

namespace KSODotNetCore.WinFormsAppSqlInjection
{
    internal static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "KSODotNetCore",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true,
        };
    }
}
