using _08Bridge._01ImplementationMethod;
using _08Bridge._02Example;

namespace _08Bridge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 桥接模式 Bridge Pattern ===\n");

            // 基础实现
            Console.WriteLine("1. 基础实现：");
            IImplementor implementorA = new ConcreteImplementorA();
            Abstraction abstraction = new RefinedAbstraction(implementorA);
            abstraction.Operation();

            IImplementor implementorB = new ConcreteImplementorB();
            abstraction = new RefinedAbstraction(implementorB);
            abstraction.Operation();

            Console.WriteLine("\n2. 实际示例 - 遥控器和设备：");
            
            // 使用普通遥控器控制电视
            IDevice tv = new TV();
            RemoteControl remote = new AdvancedRemoteControl(tv);
            remote.TogglePower();
            remote.VolumeUp();
            remote.ChannelUp();

            Console.WriteLine();

            // 使用高级遥控器控制收音机
            IDevice radio = new Radio();
            AdvancedRemoteControl advancedRemote = new AdvancedRemoteControl(radio);
            advancedRemote.TogglePower();
            advancedRemote.VolumeUp();
            advancedRemote.Mute();
        }
    }
}