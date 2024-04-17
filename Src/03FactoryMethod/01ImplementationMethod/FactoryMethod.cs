namespace _FactoryMethod._01ImplementationMethod
{
    // 抽象工厂类
    public abstract class FactoryMethod
    {
        public abstract Product CreateProduct();
    }

    // 具体工厂类
    public class ConcreteFactoryA : FactoryMethod
    {
        public override Product CreateProduct()
        {
            return new ConcreteA();
        }
    }

    // 具体工厂类
    public class ConcreteFactoryB : FactoryMethod
    {
        public override Product CreateProduct()
        {
            return new ConcreteB();
        }
    }
}
