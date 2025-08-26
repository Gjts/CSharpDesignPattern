namespace _15Template
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 模板方法模式 (Template Method Pattern) ===\n");

            // 示例1：数据导入导出流程
            Console.WriteLine("示例1：数据导入导出流程");
            Console.WriteLine("------------------------");
            RunDataImportExportExample();

            Console.WriteLine("\n示例2：报表生成流程");
            Console.WriteLine("------------------------");
            RunReportGenerationExample();

            Console.WriteLine("\n示例3：游戏AI行为模板");
            Console.WriteLine("------------------------");
            RunGameAIExample();
        }

        static void RunDataImportExportExample()
        {
            // CSV数据导入
            Console.WriteLine("CSV数据导入流程:");
            var csvImporter = new CsvDataImporter();
            csvImporter.ImportData("customers.csv");

            Console.WriteLine("\nJSON数据导入流程:");
            var jsonImporter = new JsonDataImporter();
            jsonImporter.ImportData("products.json");

            Console.WriteLine("\nXML数据导出流程:");
            var xmlExporter = new XmlDataExporter();
            var sampleData = new List<Dictionary<string, object>>
            {
                new() { ["id"] = 1, ["name"] = "产品A", ["price"] = 100 },
                new() { ["id"] = 2, ["name"] = "产品B", ["price"] = 200 }
            };
            xmlExporter.ExportData(sampleData, "products.xml");

            Console.WriteLine("\nExcel数据导出流程:");
            var excelExporter = new ExcelDataExporter();
            excelExporter.ExportData(sampleData, "products.xlsx");
        }

        static void RunReportGenerationExample()
        {
            // 销售报表生成
            Console.WriteLine("销售报表生成流程:");
            var salesReport = new SalesReport();
            salesReport.GenerateReport();

            Console.WriteLine("\n库存报表生成流程:");
            var inventoryReport = new InventoryReport();
            inventoryReport.GenerateReport();

            Console.WriteLine("\n财务报表生成流程:");
            var financialReport = new FinancialReport();
            financialReport.GenerateReport();
        }

        static void RunGameAIExample()
        {
            Console.WriteLine("战士AI行为:");
            var warrior = new WarriorAI();
            warrior.TakeTurn();

            Console.WriteLine("\n法师AI行为:");
            var mage = new MageAI();
            mage.TakeTurn();

            Console.WriteLine("\n弓箭手AI行为:");
            var archer = new ArcherAI();
            archer.TakeTurn();
        }
    }
}