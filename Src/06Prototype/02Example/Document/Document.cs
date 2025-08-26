namespace _Prototype._02Example.Document
{
    // 原型接口
    public interface IDocumentPrototype
    {
        IDocumentPrototype Clone();
        void Display();
    }

    // 具体原型 - 文档类
    public class Document : IDocumentPrototype
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Images { get; set; }
        public DateTime CreatedDate { get; set; }
        public Dictionary<string, string> Metadata { get; set; }

        public Document()
        {
            Images = new List<string>();
            Metadata = new Dictionary<string, string>();
            CreatedDate = DateTime.Now;
        }

        // 深拷贝
        public IDocumentPrototype Clone()
        {
            var cloned = new Document
            {
                Title = this.Title,
                Content = this.Content,
                CreatedDate = this.CreatedDate,
                Images = new List<string>(this.Images),
                Metadata = new Dictionary<string, string>(this.Metadata)
            };
            return cloned;
        }

        public void Display()
        {
            Console.WriteLine($"  文档标题: {Title}");
            Console.WriteLine($"  内容: {Content}");
            Console.WriteLine($"  创建时间: {CreatedDate:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"  图片数量: {Images.Count}");
            Console.WriteLine($"  元数据: {string.Join(", ", Metadata.Select(kv => $"{kv.Key}={kv.Value}"))}");
        }
    }

    // 文档管理器
    public class DocumentManager
    {
        private Dictionary<string, Document> _templates = new Dictionary<string, Document>();

        public void AddTemplate(string name, Document template)
        {
            _templates[name] = template;
        }

        public Document GetDocument(string templateName)
        {
            if (_templates.ContainsKey(templateName))
            {
                return (Document)_templates[templateName].Clone();
            }
            return null;
        }
    }
}