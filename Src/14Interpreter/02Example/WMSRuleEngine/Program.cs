using _14Interpreter;

namespace _14Interpreter.Example.WMSRuleEngine
{
    public class WMSRuleEngineExample
    {
        public static void Run()
        {
            Console.WriteLine("\n=== WMS规则引擎示例 ===");
            
            // 初始化WMS上下文和库存数据
            var context = new WMSContext();
            InitializeInventory(context);

            // 创建规则引擎
            var ruleEngine = new _14Interpreter.WMSRuleEngine();

            // 添加补货规则
            ruleEngine.AddRule("电子产品补货", new ReplenishmentRuleExpression(30, "电子产品"));
            ruleEngine.AddRule("日用品补货", new ReplenishmentRuleExpression(50, "日用品"));

            // 添加组合规则：A区低库存商品
            ruleEngine.AddRule("A区低库存", new AndExpression(
                new LocationExpression("A-01"),
                new LowStockExpression(20)
            ));

            // 执行规则
            ruleEngine.ExecuteRule("电子产品补货", context);
            ruleEngine.ExecuteRule("日用品补货", context);
            
            var areaResult = ruleEngine.ExecuteRule("A区低库存", context);
            if (areaResult.Any())
            {
                Console.WriteLine($"  A区需要补货的商品数: {areaResult.Count()}");
            }

            // 库位优化分析
            Console.WriteLine("\n库位优化建议:");
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
    }
}
