using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Singleton._02Example._01lazySingleton
{
    // 数据库连接池
    public class ConnectionPool
    {
        private static ConnectionPool? instance;
        private List<string> clients = new List<string>();


        private ConnectionPool(int database)
        {
            InitializeConnectionPool(database);
        }

        public static ConnectionPool GetInstance(ConnectionPoolConfig config, int database)
        {
            if (instance == null)
            {
                instance = new ConnectionPool(database);
            }

            return instance;
        }

        private void InitializeConnectionPool(int database)
        {
            // 初始化数据库连接Client
            var client = $"数据库{database}";
            clients.Add(client);
        }

        public string? GetClient(int timeout)
        {
            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalMilliseconds < timeout)
            {
                if (clients.Count > 0)
                {
                    Console.WriteLine("连接成功");
                    string client = clients[0];
                    return client;
                }
            }

            Console.WriteLine("连接超时");
            return null;
        }

        public void AddClient(string client)
        {
            if (client != null)
            {
                clients.Add(client);
            }
        }

        public void ReleaseClient(string client)
        {
            if (client != null)
            {
                clients.RemoveAt(0);
            }
        }

        private void CheckConnectionValidity()
        {
            // 定期检查连接的有效性
            // 如果连接失效，则移除并创建新连接
        }
    }
}
