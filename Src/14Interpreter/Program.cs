using _14Interpreter.Example.WMSRuleEngine;

namespace _14Interpreter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 解释器模式 (Interpreter Pattern) ================================");
            Console.WriteLine("适用场景：当需要解释执行特定语法规则的语言时，如SQL解析、正则表达式、配置文件解析等");
            Console.WriteLine("特点：将语法规则表示为对象结构，通过解释器递归解释执行，易于扩展新的语法规则\n");

            Console.WriteLine("-------------------------------- WMS规则引擎示例 ----------------------------------");
            
            // 初始化WMS上下文和库存数据
            var context = new WMSContext();
            InitializeInventory(context);

            // 1. 基本查询示例
            Console.WriteLine("1. 库存查询：");
            var locationQuery = new LocationExpression("A-01");
            var result1 = locationQuery.Interpret(context);
            PrintInventoryResult("位置 A-01 的商品", result1);

            // 2. 低库存预警
            Console.WriteLine("\n2. 低库存预警（库存 < 20）：");
            var lowStockQuery = new LowStockExpression(20);
            var result2 = lowStockQuery.Interpret(context);
            PrintInventoryResult("需要补货的商品", result2);

            // 3. 组合查询
            Console.WriteLine("\n3. 组合查询（电子产品 AND 库存 < 50）：");
            var complexQuery = new AndExpression(
                new CategoryExpression("电子产品"),
                new LowStockExpression(50)
            );
            var result3 = complexQuery.Interpret(context);
            PrintInventoryResult("电子产品低库存", result3);

            // 4. 规则引擎示例
            Console.WriteLine("\n-------------------------------- 规则引擎执行 ----------------------------------");
            var ruleEngine = new WMSRuleEngine();
            
            // 添加补货规则
            ruleEngine.AddRule("电子产品补货", new ReplenishmentRuleExpression(30, "电子产品"));
            ruleEngine.AddRule("日用品补货", new ReplenishmentRuleExpression(50, "日用品"));
            
            // 执行规则
            Console.WriteLine("\n执行补货规则：");
            ruleEngine.ExecuteRule("电子产品补货", context);
            ruleEngine.ExecuteRule("日用品补货", context);

            // 5. 库位优化
            Console.WriteLine("\n-------------------------------- 库位优化分析 ----------------------------------");
            var optimizer = new LocationOptimizationExpression();
            optimizer.Interpret(context);
        }

        private static void InitializeInventory(WMSContext context)
        {
            context.Inventory.AddRange(new[]
            {
                new InventoryItem("SKU001", "iPhone 15", 15, "A-01", 6999, "电子产品"),
                new InventoryItem("SKU002", "MacBook Pro", 8, "A-01", 12999, "电子产品"),
                new InventoryItem("SKU003", "AirPods Pro", 150, "B-02", 1999, "电子产品"),
                new InventoryItem("SKU004", "洗发水", 45, "C-01", 39.9m, "日用品"),
                new InventoryItem("SKU005", "毛巾", 200, "C-02", 19.9m, "日用品"),
                new InventoryItem("SKU006", "牙膏", 5, "C-01", 12.9m, "日用品"),
                new InventoryItem("SKU007", "键盘", 30, "B-01", 299, "电子产品"),
                new InventoryItem("SKU008", "显示器", 12, "A-02", 2499, "电子产品"),
            });
        }

        private static void PrintInventoryResult(string title, List<InventoryItem> items)
        {
            Console.WriteLine($"{title}:");
            if (items.Any())
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"  {item.SKU} | {item.Name} | 库存:{item.Quantity} | 位置:{item.Location} | 类别:{item.Category}");
                }
            }
            else
            {
                Console.WriteLine("  没有找到匹配的商品");
            }
        }
    }
}
