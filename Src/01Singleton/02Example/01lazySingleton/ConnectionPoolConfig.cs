﻿namespace _Singleton._02Example._01lazySingleton
{
    public class ConnectionPoolConfig
    {
        public int MaxConnections { get; set; } = 5;
        public int ConnectionTimeout { get; set; } = 3000;
        public int CheckInterval { get; set; } = 864000000;
    }
}
