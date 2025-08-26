namespace _SimpleFactory._02Example._03Transport
{
    // 运输方式枚举
    public enum TransportType
    {
        Truck,
        Ship,
        Airplane
    }

    // 运输接口
    public interface ITransport
    {
        void Deliver(string from, string to, string cargo);
    }

    // 卡车运输
    public class TruckTransport : ITransport
    {
        public void Deliver(string from, string to, string cargo)
        {
            Console.WriteLine($"[卡车运输] 从 {from} 到 {to}");
            Console.WriteLine($"  货物: {cargo}");
            Console.WriteLine($"  预计时间: 2-3天");
            Console.WriteLine($"  特点: 适合中短距离陆地运输");
        }
    }

    // 轮船运输
    public class ShipTransport : ITransport
    {
        public void Deliver(string from, string to, string cargo)
        {
            Console.WriteLine($"[轮船运输] 从 {from} 到 {to}");
            Console.WriteLine($"  货物: {cargo}");
            Console.WriteLine($"  预计时间: 15-30天");
            Console.WriteLine($"  特点: 适合大宗货物海运");
        }
    }

    // 飞机运输
    public class AirplaneTransport : ITransport
    {
        public void Deliver(string from, string to, string cargo)
        {
            Console.WriteLine($"[飞机运输] 从 {from} 到 {to}");
            Console.WriteLine($"  货物: {cargo}");
            Console.WriteLine($"  预计时间: 1-2天");
            Console.WriteLine($"  特点: 适合紧急和高价值货物");
        }
    }

    // 运输工厂
    public static class TransportFactory
    {
        public static ITransport CreateTransport(TransportType type)
        {
            switch (type)
            {
                case TransportType.Truck:
                    return new TruckTransport();
                case TransportType.Ship:
                    return new ShipTransport();
                case TransportType.Airplane:
                    return new AirplaneTransport();
                default:
                    throw new ArgumentException($"Unknown transport type: {type}");
            }
        }
    }
}
