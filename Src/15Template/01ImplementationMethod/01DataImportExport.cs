namespace _15Template
{
    // 抽象数据导入模板类
    public abstract class DataImporter
    {
        // 模板方法 - 定义导入流程的骨架
        public void ImportData(string fileName)
        {
            Console.WriteLine($"开始导入数据: {fileName}");
            
            // 步骤1：打开文件
            OpenFile(fileName);
            
            // 步骤2：验证文件格式
            if (ValidateFormat())
            {
                // 步骤3：读取数据
                var rawData = ReadData();
                
                // 步骤4：解析数据
                var parsedData = ParseData(rawData);
                
                // 步骤5：验证数据
                if (ValidateData(parsedData))
                {
                    // 步骤6：保存到数据库
                    SaveToDatabase(parsedData);
                    
                    // 步骤7：记录日志（钩子方法）
                    if (ShouldLogImport())
                    {
                        LogImport(fileName);
                    }
                }
                else
                {
                    Console.WriteLine("  数据验证失败!");
                }
            }
            else
            {
                Console.WriteLine("  文件格式验证失败!");
            }
            
            // 步骤8：关闭文件
            CloseFile();
            Console.WriteLine($"导入完成: {fileName}\n");
        }
        
        // 具体步骤 - 由子类实现
        protected abstract void OpenFile(string fileName);
        protected abstract bool ValidateFormat();
        protected abstract string ReadData();
        protected abstract object ParseData(string rawData);
        protected abstract void CloseFile();
        
        // 通用步骤 - 可被子类重写
        protected virtual bool ValidateData(object data)
        {
            Console.WriteLine("  验证数据完整性...");
            return true;
        }
        
        protected virtual void SaveToDatabase(object data)
        {
            Console.WriteLine("  保存数据到数据库...");
        }
        
        // 钩子方法 - 子类可以选择性重写
        protected virtual bool ShouldLogImport()
        {
            return true;
        }
        
        protected virtual void LogImport(string fileName)
        {
            Console.WriteLine($"  记录导入日志: {fileName}");
        }
    }
    
    // 具体实现：CSV数据导入器
    public class CsvDataImporter : DataImporter
    {
        protected override void OpenFile(string fileName)
        {
            Console.WriteLine($"  打开CSV文件: {fileName}");
        }
        
        protected override bool ValidateFormat()
        {
            Console.WriteLine("  验证CSV格式...");
            return true;
        }
        
        protected override string ReadData()
        {
            Console.WriteLine("  读取CSV数据...");
            return "id,name,value\n1,Item1,100\n2,Item2,200";
        }
        
        protected override object ParseData(string rawData)
        {
            Console.WriteLine("  解析CSV数据...");
            return new { Rows = 2, Columns = 3 };
        }
        
        protected override void CloseFile()
        {
            Console.WriteLine("  关闭CSV文件");
        }
    }
    
    // 具体实现：JSON数据导入器
    public class JsonDataImporter : DataImporter
    {
        protected override void OpenFile(string fileName)
        {
            Console.WriteLine($"  打开JSON文件: {fileName}");
        }
        
        protected override bool ValidateFormat()
        {
            Console.WriteLine("  验证JSON格式...");
            return true;
        }
        
        protected override string ReadData()
        {
            Console.WriteLine("  读取JSON数据...");
            return "{\"items\":[{\"id\":1,\"name\":\"Item1\"},{\"id\":2,\"name\":\"Item2\"}]}";
        }
        
        protected override object ParseData(string rawData)
        {
            Console.WriteLine("  解析JSON数据...");
            return new { Items = 2 };
        }
        
        protected override void CloseFile()
        {
            Console.WriteLine("  关闭JSON文件");
        }
        
        protected override bool ShouldLogImport()
        {
            return false; // JSON导入不记录日志
        }
    }
    
    // 抽象数据导出模板类
    public abstract class DataExporter
    {
        // 模板方法 - 定义导出流程的骨架
        public void ExportData(object data, string fileName)
        {
            Console.WriteLine($"开始导出数据到: {fileName}");
            
            // 步骤1：准备数据
            var preparedData = PrepareData(data);
            
            // 步骤2：格式化数据
            var formattedData = FormatData(preparedData);
            
            // 步骤3：添加头部信息（可选）
            if (ShouldAddHeader())
            {
                formattedData = AddHeader(formattedData);
            }
            
            // 步骤4：添加尾部信息（可选）
            if (ShouldAddFooter())
            {
                formattedData = AddFooter(formattedData);
            }
            
            // 步骤5：写入文件
            WriteToFile(formattedData, fileName);
            
            // 步骤6：压缩文件（可选）
            if (ShouldCompress())
            {
                CompressFile(fileName);
            }
            
            Console.WriteLine($"导出完成: {fileName}\n");
        }
        
        // 抽象方法 - 必须由子类实现
        protected abstract object PrepareData(object data);
        protected abstract string FormatData(object data);
        protected abstract void WriteToFile(string data, string fileName);
        
        // 钩子方法 - 子类可选择性重写
        protected virtual bool ShouldAddHeader() => false;
        protected virtual string AddHeader(string data) => data;
        
        protected virtual bool ShouldAddFooter() => false;
        protected virtual string AddFooter(string data) => data;
        
        protected virtual bool ShouldCompress() => false;
        protected virtual void CompressFile(string fileName)
        {
            Console.WriteLine($"  压缩文件: {fileName}");
        }
    }
    
    // 具体实现：XML数据导出器
    public class XmlDataExporter : DataExporter
    {
        protected override object PrepareData(object data)
        {
            Console.WriteLine("  准备XML数据...");
            return data;
        }
        
        protected override string FormatData(object data)
        {
            Console.WriteLine("  格式化为XML...");
            return "<?xml version=\"1.0\"?><root><items>...</items></root>";
        }
        
        protected override void WriteToFile(string data, string fileName)
        {
            Console.WriteLine($"  写入XML文件: {fileName}");
        }
        
        protected override bool ShouldAddHeader() => true;
        protected override string AddHeader(string data)
        {
            Console.WriteLine("  添加XML声明头...");
            return data;
        }
    }
    
    // 具体实现：Excel数据导出器
    public class ExcelDataExporter : DataExporter
    {
        protected override object PrepareData(object data)
        {
            Console.WriteLine("  准备Excel数据...");
            return data;
        }
        
        protected override string FormatData(object data)
        {
            Console.WriteLine("  格式化为Excel格式...");
            return "Excel Binary Data";
        }
        
        protected override void WriteToFile(string data, string fileName)
        {
            Console.WriteLine($"  写入Excel文件: {fileName}");
        }
        
        protected override bool ShouldCompress() => true;
    }
}