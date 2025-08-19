namespace _10Decorator._02Example
{
    // 文本处理接口
    public interface ITextProcessor
    {
        string Process(string text);
    }

    // 基础文本处理器
    public class PlainTextProcessor : ITextProcessor
    {
        public string Process(string text)
        {
            return text;
        }
    }

    // 文本装饰器抽象类
    public abstract class TextDecorator : ITextProcessor
    {
        protected ITextProcessor processor;

        public TextDecorator(ITextProcessor processor)
        {
            this.processor = processor;
        }

        public virtual string Process(string text)
        {
            return processor.Process(text);
        }
    }

    // 具体装饰器：大写转换
    public class UpperCaseDecorator : TextDecorator
    {
        public UpperCaseDecorator(ITextProcessor processor) : base(processor)
        {
        }

        public override string Process(string text)
        {
            string result = base.Process(text);
            return result.ToUpper();
        }
    }

    // 具体装饰器：移除空格
    public class TrimDecorator : TextDecorator
    {
        public TrimDecorator(ITextProcessor processor) : base(processor)
        {
        }

        public override string Process(string text)
        {
            string result = base.Process(text);
            return result.Trim();
        }
    }

    // 具体装饰器：添加引号
    public class QuoteDecorator : TextDecorator
    {
        public QuoteDecorator(ITextProcessor processor) : base(processor)
        {
        }

        public override string Process(string text)
        {
            string result = base.Process(text);
            return $"\"{result}\"";
        }
    }

    // 具体装饰器：替换敏感词
    public class CensorDecorator : TextDecorator
    {
        private string[] sensitiveWords = { "bad", "evil", "hate" };

        public CensorDecorator(ITextProcessor processor) : base(processor)
        {
        }

        public override string Process(string text)
        {
            string result = base.Process(text);
            foreach (var word in sensitiveWords)
            {
                result = result.Replace(word, "***", StringComparison.OrdinalIgnoreCase);
            }
            return result;
        }
    }

    // 具体装饰器：添加HTML标签
    public class HtmlDecorator : TextDecorator
    {
        private string tag;

        public HtmlDecorator(ITextProcessor processor, string tag) : base(processor)
        {
            this.tag = tag;
        }

        public override string Process(string text)
        {
            string result = base.Process(text);
            return $"<{tag}>{result}</{tag}>";
        }
    }
}