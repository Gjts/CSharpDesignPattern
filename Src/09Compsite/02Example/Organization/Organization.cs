namespace _Composite._02Example.Organization
{
    // 组织结构组件
    public abstract class OrganizationComponent
    {
        protected string name;
        protected string position;

        public OrganizationComponent(string name, string position)
        {
            this.name = name;
            this.position = position;
        }

        public abstract void Display(int depth);
        public abstract int GetEmployeeCount();
    }

    // 叶子节点 - 员工
    public class Employee : OrganizationComponent
    {
        public Employee(string name, string position) : base(name, position) { }

        public override void Display(int depth)
        {
            Console.WriteLine($"{new string(' ', depth * 2)}👤 {name} - {position}");
        }

        public override int GetEmployeeCount()
        {
            return 1;
        }
    }

    // 组合节点 - 部门
    public class Department : OrganizationComponent
    {
        private List<OrganizationComponent> members = new List<OrganizationComponent>();

        public Department(string name, string position) : base(name, position) { }

        public void Add(OrganizationComponent component)
        {
            members.Add(component);
        }

        public void Remove(OrganizationComponent component)
        {
            members.Remove(component);
        }

        public override void Display(int depth)
        {
            Console.WriteLine($"{new string(' ', depth * 2)}🏢 {name} - {position}");
            foreach (var member in members)
            {
                member.Display(depth + 1);
            }
        }

        public override int GetEmployeeCount()
        {
            int count = 0;
            foreach (var member in members)
            {
                count += member.GetEmployeeCount();
            }
            return count;
        }
    }
}
