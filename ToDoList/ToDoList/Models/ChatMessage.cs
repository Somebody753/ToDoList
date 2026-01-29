using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Models
{
    public class ChatMessage
    {
        public int Id { get; set; } //auto increasing ID
        public string MessageText { get; set; }
        public DateTime Timestamp { get; set; }


        //Author
        public string AuthorId { get; set; } 

        public ApplicationUser Author { get; set; }

        //Group

        public string GroupId { get; set; }

        public ChatGroup Group { get; set; }


        public ChatMessage()
        {

        }
    }
}
