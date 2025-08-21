namespace _09Compsite._01ImplementationMethod
{
    // 组件抽象类
    public abstract class Component
    {
        protected string name;

        public Component(string name)
        {
            this.name = name;
        }

        // 添加公共属性来访问 name
        public string Name => name;

        public abstract void Add(Component component);
        public abstract void Remove(Component component);
        public abstract void Display(int depth);
    }

    // 叶子节点
    public class Leaf : Component
    {
        public Leaf(string name) : base(name)
        {
        }

        public override void Add(Component component)
        {
            Console.WriteLine("叶子节点不能添加子节点");
        }

        public override void Remove(Component component)
        {
            Console.WriteLine("叶子节点不能移除子节点");
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + name);
        }
    }

    // 组合节点
    public class Composite : Component
    {
        private List<Component> children = new List<Component>();

        public Composite(string name) : base(name)
        {
        }

        public override void Add(Component component)
        {
            children.Add(component);
        }

        public override void Remove(Component component)
        {
            children.Remove(component);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + name);
            
            // 递归显示子节点
            foreach (Component component in children)
            {
                component.Display(depth + 2);
            }
        }
    }
}