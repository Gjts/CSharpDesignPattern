using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Singleton._02Example._03EnumSingleton
{
    public enum ConfigManager
    {
        DatabaseConnectionString,
        LogLevel,
        MaxConnections
    }

    public enum LogLevel
    {
        info,
        warn,
        error,
    }
}
