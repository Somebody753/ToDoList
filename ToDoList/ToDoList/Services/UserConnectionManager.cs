using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ToDoList.Services
{
    public class UserConnectionManager
    {
        private static readonly object _lock = new();
        private static readonly ConcurrentDictionary<string, HashSet<string>> _groupUsers =
            new ConcurrentDictionary<string, HashSet<string>>();



        public void AddUserToGroup(string groupId, string userId)
        {
            lock (_lock)
            {
                if (!_groupUsers.ContainsKey(groupId))
                    _groupUsers[groupId] = new HashSet<string>();

                _groupUsers[groupId].Add(userId);
            }
        }

        public void RemoveUserFromGroup(string groupId, string userId)
        {
            lock (_lock)
            {
                if (_groupUsers.ContainsKey(groupId))
                {
                    _groupUsers[groupId].Remove(userId);
                }
            }
        }

        public IReadOnlyCollection<string> GetOnlineUsers(string groupId)
        {
            if (_groupUsers.TryGetValue(groupId, out var users))
                return users.ToList();

            return new List<string>();
        }









    }
}
