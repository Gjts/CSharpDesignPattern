namespace _24Visitor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 访问者模式 (Visitor Pattern) ================================");
            Console.WriteLine("适用场景：需要对对象结构中的对象进行很多不同且不相关的操作；需要避免让这些操作污染对象的类");
            Console.WriteLine("特点：将数据结构与数据操作分离，使得操作集合可以独立于数据结构变化");
            Console.WriteLine("优点：增加新的操作很容易；将相关操作集中到访问者中；访问者可以跨类层次访问\n");

            Console.WriteLine("-------------------------------- 文档导出系统 ----------------------------------");
            
            // 创建文档
            var document = new Document();
            document.AddElement(new TextElement("这是一段重要的文本内容"));
            document.AddElement(new ImageElement("architecture.png"));
            document.AddElement(new TableElement(5, 3));
            document.AddElement(new TextElement("总结性文字"));
            
            Console.WriteLine("1. 导出为HTML格式：");
            var htmlExporter = new HtmlExporter();
            document.Accept(htmlExporter);
            
            Console.WriteLine("\n2. 导出为PDF格式：");
            var pdfExporter = new PdfExporter();
            document.Accept(pdfExporter);
            
            Console.WriteLine("\n3. 导出为Markdown格式：");
            var markdownExporter = new MarkdownExporter();
            document.Accept(markdownExporter);

            Console.WriteLine("\n-------------------------------- 图形计算系统 ----------------------------------");
            
            var shapes = new List<IShape>
            {
                new Circle(5),
                new Rectangle(10, 20),
                new Triangle(3, 4, 5)
            };
            
            Console.WriteLine("图形列表：圆形(r=5)、矩形(10x20)、三角形(3,4,5)\n");
            
            Console.WriteLine("1. 计算面积：");
            var areaCalculator = new AreaCalculator();
            foreach (var shape in shapes)
            {
                shape.Accept(areaCalculator);
            }
            Console.WriteLine($"   总面积: {areaCalculator.TotalArea:F2}");
            
            Console.WriteLine("\n2. 绘制图形：");
            var drawingVisitor = new DrawingVisitor();
            foreach (var shape in shapes)
            {
                shape.Accept(drawingVisitor);
            }
            
            Console.WriteLine("\n3. 计算周长：");
            var perimeterCalculator = new PerimeterCalculator();
            foreach (var shape in shapes)
            {
                shape.Accept(perimeterCalculator);
            }
            Console.WriteLine($"   总周长: {perimeterCalculator.TotalPerimeter:F2}");
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 访问者模式将操作从数据结构中分离出来");
            Console.WriteLine("- 新增操作只需新增访问者，无需修改元素类");
            Console.WriteLine("- 适合数据结构稳定但操作经常变化的场景");
        }
    }
}
