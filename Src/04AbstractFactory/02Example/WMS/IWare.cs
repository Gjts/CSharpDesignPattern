namespace _AbstractFactory._02Example.Storage
{
    // 货物接口
    public interface IWare
    {
        string GetName();
    }

    // 具体的原材料
    public class RawMaterial : IWare
    {
        public string GetName()
        {
            return "原材料";
        }
    }

    // 具体的制成品
    public class FinishedGood : IWare
    {
        public string GetName()
        {
            return "制成品";
        }
    }
}
