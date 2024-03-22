using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _AbstractFactory._01ImplementationMethod
{
    // 抽象产品接口
    public interface IProduct
    {
        void Operation();
    }

    // 具体产品类A
    public class ConcreteProductA : IProduct
    {
        public void Operation()
        {
            Console.WriteLine("使用产品A");
        }
    }

    // 具体产品类B
    public class ConcreteProductB : IProduct
    {
        public void Operation()
        {
            Console.WriteLine("使用产品B");
        }
    }
}
