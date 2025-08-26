namespace _18Iterator.Example.BlockchainIterator
{
    // 区块类
    public class Block
    {
        public int Index { get; set; }
        public DateTime Timestamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public List<Transaction> Transactions { get; set; }
        public int Nonce { get; set; }

        public Block(int index, string previousHash)
        {
            Index = index;
            Timestamp = DateTime.Now;
            PreviousHash = previousHash;
            Transactions = new List<Transaction>();
            Hash = CalculateHash();
        }

        private string CalculateHash()
        {
            return $"0x{Guid.NewGuid().ToString("N").Substring(0, 64)}";
        }
    }

    // 交易类
    public class Transaction
    {
        public string TxId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public DateTime Timestamp { get; set; }

        public Transaction(string from, string to, decimal amount, decimal fee)
        {
            TxId = $"0x{Guid.NewGuid().ToString("N").Substring(0, 40)}";
            From = from;
            To = to;
            Amount = amount;
            Fee = fee;
            Timestamp = DateTime.Now;
        }
    }

    // 区块链接口
    public interface IBlockchain
    {
        void AddBlock(Block block);
        IBlockIterator CreateIterator();
        ITransactionIterator CreateTransactionIterator();
    }

    // 区块迭代器接口
    public interface IBlockIterator
    {
        bool HasNext();
        Block Next();
        void Reset();
    }

    // 交易迭代器接口
    public interface ITransactionIterator
    {
        bool HasNext();
        Transaction Next();
        void Reset();
    }

    // 区块链实现
    public class Blockchain : IBlockchain
    {
        private List<Block> _blocks;
        private Block _genesisBlock;

        public Blockchain()
        {
            _blocks = new List<Block>();
            _genesisBlock = CreateGenesisBlock();
            _blocks.Add(_genesisBlock);
        }

        private Block CreateGenesisBlock()
        {
            var genesis = new Block(0, "0x0");
            genesis.Transactions.Add(new Transaction("System", "Miner", 50, 0));
            return genesis;
        }

        public void AddBlock(Block block)
        {
            _blocks.Add(block);
        }

        public Block GetLatestBlock()
        {
            return _blocks[_blocks.Count - 1];
        }

        public IBlockIterator CreateIterator()
        {
            return new ForwardBlockIterator(_blocks);
        }

        public IBlockIterator CreateReverseIterator()
        {
            return new ReverseBlockIterator(_blocks);
        }

        public ITransactionIterator CreateTransactionIterator()
        {
            return new AllTransactionsIterator(_blocks);
        }

        public ITransactionIterator CreateHighValueTransactionIterator(decimal threshold)
        {
            return new HighValueTransactionIterator(_blocks, threshold);
        }
    }

    // 正向区块迭代器
    public class ForwardBlockIterator : IBlockIterator
    {
        private List<Block> _blocks;
        private int _position = 0;

        public ForwardBlockIterator(List<Block> blocks)
        {
            _blocks = blocks;
        }

        public bool HasNext()
        {
            return _position < _blocks.Count;
        }

        public Block Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("没有更多区块");
            
            return _blocks[_position++];
        }

        public void Reset()
        {
            _position = 0;
        }
    }

    // 反向区块迭代器
    public class ReverseBlockIterator : IBlockIterator
    {
        private List<Block> _blocks;
        private int _position;

        public ReverseBlockIterator(List<Block> blocks)
        {
            _blocks = blocks;
            _position = _blocks.Count - 1;
        }

        public bool HasNext()
        {
            return _position >= 0;
        }

        public Block Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("没有更多区块");
            
            return _blocks[_position--];
        }

        public void Reset()
        {
            _position = _blocks.Count - 1;
        }
    }

    // 所有交易迭代器
    public class AllTransactionsIterator : ITransactionIterator
    {
        private List<Block> _blocks;
        private int _blockIndex = 0;
        private int _txIndex = 0;

        public AllTransactionsIterator(List<Block> blocks)
        {
            _blocks = blocks;
        }

        public bool HasNext()
        {
            while (_blockIndex < _blocks.Count)
            {
                if (_txIndex < _blocks[_blockIndex].Transactions.Count)
                {
                    return true;
                }
                _blockIndex++;
                _txIndex = 0;
            }
            return false;
        }

        public Transaction Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("没有更多交易");
            
            var transaction = _blocks[_blockIndex].Transactions[_txIndex++];
            
            if (_txIndex >= _blocks[_blockIndex].Transactions.Count)
            {
                _blockIndex++;
                _txIndex = 0;
            }
            
            return transaction;
        }

        public void Reset()
        {
            _blockIndex = 0;
            _txIndex = 0;
        }
    }

    // 高价值交易迭代器
    public class HighValueTransactionIterator : ITransactionIterator
    {
        private List<Transaction> _highValueTxs;
        private int _position = 0;

        public HighValueTransactionIterator(List<Block> blocks, decimal threshold)
        {
            _highValueTxs = new List<Transaction>();
            
            foreach (var block in blocks)
            {
                foreach (var tx in block.Transactions)
                {
                    if (tx.Amount >= threshold)
                    {
                        _highValueTxs.Add(tx);
                    }
                }
            }
        }

        public bool HasNext()
        {
            return _position < _highValueTxs.Count;
        }

        public Transaction Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("没有更多高价值交易");
            
            return _highValueTxs[_position++];
        }

        public void Reset()
        {
            _position = 0;
        }
    }
}
