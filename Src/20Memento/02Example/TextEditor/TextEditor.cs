namespace _20Memento._02Example.TextEditor
{
    // 备忘录类 - 存储编辑器状态
    public class EditorMemento
    {
        private string _content;
        private int _cursorPosition;
        private DateTime _timestamp;
        private string _description;

        public EditorMemento(string content, int cursorPosition, string description)
        {
            _content = content;
            _cursorPosition = cursorPosition;
            _timestamp = DateTime.Now;
            _description = description;
        }

        public string GetContent() => _content;
        public int GetCursorPosition() => _cursorPosition;
        public DateTime GetTimestamp() => _timestamp;
        public string GetDescription() => _description;
    }

    // 原发器类 - 文本编辑器
    public class TextEditor
    {
        private string _content;
        private int _cursorPosition;
        private string _selectedText;

        public TextEditor()
        {
            _content = "";
            _cursorPosition = 0;
            _selectedText = "";
        }

        public void Type(string text)
        {
            Console.WriteLine($"  ⌨️ 输入: \"{text}\"");
            _content = _content.Insert(_cursorPosition, text);
            _cursorPosition += text.Length;
            ShowContent();
        }

        public void Delete(int count)
        {
            if (_cursorPosition >= count)
            {
                Console.WriteLine($"  🔙 删除 {count} 个字符");
                _content = _content.Remove(_cursorPosition - count, count);
                _cursorPosition -= count;
                ShowContent();
            }
        }

        public void MoveCursor(int position)
        {
            if (position >= 0 && position <= _content.Length)
            {
                _cursorPosition = position;
                Console.WriteLine($"  ➡️ 光标移动到位置: {position}");
            }
        }

        public void Select(int start, int end)
        {
            if (start >= 0 && end <= _content.Length && start < end)
            {
                _selectedText = _content.Substring(start, end - start);
                Console.WriteLine($"  🔍 选中文本: \"{_selectedText}\"");
            }
        }

        public void Replace(string oldText, string newText)
        {
            Console.WriteLine($"  🔄 替换: \"{oldText}\" → \"{newText}\"");
            _content = _content.Replace(oldText, newText);
            ShowContent();
        }

        public EditorMemento Save(string description)
        {
            Console.WriteLine($"  💾 保存状态: {description}");
            return new EditorMemento(_content, _cursorPosition, description);
        }

        public void Restore(EditorMemento memento)
        {
            _content = memento.GetContent();
            _cursorPosition = memento.GetCursorPosition();
            Console.WriteLine($"  ⏮️ 恢复到: {memento.GetDescription()} ({memento.GetTimestamp():HH:mm:ss})");
            ShowContent();
        }

        public void ShowContent()
        {
            Console.WriteLine($"  📝 内容: \"{_content}\"");
            Console.WriteLine($"  📍 光标位置: {_cursorPosition}");
            
            // 显示光标位置
            if (_content.Length > 0)
            {
                string visual = new string(' ', 9) + _content.Insert(_cursorPosition, "|");
                Console.WriteLine($"{visual}");
            }
        }

        public string GetContent() => _content;
    }

    // 管理者类 - 历史记录管理器
    public class EditorHistory
    {
        private Stack<EditorMemento> _undoStack;
        private Stack<EditorMemento> _redoStack;
        private List<EditorMemento> _bookmarks;
        private int _maxHistorySize;

        public EditorHistory(int maxHistorySize = 50)
        {
            _undoStack = new Stack<EditorMemento>();
            _redoStack = new Stack<EditorMemento>();
            _bookmarks = new List<EditorMemento>();
            _maxHistorySize = maxHistorySize;
        }

        public void Push(EditorMemento memento)
        {
            _undoStack.Push(memento);
            _redoStack.Clear(); // 新操作后清空重做栈
            
            // 限制历史大小
            if (_undoStack.Count > _maxHistorySize)
            {
                var items = _undoStack.ToArray();
                _undoStack.Clear();
                for (int i = 0; i < _maxHistorySize; i++)
                {
                    _undoStack.Push(items[i]);
                }
            }
        }

        public EditorMemento? Undo(EditorMemento currentState)
        {
            if (_undoStack.Count > 0)
            {
                _redoStack.Push(currentState);
                return _undoStack.Pop();
            }
            return null;
        }

        public EditorMemento? Redo()
        {
            if (_redoStack.Count > 0)
            {
                var memento = _redoStack.Pop();
                _undoStack.Push(memento);
                return memento;
            }
            return null;
        }

        public void AddBookmark(EditorMemento memento)
        {
            _bookmarks.Add(memento);
            Console.WriteLine($"  🔖 添加书签: {memento.GetDescription()}");
        }

        public EditorMemento? GetBookmark(int index)
        {
            if (index >= 0 && index < _bookmarks.Count)
            {
                return _bookmarks[index];
            }
            return null;
        }

        public void ShowHistory()
        {
            Console.WriteLine("\n📚 编辑历史:");
            
            if (_undoStack.Count == 0)
            {
                Console.WriteLine("  (无历史记录)");
            }
            else
            {
                var history = _undoStack.ToArray();
                for (int i = 0; i < Math.Min(5, history.Length); i++)
                {
                    var m = history[i];
                    Console.WriteLine($"  {i + 1}. {m.GetDescription()} - {m.GetTimestamp():HH:mm:ss}");
                }
                if (history.Length > 5)
                {
                    Console.WriteLine($"  ... 还有 {history.Length - 5} 条记录");
                }
            }
            
            Console.WriteLine($"\n  可撤销: {_undoStack.Count} 步");
            Console.WriteLine($"  可重做: {_redoStack.Count} 步");
            Console.WriteLine($"  书签数: {_bookmarks.Count}");
        }
    }

    // 游戏存档示例
    public class GameCharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public List<string> Inventory { get; set; }
        public string Location { get; set; }

        public GameCharacter(string name)
        {
            Name = name;
            Level = 1;
            Health = 100;
            Mana = 50;
            Inventory = new List<string>();
            Location = "新手村";
        }

        public void LevelUp()
        {
            Level++;
            Health += 20;
            Mana += 10;
            Console.WriteLine($"  🎉 {Name} 升级到 Lv.{Level}!");
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Console.WriteLine($"  💔 {Name} 受到 {damage} 点伤害 (HP: {Health}/100)");
        }

        public void AddItem(string item)
        {
            Inventory.Add(item);
            Console.WriteLine($"  🎁 获得物品: {item}");
        }

        public void Travel(string location)
        {
            Location = location;
            Console.WriteLine($"  🗺️ 移动到: {location}");
        }

        public GameSave CreateSave(string saveName)
        {
            Console.WriteLine($"  💾 创建存档: {saveName}");
            return new GameSave(
                saveName,
                Name,
                Level,
                Health,
                Mana,
                new List<string>(Inventory),
                Location
            );
        }

        public void LoadSave(GameSave save)
        {
            Name = save.Name;
            Level = save.Level;
            Health = save.Health;
            Mana = save.Mana;
            Inventory = new List<string>(save.Inventory);
            Location = save.Location;
            
            Console.WriteLine($"  📂 加载存档: {save.SaveName}");
            ShowStatus();
        }

        public void ShowStatus()
        {
            Console.WriteLine($"\n🎮 角色状态:");
            Console.WriteLine($"  名称: {Name}");
            Console.WriteLine($"  等级: Lv.{Level}");
            Console.WriteLine($"  生命: {Health}/100");
            Console.WriteLine($"  魔力: {Mana}/50");
            Console.WriteLine($"  位置: {Location}");
            Console.WriteLine($"  背包: {string.Join(", ", Inventory)}");
        }
    }

    // 游戏存档备忘录
    public class GameSave
    {
        public string SaveName { get; }
        public DateTime SaveTime { get; }
        public string Name { get; }
        public int Level { get; }
        public int Health { get; }
        public int Mana { get; }
        public List<string> Inventory { get; }
        public string Location { get; }

        public GameSave(string saveName, string name, int level, int health, int mana, List<string> inventory, string location)
        {
            SaveName = saveName;
            SaveTime = DateTime.Now;
            Name = name;
            Level = level;
            Health = health;
            Mana = mana;
            Inventory = inventory;
            Location = location;
        }
    }

    // 存档管理器
    public class SaveManager
    {
        private Dictionary<string, GameSave> _saves;
        private Stack<GameSave> _quickSaves;

        public SaveManager()
        {
            _saves = new Dictionary<string, GameSave>();
            _quickSaves = new Stack<GameSave>();
        }

        public void SaveGame(string slot, GameSave save)
        {
            _saves[slot] = save;
            Console.WriteLine($"  ✅ 游戏已保存到槽位: {slot}");
        }

        public GameSave? LoadGame(string slot)
        {
            if (_saves.ContainsKey(slot))
            {
                Console.WriteLine($"  ✅ 从槽位 {slot} 加载游戏");
                return _saves[slot];
            }
            Console.WriteLine($"  ❌ 槽位 {slot} 没有存档");
            return null;
        }

        public void QuickSave(GameSave save)
        {
            _quickSaves.Push(save);
            Console.WriteLine($"  ⚡ 快速保存 (共 {_quickSaves.Count} 个快速存档)");
        }

        public GameSave? QuickLoad()
        {
            if (_quickSaves.Count > 0)
            {
                Console.WriteLine($"  ⚡ 快速加载");
                return _quickSaves.Pop();
            }
            Console.WriteLine($"  ❌ 没有快速存档");
            return null;
        }

        public void ShowSaves()
        {
            Console.WriteLine("\n💾 存档列表:");
            if (_saves.Count == 0)
            {
                Console.WriteLine("  (无存档)");
            }
            else
            {
                foreach (var kvp in _saves)
                {
                    var save = kvp.Value;
                    Console.WriteLine($"  [{kvp.Key}] {save.SaveName} - Lv.{save.Level} @ {save.Location} - {save.SaveTime:yyyy-MM-dd HH:mm}");
                }
            }
            Console.WriteLine($"  快速存档数: {_quickSaves.Count}");
        }
    }
}
