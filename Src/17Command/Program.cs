namespace _17Command
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 命令模式 (Command Pattern) ===\n");

            // 示例1：文本编辑器撤销重做功能
            Console.WriteLine("示例1：文本编辑器撤销重做功能");
            Console.WriteLine("------------------------");
            RunTextEditorExample();

            Console.WriteLine("\n示例2：智能家居控制系统");
            Console.WriteLine("------------------------");
            RunSmartHomeExample();

            Console.WriteLine("\n示例3：任务队列系统");
            Console.WriteLine("------------------------");
            RunTaskQueueExample();
        }

        static void RunTextEditorExample()
        {
            var editor = new TextEditor();
            var history = new CommandHistory();

            // 执行一系列编辑操作
            var commands = new ICommand[]
            {
                new WriteCommand(editor, "Hello "),
                new WriteCommand(editor, "World!"),
                new DeleteCommand(editor, 6),
                new WriteCommand(editor, "C# "),
                new ReplaceCommand(editor, "World", "Universe")
            };

            foreach (var command in commands)
            {
                history.ExecuteCommand(command);
                Console.WriteLine($"文档内容: {editor.GetContent()}");
            }

            // 撤销操作
            Console.WriteLine("\n执行撤销操作:");
            for (int i = 0; i < 3; i++)
            {
                history.Undo();
                Console.WriteLine($"文档内容: {editor.GetContent()}");
            }

            // 重做操作
            Console.WriteLine("\n执行重做操作:");
            for (int i = 0; i < 2; i++)
            {
                history.Redo();
                Console.WriteLine($"文档内容: {editor.GetContent()}");
            }
        }

        static void RunSmartHomeExample()
        {
            // 创建智能设备
            var livingRoomLight = new Light("客厅灯");
            var bedroomLight = new Light("卧室灯");
            var airConditioner = new AirConditioner("空调");
            var tv = new Television("电视");

            // 创建遥控器
            var remote = new RemoteControl();

            // 设置按钮命令
            remote.SetCommand(0, new LightOnCommand(livingRoomLight), new LightOffCommand(livingRoomLight));
            remote.SetCommand(1, new LightOnCommand(bedroomLight), new LightOffCommand(bedroomLight));
            remote.SetCommand(2, new AirConditionerOnCommand(airConditioner, 25), new AirConditionerOffCommand(airConditioner));
            remote.SetCommand(3, new TVOnCommand(tv, 10), new TVOffCommand(tv));

            // 创建宏命令（一键场景）
            var movieMode = new MacroCommand(new ICommand[]
            {
                new LightOffCommand(livingRoomLight),
                new TVOnCommand(tv, 15),
                new AirConditionerOnCommand(airConditioner, 23)
            });
            remote.SetCommand(4, movieMode, new NoCommand());

            // 测试遥控器
            Console.WriteLine("测试智能家居控制:");
            remote.OnButtonPressed(0);  // 开客厅灯
            remote.OnButtonPressed(2);  // 开空调
            remote.OnButtonPressed(3);  // 开电视
            
            Console.WriteLine("\n启动电影模式:");
            remote.OnButtonPressed(4);  // 电影模式
            
            Console.WriteLine("\n撤销最后操作:");
            remote.UndoButtonPressed();
            
            Console.WriteLine("\n关闭所有设备:");
            remote.OffButtonPressed(3); // 关电视
            remote.OffButtonPressed(2); // 关空调
            remote.OffButtonPressed(0); // 关客厅灯
        }

        static void RunTaskQueueExample()
        {
            var taskQueue = new TaskQueue();
            var database = new Database();
            var emailService = new EmailService();
            var fileSystem = new FileSystem();

            // 添加任务到队列
            taskQueue.AddTask(new DatabaseBackupCommand(database));
            taskQueue.AddTask(new EmailSendCommand(emailService, "admin@example.com", "系统报告", "每日报告内容"));
            taskQueue.AddTask(new FileCleanupCommand(fileSystem, "/temp"));
            taskQueue.AddTask(new DatabaseOptimizeCommand(database));
            taskQueue.AddTask(new EmailSendCommand(emailService, "user@example.com", "欢迎", "欢迎使用我们的系统"));

            // 执行任务队列
            Console.WriteLine("开始执行任务队列:");
            taskQueue.ProcessTasks();

            Console.WriteLine("\n添加优先任务:");
            taskQueue.AddPriorityTask(new SystemAlertCommand("系统即将维护"));
            taskQueue.ProcessTasks();
        }
    }
}