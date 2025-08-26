namespace _17Command.Example.WMSOperations
{
    public class WMSOperationsExample
    {
        public static void Run()
        {
            Console.WriteLine("\n=== WMS仓库操作命令示例 ===");
            
            // 基本仓库操作
            Console.WriteLine("\n1. 基本仓库操作（入库、出库、移库）:");
            Console.WriteLine("------------------------");
            RunBasicOperations();

            // 批量操作和撤销
            Console.WriteLine("\n2. 批量操作和撤销:");
            Console.WriteLine("------------------------");
            RunBatchOperations();

            // 盘点调整
            Console.WriteLine("\n3. 盘点调整:");
            Console.WriteLine("------------------------");
            RunInventoryCheck();
        }

        private static void RunBasicOperations()
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
        }

        private static void RunBatchOperations()
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

        private static void RunInventoryCheck()
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
        }
    }
}
