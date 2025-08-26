using _Singleton._01ImplementationMethod;
using _Singleton._02Example._01lazySingleton;

namespace _01Singleton
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 单例模式 (Singleton Pattern) ================================");
            Console.WriteLine("适用场景：需要确保一个类只有一个实例，并提供全局访问点；如配置管理器、日志记录器、连接池等");
            Console.WriteLine("特点：类自己负责创建自己的唯一实例，并提供访问该实例的方法");
            Console.WriteLine("优点：内存中只有一个实例，减少内存开销；避免对资源的多重占用；设置全局访问点\n");

            Console.WriteLine("-------------------------------- 实现方式演示 ----------------------------------");
            
            // 1. 懒汉式单例
            Console.WriteLine("1. 懒汉式单例（Lazy<T>实现）：");
            var lazy1 = LazySingleton.GetInstance;
            var lazy2 = LazySingleton.GetInstance;
            Console.WriteLine($"   两次获取是否为同一实例: {ReferenceEquals(lazy1, lazy2)}");
            Console.WriteLine($"   {LazySingleton.SomeMethod()}");
            
            // 2. 饿汉式单例
            Console.WriteLine("\n2. 饿汉式单例（静态初始化）：");
            var hungry1 = HungrySingleton.GetInstance;
            var hungry2 = HungrySingleton.GetInstance;
            Console.WriteLine($"   两次获取是否为同一实例: {ReferenceEquals(hungry1, hungry2)}");
            hungry1.SomeMethod();
            
            // 3. 双重检查锁定单例
            Console.WriteLine("\n3. 双重检查锁定单例（线程安全）：");
            var doubleLock1 = DoubleLockSingleton.GetInstance;
            var doubleLock2 = DoubleLockSingleton.GetInstance;
            Console.WriteLine($"   两次获取是否为同一实例: {ReferenceEquals(doubleLock1, doubleLock2)}");
            doubleLock1.SomeMethod();

            Console.WriteLine("\n-------------------------------- 数据库连接池示例 ----------------------------------");
            
            var config = new ConnectionPoolConfig { MaxConnections = 10 };
            
            Console.WriteLine("1. 创建连接池（单例）：");
            var pool1 = ConnectionPool.GetInstance(config, 1);
            var pool2 = ConnectionPool.GetInstance(config, 1);
            Console.WriteLine($"   两个连接池是否为同一实例: {ReferenceEquals(pool1, pool2)}");
            
            Console.WriteLine("\n2. 获取数据库连接：");
            var client1 = pool1.GetClient(1000);
            if (client1 != null)
            {
                Console.WriteLine($"   获取到连接: {client1}");
                pool1.ReleaseClient(client1);
                Console.WriteLine("   连接已释放");
            }
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 单例模式保证全局只有一个实例");
            Console.WriteLine("- 提供了多种实现方式，各有优缺点");
            Console.WriteLine("- 常用于资源管理、配置管理等场景");
        }
    }
}
