namespace _17Command
{
    // 命令接口
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    // 文本编辑器（接收者）
    public class TextEditor
    {
        private string _content = "";
        private Stack<string> _history = new Stack<string>();

        public void Write(string text)
        {
            _history.Push(_content);
            _content += text;
            Console.WriteLine($"  写入文本: \"{text}\"");
        }

        public void Delete(int length)
        {
            if (length > _content.Length)
                length = _content.Length;
            
            _history.Push(_content);
            var deleted = _content.Substring(_content.Length - length);
            _content = _content.Substring(0, _content.Length - length);
            Console.WriteLine($"  删除文本: \"{deleted}\"");
        }

        public void Replace(string oldText, string newText)
        {
            _history.Push(_content);
            _content = _content.Replace(oldText, newText);
            Console.WriteLine($"  替换文本: \"{oldText}\" -> \"{newText}\"");
        }

        public void SetContent(string content)
        {
            _content = content;
        }

        public string GetContent()
        {
            return _content;
        }

        public void RestorePrevious()
        {
            if (_history.Count > 0)
            {
                _content = _history.Pop();
            }
        }
    }

    // 写入命令
    public class WriteCommand : ICommand
    {
        private TextEditor _editor;
        private string _text;
        private string? _previousContent;

        public WriteCommand(TextEditor editor, string text)
        {
            _editor = editor;
            _text = text;
        }

        public void Execute()
        {
            _previousContent = _editor.GetContent();
            _editor.Write(_text);
        }

        public void Undo()
        {
            if (_previousContent != null)
            {
                Console.WriteLine($"  撤销写入: \"{_text}\"");
                _editor.SetContent(_previousContent);
            }
        }
    }

    // 删除命令
    public class DeleteCommand : ICommand
    {
        private TextEditor _editor;
        private int _length;
        private string? _previousContent;

        public DeleteCommand(TextEditor editor, int length)
        {
            _editor = editor;
            _length = length;
        }

        public void Execute()
        {
            _previousContent = _editor.GetContent();
            _editor.Delete(_length);
        }

        public void Undo()
        {
            if (_previousContent != null)
            {
                Console.WriteLine($"  撤销删除");
                _editor.SetContent(_previousContent);
            }
        }
    }

    // 替换命令
    public class ReplaceCommand : ICommand
    {
        private TextEditor _editor;
        private string _oldText;
        private string _newText;
        private string? _previousContent;

        public ReplaceCommand(TextEditor editor, string oldText, string newText)
        {
            _editor = editor;
            _oldText = oldText;
            _newText = newText;
        }

        public void Execute()
        {
            _previousContent = _editor.GetContent();
            _editor.Replace(_oldText, _newText);
        }

        public void Undo()
        {
            if (_previousContent != null)
            {
                Console.WriteLine($"  撤销替换: \"{_oldText}\" <- \"{_newText}\"");
                _editor.SetContent(_previousContent);
            }
        }
    }

    // 命令历史管理器
    public class CommandHistory
    {
        private Stack<ICommand> _history = new Stack<ICommand>();
        private Stack<ICommand> _redoStack = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _history.Push(command);
            _redoStack.Clear(); // 执行新命令后清空重做栈
        }

        public void Undo()
        {
            if (_history.Count > 0)
            {
                var command = _history.Pop();
                command.Undo();
                _redoStack.Push(command);
            }
            else
            {
                Console.WriteLine("  没有可撤销的操作");
            }
        }

        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                var command = _redoStack.Pop();
                Console.Write("  重做: ");
                command.Execute();
                _history.Push(command);
            }
            else
            {
                Console.WriteLine("  没有可重做的操作");
            }
        }
    }
}