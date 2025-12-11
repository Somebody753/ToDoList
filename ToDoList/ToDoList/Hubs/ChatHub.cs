using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Services;


namespace ToDoList.Hubs
{
    
    public class ChatHub: Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly UserConnectionManager _users;

        public ChatHub(ApplicationDbContext context, UserConnectionManager users)
        {
            _context = context;
            _users = users;
        }



        [Authorize]
        public async Task SendMessage(string message, string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);


            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = Context.User?.Identity?.Name;

            var chatMessage = new ChatMessage
            {
                AuthorId = userId,
                GroupId = groupId,
                MessageText = message,
                Timestamp = DateTime.UtcNow,
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.Group(groupId).SendAsync("ReceiveMessage", userName, message, DateTime.Now.ToString("HH:mm"));


        }





        public async Task JoinGroup(string groupId)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = Context.User?.Identity?.Name;

            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);

            _users.AddUserToGroup(groupId, userId);

            // notify group to refresh online list
            await Clients.Group(groupId)
                .SendAsync("OnlineUsersUpdated", groupId);
        }

        // user disconnects automatically
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // You must track which group each connection joined
            // simplest version: skip cleanup





            await base.OnDisconnectedAsync(exception);
        }

















    }
}
