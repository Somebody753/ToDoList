namespace ToDoList.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDetails { get; set; }
        public bool TaskDone { get; set; }
        public string? UserId { get; set; }
        public string DeadlineDate { get; set; }



        public ToDoTask()
        {
                
        }
    }
}
