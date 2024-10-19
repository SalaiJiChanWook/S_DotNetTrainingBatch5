namespace Todolist.RestApi.DataModels
{
    public class TaskDataModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }

    }
}
