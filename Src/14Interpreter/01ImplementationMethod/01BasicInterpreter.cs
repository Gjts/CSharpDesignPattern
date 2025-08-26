namespace _14Interpreter._01ImplementationMethod
{
    // 1. 基础解释器模式
    // 适用场景：当有一个语言需要解释执行，并且可以将该语言中的句子表示为一个抽象语法树时
    // 特点：定义语言的文法，并建立一个解释器来解释该语言中的句子
    // 优点：易于扩展文法，实现简单
    // 缺点：对于复杂文法难以维护，执行效率不高

    /// <summary>
    /// 抽象表达式 - 声明一个抽象的解释操作
    /// </summary>
    public abstract class AbstractExpression
    {
        public abstract int Interpret(Context context);
    }

    /// <summary>
    /// 终结符表达式 - 实现与文法中的终结符相关的解释操作
    /// </summary>
    public class TerminalExpression : AbstractExpression
    {
        private string _variable;

        public TerminalExpression(string variable)
        {
            _variable = variable;
        }

        public override int Interpret(Context context)
        {
            return context.GetValue(_variable);
        }
    }

    /// <summary>
    /// 非终结符表达式 - 加法
    /// </summary>
    public class AddExpression : AbstractExpression
    {
        private AbstractExpression _left;
        private AbstractExpression _right;

        public AddExpression(AbstractExpression left, AbstractExpression right)
        {
            _left = left;
            _right = right;
        }

        public override int Interpret(Context context)
        {
            return _left.Interpret(context) + _right.Interpret(context);
        }
    }

    /// <summary>
    /// 非终结符表达式 - 减法
    /// </summary>
    public class SubtractExpression : AbstractExpression
    {
        private AbstractExpression _left;
        private AbstractExpression _right;

        public SubtractExpression(AbstractExpression left, AbstractExpression right)
        {
            _left = left;
            _right = right;
        }

        public override int Interpret(Context context)
        {
            return _left.Interpret(context) - _right.Interpret(context);
        }
    }

    /// <summary>
    /// 上下文 - 包含解释器之外的全局信息
    /// </summary>
    public class Context
    {
        private Dictionary<string, int> _variables = new Dictionary<string, int>();

        public void SetVariable(string name, int value)
        {
            _variables[name] = value;
        }

        public int GetValue(string name)
        {
            if (_variables.ContainsKey(name))
            {
                return _variables[name];
            }
            return 0;
        }
    }

    /// <summary>
    /// 使用示例
    /// </summary>
    public class InterpreterExample
    {
        public static void Demo()
        {
            // 创建上下文
            var context = new Context();
            context.SetVariable("a", 10);
            context.SetVariable("b", 20);
            context.SetVariable("c", 30);

            // 构建表达式树: a + b - c
            var expression = new SubtractExpression(
                new AddExpression(
                    new TerminalExpression("a"),
                    new TerminalExpression("b")
                ),
                new TerminalExpression("c")
            );

            // 解释执行
            int result = expression.Interpret(context);
            Console.WriteLine($"表达式 a + b - c 的结果: {result}"); // 10 + 20 - 30 = 0
        }
    }
}