using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06Prototype._01ImplementationMethod
{
    // 原型接口
    public interface IPrototype<T>
    {
        T ShallowCopy();

        T DeepCopy();
    }
}
