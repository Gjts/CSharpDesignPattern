using _05Builder._02Example.AppConfig;
using _05Builder._02Example.WMSWarehouse;

namespace _05Builder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 建造者模式 (Builder Pattern) ================================");
            Console.WriteLine("适用场景：当需要创建复杂对象，且对象的构建过程需要独立于其表示时");
            Console.WriteLine("特点：将复杂对象的构建与表示分离，使得同样的构建过程可以创建不同的表示");
            Console.WriteLine("优点：构建过程可控；易于扩展；隔离复杂对象的创建和使用\n");

            Console.WriteLine("-------------------------------- 报表生成系统 ----------------------------------");
            
            var reportDirector = new ReportDirector();
            
            // 生成PDF报表
            Console.WriteLine("1. 生成PDF格式报表：");
            var pdfBuilder = new PDFReportBuilder();
            reportDirector.SetBuilder(pdfBuilder);
            reportDirector.ConstructFullReport();
            var pdfReport = pdfBuilder.GetReport();
            pdfReport.Show();
            
            // 生成Excel报表
            Console.WriteLine("\n2. 生成Excel格式报表：");
            var excelBuilder = new ExcelReportBuilder();
            reportDirector.SetBuilder(excelBuilder);
            reportDirector.ConstructSummaryReport();
            var excelReport = excelBuilder.GetReport();
            excelReport.Show();

            Console.WriteLine("\n-------------------------------- 电脑组装系统 ----------------------------------");
            
            var computerDirector = new ComputerDirector();
            
            // 组装游戏电脑
            Console.WriteLine("1. 组装游戏电脑：");
            var gamingBuilder = new GamingComputerBuilder();
            computerDirector.SetBuilder(gamingBuilder);
            computerDirector.ConstructComputer();
            var gamingPC = gamingBuilder.GetComputer();
            gamingPC.ShowSpecs();
            
            // 组装办公电脑
            Console.WriteLine("\n2. 组装办公电脑：");
            var officeBuilder = new OfficeComputerBuilder();
            computerDirector.SetBuilder(officeBuilder);
            computerDirector.ConstructComputer();
            var officePC = officeBuilder.GetComputer();
            officePC.ShowSpecs();
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- Director控制构建过程，Builder负责具体构建步骤");
            Console.WriteLine("- 相同的构建过程可以创建不同的产品表示");
            Console.WriteLine("- 适用于创建复杂对象，特别是对象由多个部分组成的情况");
        }
    }
}
