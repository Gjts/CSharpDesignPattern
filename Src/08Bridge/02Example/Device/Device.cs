namespace _Bridge._02Example.Device
{
    // 实现部分接口
    public interface IDevice
    {
        void PowerOn();
        void PowerOff();
        void SetVolume(int volume);
        int GetVolume();
        bool IsEnabled();
    }

    // 具体实现 - 电视
    public class TV : IDevice
    {
        private bool _isOn = false;
        private int _volume = 30;

        public void PowerOn()
        {
            _isOn = true;
            Console.WriteLine("  电视已开启");
        }

        public void PowerOff()
        {
            _isOn = false;
            Console.WriteLine("  电视已关闭");
        }

        public void SetVolume(int volume)
        {
            _volume = Math.Max(0, Math.Min(100, volume));
            Console.WriteLine($"  电视音量设置为: {_volume}");
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

    // 具体实现 - 收音机
    public class Radio : IDevice
    {
        private bool _isOn = false;
        private int _volume = 20;

        public void PowerOn()
        {
            _isOn = true;
            Console.WriteLine("  收音机已开启");
        }

        public void PowerOff()
        {
            _isOn = false;
            Console.WriteLine("  收音机已关闭");
        }

        public void SetVolume(int volume)
        {
            _volume = Math.Max(0, Math.Min(100, volume));
            Console.WriteLine($"  收音机音量设置为: {_volume}");
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

    // 抽象部分
    public abstract class RemoteControl
    {
        protected IDevice device;

        protected RemoteControl(IDevice device)
        {
            this.device = device;
        }

        public virtual void TogglePower()
        {
            if (device.IsEnabled())
            {
                device.PowerOff();
            }
            else
            {
                device.PowerOn();
            }
        }

        public virtual void VolumeUp()
        {
            device.SetVolume(device.GetVolume() + 10);
        }

        public virtual void VolumeDown()
        {
            device.SetVolume(device.GetVolume() - 10);
        }
    }

    // 扩展抽象 - 高级遥控器
    public class AdvancedRemoteControl : RemoteControl
    {
        public AdvancedRemoteControl(IDevice device) : base(device) { }

        public void Mute()
        {
            Console.WriteLine("  静音");
            device.SetVolume(0);
        }

        public void SetChannel(int channel)
        {
            Console.WriteLine($"  切换到频道 {channel}");
        }
    }
}