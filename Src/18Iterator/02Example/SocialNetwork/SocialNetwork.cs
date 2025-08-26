namespace _18Iterator._02Example.SocialNetwork
{
    // 用户类
    public class User
    {
        public string Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string City { get; }
        public List<string> FriendIds { get; }

        public User(string id, string name, string email, string city)
        {
            Id = id;
            Name = name;
            Email = email;
            City = city;
            FriendIds = new List<string>();
        }

        public void AddFriend(string friendId)
        {
            if (!FriendIds.Contains(friendId))
            {
                FriendIds.Add(friendId);
            }
        }
    }

    // 迭代器接口
    public interface IProfileIterator
    {
        bool HasNext();
        User? GetNext();
        void Reset();
    }

    // 集合接口
    public interface ISocialNetwork
    {
        IProfileIterator CreateFriendsIterator(string userId);
        IProfileIterator CreateCoworkersIterator(string companyName);
        IProfileIterator CreateLocationIterator(string city);
        User? GetUser(string userId);
    }

    // 具体迭代器 - 好友迭代器
    public class FriendsIterator : IProfileIterator
    {
        private ISocialNetwork _network;
        private string _userId;
        private List<string> _friendIds = new List<string>();
        private int _currentPosition;

        public FriendsIterator(ISocialNetwork network, string userId)
        {
            _network = network;
            _userId = userId;
            _currentPosition = 0;
            LoadFriends();
        }

        private void LoadFriends()
        {
            var user = _network.GetUser(_userId);
            _friendIds = user != null ? new List<string>(user.FriendIds) : new List<string>();
            Console.WriteLine($"  加载 {user?.Name} 的好友列表 ({_friendIds.Count}人)");
        }

        public bool HasNext()
        {
            return _currentPosition < _friendIds.Count;
        }

        public User? GetNext()
        {
            if (!HasNext())
            {
                return null;
            }

            var friendId = _friendIds[_currentPosition];
            _currentPosition++;
            return _network.GetUser(friendId);
        }

        public void Reset()
        {
            _currentPosition = 0;
        }
    }

    // 具体迭代器 - 同城用户迭代器
    public class LocationIterator : IProfileIterator
    {
        private List<User> _users;
        private int _currentPosition;
        private string _city;

        public LocationIterator(List<User> allUsers, string city)
        {
            _city = city;
            _users = allUsers.Where(u => u.City == city).ToList();
            _currentPosition = 0;
            Console.WriteLine($"  加载 {city} 的用户 ({_users.Count}人)");
        }

        public bool HasNext()
        {
            return _currentPosition < _users.Count;
        }

        public User? GetNext()
        {
            if (!HasNext())
            {
                return null;
            }

            var user = _users[_currentPosition];
            _currentPosition++;
            return user;
        }

        public void Reset()
        {
            _currentPosition = 0;
        }
    }

    // 具体迭代器 - 同事迭代器
    public class CoworkersIterator : IProfileIterator
    {
        private List<User> _coworkers;
        private int _currentPosition;

        public CoworkersIterator(List<User> allUsers, string companyDomain)
        {
            // 根据邮箱域名筛选同事
            _coworkers = allUsers.Where(u => u.Email.EndsWith(companyDomain)).ToList();
            _currentPosition = 0;
            Console.WriteLine($"  加载 {companyDomain} 的同事 ({_coworkers.Count}人)");
        }

        public bool HasNext()
        {
            return _currentPosition < _coworkers.Count;
        }

        public User? GetNext()
        {
            if (!HasNext())
            {
                return null;
            }

            var user = _coworkers[_currentPosition];
            _currentPosition++;
            return user;
        }

        public void Reset()
        {
            _currentPosition = 0;
        }
    }

    // 具体集合 - 社交网络
    public class Facebook : ISocialNetwork
    {
        private Dictionary<string, User> _users;

        public Facebook()
        {
            _users = new Dictionary<string, User>();
            InitializeTestData();
        }

        private void InitializeTestData()
        {
            // 创建测试用户
            var user1 = new User("1", "张三", "zhangsan@company.com", "北京");
            var user2 = new User("2", "李四", "lisi@company.com", "上海");
            var user3 = new User("3", "王五", "wangwu@other.com", "北京");
            var user4 = new User("4", "赵六", "zhaoliu@company.com", "北京");
            var user5 = new User("5", "钱七", "qianqi@other.com", "上海");

            // 建立好友关系
            user1.AddFriend("2");
            user1.AddFriend("3");
            user1.AddFriend("4");
            
            user2.AddFriend("1");
            user2.AddFriend("5");
            
            user3.AddFriend("1");
            user3.AddFriend("4");
            
            user4.AddFriend("1");
            user4.AddFriend("3");
            
            user5.AddFriend("2");

            // 添加到网络
            _users["1"] = user1;
            _users["2"] = user2;
            _users["3"] = user3;
            _users["4"] = user4;
            _users["5"] = user5;
        }

        public User? GetUser(string userId)
        {
            return _users.ContainsKey(userId) ? _users[userId] : null;
        }

        public IProfileIterator CreateFriendsIterator(string userId)
        {
            return new FriendsIterator(this, userId);
        }

        public IProfileIterator CreateCoworkersIterator(string companyName)
        {
            return new CoworkersIterator(_users.Values.ToList(), companyName);
        }

        public IProfileIterator CreateLocationIterator(string city)
        {
            return new LocationIterator(_users.Values.ToList(), city);
        }
    }

    // 消息发送器 - 使用迭代器
    public class SocialSpammer
    {
        public void SendSpam(IProfileIterator iterator, string message)
        {
            Console.WriteLine($"\n📧 群发消息: {message}");
            Console.WriteLine("发送给:");
            
            int count = 0;
            while (iterator.HasNext())
            {
                var user = iterator.GetNext();
                if (user != null)
                {
                    count++;
                    Console.WriteLine($"  {count}. {user.Name} ({user.Email})");
                    Thread.Sleep(100); // 模拟发送延迟
                }
            }
            
            Console.WriteLine($"✅ 消息已发送给 {count} 个用户");
        }
    }

    // 自定义树形结构迭代器示例
    public class TreeNode<T>
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Children { get; set; }

        public TreeNode(T value)
        {
            Value = value;
            Children = new List<TreeNode<T>>();
        }

        public void AddChild(TreeNode<T> child)
        {
            Children.Add(child);
        }
    }

    // 树形迭代器接口
    public interface ITreeIterator<T>
    {
        bool HasNext();
        T? GetNext();
        void Reset();
    }

    // 深度优先迭代器
    public class DepthFirstIterator<T> : ITreeIterator<T>
    {
        private Stack<TreeNode<T>> _stack = new Stack<TreeNode<T>>();
        private TreeNode<T> _root;

        public DepthFirstIterator(TreeNode<T> root)
        {
            _root = root;
            Reset();
        }

        public bool HasNext()
        {
            return _stack.Count > 0;
        }

        public T? GetNext()
        {
            if (!HasNext())
            {
                return default(T);
            }

            var node = _stack.Pop();
            
            // 将子节点逆序压栈（保证从左到右遍历）
            for (int i = node.Children.Count - 1; i >= 0; i--)
            {
                _stack.Push(node.Children[i]);
            }

            return node.Value;
        }

        public void Reset()
        {
            _stack = new Stack<TreeNode<T>>();
            if (_root != null)
            {
                _stack.Push(_root);
            }
        }
    }

    // 广度优先迭代器
    public class BreadthFirstIterator<T> : ITreeIterator<T>
    {
        private Queue<TreeNode<T>> _queue = new Queue<TreeNode<T>>();
        private TreeNode<T> _root;

        public BreadthFirstIterator(TreeNode<T> root)
        {
            _root = root;
            Reset();
        }

        public bool HasNext()
        {
            return _queue.Count > 0;
        }

        public T? GetNext()
        {
            if (!HasNext())
            {
                return default(T);
            }

            var node = _queue.Dequeue();
            
            // 将子节点加入队列
            foreach (var child in node.Children)
            {
                _queue.Enqueue(child);
            }

            return node.Value;
        }

        public void Reset()
        {
            _queue = new Queue<TreeNode<T>>();
            if (_root != null)
            {
                _queue.Enqueue(_root);
            }
        }
    }
}
