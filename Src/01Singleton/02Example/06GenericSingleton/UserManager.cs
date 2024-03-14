using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Singleton._02Example._06GenericSingleton
{
    // 用户管理器
    public class UserManager<T> where T : User, new()
    {
        private static volatile UserManager<T>? instance;
        private static readonly object lockObject = new object();

        // 存储用户的集合
        private List<T> users;

        private UserManager()
        {
            users = new List<T>();
            // 在这里可以添加其他初始化操作
        }

        public static UserManager<T> GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new UserManager<T>();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public List<T> GetUsers()
        {
            return users;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户信息</param>
        public void AddUser(T user)
        {
            users.Add(user);
        }
    }
}
