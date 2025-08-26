namespace _14Interpreter._02Example.Calculator
{
    // 抽象表达式
    public abstract class Expression
    {
        public abstract double Interpret(Dictionary<string, double> context);
    }

    // 终结符表达式 - 数字
    public class NumberExpression : Expression
    {
        private double _value;

        public NumberExpression(double value)
        {
            _value = value;
        }

        public override double Interpret(Dictionary<string, double> context)
        {
            return _value;
        }
    }

    // 终结符表达式 - 变量
    public class VariableExpression : Expression
    {
        private string _name;

        public VariableExpression(string name)
        {
            _name = name;
        }

        public override double Interpret(Dictionary<string, double> context)
        {
            if (context.ContainsKey(_name))
            {
                return context[_name];
            }
            throw new ArgumentException($"变量 '{_name}' 未定义");
        }
    }

    // 非终结符表达式 - 加法
    public class AddExpression : Expression
    {
        private Expression _left;
        private Expression _right;

        public AddExpression(Expression left, Expression right)
        {
            _left = left;
            _right = right;
        }

        public override double Interpret(Dictionary<string, double> context)
        {
            return _left.Interpret(context) + _right.Interpret(context);
        }
    }

    // 非终结符表达式 - 减法
    public class SubtractExpression : Expression
    {
        private Expression _left;
        private Expression _right;

        public SubtractExpression(Expression left, Expression right)
        {
            _left = left;
            _right = right;
        }

        public override double Interpret(Dictionary<string, double> context)
        {
            return _left.Interpret(context) - _right.Interpret(context);
        }
    }

    // 非终结符表达式 - 乘法
    public class MultiplyExpression : Expression
    {
        private Expression _left;
        private Expression _right;

        public MultiplyExpression(Expression left, Expression right)
        {
            _left = left;
            _right = right;
        }

        public override double Interpret(Dictionary<string, double> context)
        {
            return _left.Interpret(context) * _right.Interpret(context);
        }
    }

    // 非终结符表达式 - 除法
    public class DivideExpression : Expression
    {
        private Expression _left;
        private Expression _right;

        public DivideExpression(Expression left, Expression right)
        {
            _left = left;
            _right = right;
        }

        public override double Interpret(Dictionary<string, double> context)
        {
            double rightValue = _right.Interpret(context);
            if (rightValue == 0)
            {
                throw new DivideByZeroException("除数不能为零");
            }
            return _left.Interpret(context) / rightValue;
        }
    }

    // 非终结符表达式 - 幂运算
    public class PowerExpression : Expression
    {
        private Expression _base;
        private Expression _exponent;

        public PowerExpression(Expression baseExpr, Expression exponent)
        {
            _base = baseExpr;
            _exponent = exponent;
        }

        public override double Interpret(Dictionary<string, double> context)
        {
            return Math.Pow(_base.Interpret(context), _exponent.Interpret(context));
        }
    }

    // 非终结符表达式 - 平方根
    public class SquareRootExpression : Expression
    {
        private Expression _operand;

        public SquareRootExpression(Expression operand)
        {
            _operand = operand;
        }

        public override double Interpret(Dictionary<string, double> context)
        {
            double value = _operand.Interpret(context);
            if (value < 0)
            {
                throw new ArgumentException("平方根的参数不能为负数");
            }
            return Math.Sqrt(value);
        }
    }

    // 表达式解析器
    public class ExpressionParser
    {
        public static Expression Parse(string expression)
        {
            // 简化的解析器，仅用于演示
            // 实际应用中需要更复杂的词法分析和语法分析
            
            expression = expression.Replace(" ", "");
            
            // 示例：解析简单的二元运算
            if (expression.Contains("+"))
            {
                var parts = expression.Split('+');
                return new AddExpression(ParseOperand(parts[0]), ParseOperand(parts[1]));
            }
            else if (expression.Contains("-") && expression.LastIndexOf("-") > 0)
            {
                var index = expression.LastIndexOf("-");
                var left = expression.Substring(0, index);
                var right = expression.Substring(index + 1);
                return new SubtractExpression(ParseOperand(left), ParseOperand(right));
            }
            else if (expression.Contains("*"))
            {
                var parts = expression.Split('*');
                return new MultiplyExpression(ParseOperand(parts[0]), ParseOperand(parts[1]));
            }
            else if (expression.Contains("/"))
            {
                var parts = expression.Split('/');
                return new DivideExpression(ParseOperand(parts[0]), ParseOperand(parts[1]));
            }
            else if (expression.Contains("^"))
            {
                var parts = expression.Split('^');
                return new PowerExpression(ParseOperand(parts[0]), ParseOperand(parts[1]));
            }
            else if (expression.StartsWith("sqrt(") && expression.EndsWith(")"))
            {
                var inner = expression.Substring(5, expression.Length - 6);
                return new SquareRootExpression(ParseOperand(inner));
            }
            
            return ParseOperand(expression);
        }

        private static Expression ParseOperand(string operand)
        {
            operand = operand.Trim();
            
            if (double.TryParse(operand, out double value))
            {
                return new NumberExpression(value);
            }
            else
            {
                return new VariableExpression(operand);
            }
        }
    }

    // 计算器上下文
    public class CalculatorContext
    {
        private Dictionary<string, double> _variables;

        public CalculatorContext()
        {
            _variables = new Dictionary<string, double>();
        }

        public void SetVariable(string name, double value)
        {
            _variables[name] = value;
            Console.WriteLine($"  设置变量: {name} = {value}");
        }

        public double Evaluate(string expression)
        {
            Console.WriteLine($"\n计算表达式: {expression}");
            
            try
            {
                var expr = ExpressionParser.Parse(expression);
                double result = expr.Interpret(_variables);
                Console.WriteLine($"  结果: {result}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ❌ 错误: {ex.Message}");
                return double.NaN;
            }
        }

        public void ShowVariables()
        {
            Console.WriteLine("\n当前变量:");
            foreach (var kvp in _variables)
            {
                Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
            }
        }
    }
}
