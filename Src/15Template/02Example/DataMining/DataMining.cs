namespace _15Template._02Example.DataMining
{
    // 抽象类 - 数据挖掘模板
    public abstract class DataMiner
    {
        // 模板方法
        public void Mine(string path)
        {
            Console.WriteLine($"\n开始数据挖掘流程: {path}");
            Console.WriteLine(new string('=', 50));
            
            OpenFile(path);
            ExtractData();
            ParseData();
            AnalyzeData();
            SendReport();
            CloseFile();
            
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("数据挖掘完成！\n");
        }

        // 抽象方法 - 必须由子类实现
        protected abstract void OpenFile(string path);
        protected abstract void ExtractData();
        protected abstract void CloseFile();

        // 具体方法 - 提供默认实现
        protected virtual void ParseData()
        {
            Console.WriteLine("  📊 解析数据...");
            Thread.Sleep(500);
            Console.WriteLine("     数据解析完成");
        }

        // 钩子方法 - 子类可以选择覆盖
        protected virtual void AnalyzeData()
        {
            Console.WriteLine("  🔍 分析数据...");
            Thread.Sleep(800);
            Console.WriteLine("     基础分析完成");
        }

        protected virtual void SendReport()
        {
            Console.WriteLine("  📧 发送分析报告...");
            Console.WriteLine("     报告已发送到默认邮箱");
        }
    }

    // 具体类 - PDF文档挖掘
    public class PDFDataMiner : DataMiner
    {
        protected override void OpenFile(string path)
        {
            Console.WriteLine($"  📄 打开PDF文件: {path}");
            Console.WriteLine("     使用PDF解析器");
        }

        protected override void ExtractData()
        {
            Console.WriteLine("  📥 提取PDF内容...");
            Console.WriteLine("     提取文本内容");
            Console.WriteLine("     提取图片");
            Console.WriteLine("     提取表格数据");
            Thread.Sleep(600);
        }

        protected override void CloseFile()
        {
            Console.WriteLine("  ✅ 关闭PDF文件");
        }

        protected override void AnalyzeData()
        {
            base.AnalyzeData();
            Console.WriteLine("     PDF特定分析：");
            Console.WriteLine("     - 页面布局分析");
            Console.WriteLine("     - 字体使用统计");
            Console.WriteLine("     - 图文比例分析");
        }
    }

    // 具体类 - CSV数据挖掘
    public class CSVDataMiner : DataMiner
    {
        protected override void OpenFile(string path)
        {
            Console.WriteLine($"  📊 打开CSV文件: {path}");
            Console.WriteLine("     使用CSV读取器");
        }

        protected override void ExtractData()
        {
            Console.WriteLine("  📥 提取CSV数据...");
            Console.WriteLine("     读取表头");
            Console.WriteLine("     逐行读取数据");
            Console.WriteLine("     数据类型推断");
            Thread.Sleep(400);
        }

        protected override void CloseFile()
        {
            Console.WriteLine("  ✅ 关闭CSV文件流");
        }

        protected override void ParseData()
        {
            Console.WriteLine("  📊 解析CSV数据...");
            Console.WriteLine("     分割列数据");
            Console.WriteLine("     处理缺失值");
            Console.WriteLine("     数据类型转换");
            Thread.Sleep(500);
        }

        protected override void AnalyzeData()
        {
            Console.WriteLine("  🔍 分析CSV数据...");
            Console.WriteLine("     统计分析：");
            Console.WriteLine("     - 计算均值、中位数、标准差");
            Console.WriteLine("     - 数据分布分析");
            Console.WriteLine("     - 相关性分析");
            Console.WriteLine("     - 异常值检测");
            Thread.Sleep(1000);
        }
    }

    // 具体类 - 数据库挖掘
    public class DatabaseMiner : DataMiner
    {
        private string _connectionString = string.Empty;

        protected override void OpenFile(string path)
        {
            _connectionString = path;
            Console.WriteLine($"  🗄️ 连接数据库: {path}");
            Console.WriteLine("     建立数据库连接");
            Console.WriteLine("     验证权限");
        }

        protected override void ExtractData()
        {
            Console.WriteLine("  📥 提取数据库数据...");
            Console.WriteLine("     执行查询语句");
            Console.WriteLine("     获取结果集");
            Console.WriteLine("     缓存数据到内存");
            Thread.Sleep(700);
        }

        protected override void CloseFile()
        {
            Console.WriteLine("  ✅ 关闭数据库连接");
            Console.WriteLine("     释放资源");
        }

        protected override void ParseData()
        {
            Console.WriteLine("  📊 解析数据库记录...");
            Console.WriteLine("     映射到对象模型");
            Console.WriteLine("     处理关联数据");
            Console.WriteLine("     数据完整性检查");
            Thread.Sleep(600);
        }

        protected override void AnalyzeData()
        {
            Console.WriteLine("  🔍 分析数据库数据...");
            Console.WriteLine("     业务分析：");
            Console.WriteLine("     - 用户行为分析");
            Console.WriteLine("     - 事务模式识别");
            Console.WriteLine("     - 性能瓶颈分析");
            Console.WriteLine("     - 数据增长趋势");
            Thread.Sleep(900);
        }

        protected override void SendReport()
        {
            Console.WriteLine("  📧 生成并发送数据库分析报告...");
            Console.WriteLine("     生成HTML报告");
            Console.WriteLine("     包含图表和表格");
            Console.WriteLine("     发送给数据库管理员");
        }
    }

    // 具体类 - Web爬虫数据挖掘
    public class WebScraperMiner : DataMiner
    {
        protected override void OpenFile(string path)
        {
            Console.WriteLine($"  🌐 访问网页: {path}");
            Console.WriteLine("     发送HTTP请求");
            Console.WriteLine("     等待响应");
        }

        protected override void ExtractData()
        {
            Console.WriteLine("  📥 抓取网页数据...");
            Console.WriteLine("     下载HTML内容");
            Console.WriteLine("     提取JavaScript渲染内容");
            Console.WriteLine("     下载相关资源");
            Thread.Sleep(1000);
        }

        protected override void CloseFile()
        {
            Console.WriteLine("  ✅ 关闭HTTP连接");
        }

        protected override void ParseData()
        {
            Console.WriteLine("  📊 解析HTML内容...");
            Console.WriteLine("     DOM树构建");
            Console.WriteLine("     CSS选择器匹配");
            Console.WriteLine("     提取结构化数据");
            Console.WriteLine("     清理HTML标签");
            Thread.Sleep(700);
        }

        protected override void AnalyzeData()
        {
            Console.WriteLine("  🔍 分析网页数据...");
            Console.WriteLine("     内容分析：");
            Console.WriteLine("     - 关键词提取");
            Console.WriteLine("     - 情感分析");
            Console.WriteLine("     - 链接分析");
            Console.WriteLine("     - SEO评分");
            Thread.Sleep(800);
        }

        protected override void SendReport()
        {
            Console.WriteLine("  📧 发送爬虫分析报告...");
            Console.WriteLine("     生成JSON格式报告");
            Console.WriteLine("     包含爬取统计");
            Console.WriteLine("     发送到API端点");
        }
    }
}
