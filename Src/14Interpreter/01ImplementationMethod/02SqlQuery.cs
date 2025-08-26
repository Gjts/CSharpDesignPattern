namespace _14Interpreter
{
    // 模拟数据库
    public class Database
    {
        private Dictionary<string, List<Dictionary<string, object>>> _tables = new();

        public void AddTable(string tableName, List<Dictionary<string, object>> data)
        {
            _tables[tableName] = data;
        }

        public List<Dictionary<string, object>> GetTable(string tableName)
        {
            return _tables.ContainsKey(tableName) ? _tables[tableName] : new List<Dictionary<string, object>>();
        }
    }

    // 抽象条件接口
    public interface ICondition
    {
        bool Evaluate(Dictionary<string, object> row);
    }

    // 等于条件
    public class EqualsCondition : ICondition
    {
        private string _field;
        private object _value;

        public EqualsCondition(string field, object value)
        {
            _field = field;
            _value = value;
        }

        public bool Evaluate(Dictionary<string, object> row)
        {
            if (!row.ContainsKey(_field))
                return false;

            return row[_field]?.ToString() == _value?.ToString();
        }
    }

    // 大于条件
    public class GreaterThanCondition : ICondition
    {
        private string _field;
        private object _value;

        public GreaterThanCondition(string field, object value)
        {
            _field = field;
            _value = value;
        }

        public bool Evaluate(Dictionary<string, object> row)
        {
            if (!row.ContainsKey(_field))
                return false;

            if (row[_field] is IComparable rowValue && _value is IComparable compareValue)
            {
                return rowValue.CompareTo(compareValue) > 0;
            }

            return false;
        }
    }

    // 小于条件
    public class LessThanCondition : ICondition
    {
        private string _field;
        private object _value;

        public LessThanCondition(string field, object value)
        {
            _field = field;
            _value = value;
        }

        public bool Evaluate(Dictionary<string, object> row)
        {
            if (!row.ContainsKey(_field))
                return false;

            if (row[_field] is IComparable rowValue && _value is IComparable compareValue)
            {
                return rowValue.CompareTo(compareValue) < 0;
            }

            return false;
        }
    }

    // AND条件
    public class AndCondition : ICondition
    {
        private ICondition _left;
        private ICondition _right;

        public AndCondition(ICondition left, ICondition right)
        {
            _left = left;
            _right = right;
        }

        public bool Evaluate(Dictionary<string, object> row)
        {
            return _left.Evaluate(row) && _right.Evaluate(row);
        }
    }

    // OR条件
    public class OrCondition : ICondition
    {
        private ICondition _left;
        private ICondition _right;

        public OrCondition(ICondition left, ICondition right)
        {
            _left = left;
            _right = right;
        }

        public bool Evaluate(Dictionary<string, object> row)
        {
            return _left.Evaluate(row) || _right.Evaluate(row);
        }
    }

    // NOT条件
    public class NotCondition : ICondition
    {
        private ICondition _condition;

        public NotCondition(ICondition condition)
        {
            _condition = condition;
        }

        public bool Evaluate(Dictionary<string, object> row)
        {
            return !_condition.Evaluate(row);
        }
    }

    // 抽象查询接口
    public interface IQuery
    {
        List<Dictionary<string, object>> Execute(Database database);
    }

    // SELECT查询
    public class SelectQuery : IQuery
    {
        private string _tableName;
        private List<string> _columns;
        private ICondition? _whereCondition;

        public SelectQuery(string tableName, List<string> columns, ICondition? whereCondition = null)
        {
            _tableName = tableName;
            _columns = columns;
            _whereCondition = whereCondition;
        }

        public List<Dictionary<string, object>> Execute(Database database)
        {
            var table = database.GetTable(_tableName);
            var result = new List<Dictionary<string, object>>();

            foreach (var row in table)
            {
                // 应用WHERE条件
                if (_whereCondition != null && !_whereCondition.Evaluate(row))
                    continue;

                // 选择列
                var selectedRow = new Dictionary<string, object>();
                if (_columns.Contains("*"))
                {
                    selectedRow = new Dictionary<string, object>(row);
                }
                else
                {
                    foreach (var column in _columns)
                    {
                        if (row.ContainsKey(column))
                        {
                            selectedRow[column] = row[column];
                        }
                    }
                }

                result.Add(selectedRow);
            }

            return result;
        }
    }
}