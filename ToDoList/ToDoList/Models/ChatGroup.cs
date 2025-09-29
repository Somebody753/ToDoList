namespace ToDoList.Models
{
    public class ChatGroup
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();  //auto-generate
        public string GroupName { get; set; }

        public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();

        public ChatGroup()
        {

        }

    }
}