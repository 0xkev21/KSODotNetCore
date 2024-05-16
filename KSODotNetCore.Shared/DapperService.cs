using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Schema;

namespace KSODotNetCore.Shared
{
    public class DapperService
    {
        private readonly string _connectionString;

        public DapperService (string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            //if(param != null)
            //{
            //    var list = db.Query<T>(query, param).ToList();
            //}
            //else
            //{
            //    var list = db.Query<T>(query).ToList();
            //}
            var list = db.Query<T>(query, param).ToList();
            return list;
        }

        public T QueryFirstOrDefault<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var item = db.Query<T>(query, param).FirstOrDefault();
            return item!;
        }

        public int Execute(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            int result = db.Execute(query, param);
            return result;
        }
    }
}
