namespace _Adapter._02Example.DataFormat
{
    // 统一的数据解析接口
    public interface IDataParser
    {
        string ParseData(string data);
    }

    // 第三方XML读取器（需要适配）
    public class XmlDataReader
    {
        public string ReadXml(string xmlContent)
        {
            Console.WriteLine($"   [XML Reader] 读取XML数据");
            return $"XML解析: {xmlContent}";
        }
    }

    // XML适配器
    public class XmlDataAdapter : IDataParser
    {
        private readonly XmlDataReader _xmlReader;

        public XmlDataAdapter(XmlDataReader xmlReader)
        {
            _xmlReader = xmlReader;
        }

        public string ParseData(string data)
        {
            Console.WriteLine($"   适配器: 将数据转换为XML格式");
            return _xmlReader.ReadXml(data);
        }
    }

    // 第三方CSV读取器（需要适配）
    public class CsvDataReader
    {
        public string[] ReadCsv(string csvContent)
        {
            Console.WriteLine($"   [CSV Reader] 读取CSV数据");
            return csvContent.Split('\n');
        }
    }

    // CSV适配器
    public class CsvDataAdapter : IDataParser
    {
        private readonly CsvDataReader _csvReader;

        public CsvDataAdapter(CsvDataReader csvReader)
        {
            _csvReader = csvReader;
        }

        public string ParseData(string data)
        {
            Console.WriteLine($"   适配器: 将数据转换为CSV格式");
            var lines = _csvReader.ReadCsv(data);
            return $"CSV解析: {string.Join(" | ", lines)}";
        }
    }

    // 原生JSON解析器
    public class JsonDataParser : IDataParser
    {
        public string ParseData(string data)
        {
            Console.WriteLine($"   [JSON Parser] 原生解析JSON数据");
            return $"JSON解析: {data}";
        }
    }
}
