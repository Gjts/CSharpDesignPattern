namespace _16Respon
{
    // HTTP请求
    public class HttpRequest
    {
        public string Path { get; set; }
        public string Method { get; set; }
        public string Token { get; set; }
        public Dictionary<string, object> Data { get; set; }

        public HttpRequest(string path, string method, string token)
        {
            Path = path;
            Method = method;
            Token = token;
            Data = new Dictionary<string, object>();
        }
    }

    // HTTP响应
    public class HttpResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }

        public HttpResponse(int statusCode, string message, object? data = null)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public override string ToString()
        {
            return $"[{StatusCode}] {Message}";
        }
    }

    // 抽象中间件
    public abstract class Middleware
    {
        protected Middleware? _next;
        protected string _name;

        protected Middleware(string name)
        {
            _name = name;
        }

        public void SetNext(Middleware next)
        {
            _next = next;
        }

        public virtual HttpResponse Handle(HttpRequest request)
        {
            Console.WriteLine($"  [{_name}] 处理请求...");
            
            // 预处理
            var canProceed = PreProcess(request);
            if (!canProceed.Item1)
            {
                return canProceed.Item2!;
            }

            // 传递给下一个中间件
            HttpResponse response;
            if (_next != null)
            {
                response = _next.Handle(request);
            }
            else
            {
                // 最终处理器
                response = new HttpResponse(200, "请求处理成功", new { path = request.Path, method = request.Method });
            }

            // 后处理
            PostProcess(request, response);

            return response;
        }

        protected abstract (bool, HttpResponse?) PreProcess(HttpRequest request);
        protected virtual void PostProcess(HttpRequest request, HttpResponse response) { }
    }

    // 日志中间件
    public class LoggingMiddleware : Middleware
    {
        private static int _requestId = 0;

        public LoggingMiddleware() : base("日志中间件")
        {
        }

        protected override (bool, HttpResponse?) PreProcess(HttpRequest request)
        {
            _requestId++;
            request.Data["RequestId"] = _requestId;
            Console.WriteLine($"    记录请求 #{_requestId}: {request.Method} {request.Path}");
            Console.WriteLine($"    时间: {DateTime.Now:HH:mm:ss.fff}");
            return (true, null);
        }

        protected override void PostProcess(HttpRequest request, HttpResponse response)
        {
            Console.WriteLine($"    记录响应 #{request.Data["RequestId"]}: {response.StatusCode}");
        }
    }

    // 认证中间件
    public class AuthenticationMiddleware : Middleware
    {
        public AuthenticationMiddleware() : base("认证中间件")
        {
        }

        protected override (bool, HttpResponse?) PreProcess(HttpRequest request)
        {
            Console.WriteLine($"    验证Token: {request.Token}");
            
            if (string.IsNullOrEmpty(request.Token))
            {
                return (false, new HttpResponse(401, "未提供认证令牌"));
            }

            if (request.Token != "valid-token")
            {
                return (false, new HttpResponse(401, "无效的认证令牌"));
            }

            Console.WriteLine($"    认证成功");
            request.Data["UserId"] = "user123";
            return (true, null);
        }
    }

    // 速率限制中间件
    public class RateLimitMiddleware : Middleware
    {
        private static Dictionary<string, List<DateTime>> _requestHistory = new();
        private const int MaxRequestsPerMinute = 3;

        public RateLimitMiddleware() : base("速率限制中间件")
        {
        }

        protected override (bool, HttpResponse?) PreProcess(HttpRequest request)
        {
            var userId = request.Data.ContainsKey("UserId") ? request.Data["UserId"].ToString()! : "anonymous";
            
            if (!_requestHistory.ContainsKey(userId))
            {
                _requestHistory[userId] = new List<DateTime>();
            }

            var now = DateTime.Now;
            var recentRequests = _requestHistory[userId].Where(t => (now - t).TotalMinutes < 1).ToList();
            
            Console.WriteLine($"    用户 {userId} 最近1分钟内的请求数: {recentRequests.Count}");

            if (recentRequests.Count >= MaxRequestsPerMinute)
            {
                return (false, new HttpResponse(429, $"请求过于频繁，请稍后再试"));
            }

            _requestHistory[userId] = recentRequests;
            _requestHistory[userId].Add(now);
            
            return (true, null);
        }
    }

    // 缓存中间件
    public class CacheMiddleware : Middleware
    {
        private static Dictionary<string, (HttpResponse, DateTime)> _cache = new();
        private const int CacheExpirationSeconds = 30;

        public CacheMiddleware() : base("缓存中间件")
        {
        }

        protected override (bool, HttpResponse?) PreProcess(HttpRequest request)
        {
            if (request.Method != "GET")
            {
                Console.WriteLine($"    非GET请求，跳过缓存");
                return (true, null);
            }

            var cacheKey = $"{request.Method}:{request.Path}";
            
            if (_cache.ContainsKey(cacheKey))
            {
                var (cachedResponse, cacheTime) = _cache[cacheKey];
                var age = (DateTime.Now - cacheTime).TotalSeconds;
                
                if (age < CacheExpirationSeconds)
                {
                    Console.WriteLine($"    命中缓存 (年龄: {age:F1}秒)");
                    return (false, new HttpResponse(200, $"[缓存] {cachedResponse.Message}", cachedResponse.Data));
                }
                else
                {
                    Console.WriteLine($"    缓存已过期");
                    _cache.Remove(cacheKey);
                }
            }
            else
            {
                Console.WriteLine($"    缓存未命中");
            }
            
            return (true, null);
        }

        protected override void PostProcess(HttpRequest request, HttpResponse response)
        {
            if (request.Method == "GET" && response.StatusCode == 200)
            {
                var cacheKey = $"{request.Method}:{request.Path}";
                _cache[cacheKey] = (response, DateTime.Now);
                Console.WriteLine($"    响应已缓存");
            }
        }
    }
}