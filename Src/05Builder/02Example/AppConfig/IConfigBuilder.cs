namespace _05Builder._02Example.AppConfig
{
    // 抽象接口，用于定义创建对象的各个部分的方法
    public interface IConfigBuilder<T> where T : new()
    {
        void BuildPart(string partName, string part);

        T GetResult();
    }
}
