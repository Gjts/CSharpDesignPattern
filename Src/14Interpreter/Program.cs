namespace _14Interpreter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 解释器模式 (Interpreter Pattern) ===\n");
            Console.WriteLine("实际案例：WMS仓库管理系统规则引擎\n");

            // 示例1：WMS查询语言和规则引擎
            Console.WriteLine("示例1：WMS库存查询语言");
            Console.WriteLine("------------------------");
            RunWMSQueryExample();
            
            Console.WriteLine("\n示例2：WMS规则引擎");
            Console.WriteLine("------------------------");
            RunWMSRuleEngineExample();
        }

        static void RunWMSQueryExample()
        {
            // 初始化WMS上下文和库存数据
            var context = new WMSContext();
            InitializeInventory(context);

            // 查询位置A-01的所有商品
            var locationQuery = new LocationExpression("A-01");
            var result1 = locationQuery.Interpret(context);
            PrintInventoryResult("位置 A-01 的商品", result1);

            // 查询库存低于20的商品
            var lowStockQuery = new LowStockExpression(20);
            var result2 = lowStockQuery.Interpret(context);
            PrintInventoryResult("\n库存预警商品 (< 20)", result2);

            // 组合查询：电子产品类别 AND 库存低于50
            var complexQuery = new AndExpression(
                new CategoryExpression("电子产品"),
                new LowStockExpression(50)
            );
            var result3 = complexQuery.Interpret(context);
            PrintInventoryResult("\n电子产品低库存", result3);

            // 库位优化分析
            Console.WriteLine("\n库位优化建议:");
            var optimizer = new LocationOptimizationExpression();
            optimizer.Interpret(context);
        }

        static void RunWMSRuleEngineExample()
        {
            var context = new WMSContext();
            InitializeInventory(context);
            var ruleEngine = new WMSRuleEngine();

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
                Console.WriteLine($"  A区需要补货的商品数: {areaResult.Count}");
            }
        }

        static void InitializeInventory(WMSContext context)
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

        static void PrintInventoryResult(string title, List<InventoryItem> items)
        {
            Console.WriteLine($"\n{title}:");
            if (items.Any())
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"  {item.SKU} | {item.Name} | 库存:{item.Quantity} | 位置:{item.Location}");
                }
            }
            else
            {
                Console.WriteLine("  没有找到匹配的商品");
            }
        }

        static void RunMathExpressionExample()
        {
            // 构建表达式树: (5 + 3) * (10 - 2)
            var expression = new MultiplyExpression(
                new AddExpression(
                    new NumberExpression(5),
                    new NumberExpression(3)
                ),
                new SubtractExpression(
                    new NumberExpression(10),
                    new NumberExpression(2)
                )
            );

            var context = new MathContext();
            int result = expression.Interpret(context);
            Console.WriteLine($"表达式: (5 + 3) * (10 - 2) = {result}");

            // 另一个表达式: 20 / (5 + 5)
            var expression2 = new DivideExpression(
                new NumberExpression(20),
                new AddExpression(
                    new NumberExpression(5),
                    new NumberExpression(5)
                )
            );
            int result2 = expression2.Interpret(context);
            Console.WriteLine($"表达式: 20 / (5 + 5) = {result2}");
        }

        static void RunSqlQueryExample()
        {
            // 模拟数据库表
            var database = new Database();
            database.AddTable("users", new List<Dictionary<string, object>>
            {
                new() { ["id"] = 1, ["name"] = "张三", ["age"] = 25, ["city"] = "北京" },
                new() { ["id"] = 2, ["name"] = "李四", ["age"] = 30, ["city"] = "上海" },
                new() { ["id"] = 3, ["name"] = "王五", ["age"] = 28, ["city"] = "北京" },
                new() { ["id"] = 4, ["name"] = "赵六", ["age"] = 35, ["city"] = "广州" }
            });

            // SQL查询1: SELECT * FROM users WHERE age > 25
            Console.WriteLine("查询1: SELECT * FROM users WHERE age > 25");
            var query1 = new SelectQuery(
                "users",
                new List<string> { "*" },
                new GreaterThanCondition("age", 25)
            );
            var result1 = query1.Execute(database);
            PrintQueryResult(result1);

            // SQL查询2: SELECT name, city FROM users WHERE city = '北京'
            Console.WriteLine("\n查询2: SELECT name, city FROM users WHERE city = '北京'");
            var query2 = new SelectQuery(
                "users",
                new List<string> { "name", "city" },
                new EqualsCondition("city", "北京")
            );
            var result2 = query2.Execute(database);
            PrintQueryResult(result2);

            // SQL查询3: SELECT * FROM users WHERE age > 25 AND city = '北京'
            Console.WriteLine("\n查询3: SELECT * FROM users WHERE age > 25 AND city = '北京'");
            var query3 = new SelectQuery(
                "users",
                new List<string> { "*" },
                new AndCondition(
                    new GreaterThanCondition("age", 25),
                    new EqualsCondition("city", "北京")
                )
            );
            var result3 = query3.Execute(database);
            PrintQueryResult(result3);
        }

        static void PrintQueryResult(List<Dictionary<string, object>> results)
        {
            if (results.Count == 0)
            {
                Console.WriteLine("没有找到匹配的记录");
                return;
            }

            // 打印表头
            var headers = results[0].Keys.ToList();
            Console.WriteLine(string.Join(" | ", headers.Select(h => h.PadRight(10))));
            Console.WriteLine(new string('-', headers.Count * 13));

            // 打印数据
            foreach (var row in results)
            {
                var values = headers.Select(h => row[h]?.ToString()?.PadRight(10) ?? "NULL".PadRight(10));
                Console.WriteLine(string.Join(" | ", values));
            }
        }
    }
}