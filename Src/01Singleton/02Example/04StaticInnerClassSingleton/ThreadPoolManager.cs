using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Singleton._02Example._04StaticInnerClassSingleton
{
    // 线程池管理器
    public class ThreadPoolManager
    {
        private ThreadPoolManager() { }

        public static ThreadPoolManager GetInstance()
        {
            return ThreadSafeSingleton.instance;
        }

        public void AddTask(Action task)
        {
            ThreadPool.QueueUserWorkItem(state => task());
        }

        // 内部静态类
        private class ThreadSafeSingleton
        {
            internal static readonly ThreadPoolManager instance = new ThreadPoolManager();
        }
    }
}
