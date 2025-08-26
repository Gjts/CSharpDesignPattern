namespace _Composite._02Example.FileSystem
{
    // 组件接口
    public abstract class FileSystemComponent
    {
        protected string name;
        
        public FileSystemComponent(string name)
        {
            this.name = name;
        }

        public abstract void Display(int depth);
        public abstract long GetSize();
    }

    // 叶子节点 - 文件
    public class File : FileSystemComponent
    {
        private long size;

        public File(string name, long size) : base(name)
        {
            this.size = size;
        }

        public override void Display(int depth)
        {
            Console.WriteLine($"{new string(' ', depth * 2)}📄 {name} ({size} bytes)");
        }

        public override long GetSize()
        {
            return size;
        }
    }

    // 组合节点 - 文件夹
    public class Directory : FileSystemComponent
    {
        private List<FileSystemComponent> children = new List<FileSystemComponent>();

        public Directory(string name) : base(name) { }

        public void Add(FileSystemComponent component)
        {
            children.Add(component);
        }

        public void Remove(FileSystemComponent component)
        {
            children.Remove(component);
        }

        public override void Display(int depth)
        {
            Console.WriteLine($"{new string(' ', depth * 2)}📁 {name}/");
            foreach (var child in children)
            {
                child.Display(depth + 1);
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
    }
}