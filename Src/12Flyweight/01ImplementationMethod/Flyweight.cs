namespace _12Flyweight._01ImplementationMethod
{
    // 享元抽象类
    public abstract class Flyweight
    {
        // 内部状态
        protected string intrinsicState;

        public Flyweight(string intrinsicState)
        {
            this.intrinsicState = intrinsicState;
        }

        // 外部状态通过参数传入
        public abstract void Operation(string extrinsicState);
    }

    // 具体享元类
    public class ConcreteFlyweight : Flyweight
    {
        public ConcreteFlyweight(string intrinsicState) : base(intrinsicState)
        {
        }

        public override void Operation(string extrinsicState)
        {
            Console.WriteLine($"ConcreteFlyweight: 内部状态 = {intrinsicState}, 外部状态 = {extrinsicState}");
        }
    }

    // 非共享具体享元类
    public class UnsharedConcreteFlyweight : Flyweight
    {
        private string allState;

        public UnsharedConcreteFlyweight(string allState) : base(allState)
        {
            this.allState = allState;
        }

        public override void Operation(string extrinsicState)
        {
            Console.WriteLine($"UnsharedConcreteFlyweight: 所有状态 = {allState}, 外部状态 = {extrinsicState}");
        }
    }

    // 享元工厂类
    public class FlyweightFactory
    {
        private Dictionary<string, Flyweight> flyweights = new Dictionary<string, Flyweight>();

        public FlyweightFactory()
        {
            // 初始化一些享元对象
            flyweights.Add("A", new ConcreteFlyweight("A"));
            flyweights.Add("B", new ConcreteFlyweight("B"));
            flyweights.Add("C", new ConcreteFlyweight("C"));
        }

        public Flyweight GetFlyweight(string key)
        {
            if (!flyweights.ContainsKey(key))
            {
                Console.WriteLine($"创建新的享元对象: {key}");
                flyweights[key] = new ConcreteFlyweight(key);
            }
            else
            {
                Console.WriteLine($"复用已有的享元对象: {key}");
            }
            return flyweights[key];
        }

        public int GetFlyweightCount()
        {
            return flyweights.Count;
        }

        public void ListFlyweights()
        {
            Console.WriteLine($"享元工厂中有 {flyweights.Count} 个享元对象:");
            foreach (var pair in flyweights)
            {
                Console.WriteLine($"  - {pair.Key}");
            }
        }
    }
}
