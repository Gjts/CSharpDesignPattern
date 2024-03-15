using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _SimpleFactory._01ImplementationMethod
{
    // 抽象类
    public abstract class Product
    {
        public abstract void Use();
    }

    // 具体类A
    public class ConcreteA : Product
    {
        public override void Use()
        {
            Console.WriteLine("创建了A");
        }
    }

    // 具体类B
    public class ConcreteB : Product
    {
        public override void Use()
        {
            Console.WriteLine("创建了B");
        }
    }
}
