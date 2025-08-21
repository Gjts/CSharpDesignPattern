using _09Compsite._01ImplementationMethod;
using _09Compsite._02Example._01WarehouseManagement;
using _09Compsite._02Example._02ProductCatalog;
using _09Compsite._02Example._03FileSystem;
using Directory = _09Compsite._02Example._03FileSystem.Directory;
using File = _09Compsite._02Example._03FileSystem.File;

namespace _09Compsite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 组合模式 Composite Pattern ===\n");

            // 基础实现
            Console.WriteLine("1. 基础实现：");
            Composite root = new Composite("根节点");
            root.Add(new Leaf("叶子A"));
            root.Add(new Leaf("叶子B"));

            Composite comp = new Composite("分支X");
            comp.Add(new Leaf("叶子XA"));
            comp.Add(new Leaf("叶子XB"));

            Composite comp2 = new Composite("分支XY");
            comp2.Add(new Leaf("叶子XYA"));
            comp.Add(comp2);

            root.Add(comp);
            root.Add(new Leaf("叶子C"));

            root.Display(1);

            // WMS仓库管理系统示例
            Console.WriteLine("\n2. WMS仓库管理系统示例：");
            Console.WriteLine(new string('-', 60));
            
            // 创建仓库
            Warehouse warehouse = new Warehouse("WH001", "华东配送中心", "上海市浦东新区", "张经理");
            
            // 创建常温库区
            WarehouseZone normalZone = new WarehouseZone("Z001", "常温存储区", "常温");
            
            // 创建货架并添加商品
            Shelf shelf1 = new Shelf("S001", "A区1号货架", 10, "A-01");
            shelf1.Add(new SKU("SKU001", "iPhone 15 Pro", 50, 8999.00m, "693123456789"));
            shelf1.Add(new SKU("SKU002", "iPad Pro 12.9", 30, 9299.00m, "693123456790"));
            shelf1.Add(new SKU("SKU003", "MacBook Pro 14", 20, 14999.00m, "693123456791"));
            
            Shelf shelf2 = new Shelf("S002", "A区2号货架", 10, "A-02");
            shelf2.Add(new SKU("SKU004", "AirPods Pro", 100, 1999.00m, "693123456792"));
            shelf2.Add(new SKU("SKU005", "Apple Watch Ultra", 40, 6299.00m, "693123456793"));
            
            normalZone.Add(shelf1);
            normalZone.Add(shelf2);
            
            // 创建冷藏库区
            WarehouseZone coldZone = new WarehouseZone("Z002", "冷藏存储区", "冷藏");
            
            Shelf shelf3 = new Shelf("S003", "B区1号货架", 8, "B-01");
            shelf3.Add(new SKU("SKU006", "进口牛奶", 200, 89.00m, "693123456794"));
            shelf3.Add(new SKU("SKU007", "新鲜水果礼盒", 50, 299.00m, "693123456795"));
            
            coldZone.Add(shelf3);
            
            // 将库区添加到仓库
            warehouse.Add(normalZone);
            warehouse.Add(coldZone);
            
            // 显示仓库结构
            warehouse.Display(0);
            
            // 生成盘点报告
            warehouse.GenerateInventoryReport();

            // 电商商品分类系统示例
            Console.WriteLine("\n3. 电商商品分类系统示例：");
            Console.WriteLine(new string('-', 60));
            
            // 创建电商平台目录
            ECommerceCatalog catalog = new ECommerceCatalog("京东");
            
            // 创建一级分类：电子产品
            Category electronics = new Category("C001", "电子产品", "各类电子设备和配件", 1);
            
            // 创建二级分类：手机通讯
            Category phones = new Category("C001-1", "手机通讯", "智能手机及配件", 2);
            phones.Add(new Product("P001", "iPhone 15 Pro Max", "苹果最新旗舰", 9999.00m, 100, "IP15PM", "Apple"));
            phones.Add(new Product("P002", "Samsung Galaxy S24", "三星旗舰", 7999.00m, 80, "SGS24", "Samsung"));
            phones.Add(new Product("P003", "小米14 Pro", "小米高端机型", 4999.00m, 150, "MI14P", "Xiaomi"));
            
            // 创建二级分类：电脑办公
            Category computers = new Category("C001-2", "电脑办公", "笔记本、台式机及办公设备", 2);
            computers.Add(new Product("P004", "MacBook Air M3", "轻薄笔记本", 8999.00m, 50, "MBA-M3", "Apple"));
            computers.Add(new Product("P005", "ThinkPad X1 Carbon", "商务笔记本", 12999.00m, 30, "X1C", "Lenovo"));
            computers.Add(new Product("P006", "戴尔XPS 13", "高端超极本", 9999.00m, 40, "XPS13", "Dell"));
            
            electronics.Add(phones);
            electronics.Add(computers);
            
            // 创建一级分类：服装
            Category clothing = new Category("C002", "服装", "男女服装及配饰", 1);
            
            // 创建二级分类：男装
            Category mensClothing = new Category("C002-1", "男装", "男士服装", 2);
            mensClothing.Add(new Product("P007", "优衣库羽绒服", "轻薄保暖", 599.00m, 200, "UQ-DJ01", "Uniqlo"));
            mensClothing.Add(new Product("P008", "Nike运动套装", "运动休闲", 899.00m, 150, "NK-SP01", "Nike"));
            
            // 创建二级分类：女装
            Category womensClothing = new Category("C002-2", "女装", "女士服装", 2);
            womensClothing.Add(new Product("P009", "ZARA连衣裙", "时尚优雅", 399.00m, 100, "ZR-DR01", "ZARA"));
            womensClothing.Add(new Product("P010", "H&M毛衣", "舒适保暖", 299.00m, 120, "HM-SW01", "H&M"));
            
            clothing.Add(mensClothing);
            clothing.Add(womensClothing);
            
            // 添加到目录
            catalog.AddCategory(electronics);
            catalog.AddCategory(clothing);
            
            // 显示商品目录
            catalog.DisplayCatalog();
            
            // 生成销售报告
            electronics.GenerateSalesReport();
            
            // 搜索商品
            Console.WriteLine("\n搜索关键词 'Apple' 的商品:");
            var searchResults = catalog.SearchProducts("Apple");
            foreach (var product in searchResults)
            {
                Console.WriteLine($"  找到: {product.Name} - ¥{product.price:F2}");
            }
            
            // 查找低库存商品
            Console.WriteLine("\n低库存预警（库存<50）:");
            var lowStockProducts = catalog.GetLowStockProducts(50);
            foreach (var product in lowStockProducts)
            {
                Console.WriteLine($"  ⚠ {product.Name} - 库存: {product.stock}");
            }

            // 文件系统示例
            Console.WriteLine("\n4. 文件系统示例：");
            Console.WriteLine(new string('-', 60));
            
            // 创建根目录
            var rootDir = new Directory("project", "admin");
            
            // 创建src目录
            var src = new Directory("src", "developer");
            src.Add(new File("main.cs", 2048, "developer"));
            src.Add(new File("utils.cs", 1024, "developer"));
            src.Add(new File("config.json", 512, "developer"));
            
            // 创建子目录
            var models = new Directory("models", "developer");
            models.Add(new File("User.cs", 1536, "developer"));
            models.Add(new File("Product.cs", 1280, "developer"));
            models.Add(new File("Order.cs", 1920, "developer"));
            src.Add(models);
            
            // 创建docs目录
            var docs = new Directory("docs", "writer");
            docs.Add(new File("README.md", 4096, "writer"));
            docs.Add(new File("API.pdf", 10240, "writer"));
            docs.Add(new File("设计文档.docx", 8192, "writer"));
            
            // 创建resources目录
            var resources = new Directory("resources", "designer");
            resources.Add(new File("logo.png", 5120, "designer"));
            resources.Add(new File("banner.jpg", 15360, "designer"));
            resources.Add(new File("style.css", 2048, "designer"));
            
            // 添加到根目录
            rootDir.Add(src);
            rootDir.Add(docs);
            rootDir.Add(resources);
            rootDir.Add(new File(".gitignore", 256, "admin"));
            rootDir.Add(new File("LICENSE", 1024, "admin"));
            
            // 显示目录结构
            rootDir.Display(0);
            
            // 搜索文件
            Console.WriteLine("\n搜索包含 'cs' 的文件:");
            var csFiles = rootDir.SearchFiles("cs");
            foreach (var file in csFiles)
            {
                Console.WriteLine($"  找到: {file.Name}");
            }
            
            // 生成目录树报告
            rootDir.GenerateTreeReport();

            Console.ReadLine();
        }
    }
}