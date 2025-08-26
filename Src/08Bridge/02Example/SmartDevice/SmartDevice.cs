namespace _Bridge._02Example.Device
{
    // 具体实现 - 电视机
    public class Television : IDevice
    {
        private bool _isOn = false;
        private int _volume = 30;

        public void PowerOn()
        {
            _isOn = true;
            Console.WriteLine("   电视已开启");
        }

        public void PowerOff()
        {
            _isOn = false;
            Console.WriteLine("   电视已关闭");
        }

        public void SetVolume(int volume)
        {
            _volume = Math.Max(0, Math.Min(100, volume));
            Console.WriteLine($"   电视音量设置为: {_volume}");
        }

        public int GetVolume()
        {
            return _volume;
        }

        public bool IsEnabled()
        {
            return _isOn;
        }
    }

    // 具体实现 - 智能音箱
    public class SmartSpeaker : IDevice
    {
        private bool _isOn = false;
        private int _volume = 50;

        public void PowerOn()
        {
            _isOn = true;
            Console.WriteLine("   智能音箱已开启");
        }

        public void PowerOff()
        {
            _isOn = false;
            Console.WriteLine("   智能音箱已关闭");
        }

        public void SetVolume(int volume)
        {
            _volume = Math.Max(0, Math.Min(100, volume));
            Console.WriteLine($"   智能音箱音量设置为: {_volume}");
        }

        public int GetVolume()
        {
            return _volume;
        }

        public bool IsEnabled()
        {
            return _isOn;
        }
    }

    // 具体实现 - 智能灯
    public class SmartLight : IDevice
    {
        private bool _isOn = false;
        private int _brightness = 100;

        public void PowerOn()
        {
            _isOn = true;
            Console.WriteLine("   智能灯已开启");
        }

        public void PowerOff()
        {
            _isOn = false;
            Console.WriteLine("   智能灯已关闭");
        }

        public void SetVolume(int volume)
        {
            _brightness = Math.Max(0, Math.Min(100, volume));
            Console.WriteLine($"   智能灯亮度设置为: {_brightness}%");
        }

        public int GetVolume()
        {
            return _brightness;
        }

        public bool IsEnabled()
        {
            return _isOn;
        }
    }

    // 基础遥控器
    public class BasicRemote : RemoteControl
    {
        public BasicRemote(IDevice device) : base(device) { }

        public void Power()
        {
            Console.WriteLine("   基础遥控器: 电源按钮");
            TogglePower();
        }

        public IDevice Device
        {
            get { return device; }
            set { device = value; }
        }
    }

    // 高级遥控器
    public class AdvancedRemote : RemoteControl
    {
        public AdvancedRemote(IDevice device) : base(device) { }

        public void Power()
        {
            Console.WriteLine("   高级遥控器: 电源按钮");
            TogglePower();
        }

        public void Mute()
        {
            Console.WriteLine("   高级遥控器: 静音");
            device.SetVolume(0);
        }

        public IDevice Device
        {
            get { return device; }
            set { device = value; }
        }
    }
}