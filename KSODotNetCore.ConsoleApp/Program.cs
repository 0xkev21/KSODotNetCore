// See https://aka.ms/new-console-template for more information
using KSODotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

//SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
//stringBuilder.DataSource = "VivoBook";
//stringBuilder.InitialCatalog = "KSODotNetCore";
//stringBuilder.UserID = "sa";
//stringBuilder.Password = "sa@123";

//SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
//connection.Open();
//Console.WriteLine("Connection Opened");

//string query = "select * from tbl_blog";
//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
//DataTable dt = new DataTable();
//sqlDataAdapter.Fill(dt);

//connection.Close();
//Console.WriteLine("Connection Closed");

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine("Blog Id: "+ dr["BlogId"]);
//    Console.WriteLine("Blog Title: "+ dr["BlogTitle"]);
//    Console.WriteLine("Blog Author: "+ dr["BlogAuthor"]);
//    Console.WriteLine("Blog Content: "+ dr["BlogContent"]);
//    Console.WriteLine("-----------------------------");
//}

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
////adoDotNetExample.Read();
////adoDotNetExample.Create("title", "author", "content");
////adoDotNetExample.Delete(9);
//adoDotNetExample.Edit(9);
//adoDotNetExample.Edit(8);

DapperExample dapper = new DapperExample();
dapper.Run();

Console.ReadKey();