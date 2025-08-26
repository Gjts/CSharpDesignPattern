namespace _Prototype._02Example.Document
{
    public class ContractDocument : ICloneable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreateDate { get; set; }
        private List<string> Clauses { get; set; }

        public ContractDocument()
        {
            Clauses = new List<string>();
        }

        public void AddClause(string clause)
        {
            Clauses.Add(clause);
        }

        public object Clone()
        {
            var cloned = new ContractDocument
            {
                Title = this.Title,
                Content = this.Content,
                Author = this.Author,
                CreateDate = this.CreateDate,
                Clauses = new List<string>(this.Clauses)
            };
            return cloned;
        }

        public void Display()
        {
            Console.WriteLine($"  标题: {Title}");
            Console.WriteLine($"  内容: {Content}");
            Console.WriteLine($"  作者: {Author}");
            Console.WriteLine($"  创建时间: {CreateDate:yyyy-MM-dd HH:mm:ss}");
            if (Clauses.Count > 0)
            {
                Console.WriteLine($"  附加条款:");
                foreach (var clause in Clauses)
                {
                    Console.WriteLine($"    - {clause}");
                }
            }
        }
    }
}