namespace _17Command
{
    // 任务队列系统的接收者们
    public class Database
    {
        public void Backup()
        {
            Console.WriteLine("  执行数据库备份...");
            System.Threading.Thread.Sleep(500); // 模拟耗时操作
            Console.WriteLine("    数据库备份完成");
        }

        public void Optimize()
        {
            Console.WriteLine("  执行数据库优化...");
            System.Threading.Thread.Sleep(300);
            Console.WriteLine("    数据库优化完成");
        }
    }

    public class EmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"  发送邮件到: {to}");
            Console.WriteLine($"    主题: {subject}");
            Console.WriteLine($"    内容: {body}");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("    邮件发送成功");
        }
    }

    public class FileSystem
    {
        public void CleanupDirectory(string path)
        {
            Console.WriteLine($"  清理目录: {path}");
            System.Threading.Thread.Sleep(400);
            Console.WriteLine($"    已清理 15 个临时文件");
        }
    }

    // 任务命令基类
    public abstract class TaskCommand : ICommand
    {
        protected DateTime _executionTime;
        protected string _taskName;

        protected TaskCommand(string taskName)
        {
            _taskName = taskName;
        }

        public abstract void Execute();

        public virtual void Undo()
        {
            Console.WriteLine($"  撤销任务: {_taskName}");
        }

        protected void LogExecution()
        {
            _executionTime = DateTime.Now;
            Console.WriteLine($"\n[{_executionTime:HH:mm:ss}] 执行任务: {_taskName}");
        }
    }

    // 数据库备份命令
    public class DatabaseBackupCommand : TaskCommand
    {
        private Database _database;

        public DatabaseBackupCommand(Database database) : base("数据库备份")
        {
            _database = database;
        }

        public override void Execute()
        {
            LogExecution();
            _database.Backup();
        }
    }

    // 数据库优化命令
    public class DatabaseOptimizeCommand : TaskCommand
    {
        private Database _database;

        public DatabaseOptimizeCommand(Database database) : base("数据库优化")
        {
            _database = database;
        }

        public override void Execute()
        {
            LogExecution();
            _database.Optimize();
        }
    }

    // 发送邮件命令
    public class EmailSendCommand : TaskCommand
    {
        private EmailService _emailService;
        private string _to;
        private string _subject;
        private string _body;

        public EmailSendCommand(EmailService emailService, string to, string subject, string body) 
            : base($"发送邮件到 {to}")
        {
            _emailService = emailService;
            _to = to;
            _subject = subject;
            _body = body;
        }

        public override void Execute()
        {
            LogExecution();
            _emailService.SendEmail(_to, _subject, _body);
        }
    }

    // 文件清理命令
    public class FileCleanupCommand : TaskCommand
    {
        private FileSystem _fileSystem;
        private string _path;

        public FileCleanupCommand(FileSystem fileSystem, string path) 
            : base($"清理目录 {path}")
        {
            _fileSystem = fileSystem;
            _path = path;
        }

        public override void Execute()
        {
            LogExecution();
            _fileSystem.CleanupDirectory(_path);
        }
    }

    // 系统警告命令
    public class SystemAlertCommand : TaskCommand
    {
        private string _message;

        public SystemAlertCommand(string message) : base("系统警告")
        {
            _message = message;
        }

        public override void Execute()
        {
            LogExecution();
            Console.WriteLine($"  ⚠️ 系统警告: {_message}");
            Console.WriteLine("  已通知所有在线用户");
        }
    }

    // 任务队列管理器
    public class TaskQueue
    {
        private Queue<ICommand> _taskQueue = new Queue<ICommand>();
        private Queue<ICommand> _priorityQueue = new Queue<ICommand>();
        private List<ICommand> _executedTasks = new List<ICommand>();

        public void AddTask(ICommand task)
        {
            _taskQueue.Enqueue(task);
            Console.WriteLine($"  任务已添加到队列 (当前队列长度: {_taskQueue.Count})");
        }

        public void AddPriorityTask(ICommand task)
        {
            _priorityQueue.Enqueue(task);
            Console.WriteLine($"  优先任务已添加 (优先队列长度: {_priorityQueue.Count})");
        }

        public void ProcessTasks()
        {
            // 先处理优先任务
            while (_priorityQueue.Count > 0)
            {
                var task = _priorityQueue.Dequeue();
                task.Execute();
                _executedTasks.Add(task);
            }

            // 再处理普通任务
            while (_taskQueue.Count > 0)
            {
                var task = _taskQueue.Dequeue();
                task.Execute();
                _executedTasks.Add(task);
            }

            if (_executedTasks.Count > 0)
            {
                Console.WriteLine($"\n所有任务执行完成 (共执行 {_executedTasks.Count} 个任务)");
            }
            else
            {
                Console.WriteLine("\n任务队列为空");
            }
        }

        public void UndoLastTask()
        {
            if (_executedTasks.Count > 0)
            {
                var lastTask = _executedTasks[_executedTasks.Count - 1];
                lastTask.Undo();
                _executedTasks.RemoveAt(_executedTasks.Count - 1);
            }
            else
            {
                Console.WriteLine("没有可撤销的任务");
            }
        }

        public int GetQueueLength()
        {
            return _taskQueue.Count + _priorityQueue.Count;
        }

        public int GetExecutedTaskCount()
        {
            return _executedTasks.Count;
        }
    }
}