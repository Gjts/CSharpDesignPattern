namespace _09Compsite._02Example
{
    // 组织结构组件抽象类
    public abstract class OrganizationComponent
    {
        protected string name;
        protected string position;

        public OrganizationComponent(string name, string position)
        {
            this.name = name;
            this.position = position;
        }

        public abstract void Add(OrganizationComponent component);
        public abstract void Remove(OrganizationComponent component);
        public abstract void Display(int depth);
        public abstract int GetEmployeeCount();
    }

    // 员工类（叶子节点）
    public class Employee : OrganizationComponent
    {
        public Employee(string name, string position) : base(name, position)
        {
        }

        public override void Add(OrganizationComponent component)
        {
            Console.WriteLine("员工不能有下属");
        }

        public override void Remove(OrganizationComponent component)
        {
            Console.WriteLine("员工不能移除下属");
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string(' ', depth) + $"👤 {position}: {name}");
        }

        public override int GetEmployeeCount()
        {
            return 1;
        }
    }

    // 部门/经理类（组合节点）
    public class Manager : OrganizationComponent
    {
        private List<OrganizationComponent> subordinates = new List<OrganizationComponent>();

        public Manager(string name, string position) : base(name, position)
        {
        }

        public override void Add(OrganizationComponent component)
        {
            subordinates.Add(component);
        }

        public override void Remove(OrganizationComponent component)
        {
            subordinates.Remove(component);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string(' ', depth) + $"👔 {position}: {name} (团队人数: {GetEmployeeCount()})");
            
            foreach (OrganizationComponent component in subordinates)
            {
                component.Display(depth + 2);
            }
        }

        public override int GetEmployeeCount()
        {
            int count = 1; // 包括自己
            foreach (OrganizationComponent component in subordinates)
            {
                count += component.GetEmployeeCount();
            }
            return count;
        }
    }
}