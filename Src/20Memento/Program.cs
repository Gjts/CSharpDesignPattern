namespace _20Memento
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 备忘录模式 (Memento Pattern) ===\n");

            Console.WriteLine("示例1：文本编辑器");
            Console.WriteLine("------------------------");
            var editor = new TextEditor();
            var history = new EditorHistory();
            
            editor.SetContent("版本1");
            history.Save(editor.CreateMemento());
            Console.WriteLine($"当前内容: {editor.GetContent()}");
            
            editor.SetContent("版本2");
            history.Save(editor.CreateMemento());
            Console.WriteLine($"当前内容: {editor.GetContent()}");
            
            editor.SetContent("版本3");
            Console.WriteLine($"当前内容: {editor.GetContent()}");
            
            editor.RestoreFromMemento(history.Undo());
            Console.WriteLine($"撤销后: {editor.GetContent()}");
            
            editor.RestoreFromMemento(history.Undo());
            Console.WriteLine($"再次撤销: {editor.GetContent()}");

            Console.WriteLine("\n示例2：游戏存档");
            Console.WriteLine("------------------------");
            var game = new GameCharacter("勇者", 100, 1);
            var saveManager = new SaveGameManager();
            
            saveManager.SaveGame("存档1", game.Save());
            game.Display();
            
            game.Fight();
            game.Fight();
            saveManager.SaveGame("存档2", game.Save());
            game.Display();
            
            game.Fight();
            game.Fight();
            game.Display();
            
            Console.WriteLine("\n读取存档2:");
            game.Restore(saveManager.LoadGame("存档2"));
            game.Display();
        }
    }
}
