using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Singleton._01ImplementationMethod
{
    // 3.枚举单例
    // 适用场景：当需要简单、高效且线程安全的单例模式时，可以选择枚举单例。
    // 特点：枚举单例是一种简单、高效且线程安全的单例模式，通过枚举类型来实现单例对象的创建。
    public enum EnumInstance
    {
        Instance
    }

    public class EnumSingleton
    {
        public void SomeMethod()
        {
            Console.WriteLine("调用了枚举单例模式的方法");
        }
    }
}
