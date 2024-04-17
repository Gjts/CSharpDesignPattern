namespace _AbstractFactory._01ImplementationMethod
{
    // 抽象工厂接口
    public interface IFactory
    {
        IProduct CreateProduct();
    }

    // 具体工厂类A
    public class ConcreteFactoryA : IFactory
    {
        public IProduct CreateProduct()
        {
            return new ConcreteProductA();
        }
    }

    // 具体工厂类B
    public class ConcreteFactoryB : IFactory
    {
        public IProduct CreateProduct()
        {
            return new ConcreteProductB();
        }
    }
}
