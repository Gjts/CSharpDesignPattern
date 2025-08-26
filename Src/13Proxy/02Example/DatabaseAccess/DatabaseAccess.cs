namespace _Proxy._02Example.DatabaseAccess
{
    // 主题接口
    public interface IDatabase
    {
        void Connect();
        string Query(string sql);
        void Disconnect();
    }

    // 真实主题 - 真实数据库
    public class RealDatabase : IDatabase
    {
        private string connectionString;
        private bool isConnected = false;

        public RealDatabase(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Connect()
        {
            Console.WriteLine($"  建立数据库连接: {connectionString}");
            System.Threading.Thread.Sleep(1000); // 模拟连接延迟
            isConnected = true;
            Console.WriteLine("  数据库连接成功");
        }

        public string Query(string sql)
        {
            if (!isConnected)
            {
                throw new InvalidOperationException("数据库未连接");
            }
            Console.WriteLine($"  执行SQL查询: {sql}");
            System.Threading.Thread.Sleep(500); // 模拟查询延迟
            return $"查询结果: {new Random().Next(1, 100)} 条记录";
        }

        public void Disconnect()
        {
            if (isConnected)
            {
                Console.WriteLine("  断开数据库连接");
                isConnected = false;
            }
        }
    }

    // 代理类 - 数据库代理（带缓存和权限控制）
    public class DatabaseProxy : IDatabase
    {
        private RealDatabase realDatabase;
        private Dictionary<string, string> cache = new Dictionary<string, string>();
        private string userRole;
        private string userName;
        private string connectionString;

        public DatabaseProxy(string connectionString, string userRole)
        {
            this.connectionString = connectionString;
            this.userRole = userRole;
        }

        // 无参构造函数（为了兼容Program.cs）
        public DatabaseProxy()
        {
            this.connectionString = "Server=localhost;Database=TestDB";
        }

        // 设置用户（为了兼容Program.cs）
        public void SetUser(string userName, string role)
        {
            this.userName = userName;
            this.userRole = role;
            Console.WriteLine($"  [代理] 设置用户: {userName} (角色: {role})");
        }

        public void Connect()
        {
            if (realDatabase == null)
            {
                Console.WriteLine("  [代理] 延迟初始化数据库连接");
                realDatabase = new RealDatabase(connectionString);
                realDatabase.Connect();
            }
            else
            {
                Console.WriteLine("  [代理] 使用已有连接");
            }
        }

        public string Query(string sql)
        {
            Console.WriteLine($"  [代理] 执行查询: {sql}");
            
            // 权限检查
            if (!CheckAccess(sql))
            {
                return "访问被拒绝: 权限不足";
            }

            // 检查缓存
            if (cache.ContainsKey(sql))
            {
                Console.WriteLine($"  [代理] 从缓存返回结果: {sql}");
                return cache[sql];
            }

            // 确保连接
            Connect();

            // 执行查询
            string result = realDatabase.Query(sql);
            
            // 缓存结果
            cache[sql] = result;
            Console.WriteLine($"  [代理] 缓存查询结果");
            
            return result;
        }

        // Execute方法（为了兼容Program.cs）
        public string Execute(string sql)
        {
            Console.WriteLine($"  [代理] 执行命令: {sql}");
            
            // 权限检查
            if (!CheckAccess(sql))
            {
                Console.WriteLine($"  [代理] ❌ 权限拒绝: 用户 '{userRole}' 不能执行此操作");
                return "访问被拒绝: 权限不足";
            }

            // 确保连接
            Connect();

            // 执行命令
            Console.WriteLine($"  [代理] ✅ 命令执行成功");
            return "命令执行成功";
        }

        public void Disconnect()
        {
            if (realDatabase != null)
            {
                realDatabase.Disconnect();
                Console.WriteLine("  [代理] 清理缓存");
                cache.Clear();
            }
        }

        private bool CheckAccess(string sql)
        {
            Console.WriteLine($"  [代理] 检查用户权限: {userRole}");
            
            string sqlUpper = sql.ToUpper();
            
            if (userRole == "Admin")
            {
                return true;
            }
            
            if (userRole == "User")
            {
                // User不能执行UPDATE、DELETE、DROP操作
                if (sqlUpper.Contains("UPDATE") || sqlUpper.Contains("DELETE") || sqlUpper.Contains("DROP"))
                {
                    return false;
                }
                return true;
            }
            
            if (userRole == "Guest")
            {
                // Guest只能执行SELECT查询
                if (!sqlUpper.Contains("SELECT"))
                {
                    return false;
                }
                return true;
            }
            
            return false;
        }
    }

    // 虚拟代理 - 延迟加载图片
    public interface IImage
    {
        void Display();
    }

    public class RealImage : IImage
    {
        private string fileName;

        public RealImage(string fileName)
        {
            this.fileName = fileName;
            LoadFromDisk();
        }

        private void LoadFromDisk()
        {
            Console.WriteLine($"  加载图片: {fileName}");
            System.Threading.Thread.Sleep(1000); // 模拟加载延迟
        }

        public void Display()
        {
            Console.WriteLine($"  显示图片: {fileName}");
        }
    }
}