namespace _16Respon
{
    // Web3交易类
    public class Web3Transaction
    {
        public string TxHash { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal Amount { get; set; }
        public decimal GasPrice { get; set; }
        public string TokenType { get; set; } // ETH, USDT, NFT等
        public Dictionary<string, object> Metadata { get; set; }

        public Web3Transaction(string from, string to, decimal amount, string tokenType)
        {
            TxHash = GenerateTxHash();
            From = from;
            To = to;
            Amount = amount;
            TokenType = tokenType;
            GasPrice = 20; // Gwei
            Metadata = new Dictionary<string, object>();
        }

        private string GenerateTxHash()
        {
            return "0x" + Guid.NewGuid().ToString("N").Substring(0, 64);
        }
    }

    // 抽象验证器
    public abstract class TransactionValidator
    {
        protected TransactionValidator? _nextValidator;
        protected string _validatorName;

        protected TransactionValidator(string validatorName)
        {
            _validatorName = validatorName;
        }

        public void SetNext(TransactionValidator nextValidator)
        {
            _nextValidator = nextValidator;
        }

        public virtual bool Validate(Web3Transaction transaction)
        {
            Console.WriteLine($"[{_validatorName}] 验证交易 {transaction.TxHash.Substring(0, 10)}...");
            
            if (CanValidate(transaction))
            {
                var result = PerformValidation(transaction);
                if (!result)
                {
                    Console.WriteLine($"  ❌ 验证失败: {GetFailureReason()}");
                    return false;
                }
                Console.WriteLine($"  ✅ 验证通过");
            }

            if (_nextValidator != null)
            {
                return _nextValidator.Validate(transaction);
            }

            return true;
        }

        protected abstract bool CanValidate(Web3Transaction transaction);
        protected abstract bool PerformValidation(Web3Transaction transaction);
        protected abstract string GetFailureReason();
    }

    // 地址验证器
    public class AddressValidator : TransactionValidator
    {
        private HashSet<string> _blacklist = new HashSet<string>
        {
            "0xBadAddress001",
            "0xScammer002"
        };

        public AddressValidator() : base("地址验证器")
        {
        }

        protected override bool CanValidate(Web3Transaction transaction)
        {
            return true; // 所有交易都需要地址验证
        }

        protected override bool PerformValidation(Web3Transaction transaction)
        {
            // 检查地址格式
            if (!transaction.From.StartsWith("0x") || !transaction.To.StartsWith("0x"))
            {
                return false;
            }

            // 检查黑名单
            if (_blacklist.Contains(transaction.From) || _blacklist.Contains(transaction.To))
            {
                return false;
            }

            return true;
        }

        protected override string GetFailureReason()
        {
            return "地址格式错误或在黑名单中";
        }
    }

    // 余额验证器
    public class BalanceValidator : TransactionValidator
    {
        private Dictionary<string, decimal> _balances = new Dictionary<string, decimal>
        {
            { "0xAlice123", 100 },
            { "0xBob456", 50 },
            { "0xCharlie789", 200 }
        };

        public BalanceValidator() : base("余额验证器")
        {
        }

        protected override bool CanValidate(Web3Transaction transaction)
        {
            return transaction.TokenType == "ETH" || transaction.TokenType == "USDT";
        }

        protected override bool PerformValidation(Web3Transaction transaction)
        {
            if (!_balances.ContainsKey(transaction.From))
            {
                return false;
            }

            var totalCost = transaction.Amount + (transaction.GasPrice * 0.001m); // 简化的gas计算
            return _balances[transaction.From] >= totalCost;
        }

        protected override string GetFailureReason()
        {
            return "余额不足";
        }
    }

    // 智能合约验证器
    public class SmartContractValidator : TransactionValidator
    {
        private HashSet<string> _verifiedContracts = new HashSet<string>
        {
            "0xUniswap",
            "0xOpenSea",
            "0xCompound"
        };

        public SmartContractValidator() : base("智能合约验证器")
        {
        }

        protected override bool CanValidate(Web3Transaction transaction)
        {
            // 检查是否是合约交互
            return transaction.To.Contains("Contract") || transaction.To.Contains("0xUni") || transaction.To.Contains("0xOpen");
        }

        protected override bool PerformValidation(Web3Transaction transaction)
        {
            // 验证合约是否已审计
            foreach (var contract in _verifiedContracts)
            {
                if (transaction.To.Contains(contract.Substring(2)))
                {
                    Console.WriteLine($"    与已验证合约 {contract} 交互");
                    return true;
                }
            }
            return false;
        }

        protected override string GetFailureReason()
        {
            return "未验证的智能合约";
        }
    }

    // Gas费验证器
    public class GasFeeValidator : TransactionValidator
    {
        private decimal _maxGasPrice = 100; // Gwei
        private decimal _minGasPrice = 10;

        public GasFeeValidator() : base("Gas费验证器")
        {
        }

        protected override bool CanValidate(Web3Transaction transaction)
        {
            return true;
        }

        protected override bool PerformValidation(Web3Transaction transaction)
        {
            if (transaction.GasPrice < _minGasPrice)
            {
                Console.WriteLine($"    Gas价格过低: {transaction.GasPrice} Gwei");
                return false;
            }

            if (transaction.GasPrice > _maxGasPrice)
            {
                Console.WriteLine($"    Gas价格过高: {transaction.GasPrice} Gwei");
                return false;
            }

            return true;
        }

        protected override string GetFailureReason()
        {
            return "Gas费用不在合理范围内";
        }
    }

    // NFT验证器
    public class NFTValidator : TransactionValidator
    {
        private Dictionary<string, string> _nftOwners = new Dictionary<string, string>
        {
            { "BAYC#1234", "0xAlice123" },
            { "CryptoPunk#5678", "0xBob456" }
        };

        public NFTValidator() : base("NFT验证器")
        {
        }

        protected override bool CanValidate(Web3Transaction transaction)
        {
            return transaction.TokenType == "NFT";
        }

        protected override bool PerformValidation(Web3Transaction transaction)
        {
            if (!transaction.Metadata.TryGetValue("tokenId", out var rawTokenId))
            {
                return false;
            }

            var tokenId = rawTokenId?.ToString();
            if (string.IsNullOrWhiteSpace(tokenId) || !_nftOwners.ContainsKey(tokenId))
            {
                return false;
            }

            // 验证所有权
            return _nftOwners[tokenId] == transaction.From;
        }

        protected override string GetFailureReason()
        {
            return "NFT所有权验证失败";
        }
    }

    // DeFi协议验证器
    public class DeFiProtocolValidator : TransactionValidator
    {
        public DeFiProtocolValidator() : base("DeFi协议验证器")
        {
        }

        protected override bool CanValidate(Web3Transaction transaction)
        {
            return transaction.Metadata.ContainsKey("protocol");
        }

        protected override bool PerformValidation(Web3Transaction transaction)
        {
            var protocol = transaction.Metadata["protocol"].ToString();
            
            switch (protocol)
            {
                case "Uniswap":
                    Console.WriteLine("    验证Uniswap流动性池");
                    return true;
                case "Aave":
                    Console.WriteLine("    验证Aave借贷协议");
                    return true;
                case "Compound":
                    Console.WriteLine("    验证Compound利率模型");
                    return true;
                default:
                    return false;
            }
        }

        protected override string GetFailureReason()
        {
            return "不支持的DeFi协议";
        }
    }
}
