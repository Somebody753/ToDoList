using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string MessageText { get; set; }
        public DateTime Timestamp { get; set; }

        public string AuthorId { get; set; }
        public IdentityUser Author { get; set; }



        public ChatMessage()
        {

        }
    }
}
