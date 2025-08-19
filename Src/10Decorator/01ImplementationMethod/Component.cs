namespace _10Decorator._01ImplementationMethod
{
    // 抽象组件
    public abstract class Component
    {
        public abstract void Operation();
    }

    // 具体组件
    public class ConcreteComponent : Component
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteComponent: 基础操作");
        }
    }

    // 装饰器抽象类
    public abstract class Decorator : Component
    {
        protected Component component;

        public void SetComponent(Component component)
        {
            this.component = component;
        }

        public override void Operation()
        {
            if (component != null)
            {
                component.Operation();
            }
        }
    }

    // 具体装饰器A
    public class ConcreteDecoratorA : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            AddedBehavior();
        }

        private void AddedBehavior()
        {
            Console.WriteLine("ConcreteDecoratorA: 添加的行为A");
        }
    }

    // 具体装饰器B
    public class ConcreteDecoratorB : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            AddedBehavior();
        }

        private void AddedBehavior()
        {
            Console.WriteLine("ConcreteDecoratorB: 添加的行为B");
        }
    }
}