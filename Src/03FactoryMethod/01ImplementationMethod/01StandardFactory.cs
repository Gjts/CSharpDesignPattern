namespace _03FactoryMethod._01ImplementationMethod
{
    // ==================== 工厂方法模式的多种实现变体 ====================
    // 工厂方法模式也有多种实现方式，虽然没有单例模式那么多，但也很丰富

    #region 1. 标准工厂方法（抽象类实现）
    
    // 抽象产品
    public interface IProduct
    {
        void Operation();
    }
    
    // 具体产品
    public class ConcreteProductA : IProduct
    {
        public void Operation() => Console.WriteLine("产品A操作");
    }
    
    public class ConcreteProductB : IProduct
    {
        public void Operation() => Console.WriteLine("产品B操作");
    }
    
    // 抽象工厂
    public abstract class Creator
    {
        // 工厂方法
        public abstract IProduct CreateProduct();
        
        // 模板方法：可以包含一些业务逻辑
        public void DoOperation()
        {
            var product = CreateProduct();
            product.Operation();
        }
    }
    
    // 具体工厂
    public class ConcreteCreatorA : Creator
    {
        public override IProduct CreateProduct() => new ConcreteProductA();
    }
    
    public class ConcreteCreatorB : Creator
    {
        public override IProduct CreateProduct() => new ConcreteProductB();
    }
    
    #endregion

    #region 2. 泛型工厂方法
    
    // 泛型产品接口
    public interface IGenericProduct
    {
        string Name { get; }
    }
    
    // 泛型工厂接口
    public interface IGenericFactory<T> where T : IGenericProduct, new()
    {
        T Create();
        T CreateWithConfiguration(Action<T> configure);
    }
    
    // 泛型工厂实现
    public class GenericFactory<T> : IGenericFactory<T> where T : IGenericProduct, new()
    {
        public T Create()
        {
            return new T();
        }
        
        public T CreateWithConfiguration(Action<T> configure)
        {
            var product = new T();
            configure?.Invoke(product);
            return product;
        }
    }
    
    #endregion

    #region 3. 参数化工厂方法
    
    public enum ProductType
    {
        TypeA,
        TypeB,
        TypeC
    }
    
    // 参数化工厂
    public class ParameterizedFactory
    {
        public IProduct CreateProduct(ProductType type)
        {
            return type switch
            {
                ProductType.TypeA => new ConcreteProductA(),
                ProductType.TypeB => new ConcreteProductB(),
                _ => throw new ArgumentException($"Unknown product type: {type}")
            };
        }
        
        // 带参数的创建
        public IProduct CreateProduct(string typeName, Dictionary<string, object> parameters)
        {
            // 可以使用反射或其他机制
            var type = Type.GetType(typeName);
            if (type != null && typeof(IProduct).IsAssignableFrom(type))
            {
                var instance = Activator.CreateInstance(type);
                // 应用参数...
                return (IProduct)instance;
            }
            throw new ArgumentException($"Invalid type: {typeName}");
        }
    }
    
    #endregion

    #region 4. 注册式工厂（使用委托）
    
    public class RegisterFactory
    {
        private readonly Dictionary<string, Func<IProduct>> _creators = new();
        
        // 注册创建函数
        public void Register(string key, Func<IProduct> creator)
        {
            _creators[key] = creator;
        }
        
        // 注册类型
        public void Register<T>(string key) where T : IProduct, new()
        {
            _creators[key] = () => new T();
        }
        
        // 创建产品
        public IProduct Create(string key)
        {
            if (_creators.TryGetValue(key, out var creator))
            {
                return creator();
            }
            throw new KeyNotFoundException($"No creator registered for key: {key}");
        }
        
        // 批量创建
        public IEnumerable<IProduct> CreateAll()
        {
            return _creators.Values.Select(creator => creator());
        }
    }
    
    #endregion

    #region 5. 异步工厂方法
    
    // 异步产品接口（用于需要异步初始化的对象）
    public interface IAsyncProduct
    {
        Task InitializeAsync();
        bool IsInitialized { get; }
    }
    
    // 异步工厂接口
    public interface IAsyncFactory<T> where T : IAsyncProduct
    {
        Task<T> CreateAsync();
        Task<T> CreateAndInitializeAsync();
    }
    
    // 异步工厂实现
    public abstract class AsyncFactory<T> : IAsyncFactory<T> where T : IAsyncProduct, new()
    {
        public virtual async Task<T> CreateAsync()
        {
            return await Task.FromResult(new T());
        }
        
        public async Task<T> CreateAndInitializeAsync()
        {
            var product = await CreateAsync();
            await product.InitializeAsync();
            return product;
        }
    }
    
    #endregion

    #region 6. 依赖注入工厂（DI Factory）
    
    // 用于依赖注入容器的工厂
    public interface IServiceFactory<T>
    {
        T Create(IServiceProvider serviceProvider);
    }
    
    public class DIFactory<T> : IServiceFactory<T> where T : class
    {
        private readonly Func<IServiceProvider, T> _factory;
        
        public DIFactory(Func<IServiceProvider, T> factory)
        {
            _factory = factory;
        }
        
        public T Create(IServiceProvider serviceProvider)
        {
            return _factory(serviceProvider);
        }
    }
    
    #endregion

    #region 7. 抽象工厂与工厂方法结合
    
    // 产品族接口
    public interface IProductFamily
    {
        IProduct CreateProductA();
        IProduct CreateProductB();
    }
    
    // 混合工厂：既是工厂方法又包含抽象工厂的特征
    public abstract class HybridFactory
    {
        // 工厂方法
        public abstract IProduct CreateSingleProduct();
        
        // 抽象工厂方法
        public abstract IProductFamily CreateProductFamily();
        
        // 条件创建
        public IProduct CreateByCondition(bool condition)
        {
            if (condition)
            {
                return CreateSingleProduct();
            }
            else
            {
                var family = CreateProductFamily();
                return family.CreateProductA();
            }
        }
    }
    
    #endregion

    // ==================== 使用示例 ====================
    public class FactoryMethodVariantsDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== 工厂方法模式的多种变体 ===\n");
            
            // 1. 标准工厂方法
            Console.WriteLine("1. 标准工厂方法：");
            Creator creator = new ConcreteCreatorA();
            creator.DoOperation();
            
            // 2. 泛型工厂
            Console.WriteLine("\n2. 泛型工厂：");
            var genericFactory = new GenericFactory<GenericProduct>();
            var product = genericFactory.CreateWithConfiguration(p => p.Name = "配置的产品");
            
            // 3. 参数化工厂
            Console.WriteLine("\n3. 参数化工厂：");
            var paramFactory = new ParameterizedFactory();
            var productA = paramFactory.CreateProduct(ProductType.TypeA);
            productA.Operation();
            
            // 4. 注册式工厂
            Console.WriteLine("\n4. 注册式工厂：");
            var registerFactory = new RegisterFactory();
            registerFactory.Register("A", () => new ConcreteProductA());
            registerFactory.Register<ConcreteProductB>("B");
            var registeredProduct = registerFactory.Create("A");
            registeredProduct.Operation();
            
            // 5. 异步工厂
            Console.WriteLine("\n5. 异步工厂：");
            Task.Run(async () =>
            {
                var asyncFactory = new ConcreteAsyncFactory();
                var asyncProduct = await asyncFactory.CreateAndInitializeAsync();
                Console.WriteLine($"异步产品已初始化: {asyncProduct.IsInitialized}");
            }).Wait();
        }
    }
    
    // 辅助类
    public class GenericProduct : IGenericProduct
    {
        public string Name { get; set; }
    }
    
    public class AsyncProduct : IAsyncProduct
    {
        public bool IsInitialized { get; private set; }
        
        public async Task InitializeAsync()
        {
            await Task.Delay(100); // 模拟异步初始化
            IsInitialized = true;
        }
    }
    
    public class ConcreteAsyncFactory : AsyncFactory<AsyncProduct>
    {
    }
}