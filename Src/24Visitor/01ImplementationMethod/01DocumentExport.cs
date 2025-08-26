namespace _24Visitor
{
    // 访问者接口
    public interface IDocumentVisitor
    {
        void Visit(TextElement element);
        void Visit(ImageElement element);
        void Visit(TableElement element);
    }

    // 元素接口
    public interface IDocumentElement
    {
        void Accept(IDocumentVisitor visitor);
    }

    // 文本元素
    public class TextElement : IDocumentElement
    {
        public string Content { get; }

        public TextElement(string content)
        {
            Content = content;
        }

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    // 图片元素
    public class ImageElement : IDocumentElement
    {
        public string Source { get; }

        public ImageElement(string source)
        {
            Source = source;
        }

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    // 表格元素
    public class TableElement : IDocumentElement
    {
        public int Rows { get; }
        public int Columns { get; }

        public TableElement(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    // HTML导出访问者
    public class HtmlExporter : IDocumentVisitor
    {
        public void Visit(TextElement element)
        {
            Console.WriteLine($"  <p>{element.Content}</p>");
        }

        public void Visit(ImageElement element)
        {
            Console.WriteLine($"  <img src=\"{element.Source}\" />");
        }

        public void Visit(TableElement element)
        {
            Console.WriteLine($"  <table rows=\"{element.Rows}\" cols=\"{element.Columns}\"></table>");
        }
    }

    // PDF导出访问者
    public class PdfExporter : IDocumentVisitor
    {
        public void Visit(TextElement element)
        {
            Console.WriteLine($"  PDF文本: {element.Content}");
        }

        public void Visit(ImageElement element)
        {
            Console.WriteLine($"  PDF图片: 嵌入 {element.Source}");
        }

        public void Visit(TableElement element)
        {
            Console.WriteLine($"  PDF表格: {element.Rows}行 x {element.Columns}列");
        }
    }

    // Markdown导出访问者
    public class MarkdownExporter : IDocumentVisitor
    {
        public void Visit(TextElement element)
        {
            Console.WriteLine($"  {element.Content}");
        }

        public void Visit(ImageElement element)
        {
            Console.WriteLine($"  ![图片]({element.Source})");
        }

        public void Visit(TableElement element)
        {
            Console.WriteLine($"  | 表格 {element.Rows}x{element.Columns} |");
        }
    }

    // 文档类
    public class Document
    {
        private List<IDocumentElement> _elements = new List<IDocumentElement>();

        public void AddElement(IDocumentElement element)
        {
            _elements.Add(element);
        }

        public void Accept(IDocumentVisitor visitor)
        {
            foreach (var element in _elements)
            {
                element.Accept(visitor);
            }
        }
    }

    // 图形访问者接口
    public interface IShapeVisitor
    {
        void Visit(Circle circle);
        void Visit(Rectangle rectangle);
        void Visit(Triangle triangle);
    }

    // 图形接口
    public interface IShape
    {
        void Accept(IShapeVisitor visitor);
    }

    // 圆形
    public class Circle : IShape
    {
        public double Radius { get; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public void Accept(IShapeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    // 矩形
    public class Rectangle : IShape
    {
        public double Width { get; }
        public double Height { get; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public void Accept(IShapeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    // 三角形
    public class Triangle : IShape
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }

        public Triangle(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public void Accept(IShapeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    // 面积计算访问者
    public class AreaCalculator : IShapeVisitor
    {
        public double TotalArea { get; private set; }

        public void Visit(Circle circle)
        {
            double area = Math.PI * circle.Radius * circle.Radius;
            Console.WriteLine($"圆形面积: {area:F2}");
            TotalArea += area;
        }

        public void Visit(Rectangle rectangle)
        {
            double area = rectangle.Width * rectangle.Height;
            Console.WriteLine($"矩形面积: {area:F2}");
            TotalArea += area;
        }

        public void Visit(Triangle triangle)
        {
            // 使用海伦公式
            double s = (triangle.A + triangle.B + triangle.C) / 2;
            double area = Math.Sqrt(s * (s - triangle.A) * (s - triangle.B) * (s - triangle.C));
            Console.WriteLine($"三角形面积: {area:F2}");
            TotalArea += area;
        }
    }

    // 绘制访问者
    public class DrawingVisitor : IShapeVisitor
    {
        public void Visit(Circle circle)
        {
            Console.WriteLine($"绘制圆形: 半径={circle.Radius}");
        }

        public void Visit(Rectangle rectangle)
        {
            Console.WriteLine($"绘制矩形: {rectangle.Width}x{rectangle.Height}");
        }

        public void Visit(Triangle triangle)
        {
            Console.WriteLine($"绘制三角形: 边长={triangle.A}, {triangle.B}, {triangle.C}");
        }
    }

    // 周长计算访问者
    public class PerimeterCalculator : IShapeVisitor
    {
        public double TotalPerimeter { get; private set; }

        public void Visit(Circle circle)
        {
            double perimeter = 2 * Math.PI * circle.Radius;
            TotalPerimeter += perimeter;
            Console.WriteLine($"   圆形周长: {perimeter:F2}");
        }

        public void Visit(Rectangle rectangle)
        {
            double perimeter = 2 * (rectangle.Width + rectangle.Height);
            TotalPerimeter += perimeter;
            Console.WriteLine($"   矩形周长: {perimeter:F2}");
        }

        public void Visit(Triangle triangle)
        {
            double perimeter = triangle.A + triangle.B + triangle.C;
            TotalPerimeter += perimeter;
            Console.WriteLine($"   三角形周长: {perimeter:F2}");
        }
    }
}
