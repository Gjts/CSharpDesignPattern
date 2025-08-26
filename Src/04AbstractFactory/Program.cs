using _AbstractFactory._02Example.Storage;

namespace _04AbstractFactory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 抽象工厂模式 (Abstract Factory Pattern) ================================");
            Console.WriteLine("适用场景：当需要创建一系列相关或相互依赖的对象，且这些对象属于不同的产品族时");
            Console.WriteLine("特点：提供一个创建一系列相关对象的接口，而无需指定它们的具体类");
            Console.WriteLine("优点：保证同一产品族的对象一起使用；易于切换产品族；符合开闭原则\n");

            Console.WriteLine("-------------------------------- UI主题系统 ----------------------------------");
            
            IUIFactory uiFactory;
            
            // 亮色主题
            Console.WriteLine("1. 亮色主题UI组件：");
            uiFactory = new LightThemeFactory();
            var lightButton = uiFactory.CreateButton();
            var lightTextBox = uiFactory.CreateTextBox();
            var lightCheckBox = uiFactory.CreateCheckBox();
            lightButton.Render();
            lightTextBox.Render();
            lightCheckBox.Render();
            
            // 暗色主题
            Console.WriteLine("\n2. 暗色主题UI组件：");
            uiFactory = new DarkThemeFactory();
            var darkButton = uiFactory.CreateButton();
            var darkTextBox = uiFactory.CreateTextBox();
            var darkCheckBox = uiFactory.CreateCheckBox();
            darkButton.Render();
            darkTextBox.Render();
            darkCheckBox.Render();

            Console.WriteLine("\n-------------------------------- 数据库访问层 ----------------------------------");
            
            IDatabaseFactory dbFactory;
            
            // MySQL数据库
            Console.WriteLine("1. MySQL数据库操作：");
            dbFactory = new MySQLFactory();
            var mysqlConnection = dbFactory.CreateConnection();
            var mysqlCommand = dbFactory.CreateCommand();
            var mysqlTransaction = dbFactory.CreateTransaction();
            mysqlConnection.Connect();
            mysqlCommand.Execute("SELECT * FROM users");
            mysqlTransaction.Begin();
            mysqlTransaction.Commit();
            
            // PostgreSQL数据库
            Console.WriteLine("\n2. PostgreSQL数据库操作：");
            dbFactory = new PostgreSQLFactory();
            var pgConnection = dbFactory.CreateConnection();
            var pgCommand = dbFactory.CreateCommand();
            var pgTransaction = dbFactory.CreateTransaction();
            pgConnection.Connect();
            pgCommand.Execute("SELECT * FROM products");
            pgTransaction.Begin();
            pgTransaction.Commit();
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 每个工厂创建一整套相关的对象（产品族）");
            Console.WriteLine("- 切换产品族只需更换工厂实例，客户端代码无需修改");
            Console.WriteLine("- 确保同一产品族的对象能够协同工作");
        }
    }
}
