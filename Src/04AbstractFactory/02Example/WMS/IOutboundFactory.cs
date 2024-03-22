using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _AbstractFactory._02Example.Storage
{
    // 出库工厂接口
    public interface IOutboundFactory
    {
        IWare GetWare();
    }

    // 具体出库工厂
    public class RawMaterialOutboundFactory : IOutboundFactory
    {
        public IWare GetWare()
        {
            return new RawMaterial();
        }
    }

    public class FinishedGoodOutboundFactory : IOutboundFactory
    {
        public IWare GetWare()
        {
            return new FinishedGood();
        }
    }
}
