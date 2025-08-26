namespace _22State._02Example.MediaPlayer
{
    // 媒体播放器状态接口
    public interface IMediaPlayerState
    {
        void Play(MediaPlayerContext context);
        void Pause(MediaPlayerContext context);
        void Stop(MediaPlayerContext context);
        void Next(MediaPlayerContext context);
        void Previous(MediaPlayerContext context);
        string GetStateName();
    }

    // 媒体播放器上下文
    public class MediaPlayerContext
    {
        private IMediaPlayerState _state;
        private List<string> _playlist;
        private int _currentTrack;

        public MediaPlayerContext()
        {
            _playlist = new List<string> 
            { 
                "歌曲1 - 流行音乐", 
                "歌曲2 - 摇滚乐", 
                "歌曲3 - 古典音乐",
                "歌曲4 - 爵士乐"
            };
            _currentTrack = 0;
            _state = new StoppedState();
            Console.WriteLine("媒体播放器初始化完成");
        }

        public void SetState(IMediaPlayerState state)
        {
            _state = state;
            Console.WriteLine($"  播放器状态: {state.GetStateName()}");
        }

        public string GetCurrentTrack()
        {
            if (_currentTrack >= 0 && _currentTrack < _playlist.Count)
                return _playlist[_currentTrack];
            return "无歌曲";
        }

        public void NextTrack()
        {
            _currentTrack = (_currentTrack + 1) % _playlist.Count;
            Console.WriteLine($"  切换到: {GetCurrentTrack()}");
        }

        public void PreviousTrack()
        {
            _currentTrack = _currentTrack > 0 ? _currentTrack - 1 : _playlist.Count - 1;
            Console.WriteLine($"  切换到: {GetCurrentTrack()}");
        }

        public void Play() => _state.Play(this);
        public void Pause() => _state.Pause(this);
        public void Stop() => _state.Stop(this);
        public void Next() => _state.Next(this);
        public void Previous() => _state.Previous(this);
    }

    // 停止状态
    public class StoppedState : IMediaPlayerState
    {
        public void Play(MediaPlayerContext context)
        {
            Console.WriteLine($"  ▶️ 开始播放: {context.GetCurrentTrack()}");
            context.SetState(new PlayingState());
        }

        public void Pause(MediaPlayerContext context)
        {
            Console.WriteLine("  ❌ 播放器已停止，无法暂停");
        }

        public void Stop(MediaPlayerContext context)
        {
            Console.WriteLine("  ❌ 播放器已经是停止状态");
        }

        public void Next(MediaPlayerContext context)
        {
            context.NextTrack();
            Console.WriteLine("  ℹ️ 已选择下一首，按播放键开始");
        }

        public void Previous(MediaPlayerContext context)
        {
            context.PreviousTrack();
            Console.WriteLine("  ℹ️ 已选择上一首，按播放键开始");
        }

        public string GetStateName() => "停止";
    }

    // 播放状态
    public class PlayingState : IMediaPlayerState
    {
        public void Play(MediaPlayerContext context)
        {
            Console.WriteLine("  ❌ 正在播放中");
        }

        public void Pause(MediaPlayerContext context)
        {
            Console.WriteLine($"  ⏸️ 暂停播放: {context.GetCurrentTrack()}");
            context.SetState(new PausedState());
        }

        public void Stop(MediaPlayerContext context)
        {
            Console.WriteLine("  ⏹️ 停止播放");
            context.SetState(new StoppedState());
        }

        public void Next(MediaPlayerContext context)
        {
            context.NextTrack();
            Console.WriteLine($"  ⏭️ 播放下一首");
        }

        public void Previous(MediaPlayerContext context)
        {
            context.PreviousTrack();
            Console.WriteLine($"  ⏮️ 播放上一首");
        }

        public string GetStateName() => "播放中";
    }

    // 暂停状态
    public class PausedState : IMediaPlayerState
    {
        public void Play(MediaPlayerContext context)
        {
            Console.WriteLine($"  ▶️ 继续播放: {context.GetCurrentTrack()}");
            context.SetState(new PlayingState());
        }

        public void Pause(MediaPlayerContext context)
        {
            Console.WriteLine("  ❌ 已经是暂停状态");
        }

        public void Stop(MediaPlayerContext context)
        {
            Console.WriteLine("  ⏹️ 停止播放");
            context.SetState(new StoppedState());
        }

        public void Next(MediaPlayerContext context)
        {
            context.NextTrack();
            Console.WriteLine("  ℹ️ 已选择下一首（暂停中）");
        }

        public void Previous(MediaPlayerContext context)
        {
            context.PreviousTrack();
            Console.WriteLine("  ℹ️ 已选择上一首（暂停中）");
        }

        public string GetStateName() => "暂停";
    }
}
