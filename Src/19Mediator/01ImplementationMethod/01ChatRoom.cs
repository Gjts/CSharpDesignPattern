namespace _19Mediator
{
    // 中介者接口
    public interface IMediator
    {
        void SendMessage(string message, IColleague colleague);
    }

    // 同事接口
    public interface IColleague
    {
        void SetMediator(IMediator mediator);
        void Send(string message);
        void Receive(string message);
    }

    // 聊天室（具体中介者）
    public class ChatRoom : IMediator
    {
        private List<ChatUser> _users = new List<ChatUser>();

        public void RegisterUser(ChatUser user)
        {
            _users.Add(user);
            user.SetMediator(this);
            Console.WriteLine($"[系统] {user.Name} 加入聊天室");
        }

        public void SendMessage(string message, IColleague colleague)
        {
            var sender = colleague as ChatUser;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {sender?.Name}: {message}");
            
            foreach (var user in _users)
            {
                if (user != colleague)
                {
                    user.Receive(message);
                }
            }
        }

        public void SendPrivateMessage(string message, ChatUser from, ChatUser to)
        {
            Console.WriteLine($"[私聊] {from.Name} -> {to.Name}: {message}");
            to.Receive($"[私聊来自 {from.Name}]: {message}");
        }
    }

    // 聊天用户（具体同事）
    public class ChatUser : IColleague
    {
        private IMediator? _mediator;
        public string Name { get; private set; }

        public ChatUser(string name)
        {
            Name = name;
        }

        public void SetMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Send(string message)
        {
            Console.WriteLine($"{Name} 发送: {message}");
            _mediator?.SendMessage(message, this);
        }

        public void Receive(string message)
        {
            Console.WriteLine($"  {Name} 收到: {message}");
        }
    }

    // 事件总线（另一种中介者实现）
    public class EventBus
    {
        private Dictionary<string, List<Action<object>>> _handlers = new();

        public void Subscribe(string eventType, Action<object> handler)
        {
            if (!_handlers.ContainsKey(eventType))
            {
                _handlers[eventType] = new List<Action<object>>();
            }
            _handlers[eventType].Add(handler);
        }

        public void Publish(string eventType, object data)
        {
            Console.WriteLine($"[事件总线] 发布事件: {eventType}");
            if (_handlers.ContainsKey(eventType))
            {
                foreach (var handler in _handlers[eventType])
                {
                    handler(data);
                }
            }
        }
    }

    // 组件基类
    public abstract class Component
    {
        protected EventBus? _eventBus;
        protected string _name;

        protected Component(string name)
        {
            _name = name;
        }

        public void SetEventBus(EventBus eventBus)
        {
            _eventBus = eventBus;
            SubscribeToEvents();
        }

        protected abstract void SubscribeToEvents();
    }

    // UI组件
    public class UIComponent : Component
    {
        public UIComponent(string name) : base(name) { }

        protected override void SubscribeToEvents()
        {
            _eventBus?.Subscribe("DataChanged", OnDataChanged);
            _eventBus?.Subscribe("UserLogin", OnUserLogin);
        }

        private void OnDataChanged(object data)
        {
            Console.WriteLine($"  [{_name}] UI更新: {data}");
        }

        private void OnUserLogin(object data)
        {
            Console.WriteLine($"  [{_name}] 显示用户信息: {data}");
        }

        public void ButtonClick()
        {
            Console.WriteLine($"[{_name}] 按钮被点击");
            _eventBus?.Publish("ButtonClicked", $"{_name}的按钮");
        }
    }

    // 数据组件
    public class DataComponent : Component
    {
        public DataComponent(string name) : base(name) { }

        protected override void SubscribeToEvents()
        {
            _eventBus?.Subscribe("ButtonClicked", OnButtonClicked);
            _eventBus?.Subscribe("SaveData", OnSaveData);
        }

        private void OnButtonClicked(object data)
        {
            Console.WriteLine($"  [{_name}] 响应按钮点击: {data}");
            UpdateData("新数据");
        }

        private void OnSaveData(object data)
        {
            Console.WriteLine($"  [{_name}] 保存数据: {data}");
        }

        public void UpdateData(string newData)
        {
            Console.WriteLine($"[{_name}] 数据更新为: {newData}");
            _eventBus?.Publish("DataChanged", newData);
        }
    }

    // 日志组件
    public class LogComponent : Component
    {
        public LogComponent(string name) : base(name) { }

        protected override void SubscribeToEvents()
        {
            // 订阅所有事件进行日志记录
            _eventBus?.Subscribe("DataChanged", LogEvent);
            _eventBus?.Subscribe("UserLogin", LogEvent);
            _eventBus?.Subscribe("ButtonClicked", LogEvent);
            _eventBus?.Subscribe("SaveData", LogEvent);
        }

        private void LogEvent(object data)
        {
            Console.WriteLine($"  [{_name}] 记录日志: {data}");
        }
    }
}