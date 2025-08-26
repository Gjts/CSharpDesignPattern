namespace _Flyweight._02Example.TextEditor
{
    // 享元接口
    public interface ICharacter
    {
        void Display(int fontSize, string fontFamily, string color);
    }

    // 具体享元 - 字符
    public class Character : ICharacter
    {
        private char symbol;  // 内部状态

        public Character(char symbol)
        {
            this.symbol = symbol;
        }

        public void Display(int fontSize, string fontFamily, string color)  // 外部状态作为参数
        {
            Console.WriteLine($"  字符: '{symbol}' [字体: {fontFamily}, 大小: {fontSize}px, 颜色: {color}]");
        }
    }

    // 享元工厂
    public class CharacterFactory
    {
        private static Dictionary<char, ICharacter> characters = new Dictionary<char, ICharacter>();

        public ICharacter GetCharacter(char symbol)
        {
            if (!characters.ContainsKey(symbol))
            {
                Console.WriteLine($"  创建新字符对象: '{symbol}'");
                characters[symbol] = new Character(symbol);
            }
            else
            {
                Console.WriteLine($"  复用已有字符对象: '{symbol}'");
            }
            return characters[symbol];
        }

        public int GetCharacterCount()
        {
            return characters.Count;
        }

        public static void ShowPoolStatus()
        {
            Console.WriteLine($"\n字符池状态: 共享了 {characters.Count} 个不同字符对象");
            Console.WriteLine($"字符池内容: {string.Join(", ", characters.Keys.Select(c => $"'{c}'"))}");
        }
    }

    // 文档类
    public class Document
    {
        private CharacterFactory factory = new CharacterFactory();
        private List<(ICharacter character, int fontSize, string fontFamily, string color)> content = new();

        public void AddCharacter(char symbol, int fontSize, string fontFamily, string color)
        {
            var character = factory.GetCharacter(symbol);
            content.Add((character, fontSize, fontFamily, color));
        }

        public void Display()
        {
            Console.WriteLine("\n显示文档内容:");
            foreach (var item in content)
            {
                item.character.Display(item.fontSize, item.fontFamily, item.color);
            }
            Console.WriteLine($"\n统计: 文档共 {content.Count} 个字符，使用了 {factory.GetCharacterCount()} 个字符对象");
        }
    }

    // 文本编辑器类
    public class TextEditor
    {
        private Document document;

        public TextEditor()
        {
            document = new Document();
        }

        public void TypeText(string text)
        {
            foreach (char c in text)
            {
                // 为简单起见，使用固定的格式
                document.AddCharacter(c, 12, "Arial", "Black");
            }
        }

        public void Display()
        {
            document.Display();
        }
    }
}
