namespace _17Command._02Example.SmartHome
{
    // 命令接口
    public interface ICommand
    {
        void Execute();
        void Undo();
        string GetDescription();
    }

    // 接收者 - 智能灯
    public class SmartLight
    {
        private string _location;
        private int _brightness;
        private bool _isOn;

        public SmartLight(string location)
        {
            _location = location;
            _brightness = 0;
            _isOn = false;
        }

        public void TurnOn()
        {
            _isOn = true;
            _brightness = 100;
            Console.WriteLine($"  💡 {_location}的灯已打开 (亮度: {_brightness}%)");
        }

        public void TurnOff()
        {
            _isOn = false;
            _brightness = 0;
            Console.WriteLine($"  💡 {_location}的灯已关闭");
        }

        public void SetBrightness(int brightness)
        {
            _brightness = brightness;
            _isOn = brightness > 0;
            Console.WriteLine($"  💡 {_location}的灯亮度设为: {_brightness}%");
        }

        public int GetBrightness() => _brightness;
        public bool IsOn() => _isOn;
    }

    // 接收者 - 空调
    public class AirConditioner
    {
        private string _location;
        private int _temperature;
        private bool _isOn;
        private string _mode;

        public AirConditioner(string location)
        {
            _location = location;
            _temperature = 24;
            _isOn = false;
            _mode = "制冷";
        }

        public void TurnOn()
        {
            _isOn = true;
            Console.WriteLine($"  🌡️ {_location}的空调已打开 (温度: {_temperature}°C, 模式: {_mode})");
        }

        public void TurnOff()
        {
            _isOn = false;
            Console.WriteLine($"  🌡️ {_location}的空调已关闭");
        }

        public void SetTemperature(int temperature)
        {
            _temperature = temperature;
            Console.WriteLine($"  🌡️ {_location}的空调温度设为: {_temperature}°C");
        }

        public void SetMode(string mode)
        {
            _mode = mode;
            Console.WriteLine($"  🌡️ {_location}的空调模式设为: {_mode}");
        }

        public int GetTemperature() => _temperature;
        public string GetMode() => _mode;
        public bool IsOn() => _isOn;
    }

    // 接收者 - 音响系统
    public class SoundSystem
    {
        private int _volume;
        private string _source;

        public SoundSystem()
        {
            _volume = 0;
            _source = "蓝牙";
        }

        public void TurnOn()
        {
            _volume = 30;
            Console.WriteLine($"  🔊 音响系统已打开 (音量: {_volume}, 音源: {_source})");
        }

        public void TurnOff()
        {
            _volume = 0;
            Console.WriteLine($"  🔊 音响系统已关闭");
        }

        public void SetVolume(int volume)
        {
            _volume = volume;
            Console.WriteLine($"  🔊 音量设为: {_volume}");
        }

        public void SetSource(string source)
        {
            _source = source;
            Console.WriteLine($"  🔊 音源切换到: {_source}");
        }

        public int GetVolume() => _volume;
        public string GetSource() => _source;
    }

    // 具体命令 - 开灯命令
    public class LightOnCommand : ICommand
    {
        private SmartLight _light;

        public LightOnCommand(SmartLight light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.TurnOn();
        }

        public void Undo()
        {
            _light.TurnOff();
        }

        public string GetDescription() => "开灯";
    }

    // 具体命令 - 关灯命令
    public class LightOffCommand : ICommand
    {
        private SmartLight _light;

        public LightOffCommand(SmartLight light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.TurnOff();
        }

        public void Undo()
        {
            _light.TurnOn();
        }

        public string GetDescription() => "关灯";
    }

    // 具体命令 - 调光命令
    public class LightDimCommand : ICommand
    {
        private SmartLight _light;
        private int _newBrightness;
        private int _previousBrightness;

        public LightDimCommand(SmartLight light, int brightness)
        {
            _light = light;
            _newBrightness = brightness;
        }

        public void Execute()
        {
            _previousBrightness = _light.GetBrightness();
            _light.SetBrightness(_newBrightness);
        }

        public void Undo()
        {
            _light.SetBrightness(_previousBrightness);
        }

        public string GetDescription() => $"调光到{_newBrightness}%";
    }

    // 具体命令 - 空调控制命令
    public class ACControlCommand : ICommand
    {
        private AirConditioner _ac;
        private bool _turnOn;
        private int _temperature;
        private bool _wasOn;
        private int _previousTemp;

        public ACControlCommand(AirConditioner ac, bool turnOn, int temperature = 24)
        {
            _ac = ac;
            _turnOn = turnOn;
            _temperature = temperature;
        }

        public void Execute()
        {
            _wasOn = _ac.IsOn();
            _previousTemp = _ac.GetTemperature();

            if (_turnOn)
            {
                _ac.TurnOn();
                _ac.SetTemperature(_temperature);
            }
            else
            {
                _ac.TurnOff();
            }
        }

        public void Undo()
        {
            if (_wasOn)
            {
                _ac.TurnOn();
                _ac.SetTemperature(_previousTemp);
            }
            else
            {
                _ac.TurnOff();
            }
        }

        public string GetDescription() => _turnOn ? $"开空调({_temperature}°C)" : "关空调";
    }

    // 宏命令 - 场景命令
    public class SceneCommand : ICommand
    {
        private List<ICommand> _commands;
        private string _sceneName;

        public SceneCommand(string sceneName)
        {
            _sceneName = sceneName;
            _commands = new List<ICommand>();
        }

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public void Execute()
        {
            Console.WriteLine($"\n🎬 执行场景: {_sceneName}");
            foreach (var command in _commands)
            {
                command.Execute();
            }
        }

        public void Undo()
        {
            Console.WriteLine($"\n🎬 撤销场景: {_sceneName}");
            // 逆序撤销
            for (int i = _commands.Count - 1; i >= 0; i--)
            {
                _commands[i].Undo();
            }
        }

        public string GetDescription() => $"场景: {_sceneName}";
    }

    // 调用者 - 智能遥控器
    public class SmartRemoteControl
    {
        private Dictionary<int, ICommand> _onCommands;
        private Dictionary<int, ICommand> _offCommands;
        private Stack<ICommand> _history;
        private ICommand _lastCommand;

        public SmartRemoteControl()
        {
            _onCommands = new Dictionary<int, ICommand>();
            _offCommands = new Dictionary<int, ICommand>();
            _history = new Stack<ICommand>();
            _lastCommand = new NoCommand();

            // 初始化为空命令
            for (int i = 0; i < 7; i++)
            {
                _onCommands[i] = new NoCommand();
                _offCommands[i] = new NoCommand();
            }
        }

        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
        {
            _onCommands[slot] = onCommand;
            _offCommands[slot] = offCommand;
            Console.WriteLine($"  设置按钮{slot}: ON={onCommand.GetDescription()}, OFF={offCommand.GetDescription()}");
        }

        public void OnButtonPressed(int slot)
        {
            if (_onCommands.ContainsKey(slot))
            {
                Console.WriteLine($"\n按下ON按钮{slot}:");
                _onCommands[slot].Execute();
                _lastCommand = _onCommands[slot];
                _history.Push(_lastCommand);
            }
        }

        public void OffButtonPressed(int slot)
        {
            if (_offCommands.ContainsKey(slot))
            {
                Console.WriteLine($"\n按下OFF按钮{slot}:");
                _offCommands[slot].Execute();
                _lastCommand = _offCommands[slot];
                _history.Push(_lastCommand);
            }
        }

        public void UndoButtonPressed()
        {
            Console.WriteLine("\n按下撤销按钮:");
            if (_history.Count > 0)
            {
                var command = _history.Pop();
                command.Undo();
            }
            else
            {
                Console.WriteLine("  没有可撤销的操作");
            }
        }

        public void ShowHistory()
        {
            Console.WriteLine("\n📜 命令历史:");
            if (_history.Count == 0)
            {
                Console.WriteLine("  (空)");
            }
            else
            {
                var commands = _history.ToArray();
                for (int i = commands.Length - 1; i >= 0; i--)
                {
                    Console.WriteLine($"  {commands.Length - i}. {commands[i].GetDescription()}");
                }
            }
        }
    }

    // 空命令
    public class NoCommand : ICommand
    {
        public void Execute() { }
        public void Undo() { }
        public string GetDescription() => "无操作";
    }
}
