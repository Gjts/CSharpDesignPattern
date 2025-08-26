namespace _AbstractFactory._02Example.DatabaseProvider
{
    // 抽象产品 - 数据库连接
    public interface IConnection
    {
        void Connect();
        void Disconnect();
    }

    // 抽象产品 - 数据库命令
    public interface ICommand
    {
        void Execute(string sql);
    }

    // 抽象产品 - 数据读取器
    public interface IDataReader
    {
        void Read();
    }

    // 抽象产品 - 事务
    public interface ITransaction
    {
        void Begin();
        void Commit();
        void Rollback();
    }

    // 抽象工厂 - 数据库提供者工厂
    public interface IDatabaseFactory
    {
        IConnection CreateConnection();
        ICommand CreateCommand();
        IDataReader CreateDataReader();
        ITransaction CreateTransaction();
    }

    // 具体产品 - MySQL连接
    public class MySQLConnection : IConnection
    {
        public void Connect()
        {
            Console.WriteLine("  建立 MySQL 数据库连接");
        }

        public void Disconnect()
        {
            Console.WriteLine("  断开 MySQL 数据库连接");
        }
    }

    // 具体产品 - MySQL命令
    public class MySQLCommand : ICommand
    {
        public void Execute(string sql)
        {
            Console.WriteLine($"  执行 MySQL 命令: {sql}");
        }
    }

    // 具体产品 - MySQL数据读取器
    public class MySQLDataReader : IDataReader
    {
        public void Read()
        {
            Console.WriteLine("  读取 MySQL 数据");
        }
    }

    // 具体产品 - MySQL事务
    public class MySQLTransaction : ITransaction
    {
        public void Begin()
        {
            Console.WriteLine("  开始 MySQL 事务");
        }

        public void Commit()
        {
            Console.WriteLine("  提交 MySQL 事务");
        }

        public void Rollback()
        {
            Console.WriteLine("  回滚 MySQL 事务");
        }
    }

    // 具体工厂 - MySQL数据库工厂
    public class MySQLFactory : IDatabaseFactory
    {
        public IConnection CreateConnection()
        {
            return new MySQLConnection();
        }

        public ICommand CreateCommand()
        {
            return new MySQLCommand();
        }

        public IDataReader CreateDataReader()
        {
            return new MySQLDataReader();
        }

        public ITransaction CreateTransaction()
        {
            return new MySQLTransaction();
        }
    }

    // 具体产品 - PostgreSQL连接
    public class PostgreSQLConnection : IConnection
    {
        public void Connect()
        {
            Console.WriteLine("  建立 PostgreSQL 数据库连接");
        }

        public void Disconnect()
        {
            Console.WriteLine("  断开 PostgreSQL 数据库连接");
        }
    }

    // 具体产品 - PostgreSQL命令
    public class PostgreSQLCommand : ICommand
    {
        public void Execute(string sql)
        {
            Console.WriteLine($"  执行 PostgreSQL 命令: {sql}");
        }
    }

    // 具体产品 - PostgreSQL数据读取器
    public class PostgreSQLDataReader : IDataReader
    {
        public void Read()
        {
            Console.WriteLine("  读取 PostgreSQL 数据");
        }
    }

    // 具体产品 - PostgreSQL事务
    public class PostgreSQLTransaction : ITransaction
    {
        public void Begin()
        {
            Console.WriteLine("  开始 PostgreSQL 事务");
        }

        public void Commit()
        {
            Console.WriteLine("  提交 PostgreSQL 事务");
        }

        public void Rollback()
        {
            Console.WriteLine("  回滚 PostgreSQL 事务");
        }
    }

    // 具体工厂 - PostgreSQL数据库工厂
    public class PostgreSQLFactory : IDatabaseFactory
    {
        public IConnection CreateConnection()
        {
            return new PostgreSQLConnection();
        }

        public ICommand CreateCommand()
        {
            return new PostgreSQLCommand();
        }

        public IDataReader CreateDataReader()
        {
            return new PostgreSQLDataReader();
        }

        public ITransaction CreateTransaction()
        {
            return new PostgreSQLTransaction();
        }
    }

    // 数据访问层类
    public class DataAccessLayer
    {
        private readonly IConnection _connection;
        private readonly ICommand _command;
        private readonly IDataReader _dataReader;
        private readonly ITransaction _transaction;

        public DataAccessLayer(IDatabaseFactory factory)
        {
            _connection = factory.CreateConnection();
            _command = factory.CreateCommand();
            _dataReader = factory.CreateDataReader();
            _transaction = factory.CreateTransaction();
        }

        public void PerformDatabaseOperations()
        {
            _connection.Connect();
            _transaction.Begin();
            _command.Execute("SELECT * FROM users");
            _dataReader.Read();
            _transaction.Commit();
            _connection.Disconnect();
        }
    }
}
