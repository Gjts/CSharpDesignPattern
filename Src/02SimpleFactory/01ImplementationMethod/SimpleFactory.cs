namespace _SimpleFactory._01ImplementationMethod
{
    // 简单工厂类
    public class SimpleFactory
    {
        public T CreateProduct<T>() where T : Product, new()
        {
            return new T();
        }
    }
}
