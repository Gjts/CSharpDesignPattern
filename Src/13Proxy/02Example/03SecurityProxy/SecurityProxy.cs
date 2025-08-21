namespace _13Proxy._02Example._03SecurityProxy
{
    // 用户角色枚举
    public enum UserRole
    {
        Guest,
        User,
        Admin,
        SuperAdmin
    }

    // 用户上下文
    public class UserContext
    {
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public UserRole Role { get; set; }
        public required List<string> Permissions { get; set; }
        public DateTime LoginTime { get; set; }

        public UserContext()
        {
            Permissions = new List<string>();
        }
    }

    // 文档接口
    public interface IDocument
    {
        string Read();
        void Write(string content);
        void Delete();
        string GetMetadata();
    }

    // 真实文档类
    public class Document : IDocument
    {
        private string id;
        private string title;
        private string? content;
        private string author;
        private DateTime createdDate;
        private DateTime lastModified;

        public Document(string id, string title, string content, string author)
        {
            this.id = id;
            this.title = title;
            this.content = content;
            this.author = author;
            this.createdDate = DateTime.Now;
            this.lastModified = DateTime.Now;
        }

        public string Read()
        {
            Console.WriteLine($"[文档] 读取文档: {title}");
            return content ?? string.Empty;
        }

        public void Write(string newContent)
        {
            Console.WriteLine($"[文档] 修改文档: {title}");
            content = newContent;
            lastModified = DateTime.Now;
            Console.WriteLine($"  ✓ 文档已更新");
        }

        public void Delete()
        {
            Console.WriteLine($"[文档] 删除文档: {title}");
            content = null;
            Console.WriteLine($"  ✓ 文档已删除");
        }

        public string GetMetadata()
        {
            return $"ID: {id}, 标题: {title}, 作者: {author}, " +
                   $"创建时间: {createdDate:yyyy-MM-dd}, 最后修改: {lastModified:yyyy-MM-dd HH:mm:ss}";
        }
    }

    // 安全代理类
    public class SecureDocumentProxy : IDocument
    {
        private Document document;
        private UserContext currentUser;
        private Dictionary<string, List<UserRole>> accessControl = new();
        private List<string> auditLog;

        public SecureDocumentProxy(Document document, UserContext user)
        {
            this.document = document;
            this.currentUser = user;
            this.auditLog = new List<string>();
            InitializeAccessControl();
        }

        private void InitializeAccessControl()
        {
            accessControl = new Dictionary<string, List<UserRole>>
            {
                ["Read"] = new List<UserRole> { UserRole.Guest, UserRole.User, UserRole.Admin, UserRole.SuperAdmin },
                ["Write"] = new List<UserRole> { UserRole.User, UserRole.Admin, UserRole.SuperAdmin },
                ["Delete"] = new List<UserRole> { UserRole.Admin, UserRole.SuperAdmin },
                ["GetMetadata"] = new List<UserRole> { UserRole.Guest, UserRole.User, UserRole.Admin, UserRole.SuperAdmin }
            };
        }

        private bool CheckPermission(string operation)
        {
            Console.WriteLine($"\n[安全代理] 检查权限: {operation}");
            Console.WriteLine($"  用户: {currentUser.UserName} (角色: {currentUser.Role})");
            
            if (!accessControl.ContainsKey(operation))
            {
                Console.WriteLine($"  ✗ 未知操作: {operation}");
                return false;
            }

            bool hasPermission = accessControl[operation].Contains(currentUser.Role);
            
            if (hasPermission)
            {
                Console.WriteLine($"  ✓ 权限验证通过");
            }
            else
            {
                Console.WriteLine($"  ✗ 权限不足");
            }
            
            // 记录审计日志
            LogAccess(operation, hasPermission);
            
            return hasPermission;
        }

        private void LogAccess(string operation, bool success)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | " +
                            $"用户: {currentUser.UserName} | " +
                            $"操作: {operation} | " +
                            $"结果: {(success ? "成功" : "拒绝")}";
            auditLog.Add(logEntry);
        }

        public string Read()
        {
            if (CheckPermission("Read"))
            {
                return document.Read();
            }
            else
            {
                throw new UnauthorizedAccessException($"用户 {currentUser.UserName} 无权读取文档");
            }
        }

        public void Write(string content)
        {
            if (CheckPermission("Write"))
            {
                // 添加额外的内容验证
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("[安全代理] 拒绝写入空内容");
                    throw new ArgumentException("内容不能为空");
                }
                
                if (content.Length > 10000)
                {
                    Console.WriteLine("[安全代理] 内容超过长度限制");
                    throw new ArgumentException("内容超过最大长度限制(10000字符)");
                }
                
                document.Write(content);
            }
            else
            {
                throw new UnauthorizedAccessException($"用户 {currentUser.UserName} 无权修改文档");
            }
        }

        public void Delete()
        {
            if (CheckPermission("Delete"))
            {
                // 二次确认（仅对管理员）
                if (currentUser.Role == UserRole.Admin)
                {
                    Console.WriteLine("[安全代理] 删除操作需要二次确认");
                    Console.WriteLine("  模拟二次确认: 确认删除");
                }
                
                document.Delete();
            }
            else
            {
                throw new UnauthorizedAccessException($"用户 {currentUser.UserName} 无权删除文档");
            }
        }

        public string GetMetadata()
        {
            if (CheckPermission("GetMetadata"))
            {
                return document.GetMetadata();
            }
            else
            {
                throw new UnauthorizedAccessException($"用户 {currentUser.UserName} 无权查看文档元数据");
            }
        }

        public void ShowAuditLog()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("审计日志");
            Console.WriteLine(new string('=', 60));
            foreach (var log in auditLog)
            {
                Console.WriteLine(log);
            }
            Console.WriteLine(new string('=', 60));
        }
    }

    // 数据库访问接口
    public interface IDatabase
    {
        List<string> Query(string sql);
        int Execute(string sql);
        void BeginTransaction();
        void Commit();
        void Rollback();
    }

    // 真实数据库类
    public class Database : IDatabase
    {
        private bool inTransaction = false;
        private List<string> transactionLog = new List<string>();

        public List<string> Query(string sql)
        {
            Console.WriteLine($"[数据库] 执行查询: {sql}");
            
            // 模拟查询结果
            var results = new List<string>
            {
                "Record 1",
                "Record 2",
                "Record 3"
            };
            
            Console.WriteLine($"  返回 {results.Count} 条记录");
            return results;
        }

        public int Execute(string sql)
        {
            Console.WriteLine($"[数据库] 执行命令: {sql}");
            
            if (inTransaction)
            {
                transactionLog.Add(sql);
            }
            
            // 模拟影响的行数
            int affectedRows = new Random().Next(1, 10);
            Console.WriteLine($"  影响 {affectedRows} 行");
            return affectedRows;
        }

        public void BeginTransaction()
        {
            Console.WriteLine("[数据库] 开始事务");
            inTransaction = true;
            transactionLog.Clear();
        }

        public void Commit()
        {
            Console.WriteLine("[数据库] 提交事务");
            Console.WriteLine($"  执行了 {transactionLog.Count} 条语句");
            inTransaction = false;
            transactionLog.Clear();
        }

        public void Rollback()
        {
            Console.WriteLine("[数据库] 回滚事务");
            Console.WriteLine($"  撤销了 {transactionLog.Count} 条语句");
            inTransaction = false;
            transactionLog.Clear();
        }
    }

    // 数据库安全代理
    public class SecureDatabaseProxy : IDatabase
    {
        private Database database;
        private UserContext currentUser;
        private List<string> sqlWhitelist = new();
        private List<string> dangerousKeywords = new();

        public SecureDatabaseProxy(Database database, UserContext user)
        {
            this.database = database;
            this.currentUser = user;
            InitializeSecurity();
        }

        private void InitializeSecurity()
        {
            // SQL白名单（仅允许某些操作）
            sqlWhitelist = new List<string>
            {
                "SELECT",
                "INSERT",
                "UPDATE"
            };

            // 危险关键词
            dangerousKeywords = new List<string>
            {
                "DROP",
                "TRUNCATE",
                "DELETE FROM users",
                "UPDATE users SET role"
            };
        }

        private bool ValidateSQL(string sql)
        {
            Console.WriteLine($"[安全代理] SQL验证: {sql}");
            
            // 检查危险关键词
            foreach (var keyword in dangerousKeywords)
            {
                if (sql.ToUpper().Contains(keyword.ToUpper()))
                {
                    Console.WriteLine($"  ✗ 检测到危险操作: {keyword}");
                    return false;
                }
            }
            
            // 检查用户权限
            if (currentUser.Role == UserRole.Guest)
            {
                // 访客只能执行SELECT
                if (!sql.ToUpper().StartsWith("SELECT"))
                {
                    Console.WriteLine($"  ✗ 访客只能执行查询操作");
                    return false;
                }
            }
            
            Console.WriteLine($"  ✓ SQL验证通过");
            return true;
        }

        public List<string> Query(string sql)
        {
            if (!ValidateSQL(sql))
            {
                throw new SecurityException($"SQL语句被拒绝: {sql}");
            }
            
            // 添加行级安全过滤
            if (currentUser.Role == UserRole.User)
            {
                sql = AddRowLevelSecurity(sql);
            }
            
            return database.Query(sql);
        }

        private string AddRowLevelSecurity(string sql)
        {
            // 为普通用户添加行级安全过滤
            if (sql.ToUpper().Contains("FROM") && !sql.ToUpper().Contains("WHERE"))
            {
                sql += $" WHERE user_id = '{currentUser.UserId}'";
                Console.WriteLine($"[安全代理] 添加行级安全: WHERE user_id = '{currentUser.UserId}'");
            }
            return sql;
        }

        public int Execute(string sql)
        {
            if (!ValidateSQL(sql))
            {
                throw new SecurityException($"SQL语句被拒绝: {sql}");
            }
            
            // 只有管理员可以执行DDL语句
            if (sql.ToUpper().StartsWith("CREATE") || sql.ToUpper().StartsWith("ALTER"))
            {
                if (currentUser.Role != UserRole.Admin && currentUser.Role != UserRole.SuperAdmin)
                {
                    throw new UnauthorizedAccessException("只有管理员可以执行DDL语句");
                }
            }
            
            return database.Execute(sql);
        }

        public void BeginTransaction()
        {
            if (currentUser.Role == UserRole.Guest)
            {
                throw new UnauthorizedAccessException("访客不能开启事务");
            }
            
            Console.WriteLine($"[安全代理] 用户 {currentUser.UserName} 开启事务");
            database.BeginTransaction();
        }

        public void Commit()
        {
            Console.WriteLine($"[安全代理] 用户 {currentUser.UserName} 提交事务");
            database.Commit();
        }

        public void Rollback()
        {
            Console.WriteLine($"[安全代理] 用户 {currentUser.UserName} 回滚事务");
            database.Rollback();
        }
    }

    // 自定义异常
    public class SecurityException : Exception
    {
        public SecurityException(string message) : base(message) { }
    }
}
