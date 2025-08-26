# 设计模式实现变体深度分析

## 一、创建型模式变体

### 1. 单例模式 (Singleton) - 变体最多 ⭐⭐⭐⭐⭐

单例模式拥有最多的实现变体，主要是因为需要解决**线程安全**和**性能**的平衡问题。

#### 实现变体：
1. **懒汉式（Lazy Initialization）**
   - 简单懒汉式（线程不安全）
   - 同步方法懒汉式（synchronized）
   - 双重检查锁定（Double-Check Locking）
   - 静态内部类（Holder Pattern）
   - 枚举实现（Enum Singleton）

2. **饿汉式（Eager Initialization）**
   - 静态常量
   - 静态代码块

3. **现代实现**
   - Lazy<T>（.NET特有）
   - volatile + DCL
   - CAS（Compare-And-Swap）实现

4. **特殊变体**
   - 注册式单例（Registry Singleton）
   - 容器式单例（Container Singleton）
   - ThreadLocal单例（线程级单例）

```csharp
// 变体示例对比
// 1. 简单懒汉式（线程不安全）
public class SimpleLazy {
    private static SimpleLazy instance;
    public static SimpleLazy GetInstance() {
        if (instance == null) {
            instance = new SimpleLazy();
        }
        return instance;
    }
}

// 2. 双重检查锁定
public class DoubleCheckLocking {
    private static volatile DoubleCheckLocking instance;
    private static readonly object lockObj = new object();
    
    public static DoubleCheckLocking GetInstance() {
        if (instance == null) {
            lock (lockObj) {
                if (instance == null) {
                    instance = new DoubleCheckLocking();
                }
            }
        }
        return instance;
    }
}

// 3. 静态内部类
public class InnerClassSingleton {
    private class Holder {
        public static readonly InnerClassSingleton Instance = new InnerClassSingleton();
    }
    public static InnerClassSingleton GetInstance() {
        return Holder.Instance;
    }
}

// 4. .NET Lazy<T>
public class LazySingleton {
    private static readonly Lazy<LazySingleton> lazy = 
        new Lazy<LazySingleton>(() => new LazySingleton());
    
    public static LazySingleton Instance => lazy.Value;
}
```

---

### 2. 工厂模式家族 - 变体较多 ⭐⭐⭐⭐

工厂模式有三个主要变体，每个还有子变体：

#### A. 简单工厂（Simple Factory）
1. **静态工厂方法**
2. **实例工厂方法**
3. **参数化工厂**
4. **注册工厂（使用反射）**

#### B. 工厂方法（Factory Method）
1. **标准实现（抽象类）**
2. **接口实现**
3. **参数化工厂方法**
4. **泛型工厂方法**

#### C. 抽象工厂（Abstract Factory）
1. **标准实现**
2. **工厂的工厂**
3. **动态工厂（使用反射）**
4. **可配置工厂（使用配置文件）**

```csharp
// 工厂方法的不同变体
// 1. 标准实现
public abstract class Creator {
    public abstract IProduct CreateProduct();
}

// 2. 泛型工厂
public interface IFactory<T> where T : IProduct {
    T Create();
}

// 3. 参数化工厂
public interface IParameterizedFactory {
    IProduct Create(string type, Dictionary<string, object> parameters);
}

// 4. 注册工厂
public class RegisterFactory {
    private Dictionary<string, Func<IProduct>> creators = new();
    
    public void Register(string key, Func<IProduct> creator) {
        creators[key] = creator;
    }
    
    public IProduct Create(string key) {
        return creators[key]();
    }
}
```

---

### 3. 建造者模式 (Builder) - 变体中等 ⭐⭐⭐

#### 实现变体：
1. **经典建造者（含Director）**
2. **流式建造者（Fluent Builder）**
3. **静态内部类建造者**
4. **递归泛型建造者**
5. **步骤建造者（Step Builder）**

```csharp
// 1. 流式建造者
public class FluentBuilder {
    private Product product = new Product();
    
    public FluentBuilder WithName(string name) {
        product.Name = name;
        return this;
    }
    
    public FluentBuilder WithPrice(decimal price) {
        product.Price = price;
        return this;
    }
    
    public Product Build() => product;
}

// 2. 步骤建造者（强制构建顺序）
public interface IStep1 {
    IStep2 SetName(string name);
}

public interface IStep2 {
    IStep3 SetPrice(decimal price);
}

public interface IStep3 {
    Product Build();
}

public class StepBuilder : IStep1, IStep2, IStep3 {
    private Product product = new Product();
    
    public IStep2 SetName(string name) {
        product.Name = name;
        return this;
    }
    
    public IStep3 SetPrice(decimal price) {
        product.Price = price;
        return this;
    }
    
    public Product Build() => product;
}
```

---

### 4. 原型模式 (Prototype) - 变体较少 ⭐⭐

#### 实现变体：
1. **浅克隆（Shallow Clone）**
2. **深克隆（Deep Clone）**
3. **序列化克隆**
4. **原型管理器**

```csharp
// 1. 浅克隆
public class ShallowPrototype : ICloneable {
    public object Clone() => this.MemberwiseClone();
}

// 2. 深克隆（序列化方式）
public class DeepPrototype {
    public T DeepClone<T>() {
        using var stream = new MemoryStream();
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, this);
        stream.Seek(0, SeekOrigin.Begin);
        return (T)formatter.Deserialize(stream);
    }
}

// 3. 原型管理器
public class PrototypeManager {
    private Dictionary<string, IPrototype> prototypes = new();
    
    public void Register(string key, IPrototype prototype) {
        prototypes[key] = prototype;
    }
    
    public IPrototype Clone(string key) {
        return prototypes[key].Clone();
    }
}
```

---

## 二、结构型模式变体

### 5. 适配器模式 (Adapter) - 变体中等 ⭐⭐⭐

#### 实现变体：
1. **类适配器（继承）**
2. **对象适配器（组合）**
3. **双向适配器**
4. **多重适配器**
5. **缺省适配器**

```csharp
// 1. 类适配器（C#中使用接口）
public class ClassAdapter : Adaptee, ITarget {
    public void Request() {
        base.SpecificRequest();
    }
}

// 2. 对象适配器
public class ObjectAdapter : ITarget {
    private Adaptee adaptee;
    
    public ObjectAdapter(Adaptee adaptee) {
        this.adaptee = adaptee;
    }
    
    public void Request() {
        adaptee.SpecificRequest();
    }
}

// 3. 双向适配器
public class TwoWayAdapter : ITarget, IAdaptee {
    private ITarget target;
    private IAdaptee adaptee;
    
    public void Request() { /* 适配逻辑 */ }
    public void SpecificRequest() { /* 反向适配逻辑 */ }
}
```

---

### 6. 装饰器模式 (Decorator) - 变体较多 ⭐⭐⭐⭐

#### 实现变体：
1. **标准装饰器**
2. **透明装饰器**
3. **半透明装饰器**
4. **动态装饰器（使用动态代理）**
5. **条件装饰器**

```csharp
// 1. 标准装饰器
public abstract class Decorator : Component {
    protected Component component;
    
    public Decorator(Component component) {
        this.component = component;
    }
    
    public override void Operation() {
        component?.Operation();
    }
}

// 2. 条件装饰器
public class ConditionalDecorator : Decorator {
    private Func<bool> condition;
    
    public ConditionalDecorator(Component component, Func<bool> condition) 
        : base(component) {
        this.condition = condition;
    }
    
    public override void Operation() {
        if (condition()) {
            // 添加装饰
            Console.WriteLine("额外功能");
        }
        base.Operation();
    }
}

// 3. 动态装饰器链
public class DecoratorChain {
    private Component component;
    private List<Func<Component, Component>> decorators = new();
    
    public DecoratorChain Add(Func<Component, Component> decorator) {
        decorators.Add(decorator);
        return this;
    }
    
    public Component Build() {
        var result = component;
        foreach (var decorator in decorators) {
            result = decorator(result);
        }
        return result;
    }
}
```

---

### 7. 代理模式 (Proxy) - 变体较多 ⭐⭐⭐⭐

#### 实现变体：
1. **虚拟代理（Virtual Proxy）**
2. **远程代理（Remote Proxy）**
3. **保护代理（Protection Proxy）**
4. **缓存代理（Cache Proxy）**
5. **智能引用代理（Smart Reference）**
6. **动态代理（Dynamic Proxy）**
7. **写时复制代理（Copy-on-Write）**

```csharp
// 1. 虚拟代理（延迟加载）
public class VirtualProxy : ISubject {
    private RealSubject realSubject;
    
    public void Request() {
        if (realSubject == null) {
            realSubject = new RealSubject(); // 延迟创建
        }
        realSubject.Request();
    }
}

// 2. 保护代理（权限控制）
public class ProtectionProxy : ISubject {
    private RealSubject realSubject;
    private IPermissionChecker checker;
    
    public void Request() {
        if (checker.HasPermission()) {
            realSubject.Request();
        } else {
            throw new UnauthorizedAccessException();
        }
    }
}

// 3. 缓存代理
public class CacheProxy : IDataService {
    private IDataService realService;
    private Dictionary<string, object> cache = new();
    
    public object GetData(string key) {
        if (!cache.ContainsKey(key)) {
            cache[key] = realService.GetData(key);
        }
        return cache[key];
    }
}
```

---

### 8. 桥接模式 (Bridge) - 变体较少 ⭐⭐

#### 实现变体：
1. **标准桥接**
2. **多维度桥接**
3. **动态桥接**

---

### 9. 组合模式 (Composite) - 变体较少 ⭐⭐

#### 实现变体：
1. **透明组合（统一接口）**
2. **安全组合（区分接口）**
3. **带缓存的组合**

---

### 10. 外观模式 (Facade) - 变体较少 ⭐⭐

#### 实现变体：
1. **静态外观**
2. **实例外观**
3. **抽象外观**
4. **分层外观**

---

### 11. 享元模式 (Flyweight) - 变体较少 ⭐⭐

#### 实现变体：
1. **单纯享元**
2. **复合享元**
3. **享元工厂变体**

---

## 三、行为型模式变体

### 12. 策略模式 (Strategy) - 变体中等 ⭐⭐⭐

#### 实现变体：
1. **经典策略（接口）**
2. **委托策略（使用委托/函数）**
3. **枚举策略**
4. **注解策略**
5. **混合策略**

```csharp
// 1. 经典策略
public interface IStrategy {
    void Execute();
}

// 2. 委托策略
public class DelegateStrategy {
    private Action<Context> strategy;
    
    public DelegateStrategy(Action<Context> strategy) {
        this.strategy = strategy;
    }
    
    public void Execute(Context context) {
        strategy(context);
    }
}

// 3. 枚举策略
public enum StrategyType {
    [Strategy(typeof(ConcreteStrategyA))]
    TypeA,
    [Strategy(typeof(ConcreteStrategyB))]
    TypeB
}

// 4. 混合策略（组合多个策略）
public class CompositeStrategy : IStrategy {
    private List<IStrategy> strategies = new();
    
    public void AddStrategy(IStrategy strategy) {
        strategies.Add(strategy);
    }
    
    public void Execute() {
        foreach (var strategy in strategies) {
            strategy.Execute();
        }
    }
}
```

---

### 13. 观察者模式 (Observer) - 变体较多 ⭐⭐⭐⭐

#### 实现变体：
1. **推模型（Push）**
2. **拉模型（Pull）**
3. **事件驱动（.NET Events）**
4. **发布-订阅（Pub-Sub）**
5. **响应式（Reactive）**
6. **弱引用观察者**

```csharp
// 1. 推模型
public interface IPushObserver {
    void Update(string data);
}

// 2. 拉模型
public interface IPullObserver {
    void Update(ISubject subject);
}

// 3. 事件驱动
public class EventSubject {
    public event EventHandler<DataEventArgs> DataChanged;
    
    protected virtual void OnDataChanged(DataEventArgs e) {
        DataChanged?.Invoke(this, e);
    }
}

// 4. 弱引用观察者（防止内存泄漏）
public class WeakObserver {
    private WeakReference weakRef;
    
    public WeakObserver(IObserver observer) {
        weakRef = new WeakReference(observer);
    }
    
    public void Notify(object data) {
        if (weakRef.IsAlive) {
            ((IObserver)weakRef.Target).Update(data);
        }
    }
}
```

---

### 14. 模板方法模式 (Template Method) - 变体中等 ⭐⭐⭐

#### 实现变体：
1. **抽象类模板**
2. **接口+默认方法**
3. **钩子方法模板**
4. **回调模板**
5. **策略模板混合**

```csharp
// 1. 带钩子的模板方法
public abstract class HookTemplate {
    public void TemplateMethod() {
        Step1();
        if (Hook()) {  // 钩子方法
            Step2();
        }
        Step3();
    }
    
    protected abstract void Step1();
    protected abstract void Step2();
    protected abstract void Step3();
    
    protected virtual bool Hook() => true;  // 可选覆盖
}

// 2. 回调模板
public class CallbackTemplate {
    public void Execute(
        Action step1, 
        Action step2, 
        Action step3) {
        step1();
        step2();
        step3();
    }
}
```

---

### 15. 命令模式 (Command) - 变体较多 ⭐⭐⭐⭐

#### 实现变体：
1. **简单命令**
2. **宏命令（组合命令）**
3. **撤销命令**
4. **队列命令**
5. **异步命令**
6. **智能命令**

```csharp
// 1. 撤销命令
public interface IUndoableCommand : ICommand {
    void Execute();
    void Undo();
}

// 2. 宏命令
public class MacroCommand : ICommand {
    private List<ICommand> commands = new();
    
    public void Add(ICommand command) {
        commands.Add(command);
    }
    
    public void Execute() {
        foreach (var command in commands) {
            command.Execute();
        }
    }
}

// 3. 异步命令
public interface IAsyncCommand {
    Task ExecuteAsync();
}

// 4. 智能命令（带条件执行）
public class SmartCommand : ICommand {
    private Func<bool> canExecute;
    private Action execute;
    
    public void Execute() {
        if (canExecute()) {
            execute();
        }
    }
}
```

---

### 16. 迭代器模式 (Iterator) - 变体中等 ⭐⭐⭐

#### 实现变体：
1. **外部迭代器**
2. **内部迭代器**
3. **双向迭代器**
4. **过滤迭代器**
5. **并发迭代器**

---

### 17. 责任链模式 (Chain of Responsibility) - 变体中等 ⭐⭐⭐

#### 实现变体：
1. **纯责任链（只有一个处理）**
2. **不纯责任链（多个处理）**
3. **动态责任链**
4. **双向责任链**

---

### 18. 中介者模式 (Mediator) - 变体较少 ⭐⭐

#### 实现变体：
1. **简单中介者**
2. **智能中介者**
3. **事件中介者**

---

### 19. 备忘录模式 (Memento) - 变体较少 ⭐⭐

#### 实现变体：
1. **白箱备忘录**
2. **黑箱备忘录**
3. **多状态备忘录**

---

### 20. 状态模式 (State) - 变体中等 ⭐⭐⭐

#### 实现变体：
1. **状态转换表**
2. **分层状态机**
3. **并发状态机**

---

### 21. 访问者模式 (Visitor) - 变体较少 ⭐⭐

#### 实现变体：
1. **单分派访问者**
2. **双分派访问者**
3. **反射访问者**

---

### 22. 解释器模式 (Interpreter) - 变体较少 ⭐⭐

#### 实现变体：
1. **递归下降解释器**
2. **基于栈的解释器**

---

## 四、总结分析

### 变体数量排行榜

| 排名 | 设计模式 | 变体数量 | 原因 |
|------|---------|---------|------|
| 1 | **单例模式** | 10+ | 线程安全和性能的权衡导致多种实现 |
| 2 | **工厂模式家族** | 8+ | 三个相关模式，每个都有变体 |
| 3 | **观察者模式** | 6+ | 不同的通知机制和耦合程度 |
| 4 | **代理模式** | 7+ | 不同的代理目的（虚拟、保护、缓存等） |
| 5 | **命令模式** | 6+ | 支持撤销、宏命令、异步等扩展 |
| 6 | **装饰器模式** | 5+ | 透明性和功能组合的不同方式 |
| 7 | **策略模式** | 5+ | 策略的不同组织和选择方式 |
| 8 | **适配器模式** | 5+ | 类适配、对象适配、双向适配等 |
| 9 | **模板方法模式** | 5+ | 钩子、回调等变体 |
| 10 | **建造者模式** | 5+ | 流式API、步骤建造等现代变体 |

### 为什么有些模式变体多，有些少？

#### 变体多的原因：
1. **性能与安全的权衡**（如单例模式）
2. **不同应用场景的需求**（如代理模式）
3. **语言特性的演进**（如观察者模式的事件机制）
4. **设计灵活性需求**（如工厂模式）

#### 变体少的原因：
1. **模式本身已经很简单**（如外观模式）
2. **应用场景相对固定**（如访问者模式）
3. **模式的核心思想限制了变化**（如解释器模式）

### 实践建议

1. **选择合适的变体**
   - 根据具体需求选择
   - 考虑性能、可维护性、团队熟悉度

2. **不要过度设计**
   - 从简单实现开始
   - 根据需要逐步演进

3. **理解核心思想**
   - 变体只是实现细节
   - 核心思想才是关键

4. **结合使用**
   - 多个模式可以组合使用
   - 变体之间也可以混合