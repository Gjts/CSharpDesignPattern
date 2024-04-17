namespace _Singleton._01ImplementationMethod
{
    // 1.懒加载单例模式
    // 适用场景：当程序中的单例对象不是经常被使用，而是在需要时才被创建时，可以选择懒汉式单例。
    // 特点：懒汉式单例在第一次使用时才会被实例化，延迟了对象的创建时间，但需要注意在多线程环境下需要考虑线程安全性。
    public class LazySingleton
    {
        private static readonly Lazy<LazySingleton> instance = new(() => new LazySingleton());

        // 私有构造函数，避免外部直接实例化
        private LazySingleton()
        {

        }

        public static LazySingleton GetInstance
        {
            get
            {
                return instance.Value;
            }
        }

        public static string SomeMethod()
        {
            return "调用了懒加载单例模式的方法";
        }
    }
}
