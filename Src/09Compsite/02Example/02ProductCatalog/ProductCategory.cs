using System.Linq;

namespace _09Compsite._02Example._02ProductCatalog
{
    // 商品分类组件抽象类
    public abstract class CategoryComponent
    {
        protected string id;
        protected string name;
        protected string description;

        public CategoryComponent(string id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        public abstract void Add(CategoryComponent component);
        public abstract void Remove(CategoryComponent component);
        public abstract void Display(int depth);
        public abstract int GetProductCount();
        public abstract decimal GetTotalSales();
        public abstract List<Product> GetAllProducts();
    }

    // 商品类（叶子节点）
    public class Product : CategoryComponent
    {
        public decimal price { get; private set; }
        public int stock { get; private set; }
        public int salesCount { get; private set; }
        public string sku { get; private set; }
        public string brand { get; private set; }

        public Product(string id, string name, string description, decimal price, int stock, string sku, string brand) 
            : base(id, name, description)
        {
            this.price = price;
            this.stock = stock;
            this.sku = sku;
            this.brand = brand;
            this.salesCount = new Random().Next(0, 1000); // 模拟销量
        }

        public override void Add(CategoryComponent component)
        {
            throw new NotSupportedException("商品不能添加子项");
        }

        public override void Remove(CategoryComponent component)
        {
            throw new NotSupportedException("商品不能移除子项");
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string(' ', depth) + 
                $"🛍 商品: {name} | SKU: {sku} | 品牌: {brand} | " +
                $"价格: ¥{price:F2} | 库存: {stock} | 销量: {salesCount}");
        }

        public override int GetProductCount()
        {
            return 1;
        }

        public override decimal GetTotalSales()
        {
            return price * salesCount;
        }

        public override List<Product> GetAllProducts()
        {
            return new List<Product> { this };
        }
    }

    // 商品分类（组合节点）
    public class Category : CategoryComponent
    {
        private List<CategoryComponent> children = new List<CategoryComponent>();
        private int level; // 分类级别：1-一级分类，2-二级分类，3-三级分类

        public Category(string id, string name, string description, int level) 
            : base(id, name, description)
        {
            this.level = level;
        }

        public override void Add(CategoryComponent component)
        {
            children.Add(component);
        }

        public override void Remove(CategoryComponent component)
        {
            children.Remove(component);
        }

        public override void Display(int depth)
        {
            string levelName = level switch
            {
                1 => "一级分类",
                2 => "二级分类",
                3 => "三级分类",
                _ => "分类"
            };

            Console.WriteLine(new string(' ', depth) + 
                $"📂 {levelName}: {name} | " +
                $"商品数: {GetProductCount()} | " +
                $"总销售额: ¥{GetTotalSales():F2}");
            
            if (!string.IsNullOrEmpty(description))
            {
                Console.WriteLine(new string(' ', depth + 2) + $"描述: {description}");
            }

            foreach (var child in children)
            {
                child.Display(depth + 2);
            }
        }

        public override int GetProductCount()
        {
            int count = 0;
            foreach (var child in children)
            {
                count += child.GetProductCount();
            }
            return count;
        }

        public override decimal GetTotalSales()
        {
            decimal total = 0;
            foreach (var child in children)
            {
                total += child.GetTotalSales();
            }
            return total;
        }

        public override List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            foreach (var child in children)
            {
                products.AddRange(child.GetAllProducts());
            }
            return products;
        }

        // 获取热销商品
        public List<Product> GetTopSellingProducts(int topN = 5)
        {
            var allProducts = GetAllProducts();
            return allProducts.OrderByDescending(p => p.salesCount).Take(topN).ToList();
        }

        // 生成分类销售报告
        public void GenerateSalesReport()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine($"分类销售报告 - {name}");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"生成时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"分类描述: {description}");
            Console.WriteLine(new string('-', 60));
            
            // 统计子分类
            var subCategories = children.Where(c => c is Category).ToList();
            if (subCategories.Any())
            {
                Console.WriteLine("\n子分类销售情况:");
                foreach (var subCategory in subCategories)
                {
                    Console.WriteLine($"  {subCategory.name}:");
                    Console.WriteLine($"    商品数量: {subCategory.GetProductCount()}");
                    Console.WriteLine($"    销售总额: ¥{subCategory.GetTotalSales():F2}");
                }
            }

            // 热销商品TOP5
            var topProducts = GetTopSellingProducts(5);
            if (topProducts.Any())
            {
                Console.WriteLine("\n热销商品TOP5:");
                int rank = 1;
                foreach (var product in topProducts)
                {
                    Console.WriteLine($"  {rank}. {product.name} - 销量: {product.salesCount} - 销售额: ¥{product.GetTotalSales():F2}");
                    rank++;
                }
            }

            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"汇总:");
            Console.WriteLine($"  总商品数: {GetProductCount()}");
            Console.WriteLine($"  总销售额: ¥{GetTotalSales():F2}");
            Console.WriteLine($"  平均单品销售额: ¥{(GetProductCount() > 0 ? GetTotalSales() / GetProductCount() : 0):F2}");
            Console.WriteLine(new string('=', 60));
        }
    }

    // 电商平台商品管理系统
    public class ECommerceCatalog
    {
        private Category rootCategory;

        public ECommerceCatalog(string platformName)
        {
            rootCategory = new Category("ROOT", platformName + "商品目录", "平台所有商品分类", 0);
        }

        public void AddCategory(CategoryComponent category)
        {
            rootCategory.Add(category);
        }

        public void RemoveCategory(CategoryComponent category)
        {
            rootCategory.Remove(category);
        }

        public void DisplayCatalog()
        {
            rootCategory.Display(0);
        }

        public void GeneratePlatformReport()
        {
            Console.WriteLine("\n" + new string('*', 70));
            Console.WriteLine("平台商品统计报告");
            Console.WriteLine(new string('*', 70));
            rootCategory.GenerateSalesReport();
        }

        // 搜索商品
        public List<Product> SearchProducts(string keyword)
        {
            var allProducts = rootCategory.GetAllProducts();
            return allProducts.Where(p => 
                p.name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                p.description.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                p.brand.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        // 获取低库存商品
        public List<Product> GetLowStockProducts(int threshold = 10)
        {
            var allProducts = rootCategory.GetAllProducts();
            return allProducts.Where(p => p.stock < threshold).ToList();
        }
    }
}
