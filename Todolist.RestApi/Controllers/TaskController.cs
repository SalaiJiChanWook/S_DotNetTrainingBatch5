using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todolist.RestApi.DataModels;
using Todolist.RestApi.ViewModels;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Todolist.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {


        private readonly string _connectionString = "Data Source= UCHIASALAI\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=salai;Password=Vpjtqwv23@#;TrustServerCertificate=True;";
        [HttpGet]
            public IActionResult GetTaskCategory()
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = @"
                    SELECT [CategoryId]
                          ,[CategoryName]
                          ,[IsDeleted]
                      FROM [dbo].[TaskCategory] WHERE IsDeleted = 0";

                    List<TaskViewModel> lst = db.Query<TaskViewModel>(query).ToList();
                    return Ok(lst);
                }

            }

            [HttpGet("{id}")]
            public IActionResult GetTaskCategoryById(int id)
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = @"
                    SELECT [CategoryId]
                          ,[CategoryName]
                          ,[IsDeleted]
                      FROM [dbo].[TaskCategory] WHERE IsDeleted = 0";

                    var item = db.Query(query, new TaskViewModel
                    {
                        CategoryId = id
                    }).FirstOrDefault();

                    if (item is null)
                    {
                        return NotFound();
                    };

                    return Ok(item);
                }


            }

            [HttpPost]
            public IActionResult CreateTaskCategory(TaskDataModel Task)
            {
                string query = $@"
                INSERT INTO [dbo].[TaskCategory]
                           ([CategoryName]
                           ,[IsDeleted])
                     VALUES
                           (@CategoryName
                           , 0)";
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    int result = db.Execute(query, new TaskDataModel
                    {
                        CategoryId = Task.CategoryId,
                        CategoryName = Task.CategoryName
                    });

                    return Ok(result == 1 ? "TaskCategory Created" : "TaskCategory Creating Failed");
                }


            }

            [HttpPut("{id}")]
            public IActionResult UpdateTaskCategory(int id, TaskViewModel Task)
            {
                string query = $@"
                UPDATE [dbo].[TaskCategory]
                   SET [CategoryName] = @CategoryName
                      ,[IsDeleted] = 0
                 WHERE CategoryId = @Id ";
                using (IDbConnection db = new SqlConnection(_connectionString))

                {
                    int result = db.Execute(query, new TaskViewModel
                    {
                        CategoryId = Task.CategoryId,
                        CategoryName = Task.CategoryName
                    });
                    return Ok(result == 1 ? "Updating Successful" : "Updating Failed");
                }


            }

            [HttpPatch("{id}")]
            public IActionResult PatchTaskCategory(int id, TaskViewModel Task)
            {

                string conditions = "";
                if (!string.IsNullOrEmpty(Task.CategoryName))
                {
                    conditions += " [CategoryName] = @CategoryName ";
                }

                if (conditions.Length == 0)
                {
                    BadRequest("Invalid Parameter");
                }

                conditions = conditions.Substring(0, conditions.Length - 2);

                using (IDbConnection db = new SqlConnection(_connectionString))

                {
                    string query = $@"
                UPDATE [dbo].[TaskCategory]
                   SET{conditions}
                      ,[IsDeleted] = 0
                 WHERE CategoryId = @Id ";
                    int result = db.Execute(query, new TaskViewModel
                    {
                        CategoryId = Task.CategoryId,
                        CategoryName = Task.CategoryName
                    });
                    return Ok(result == 1 ? "Updating Successful" : "Updating Failed");
                }

            }


            [HttpDelete("{id}")]
            public IActionResult DeleteTaskCategory(int id, TaskViewModel Task)
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = @"DELETE FROM [dbo].[TaskCategory] WHERE IsDeleted = 1";
                    int result = db.Execute(query, new TaskViewModel { CategoryId = Task.CategoryId });
                    return Ok(result == 0 ? "Deleting TaskCategory Failed" : "TaskCategory Successfully Deleted");
                }

            }
        }
    }


