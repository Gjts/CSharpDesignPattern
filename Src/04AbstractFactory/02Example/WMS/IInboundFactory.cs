namespace _AbstractFactory._02Example.Storage
{
    // 入库工厂接口
    public interface IInboundFactory
    {
        IWare GetWare();
    }

    // 具体入库工厂
    public class RawMaterialInboundFactory : IInboundFactory
    {
        public IWare GetWare()
        {
            return new RawMaterial();
        }
    }

    public class FinishedGoodInboundFactory : IInboundFactory
    {
        public IWare GetWare()
        {
            return new FinishedGood();
        }
    }
}
