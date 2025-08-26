namespace _23Strategy._02Example.CompressionStrategy
{
    // 压缩策略接口
    public interface ICompressionStrategy
    {
        string AlgorithmName { get; }
        byte[] Compress(byte[] data);
        byte[] Decompress(byte[] compressedData);
        double GetCompressionRatio(byte[] original, byte[] compressed);
    }

    // 文件压缩器上下文
    public class FileCompressor
    {
        private ICompressionStrategy _strategy;

        public void SetCompressionStrategy(ICompressionStrategy strategy)
        {
            _strategy = strategy;
            Console.WriteLine($"  选择压缩算法: {strategy.AlgorithmName}");
        }

        public void CompressFile(string fileName, byte[] fileData)
        {
            if (_strategy == null)
            {
                Console.WriteLine("  ❌ 请先选择压缩算法");
                return;
            }

            Console.WriteLine($"\n压缩文件: {fileName}");
            Console.WriteLine($"  原始大小: {fileData.Length:N0} 字节");

            var startTime = DateTime.Now;
            byte[] compressedData = _strategy.Compress(fileData);
            var duration = (DateTime.Now - startTime).TotalMilliseconds;

            Console.WriteLine($"  压缩后大小: {compressedData.Length:N0} 字节");
            double ratio = _strategy.GetCompressionRatio(fileData, compressedData);
            Console.WriteLine($"  压缩率: {ratio:P2}");
            Console.WriteLine($"  压缩时间: {duration:F2} ms");

            // 验证解压
            byte[] decompressedData = _strategy.Decompress(compressedData);
            bool isValid = decompressedData.Length == fileData.Length;
            Console.WriteLine($"  验证解压: {(isValid ? "✅ 成功" : "❌ 失败")}");
        }
    }

    // 具体策略 - ZIP压缩
    public class ZipCompressionStrategy : ICompressionStrategy
    {
        public string AlgorithmName => "ZIP";

        public byte[] Compress(byte[] data)
        {
            // 模拟ZIP压缩
            Console.WriteLine("  使用ZIP算法压缩中...");
            Thread.Sleep(100);
            
            // 简单模拟：返回70%的数据大小
            int compressedSize = (int)(data.Length * 0.7);
            byte[] compressed = new byte[compressedSize];
            Array.Copy(data, compressed, Math.Min(data.Length, compressedSize));
            return compressed;
        }

        public byte[] Decompress(byte[] compressedData)
        {
            // 模拟ZIP解压
            int originalSize = (int)(compressedData.Length / 0.7);
            byte[] decompressed = new byte[originalSize];
            Array.Copy(compressedData, decompressed, compressedData.Length);
            return decompressed;
        }

        public double GetCompressionRatio(byte[] original, byte[] compressed)
        {
            return 1.0 - (double)compressed.Length / original.Length;
        }
    }

    // 具体策略 - RAR压缩
    public class RarCompressionStrategy : ICompressionStrategy
    {
        public string AlgorithmName => "RAR";

        public byte[] Compress(byte[] data)
        {
            // 模拟RAR压缩
            Console.WriteLine("  使用RAR算法压缩中...");
            Thread.Sleep(150);
            
            // 简单模拟：返回65%的数据大小（RAR通常压缩率更高）
            int compressedSize = (int)(data.Length * 0.65);
            byte[] compressed = new byte[compressedSize];
            Array.Copy(data, compressed, Math.Min(data.Length, compressedSize));
            return compressed;
        }

        public byte[] Decompress(byte[] compressedData)
        {
            // 模拟RAR解压
            int originalSize = (int)(compressedData.Length / 0.65);
            byte[] decompressed = new byte[originalSize];
            Array.Copy(compressedData, decompressed, compressedData.Length);
            return decompressed;
        }

        public double GetCompressionRatio(byte[] original, byte[] compressed)
        {
            return 1.0 - (double)compressed.Length / original.Length;
        }
    }

    // 具体策略 - 7Z压缩
    public class SevenZipCompressionStrategy : ICompressionStrategy
    {
        public string AlgorithmName => "7-Zip";

        public byte[] Compress(byte[] data)
        {
            // 模拟7Z压缩
            Console.WriteLine("  使用7-Zip算法压缩中...");
            Thread.Sleep(200);
            
            // 简单模拟：返回60%的数据大小（7Z压缩率最高）
            int compressedSize = (int)(data.Length * 0.6);
            byte[] compressed = new byte[compressedSize];
            Array.Copy(data, compressed, Math.Min(data.Length, compressedSize));
            return compressed;
        }

        public byte[] Decompress(byte[] compressedData)
        {
            // 模拟7Z解压
            int originalSize = (int)(compressedData.Length / 0.6);
            byte[] decompressed = new byte[originalSize];
            Array.Copy(compressedData, decompressed, compressedData.Length);
            return decompressed;
        }

        public double GetCompressionRatio(byte[] original, byte[] compressed)
        {
            return 1.0 - (double)compressed.Length / original.Length;
        }
    }

    // 具体策略 - GZIP压缩（适合文本）
    public class GzipCompressionStrategy : ICompressionStrategy
    {
        public string AlgorithmName => "GZIP";

        public byte[] Compress(byte[] data)
        {
            // 模拟GZIP压缩
            Console.WriteLine("  使用GZIP算法压缩中...");
            Thread.Sleep(80);
            
            // 简单模拟：返回75%的数据大小
            int compressedSize = (int)(data.Length * 0.75);
            byte[] compressed = new byte[compressedSize];
            Array.Copy(data, compressed, Math.Min(data.Length, compressedSize));
            return compressed;
        }

        public byte[] Decompress(byte[] compressedData)
        {
            // 模拟GZIP解压
            int originalSize = (int)(compressedData.Length / 0.75);
            byte[] decompressed = new byte[originalSize];
            Array.Copy(compressedData, decompressed, compressedData.Length);
            return decompressed;
        }

        public double GetCompressionRatio(byte[] original, byte[] compressed)
        {
            return 1.0 - (double)compressed.Length / original.Length;
        }
    }

    // 具体策略 - 无压缩（存储模式）
    public class NoCompressionStrategy : ICompressionStrategy
    {
        public string AlgorithmName => "无压缩（存储）";

        public byte[] Compress(byte[] data)
        {
            Console.WriteLine("  使用存储模式（无压缩）...");
            Thread.Sleep(10);
            return (byte[])data.Clone();
        }

        public byte[] Decompress(byte[] compressedData)
        {
            return (byte[])compressedData.Clone();
        }

        public double GetCompressionRatio(byte[] original, byte[] compressed)
        {
            return 0.0; // 无压缩
        }
    }
}