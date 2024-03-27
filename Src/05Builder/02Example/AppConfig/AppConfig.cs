using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05Builder._02Example.AppConfig
{
    public class AppConfig
    {
        private readonly Dictionary<string, string> _parts = new Dictionary<string, string>();

        public void Add(string partName, string part)
        {
            _parts[partName] = part;
        }

        public void ShowProduct()
        {
            Console.WriteLine("配置部分:");
            foreach (var part in _parts)
            {
                Console.WriteLine($"{part.Key}: {part.Value}");
            }
        }
    }
}
