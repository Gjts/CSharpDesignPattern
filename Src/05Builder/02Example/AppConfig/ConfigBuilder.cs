namespace _05Builder._02Example.AppConfig
{
    // 实现Builder接口的类，提供创建对象的具体实现
    public class ConfigBuilder : IConfigBuilder<AppConfig>
    {
        private AppConfig _appConfig = new AppConfig();

        public void BuildPart(string partName, string part)
        {
            _appConfig.Add(partName, part);
        }

        public AppConfig GetResult()
        {
            return _appConfig;
        }
    }
}
