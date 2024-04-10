using System.Data.SqlClient;
using System.Data;
Console.WriteLine("Hello,World");

SqlConnectionStringBuilder stringBuilder= new SqlConnectionStringBuilder();
stringBuilder.DataSource = "localhost";
stringBuilder.InitialCatalog = "DoNetTraining";
stringBuilder.UserID = "sa";
stringBuilder.Password = "Thway265136";
SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
connection.Open();
Console.WriteLine("Connection Open!");

string query = "select * from tbl_blog";
SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter adapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
adapter.Fill(dt);

connection.Close();
Console.WriteLine("Connection Close!");
foreach ( DataRow dr in dt.Rows )
{
    Console.WriteLine("Blog ID => " + dr["BlogId"]);
    Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
    Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
    Console.WriteLine("Blog Conent => " + dr["BlogContent"]);
    Console.WriteLine("---------------------------------------");
    
}
Console.ReadKey();

