using System.Data.SqlClient;
using System.Data;

namespace MTKDotNetCore.ConsoleApp
{
    internal class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "localhost",
            InitialCatalog = "DoNetTraining",
            UserID = "sa",
            Password = "Thway265136",
        };

        public void Read()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
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
        }

        // create
        public void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"INSERT INTO [dbo].[tbl_blog]
                (
                    [BlogTitle],
                    [BlogAuthor],
                    [BlogContent]
                )   
                VALUES
                (
                    @BlogTitle,
                    @BlogAuthor, 
                    @BlogContent
                )";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            try
                {
                    int result = cmd.ExecuteNonQuery();
                    Console.WriteLine("Number of rows affected: " + result);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            connection.Close();
        }
    }
}