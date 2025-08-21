namespace _12Flyweight._02Example._02ConnectionPool
{
    // 数据库连接配置（内部状态 - 共享）
    public class ConnectionConfig
    {
        public string DatabaseType { get; }
        public string Host { get; }
        public int Port { get; }
        public string DatabaseName { get; }
        public int MaxPoolSize { get; }
        public int ConnectionTimeout { get; }

        public ConnectionConfig(string dbType, string host, int port, 
            string dbName, int maxPoolSize = 10, int timeout = 30)
        {
            DatabaseType = dbType;
            Host = host;
            Port = port;
            DatabaseName = dbName;
            MaxPoolSize = maxPoolSize;
            ConnectionTimeout = timeout;
        }

        public string GetConnectionString()
        {
            return $"{DatabaseType}://{Host}:{Port}/{DatabaseName}?timeout={ConnectionTimeout}";
        }
    }

    // 数据库连接（享元对象）
    public class DatabaseConnection
    {
        private static int connectionIdCounter = 0;
        
        public int ConnectionId { get; }
        public ConnectionConfig Config { get; }  // 共享的配置
        public bool InUse { get; set; }         // 外部状态
        public string? CurrentUser { get; set; }  // 外部状态
        public DateTime LastUsedTime { get; set; } // 外部状态
        public DateTime CreatedTime { get; }

        public DatabaseConnection(ConnectionConfig config)
        {
            ConnectionId = ++connectionIdCounter;
            Config = config;
            InUse = false;
            CreatedTime = DateTime.Now;
            LastUsedTime = DateTime.Now;
            
            Console.WriteLine($"[连接创建] 连接 #{ConnectionId} 已创建 ({Config.GetConnectionString()})");
        }

        public void Open(string user)
        {
            if (InUse)
            {
                throw new InvalidOperationException($"连接 #{ConnectionId} 已被使用");
            }

            InUse = true;
            CurrentUser = user;
            LastUsedTime = DateTime.Now;
            Console.WriteLine($"[连接打开] 连接 #{ConnectionId} 被 {user} 获取");
        }

        public void Close()
        {
            if (!InUse)
            {
                return;
            }

            Console.WriteLine($"[连接关闭] 连接 #{ConnectionId} 被 {CurrentUser} 释放");
            InUse = false;
            CurrentUser = null;
            LastUsedTime = DateTime.Now;
        }

        public void ExecuteQuery(string query)
        {
            if (!InUse)
            {
                throw new InvalidOperationException("连接未打开");
            }

            Console.WriteLine($"[执行查询] 连接 #{ConnectionId} - 用户: {CurrentUser}");
            Console.WriteLine($"  SQL: {query}");
            
            // 模拟查询执行
            System.Threading.Thread.Sleep(100);
            
            Console.WriteLine($"  ✓ 查询执行成功");
        }

        public bool IsExpired(int maxIdleMinutes = 5)
        {
            return !InUse && (DateTime.Now - LastUsedTime).TotalMinutes > maxIdleMinutes;
        }
    }

    // 连接池（享元工厂）
    public class ConnectionPool
    {
        private Dictionary<string, List<DatabaseConnection>> pools;
        private Dictionary<string, ConnectionConfig> configs;
        private Dictionary<string, object> poolLocks;
        private int maxConnectionsPerPool;

        public ConnectionPool(int maxConnectionsPerPool = 10)
        {
            pools = new Dictionary<string, List<DatabaseConnection>>();
            configs = new Dictionary<string, ConnectionConfig>();
            poolLocks = new Dictionary<string, object>();
            this.maxConnectionsPerPool = maxConnectionsPerPool;
        }

        public void RegisterDatabase(string poolName, ConnectionConfig config)
        {
            if (!configs.ContainsKey(poolName))
            {
                configs[poolName] = config;
                pools[poolName] = new List<DatabaseConnection>();
                poolLocks[poolName] = new object();
                
                Console.WriteLine($"[连接池] 注册数据库池: {poolName}");
                Console.WriteLine($"  配置: {config.GetConnectionString()}");
                Console.WriteLine($"  最大连接数: {config.MaxPoolSize}");
                
                // 预创建最小连接数
                int minConnections = Math.Min(2, config.MaxPoolSize);
                for (int i = 0; i < minConnections; i++)
                {
                    pools[poolName].Add(new DatabaseConnection(config));
                }
            }
        }

        public DatabaseConnection GetConnection(string poolName, string user)
        {
            if (!pools.ContainsKey(poolName))
            {
                throw new ArgumentException($"连接池 {poolName} 不存在");
            }

            lock (poolLocks[poolName])
            {
                var pool = pools[poolName];
                var config = configs[poolName];

                // 查找可用连接
                var availableConnection = pool.FirstOrDefault(c => !c.InUse);
                
                if (availableConnection != null)
                {
                    availableConnection.Open(user);
                    Console.WriteLine($"[连接池] 复用现有连接 #{availableConnection.ConnectionId}");
                    return availableConnection;
                }

                // 如果没有可用连接且未达到上限，创建新连接
                if (pool.Count < config.MaxPoolSize)
                {
                    var newConnection = new DatabaseConnection(config);
                    pool.Add(newConnection);
                    newConnection.Open(user);
                    Console.WriteLine($"[连接池] 创建新连接 #{newConnection.ConnectionId}");
                    return newConnection;
                }

                // 连接池已满
                Console.WriteLine($"[连接池] 警告: {poolName} 连接池已满，等待可用连接...");
                
                // 简单等待重试（实际应用中应该使用更复杂的等待机制）
                System.Threading.Thread.Sleep(1000);
                availableConnection = pool.FirstOrDefault(c => !c.InUse);
                if (availableConnection != null)
                {
                    availableConnection.Open(user);
                    return availableConnection;
                }

                throw new InvalidOperationException($"连接池 {poolName} 无可用连接");
            }
        }

        public void ReleaseConnection(DatabaseConnection connection)
        {
            connection.Close();
        }

        public void CleanupExpiredConnections(string poolName)
        {
            if (!pools.ContainsKey(poolName))
            {
                return;
            }

            lock (poolLocks[poolName])
            {
                var pool = pools[poolName];
                var expiredConnections = pool.Where(c => c.IsExpired()).ToList();
                
                foreach (var conn in expiredConnections)
                {
                    pool.Remove(conn);
                    Console.WriteLine($"[连接池] 清理过期连接 #{conn.ConnectionId}");
                }

                if (expiredConnections.Count > 0)
                {
                    Console.WriteLine($"[连接池] 已清理 {expiredConnections.Count} 个过期连接");
                }
            }
        }

        public void ShowPoolStatistics()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("连接池统计信息");
            Console.WriteLine(new string('=', 60));

            foreach (var poolName in pools.Keys)
            {
                var pool = pools[poolName];
                var config = configs[poolName];
                var inUseCount = pool.Count(c => c.InUse);
                var availableCount = pool.Count(c => !c.InUse);

                Console.WriteLine($"\n池名称: {poolName}");
                Console.WriteLine($"  连接字符串: {config.GetConnectionString()}");
                Console.WriteLine($"  总连接数: {pool.Count}/{config.MaxPoolSize}");
                Console.WriteLine($"  使用中: {inUseCount}");
                Console.WriteLine($"  空闲: {availableCount}");
                Console.WriteLine($"  使用率: {(pool.Count > 0 ? (double)inUseCount / pool.Count : 0):P1}");
                
                if (pool.Count > 0)
                {
                    Console.WriteLine($"  连接详情:");
                    foreach (var conn in pool)
                    {
                        string status = conn.InUse ? $"使用中 ({conn.CurrentUser})" : "空闲";
                        Console.WriteLine($"    - 连接 #{conn.ConnectionId}: {status}, " +
                            $"最后使用: {conn.LastUsedTime:HH:mm:ss}");
                    }
                }
            }
            
            Console.WriteLine(new string('=', 60));
        }
    }

    // 数据库操作管理器
    public class DatabaseManager
    {
        private ConnectionPool connectionPool;
        private Dictionary<string, DatabaseConnection> userConnections;

        public DatabaseManager()
        {
            connectionPool = new ConnectionPool(maxConnectionsPerPool: 5);
            userConnections = new Dictionary<string, DatabaseConnection>();
            InitializeDatabases();
        }

        private void InitializeDatabases()
        {
            // 注册主数据库
            connectionPool.RegisterDatabase("主库", new ConnectionConfig(
                "MySQL", "192.168.1.100", 3306, "main_db", 10, 30
            ));

            // 注册从库（读库）
            connectionPool.RegisterDatabase("从库", new ConnectionConfig(
                "MySQL", "192.168.1.101", 3306, "slave_db", 20, 30
            ));

            // 注册缓存数据库
            connectionPool.RegisterDatabase("缓存", new ConnectionConfig(
                "Redis", "192.168.1.200", 6379, "cache", 5, 10
            ));
        }

        public void ExecuteQuery(string user, string poolName, string query)
        {
            try
            {
                Console.WriteLine($"\n[数据库管理] 用户 {user} 请求执行查询");
                
                // 获取连接
                var connection = connectionPool.GetConnection(poolName, user);
                userConnections[user] = connection;
                
                // 执行查询
                connection.ExecuteQuery(query);
                
                // 释放连接
                connectionPool.ReleaseConnection(connection);
                userConnections.Remove(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[错误] {ex.Message}");
            }
        }

        public void SimulateMultipleUsers()
        {
            Console.WriteLine("\n模拟多用户并发访问:");
            Console.WriteLine(new string('-', 60));

            var tasks = new List<System.Threading.Tasks.Task>();
            string[] users = { "user1", "user2", "user3", "user4", "user5" };
            Random random = new Random();

            foreach (var user in users)
            {
                var task = System.Threading.Tasks.Task.Run(() =>
                {
                    string[] pools = { "主库", "从库", "缓存" };
                    string pool = pools[random.Next(pools.Length)];
                    
                    string query = pool == "缓存" 
                        ? $"GET key_{random.Next(100)}"
                        : $"SELECT * FROM table_{random.Next(10)} WHERE id = {random.Next(1000)}";
                    
                    ExecuteQuery(user, pool, query);
                    
                    // 模拟用户操作间隔
                    System.Threading.Thread.Sleep(random.Next(100, 500));
                    
                    // 再执行一次查询
                    ExecuteQuery(user, pool, $"SELECT COUNT(*) FROM table_{random.Next(10)}");
                });
                tasks.Add(task);
            }

            System.Threading.Tasks.Task.WaitAll(tasks.ToArray());
        }

        public void ShowStatistics()
        {
            connectionPool.ShowPoolStatistics();
        }

        public void Cleanup()
        {
            Console.WriteLine("\n执行连接池清理...");
            connectionPool.CleanupExpiredConnections("主库");
            connectionPool.CleanupExpiredConnections("从库");
            connectionPool.CleanupExpiredConnections("缓存");
        }
    }
}
