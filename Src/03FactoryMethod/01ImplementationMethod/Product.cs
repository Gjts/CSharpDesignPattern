namespace _FactoryMethod._01ImplementationMethod
{
    // 抽象产品类
    public abstract class Product
    {
        public abstract void Use();
    }

    // 具体产品类A
    public class ConcreteA : Product
    {
        public override void Use()
        {
            Console.WriteLine("使用产品A");
        }
    }

    // 具体产品类A
    public class ConcreteB : Product
    {
        public override void Use()
        {
            Console.WriteLine("使用产品B");
        }
    }
}
