using _17Command.Example.WMSOperations;

namespace _17Command
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 命令模式 (Command Pattern) ================================");
            Console.WriteLine("适用场景：需要将请求发送者和接收者解耦；需要支持撤销、重做、事务等操作");
            Console.WriteLine("特点：将请求封装为对象，从而可以用不同的请求对客户进行参数化");
            Console.WriteLine("优点：解耦调用者和接收者；可以实现撤销和重做；可以实现宏命令；可以实现队列和日志\n");

            Console.WriteLine("-------------------------------- WMS仓库操作系统 ----------------------------------");
            
            var warehouse = new WarehouseSystem();
            var manager = new WMSOperationManager();
            
            Console.WriteLine("1. 基本仓库操作：");
            
            // 入库
            var inbound1 = new InboundCommand(warehouse, "SKU001", 100, "A-01");
            var inbound2 = new InboundCommand(warehouse, "SKU002", 50, "B-02");
            manager.ExecuteCommand(inbound1);
            manager.ExecuteCommand(inbound2);
            
            // 出库
            var outbound = new OutboundCommand(warehouse, "SKU001", 30);
            manager.ExecuteCommand(outbound);
            
            // 移库
            var transfer = new TransferCommand(warehouse, "SKU002", "B-02", "A-02");
            manager.ExecuteCommand(transfer);
            
            warehouse.PrintInventory();
            
            Console.WriteLine("\n2. 撤销操作：");
            manager.Undo();
            manager.Undo();
            warehouse.PrintInventory();
            
            Console.WriteLine("\n3. 重做操作：");
            manager.Redo();
            warehouse.PrintInventory();
            
            Console.WriteLine("\n4. 批量操作（订单履行）：");
            var batch = new BatchCommand("订单#12345");
            batch.AddCommand(new OutboundCommand(warehouse, "SKU001", 20));
            batch.AddCommand(new OutboundCommand(warehouse, "SKU002", 10));
            manager.ExecuteCommand(batch);
            warehouse.PrintInventory();
            
            Console.WriteLine("\n5. 操作队列：");
            manager.QueueCommand(new InboundCommand(warehouse, "SKU003", 200, "C-01"));
            manager.QueueCommand(new TransferCommand(warehouse, "SKU001", "A-01", "D-01"));
            manager.ProcessQueue();
            warehouse.PrintInventory();
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 每个操作都是一个命令对象");
            Console.WriteLine("- 支持撤销、重做、批量操作");
            Console.WriteLine("- 可以将命令放入队列延迟执行");
        }
    }
}
