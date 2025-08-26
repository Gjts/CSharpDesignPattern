namespace _14Interpreter
{
    // 上下文类，用于存储解释器需要的信息
    public class MathContext
    {
        private Dictionary<string, int> _variables = new Dictionary<string, int>();

        public void SetVariable(string name, int value)
        {
            _variables[name] = value;
        }

        public int GetVariable(string name)
        {
            return _variables.ContainsKey(name) ? _variables[name] : 0;
        }
    }

    // 抽象表达式接口
    public interface IMathExpression
    {
        int Interpret(MathContext context);
    }

    // 终结符表达式：数字
    public class NumberExpression : IMathExpression
    {
        private int _number;

        public NumberExpression(int number)
        {
            _number = number;
        }

        public int Interpret(MathContext context)
        {
            return _number;
        }
    }

    // 终结符表达式：变量
    public class VariableExpression : IMathExpression
    {
        private string _name;

        public VariableExpression(string name)
        {
            _name = name;
        }

        public int Interpret(MathContext context)
        {
            return context.GetVariable(_name);
        }
    }

    // 非终结符表达式：加法
    public class AddExpression : IMathExpression
    {
        private IMathExpression _left;
        private IMathExpression _right;

        public AddExpression(IMathExpression left, IMathExpression right)
        {
            _left = left;
            _right = right;
        }

        public int Interpret(MathContext context)
        {
            return _left.Interpret(context) + _right.Interpret(context);
        }
    }

    // 非终结符表达式：减法
    public class SubtractExpression : IMathExpression
    {
        private IMathExpression _left;
        private IMathExpression _right;

        public SubtractExpression(IMathExpression left, IMathExpression right)
        {
            _left = left;
            _right = right;
        }

        public int Interpret(MathContext context)
        {
            return _left.Interpret(context) - _right.Interpret(context);
        }
    }

    // 非终结符表达式：乘法
    public class MultiplyExpression : IMathExpression
    {
        private IMathExpression _left;
        private IMathExpression _right;

        public MultiplyExpression(IMathExpression left, IMathExpression right)
        {
            _left = left;
            _right = right;
        }

        public int Interpret(MathContext context)
        {
            return _left.Interpret(context) * _right.Interpret(context);
        }
    }

    // 非终结符表达式：除法
    public class DivideExpression : IMathExpression
    {
        private IMathExpression _left;
        private IMathExpression _right;

        public DivideExpression(IMathExpression left, IMathExpression right)
        {
            _left = left;
            _right = right;
        }

        public int Interpret(MathContext context)
        {
            int rightValue = _right.Interpret(context);
            if (rightValue == 0)
            {
                throw new DivideByZeroException("除数不能为零");
            }
            return _left.Interpret(context) / rightValue;
        }
    }
}