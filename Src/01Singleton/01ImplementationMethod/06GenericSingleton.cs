using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Singleton._01ImplementationMethod
{
    // 6.泛型单例模式
    // 适用场景：泛型单例模式适用于需要创建多种类型的单例对象，并且希望在第一次使用时才实例化该对象的场景。
    // 特点：泛型单例在实现上与枚举单例类似，通过泛型参数来指定单例对象的类型。这样可以确保单例对象的类型安全性，并且在使用时不需要进行类型转换操作。泛型单例也具有简洁、高效和线程安全的特点，适用于需要灵活支持不同类型的单例对象的情况。
    public class GenericSingleton<T> where T : class // new()
    {
        // 定义一个静态变量instance，用来保存实例
        private static T? instance;

        // 定义一个静态的同步锁，用于线程安全
        private static readonly object lockObject = new object();

        // 私有构造函数，避免外部直接实例化
        private GenericSingleton()
        {
        }

        // 定义一个静态方法GetInstance，用来获取实例
        public static T? GetInstance
        {
            get
            {
                // 如果实例为空，则进入加锁状态
                if (instance == null)
                {
                    // 加锁，线程安全
                    lock (lockObject)
                    {
                        // 如果实例为空，则创建实例
                        if (instance == null)
                        {
                            // new不支持非公共的无参构造函数 
                            // instance = new T();

                            // 第二个参数防止异常：“没有为该对象定义无参数的构造函数。” 
                            instance = Activator.CreateInstance(typeof(T), true) as T;
                        }
                    }
                }

                // 返回实例
                return instance;
            }
        }

        public void SomeMethod()
        {
            Console.WriteLine("调用了泛型单例模式的方法");
        }
    }
}
