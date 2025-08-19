namespace _08Bridge._01ImplementationMethod
{
    // 抽象类
    public abstract class Abstraction
    {
        protected IImplementor implementor;

        public Abstraction(IImplementor implementor)
        {
            this.implementor = implementor;
        }

        public abstract void Operation();
    }

    // 扩展抽象类
    public class RefinedAbstraction : Abstraction
    {
        public RefinedAbstraction(IImplementor implementor) : base(implementor)
        {
        }

        public override void Operation()
        {
            Console.WriteLine("RefinedAbstraction: 扩展操作");
            implementor.OperationImpl();
        }
    }
}