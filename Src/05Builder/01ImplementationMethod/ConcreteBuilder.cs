namespace _05Builder._01ImplementationMethod
{
    // 实现Builder接口的类，提供创建对象的具体实现
    public class ConcreteBuilder : IBuilder<Product>
    {
        private Product _product = new Product();

        public void BuildPart(string partName, string part)
        {
            _product.Add(partName, part);
        }

        public Product GetResult()
        {
            return _product;
        }
    }
}
