namespace _Singleton._01ImplementationMethod
{
    // 5.双检查单例模式
    // 适用场景：当需要延迟加载单例对象，并且要保证线程安全性时，可以选择双重检查锁定单例。
    // 特点：双重检查锁定单例结合了懒汉式和饿汉式的优点，既实现了延迟加载，又保证了线程安全。通过双重检查锁定，可以避免多线程环境下的竞态条件。
    public class DoubleLockSingleton
    {
        // 使用volatile关键字可以确保在多线程环境下，当读取或写入 instance 变量时，不会发生数据竞争问题。还可以确保最新的值会被其他线程可见
        private static volatile DoubleLockSingleton? instance;

        // 定义一个静态的同步锁，用于线程安全
        private static object syncRoot = new object();

        // 私有构造函数，避免外部直接实例化
        private DoubleLockSingleton()
        {
        }

        // 定义一个静态的单例方法，用于获取单例对象
        public static DoubleLockSingleton GetInstance
        {
            get
            {
                // 如果实例变量为空，则进入加锁状态
                if (instance == null)
                {
                    // 加锁，线程安全
                    lock (syncRoot)
                    {
                        // 如果实例变量为空，则创建一个新的单例对象
                        if (instance == null)
                        {
                            instance = new DoubleLockSingleton();
                        }
                    }
                }

                // 返回实例变量
                return instance;
            }
        }

        public void SomeMethod()
        {
            Console.WriteLine("调用了多线程双检查单例模式的方法");
        }
    }
}
