using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Singleton._02Example
{
    public class ConnectionPoolConfig
    {
        public int MaxConnections { get; set; } = 5;
        public int ConnectionTimeout { get; set; } = 3000;
        public int CheckInterval { get; set; } = 864000000;
    }
}
