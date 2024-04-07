using _06Prototype._01ImplementationMethod;
using _06Prototype._02Example;
using _06Prototype._02Example.Inventory;
using _06Prototype._02Example.VirtualMachine;

namespace _06Prototype
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1.创建一个接口或抽象类:定义一个用于克隆对象的方法，通常命名为 Clone 或 DeepCopy

            // 2.创建具体的实现类：实现步骤1中定义的接口或抽象类，提供克隆方法的具体实现。

            // 3.创建原型管理器（可选）：原型管理器用于注册和管理不同类型的原型对象，并在需要时检索和克隆这些对象。
            Console.WriteLine("-------------------------------- 虚拟机和容器管理 ----------------------------------");
            var prototype = new VirtualMachine { Name = "Ubuntu", Memory = 4096, OperatingSystem = "Linux" };
            var manager = new VirtualMachineManager();
            manager.RegisterVirtualMachine("UbuntuPrototype", prototype);

            var vm1 = manager.CreateVirtualMachine("UbuntuPrototype");
            Console.WriteLine($"Created VM - Name:{vm1.Name},Memory:{vm1.Memory},OS:{vm1.OperatingSystem}");

            // 修改原型对象，不影响已创建的虚拟机实例
            prototype.Memory = 8192;

            var vm2 = manager.CreateVirtualMachine("UbuntuPrototype");
            Console.WriteLine($"Created VM - Name:{vm2.Name},Memory:{vm2.Memory},OS:{vm2.OperatingSystem}");

            Console.WriteLine("-------------------------------- 管理不同类型的库存物品 ----------------------------------");

            // 创建原型实例
            var originalPrototype = new Prototype<Inventory>();

            // 添加属性
            originalPrototype["衣服"] = new Inventory("衣服", 100);
            originalPrototype["裤子"] = new Inventory("裤子", 150);
            originalPrototype["鞋子"] = new Inventory("鞋子", 650);

            // 执行浅拷贝
            var shallowCopyPrototype = originalPrototype.ShallowCopy();

            // 执行深拷贝
            var deepCopyPrototype = originalPrototype.DeepCopy();

            // 演示结果
            Console.WriteLine("原始值:");
            foreach (var entry in originalPrototype)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value.Name}, {entry.Value.Value}");
            }

            Console.WriteLine("\n浅拷贝值:");
            foreach (var entry in shallowCopyPrototype)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value.Name}, {entry.Value.Value}");
            }

            Console.WriteLine("\n深拷贝值:");
            foreach (var entry in deepCopyPrototype)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value.Name}, {entry.Value.Value}");
            }

            // 修改原型中的一个属性值
            Console.WriteLine("\n把衣服值改为:500元");
            originalPrototype["衣服"].Value = 500;

            // 检查浅拷贝和深拷贝的属性值是否发生变化
            Console.WriteLine("\n修改之后值:");
            Console.WriteLine($"Original 衣服: {originalPrototype["衣服"].Value}");
            Console.WriteLine($"Shallow Copy 衣服: {shallowCopyPrototype["衣服"].Value}");
            Console.WriteLine($"Deep Copy 衣服: {deepCopyPrototype["衣服"].Value}");
        }
    }
}