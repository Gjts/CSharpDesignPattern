namespace _Flyweight._02Example.Game
{
    // 享元接口
    public interface ITree
    {
        void Display(int x, int y, int age);
    }

    // 具体享元 - 树类型
    public class TreeType : ITree
    {
        private string name;
        private string color;
        private string texture;

        public TreeType(string name, string color, string texture)
        {
            this.name = name;
            this.color = color;
            this.texture = texture;
            Console.WriteLine($"  创建树类型: {name} ({color}, {texture})");
        }

        public void Display(int x, int y, int age)
        {
            Console.WriteLine($"  绘制 {name} 树 [位置: ({x},{y}), 年龄: {age}年, 颜色: {color}, 纹理: {texture}]");
        }
    }

    // 享元工厂
    public class TreeFactory
    {
        private static Dictionary<string, TreeType> treeTypes = new Dictionary<string, TreeType>();

        public static TreeType GetTreeType(string name, string color, string texture)
        {
            string key = $"{name}_{color}_{texture}";
            
            if (!treeTypes.ContainsKey(key))
            {
                treeTypes[key] = new TreeType(name, color, texture);
            }
            
            return treeTypes[key];
        }

        public static int GetTreeTypeCount()
        {
            return treeTypes.Count;
        }
    }

    // TreeTypeFactory别名类（为了兼容Program.cs）
    public class TreeTypeFactory
    {
        public static void ShowMemoryUsage()
        {
            int treeTypeCount = TreeFactory.GetTreeTypeCount();
            Console.WriteLine($"\n内存使用统计:");
            Console.WriteLine($"  树类型对象数: {treeTypeCount}");
            Console.WriteLine($"  如果不使用享元模式，每棵树都需要存储完整信息");
            Console.WriteLine($"  使用享元模式后，只需要 {treeTypeCount} 个类型对象 + 位置信息");
        }
    }

    // 树实例（包含外部状态）
    public class Tree
    {
        private int x;
        private int y;
        private int age;
        private TreeType type;

        public Tree(int x, int y, int age, TreeType type)
        {
            this.x = x;
            this.y = y;
            this.age = age;
            this.type = type;
        }

        public void Display()
        {
            type.Display(x, y, age);
        }
    }

    // 森林类
    public class Forest
    {
        private List<Tree> trees = new List<Tree>();

        public void PlantTree(int x, int y, int age, string name, string color, string texture)
        {
            TreeType type = TreeFactory.GetTreeType(name, color, texture);
            Tree tree = new Tree(x, y, age, type);
            trees.Add(tree);
        }

        // 重载方法以支持Program.cs的调用方式
        public void PlantTree(int x, int y, string name, string color, string texture)
        {
            PlantTree(x, y, 1, name, color, texture); // 默认年龄为1
        }

        public void Display()
        {
            Console.WriteLine("\n森林中的树木:");
            foreach (var tree in trees)
            {
                tree.Display();
            }
            Console.WriteLine($"\n统计: 森林中共有 {trees.Count} 棵树，使用了 {TreeFactory.GetTreeTypeCount()} 种树类型");
        }
    }
}