namespace _Decorator._02Example.DataStream
{
    // 组件接口
    public interface IDataStream
    {
        void Write(string data);
        string Read();
    }

    // 具体组件 - 文件数据流
    public class FileDataStream : IDataStream
    {
        private string fileName;
        private string data = "";

        public FileDataStream(string fileName)
        {
            this.fileName = fileName;
        }

        public void Write(string data)
        {
            this.data = data;
            Console.WriteLine($"  写入文件 {fileName}: {data}");
        }

        public string Read()
        {
            Console.WriteLine($"  从文件 {fileName} 读取: {data}");
            return data;
        }
    }

    // 装饰器基类
    public abstract class DataStreamDecorator : IDataStream
    {
        protected IDataStream wrappee;

        public DataStreamDecorator(IDataStream stream)
        {
            this.wrappee = stream;
        }

        public virtual void Write(string data)
        {
            wrappee.Write(data);
        }

        public virtual string Read()
        {
            return wrappee.Read();
        }
    }

    // 具体装饰器 - 加密装饰器
    public class EncryptionDecorator : DataStreamDecorator
    {
        public EncryptionDecorator(IDataStream stream) : base(stream) { }

        public override void Write(string data)
        {
            string encrypted = Encrypt(data);
            Console.WriteLine($"  加密数据: {data} -> {encrypted}");
            base.Write(encrypted);
        }

        public override string Read()
        {
            string data = base.Read();
            string decrypted = Decrypt(data);
            Console.WriteLine($"  解密数据: {data} -> {decrypted}");
            return decrypted;
        }

        private string Encrypt(string data)
        {
            // 简单的加密模拟
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(data));
        }

        private string Decrypt(string data)
        {
            // 简单的解密模拟
            try
            {
                return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(data));
            }
            catch
            {
                return data;
            }
        }
    }

    // 具体装饰器 - 压缩装饰器
    public class CompressionDecorator : DataStreamDecorator
    {
        public CompressionDecorator(IDataStream stream) : base(stream) { }

        public override void Write(string data)
        {
            string compressed = Compress(data);
            Console.WriteLine($"  压缩数据: {data.Length} bytes -> {compressed.Length} bytes");
            base.Write(compressed);
        }

        public override string Read()
        {
            string data = base.Read();
            string decompressed = Decompress(data);
            Console.WriteLine($"  解压数据: {data.Length} bytes -> {decompressed.Length} bytes");
            return decompressed;
        }

        private string Compress(string data)
        {
            // 简单的压缩模拟
            return data.Replace(" ", "").Replace("\n", "");
        }

        private string Decompress(string data)
        {
            // 简单的解压模拟
            return data;
        }
    }
}
