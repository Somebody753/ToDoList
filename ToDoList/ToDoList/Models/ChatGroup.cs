namespace ToDoList.Models
{
    public class ChatGroup
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();  //auto-generate
        public string GroupName { get; set; }

        public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

        public ChatGroup()
        {

        }

    }
}