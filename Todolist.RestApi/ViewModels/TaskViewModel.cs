namespace Todolist.RestApi.ViewModels
{
    public class TaskViewModel
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
