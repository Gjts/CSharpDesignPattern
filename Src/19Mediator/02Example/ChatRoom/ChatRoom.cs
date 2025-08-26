namespace _19Mediator._02Example.ChatRoom
{
    // 中介者接口
    public interface IChatRoom
    {
        void RegisterUser(User user);
        void SendMessage(string message, User sender);
        void SendPrivateMessage(string message, User sender, string receiverId);
        void SendGroupMessage(string message, User sender, string groupName);
        void CreateGroup(string groupName, User creator);
        void JoinGroup(string groupName, User user);
    }

    // 抽象用户类
    public abstract class User
    {
        protected IChatRoom? _chatRoom;
        public string Id { get; }
        public string Name { get; }
        protected List<string> _messageHistory;

        public User(string id, string name)
        {
            Id = id;
            Name = name;
            _messageHistory = new List<string>();
        }

        public void SetChatRoom(IChatRoom chatRoom)
        {
            _chatRoom = chatRoom;
            _chatRoom.RegisterUser(this);
        }

        public abstract void Send(string message);
        public abstract void SendTo(string message, string userId);
        public abstract void SendToGroup(string message, string groupName);
        public abstract void Receive(string message, User sender);

        public void ShowHistory()
        {
            Console.WriteLine($"\n📜 {Name} 的消息历史:");
            if (_messageHistory.Count == 0)
            {
                Console.WriteLine("  (无消息)");
            }
            else
            {
                foreach (var msg in _messageHistory)
                {
                    Console.WriteLine($"  {msg}");
                }
            }
        }
    }

    // 具体用户 - 普通用户
    public class RegularUser : User
    {
        public RegularUser(string id, string name) : base(id, name) { }

        public override void Send(string message)
        {
            Console.WriteLine($"\n💬 {Name} 发送广播消息: {message}");
            _chatRoom?.SendMessage(message, this);
        }

        public override void SendTo(string message, string userId)
        {
            Console.WriteLine($"\n🔒 {Name} 发送私信给 {userId}: {message}");
            _chatRoom?.SendPrivateMessage(message, this, userId);
        }

        public override void SendToGroup(string message, string groupName)
        {
            Console.WriteLine($"\n👥 {Name} 发送群组消息到 [{groupName}]: {message}");
            _chatRoom?.SendGroupMessage(message, this, groupName);
        }

        public override void Receive(string message, User sender)
        {
            string formattedMessage = $"[{DateTime.Now:HH:mm}] {sender.Name}: {message}";
            Console.WriteLine($"  📨 {Name} 收到: {formattedMessage}");
            _messageHistory.Add(formattedMessage);
        }
    }

    // 具体用户 - VIP用户
    public class VIPUser : User
    {
        private bool _doNotDisturb;

        public VIPUser(string id, string name) : base(id, name)
        {
            _doNotDisturb = false;
        }

        public void SetDoNotDisturb(bool enabled)
        {
            _doNotDisturb = enabled;
            Console.WriteLine($"  🔕 {Name} 免打扰模式: {(enabled ? "开启" : "关闭")}");
        }

        public override void Send(string message)
        {
            Console.WriteLine($"\n⭐ VIP {Name} 发送广播消息: {message}");
            _chatRoom?.SendMessage($"[VIP] {message}", this);
        }

        public override void SendTo(string message, string userId)
        {
            Console.WriteLine($"\n⭐🔒 VIP {Name} 发送私信给 {userId}: {message}");
            _chatRoom?.SendPrivateMessage($"[VIP] {message}", this, userId);
        }

        public override void SendToGroup(string message, string groupName)
        {
            Console.WriteLine($"\n⭐👥 VIP {Name} 发送群组消息到 [{groupName}]: {message}");
            _chatRoom?.SendGroupMessage($"[VIP] {message}", this, groupName);
        }

        public override void Receive(string message, User sender)
        {
            if (_doNotDisturb && sender.GetType() != typeof(VIPUser))
            {
                // VIP用户开启免打扰，只接收其他VIP的消息
                return;
            }

            string formattedMessage = $"[{DateTime.Now:HH:mm}] {sender.Name}: {message}";
            Console.WriteLine($"  ⭐📨 VIP {Name} 收到: {formattedMessage}");
            _messageHistory.Add(formattedMessage);
        }

        public void CreateGroup(string groupName)
        {
            Console.WriteLine($"\n⭐ VIP {Name} 创建群组: {groupName}");
            _chatRoom?.CreateGroup(groupName, this);
        }
    }

    // 具体中介者 - 聊天室
    public class ChatRoom : IChatRoom
    {
        private Dictionary<string, User> _users;
        private Dictionary<string, List<User>> _groups;
        private List<string> _bannedWords;

        public ChatRoom()
        {
            _users = new Dictionary<string, User>();
            _groups = new Dictionary<string, List<User>>();
            _bannedWords = new List<string> { "spam", "广告", "违禁" };
        }

        public void RegisterUser(User user)
        {
            if (!_users.ContainsKey(user.Id))
            {
                _users[user.Id] = user;
                Console.WriteLine($"  ✅ {user.Name} 加入聊天室");
                
                // 发送欢迎消息
                SendSystemMessage($"欢迎 {user.Name} 加入聊天室！", user);
            }
        }

        public void SendMessage(string message, User sender)
        {
            // 检查违禁词
            if (ContainsBannedWords(message))
            {
                SendSystemMessage("消息包含违禁词，已被屏蔽", sender);
                return;
            }

            // 发送给所有其他用户
            foreach (var user in _users.Values)
            {
                if (user.Id != sender.Id)
                {
                    user.Receive(message, sender);
                }
            }
        }

        public void SendPrivateMessage(string message, User sender, string receiverId)
        {
            if (ContainsBannedWords(message))
            {
                SendSystemMessage("消息包含违禁词，已被屏蔽", sender);
                return;
            }

            if (_users.ContainsKey(receiverId))
            {
                _users[receiverId].Receive($"[私信] {message}", sender);
            }
            else
            {
                SendSystemMessage($"用户 {receiverId} 不存在", sender);
            }
        }

        public void SendGroupMessage(string message, User sender, string groupName)
        {
            if (ContainsBannedWords(message))
            {
                SendSystemMessage("消息包含违禁词，已被屏蔽", sender);
                return;
            }

            if (_groups.ContainsKey(groupName))
            {
                var group = _groups[groupName];
                if (group.Contains(sender))
                {
                    foreach (var user in group)
                    {
                        if (user.Id != sender.Id)
                        {
                            user.Receive($"[{groupName}] {message}", sender);
                        }
                    }
                }
                else
                {
                    SendSystemMessage($"您不是群组 {groupName} 的成员", sender);
                }
            }
            else
            {
                SendSystemMessage($"群组 {groupName} 不存在", sender);
            }
        }

        public void CreateGroup(string groupName, User creator)
        {
            if (!_groups.ContainsKey(groupName))
            {
                _groups[groupName] = new List<User> { creator };
                Console.WriteLine($"  ✅ 群组 {groupName} 创建成功");
                SendSystemMessage($"您已创建群组 {groupName}", creator);
            }
            else
            {
                SendSystemMessage($"群组 {groupName} 已存在", creator);
            }
        }

        public void JoinGroup(string groupName, User user)
        {
            if (_groups.ContainsKey(groupName))
            {
                if (!_groups[groupName].Contains(user))
                {
                    _groups[groupName].Add(user);
                    Console.WriteLine($"  ✅ {user.Name} 加入群组 {groupName}");
                    
                    // 通知群组成员
                    foreach (var member in _groups[groupName])
                    {
                        if (member.Id != user.Id)
                        {
                            member.Receive($"{user.Name} 加入了群组", new SystemUser());
                        }
                    }
                }
            }
            else
            {
                SendSystemMessage($"群组 {groupName} 不存在", user);
            }
        }

        private bool ContainsBannedWords(string message)
        {
            foreach (var word in _bannedWords)
            {
                if (message.Contains(word))
                {
                    return true;
                }
            }
            return false;
        }

        private void SendSystemMessage(string message, User receiver)
        {
            receiver.Receive(message, new SystemUser());
        }

        // 内部系统用户
        private class SystemUser : User
        {
            public SystemUser() : base("SYSTEM", "系统") { }
            public override void Send(string message) { }
            public override void SendTo(string message, string userId) { }
            public override void SendToGroup(string message, string groupName) { }
            public override void Receive(string message, User sender) { }
        }
    }
}
