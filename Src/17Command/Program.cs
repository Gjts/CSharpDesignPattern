namespace _17Command
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 命令模式 (Command Pattern) ===\n");
            Console.WriteLine("实际案例：WMS仓库管理系统操作\n");

            // 示例1：基本仓库操作
            Console.WriteLine("示例1：基本仓库操作（入库、出库、移库）");
            Console.WriteLine("------------------------");
            RunBasicWarehouseOperations();

            Console.WriteLine("\n示例2：批量操作和撤销");
            Console.WriteLine("------------------------");
            RunBatchOperations();

            Console.WriteLine("\n示例3：盘点调整和操作队列");
            Console.WriteLine("------------------------");
            RunInventoryCheckAndQueue();
        }

        static void RunBasicWarehouseOperations()
        {
            var warehouse = new WarehouseSystem();
            var manager = new WMSOperationManager();

            // 入库操作
            var inbound1 = new InboundCommand(warehouse, "SKU001", 100, "A-01");
            var inbound2 = new InboundCommand(warehouse, "SKU002", 50, "B-02");
            
            manager.ExecuteCommand(inbound1);
            manager.ExecuteCommand(inbound2);

            // 出库操作
            var outbound1 = new OutboundCommand(warehouse, "SKU001", 30);
            manager.ExecuteCommand(outbound1);

            // 移库操作
            var transfer1 = new TransferCommand(warehouse, "SKU002", "B-02", "A-02");
            manager.ExecuteCommand(transfer1);

            warehouse.PrintInventory();

            // 撤销操作
            Console.WriteLine("\n撤销最后两个操作:");
            manager.Undo();
            manager.Undo();
            
            warehouse.PrintInventory();

            // 重做操作
            Console.WriteLine("\n重做一个操作:");
            manager.Redo();
            
            warehouse.PrintInventory();
        }

        static void RunBatchOperations()
        {
            var warehouse = new WarehouseSystem();
            var manager = new WMSOperationManager();

            // 初始化库存
            warehouse.AddStock("PHONE001", 50, "C-01");
            warehouse.AddStock("LAPTOP001", 30, "C-02");
            warehouse.AddStock("TABLET001", 40, "C-03");

            // 创建批量操作（订单履行）
            var orderFulfillment = new BatchCommand("订单#12345履行");
            orderFulfillment.AddCommand(new OutboundCommand(warehouse, "PHONE001", 2));
            orderFulfillment.AddCommand(new OutboundCommand(warehouse, "LAPTOP001", 1));
            orderFulfillment.AddCommand(new OutboundCommand(warehouse, "TABLET001", 3));

            Console.WriteLine("执行订单履行批量操作:");
            manager.ExecuteCommand(orderFulfillment);
            
            warehouse.PrintInventory();

            // 撤销整个批量操作
            Console.WriteLine("\n撤销整个订单（退货）:");
            manager.Undo();
            
            warehouse.PrintInventory();
        }

        static void RunInventoryCheckAndQueue()
        {
            var warehouse = new WarehouseSystem();
            var manager = new WMSOperationManager();

            // 初始化库存
            warehouse.AddStock("ITEM001", 100, "D-01");
            warehouse.AddStock("ITEM002", 80, "D-02");
            warehouse.AddStock("ITEM003", 60, "D-03");

            Console.WriteLine("初始库存:");
            warehouse.PrintInventory();

            // 盘点调整
            var adjustments = new Dictionary<string, int>
            {
                { "ITEM001", 95 },  // 盘亏5个
                { "ITEM002", 85 },  // 盘盈5个
                { "ITEM003", 60 }   // 无差异
            };

            var inventoryCheck = new InventoryCheckCommand(warehouse, adjustments);
            manager.ExecuteCommand(inventoryCheck);

            Console.WriteLine("\n盘点后库存:");
            warehouse.PrintInventory();

            // 使用命令队列
            Console.WriteLine("\n添加操作到队列:");
            manager.QueueCommand(new InboundCommand(warehouse, "ITEM004", 200, "D-04"));
            manager.QueueCommand(new TransferCommand(warehouse, "ITEM001", "D-01", "E-01"));
            manager.QueueCommand(new OutboundCommand(warehouse, "ITEM002", 10));

            // 处理队列
            manager.ProcessQueue();

            Console.WriteLine("\n最终库存:");
            warehouse.PrintInventory();
        }
    }
}