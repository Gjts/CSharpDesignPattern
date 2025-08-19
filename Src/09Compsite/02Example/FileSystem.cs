namespace _09Compsite._02Example
{
    // 文件系统组件抽象类
    public abstract class FileSystemComponent
    {
        protected string name;
        
        public FileSystemComponent(string name)
        {
            this.name = name;
        }

        public abstract void Add(FileSystemComponent component);
        public abstract void Remove(FileSystemComponent component);
        public abstract void Display(int depth);
        public abstract long GetSize();
    }

    // 文件类（叶子节点）
    public class File : FileSystemComponent
    {
        private long size;

        public File(string name, long size) : base(name)
        {
            this.size = size;
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
            Console.WriteLine(new string(' ', depth) + "📄 " + name + $" ({size} bytes)");
        }

        public override long GetSize()
        {
            return size;
        }
    }

    // 文件夹类（组合节点）
    public class Directory : FileSystemComponent
    {
        private List<FileSystemComponent> children = new List<FileSystemComponent>();

        public Directory(string name) : base(name)
        {
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
            Console.WriteLine(new string(' ', depth) + "📁 " + name + $" ({GetSize()} bytes)");
            
            foreach (FileSystemComponent component in children)
            {
                component.Display(depth + 2);
            }
        }

        public override long GetSize()
        {
            long totalSize = 0;
            foreach (FileSystemComponent component in children)
            {
                totalSize += component.GetSize();
            }
            return totalSize;
        }
    }
}