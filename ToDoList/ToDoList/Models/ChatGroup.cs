namespace ToDoList.Models
{
    public class ChatGroup
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();  //auto generating unique ID
        public string GroupName { get; set; } 

        //Each group can have multiple users, messages and tasks
        public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>(); //cross table
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
        public ICollection<ToDoTask> ToDoTasks { get; set; } = new List<ToDoTask>();

        public ChatGroup()
        {

        }

    }
}