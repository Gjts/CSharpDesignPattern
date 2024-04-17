namespace _05Builder._01ImplementationMethod
{
    // 表示被构造的复杂对象
    public class Product
    {
        private readonly Dictionary<string, string> _parts = new Dictionary<string, string>();

        public void Add(string partName, string part)
        {
            _parts[partName] = part;
        }

        public void ShowProduct()
        {
            Console.WriteLine("产品部分:");
            foreach (var part in _parts)
            {
                Console.WriteLine($"{part.Key}: {part.Value}");
            }
        }
    }
}
