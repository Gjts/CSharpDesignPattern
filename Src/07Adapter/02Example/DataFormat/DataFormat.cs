namespace _Adapter._02Example.DataFormat
{
    // 目标接口 - JSON数据处理器
    public interface IJsonProcessor
    {
        string ProcessJson(string jsonData);
    }

    // 被适配者 - XML处理器
    public class XmlProcessor
    {
        public string ProcessXml(string xmlData)
        {
            Console.WriteLine($"  处理XML数据: {xmlData}");
            return $"<processed>{xmlData}</processed>";
        }
    }

    // 被适配者 - CSV处理器
    public class CsvProcessor
    {
        public string[] ProcessCsv(string csvData)
        {
            Console.WriteLine($"  处理CSV数据: {csvData}");
            return csvData.Split(',');
        }
    }

    // 适配器 - XML到JSON适配器
    public class XmlToJsonAdapter : IJsonProcessor
    {
        private readonly XmlProcessor _xmlProcessor;

        public XmlToJsonAdapter(XmlProcessor xmlProcessor)
        {
            _xmlProcessor = xmlProcessor;
        }

        public string ProcessJson(string jsonData)
        {
            // 模拟JSON转XML
            string xmlData = ConvertJsonToXml(jsonData);
            string processedXml = _xmlProcessor.ProcessXml(xmlData);
            // 模拟XML转JSON
            return ConvertXmlToJson(processedXml);
        }

        private string ConvertJsonToXml(string json)
        {
            Console.WriteLine($"  转换JSON到XML: {json} -> <data>{json}</data>");
            return $"<data>{json}</data>";
        }

        private string ConvertXmlToJson(string xml)
        {
            Console.WriteLine($"  转换XML到JSON: {xml} -> {{\"data\":\"{xml}\"}}");
            return $"{{\"data\":\"{xml}\"}}";
        }
    }

    // 适配器 - CSV到JSON适配器
    public class CsvToJsonAdapter : IJsonProcessor
    {
        private readonly CsvProcessor _csvProcessor;

        public CsvToJsonAdapter(CsvProcessor csvProcessor)
        {
            _csvProcessor = csvProcessor;
        }

        public string ProcessJson(string jsonData)
        {
            // 模拟JSON转CSV
            string csvData = ConvertJsonToCsv(jsonData);
            string[] processedCsv = _csvProcessor.ProcessCsv(csvData);
            // 模拟CSV转JSON
            return ConvertCsvToJson(processedCsv);
        }

        private string ConvertJsonToCsv(string json)
        {
            Console.WriteLine($"  转换JSON到CSV: {json} -> value1,value2,value3");
            return "value1,value2,value3";
        }

        private string ConvertCsvToJson(string[] csv)
        {
            var jsonArray = string.Join(",", csv.Select(v => $"\"{v}\""));
            Console.WriteLine($"  转换CSV到JSON: [{jsonArray}]");
            return $"[{jsonArray}]";
        }
    }

    // 客户端代码
    public class DataFormatClient
    {
        public static void TestDataFormatAdapters()
        {
            Console.WriteLine("测试XML适配器:");
            IJsonProcessor xmlAdapter = new XmlToJsonAdapter(new XmlProcessor());
            string result1 = xmlAdapter.ProcessJson("{\"name\":\"test\"}");
            Console.WriteLine($"  结果: {result1}\n");

            Console.WriteLine("测试CSV适配器:");
            IJsonProcessor csvAdapter = new CsvToJsonAdapter(new CsvProcessor());
            string result2 = csvAdapter.ProcessJson("{\"values\":[1,2,3]}");
            Console.WriteLine($"  结果: {result2}");
        }
    }
}
