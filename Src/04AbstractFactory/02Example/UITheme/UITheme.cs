namespace _AbstractFactory._02Example.UITheme
{
    // 抽象产品 - 按钮
    public interface IButton
    {
        void Render();
        void Click();
    }

    // 抽象产品 - 文本框
    public interface ITextBox
    {
        void Render();
        void SetText(string text);
    }

    // 抽象产品 - 复选框
    public interface ICheckBox
    {
        void Render();
        void Check();
    }

    // 抽象工厂
    public interface IUIFactory
    {
        IButton CreateButton();
        ITextBox CreateTextBox();
        ICheckBox CreateCheckBox();
    }

    // 具体产品 - Windows风格按钮
    public class WindowsButton : IButton
    {
        public void Render()
        {
            Console.WriteLine("  渲染 Windows 风格按钮");
        }

        public void Click()
        {
            Console.WriteLine("  Windows 按钮被点击");
        }
    }

    // 具体产品 - Windows风格文本框
    public class WindowsTextBox : ITextBox
    {
        public void Render()
        {
            Console.WriteLine("  渲染 Windows 风格文本框");
        }

        public void SetText(string text)
        {
            Console.WriteLine($"  Windows 文本框输入: {text}");
        }
    }

    // 具体产品 - Windows风格复选框
    public class WindowsCheckBox : ICheckBox
    {
        public void Render()
        {
            Console.WriteLine("  渲染 Windows 风格复选框");
        }

        public void Check()
        {
            Console.WriteLine("  Windows 复选框被选中");
        }
    }

    // 具体工厂 - Windows UI工厂
    public class WindowsUIFactory : IUIFactory
    {
        public IButton CreateButton()
        {
            return new WindowsButton();
        }

        public ITextBox CreateTextBox()
        {
            return new WindowsTextBox();
        }

        public ICheckBox CreateCheckBox()
        {
            return new WindowsCheckBox();
        }
    }

    // 具体产品 - Mac风格按钮
    public class MacButton : IButton
    {
        public void Render()
        {
            Console.WriteLine("  渲染 Mac 风格按钮");
        }

        public void Click()
        {
            Console.WriteLine("  Mac 按钮被点击");
        }
    }

    // 具体产品 - Mac风格文本框
    public class MacTextBox : ITextBox
    {
        public void Render()
        {
            Console.WriteLine("  渲染 Mac 风格文本框");
        }

        public void SetText(string text)
        {
            Console.WriteLine($"  Mac 文本框输入: {text}");
        }
    }

    // 具体产品 - Mac风格复选框
    public class MacCheckBox : ICheckBox
    {
        public void Render()
        {
            Console.WriteLine("  渲染 Mac 风格复选框");
        }

        public void Check()
        {
            Console.WriteLine("  Mac 复选框被选中");
        }
    }

    // 具体工厂 - Mac UI工厂
    public class MacUIFactory : IUIFactory
    {
        public IButton CreateButton()
        {
            return new MacButton();
        }

        public ITextBox CreateTextBox()
        {
            return new MacTextBox();
        }

        public ICheckBox CreateCheckBox()
        {
            return new MacCheckBox();
        }
    }

    // 应用程序类
    public class Application
    {
        private readonly IButton _button;
        private readonly ITextBox _textBox;
        private readonly ICheckBox _checkBox;

        public Application(IUIFactory factory)
        {
            _button = factory.CreateButton();
            _textBox = factory.CreateTextBox();
            _checkBox = factory.CreateCheckBox();
        }

        public void Run()
        {
            _button.Render();
            _textBox.Render();
            _checkBox.Render();
            
            _button.Click();
            _textBox.SetText("Hello Abstract Factory!");
            _checkBox.Check();
        }
    }
}