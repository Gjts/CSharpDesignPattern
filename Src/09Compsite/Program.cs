using FSFile = _Composite._02Example.FileSystem.File;
using FSDirectory = _Composite._02Example.FileSystem.Directory;
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
            var root = new FSDirectory("根目录");
            
            var docs = new FSDirectory("文档");
            docs.Add(new FSFile("简历.docx", 50));
            docs.Add(new FSFile("报告.pdf", 120));
            
            var photos = new FSDirectory("照片");
            photos.Add(new FSFile("风景.jpg", 2048));
            photos.Add(new FSFile("人物.png", 1536));
            
            var projects = new FSDirectory("项目");
            var project1 = new FSDirectory("项目A");
            project1.Add(new FSFile("代码.cs", 10));
            project1.Add(new FSFile("文档.md", 5));
            projects.Add(project1);
            
            root.Add(docs);
            root.Add(photos);
            root.Add(projects);
            root.Add(new FSFile("readme.txt", 2));
            
            // 显示文件系统结构
            Console.WriteLine("文件系统结构：");
            root.Display(0);
            Console.WriteLine($"\n总大小: {root.GetSize()} KB");

            Console.WriteLine("\n-------------------------------- 组织架构示例 ----------------------------------");
            
            // 创建公司组织结构
            var ceo = new Department("张总", "CEO");
            
            var cto = new Department("李总", "CTO");
            cto.Add(new Employee("王工", "高级开发"));
            cto.Add(new Employee("赵工", "中级开发"));
            
            var cfo = new Department("陈总", "CFO");
            cfo.Add(new Employee("孙会计", "高级会计"));
            cfo.Add(new Employee("钱会计", "会计"));
            
            ceo.Add(cto);
            ceo.Add(cfo);
            ceo.Add(new Employee("周助理", "助理"));
            
            // 显示组织结构
            Console.WriteLine("公司组织结构：");
            ceo.Display(0);
            Console.WriteLine($"\n员工总数: {ceo.GetEmployeeCount()}");
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 文件和目录、员工和经理都使用相同的接口");
            Console.WriteLine("- 客户端可以一致地处理单个对象和组合对象");
            Console.WriteLine("- 组合对象可以包含其他组合对象，形成树形结构");
        }
    }
}
