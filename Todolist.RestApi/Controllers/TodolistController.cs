using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Todolist.RestApi.DataModels;
using Todolist.RestApi.ViewModels;
using Dapper;
using System.Reflection.Metadata;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;


namespace Todolist.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodolistController : ControllerBase
    {
        private readonly string _connectionString = "Data Source= UCHIASALAI\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=salai;Password=Vpjtqwv23@#;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult GetToDoList()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT TaskId as Id
                          ,TaskTitle as Title
                          ,TaskDescription as Description
                          ,[CategoryId]
                          ,[PriorityLevel]
                          ,[Status]
                          ,[DueDate]
                          ,[CreatedDate]
                          ,[CompletedDate]
                          ,[ForeignKey]
                          ,[TaskCategory]
                          ,[IsDeleted]
                      FROM [dbo].[ToDoList]";
                List<TodoListViewModel> lst = db.Query<TodoListViewModel>(query).ToList();
                return Ok(lst);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetToDoListById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                string query = @"
                    SELECT TaskId as Id
                          ,TaskTitle as Title
                          ,TaskDescription as Description
                          ,[CategoryId]
                          ,[PriorityLevel]
                          ,[Status]
                          ,[DueDate]
                          ,[CreatedDate]
                          ,[CompletedDate]
                          ,[ForeignKey]
                          ,[TaskCategory]
                          ,[IsDeleted]
                      FROM [dbo].[ToDoList]";
                var item = db.Query<TodoListViewModel>(query, new TodoListViewModel
                {
                    Id = id
                }).FirstOrDefault();

                if (item is null)
                {
                    return NotFound();
                }
                return Ok(item);
            }
        }

        [HttpPost]
        public IActionResult CreateToDoList(TodoListDataMdoel Tdl)

        {
            string query = $@"
            INSERT INTO [dbo].[ToDoList]
               ([TaskTitle]
               ,[TaskDescription]
               ,[CategoryId]
               ,[PriorityLevel]
               ,[Status]
               ,[DueDate]
               ,[CreatedDate]
               ,[CompletedDate]
               ,[ForeignKey]
               ,[TaskCategory]  
               ,[IsDeleted])
     VALUES
               (@TaskTitle
               , @TaskDescription
               , @CategoryId
               , @PriorityLevel
               , @Status
               , @DueDate
               , @CreatedDate
               , @CompletedDate
               , @ForeignKey
               , @TaskCategory
               ,0)";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new TodoListDataMdoel
                {
                    TaskId = Tdl.TaskId,
                    TaskTitle = Tdl.TaskTitle,
                    TaskDescription = Tdl.TaskDescription,
                    CategoryId = Tdl.CategoryId,
                    PriorityLevel = Tdl.PriorityLevel,
                    Status = Tdl.Status,
                    date = Tdl.date,
                    CreatedDate = Tdl.CreatedDate,
                    CompletedDate = Tdl.CompletedDate,
                    ForeignKey = Tdl.ForeignKey,
                    TaskCategory = Tdl.TaskCategory
                });
                return Ok(result == 1 ? " TodoList Successfully Created" : "Creating Failed");
            }


        }

        [HttpPut("{id}")]
        public IActionResult UpdateToDoList(int id, TodoListViewModel Tdl)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"
                    UPDATE [dbo].[ToDoList]
                       SET [TaskTitle] = @Title
                          ,[TaskDescription] = @Description
                          ,[CategoryId] = @CategotyId
                          ,[PriorityLevel] = @PriorityLevel
                          ,[Status] = @Status
                          ,[DueDate] = @DueDate
                          ,[CreatedDate] = @CreatedDate
                          ,[CompletedDate] = @CompletedDate
                          ,[ForeignKey] = @ForeignKey
                          ,[TaskCategory] = @TaskCategory
                          ,[IsDeleted] = 0
                     WHERE TaskId =@Id";
                int result = db.Execute(query, new TodoListViewModel
                {
                    Id = Tdl.Id,
                    Title = Tdl.Title,
                    Description = Tdl.Description,
                    CategoryId = Tdl.CategoryId,
                    PriorityLevel = Tdl.PriorityLevel,
                    Status = Tdl.Status,
                    date = Tdl.date,
                    CreatedDate = Tdl.CreatedDate,
                    CompletedDate = Tdl.CompletedDate,
                    ForeignKey = Tdl.ForeignKey,
                    TaskCategory = Tdl.TaskCategory
                });

                return Ok(result == 1 ? "Updating Successful" : "Updating Failed");
            }



        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogToDoList(int id, TodoListViewModel Tdl)

        {
            string conditions = "";
            if (!string.IsNullOrEmpty(Tdl.Title))
            {
                conditions += " [TaskTitle] = @Title, ";
            }
            if (!string.IsNullOrEmpty(Tdl.Description))
            {
                conditions += " [TaskDescription] = @Description ";
            }
            //if (!string.IsNullOrEmpty(Tdl.CategoryId))
            //{
            //    conditions += " [CategotyId] = @CategotyId ";
            //}
            //if (!string.IsNullOrEmpty(Tdl.PriorityLevel))
            //{
            //    conditions += " [PriorityLevel] = @PriorityLevel ";
            //}
            if (!string.IsNullOrEmpty(Tdl.Status))
            {
                conditions += " [Status] = @Status, ";
            }
            if (!string.IsNullOrEmpty(Tdl.date))
            {
                conditions += " [DueDate] = @DueDate, ";
            }
            if (!string.IsNullOrEmpty(Tdl.CreatedDate))
            {
                conditions += " [CreatedDate] = @CreatedDate, ";
            }
            if (!string.IsNullOrEmpty(Tdl.CompletedDate))
            {
                conditions += " [CompletedDate] = @CompletedDate ";
            }

            if (conditions.Length == 0)
            {
                BadRequest("Invalid Parameter");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@" UPDATE [dbo].[ToDoList] SET {conditions} ,[IsDeleted] =  WHERE TaskId =@Id";
                int result = db.Execute(query, new TodoListViewModel
                {
                    Id = Tdl.Id,
                    Title = Tdl.Title,
                    Description = Tdl.Description,
                    CategoryId = Tdl.CategoryId,
                    PriorityLevel = Tdl.PriorityLevel,
                    Status = Tdl.Status,
                    date = Tdl.date,
                    CreatedDate = Tdl.CreatedDate,
                    CompletedDate = Tdl.CompletedDate,
                    ForeignKey = Tdl.ForeignKey,
                    TaskCategory = Tdl.TaskCategory
                });
                return Ok(result == 1 ? "Updating Successful" : "Updating Failed");
            }


        }


        [HttpDelete("{id}")]
        public IActionResult DeleteToDoList(int id, TodoListViewModel Tdl)
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"DELETE FROM [dbo].[ToDoList] WHERE IsDeleted = 1 TaskId=@TaskId";
                int result = db.Execute(query, new TodoListViewModel { Id = id });
                return Ok(result == 1 ? "Deleting Successful" : "Deleting Failed");
            }
        }

    }
}
