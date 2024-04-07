using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06Prototype._02Example.VirtualMachine
{
    // 虚拟机/容器接口
    public interface IPrototype<T>
    {
        T Clone();
    }
}
