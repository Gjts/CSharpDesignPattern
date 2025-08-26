namespace _15Template
{
    // 抽象报表生成模板类
    public abstract class ReportGenerator
    {
        // 模板方法 - 定义报表生成的骨架
        public void GenerateReport()
        {
            Console.WriteLine($"生成{GetReportType()}:");
            
            // 步骤1：收集数据
            var data = CollectData();
            
            // 步骤2：处理数据
            var processedData = ProcessData(data);
            
            // 步骤3：分析数据（可选）
            if (ShouldAnalyzeData())
            {
                var analysis = AnalyzeData(processedData);
                processedData = MergeAnalysis(processedData, analysis);
            }
            
            // 步骤4：创建报表
            CreateReportHeader();
            CreateReportBody(processedData);
            CreateReportFooter();
            
            // 步骤5：格式化报表
            FormatReport();
            
            // 步骤6：发送报表（可选）
            if (ShouldSendReport())
            {
                SendReport();
            }
            
            Console.WriteLine($"{GetReportType()}生成完成\n");
        }
        
        // 抽象方法 - 必须由子类实现
        protected abstract string GetReportType();
        protected abstract object CollectData();
        protected abstract object ProcessData(object data);
        protected abstract void CreateReportBody(object data);
        
        // 通用方法 - 可被子类重写
        protected virtual void CreateReportHeader()
        {
            Console.WriteLine($"  创建{GetReportType()}头部");
            Console.WriteLine($"  报表日期: {DateTime.Now:yyyy-MM-dd}");
        }
        
        protected virtual void CreateReportFooter()
        {
            Console.WriteLine($"  创建{GetReportType()}尾部");
        }
        
        protected virtual void FormatReport()
        {
            Console.WriteLine("  格式化报表...");
        }
        
        // 钩子方法
        protected virtual bool ShouldAnalyzeData() => false;
        protected virtual object AnalyzeData(object data) => data;
        protected virtual object MergeAnalysis(object data, object analysis) => data;
        
        protected virtual bool ShouldSendReport() => false;
        protected virtual void SendReport()
        {
            Console.WriteLine("  发送报表...");
        }
    }
    
    // 具体实现：销售报表
    public class SalesReport : ReportGenerator
    {
        protected override string GetReportType() => "销售报表";
        
        protected override object CollectData()
        {
            Console.WriteLine("  收集销售数据...");
            return new { Orders = 150, Revenue = 250000 };
        }
        
        protected override object ProcessData(object data)
        {
            Console.WriteLine("  处理销售数据...");
            Console.WriteLine("  计算销售额、利润率、同比增长...");
            return data;
        }
        
        protected override void CreateReportBody(object data)
        {
            Console.WriteLine("  创建销售报表主体:");
            Console.WriteLine("    - 本月订单数: 150");
            Console.WriteLine("    - 本月销售额: ¥250,000");
            Console.WriteLine("    - 同比增长: 15%");
        }
        
        protected override bool ShouldAnalyzeData() => true;
        protected override object AnalyzeData(object data)
        {
            Console.WriteLine("  分析销售趋势...");
            return new { Trend = "上升" };
        }
        
        protected override bool ShouldSendReport() => true;
        protected override void SendReport()
        {
            Console.WriteLine("  发送销售报表给管理层...");
        }
    }
    
    // 具体实现：库存报表
    public class InventoryReport : ReportGenerator
    {
        protected override string GetReportType() => "库存报表";
        
        protected override object CollectData()
        {
            Console.WriteLine("  收集库存数据...");
            return new { TotalItems = 500, LowStock = 25 };
        }
        
        protected override object ProcessData(object data)
        {
            Console.WriteLine("  处理库存数据...");
            Console.WriteLine("  统计库存量、预警项目...");
            return data;
        }
        
        protected override void CreateReportBody(object data)
        {
            Console.WriteLine("  创建库存报表主体:");
            Console.WriteLine("    - 总库存项: 500");
            Console.WriteLine("    - 低库存预警: 25项");
            Console.WriteLine("    - 库存周转率: 4.5");
        }
        
        protected override void FormatReport()
        {
            base.FormatReport();
            Console.WriteLine("  添加库存预警标记...");
        }
    }
    
    // 具体实现：财务报表
    public class FinancialReport : ReportGenerator
    {
        protected override string GetReportType() => "财务报表";
        
        protected override object CollectData()
        {
            Console.WriteLine("  收集财务数据...");
            Console.WriteLine("  从会计系统获取数据...");
            return new { Income = 500000, Expense = 300000 };
        }
        
        protected override object ProcessData(object data)
        {
            Console.WriteLine("  处理财务数据...");
            Console.WriteLine("  计算利润、现金流、资产负债...");
            return data;
        }
        
        protected override void CreateReportBody(object data)
        {
            Console.WriteLine("  创建财务报表主体:");
            Console.WriteLine("    - 收入: ¥500,000");
            Console.WriteLine("    - 支出: ¥300,000");
            Console.WriteLine("    - 净利润: ¥200,000");
            Console.WriteLine("    - 利润率: 40%");
        }
        
        protected override void CreateReportHeader()
        {
            base.CreateReportHeader();
            Console.WriteLine("  添加审计信息...");
        }
        
        protected override void CreateReportFooter()
        {
            base.CreateReportFooter();
            Console.WriteLine("  添加法律声明...");
        }
        
        protected override bool ShouldAnalyzeData() => true;
        protected override object AnalyzeData(object data)
        {
            Console.WriteLine("  进行财务分析...");
            Console.WriteLine("  计算财务指标...");
            return new { ROI = 0.25, ROE = 0.18 };
        }
    }
}