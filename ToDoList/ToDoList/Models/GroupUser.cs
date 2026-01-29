using Microsoft.AspNetCore.Identity;

namespace ToDoList.Models
{

    /*
     
    Cross table between group and user
     
     */
    public class GroupUser
    {
        //User
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        //Group
        public string ChatGroupId { get; set; }
        public ChatGroup ChatGroup { get; set; }

        public GroupUser()
        { }
    }
}
