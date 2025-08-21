namespace _09Compsite._02Example._01WarehouseManagement
{
    // 仓库存储单元抽象类
    public abstract class StorageUnit
    {
        protected string code;
        protected string name;
        protected string type;

        public StorageUnit(string code, string name, string type)
        {
            this.code = code;
            this.name = name;
            this.type = type;
        }

        // 添加公共属性来访问 name
        public string Name => name;

        public abstract void Add(StorageUnit unit);
        public abstract void Remove(StorageUnit unit);
        public abstract void Display(int depth);
        public abstract int GetTotalSKU();
        public abstract int GetTotalQuantity();
        public abstract decimal GetTotalValue();
    }

    // 商品SKU（叶子节点）
    public class SKU : StorageUnit
    {
        private int quantity;
        private decimal unitPrice;
        private string barcode;

        public SKU(string code, string name, int quantity, decimal unitPrice, string barcode) 
            : base(code, name, "SKU")
        {
            this.quantity = quantity;
            this.unitPrice = unitPrice;
            this.barcode = barcode;
        }

        public override void Add(StorageUnit unit)
        {
            throw new NotSupportedException("SKU不能添加子单元");
        }

        public override void Remove(StorageUnit unit)
        {
            throw new NotSupportedException("SKU不能移除子单元");
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string(' ', depth) + 
                $"📦 SKU: {code} - {name} | 数量: {quantity} | 单价: ¥{unitPrice:F2} | 条码: {barcode}");
        }

        public override int GetTotalSKU()
        {
            return 1;
        }

        public override int GetTotalQuantity()
        {
            return quantity;
        }

        public override decimal GetTotalValue()
        {
            return quantity * unitPrice;
        }
    }

    // 货架（组合节点）
    public class Shelf : StorageUnit
    {
        private List<StorageUnit> items = new List<StorageUnit>();
        private int capacity;
        private string location;

        public Shelf(string code, string name, int capacity, string location) 
            : base(code, name, "货架")
        {
            this.capacity = capacity;
            this.location = location;
        }

        public override void Add(StorageUnit unit)
        {
            if (items.Count >= capacity)
            {
                Console.WriteLine($"⚠ 货架 {code} 已满，无法添加更多商品");
                return;
            }
            items.Add(unit);
        }

        public override void Remove(StorageUnit unit)
        {
            items.Remove(unit);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string(' ', depth) + 
                $"🗄 货架: {code} - {name} | 位置: {location} | 容量: {items.Count}/{capacity} | " +
                $"SKU数: {GetTotalSKU()} | 总价值: ¥{GetTotalValue():F2}");
            
            foreach (var item in items)
            {
                item.Display(depth + 2);
            }
        }

        public override int GetTotalSKU()
        {
            int total = 0;
            foreach (var item in items)
            {
                total += item.GetTotalSKU();
            }
            return total;
        }

        public override int GetTotalQuantity()
        {
            int total = 0;
            foreach (var item in items)
            {
                total += item.GetTotalQuantity();
            }
            return total;
        }

        public override decimal GetTotalValue()
        {
            decimal total = 0;
            foreach (var item in items)
            {
                total += item.GetTotalValue();
            }
            return total;
        }
    }

    // 库区（组合节点）
    public class WarehouseZone : StorageUnit
    {
        private List<StorageUnit> storageUnits = new List<StorageUnit>();
        private string zoneType; // 常温区、冷藏区、危险品区等

        public WarehouseZone(string code, string name, string zoneType) 
            : base(code, name, "库区")
        {
            this.zoneType = zoneType;
        }

        public override void Add(StorageUnit unit)
        {
            storageUnits.Add(unit);
        }

        public override void Remove(StorageUnit unit)
        {
            storageUnits.Remove(unit);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string(' ', depth) + 
                $"🏢 库区: {code} - {name} | 类型: {zoneType} | " +
                $"货架数: {storageUnits.Count} | SKU总数: {GetTotalSKU()} | " +
                $"总库存: {GetTotalQuantity()} | 总价值: ¥{GetTotalValue():F2}");
            
            foreach (var unit in storageUnits)
            {
                unit.Display(depth + 2);
            }
        }

        public override int GetTotalSKU()
        {
            int total = 0;
            foreach (var unit in storageUnits)
            {
                total += unit.GetTotalSKU();
            }
            return total;
        }

        public override int GetTotalQuantity()
        {
            int total = 0;
            foreach (var unit in storageUnits)
            {
                total += unit.GetTotalQuantity();
            }
            return total;
        }

        public override decimal GetTotalValue()
        {
            decimal total = 0;
            foreach (var unit in storageUnits)
            {
                total += unit.GetTotalValue();
            }
            return total;
        }
    }

    // 仓库（组合节点）
    public class Warehouse : StorageUnit
    {
        private List<StorageUnit> zones = new List<StorageUnit>();
        private string address;
        private string manager;

        public Warehouse(string code, string name, string address, string manager) 
            : base(code, name, "仓库")
        {
            this.address = address;
            this.manager = manager;
        }

        public override void Add(StorageUnit unit)
        {
            zones.Add(unit);
        }

        public override void Remove(StorageUnit unit)
        {
            zones.Remove(unit);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string(' ', depth) + 
                $"🏭 仓库: {code} - {name}");
            Console.WriteLine(new string(' ', depth) + 
                $"   地址: {address} | 负责人: {manager}");
            Console.WriteLine(new string(' ', depth) + 
                $"   库区数: {zones.Count} | SKU总数: {GetTotalSKU()} | " +
                $"总库存: {GetTotalQuantity()} | 总价值: ¥{GetTotalValue():F2}");
            
            foreach (var zone in zones)
            {
                zone.Display(depth + 2);
            }
        }

        public override int GetTotalSKU()
        {
            int total = 0;
            foreach (var zone in zones)
            {
                total += zone.GetTotalSKU();
            }
            return total;
        }

        public override int GetTotalQuantity()
        {
            int total = 0;
            foreach (var zone in zones)
            {
                total += zone.GetTotalQuantity();
            }
            return total;
        }

        public override decimal GetTotalValue()
        {
            decimal total = 0;
            foreach (var zone in zones)
            {
                total += zone.GetTotalValue();
            }
            return total;
        }

        // 库存盘点报告
        public void GenerateInventoryReport()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine($"仓库盘点报告 - {name} ({code})");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"盘点时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"仓库地址: {address}");
            Console.WriteLine($"负责人: {manager}");
            Console.WriteLine(new string('-', 60));
            
            foreach (var zone in zones)
            {
                Console.WriteLine($"\n库区: {zone.Name}");
                Console.WriteLine($"  SKU种类: {zone.GetTotalSKU()}");
                Console.WriteLine($"  总数量: {zone.GetTotalQuantity()}");
                Console.WriteLine($"  总价值: ¥{zone.GetTotalValue():F2}");
            }
            
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"汇总:");
            Console.WriteLine($"  总SKU种类: {GetTotalSKU()}");
            Console.WriteLine($"  总库存数量: {GetTotalQuantity()}");
            Console.WriteLine($"  总库存价值: ¥{GetTotalValue():F2}");
            Console.WriteLine(new string('=', 60));
        }
    }
}