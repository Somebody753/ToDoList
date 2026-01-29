using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class ToDoTask
    {
        public int Id { get; set; } //Id
        public string TaskName { get; set; } //name
        public string TaskDetails { get; set; } //details
        public bool TaskDone { get; set; } //Is task done

        //Id of user who created task
        public string? UserId { get; set; }


        //Date and time the message was created, and the way it would be displayed
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeadlineDate { get; set; }


        //ChatGroup
        public string? ChatGroupId { get; set; }
        public ChatGroup? ChatGroup { get; set; }

        public ToDoTask()
        {
                
        }
    }
}
