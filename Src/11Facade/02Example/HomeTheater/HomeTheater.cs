namespace _Facade._02Example.HomeTheater
{
    // 子系统类 - DVD播放器
    public class DVDPlayer
    {
        public void On()
        {
            Console.WriteLine("  DVD播放器开启");
        }

        public void Play(string movie)
        {
            Console.WriteLine($"  播放电影: {movie}");
        }

        public void Off()
        {
            Console.WriteLine("  DVD播放器关闭");
        }
    }

    // 子系统类 - 投影仪
    public class Projector
    {
        public void On()
        {
            Console.WriteLine("  投影仪开启");
        }

        public void SetInput(string input)
        {
            Console.WriteLine($"  投影仪输入源设置为: {input}");
        }

        public void Off()
        {
            Console.WriteLine("  投影仪关闭");
        }
    }

    // 子系统类 - 音响系统
    public class SoundSystem
    {
        public void On()
        {
            Console.WriteLine("  音响系统开启");
        }

        public void SetVolume(int level)
        {
            Console.WriteLine($"  音量设置为: {level}");
        }

        public void SetSurroundSound()
        {
            Console.WriteLine("  环绕立体声模式已开启");
        }

        public void Off()
        {
            Console.WriteLine("  音响系统关闭");
        }
    }

    // 子系统类 - 灯光控制
    public class Lights
    {
        public void Dim(int level)
        {
            Console.WriteLine($"  灯光调暗至 {level}%");
        }

        public void On()
        {
            Console.WriteLine("  灯光开启");
        }
    }

    // 外观类 - 家庭影院外观
    public class HomeTheaterFacade
    {
        private DVDPlayer dvd;
        private Projector projector;
        private SoundSystem sound;
        private Lights lights;

        public HomeTheaterFacade()
        {
            dvd = new DVDPlayer();
            projector = new Projector();
            sound = new SoundSystem();
            lights = new Lights();
        }

        public void WatchMovie(string movie)
        {
            Console.WriteLine("\n准备观看电影...");
            lights.Dim(10);
            projector.On();
            projector.SetInput("DVD");
            sound.On();
            sound.SetSurroundSound();
            sound.SetVolume(20);
            dvd.On();
            dvd.Play(movie);
            Console.WriteLine("电影开始播放！\n");
        }

        public void EndMovie()
        {
            Console.WriteLine("\n关闭家庭影院...");
            dvd.Off();
            sound.Off();
            projector.Off();
            lights.On();
            Console.WriteLine("家庭影院已关闭！\n");
        }
    }
}