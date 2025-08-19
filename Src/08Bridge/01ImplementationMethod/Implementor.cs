namespace _08Bridge._01ImplementationMethod
{
    // 实现接口
    public interface IImplementor
    {
        void OperationImpl();
    }

    // 具体实现A
    public class ConcreteImplementorA : IImplementor
    {
        public void OperationImpl()
        {
            Console.WriteLine("ConcreteImplementorA: 具体实现A的操作");
        }
    }

    // 具体实现B
    public class ConcreteImplementorB : IImplementor
    {
        public void OperationImpl()
        {
            Console.WriteLine("ConcreteImplementorB: 具体实现B的操作");
        }
    }
}