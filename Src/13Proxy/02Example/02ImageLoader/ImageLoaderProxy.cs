namespace _13Proxy._02Example._02ImageLoader
{
    // 图片接口
    public interface IImage
    {
        void Display();
        string GetInfo();
    }

    // 真实图片类
    public class RealImage : IImage
    {
        private string fileName;
        private byte[]? imageData;
        private int width;
        private int height;
        private string? format;

        public RealImage(string fileName)
        {
            this.fileName = fileName;
            LoadFromDisk();
        }

        private void LoadFromDisk()
        {
            Console.WriteLine($"[真实图片] 从磁盘加载图片: {fileName}");
            Console.WriteLine($"  读取文件...");
            
            // 模拟耗时的加载操作
            System.Threading.Thread.Sleep(2000);
            
            // 模拟图片数据
            Random random = new Random();
            int size = random.Next(1000000, 5000000); // 1MB-5MB
            imageData = new byte[size];
            width = random.Next(800, 4000);
            height = random.Next(600, 3000);
            format = Path.GetExtension(fileName).TrimStart('.');
            
            Console.WriteLine($"  ✓ 加载完成");
            Console.WriteLine($"  大小: {size / 1024.0 / 1024.0:F2} MB");
            Console.WriteLine($"  尺寸: {width}x{height}");
            Console.WriteLine($"  格式: {format?.ToUpper()}");
        }

        public void Display()
        {
            Console.WriteLine($"[真实图片] 显示图片: {fileName}");
            Console.WriteLine($"  渲染 {width}x{height} 像素...");
            Console.WriteLine($"  ✓ 图片已显示");
        }

        public string GetInfo()
        {
            return $"{fileName} ({width}x{height}, {imageData?.Length / 1024.0 / 1024.0:F2}MB)";
        }
    }

    // 图片代理类（虚拟代理 - 延迟加载）
    public class ProxyImage : IImage
    {
        private RealImage? realImage;
        private string fileName;
        private string thumbnailUrl;
        private bool isLoaded = false;

        public ProxyImage(string fileName)
        {
            this.fileName = fileName;
            this.thumbnailUrl = GenerateThumbnailUrl(fileName);
            Console.WriteLine($"[代理图片] 创建图片代理: {fileName}");
            Console.WriteLine($"  缩略图: {thumbnailUrl}");
        }

        private string GenerateThumbnailUrl(string fileName)
        {
            return $"https://cdn.example.com/thumbnails/{Path.GetFileNameWithoutExtension(fileName)}_thumb.jpg";
        }

        public void Display()
        {
            if (!isLoaded)
            {
                Console.WriteLine($"[代理图片] 首次显示，需要加载完整图片");
                
                // 显示占位符或缩略图
                Console.WriteLine($"[代理图片] 显示缩略图: {thumbnailUrl}");
                
                // 延迟加载真实图片
                if (realImage == null)
                {
                    realImage = new RealImage(fileName);
                    isLoaded = true;
                }
            }
            
            // 显示真实图片
            if (realImage != null)
            {
                realImage.Display();
            }
        }

        public string GetInfo()
        {
            // 不需要加载完整图片就可以返回基本信息
            if (realImage != null)
            {
                return realImage.GetInfo();
            }
            else
            {
                return $"{fileName} (未加载)";
            }
        }
    }

    // 智能图片代理（带缓存和预加载）
    public class SmartImageProxy : IImage
    {
        private static Dictionary<string, RealImage> imageCache = new Dictionary<string, RealImage>();
        private static Queue<string> lruQueue = new Queue<string>();
        private static int maxCacheSize = 5;
        
        private string fileName;
        private RealImage? cachedImage;

        public SmartImageProxy(string fileName)
        {
            this.fileName = fileName;
            Console.WriteLine($"[智能代理] 创建智能图片代理: {fileName}");
        }

        public void Display()
        {
            Console.WriteLine($"[智能代理] 请求显示图片: {fileName}");
            
            // 检查缓存
            if (imageCache.ContainsKey(fileName))
            {
                Console.WriteLine($"[智能代理] ✓ 缓存命中");
                cachedImage = imageCache[fileName];
            }
            else
            {
                Console.WriteLine($"[智能代理] ✗ 缓存未命中，加载图片");
                
                // 检查缓存大小
                if (imageCache.Count >= maxCacheSize)
                {
                    // LRU淘汰
                    string oldestFile = lruQueue.Dequeue();
                    imageCache.Remove(oldestFile);
                    Console.WriteLine($"[智能代理] 缓存已满，移除最旧图片: {oldestFile}");
                }
                
                // 加载并缓存
                cachedImage = new RealImage(fileName);
                imageCache[fileName] = cachedImage;
                lruQueue.Enqueue(fileName);
                
                Console.WriteLine($"[智能代理] 图片已缓存 (缓存数: {imageCache.Count}/{maxCacheSize})");
            }
            
            // 显示真实图片
            if (cachedImage != null)
            {
                cachedImage.Display();
            }
        }

        public string GetInfo()
        {
            if (cachedImage != null)
            {
                return cachedImage.GetInfo();
            }
            else if (imageCache.ContainsKey(fileName))
            {
                return imageCache[fileName].GetInfo();
            }
            else
            {
                return $"{fileName} (未加载)";
            }
        }

        public static void PreloadImages(params string[] fileNames)
        {
            Console.WriteLine($"\n[智能代理] 预加载 {fileNames.Length} 张图片");
            foreach (var fileName in fileNames)
            {
                if (!imageCache.ContainsKey(fileName))
                {
                    Console.WriteLine($"  预加载: {fileName}");
                    var image = new RealImage(fileName);
                    
                    if (imageCache.Count >= maxCacheSize)
                    {
                        string oldestFile = lruQueue.Dequeue();
                        imageCache.Remove(oldestFile);
                    }
                    
                    imageCache[fileName] = image;
                    lruQueue.Enqueue(fileName);
                }
            }
            Console.WriteLine($"[智能代理] 预加载完成");
        }

        public static void ShowCacheStatus()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("图片缓存状态");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"缓存容量: {imageCache.Count}/{maxCacheSize}");
            Console.WriteLine("缓存内容:");
            foreach (var item in imageCache)
            {
                Console.WriteLine($"  - {item.Key}: {item.Value.GetInfo()}");
            }
            Console.WriteLine(new string('=', 60));
        }
    }

    // 图片库管理器
    public class ImageGallery
    {
        private List<IImage> images;
        private bool useProxy;

        public ImageGallery(bool useProxy = true)
        {
            this.useProxy = useProxy;
            images = new List<IImage>();
        }

        public void AddImage(string fileName)
        {
            IImage image;
            if (useProxy)
            {
                image = new SmartImageProxy(fileName);
                Console.WriteLine($"[图片库] 添加图片（使用代理）: {fileName}");
            }
            else
            {
                image = new RealImage(fileName);
                Console.WriteLine($"[图片库] 添加图片（直接加载）: {fileName}");
            }
            images.Add(image);
        }

        public void DisplayAll()
        {
            Console.WriteLine($"\n[图片库] 显示所有图片 ({images.Count} 张)");
            Console.WriteLine(new string('-', 60));
            
            foreach (var image in images)
            {
                image.Display();
                Console.WriteLine();
            }
        }

        public void DisplayImage(int index)
        {
            if (index >= 0 && index < images.Count)
            {
                Console.WriteLine($"\n[图片库] 显示第 {index + 1} 张图片");
                images[index].Display();
            }
            else
            {
                Console.WriteLine($"[图片库] 无效的图片索引: {index}");
            }
        }

        public void ShowGalleryInfo()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("图片库信息");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"图片总数: {images.Count}");
            Console.WriteLine($"使用代理: {(useProxy ? "是" : "否")}");
            Console.WriteLine("图片列表:");
            for (int i = 0; i < images.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {images[i].GetInfo()}");
            }
            Console.WriteLine(new string('=', 60));
        }
    }

    // Path辅助类
    public static class Path
    {
        public static string GetExtension(string fileName)
        {
            int lastDot = fileName.LastIndexOf('.');
            return lastDot > 0 ? fileName.Substring(lastDot) : "";
        }

        public static string GetFileNameWithoutExtension(string fileName)
        {
            int lastDot = fileName.LastIndexOf('.');
            return lastDot > 0 ? fileName.Substring(0, lastDot) : fileName;
        }
    }
}
