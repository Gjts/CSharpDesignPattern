namespace _18Iterator.Example.BlockchainIterator
{
    public class BlockchainIteratorExample
    {
        public static void Run()
        {
            Console.WriteLine("\n=== 区块链迭代器示例 ===");
            
            // 创建区块链
            var blockchain = new Blockchain();
            
            // 添加一些区块和交易
            AddSampleBlocks(blockchain);
            
            // 正向遍历区块
            Console.WriteLine("\n1. 正向遍历区块链:");
            Console.WriteLine("------------------------");
            var forwardIterator = blockchain.CreateIterator();
            while (forwardIterator.HasNext())
            {
                var block = forwardIterator.Next();
                Console.WriteLine($"区块 #{block.Index} | Hash: {block.Hash.Substring(0, 10)}... | 交易数: {block.Transactions.Count}");
            }
            
            // 反向遍历区块
            Console.WriteLine("\n2. 反向遍历区块链:");
            Console.WriteLine("------------------------");
            var reverseIterator = blockchain.CreateReverseIterator();
            while (reverseIterator.HasNext())
            {
                var block = reverseIterator.Next();
                Console.WriteLine($"区块 #{block.Index} | Hash: {block.Hash.Substring(0, 10)}... | 时间: {block.Timestamp:HH:mm:ss}");
            }
            
            // 遍历所有交易
            Console.WriteLine("\n3. 遍历所有交易:");
            Console.WriteLine("------------------------");
            var txIterator = blockchain.CreateTransactionIterator();
            int txCount = 0;
            decimal totalVolume = 0;
            while (txIterator.HasNext())
            {
                var tx = txIterator.Next();
                txCount++;
                totalVolume += tx.Amount;
                Console.WriteLine($"交易: {tx.From} -> {tx.To} | 金额: {tx.Amount} ETH");
            }
            Console.WriteLine($"总交易数: {txCount}, 总交易量: {totalVolume} ETH");
            
            // 遍历高价值交易
            Console.WriteLine("\n4. 高价值交易 (>= 100 ETH):");
            Console.WriteLine("------------------------");
            var highValueIterator = blockchain.CreateHighValueTransactionIterator(100);
            while (highValueIterator.HasNext())
            {
                var tx = highValueIterator.Next();
                Console.WriteLine($"大额交易: {tx.TxId.Substring(0, 10)}... | {tx.Amount} ETH | 手续费: {tx.Fee} ETH");
            }
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
