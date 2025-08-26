namespace _16Respon.Example.Web3Validation
{
    public class Web3ValidationExample
    {
        public static void Run()
        {
            Console.WriteLine("\n=== Web3交易验证链示例 ===");
            
            // ETH转账验证
            Console.WriteLine("\n1. ETH转账交易验证:");
            Console.WriteLine("------------------------");
            ValidateETHTransaction();

            // NFT交易验证
            Console.WriteLine("\n2. NFT交易验证:");
            Console.WriteLine("------------------------");
            ValidateNFTTransaction();

            // DeFi协议交互验证
            Console.WriteLine("\n3. DeFi协议交互验证:");
            Console.WriteLine("------------------------");
            ValidateDeFiTransaction();
        }

        private static void ValidateETHTransaction()
        {
            // 构建验证链
            var addressValidator = new AddressValidator();
            var balanceValidator = new BalanceValidator();
            var gasValidator = new GasFeeValidator();

            addressValidator.SetNext(balanceValidator);
            balanceValidator.SetNext(gasValidator);

            // 创建ETH转账交易
            var tx1 = new Web3Transaction("0xAlice123", "0xBob456", 10, "ETH");
            Console.WriteLine($"交易1: Alice -> Bob, 10 ETH");
            var result1 = addressValidator.Validate(tx1);
            Console.WriteLine($"验证结果: {(result1 ? "✅ 成功" : "❌ 失败")}");

            // 余额不足的交易
            var tx2 = new Web3Transaction("0xBob456", "0xCharlie789", 100, "ETH");
            Console.WriteLine($"\n交易2: Bob -> Charlie, 100 ETH");
            var result2 = addressValidator.Validate(tx2);
            Console.WriteLine($"验证结果: {(result2 ? "✅ 成功" : "❌ 失败")}");
        }

        private static void ValidateNFTTransaction()
        {
            // 构建NFT验证链
            var addressValidator = new AddressValidator();
            var nftValidator = new NFTValidator();
            var gasValidator = new GasFeeValidator();

            addressValidator.SetNext(nftValidator);
            nftValidator.SetNext(gasValidator);

            // 创建NFT转账交易
            var nftTx = new Web3Transaction("0xAlice123", "0xBob456", 1, "NFT");
            nftTx.Metadata["tokenId"] = "BAYC#1234";
            
            Console.WriteLine($"交易: 转移 BAYC#1234 NFT");
            var result = addressValidator.Validate(nftTx);
            Console.WriteLine($"验证结果: {(result ? "✅ 成功" : "❌ 失败")}");
        }

        private static void ValidateDeFiTransaction()
        {
            // 构建DeFi验证链
            var addressValidator = new AddressValidator();
            var balanceValidator = new BalanceValidator();
            var contractValidator = new SmartContractValidator();
            var defiValidator = new DeFiProtocolValidator();
            var gasValidator = new GasFeeValidator();

            addressValidator.SetNext(balanceValidator);
            balanceValidator.SetNext(contractValidator);
            contractValidator.SetNext(defiValidator);
            defiValidator.SetNext(gasValidator);

            // Uniswap交易
            var uniswapTx = new Web3Transaction("0xAlice123", "0xUniswapV3", 50, "ETH");
            uniswapTx.Metadata["protocol"] = "Uniswap";
            
            Console.WriteLine($"交易: 在Uniswap上交换50 ETH");
            var result = addressValidator.Validate(uniswapTx);
            Console.WriteLine($"验证结果: {(result ? "✅ 成功" : "❌ 失败")}");
        }
    }
}
