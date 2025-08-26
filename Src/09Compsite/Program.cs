using _Composite._02Example.FileSystem;
using _Composite._02Example.Organization;

namespace _09Composite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 组合模式 (Composite Pattern) ================================");
            Console.WriteLine("适用场景：需要表示对象的部分-整体层次结构；希望用户忽略组合对象与单个对象的不同");
            Console.WriteLine("特点：将对象组合成树形结构以表示'部分-整体'的层次结构");
            Console.WriteLine("优点：定义了包含基本对象和组合对象的类层次结构；简化客户端代码；易于增加新类型的组件\n");

            Console.WriteLine("-------------------------------- 文件系统示例 ----------------------------------");
            
            // 创建文件系统结构
            var root = new Directory("根目录");
            
            var docs = new Directory("文档");
            docs.Add(new File("简历.docx", 50));
            docs.Add(new File("报告.pdf", 120));
            
            var photos = new Directory("照片");
            photos.Add(new File("风景.jpg", 2048));
            photos.Add(new File("人物.png", 1536));
            
            var projects = new Directory("项目");
            var project1 = new Directory("项目A");
            project1.Add(new File("代码.cs", 10));
            project1.Add(new File("文档.md", 5));
            projects.Add(project1);
            
            root.Add(docs);
            root.Add(photos);
            root.Add(projects);
            root.Add(new File("readme.txt", 2));
            
            // 显示文件系统结构
            Console.WriteLine("文件系统结构：");
            root.Display(0);
            Console.WriteLine($"\n总大小: {root.GetSize()} KB");

            Console.WriteLine("\n-------------------------------- 组织架构示例 ----------------------------------");
            
            // 创建公司组织结构
            var ceo = new Manager("张总", "CEO", 50000);
            
            var cto = new Manager("李总", "CTO", 30000);
            cto.Add(new Developer("王工", "高级开发", 15000));
            cto.Add(new Developer("赵工", "中级开发", 12000));
            
            var cfo = new Manager("陈总", "CFO", 30000);
            cfo.Add(new Developer("孙会计", "高级会计", 13000));
            cfo.Add(new Developer("钱会计", "会计", 10000));
            
            ceo.Add(cto);
            ceo.Add(cfo);
            ceo.Add(new Developer("周助理", "助理", 8000));
            
            // 显示组织结构
            Console.WriteLine("公司组织结构：");
            ceo.Display(0);
            Console.WriteLine($"\n工资总额: ¥{ceo.GetSalary():N0}");
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 文件和目录、员工和经理都使用相同的接口");
            Console.WriteLine("- 客户端可以一致地处理单个对象和组合对象");
            Console.WriteLine("- 组合对象可以包含其他组合对象，形成树形结构");
        }
    }
}
