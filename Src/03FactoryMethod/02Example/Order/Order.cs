using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _FactoryMethod._02Example.Order
{
    // 抽象订单类
    public abstract class Order
    {
        public abstract void Process();
    }

    // 具体订单类：普通订单
    public class NormalOrder : Order
    {
        public override void Process()
        {
            Console.WriteLine("普通订单");
        }
    }

    // 具体订单类：预售订单
    public class PreSaleOrder : Order
    {
        public override void Process()
        {
            Console.WriteLine("预售订单");
        }
    }

    // 具体订单类：定制订单
    public class CustomOrder : Order
    {
        public override void Process()
        {
            Console.WriteLine("定制订单");
        }
    }
}
