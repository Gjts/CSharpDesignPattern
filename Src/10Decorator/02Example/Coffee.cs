namespace _10Decorator._02Example
{
    // 饮料抽象类
    public abstract class Beverage
    {
        protected string description = "Unknown Beverage";

        public virtual string GetDescription()
        {
            return description;
        }

        public abstract double GetCost();
    }

    // 具体饮料：浓缩咖啡
    public class Espresso : Beverage
    {
        public Espresso()
        {
            description = "浓缩咖啡";
        }

        public override double GetCost()
        {
            return 15.0;
        }
    }

    // 具体饮料：混合咖啡
    public class HouseBlend : Beverage
    {
        public HouseBlend()
        {
            description = "混合咖啡";
        }

        public override double GetCost()
        {
            return 12.0;
        }
    }

    // 具体饮料：深焙咖啡
    public class DarkRoast : Beverage
    {
        public DarkRoast()
        {
            description = "深焙咖啡";
        }

        public override double GetCost()
        {
            return 13.0;
        }
    }

    // 调料装饰器抽象类
    public abstract class CondimentDecorator : Beverage
    {
        protected Beverage beverage;

        public CondimentDecorator(Beverage beverage)
        {
            this.beverage = beverage;
        }

        public override abstract string GetDescription();
    }

    // 具体调料：摩卡
    public class Mocha : CondimentDecorator
    {
        public Mocha(Beverage beverage) : base(beverage)
        {
        }

        public override string GetDescription()
        {
            return beverage.GetDescription() + ", 摩卡";
        }

        public override double GetCost()
        {
            return beverage.GetCost() + 3.0;
        }
    }

    // 具体调料：豆浆
    public class Soy : CondimentDecorator
    {
        public Soy(Beverage beverage) : base(beverage)
        {
        }

        public override string GetDescription()
        {
            return beverage.GetDescription() + ", 豆浆";
        }

        public override double GetCost()
        {
            return beverage.GetCost() + 2.0;
        }
    }

    // 具体调料：奶泡
    public class Whip : CondimentDecorator
    {
        public Whip(Beverage beverage) : base(beverage)
        {
        }

        public override string GetDescription()
        {
            return beverage.GetDescription() + ", 奶泡";
        }

        public override double GetCost()
        {
            return beverage.GetCost() + 2.5;
        }
    }

    // 具体调料：牛奶
    public class Milk : CondimentDecorator
    {
        public Milk(Beverage beverage) : base(beverage)
        {
        }

        public override string GetDescription()
        {
            return beverage.GetDescription() + ", 牛奶";
        }

        public override double GetCost()
        {
            return beverage.GetCost() + 2.0;
        }
    }
}