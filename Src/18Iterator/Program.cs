using _18Iterator.Example.BlockchainIterator;

namespace _18Iterator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 迭代器模式 (Iterator Pattern) ================================");
            Console.WriteLine("适用场景：需要遍历聚合对象的元素而不暴露其内部结构；需要为聚合对象提供多种遍历方式");
            Console.WriteLine("特点：提供一种方法顺序访问聚合对象中的各个元素，而又不暴露该对象的内部表示");
            Console.WriteLine("优点：简化了聚合类；支持多种遍历方式；符合单一职责原则\n");

            Console.WriteLine("-------------------------------- 区块链遍历系统 ----------------------------------");
            
            // 创建区块链
            var blockchain = new Blockchain();
            
            // 添加区块和交易
            AddSampleBlocks(blockchain);
            
            Console.WriteLine("1. 正向遍历区块链：");
            var forwardIterator = blockchain.CreateIterator();
            while (forwardIterator.HasNext())
            {
                var block = forwardIterator.Next();
                Console.WriteLine($"   区块 #{block.Index} | Hash: {block.Hash.Substring(0, 10)}... | 交易数: {block.Transactions.Count}");
            }
            
            Console.WriteLine("\n2. 反向遍历区块链：");
            var reverseIterator = blockchain.CreateReverseIterator();
            while (reverseIterator.HasNext())
            {
                var block = reverseIterator.Next();
                Console.WriteLine($"   区块 #{block.Index} | 时间: {block.Timestamp:HH:mm:ss}");
            }
            
            Console.WriteLine("\n3. 遍历所有交易：");
            var txIterator = blockchain.CreateTransactionIterator();
            int txCount = 0;
            decimal totalVolume = 0;
            while (txIterator.HasNext() && txCount < 5)  // 只显示前5个
            {
                var tx = txIterator.Next();
                txCount++;
                totalVolume += tx.Amount;
                Console.WriteLine($"   交易 {txCount}: {tx.From} -> {tx.To} | {tx.Amount} ETH");
            }
            Console.WriteLine($"   总交易数: {txCount}+, 显示的交易量: {totalVolume} ETH");
            
            Console.WriteLine("\n4. 高价值交易筛选（>= 100 ETH）：");
            var highValueIterator = blockchain.CreateHighValueTransactionIterator(100);
            while (highValueIterator.HasNext())
            {
                var tx = highValueIterator.Next();
                Console.WriteLine($"   大额交易: {tx.TxId.Substring(0, 10)}... | {tx.Amount} ETH | 手续费: {tx.Fee} ETH");
            }
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 迭代器封装了遍历逻辑，客户端无需了解内部结构");
            Console.WriteLine("- 支持多种遍历方式（正向、反向、筛选）");
            Console.WriteLine("- 可以同时有多个迭代器遍历同一个聚合对象");
        }
        
        private static void AddSampleBlocks(Blockchain blockchain)
        {
            // 区块 1
            var block1 = new Block(1, blockchain.GetLatestBlock().Hash);
            block1.Transactions.Add(new Transaction("Alice", "Bob", 50, 0.001m));
            block1.Transactions.Add(new Transaction("Bob", "Charlie", 30, 0.001m));
            blockchain.AddBlock(block1);
            
            // 区块 2
            var block2 = new Block(2, blockchain.GetLatestBlock().Hash);
            block2.Transactions.Add(new Transaction("Charlie", "David", 150, 0.002m));
            block2.Transactions.Add(new Transaction("Alice", "Eve", 75, 0.001m));
            block2.Transactions.Add(new Transaction("Frank", "Grace", 200, 0.003m));
            blockchain.AddBlock(block2);
            
            // 区块 3
            var block3 = new Block(3, blockchain.GetLatestBlock().Hash);
            block3.Transactions.Add(new Transaction("Grace", "Henry", 500, 0.005m));
            block3.Transactions.Add(new Transaction("Ivan", "Julia", 25, 0.001m));
            blockchain.AddBlock(block3);
        }
    }
}
