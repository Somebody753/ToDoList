namespace ToDoList.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string MessageText { get; set; }
        public DateTime Timestamp { get; set; }



        public ChatMessage()
        {

        }
    }
}
