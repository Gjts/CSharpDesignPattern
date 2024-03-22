using _AbstractFactory._02Example.Storage;

namespace _04AbstractFactory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------------- WMS入库出库 ----------------------------------");
            // 入库操作
            IInboundFactory rawMaterialInboundFactory = new RawMaterialInboundFactory();
            IWare rawMaterial = rawMaterialInboundFactory.GetWare();
            Console.WriteLine("Inbound " + rawMaterial.GetName());

            IInboundFactory finishedGoodInboundFactory = new FinishedGoodInboundFactory();
            IWare finishedGood = finishedGoodInboundFactory.GetWare();
            Console.WriteLine("Inbound " + finishedGood.GetName());

            // 出库操作
            IOutboundFactory rawMaterialOutboundFactory = new RawMaterialOutboundFactory();
            IWare rawMaterialOutbound = rawMaterialOutboundFactory.GetWare();
            Console.WriteLine("Outbound " + rawMaterialOutbound.GetName());

            IOutboundFactory finishedGoodOutboundFactory = new FinishedGoodOutboundFactory();
            IWare finishedGoodOutbound = finishedGoodOutboundFactory.GetWare();
            Console.WriteLine("Outbound " + finishedGoodOutbound.GetName());

            Console.WriteLine("----------------------------- 云服务商的存储服务 ----------------------------------");
            // 使用AWS存储服务
            IStorageServiceFactory awsFactory = new AWSStorageServiceFactory();
            IStorageService awsStorageService = awsFactory.CreateStorageService();
            awsStorageService.UploadFile("example.txt");
            Console.WriteLine(awsStorageService.DownloadFile("121"));

            // 使用Azure存储服务
            IStorageServiceFactory azureFactory = new AzureStorageServiceFactory();
            IStorageService azureStorageService = azureFactory.CreateStorageService();
            azureStorageService.UploadFile("example.txt");
            Console.WriteLine(azureStorageService.DownloadFile("123"));
        }
    }
}