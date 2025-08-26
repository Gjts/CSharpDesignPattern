namespace _20Memento
{
    // 备忘录
    public class EditorMemento
    {
        private string _content;
        private DateTime _savedTime;

        public EditorMemento(string content)
        {
            _content = content;
            _savedTime = DateTime.Now;
        }

        public string GetContent() => _content;
        public DateTime GetSavedTime() => _savedTime;
    }

    // 发起人
    public class TextEditor
    {
        private string _content = "";

        public void SetContent(string content)
        {
            _content = content;
        }

        public string GetContent() => _content;

        public EditorMemento CreateMemento()
        {
            return new EditorMemento(_content);
        }

        public void RestoreFromMemento(EditorMemento memento)
        {
            if (memento != null)
            {
                _content = memento.GetContent();
            }
        }
    }

    // 管理者
    public class EditorHistory
    {
        private Stack<EditorMemento> _history = new Stack<EditorMemento>();

        public void Save(EditorMemento memento)
        {
            _history.Push(memento);
        }

        public EditorMemento Undo()
        {
            if (_history.Count > 0)
            {
                return _history.Pop();
            }
            return null;
        }
    }

    // 游戏角色
    public class GameCharacter
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Level { get; private set; }

        public GameCharacter(string name, int health, int level)
        {
            Name = name;
            Health = health;
            Level = level;
        }

        public void Fight()
        {
            Health -= 10;
            Level++;
            Console.WriteLine($"{Name} 战斗后: 生命值-10, 等级+1");
        }

        public GameSave Save()
        {
            return new GameSave(Name, Health, Level);
        }

        public void Restore(GameSave save)
        {
            if (save != null)
            {
                Name = save.Name;
                Health = save.Health;
                Level = save.Level;
            }
        }

        public void Display()
        {
            Console.WriteLine($"角色: {Name}, 生命值: {Health}, 等级: {Level}");
        }
    }

    // 游戏存档
    public class GameSave
    {
        public string Name { get; }
        public int Health { get; }
        public int Level { get; }
        public DateTime SaveTime { get; }

        public GameSave(string name, int health, int level)
        {
            Name = name;
            Health = health;
            Level = level;
            SaveTime = DateTime.Now;
        }
    }

    // 存档管理器
    public class SaveGameManager
    {
        private Dictionary<string, GameSave> _saves = new Dictionary<string, GameSave>();

        public void SaveGame(string saveName, GameSave save)
        {
            _saves[saveName] = save;
            Console.WriteLine($"游戏已保存到: {saveName}");
        }

        public GameSave LoadGame(string saveName)
        {
            if (_saves.ContainsKey(saveName))
            {
                Console.WriteLine($"加载存档: {saveName}");
                return _saves[saveName];
            }
            return null;
        }
    }
}

