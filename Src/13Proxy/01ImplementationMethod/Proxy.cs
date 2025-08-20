namespace _13Proxy._01ImplementationMethod
{
    // Subject接口
    public interface ISubject
    {
        void Request();
    }

    // 真实主题类
    public class RealSubject : ISubject
    {
        public void Request()
        {
            Console.WriteLine("RealSubject: 处理请求");
        }
    }

    // 代理类
    public class Proxy : ISubject
    {
        private RealSubject realSubject;

        public void Request()
        {
            // 在访问真实主题之前可以执行一些操作
            if (CheckAccess())
            {
                if (realSubject == null)
                {
                    realSubject = new RealSubject();
                    Console.WriteLine("Proxy: 创建RealSubject实例");
                }

                Console.WriteLine("Proxy: 转发请求给RealSubject");
                realSubject.Request();
                
                // 在访问真实主题之后可以执行一些操作
                LogAccess();
            }
        }

        private bool CheckAccess()
        {
            Console.WriteLine("Proxy: 检查访问权限");
            return true;
        }

        private void LogAccess()
        {
            Console.WriteLine($"Proxy: 记录访问日志 - {DateTime.Now}");
        }
    }

    // 虚拟代理示例
    public interface IExpensiveObject
    {
        void Process();
    }

    public class ExpensiveObject : IExpensiveObject
    {
        public ExpensiveObject()
        {
            Console.WriteLine("ExpensiveObject: 初始化（耗时操作）");
            System.Threading.Thread.Sleep(1000); // 模拟耗时初始化
        }

        public void Process()
        {
            Console.WriteLine("ExpensiveObject: 处理数据");
        }
    }

    public class VirtualProxy : IExpensiveObject
    {
        private ExpensiveObject expensiveObject;

        public void Process()
        {
            Console.WriteLine("VirtualProxy: 延迟初始化检查");
            if (expensiveObject == null)
            {
                Console.WriteLine("VirtualProxy: 第一次使用，创建ExpensiveObject");
                expensiveObject = new ExpensiveObject();
            }
            expensiveObject.Process();
        }
    }
}
