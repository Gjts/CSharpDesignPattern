namespace _24Visitor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 访问者模式 (Visitor Pattern) ===\n");

            Console.WriteLine("示例1：文档导出");
            Console.WriteLine("------------------------");
            var document = new Document();
            document.AddElement(new TextElement("这是一段文本"));
            document.AddElement(new ImageElement("image.png"));
            document.AddElement(new TableElement(3, 4));
            
            var htmlExporter = new HtmlExporter();
            var pdfExporter = new PdfExporter();
            var markdownExporter = new MarkdownExporter();
            
            Console.WriteLine("导出为HTML:");
            document.Accept(htmlExporter);
            
            Console.WriteLine("\n导出为PDF:");
            document.Accept(pdfExporter);
            
            Console.WriteLine("\n导出为Markdown:");
            document.Accept(markdownExporter);

            Console.WriteLine("\n示例2：图形对象操作");
            Console.WriteLine("------------------------");
            var shapes = new List<IShape>
            {
                new Circle(5),
                new Rectangle(10, 20),
                new Triangle(3, 4, 5)
            };
            
            var areaCalculator = new AreaCalculator();
            var drawingVisitor = new DrawingVisitor();
            
            foreach (var shape in shapes)
            {
                shape.Accept(areaCalculator);
                shape.Accept(drawingVisitor);
            }
            
            Console.WriteLine($"\n总面积: {areaCalculator.TotalArea:F2}");
        }
    }
}
