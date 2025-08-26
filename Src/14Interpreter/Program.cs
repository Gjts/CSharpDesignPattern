namespace _14Interpreter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 解释器模式 (Interpreter Pattern) ===\n");

            // 示例1：数学表达式计算器
            Console.WriteLine("示例1：数学表达式计算器");
            Console.WriteLine("------------------------");
            RunMathExpressionExample();
            
            Console.WriteLine("\n示例2：简单SQL查询解析器");
            Console.WriteLine("------------------------");
            RunSqlQueryExample();
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