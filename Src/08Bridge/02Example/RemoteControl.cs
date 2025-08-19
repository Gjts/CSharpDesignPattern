namespace _08Bridge._02Example
{
    // 实现接口：设备
    public interface IDevice
    {
        void PowerOn();
        void PowerOff();
        void SetChannel(int channel);
        int GetChannel();
        void SetVolume(int volume);
        int GetVolume();
    }

    // 具体设备：电视
    public class TV : IDevice
    {
        private bool powerOn = false;
        private int channel = 1;
        private int volume = 50;

        public void PowerOn()
        {
            powerOn = true;
            Console.WriteLine("电视已开机");
        }

        public void PowerOff()
        {
            powerOn = false;
            Console.WriteLine("电视已关机");
        }

        public void SetChannel(int channel)
        {
            this.channel = channel;
            Console.WriteLine($"电视切换到频道 {channel}");
        }

        public int GetChannel()
        {
            return channel;
        }

        public void SetVolume(int volume)
        {
            this.volume = volume;
            Console.WriteLine($"电视音量设置为 {volume}");
        }

        public int GetVolume()
        {
            return volume;
        }
    }

    // 具体设备：收音机
    public class Radio : IDevice
    {
        private bool powerOn = false;
        private int channel = 1;
        private int volume = 30;

        public void PowerOn()
        {
            powerOn = true;
            Console.WriteLine("收音机已开机");
        }

        public void PowerOff()
        {
            powerOn = false;
            Console.WriteLine("收音机已关机");
        }

        public void SetChannel(int channel)
        {
            this.channel = channel;
            Console.WriteLine($"收音机切换到频道 {channel}");
        }

        public int GetChannel()
        {
            return channel;
        }

        public void SetVolume(int volume)
        {
            this.volume = volume;
            Console.WriteLine($"收音机音量设置为 {volume}");
        }

        public int GetVolume()
        {
            return volume;
        }
    }

    // 抽象：遥控器
    public abstract class RemoteControl
    {
        protected IDevice device;

        public RemoteControl(IDevice device)
        {
            this.device = device;
        }

        public virtual void TogglePower()
        {
            Console.WriteLine("切换电源状态");
            device.PowerOn();
        }

        public virtual void VolumeUp()
        {
            int volume = device.GetVolume();
            device.SetVolume(volume + 10);
        }

        public virtual void VolumeDown()
        {
            int volume = device.GetVolume();
            device.SetVolume(volume - 10);
        }

        public virtual void ChannelUp()
        {
            int channel = device.GetChannel();
            device.SetChannel(channel + 1);
        }

        public virtual void ChannelDown()
        {
            int channel = device.GetChannel();
            device.SetChannel(channel - 1);
        }
    }

    // 高级遥控器
    public class AdvancedRemoteControl : RemoteControl
    {
        public AdvancedRemoteControl(IDevice device) : base(device)
        {
        }

        public void Mute()
        {
            Console.WriteLine("静音");
            device.SetVolume(0);
        }
    }
}