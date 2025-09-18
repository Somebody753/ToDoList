using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDetails { get; set; }
        public bool TaskDone { get; set; }
        public string? UserId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeadlineDate { get; set; }



        public ToDoTask()
        {
                
        }
    }
}
