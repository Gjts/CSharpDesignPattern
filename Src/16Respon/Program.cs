namespace _16Respon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 责任链模式 (Chain of Responsibility Pattern) ===\n");

            // 示例1：请求审批流程
            Console.WriteLine("示例1：请求审批流程");
            Console.WriteLine("------------------------");
            RunApprovalWorkflowExample();

            Console.WriteLine("\n示例2：异常处理链");
            Console.WriteLine("------------------------");
            RunExceptionHandlingExample();

            Console.WriteLine("\n示例3：HTTP请求处理管道");
            Console.WriteLine("------------------------");
            RunHttpPipelineExample();
        }

        static void RunApprovalWorkflowExample()
        {
            // 构建审批链
            var teamLeader = new TeamLeaderApprover();
            var manager = new ManagerApprover();
            var director = new DirectorApprover();
            var ceo = new CEOApprover();

            teamLeader.SetNext(manager);
            manager.SetNext(director);
            director.SetNext(ceo);

            // 测试不同金额的请求
            var requests = new[]
            {
                new PurchaseRequest("办公用品", 800, "张三"),
                new PurchaseRequest("笔记本电脑", 8000, "李四"),
                new PurchaseRequest("团队建设活动", 25000, "王五"),
                new PurchaseRequest("服务器设备", 80000, "赵六"),
                new PurchaseRequest("公司并购", 1000000, "钱七")
            };

            foreach (var request in requests)
            {
                Console.WriteLine($"\n处理请求: {request.Purpose} (金额: ¥{request.Amount:N0})");
                teamLeader.HandleRequest(request);
            }
        }

        static void RunExceptionHandlingExample()
        {
            // 构建异常处理链
            var validationHandler = new ValidationExceptionHandler();
            var authHandler = new AuthenticationExceptionHandler();
            var dbHandler = new DatabaseExceptionHandler();
            var generalHandler = new GeneralExceptionHandler();

            validationHandler.SetNext(authHandler);
            authHandler.SetNext(dbHandler);
            dbHandler.SetNext(generalHandler);

            // 模拟不同类型的异常
            var exceptions = new Exception[]
            {
                new ValidationException("邮箱格式不正确"),
                new AuthenticationException("用户名或密码错误"),
                new DatabaseException("数据库连接超时"),
                new FileNotFoundException("配置文件未找到"),
                new NullReferenceException("对象引用未设置")
            };

            foreach (var ex in exceptions)
            {
                Console.WriteLine($"\n处理异常: {ex.GetType().Name}");
                validationHandler.Handle(ex);
            }
        }

        static void RunHttpPipelineExample()
        {
            // 构建HTTP请求处理管道
            var authMiddleware = new AuthenticationMiddleware();
            var rateLimitMiddleware = new RateLimitMiddleware();
            var cacheMiddleware = new CacheMiddleware();
            var loggingMiddleware = new LoggingMiddleware();

            loggingMiddleware.SetNext(authMiddleware);
            authMiddleware.SetNext(rateLimitMiddleware);
            rateLimitMiddleware.SetNext(cacheMiddleware);

            // 模拟不同的HTTP请求
            var requests = new[]
            {
                new HttpRequest("/api/users", "GET", "valid-token"),
                new HttpRequest("/api/users", "POST", "invalid-token"),
                new HttpRequest("/api/products", "GET", "valid-token"),
                new HttpRequest("/api/orders", "GET", "valid-token"),
                new HttpRequest("/api/admin", "DELETE", "valid-token")
            };

            foreach (var request in requests)
            {
                Console.WriteLine($"\n处理HTTP请求: {request.Method} {request.Path}");
                var response = loggingMiddleware.Handle(request);
                Console.WriteLine($"响应: {response}");
            }
        }
    }
}