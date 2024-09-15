// See https://aka.ms/new-console-template for more information
using DotNetTrainingBatch5.ConsoleAPP;
using System.Data;
using System.Data.SqlClient;
Console.WriteLine("Hello, World!");
//Console.ReadLine();
Console.WriteLine("Hello, Myanmar Pyi Gyi");
//Console.ReadKey();

// ADO.NET
//DAPPER (ORM)
//EFCore / Entity Framework (ORM)

//SqlConnection connection = new SqlConnection("Data Source=UCHIASALAI\\SQLEXPRESS; Initial Catalog=DotNetTrainingBatch5;User ID=salai;Password=Vpjtqwv23@;");
//connection.Open();
//connection.Close();


string connectionString = "Data Source=UCHIASALAI\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=salai;Password=Vpjtqwv23@#;";
Console.WriteLine("Connection string: " + connectionString);
SqlConnection connection = new SqlConnection(connectionString);

Console.WriteLine("Connection is opening...");
connection.Open();
Console.WriteLine("Connection was opened.");

string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//DataTable dt = new DataTable();
//adapter.Fill(dt);
SqlDataReader reader = cmd.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine($"BlogId: {reader["BlogId"]}");
    Console.WriteLine($"BlogTitle: {reader["BlogTitle"]}");
    Console.WriteLine($"BlogAuthor: {reader["BlogAuthor"]}");
    Console.WriteLine($"BlogContent: {reader["BlogContent"]}");
    Console.WriteLine($"DeleteFlag: {reader["DeleteFlag"]}");
    //Console.WriteLine(dr["DeleteFlag"]);
}

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine(dr["BlogId"]);
//    Console.WriteLine(dr["BlogTitle"]);
//    Console.WriteLine(dr["BlogAuthor"]);
//    Console.WriteLine(dr["BlogContent"]);
//    //Console.WriteLine(dr["DeleteFlag"]);
//}

Console.WriteLine("Connection is closing...");
connection.Close();
Console.WriteLine("Connection was closed.");

// DataSet
// DataTable
// DataRow
// DataColumn

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine(dr["BlogId"]);
//    Console.WriteLine(dr["BlogTitle"]);
//    Console.WriteLine(dr["BlogAuthor"]);
//    Console.WriteLine(dr["BlogContent"]);
//    //Console.WriteLine(dr["DeleteFlag"]);
//}


AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();


Console.ReadKey();