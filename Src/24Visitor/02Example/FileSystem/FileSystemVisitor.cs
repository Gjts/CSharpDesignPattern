namespace _24Visitor._02Example.FileSystem
{
    // 访问者接口
    public interface IFileSystemVisitor
    {
        void VisitTextFile(TextFile file);
        void VisitImageFile(ImageFile file);
        void VisitVideoFile(VideoFile file);
        void VisitDirectory(Directory directory);
    }

    // 元素接口
    public interface IFileSystemElement
    {
        void Accept(IFileSystemVisitor visitor);
        string Name { get; }
    }

    // 具体元素 - 文本文件
    public class TextFile : IFileSystemElement
    {
        public string Name { get; }
        public int Lines { get; }
        public string Encoding { get; }

        public TextFile(string name, int lines, string encoding = "UTF-8")
        {
            Name = name;
            Lines = lines;
            Encoding = encoding;
        }

        public void Accept(IFileSystemVisitor visitor)
        {
            visitor.VisitTextFile(this);
        }
    }

    // 具体元素 - 图片文件
    public class ImageFile : IFileSystemElement
    {
        public string Name { get; }
        public int Width { get; }
        public int Height { get; }
        public string Format { get; }

        public ImageFile(string name, int width, int height, string format)
        {
            Name = name;
            Width = width;
            Height = height;
            Format = format;
        }

        public void Accept(IFileSystemVisitor visitor)
        {
            visitor.VisitImageFile(this);
        }
    }

    // 具体元素 - 视频文件
    public class VideoFile : IFileSystemElement
    {
        public string Name { get; }
        public int Duration { get; } // 秒
        public string Resolution { get; }
        public string Codec { get; }

        public VideoFile(string name, int duration, string resolution, string codec)
        {
            Name = name;
            Duration = duration;
            Resolution = resolution;
            Codec = codec;
        }

        public void Accept(IFileSystemVisitor visitor)
        {
            visitor.VisitVideoFile(this);
        }
    }

    // 具体元素 - 目录
    public class Directory : IFileSystemElement
    {
        public string Name { get; }
        public List<IFileSystemElement> Elements { get; }

        public Directory(string name)
        {
            Name = name;
            Elements = new List<IFileSystemElement>();
        }

        public void Add(IFileSystemElement element)
        {
            Elements.Add(element);
        }

        public void Accept(IFileSystemVisitor visitor)
        {
            visitor.VisitDirectory(this);
            foreach (var element in Elements)
            {
                element.Accept(visitor);
            }
        }
    }

    // 具体访问者 - 文件大小计算器
    public class FileSizeCalculator : IFileSystemVisitor
    {
        public long TotalSize { get; private set; }
        private int _indentLevel = 0;

        public void VisitTextFile(TextFile file)
        {
            long size = file.Lines * 50; // 假设每行平均50字节
            TotalSize += size;
            PrintIndented($"📄 {file.Name}: {size:N0} 字节");
        }

        public void VisitImageFile(ImageFile file)
        {
            long size = file.Width * file.Height * 3; // RGB每像素3字节
            TotalSize += size;
            PrintIndented($"🖼️ {file.Name}: {size:N0} 字节");
        }

        public void VisitVideoFile(VideoFile file)
        {
            long size = file.Duration * 1024 * 1024; // 假设每秒1MB
            TotalSize += size;
            PrintIndented($"🎬 {file.Name}: {size:N0} 字节");
        }

        public void VisitDirectory(Directory directory)
        {
            PrintIndented($"📁 {directory.Name}/");
            _indentLevel++;
        }

        private void PrintIndented(string text)
        {
            Console.WriteLine($"{new string(' ', _indentLevel * 2)}{text}");
        }
    }

    // 具体访问者 - 文件搜索器
    public class FileSearchVisitor : IFileSystemVisitor
    {
        private string _searchPattern;
        public List<string> FoundFiles { get; }

        public FileSearchVisitor(string searchPattern)
        {
            _searchPattern = searchPattern.ToLower();
            FoundFiles = new List<string>();
        }

        public void VisitTextFile(TextFile file)
        {
            if (file.Name.ToLower().Contains(_searchPattern))
            {
                FoundFiles.Add($"文本文件: {file.Name}");
                Console.WriteLine($"  ✅ 找到文本文件: {file.Name}");
            }
        }

        public void VisitImageFile(ImageFile file)
        {
            if (file.Name.ToLower().Contains(_searchPattern))
            {
                FoundFiles.Add($"图片文件: {file.Name}");
                Console.WriteLine($"  ✅ 找到图片文件: {file.Name}");
            }
        }

        public void VisitVideoFile(VideoFile file)
        {
            if (file.Name.ToLower().Contains(_searchPattern))
            {
                FoundFiles.Add($"视频文件: {file.Name}");
                Console.WriteLine($"  ✅ 找到视频文件: {file.Name}");
            }
        }

        public void VisitDirectory(Directory directory)
        {
            if (directory.Name.ToLower().Contains(_searchPattern))
            {
                FoundFiles.Add($"目录: {directory.Name}");
                Console.WriteLine($"  ✅ 找到目录: {directory.Name}");
            }
        }
    }

    // 具体访问者 - 文件统计器
    public class FileStatisticsVisitor : IFileSystemVisitor
    {
        public int TextFileCount { get; private set; }
        public int ImageFileCount { get; private set; }
        public int VideoFileCount { get; private set; }
        public int DirectoryCount { get; private set; }
        public int TotalLines { get; private set; }
        public int TotalDuration { get; private set; }

        public void VisitTextFile(TextFile file)
        {
            TextFileCount++;
            TotalLines += file.Lines;
        }

        public void VisitImageFile(ImageFile file)
        {
            ImageFileCount++;
        }

        public void VisitVideoFile(VideoFile file)
        {
            VideoFileCount++;
            TotalDuration += file.Duration;
        }

        public void VisitDirectory(Directory directory)
        {
            DirectoryCount++;
        }

        public void PrintStatistics()
        {
            Console.WriteLine("\n📊 文件系统统计:");
            Console.WriteLine($"  目录数量: {DirectoryCount}");
            Console.WriteLine($"  文本文件: {TextFileCount} 个，共 {TotalLines} 行");
            Console.WriteLine($"  图片文件: {ImageFileCount} 个");
            Console.WriteLine($"  视频文件: {VideoFileCount} 个，总时长 {TotalDuration} 秒");
            Console.WriteLine($"  文件总数: {TextFileCount + ImageFileCount + VideoFileCount}");
        }
    }
}
