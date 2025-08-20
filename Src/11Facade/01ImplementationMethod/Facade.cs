namespace _11Facade._01ImplementationMethod
{
    // 子系统A
    public class SubSystemA
    {
        public void OperationA1()
        {
            Console.WriteLine("SubSystemA: 执行操作A1");
        }

        public void OperationA2()
        {
            Console.WriteLine("SubSystemA: 执行操作A2");
        }
    }

    // 子系统B
    public class SubSystemB
    {
        public void OperationB1()
        {
            Console.WriteLine("SubSystemB: 执行操作B1");
        }

        public void OperationB2()
        {
            Console.WriteLine("SubSystemB: 执行操作B2");
        }
    }

    // 子系统C
    public class SubSystemC
    {
        public void OperationC1()
        {
            Console.WriteLine("SubSystemC: 执行操作C1");
        }

        public void OperationC2()
        {
            Console.WriteLine("SubSystemC: 执行操作C2");
        }
    }

    // 外观类
    public class Facade
    {
        private SubSystemA subSystemA;
        private SubSystemB subSystemB;
        private SubSystemC subSystemC;

        public Facade()
        {
            subSystemA = new SubSystemA();
            subSystemB = new SubSystemB();
            subSystemC = new SubSystemC();
        }

        // 提供简单的接口方法1
        public void SimpleOperation1()
        {
            Console.WriteLine("Facade: 组织子系统执行操作1");
            subSystemA.OperationA1();
            subSystemB.OperationB1();
            subSystemC.OperationC1();
        }

        // 提供简单的接口方法2
        public void SimpleOperation2()
        {
            Console.WriteLine("Facade: 组织子系统执行操作2");
            subSystemA.OperationA2();
            subSystemB.OperationB2();
            subSystemC.OperationC2();
        }

        // 提供复杂的组合操作
        public void ComplexOperation()
        {
            Console.WriteLine("Facade: 执行复杂的组合操作");
            subSystemA.OperationA1();
            subSystemB.OperationB2();
            subSystemA.OperationA2();
            subSystemC.OperationC1();
            subSystemB.OperationB1();
            subSystemC.OperationC2();
        }
    }
}
