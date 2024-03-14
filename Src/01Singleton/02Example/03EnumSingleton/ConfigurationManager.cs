using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Singleton._02Example._03EnumSingleton
{
    // 配置管理器
    public class ConfigurationManager
    {
        private static string databaseConnectionString = "你的数据库连接字符串";
        private static LogLevel info = LogLevel.info;
        private static string logLevel = info.ToString();
        private static int maxConnections = 100;

        public static void SetConfigValue(ConfigManager config, string value)
        {
            switch (config)
            {
                case ConfigManager.DatabaseConnectionString:
                    databaseConnectionString = value;
                    break;
                case ConfigManager.LogLevel:
                    logLevel = value;
                    break;
                case ConfigManager.MaxConnections:
                    if (int.TryParse(value, out int intValue))
                    {
                        maxConnections = intValue;
                    }
                    else
                    {
                        Console.WriteLine("请提供一个整数");
                    }
                    break;
                default:
                    Console.WriteLine("无效的配置");
                    break;
            }
        }

        public static string? GetConfigValue(ConfigManager config)
        {
            switch (config)
            {
                case ConfigManager.DatabaseConnectionString:
                    return databaseConnectionString;
                case ConfigManager.LogLevel:
                    return logLevel;
                case ConfigManager.MaxConnections:
                    return maxConnections.ToString();
                default:
                    return null;
            }
        }
    }
}
