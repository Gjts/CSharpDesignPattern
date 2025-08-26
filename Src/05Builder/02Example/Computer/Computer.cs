namespace _Builder._02Example.Computer
{
    // 产品类 - 电脑
    public class Computer
    {
        public string CPU { get; set; } = "";
        public string RAM { get; set; } = "";
        public string Storage { get; set; } = "";
        public string GPU { get; set; } = "";
        public string OS { get; set; } = "";
        public decimal Price { get; set; }

        public void ShowSpecs()
        {
            Console.WriteLine("  === 电脑配置 ===");
            Console.WriteLine($"  CPU: {CPU}");
            Console.WriteLine($"  内存: {RAM}");
            Console.WriteLine($"  存储: {Storage}");
            Console.WriteLine($"  显卡: {GPU}");
            Console.WriteLine($"  系统: {OS}");
            Console.WriteLine($"  价格: ￥{Price:F2}");
        }
    }

    // 抽象建造者
    public interface IComputerBuilder
    {
        void SetCPU();
        void SetRAM();
        void SetStorage();
        void SetGPU();
        void SetOS();
        void SetPrice();
        Computer GetComputer();
    }

    // 具体建造者 - 游戏电脑建造者
    public class GamingComputerBuilder : IComputerBuilder
    {
        private Computer computer = new Computer();

        public void SetCPU()
        {
            computer.CPU = "Intel Core i9-13900K";
        }

        public void SetRAM()
        {
            computer.RAM = "32GB DDR5 6000MHz";
        }

        public void SetStorage()
        {
            computer.Storage = "2TB NVMe SSD";
        }

        public void SetGPU()
        {
            computer.GPU = "NVIDIA RTX 4090";
        }

        public void SetOS()
        {
            computer.OS = "Windows 11 Pro";
        }

        public void SetPrice()
        {
            computer.Price = 25999.99m;
        }

        public Computer GetComputer()
        {
            return computer;
        }
    }

    // 具体建造者 - 办公电脑建造者
    public class OfficeComputerBuilder : IComputerBuilder
    {
        private Computer computer = new Computer();

        public void SetCPU()
        {
            computer.CPU = "Intel Core i5-13400";
        }

        public void SetRAM()
        {
            computer.RAM = "16GB DDR4 3200MHz";
        }

        public void SetStorage()
        {
            computer.Storage = "512GB SSD";
        }

        public void SetGPU()
        {
            computer.GPU = "Intel UHD Graphics 730";
        }

        public void SetOS()
        {
            computer.OS = "Windows 11 Home";
        }

        public void SetPrice()
        {
            computer.Price = 5999.99m;
        }

        public Computer GetComputer()
        {
            return computer;
        }
    }

    // 指挥者
    public class ComputerDirector
    {
        private IComputerBuilder? _builder;

        public ComputerDirector()
        {
            // Default constructor
        }

        public ComputerDirector(IComputerBuilder builder)
        {
            _builder = builder;
        }

        public void SetBuilder(IComputerBuilder builder)
        {
            _builder = builder;
        }

        public void ConstructComputer()
        {
            var builder = _builder ?? throw new InvalidOperationException("请先通过 SetBuilder 指定建造者。");
            builder.SetCPU();
            builder.SetRAM();
            builder.SetStorage();
            builder.SetGPU();
            builder.SetOS();
            builder.SetPrice();
        }
    }
}
