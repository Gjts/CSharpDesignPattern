namespace _18Iterator
{
    // 迭代器接口
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
        void Reset();
    }

    // 可迭代集合接口
    public interface IIterable<T>
    {
        IIterator<T> CreateIterator();
    }

    // 图书类
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public override string ToString()
        {
            return $"《{Title}》 - {Author} ({Year})";
        }
    }

    // 图书馆类（聚合对象）
    public class Library : IIterable<Book>
    {
        private List<Book> _books = new List<Book>();

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            _books.Remove(book);
        }

        public IIterator<Book> CreateIterator()
        {
            return new LibraryIterator(_books);
        }

        public IIterator<Book> CreateReverseIterator()
        {
            return new ReverseLibraryIterator(_books);
        }

        public IIterator<Book> CreateFilteredIterator(Func<Book, bool> filter)
        {
            return new FilteredLibraryIterator(_books, filter);
        }
    }

    // 正向迭代器
    public class LibraryIterator : IIterator<Book>
    {
        private List<Book> _books;
        private int _position = 0;

        public LibraryIterator(List<Book> books)
        {
            _books = books;
        }

        public bool HasNext()
        {
            return _position < _books.Count;
        }

        public Book Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("没有更多元素");
            
            return _books[_position++];
        }

        public void Reset()
        {
            _position = 0;
        }
    }

    // 反向迭代器
    public class ReverseLibraryIterator : IIterator<Book>
    {
        private List<Book> _books;
        private int _position;

        public ReverseLibraryIterator(List<Book> books)
        {
            _books = books;
            _position = _books.Count - 1;
        }

        public bool HasNext()
        {
            return _position >= 0;
        }

        public Book Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("没有更多元素");
            
            return _books[_position--];
        }

        public void Reset()
        {
            _position = _books.Count - 1;
        }
    }

    // 过滤迭代器
    public class FilteredLibraryIterator : IIterator<Book>
    {
        private List<Book> _books;
        private Func<Book, bool> _filter;
        private int _position = 0;
        private Book? _nextBook;

        public FilteredLibraryIterator(List<Book> books, Func<Book, bool> filter)
        {
            _books = books;
            _filter = filter;
            FindNext();
        }

        private void FindNext()
        {
            _nextBook = null;
            while (_position < _books.Count)
            {
                if (_filter(_books[_position]))
                {
                    _nextBook = _books[_position];
                    break;
                }
                _position++;
            }
        }

        public bool HasNext()
        {
            return _nextBook != null;
        }

        public Book Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("没有更多元素");
            
            var book = _nextBook!;
            _position++;
            FindNext();
            return book;
        }

        public void Reset()
        {
            _position = 0;
            FindNext();
        }
    }

    // 文件节点
    public class FileNode
    {
        public string Name { get; set; }
        public bool IsDirectory { get; set; }
        public List<FileNode> Children { get; set; }
        public int Level { get; set; }

        public FileNode(string name, bool isDirectory)
        {
            Name = name;
            IsDirectory = isDirectory;
            Children = new List<FileNode>();
            Level = 0;
        }

        public void Add(FileNode node)
        {
            if (!IsDirectory)
                throw new InvalidOperationException("不能向文件添加子节点");
            
            node.Level = Level + 1;
            Children.Add(node);
        }
    }

    // 深度优先迭代器
    public class DepthFirstIterator : IIterator<FileNode>
    {
        private Stack<FileNode> _stack;

        public DepthFirstIterator(FileNode root)
        {
            _stack = new Stack<FileNode>();
            _stack.Push(root);
        }

        public bool HasNext()
        {
            return _stack.Count > 0;
        }

        public FileNode Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("没有更多元素");
            
            var node = _stack.Pop();
            
            // 将子节点逆序压入栈中（确保正确的遍历顺序）
            for (int i = node.Children.Count - 1; i >= 0; i--)
            {
                _stack.Push(node.Children[i]);
            }
            
            return node;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    // 广度优先迭代器
    public class BreadthFirstIterator : IIterator<FileNode>
    {
        private Queue<FileNode> _queue;

        public BreadthFirstIterator(FileNode root)
        {
            _queue = new Queue<FileNode>();
            _queue.Enqueue(root);
        }

        public bool HasNext()
        {
            return _queue.Count > 0;
        }

        public FileNode Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("没有更多元素");
            
            var node = _queue.Dequeue();
            
            // 将子节点加入队列
            foreach (var child in node.Children)
            {
                _queue.Enqueue(child);
            }
            
            return node;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    // 用户类
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        private List<User> _friends = new List<User>();

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public void AddFriend(User friend)
        {
            if (!_friends.Contains(friend))
            {
                _friends.Add(friend);
                friend._friends.Add(this); // 双向好友关系
            }
        }

        public List<User> GetFriends()
        {
            return new List<User>(_friends);
        }

        public int GetFriendsCount()
        {
            return _friends.Count;
        }
    }

    // 社交网络
    public class SocialNetwork
    {
        private Dictionary<string, User> _users = new Dictionary<string, User>();

        public void AddUser(User user)
        {
            _users[user.Name] = user;
        }

        public IIterator<User> GetFriendsIterator(string userName)
        {
            if (_users.ContainsKey(userName))
            {
                return new FriendsIterator(_users[userName]);
            }
            return new EmptyIterator<User>();
        }

        public IIterator<User> GetSecondDegreeFriendsIterator(string userName)
        {
            if (_users.ContainsKey(userName))
            {
                return new SecondDegreeFriendsIterator(_users[userName]);
            }
            return new EmptyIterator<User>();
        }

        public IIterator<User> GetAllUsersIterator()
        {
            return new AllUsersIterator(_users.Values.ToList());
        }
    }

    // 好友迭代器
    public class FriendsIterator : IIterator<User>
    {
        private List<User> _friends;
        private int _position = 0;

        public FriendsIterator(User user)
        {
            _friends = user.GetFriends();
        }

        public bool HasNext()
        {
            return _position < _friends.Count;
        }

        public User Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("没有更多元素");
            
            return _friends[_position++];
        }

        public void Reset()
        {
            _position = 0;
        }
    }

    // 二度好友迭代器
    public class SecondDegreeFriendsIterator : IIterator<User>
    {
        private HashSet<User> _secondDegreeFriends;
        private List<User> _friendsList;
        private int _position = 0;

        public SecondDegreeFriendsIterator(User user)
        {
            _secondDegreeFriends = new HashSet<User>();
            var directFriends = user.GetFriends();
            
            foreach (var friend in directFriends)
            {
                foreach (var friendOfFriend in friend.GetFriends())
                {
                    // 排除自己和直接好友
                    if (friendOfFriend != user && !directFriends.Contains(friendOfFriend))
                    {
                        _secondDegreeFriends.Add(friendOfFriend);
                    }
                }
            }
            
            _friendsList = _secondDegreeFriends.ToList();
        }

        public bool HasNext()
        {
            return _position < _friendsList.Count;
        }

        public User Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("没有更多元素");
            
            return _friendsList[_position++];
        }

        public void Reset()
        {
            _position = 0;
        }
    }

    // 所有用户迭代器
    public class AllUsersIterator : IIterator<User>
    {
        private List<User> _users;
        private int _position = 0;

        public AllUsersIterator(List<User> users)
        {
            _users = users;
        }

        public bool HasNext()
        {
            return _position < _users.Count;
        }

        public User Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("没有更多元素");
            
            return _users[_position++];
        }

        public void Reset()
        {
            _position = 0;
        }
    }

    // 空迭代器
    public class EmptyIterator<T> : IIterator<T>
    {
        public bool HasNext()
        {
            return false;
        }

        public T Next()
        {
            throw new InvalidOperationException("空迭代器没有元素");
        }

        public void Reset()
        {
        }
    }
}