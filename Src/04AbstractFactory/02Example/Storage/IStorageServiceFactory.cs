namespace _AbstractFactory._02Example.Storage
{
    // 存储服务工厂接口
    public interface IStorageServiceFactory
    {
        IStorageService CreateStorageService();
    }

    // AWS存储服务工厂
    public class AWSStorageServiceFactory : IStorageServiceFactory
    {
        public IStorageService CreateStorageService()
        {
            return new AWSStorageService();
        }
    }

    // Azure存储服务工厂
    public class AzureStorageServiceFactory : IStorageServiceFactory
    {
        public IStorageService CreateStorageService()
        {
            return new AzureStorageService();
        }
    }
}
