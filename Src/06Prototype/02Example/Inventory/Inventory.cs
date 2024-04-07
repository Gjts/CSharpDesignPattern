using _06Prototype._01ImplementationMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06Prototype._02Example.Inventory
{
    // 定义库存物品类
    public class Inventory : IPrototype<Inventory>
    {
        public string Name { get; set; }
        public int Value { get; set; }

        // 无参数构造函数
        public Inventory()
        {
            Name = "Default";
            Value = 0;
        }

        // 带参数的构造函数
        public Inventory(string name, int value)
        {
            Name = name;
            Value = value;
        }

        // 实现深拷贝
        public Inventory DeepCopy()
        {
            return new Inventory(Name, Value);
        }

        // 实现浅拷贝
        public Inventory ShallowCopy()
        {
            return (Inventory)this.MemberwiseClone();
        }
    }
}
