using _Flyweight._02Example.TextEditor;
using _Flyweight._02Example.Game;

namespace _12Flyweight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 享元模式 (Flyweight Pattern) ================================");
            Console.WriteLine("适用场景：系统中存在大量相似对象；需要缓冲池的场景");
            Console.WriteLine("特点：运用共享技术有效地支持大量细粒度的对象");
            Console.WriteLine("优点：减少内存使用；提高性能；外部状态相对独立\n");

            Console.WriteLine("-------------------------------- 文本编辑器系统 ----------------------------------");
            
            var editor = new TextEditor();
            
            // 输入文本
            string text = "Hello World! Hello Flyweight Pattern!";
            Console.WriteLine($"输入文本: {text}");
            
            // 显示字符和内存使用
            editor.TypeText(text);
            editor.Display();
            
            // 显示享元池状态
            CharacterFactory.ShowPoolStatus();

            Console.WriteLine("\n-------------------------------- 游戏场景系统 ----------------------------------");
            
            var forest = new Forest();
            
            // 种植树木
            Console.WriteLine("种植树木：");
            
            // 种植橡树
            for (int i = 0; i < 5; i++)
            {
                forest.PlantTree(i * 10, i * 5, "橡树", "绿色", "粗糙");
            }
            
            // 种植松树
            for (int i = 0; i < 5; i++)
            {
                forest.PlantTree(i * 15 + 50, i * 8, "松树", "深绿", "针状");
            }
            
            // 种植枫树
            for (int i = 0; i < 5; i++)
            {
                forest.PlantTree(i * 12 + 100, i * 6, "枫树", "红色", "光滑");
            }
            
            // 显示森林
            forest.Display();
            
            // 显示内存使用情况
            TreeTypeFactory.ShowMemoryUsage();
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 内部状态（字符样式、树木类型）被共享");
            Console.WriteLine("- 外部状态（位置坐标）由客户端维护");
            Console.WriteLine("- 通过共享大幅减少内存使用");
        }
    }
}
