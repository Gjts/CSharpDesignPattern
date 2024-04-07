using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06Prototype._02Example.VirtualMachine
{
    // 具体的虚拟机类
    public class VirtualMachine : IPrototype<VirtualMachine>
    {
        public string Name { get; set; } = "";
        public int Memory { get; set; } = 0;
        public string OperatingSystem { get; set; } = "";

        public VirtualMachine Clone()
        {
            return (VirtualMachine)MemberwiseClone();
        }
    }
}
