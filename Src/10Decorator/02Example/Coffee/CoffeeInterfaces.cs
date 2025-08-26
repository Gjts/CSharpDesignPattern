namespace _Decorator._02Example.Coffee
{
    // Coffee interface
    public interface ICoffee
    {
        string GetDescription();
        double GetCost();
    }

    // Base coffee implementations
    public class Espresso : ICoffee
    {
        public string GetDescription()
        {
            return "浓缩咖啡";
        }

        public double GetCost()
        {
            return 15.0;
        }
    }

    public class Americano : ICoffee
    {
        public string GetDescription()
        {
            return "美式咖啡";
        }

        public double GetCost()
        {
            return 12.0;
        }
    }

    // Decorator base class
    public abstract class CoffeeDecorator : ICoffee
    {
        protected ICoffee coffee;

        protected CoffeeDecorator(ICoffee coffee)
        {
            this.coffee = coffee;
        }

        public virtual string GetDescription()
        {
            return coffee.GetDescription();
        }

        public virtual double GetCost()
        {
            return coffee.GetCost();
        }
    }

    // Concrete decorators
    public class MilkDecorator : CoffeeDecorator
    {
        public MilkDecorator(ICoffee coffee) : base(coffee) { }

        public override string GetDescription()
        {
            return coffee.GetDescription() + ", 牛奶";
        }

        public override double GetCost()
        {
            return coffee.GetCost() + 3.0;
        }
    }

    public class ChocolateDecorator : CoffeeDecorator
    {
        public ChocolateDecorator(ICoffee coffee) : base(coffee) { }

        public override string GetDescription()
        {
            return coffee.GetDescription() + ", 巧克力";
        }

        public override double GetCost()
        {
            return coffee.GetCost() + 4.0;
        }
    }

    public class WhipCreamDecorator : CoffeeDecorator
    {
        public WhipCreamDecorator(ICoffee coffee) : base(coffee) { }

        public override string GetDescription()
        {
            return coffee.GetDescription() + ", 奶泡";
        }

        public override double GetCost()
        {
            return coffee.GetCost() + 2.0;
        }
    }

    public class SizeDecorator : CoffeeDecorator
    {
        private string size;

        public SizeDecorator(ICoffee coffee, string size) : base(coffee)
        {
            this.size = size;
        }

        public override string GetDescription()
        {
            return size + " " + coffee.GetDescription();
        }

        public override double GetCost()
        {
            double extraCost = size switch
            {
                "大杯" => 5.0,
                "超大杯" => 8.0,
                _ => 0.0
            };
            return coffee.GetCost() + extraCost;
        }
    }
}