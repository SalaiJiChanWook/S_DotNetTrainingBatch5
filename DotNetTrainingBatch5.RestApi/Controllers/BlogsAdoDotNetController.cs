using DotNetTrainingBatch5.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using DotNetTrainingBatch5.RestApi.ViewModels;
using DotNetTrainingBatch5.RestApi.DataModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System;

namespace DotNetTrainingBatch5.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source= UCHIASALAI\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=salai;Password=Vpjtqwv23@#";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId],
                            [BlogTitle],
                            [BlogAuthor],
                            [BlogContent],
                            [DeleteFlag]
                        FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                {
                    Console.WriteLine(reader["BlogId"]);
                    Console.WriteLine(reader["BlogTitle"]);
                    Console.WriteLine(reader["BlogAuthor"]);
                    Console.WriteLine(reader["BlogContent"]);
                    //lst.Add(new BlogViewModel
                    //{
                    //    Id = Convert.ToInt32(reader["BlogId"]),
                    //    Title = Convert.ToString(reader["BlogTitle"]),
                    //    Author = Convert.ToString(reader["BlogAuthor"]),
                    //    Content = Convert.ToString(reader["BlogContent"]),
                    //    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
                    //});
                    var item = new BlogViewModel
                    {
                        Id = Convert.ToInt32(reader["BlogId"]),
                        Title = Convert.ToString(reader["BlogTitle"]),
                        Author = Convert.ToString(reader["BlogAuthor"]),
                        Content = Convert.ToString(reader["BlogContent"]),
                        DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
                    };
                    lst.Add(item);
                }
            }
            connection.Close();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"SELECT [BlogId],
                            [BlogTitle],
                            [BlogAuthor],
                            [BlogContent],
                            [DeleteFlag]
                     FROM [dbo].[Tbl_Blog] 
                     WHERE BlogId = @BlogId AND DeleteFlag = 0";
            
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                //Console.WriteLine(reader["BlogId"]);
                //Console.WriteLine(reader["BlogTitle"]);
                //Console.WriteLine(reader["BlogAuthor"]);
                //Console.WriteLine(reader["BlogContent"]);
                var blogItem = new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
                };
                connection.Close();
                return Ok(blogItem);
                
            }
            else
            {
                return NotFound("Blog not found.");
            }
            


        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            //while (reader.Read())
            //{

            //    {
            //        Console.WriteLine(reader["BlogId"]);
            //        Console.WriteLine(reader["BlogTitle"]);
            //        Console.WriteLine(reader["BlogAuthor"]);
            //        Console.WriteLine(reader["BlogContent"]);
                 { 
                    command.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
                    command.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
                    command.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
                }
                    
                
            //}
            int result = command.ExecuteNonQuery();
            connection.Close();

            return Ok(result == 1 ? "Saving in Databaes is Successful." : "Saving in Database was Failed.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string queryInsert = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                              ,[DeleteFlag] = 0
                         WHERE BlogId = @BlogId";

            SqlCommand command = new SqlCommand(queryInsert, connection);
            command.Parameters.AddWithValue("@BlogId", id);
            command.Parameters.AddWithValue("@BlogTitle", blog.Title);
            command.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            command.Parameters.AddWithValue("@BlogContent", blog.Content);

            int result = command.ExecuteNonQuery();

            connection.Close();

            //Console.WriteLine(result == 1 ? "Updating successfully.." : "Updating failed..");

            return Ok(result == 1 ? $"Updating the blog where Id={id} is successfully.." : $"Updating the blog where Id={id} was failed..");

        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            string conditions = "";
            if(!string.IsNullOrEmpty(blog.Title))
            {
                conditions += "[BlogTitle] = @BlogTitle,";
            }

            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += "[BlogAuthor] = @BlogAuthor,";
            }

            if (!string.IsNullOrEmpty(blog.Content))
            {
                conditions += "[BlogContent] = @BlogContent,";
            }

            if(conditions.Length == 0)
            {
                return BadRequest("You requested with the Invalid Parameters!");
            }

            conditions = conditions.Substring(0, conditions.Length - 1);

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog]  SET {conditions}
 WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            if (!string.IsNullOrEmpty(blog.Title))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            }
            

            int result = cmd.ExecuteNonQuery();

            connection.Close();
            Console.WriteLine(result == 1 ? "Updating Successful." : "Updating Failed.");
            return Ok(result == 1 ? "Updating Successful." : "Updating Failed.");
           
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"DELETE  FROM [dbo].[Tbl_blog] where BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            int result = cmd.ExecuteNonQuery();

            connection.Close();
            return Ok(result == 1 ? $"Deleting id={id} is Successful." : $"Deleting id={id} was Failed.");

            Console.WriteLine(result == 1 ? "Deleting  Successful." : "Deleting Failed.");


            //var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            //if (item is null)
            //{
            //    return NotFound();
            //}

            //item.DeleteFlag = true;
            //_db.Entry(item).State = EntityState.Modified;

            ////_db.Entry(item).State = EntityState.Deleted;
            //_db.SaveChanges();

            //return Ok();

        }

    }

   
}
