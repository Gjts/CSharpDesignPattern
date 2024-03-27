using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05Builder._01ImplementationMethod
{
    // 抽象接口，用于定义创建对象的各个部分的方法
    public interface IBuilder<T> where T : new()
    {
        void BuildPart(string partName, string part);

        T GetResult();
    }
}
