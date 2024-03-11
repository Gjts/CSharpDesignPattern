using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Singleton._01ImplementationMethod
{
    // 2.饿加载单例模式
    // 适用场景：当程序中的单例对象会被频繁使用，或者需要在程序启动时就立即创建对象时，可以选择饿汉式单例。
    // 特点：饿汉式单例在程序启动时就会被实例化，保证了对象的唯一性和线程安全性。但可能会造成资源浪费，因为对象在程序启动时就被创建。
    public class HungrySingleton
    {
        // 定义为static 保证了对象的唯一性和线程安全性
        private static readonly HungrySingleton instance = new HungrySingleton();

        // 私有构造函数，避免外部直接实例化
        private HungrySingleton()
        {

        }

        public static HungrySingleton GetInstance
        {
            get
            {
                return instance;
            }
        }

        public void SomeMethod()
        {
            Console.WriteLine("调用了饿加载单例模式的方法");
        }
    }
}
