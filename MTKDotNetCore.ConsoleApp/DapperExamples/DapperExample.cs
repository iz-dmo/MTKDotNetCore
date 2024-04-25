using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MTKDotNetCore.ConsoleApp.Dtos;
using MTKDotNetCore.ConsoleApp.Services;


namespace MTKDotNetCore.ConsoleApp.DapperExamples
{
    internal class DapperExample
    {
        public void Run()
        {
            // Read();
            // Edit(3);
            // Edit(102);
            // Create("dapper_title","dapper_author","dapper_content");
            // Update(4,"dapper_upd_title","dapper_upd_author","dapper_upd_content");
            Delete(4);

        }

        private void Read()
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst= db.Query<BlogDto>("select * from tbl_blog").ToList();
            foreach (BlogDto item in lst){
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("--------------------------");

            }
        }

        private void Edit(int id)
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogDto>("select * from tbl_blog where blogId = @BlogId",new BlogDto{ BlogId = id}).FirstOrDefault();
            if( item is null ){
                Console.WriteLine("No Data Found!");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("--------------------------");

        }

        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
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
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query,item);
            string message = result > 0 ? "Saving Successful." : "Saving failed!";
            Console.WriteLine(message);
        }

        private void Update(int id,string title,string author,string content) 
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string query = @"UPDATE [dbo].[tbl_blog]
                SET
                [BlogTitle] = @BlogTitle,
                [BlogAuthor] = @BlogAuthor,
                [BlogContent] = @BlogContent
                WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query,item);
            string message = result > 0 ? "Updating Successful." : "Updating failed!";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            var item = new BlogDto
            {
                BlogId = id
            };
            string query = @"DELETE FROM tbl_blog where BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query,item);
            string message = result > 0 ? "Deleting Successful." : "Deleting failed!";
            Console.WriteLine(message);        }
    }
}