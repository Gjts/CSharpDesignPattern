using Microsoft.Win32;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Singleton._02Example._07RegistrySingleton
{
    // 负载均衡器
    public class LoadBalancer
    {
        // 服务器集合
        private static readonly ConcurrentDictionary<string, LoadBalancer> instances = new ConcurrentDictionary<string, LoadBalancer>();
        private List<CustomServer> servers;
        private Random random = new Random();

        private LoadBalancer()
        {
            // 初始化负载均衡器，添加初始服务器地址
            servers = new List<CustomServer>();
        }

        public static LoadBalancer GetInstance(string key)
        {
            if (!instances.ContainsKey(key))
            {
                instances[key] = new LoadBalancer();
            }

            return instances[key];
        }


        public void AddServer(CustomServer server)
        {
            servers.Add(server);
            Console.WriteLine($"添加服务器: {server}");
        }

        public void RemoveServer(CustomServer server)
        {
            if (servers.Contains(server))
            {
                servers.Remove(server);
                Console.WriteLine($"删除服务器: {server}");
            }
        }

        /// <summary>
        /// 获取随机服务器
        /// </summary>
        /// <returns></returns>
        public CustomServer GetRandomServer()
        {
            var server = servers[random.Next(servers.Count)];
            return server;
        }

        public List<CustomServer> GetCustomServers()
        {
            return servers;
        }
    }
}
