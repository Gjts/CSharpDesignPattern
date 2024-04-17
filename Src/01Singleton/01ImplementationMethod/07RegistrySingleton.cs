using System.Collections.Concurrent;

namespace _Singleton._01ImplementationMethod
{
    // 7.泛型登记式单例
    // 适用场景：当需要管理多个单例对象，并且希望通过一个中心化的管理类来统一管理时，可以选择登记式单例。
    // 特点：登记式单例通过一个中心化的注册表来管理多个单例对象，可以动态添加、删除单例对象，灵活性较高。但需要注意注册表的线程安全性。
    public class RegistrySingleton<T> where T : class // new()
    {
        // ConcurrentDictionary 线程安全集合
        private static readonly ConcurrentDictionary<string, T> registry = new ConcurrentDictionary<string, T>();

        // 私有构造函数，避免外部直接实例化
        private RegistrySingleton()
        {

        }

        public static T GetInstance(string key)
        {
            if (!registry.ContainsKey(key))
            {
                // new不支持非公共的无参构造函数 
                // instance = new T();

                // 第二个参数防止异常：“没有为该对象定义无参数的构造函数。” 
                var instance = Activator.CreateInstance(typeof(T), true) as T;
                if (instance != null)
                {
                    registry[key] = instance;
                }
            }

            return registry[key];
        }

        public void SomeMethod()
        {
            Console.WriteLine("调用了登记式单例模式的方法");
        }
    }
}
