using _05Builder._01ImplementationMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05Builder._02Example.AppConfig
{
    // 负责管理正确的构造过程
    public class AppConfigDirector<T> where T : new()
    {
        public void Construct(IConfigBuilder<T> builder, List<Tuple<string, string>> parts)
        {
            foreach (var part in parts)
            {
                builder.BuildPart(part.Item1, part.Item2);
            }
        }
    }
}
