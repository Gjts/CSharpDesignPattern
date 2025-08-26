namespace _16Respon
{
    // 自定义异常类型
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }

    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base(message) { }
    }

    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message) { }
    }

    // 抽象异常处理器
    public abstract class ExceptionHandler
    {
        protected ExceptionHandler? _nextHandler;
        protected string _handlerName;

        protected ExceptionHandler(string handlerName)
        {
            _handlerName = handlerName;
        }

        public void SetNext(ExceptionHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public void Handle(Exception exception)
        {
            if (CanHandle(exception))
            {
                ProcessException(exception);
            }
            else if (_nextHandler != null)
            {
                Console.WriteLine($"  {_handlerName}: 无法处理此异常，传递给下一个处理器");
                _nextHandler.Handle(exception);
            }
            else
            {
                Console.WriteLine($"  {_handlerName}: 没有处理器能够处理此异常!");
                Console.WriteLine($"    未处理的异常: {exception.GetType().Name} - {exception.Message}");
            }
        }

        protected abstract bool CanHandle(Exception exception);
        protected abstract void ProcessException(Exception exception);
    }

    // 验证异常处理器
    public class ValidationExceptionHandler : ExceptionHandler
    {
        public ValidationExceptionHandler() : base("验证异常处理器")
        {
        }

        protected override bool CanHandle(Exception exception)
        {
            return exception is ValidationException;
        }

        protected override void ProcessException(Exception exception)
        {
            Console.WriteLine($"  {_handlerName}: 处理验证异常");
            Console.WriteLine($"    错误信息: {exception.Message}");
            Console.WriteLine($"    处理方式: 返回400错误，提示用户输入正确的数据");
        }
    }

    // 认证异常处理器
    public class AuthenticationExceptionHandler : ExceptionHandler
    {
        public AuthenticationExceptionHandler() : base("认证异常处理器")
        {
        }

        protected override bool CanHandle(Exception exception)
        {
            return exception is AuthenticationException;
        }

        protected override void ProcessException(Exception exception)
        {
            Console.WriteLine($"  {_handlerName}: 处理认证异常");
            Console.WriteLine($"    错误信息: {exception.Message}");
            Console.WriteLine($"    处理方式: 返回401错误，重定向到登录页面");
            Console.WriteLine($"    记录失败尝试到安全日志");
        }
    }

    // 数据库异常处理器
    public class DatabaseExceptionHandler : ExceptionHandler
    {
        public DatabaseExceptionHandler() : base("数据库异常处理器")
        {
        }

        protected override bool CanHandle(Exception exception)
        {
            return exception is DatabaseException;
        }

        protected override void ProcessException(Exception exception)
        {
            Console.WriteLine($"  {_handlerName}: 处理数据库异常");
            Console.WriteLine($"    错误信息: {exception.Message}");
            Console.WriteLine($"    处理方式: 重试连接，记录错误日志");
            Console.WriteLine($"    通知DBA团队");
        }
    }

    // 通用异常处理器（处理所有未处理的异常）
    public class GeneralExceptionHandler : ExceptionHandler
    {
        public GeneralExceptionHandler() : base("通用异常处理器")
        {
        }

        protected override bool CanHandle(Exception exception)
        {
            // 总是返回true，处理所有异常
            return true;
        }

        protected override void ProcessException(Exception exception)
        {
            Console.WriteLine($"  {_handlerName}: 处理通用异常");
            Console.WriteLine($"    异常类型: {exception.GetType().Name}");
            Console.WriteLine($"    错误信息: {exception.Message}");
            Console.WriteLine($"    处理方式: 记录详细错误日志，返回500错误");
            Console.WriteLine($"    发送错误报告给开发团队");
        }
    }
}