namespace _Singleton._01ImplementationMethod
{
    // 4.静态内部类单例模式
    // 适用场景：静态内部类单例适用于需要延迟加载、线程安全的场景，且不需要额外的线程同步措施。
    // 特点：静态内部类单例通过静态内部类来延迟加载单例对象，保证了线程安全性，且代码结构清晰，不需要额外的同步措施。
    public class StaticInnerClassSingleton
    {
        // 私有构造函数，避免外部直接实例化
        private StaticInnerClassSingleton()
        {

        }

        // 获取单例对象
        public static StaticInnerClassSingleton GetInstance
        {
            get
            {
                return Nested.instance;
            }
        }

        // 静态内部类
        private class Nested
        {
            // 静态单例对象
            internal static readonly StaticInnerClassSingleton instance = new StaticInnerClassSingleton();

            // 静态构造函数
            static Nested()
            {
                // 静态构造函数
            }
        }

        public void SomeMethod()
        {
            Console.WriteLine("调用了静态内部单例模式的方法");
        }
    }
}
