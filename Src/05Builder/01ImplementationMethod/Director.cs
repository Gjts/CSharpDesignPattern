using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05Builder._01ImplementationMethod
{
    // 负责管理正确的构造过程
    public class Director<T> where T : new()
    {
        public void Construct(IBuilder<T> builder, List<Tuple<string, string>> parts)
        {
            foreach (var part in parts)
            {
                builder.BuildPart(part.Item1, part.Item2);
            }
        }
    }
}
