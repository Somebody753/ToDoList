using Microsoft.AspNetCore.Identity;

namespace ToDoList.Models
{
    public class GroupUser
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string ChatGroupId { get; set; }
        public ChatGroup ChatGroup { get; set; }

        public GroupUser()
        { }
    }
}
