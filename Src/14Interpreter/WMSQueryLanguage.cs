namespace _14Interpreter
{
    // WMS 查询语言解释器 - 用于仓库规则引擎
    
    // 库存项
    public class InventoryItem
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
        public DateTime LastUpdated { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public InventoryItem(string sku, string name, int quantity, string location, decimal price, string category)
        {
            SKU = sku;
            Name = name;
            Quantity = quantity;
            Location = location;
            Price = price;
            Category = category;
            LastUpdated = DateTime.Now;
        }
    }

    // WMS上下文
    public class WMSContext
    {
        public List<InventoryItem> Inventory { get; set; }
        public Dictionary<string, object> Variables { get; set; }

        public WMSContext()
        {
            Inventory = new List<InventoryItem>();
            Variables = new Dictionary<string, object>();
        }
    }

    // 抽象表达式
    public interface IWMSExpression
    {
        List<InventoryItem> Interpret(WMSContext context);
    }

    // 查询所有库存
    public class AllItemsExpression : IWMSExpression
    {
        public List<InventoryItem> Interpret(WMSContext context)
        {
            Console.WriteLine("  执行: 查询所有库存");
            return context.Inventory;
        }
    }

    // 按位置查询
    public class LocationExpression : IWMSExpression
    {
        private string _location;

        public LocationExpression(string location)
        {
            _location = location;
        }

        public List<InventoryItem> Interpret(WMSContext context)
        {
            Console.WriteLine($"  执行: 查询位置 [{_location}] 的库存");
            return context.Inventory.Where(i => i.Location == _location).ToList();
        }
    }

    // 低库存查询
    public class LowStockExpression : IWMSExpression
    {
        private int _threshold;

        public LowStockExpression(int threshold)
        {
            _threshold = threshold;
        }

        public List<InventoryItem> Interpret(WMSContext context)
        {
            Console.WriteLine($"  执行: 查询库存量低于 {_threshold} 的商品");
            return context.Inventory.Where(i => i.Quantity < _threshold).ToList();
        }
    }

    // 类别查询
    public class CategoryExpression : IWMSExpression
    {
        private string _category;

        public CategoryExpression(string category)
        {
            _category = category;
        }

        public List<InventoryItem> Interpret(WMSContext context)
        {
            Console.WriteLine($"  执行: 查询类别 [{_category}] 的商品");
            return context.Inventory.Where(i => i.Category == _category).ToList();
        }
    }

    // 组合表达式 - AND
    public class AndExpression : IWMSExpression
    {
        private IWMSExpression _left;
        private IWMSExpression _right;

        public AndExpression(IWMSExpression left, IWMSExpression right)
        {
            _left = left;
            _right = right;
        }

        public List<InventoryItem> Interpret(WMSContext context)
        {
            var leftResult = _left.Interpret(context);
            var rightResult = _right.Interpret(context);
            Console.WriteLine("  执行: AND 操作");
            return leftResult.Intersect(rightResult).ToList();
        }
    }

    // 组合表达式 - OR
    public class OrExpression : IWMSExpression
    {
        private IWMSExpression _left;
        private IWMSExpression _right;

        public OrExpression(IWMSExpression left, IWMSExpression right)
        {
            _left = left;
            _right = right;
        }

        public List<InventoryItem> Interpret(WMSContext context)
        {
            var leftResult = _left.Interpret(context);
            var rightResult = _right.Interpret(context);
            Console.WriteLine("  执行: OR 操作");
            return leftResult.Union(rightResult).ToList();
        }
    }

    // WMS规则引擎
    public class WMSRuleEngine
    {
        private Dictionary<string, IWMSExpression> _rules = new();

        public void AddRule(string ruleName, IWMSExpression expression)
        {
            _rules[ruleName] = expression;
            Console.WriteLine($"规则引擎: 添加规则 [{ruleName}]");
        }

        public List<InventoryItem> ExecuteRule(string ruleName, WMSContext context)
        {
            if (_rules.ContainsKey(ruleName))
            {
                Console.WriteLine($"\n执行规则: {ruleName}");
                return _rules[ruleName].Interpret(context);
            }
            return new List<InventoryItem>();
        }
    }

    // 补货规则表达式
    public class ReplenishmentRuleExpression : IWMSExpression
    {
        private int _minStock;
        private string _category;

        public ReplenishmentRuleExpression(int minStock, string category)
        {
            _minStock = minStock;
            _category = category;
        }

        public List<InventoryItem> Interpret(WMSContext context)
        {
            Console.WriteLine($"  执行补货规则: 类别={_category}, 最小库存={_minStock}");
            var items = context.Inventory
                .Where(i => i.Category == _category && i.Quantity < _minStock)
                .ToList();
            
            foreach (var item in items)
            {
                var replenishQuantity = _minStock * 2 - item.Quantity;
                Console.WriteLine($"    需要补货: {item.SKU} - {item.Name}, 建议补货量: {replenishQuantity}");
            }
            
            return items;
        }
    }

    // 库位优化表达式
    public class LocationOptimizationExpression : IWMSExpression
    {
        public List<InventoryItem> Interpret(WMSContext context)
        {
            Console.WriteLine("  执行库位优化分析");
            
            // 按类别和使用频率分组
            var categoryGroups = context.Inventory.GroupBy(i => i.Category);
            
            foreach (var group in categoryGroups)
            {
                Console.WriteLine($"    类别 [{group.Key}]:");
                var hotItems = group.Where(i => i.Quantity > 100).ToList();
                if (hotItems.Any())
                {
                    Console.WriteLine($"      热门商品 (建议移至A区): {string.Join(", ", hotItems.Select(i => i.SKU))}");
                }
                
                var coldItems = group.Where(i => i.Quantity < 10).ToList();
                if (coldItems.Any())
                {
                    Console.WriteLine($"      冷门商品 (建议移至C区): {string.Join(", ", coldItems.Select(i => i.SKU))}");
                }
            }
            
            return context.Inventory;
        }
    }
}