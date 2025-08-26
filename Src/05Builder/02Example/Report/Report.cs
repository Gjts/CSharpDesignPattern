namespace _Builder._02Example.Report
{
    // 产品类 - 报表
    public class Report
    {
        public string Title { get; set; } = "";
        public string Header { get; set; } = "";
        public List<string> Sections { get; set; } = new List<string>();
        public string Footer { get; set; } = "";
        public string Format { get; set; } = "";

        public void Show()  // Changed from Display to Show
        {
            Console.WriteLine($"  === {Title} ({Format}格式) ===");
            Console.WriteLine($"  {Header}");
            foreach (var section in Sections)
            {
                Console.WriteLine($"  - {section}");
            }
            Console.WriteLine($"  {Footer}");
        }
    }

    // 抽象建造者
    public abstract class ReportBuilder
    {
        protected Report report = new Report();

        public abstract void SetTitle();
        public abstract void SetHeader();
        public abstract void AddSections();
        public abstract void SetFooter();
        public abstract void SetFormat();

        public Report GetReport()
        {
            return report;
        }
    }

    // 具体建造者 - PDF报表建造者
    public class PDFReportBuilder : ReportBuilder
    {
        public override void SetTitle()
        {
            report.Title = "月度销售报表";
        }

        public override void SetHeader()
        {
            report.Header = "2024年1月销售数据汇总";
        }

        public override void AddSections()
        {
            report.Sections.Add("销售总额: ￥1,234,567");
            report.Sections.Add("订单数量: 3,456");
            report.Sections.Add("客户数量: 890");
            report.Sections.Add("热销产品: 产品A, 产品B, 产品C");
        }

        public override void SetFooter()
        {
            report.Footer = "生成时间: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public override void SetFormat()
        {
            report.Format = "PDF";
        }
    }

    // 具体建造者 - Excel报表建造者
    public class ExcelReportBuilder : ReportBuilder
    {
        public override void SetTitle()
        {
            report.Title = "季度财务报表";
        }

        public override void SetHeader()
        {
            report.Header = "2024年第一季度财务数据";
        }

        public override void AddSections()
        {
            report.Sections.Add("总收入: ￥5,678,900");
            report.Sections.Add("总支出: ￥3,456,789");
            report.Sections.Add("净利润: ￥2,222,111");
            report.Sections.Add("利润率: 39.1%");
        }

        public override void SetFooter()
        {
            report.Footer = "财务部制作 | " + DateTime.Now.ToString("yyyy-MM-dd");
        }

        public override void SetFormat()
        {
            report.Format = "Excel";
        }
    }

    // 指挥者
    public class ReportDirector
    {
        private ReportBuilder? _builder;

        public ReportDirector()
        {
            // Default constructor
        }

        public ReportDirector(ReportBuilder builder)
        {
            _builder = builder;
        }

        public void SetBuilder(ReportBuilder builder)
        {
            _builder = builder;
        }

        public void ConstructFullReport()
        {
            var builder = _builder ?? throw new InvalidOperationException("请先通过 SetBuilder 指定建造者。");
            builder.SetTitle();
            builder.SetHeader();
            builder.AddSections();
            builder.SetFooter();
            builder.SetFormat();
        }

        public void ConstructSummaryReport()
        {
            var builder = _builder ?? throw new InvalidOperationException("请先通过 SetBuilder 指定建造者。");
            builder.SetTitle();
            builder.SetHeader();
            builder.AddSections();
            builder.SetFormat();
            // Skip footer for summary report
        }
    }
}
