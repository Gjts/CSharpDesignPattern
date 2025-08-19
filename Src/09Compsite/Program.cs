using _09Compsite._01ImplementationMethod;
using _09Compsite._02Example;

namespace _09Compsite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 组合模式 Composite Pattern ===\n");

            // 基础实现
            Console.WriteLine("1. 基础实现：");
            Composite root = new Composite("根节点");
            root.Add(new Leaf("叶子A"));
            root.Add(new Leaf("叶子B"));

            Composite comp = new Composite("分支X");
            comp.Add(new Leaf("叶子XA"));
            comp.Add(new Leaf("叶子XB"));

            Composite comp2 = new Composite("分支XY");
            comp2.Add(new Leaf("叶子XYA"));
            comp.Add(comp2);

            root.Add(comp);
            root.Add(new Leaf("叶子C"));

            root.Display(1);

            // 文件系统示例
            Console.WriteLine("\n2. 文件系统示例：");
            Directory rootDir = new Directory("根目录");
            
            Directory srcDir = new Directory("src");
            srcDir.Add(new File("main.cs", 1024));
            srcDir.Add(new File("utils.cs", 512));
            
            Directory testDir = new Directory("test");
            testDir.Add(new File("test1.cs", 256));
            testDir.Add(new File("test2.cs", 256));
            
            rootDir.Add(srcDir);
            rootDir.Add(testDir);
            rootDir.Add(new File("README.md", 128));
            rootDir.Add(new File(".gitignore", 64));
            
            rootDir.Display(0);

            // 组织结构示例
            Console.WriteLine("\n3. 组织结构示例：");
            Manager ceo = new Manager("张三", "CEO");
            
            Manager cto = new Manager("李四", "CTO");
            Manager cfo = new Manager("王五", "CFO");
            
            Manager devManager = new Manager("赵六", "开发经理");
            devManager.Add(new Employee("小明", "高级开发"));
            devManager.Add(new Employee("小红", "中级开发"));
            devManager.Add(new Employee("小刚", "初级开发"));
            
            Manager testManager = new Manager("孙七", "测试经理");
            testManager.Add(new Employee("小李", "测试工程师"));
            testManager.Add(new Employee("小张", "测试工程师"));
            
            cto.Add(devManager);
            cto.Add(testManager);
            
            cfo.Add(new Employee("小王", "会计"));
            cfo.Add(new Employee("小陈", "出纳"));
            
            ceo.Add(cto);
            ceo.Add(cfo);
            ceo.Add(new Employee("小秘", "秘书"));
            
            ceo.Display(0);
        }
    }
}