namespace _24Visitor._02Example.ShoppingCart
{
    // 访问者接口
    public interface IShoppingVisitor
    {
        void VisitBook(Book book);
        void VisitElectronics(Electronics electronics);
        void VisitClothing(Clothing clothing);
        void VisitFood(Food food);
    }

    // 商品接口
    public interface IShoppingItem
    {
        void Accept(IShoppingVisitor visitor);
        string Name { get; }
        decimal Price { get; }
        int Quantity { get; }
    }

    // 具体商品 - 书籍
    public class Book : IShoppingItem
    {
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; }
        public string Author { get; }
        public string ISBN { get; }

        public Book(string name, decimal price, int quantity, string author, string isbn)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Author = author;
            ISBN = isbn;
        }

        public void Accept(IShoppingVisitor visitor)
        {
            visitor.VisitBook(this);
        }
    }

    // 具体商品 - 电子产品
    public class Electronics : IShoppingItem
    {
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; }
        public string Brand { get; }
        public int WarrantyMonths { get; }

        public Electronics(string name, decimal price, int quantity, string brand, int warrantyMonths)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Brand = brand;
            WarrantyMonths = warrantyMonths;
        }

        public void Accept(IShoppingVisitor visitor)
        {
            visitor.VisitElectronics(this);
        }
    }

    // 具体商品 - 服装
    public class Clothing : IShoppingItem
    {
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; }
        public string Size { get; }
        public string Material { get; }

        public Clothing(string name, decimal price, int quantity, string size, string material)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Size = size;
            Material = material;
        }

        public void Accept(IShoppingVisitor visitor)
        {
            visitor.VisitClothing(this);
        }
    }

    // 具体商品 - 食品
    public class Food : IShoppingItem
    {
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; }
        public DateTime ExpiryDate { get; }
        public bool IsOrganic { get; }

        public Food(string name, decimal price, int quantity, DateTime expiryDate, bool isOrganic)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            ExpiryDate = expiryDate;
            IsOrganic = isOrganic;
        }

        public void Accept(IShoppingVisitor visitor)
        {
            visitor.VisitFood(this);
        }
    }

    // 具体访问者 - 价格计算器（含折扣）
    public class PriceCalculatorVisitor : IShoppingVisitor
    {
        public decimal TotalPrice { get; private set; }
        public decimal TotalDiscount { get; private set; }

        public void VisitBook(Book book)
        {
            decimal discount = 0.1m; // 书籍10%折扣
            decimal originalPrice = book.Price * book.Quantity;
            decimal discountAmount = originalPrice * discount;
            decimal finalPrice = originalPrice - discountAmount;
            
            TotalPrice += finalPrice;
            TotalDiscount += discountAmount;
            
            Console.WriteLine($"  📚 {book.Name} x{book.Quantity}");
            Console.WriteLine($"     原价: ¥{originalPrice:F2}, 折扣: -¥{discountAmount:F2}, 实付: ¥{finalPrice:F2}");
        }

        public void VisitElectronics(Electronics electronics)
        {
            decimal discount = electronics.Price > 1000 ? 0.15m : 0.05m; // 超过1000元15%折扣，否则5%
            decimal originalPrice = electronics.Price * electronics.Quantity;
            decimal discountAmount = originalPrice * discount;
            decimal finalPrice = originalPrice - discountAmount;
            
            TotalPrice += finalPrice;
            TotalDiscount += discountAmount;
            
            Console.WriteLine($"  💻 {electronics.Name} x{electronics.Quantity}");
            Console.WriteLine($"     原价: ¥{originalPrice:F2}, 折扣: -¥{discountAmount:F2}, 实付: ¥{finalPrice:F2}");
        }

        public void VisitClothing(Clothing clothing)
        {
            decimal discount = clothing.Quantity >= 3 ? 0.2m : 0; // 买3件以上8折
            decimal originalPrice = clothing.Price * clothing.Quantity;
            decimal discountAmount = originalPrice * discount;
            decimal finalPrice = originalPrice - discountAmount;
            
            TotalPrice += finalPrice;
            TotalDiscount += discountAmount;
            
            Console.WriteLine($"  👔 {clothing.Name} x{clothing.Quantity}");
            Console.WriteLine($"     原价: ¥{originalPrice:F2}, 折扣: -¥{discountAmount:F2}, 实付: ¥{finalPrice:F2}");
        }

        public void VisitFood(Food food)
        {
            decimal discount = 0;
            // 临期食品额外折扣
            if ((food.ExpiryDate - DateTime.Now).TotalDays < 7)
            {
                discount = 0.3m; // 临期30%折扣
            }
            else if (food.IsOrganic)
            {
                discount = -0.1m; // 有机食品加价10%
            }
            
            decimal originalPrice = food.Price * food.Quantity;
            decimal discountAmount = originalPrice * discount;
            decimal finalPrice = originalPrice - discountAmount;
            
            TotalPrice += finalPrice;
            TotalDiscount += discountAmount;
            
            Console.WriteLine($"  🍎 {food.Name} x{food.Quantity}");
            Console.WriteLine($"     原价: ¥{originalPrice:F2}, {(discount > 0 ? "折扣" : "加价")}: {(discount > 0 ? "-" : "+")}¥{Math.Abs(discountAmount):F2}, 实付: ¥{finalPrice:F2}");
        }
    }

    // 具体访问者 - 库存检查器
    public class InventoryCheckerVisitor : IShoppingVisitor
    {
        private Dictionary<string, int> _inventory = new Dictionary<string, int>
        {
            { "书籍", 100 },
            { "电子产品", 50 },
            { "服装", 200 },
            { "食品", 500 }
        };

        public List<string> OutOfStockItems { get; } = new List<string>();
        public List<string> LowStockItems { get; } = new List<string>();

        public void VisitBook(Book book)
        {
            CheckStock("书籍", book.Name, book.Quantity);
        }

        public void VisitElectronics(Electronics electronics)
        {
            CheckStock("电子产品", electronics.Name, electronics.Quantity);
        }

        public void VisitClothing(Clothing clothing)
        {
            CheckStock("服装", clothing.Name, clothing.Quantity);
        }

        public void VisitFood(Food food)
        {
            CheckStock("食品", food.Name, food.Quantity);
            
            // 检查过期
            if (food.ExpiryDate < DateTime.Now)
            {
                Console.WriteLine($"  ⚠️ {food.Name} 已过期！");
            }
            else if ((food.ExpiryDate - DateTime.Now).TotalDays < 3)
            {
                Console.WriteLine($"  ⚠️ {food.Name} 即将过期（{food.ExpiryDate:yyyy-MM-dd}）");
            }
        }

        private void CheckStock(string category, string itemName, int requestedQuantity)
        {
            int availableStock = _inventory[category];
            
            if (availableStock < requestedQuantity)
            {
                OutOfStockItems.Add(itemName);
                Console.WriteLine($"  ❌ {itemName}: 库存不足（需要{requestedQuantity}，库存{availableStock}）");
            }
            else if (availableStock - requestedQuantity < 10)
            {
                LowStockItems.Add(itemName);
                Console.WriteLine($"  ⚠️ {itemName}: 库存偏低（购买后剩余{availableStock - requestedQuantity}）");
            }
            else
            {
                Console.WriteLine($"  ✅ {itemName}: 库存充足");
            }
            
            _inventory[category] = Math.Max(0, availableStock - requestedQuantity);
        }
    }

    // 具体访问者 - 配送费用计算器
    public class ShippingCalculatorVisitor : IShoppingVisitor
    {
        public decimal TotalWeight { get; private set; } // 千克
        public decimal ShippingCost { get; private set; }
        public bool RequiresSpecialHandling { get; private set; }

        public void VisitBook(Book book)
        {
            decimal weight = 0.5m * book.Quantity; // 每本书0.5kg
            TotalWeight += weight;
            Console.WriteLine($"  📚 {book.Name}: {weight}kg");
        }

        public void VisitElectronics(Electronics electronics)
        {
            decimal weight = 2.0m * electronics.Quantity; // 每个电子产品2kg
            TotalWeight += weight;
            RequiresSpecialHandling = true; // 电子产品需要特殊处理
            Console.WriteLine($"  💻 {electronics.Name}: {weight}kg (需特殊包装)");
        }

        public void VisitClothing(Clothing clothing)
        {
            decimal weight = 0.3m * clothing.Quantity; // 每件衣服0.3kg
            TotalWeight += weight;
            Console.WriteLine($"  👔 {clothing.Name}: {weight}kg");
        }

        public void VisitFood(Food food)
        {
            decimal weight = 1.0m * food.Quantity; // 每个食品1kg
            TotalWeight += weight;
            
            if ((food.ExpiryDate - DateTime.Now).TotalDays < 7)
            {
                RequiresSpecialHandling = true; // 临期食品需要快速配送
                Console.WriteLine($"  🍎 {food.Name}: {weight}kg (需冷链/快速配送)");
            }
            else
            {
                Console.WriteLine($"  🍎 {food.Name}: {weight}kg");
            }
        }

        public void CalculateShipping()
        {
            // 基础运费
            if (TotalWeight <= 1)
            {
                ShippingCost = 10;
            }
            else if (TotalWeight <= 5)
            {
                ShippingCost = 20;
            }
            else
            {
                ShippingCost = 20 + (TotalWeight - 5) * 3;
            }

            // 特殊处理费
            if (RequiresSpecialHandling)
            {
                ShippingCost += 15;
            }

            Console.WriteLine($"\n📦 配送信息:");
            Console.WriteLine($"  总重量: {TotalWeight}kg");
            Console.WriteLine($"  特殊处理: {(RequiresSpecialHandling ? "是" : "否")}");
            Console.WriteLine($"  运费: ¥{ShippingCost:F2}");
        }
    }
}
