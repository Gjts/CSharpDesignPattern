using System.Runtime.CompilerServices;

namespace _21Observer._01ImplementationMethod
{
    // ==================== 观察者模式的多种实现变体 ====================
    // 观察者模式是变体较多的模式之一，有6种以上的实现方式

    #region 1. 经典观察者模式（推模型）
    
    // 观察者接口
    public interface IObserver
    {
        void Update(string message);
    }
    
    // 主题接口
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string message);
    }
    
    // 具体主题
    public class ConcreteSubject : ISubject
    {
        private List<IObserver> observers = new();
        
        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }
        
        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }
        
        public void Notify(string message)
        {
            foreach (var observer in observers)
            {
                observer.Update(message);
            }
        }
    }
    
    #endregion

    #region 2. 拉模型观察者
    
    // 拉模型观察者接口
    public interface IPullObserver
    {
        void Update(IPullSubject subject);
    }
    
    // 拉模型主题接口
    public interface IPullSubject
    {
        void Attach(IPullObserver observer);
        void Detach(IPullObserver observer);
        void Notify();
        object GetState();
    }
    
    // 拉模型具体主题
    public class PullSubject : IPullSubject
    {
        private List<IPullObserver> observers = new();
        private object state;
        
        public void Attach(IPullObserver observer) => observers.Add(observer);
        public void Detach(IPullObserver observer) => observers.Remove(observer);
        public object GetState() => state;
        
        public void SetState(object newState)
        {
            state = newState;
            Notify();
        }
        
        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }
    }
    
    #endregion

    #region 3. 事件驱动观察者（.NET Events）
    
    // 事件参数
    public class StateChangedEventArgs : EventArgs
    {
        public string PropertyName { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
    
    // 事件驱动主题
    public class EventSubject
    {
        // 声明事件
        public event EventHandler<StateChangedEventArgs> StateChanged;
        
        private string _state;
        public string State
        {
            get => _state;
            set
            {
                var oldValue = _state;
                _state = value;
                OnStateChanged(new StateChangedEventArgs
                {
                    PropertyName = nameof(State),
                    OldValue = oldValue,
                    NewValue = value
                });
            }
        }
        
        protected virtual void OnStateChanged(StateChangedEventArgs e)
        {
            StateChanged?.Invoke(this, e);
        }
    }
    
    #endregion

    #region 4. 弱引用观察者（防止内存泄漏）
    
    public class WeakObserverCollection
    {
        private List<WeakReference> observers = new();
        
        public void Add(IObserver observer)
        {
            // 清理已经被回收的弱引用
            CleanUp();
            observers.Add(new WeakReference(observer));
        }
        
        public void Remove(IObserver observer)
        {
            observers.RemoveAll(wr => !wr.IsAlive || wr.Target == observer);
        }
        
        public void NotifyAll(string message)
        {
            foreach (var weakRef in observers.ToList())
            {
                if (weakRef.IsAlive)
                {
                    (weakRef.Target as IObserver)?.Update(message);
                }
            }
            CleanUp();
        }
        
        private void CleanUp()
        {
            observers.RemoveAll(wr => !wr.IsAlive);
        }
    }
    
    public class WeakSubject
    {
        private WeakObserverCollection observers = new();
        
        public void Attach(IObserver observer) => observers.Add(observer);
        public void Detach(IObserver observer) => observers.Remove(observer);
        public void Notify(string message) => observers.NotifyAll(message);
    }
    
    #endregion

    #region 5. 发布-订阅模式（通过中介）
    
    // 消息类型
    public class Message<T>
    {
        public string Topic { get; set; }
        public T Data { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
    
    // 订阅者接口
    public interface ISubscriber<T>
    {
        void OnMessage(Message<T> message);
    }
    
    // 事件总线（发布-订阅中介）
    public class EventBus
    {
        private Dictionary<Type, Dictionary<string, List<object>>> subscribers = new();
        
        // 订阅
        public void Subscribe<T>(string topic, ISubscriber<T> subscriber)
        {
            var type = typeof(T);
            if (!subscribers.ContainsKey(type))
            {
                subscribers[type] = new Dictionary<string, List<object>>();
            }
            
            if (!subscribers[type].ContainsKey(topic))
            {
                subscribers[type][topic] = new List<object>();
            }
            
            subscribers[type][topic].Add(subscriber);
        }
        
        // 取消订阅
        public void Unsubscribe<T>(string topic, ISubscriber<T> subscriber)
        {
            var type = typeof(T);
            if (subscribers.ContainsKey(type) && 
                subscribers[type].ContainsKey(topic))
            {
                subscribers[type][topic].Remove(subscriber);
            }
        }
        
        // 发布
        public void Publish<T>(string topic, T data)
        {
            var type = typeof(T);
            if (subscribers.ContainsKey(type) && 
                subscribers[type].ContainsKey(topic))
            {
                var message = new Message<T> { Topic = topic, Data = data };
                foreach (var subscriber in subscribers[type][topic])
                {
                    (subscriber as ISubscriber<T>)?.OnMessage(message);
                }
            }
        }
    }
    
    #endregion

    #region 6. 响应式观察者（Reactive）
    
    // 简化的响应式观察者
    public interface IObservable<T>
    {
        IDisposable Subscribe(IObserver<T> observer);
    }
    
    public interface IObserver<T>
    {
        void OnNext(T value);
        void OnError(Exception error);
        void OnCompleted();
    }
    
    // 响应式主题
    public class ReactiveSubject<T> : IObservable<T>, IObserver<T>
    {
        private List<IObserver<T>> observers = new();
        private bool isCompleted = false;
        
        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!isCompleted)
            {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }
        
        public void OnNext(T value)
        {
            if (!isCompleted)
            {
                foreach (var observer in observers.ToList())
                {
                    observer.OnNext(value);
                }
            }
        }
        
        public void OnError(Exception error)
        {
            if (!isCompleted)
            {
                foreach (var observer in observers.ToList())
                {
                    observer.OnError(error);
                }
                isCompleted = true;
            }
        }
        
        public void OnCompleted()
        {
            if (!isCompleted)
            {
                foreach (var observer in observers.ToList())
                {
                    observer.OnCompleted();
                }
                isCompleted = true;
            }
        }
        
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<T>> _observers;
            private IObserver<T> _observer;
            
            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
            {
                _observers = observers;
                _observer = observer;
            }
            
            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }
    }
    
    #endregion

    #region 7. 属性变更通知（INotifyPropertyChanged）
    
    // .NET标准的属性变更通知
    public class ObservableObject : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) 
                return false;
                
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
    
    // 使用示例
    public class Person : ObservableObject
    {
        private string _name;
        private int _age;
        
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }
        
        public int Age
        {
            get => _age;
            set => SetField(ref _age, value);
        }
    }
    
    #endregion

    #region 8. 条件观察者（带过滤）
    
    public interface IConditionalObserver<T>
    {
        bool ShouldUpdate(T data);
        void Update(T data);
    }
    
    public class ConditionalSubject<T>
    {
        private List<IConditionalObserver<T>> observers = new();
        
        public void Attach(IConditionalObserver<T> observer)
        {
            observers.Add(observer);
        }
        
        public void Notify(T data)
        {
            foreach (var observer in observers)
            {
                if (observer.ShouldUpdate(data))
                {
                    observer.Update(data);
                }
            }
        }
    }
    
    #endregion

    // ==================== 使用示例 ====================
    public class ObserverVariantsDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== 观察者模式的多种变体 ===\n");
            
            // 1. 经典观察者（推模型）
            Console.WriteLine("1. 经典观察者模式：");
            var subject = new ConcreteSubject();
            subject.Attach(new ConcreteObserver("观察者1"));
            subject.Notify("状态已更新");
            
            // 2. 事件驱动
            Console.WriteLine("\n2. 事件驱动观察者：");
            var eventSubject = new EventSubject();
            eventSubject.StateChanged += (s, e) => 
                Console.WriteLine($"状态变更: {e.PropertyName} = {e.NewValue}");
            eventSubject.State = "新状态";
            
            // 3. 发布-订阅
            Console.WriteLine("\n3. 发布-订阅模式：");
            var eventBus = new EventBus();
            var subscriber = new ConcreteSubscriber();
            eventBus.Subscribe<string>("news", subscriber);
            eventBus.Publish("news", "重要新闻");
            
            // 4. 响应式
            Console.WriteLine("\n4. 响应式观察者：");
            var reactive = new ReactiveSubject<int>();
            var observer = new ReactiveObserver();
            using (reactive.Subscribe(observer))
            {
                reactive.OnNext(1);
                reactive.OnNext(2);
                reactive.OnCompleted();
            }
            
            // 5. 属性变更通知
            Console.WriteLine("\n5. 属性变更通知：");
            var person = new Person();
            person.PropertyChanged += (s, e) => 
                Console.WriteLine($"属性 {e.PropertyName} 已变更");
            person.Name = "张三";
            person.Age = 25;
        }
    }
    
    // 辅助类
    public class ConcreteObserver : IObserver
    {
        private string name;
        public ConcreteObserver(string name) => this.name = name;
        public void Update(string message) => Console.WriteLine($"{name} 收到: {message}");
    }
    
    public class ConcreteSubscriber : ISubscriber<string>
    {
        public void OnMessage(Message<string> message)
        {
            Console.WriteLine($"收到消息 [{message.Topic}]: {message.Data}");
        }
    }
    
    public class ReactiveObserver : IObserver<int>
    {
        public void OnNext(int value) => Console.WriteLine($"收到值: {value}");
        public void OnError(Exception error) => Console.WriteLine($"错误: {error.Message}");
        public void OnCompleted() => Console.WriteLine("完成");
    }
}