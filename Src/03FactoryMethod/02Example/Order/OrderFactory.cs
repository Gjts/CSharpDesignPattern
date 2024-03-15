using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _FactoryMethod._02Example.Order
{
    // 抽象工厂类
    public abstract class OrderFactory
    {
        public abstract Order CreateOrder();
    }

    // 具体工厂类：普通订单
    public class NormalOrderFactory : OrderFactory
    {
        public override Order CreateOrder()
        {
            return new NormalOrder();
        }
    }

    // 具体工厂类：预售订单
    public class PreSaleOrderFactory : OrderFactory
    {
        public override Order CreateOrder()
        {
            return new PreSaleOrder();
        }
    }

    // 具体工厂类：定制订单
    public class CustomOrderFactory : OrderFactory
    {
        public override Order CreateOrder()
        {
            return new CustomOrder();
        }
    }
}
