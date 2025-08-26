using _16Respon.Example.Web3Validation;

namespace _16Respon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 责任链模式 (Chain of Responsibility Pattern) ================================");
            Console.WriteLine("适用场景：有多个对象可以处理请求，但具体由哪个对象处理在运行时确定");
            Console.WriteLine("特点：将请求的发送者和接收者解耦，通过职责链传递请求直到被处理");
            Console.WriteLine("优点：降低耦合度；增强灵活性；责任分担\n");

            Console.WriteLine("-------------------------------- Web3交易验证链 ----------------------------------");
            
            // 构建验证链
            var addressValidator = new AddressValidator();
            var balanceValidator = new BalanceValidator();
            var gasValidator = new GasFeeValidator();
            var contractValidator = new SmartContractValidator();
            var nftValidator = new NFTValidator();
            
            // ETH交易验证链
            addressValidator.SetNext(balanceValidator);
            balanceValidator.SetNext(gasValidator);
            
            Console.WriteLine("1. ETH转账交易验证：");
            var ethTx = new Web3Transaction("0xAlice123", "0xBob456", 10, "ETH");
            Console.WriteLine($"   交易: Alice -> Bob, 10 ETH");
            var result = addressValidator.Validate(ethTx);
            Console.WriteLine($"   结果: {(result ? "✅ 验证通过" : "❌ 验证失败")}");
            
            // NFT交易验证链
            addressValidator.SetNext(nftValidator);
            nftValidator.SetNext(gasValidator);
            
            Console.WriteLine("\n2. NFT交易验证：");
            var nftTx = new Web3Transaction("0xAlice123", "0xBob456", 1, "NFT");
            nftTx.Metadata["tokenId"] = "BAYC#1234";
            Console.WriteLine($"   交易: 转移 BAYC#1234 NFT");
            result = addressValidator.Validate(nftTx);
            Console.WriteLine($"   结果: {(result ? "✅ 验证通过" : "❌ 验证失败")}");
            
            // DeFi交易验证链
            addressValidator.SetNext(balanceValidator);
            balanceValidator.SetNext(contractValidator);
            contractValidator.SetNext(gasValidator);
            
            Console.WriteLine("\n3. DeFi协议交易验证：");
            var defiTx = new Web3Transaction("0xAlice123", "0xUniswapV3", 50, "ETH");
            Console.WriteLine($"   交易: 在Uniswap交换50 ETH");
            result = addressValidator.Validate(defiTx);
            Console.WriteLine($"   结果: {(result ? "✅ 验证通过" : "❌ 验证失败")}");
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 每个验证器只处理自己负责的验证逻辑");
            Console.WriteLine("- 验证链可以动态配置，灵活组合");
            Console.WriteLine("- 请求沿着链传递，直到被处理或到达链尾");
        }
    }
}
