using _05Builder._01ImplementationMethod;
using _05Builder._02Example.AppConfig;
using _05Builder._02Example.WMSWarehouse;

namespace _05Builder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1.创建产品类: 用来被构造的复杂对象
            // 2.创建构造者接口：包含设置产品各个部分的方法，并且包含一个获取最终产品的方法
            // 3.创建具体构造者类：实现构造者接口，创建具体的构造者。
            // 4.创建指挥者类：负责管理构造顺序过程。

            Console.WriteLine("-------------------------------- 应用程序配置对象 ----------------------------------");
            var director = new AppConfigDirector<AppConfig>();
            var builder = new ConfigBuilder();
            var parts = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("应用程序配置", "应用程序配置11"),
            new Tuple<string, string>("应用程序名称", "应用程序名称11"),
            new Tuple<string, string>("数据库主机", "数据库主机11"),
            new Tuple<string, string>("数据库名称", "数据库名称11"),
            new Tuple<string, string>("缓存主机", "缓存主机11"),
            new Tuple<string, string>("数据库主机", "数据库主机11")
        };
            director.Construct(builder, parts);
            var product = builder.GetResult();
            product.ShowProduct();

            Console.WriteLine("-------------------------------- 创建不同类型的仓储物品 ----------------------------------");
            var directorWarehouse = new WarehouseItemDirector();
            var builderWarehouse = new WarehouseItemBuilder();
            directorWarehouse.Construct(builderWarehouse, "产品1", "苹果20", 100);
            directorWarehouse.Construct(builderWarehouse, "产品2", "特斯拉 Model X", 1000);
            var item = builderWarehouse.GetResult();
            item.DisplayItem();

            Console.ReadKey();
        }
    }
}