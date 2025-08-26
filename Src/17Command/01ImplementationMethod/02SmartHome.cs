namespace _17Command
{
    // 智能设备接口
    public interface ISmartDevice
    {
        void TurnOn();
        void TurnOff();
        string GetStatus();
    }

    // 电灯
    public class Light : ISmartDevice
    {
        private string _location;
        private bool _isOn;

        public Light(string location)
        {
            _location = location;
        }

        public void TurnOn()
        {
            _isOn = true;
            Console.WriteLine($"  {_location} 已打开");
        }

        public void TurnOff()
        {
            _isOn = false;
            Console.WriteLine($"  {_location} 已关闭");
        }

        public string GetStatus()
        {
            return $"{_location}: {(_isOn ? "开" : "关")}";
        }
    }

    // 空调
    public class AirConditioner : ISmartDevice
    {
        private string _location;
        private bool _isOn;
        private int _temperature;

        public AirConditioner(string location)
        {
            _location = location;
            _temperature = 26;
        }

        public void TurnOn()
        {
            _isOn = true;
            Console.WriteLine($"  {_location} 已打开");
        }

        public void TurnOff()
        {
            _isOn = false;
            Console.WriteLine($"  {_location} 已关闭");
        }

        public void SetTemperature(int temperature)
        {
            _temperature = temperature;
            if (_isOn)
            {
                Console.WriteLine($"  {_location} 温度设置为 {_temperature}°C");
            }
        }

        public string GetStatus()
        {
            return $"{_location}: {(_isOn ? $"开 ({_temperature}°C)" : "关")}";
        }
    }

    // 电视
    public class Television : ISmartDevice
    {
        private string _location;
        private bool _isOn;
        private int _channel;
        private int _volume;

        public Television(string location)
        {
            _location = location;
            _channel = 1;
            _volume = 20;
        }

        public void TurnOn()
        {
            _isOn = true;
            Console.WriteLine($"  {_location} 已打开");
        }

        public void TurnOff()
        {
            _isOn = false;
            Console.WriteLine($"  {_location} 已关闭");
        }

        public void SetChannel(int channel)
        {
            _channel = channel;
            if (_isOn)
            {
                Console.WriteLine($"  {_location} 切换到频道 {_channel}");
            }
        }

        public void SetVolume(int volume)
        {
            _volume = volume;
            if (_isOn)
            {
                Console.WriteLine($"  {_location} 音量设置为 {_volume}");
            }
        }

        public string GetStatus()
        {
            return $"{_location}: {(_isOn ? $"开 (频道{_channel}, 音量{_volume})" : "关")}";
        }
    }

    // 具体命令：开灯
    public class LightOnCommand : ICommand
    {
        private Light _light;

        public LightOnCommand(Light light)
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
    }

    // 具体命令：关灯
    public class LightOffCommand : ICommand
    {
        private Light _light;

        public LightOffCommand(Light light)
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
    }

    // 具体命令：开空调
    public class AirConditionerOnCommand : ICommand
    {
        private AirConditioner _ac;
        private int _temperature;

        public AirConditionerOnCommand(AirConditioner ac, int temperature)
        {
            _ac = ac;
            _temperature = temperature;
        }

        public void Execute()
        {
            _ac.TurnOn();
            _ac.SetTemperature(_temperature);
        }

        public void Undo()
        {
            _ac.TurnOff();
        }
    }

    // 具体命令：关空调
    public class AirConditionerOffCommand : ICommand
    {
        private AirConditioner _ac;

        public AirConditionerOffCommand(AirConditioner ac)
        {
            _ac = ac;
        }

        public void Execute()
        {
            _ac.TurnOff();
        }

        public void Undo()
        {
            _ac.TurnOn();
        }
    }

    // 具体命令：开电视
    public class TVOnCommand : ICommand
    {
        private Television _tv;
        private int _channel;

        public TVOnCommand(Television tv, int channel)
        {
            _tv = tv;
            _channel = channel;
        }

        public void Execute()
        {
            _tv.TurnOn();
            _tv.SetChannel(_channel);
            _tv.SetVolume(20);
        }

        public void Undo()
        {
            _tv.TurnOff();
        }
    }

    // 具体命令：关电视
    public class TVOffCommand : ICommand
    {
        private Television _tv;

        public TVOffCommand(Television tv)
        {
            _tv = tv;
        }

        public void Execute()
        {
            _tv.TurnOff();
        }

        public void Undo()
        {
            _tv.TurnOn();
        }
    }

    // 空命令（什么都不做）
    public class NoCommand : ICommand
    {
        public void Execute() { }
        public void Undo() { }
    }

    // 宏命令（组合多个命令）
    public class MacroCommand : ICommand
    {
        private ICommand[] _commands;

        public MacroCommand(ICommand[] commands)
        {
            _commands = commands;
        }

        public void Execute()
        {
            foreach (var command in _commands)
            {
                command.Execute();
            }
        }

        public void Undo()
        {
            // 反向撤销
            for (int i = _commands.Length - 1; i >= 0; i--)
            {
                _commands[i].Undo();
            }
        }
    }

    // 遥控器（调用者）
    public class RemoteControl
    {
        private ICommand[] _onCommands;
        private ICommand[] _offCommands;
        private ICommand _lastCommand;

        public RemoteControl()
        {
            _onCommands = new ICommand[7];
            _offCommands = new ICommand[7];
            var noCommand = new NoCommand();
            
            for (int i = 0; i < 7; i++)
            {
                _onCommands[i] = noCommand;
                _offCommands[i] = noCommand;
            }
            
            _lastCommand = noCommand;
        }

        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
        {
            _onCommands[slot] = onCommand;
            _offCommands[slot] = offCommand;
        }

        public void OnButtonPressed(int slot)
        {
            _onCommands[slot].Execute();
            _lastCommand = _onCommands[slot];
        }

        public void OffButtonPressed(int slot)
        {
            _offCommands[slot].Execute();
            _lastCommand = _offCommands[slot];
        }

        public void UndoButtonPressed()
        {
            _lastCommand.Undo();
        }
    }
}