using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Singleton._02Example._02HungrySingleton
{
    // 单线程日志管理器
    public class Logger
    {
        // 定义为static 保证了对象的唯一性和线程安全性
        private static readonly Logger instance = new Logger();

        // 私有构造函数，避免外部直接实例化
        private Logger()
        {

        }

        public static Logger GetInstance
        {
            get
            {
                return instance;
            }
        }

        public void LogMessage(string message)
        {
            Console.WriteLine($"Logging message: {message}");
        }
    }
}
