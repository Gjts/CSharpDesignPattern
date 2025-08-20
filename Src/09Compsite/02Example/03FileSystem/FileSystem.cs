namespace _09Compsite._02Example._03FileSystem
{
    // 文件系统组件抽象类
    public abstract class FileSystemComponent
    {
        protected string name;
        protected DateTime createdTime;
        protected string owner;

        public FileSystemComponent(string name, string owner)
        {
            this.name = name;
            this.owner = owner;
            this.createdTime = DateTime.Now;
        }

        public abstract void Add(FileSystemComponent component);
        public abstract void Remove(FileSystemComponent component);
        public abstract void Display(int depth);
        public abstract long GetSize();
        public abstract int GetFileCount();
    }

    // 文件类（叶子节点）
    public class File : FileSystemComponent
    {
        private long size;
        private string extension;

        public File(string name, long size, string owner) : base(name, owner)
        {
            this.size = size;
            var parts = name.Split('.');
            this.extension = parts.Length > 1 ? parts[parts.Length - 1] : "";
        }

        public override void Add(FileSystemComponent component)
        {
            throw new NotSupportedException("文件不能添加子项");
        }

        public override void Remove(FileSystemComponent component)
        {
            throw new NotSupportedException("文件不能移除子项");
        }

        public override void Display(int depth)
        {
            string icon = extension switch
            {
                "txt" => "📄",
                "pdf" => "📕",
                "doc" or "docx" => "📘",
                "xls" or "xlsx" => "📊",
                "jpg" or "png" => "🖼️",
                "mp3" or "wav" => "🎵",
                "mp4" or "avi" => "🎬",
                "zip" or "rar" => "📦",
                _ => "📄"
            };

            Console.WriteLine(new string(' ', depth) + 
                $"{icon} {name} ({FormatFileSize(size)}) - 所有者: {owner} - 创建时间: {createdTime:yyyy-MM-dd HH:mm}");
        }

        public override long GetSize()
        {
            return size;
        }

        public override int GetFileCount()
        {
            return 1;
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double size = bytes;
            
            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size = size / 1024;
            }
            
            return $"{size:F2} {sizes[order]}";
        }
    }

    // 文件夹类（组合节点）
    public class Directory : FileSystemComponent
    {
        private List<FileSystemComponent> children = new List<FileSystemComponent>();
        private string permissions;

        public Directory(string name, string owner, string permissions = "rwxr-xr-x") 
            : base(name, owner)
        {
            this.permissions = permissions;
        }

        public override void Add(FileSystemComponent component)
        {
            children.Add(component);
        }

        public override void Remove(FileSystemComponent component)
        {
            children.Remove(component);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string(' ', depth) + 
                $"📁 {name}/ - 所有者: {owner} - 权限: {permissions} - " +
                $"包含: {GetFileCount()} 个文件 - 大小: {FormatFileSize(GetSize())}");
            
            foreach (var child in children)
            {
                child.Display(depth + 2);
            }
        }

        public override long GetSize()
        {
            long totalSize = 0;
            foreach (var child in children)
            {
                totalSize += child.GetSize();
            }
            return totalSize;
        }

        public override int GetFileCount()
        {
            int count = 0;
            foreach (var child in children)
            {
                count += child.GetFileCount();
            }
            return count;
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double size = bytes;
            
            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size = size / 1024;
            }
            
            return $"{size:F2} {sizes[order]}";
        }

        // 搜索文件
        public List<File> SearchFiles(string keyword)
        {
            List<File> results = new List<File>();
            SearchFilesRecursive(this, keyword, results);
            return results;
        }

        private void SearchFilesRecursive(FileSystemComponent component, string keyword, List<File> results)
        {
            if (component is File file && file.name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(file);
            }
            else if (component is Directory dir)
            {
                foreach (var child in dir.children)
                {
                    SearchFilesRecursive(child, keyword, results);
                }
            }
        }

        // 获取指定扩展名的文件
        public List<File> GetFilesByExtension(string extension)
        {
            List<File> results = new List<File>();
            GetFilesByExtensionRecursive(this, extension, results);
            return results;
        }

        private void GetFilesByExtensionRecursive(FileSystemComponent component, string extension, List<File> results)
        {
            if (component is File file && file.name.EndsWith($".{extension}", StringComparison.OrdinalIgnoreCase))
            {
                results.Add(file);
            }
            else if (component is Directory dir)
            {
                foreach (var child in dir.children)
                {
                    GetFilesByExtensionRecursive(child, extension, results);
                }
            }
        }

        // 生成目录树报告
        public void GenerateTreeReport()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine($"目录树报告 - {name}");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"生成时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"所有者: {owner}");
            Console.WriteLine($"权限: {permissions}");
            Console.WriteLine(new string('-', 60));
            
            Display(0);
            
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("统计信息:");
            Console.WriteLine($"  总文件数: {GetFileCount()}");
            Console.WriteLine($"  总大小: {FormatFileSize(GetSize())}");
            
            // 按扩展名统计
            var extensions = new Dictionary<string, int>();
            CountExtensions(this, extensions);
            
            if (extensions.Count > 0)
            {
                Console.WriteLine("\n文件类型分布:");
                foreach (var ext in extensions.OrderByDescending(e => e.Value))
                {
                    Console.WriteLine($"  .{ext.Key}: {ext.Value} 个文件");
                }
            }
            
            Console.WriteLine(new string('=', 60));
        }

        private void CountExtensions(FileSystemComponent component, Dictionary<string, int> extensions)
        {
            if (component is File file)
            {
                var parts = file.name.Split('.');
                if (parts.Length > 1)
                {
                    var ext = parts[parts.Length - 1];
                    if (extensions.ContainsKey(ext))
                        extensions[ext]++;
                    else
                        extensions[ext] = 1;
                }
            }
            else if (component is Directory dir)
            {
                foreach (var child in dir.children)
                {
                    CountExtensions(child, extensions);
                }
            }
        }
    }
}