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
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Saving Successful." : "Saving failed!";
            Console.WriteLine(message);
        }

        // update
        public void Update(int id,string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"UPDATE [dbo].[tbl_blog]
                SET
                [BlogTitle] = @BlogTitle,
                [BlogAuthor] = @BlogAuthor,
                [BlogContent] = @BlogContent
                WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Updating Successful." : "Updating failed!";
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"DELETE FROM tbl_blog where BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Deleting Successful." : "Deleting failed!";
            Console.WriteLine(message);
        }
    }
}