namespace _Bridge._02Example.Notification
{
    // 实现部分 - 消息发送器接口
    public interface IMessageSender
    {
        void SendMessage(string title, string message);
    }

    // 具体实现 - 邮件发送器
    public class EmailSender : IMessageSender
    {
        public void SendMessage(string title, string message)
        {
            Console.WriteLine("  [邮件通知]");
            Console.WriteLine($"    主题: {title}");
            Console.WriteLine($"    内容: {message}");
            Console.WriteLine("    状态: 邮件已发送");
        }
    }

    // 具体实现 - 短信发送器
    public class SmsSender : IMessageSender
    {
        public void SendMessage(string title, string message)
        {
            Console.WriteLine("  [短信通知]");
            Console.WriteLine($"    标题: {title}");
            Console.WriteLine($"    内容: {message}");
            Console.WriteLine("    状态: 短信已发送");
        }
    }

    // 具体实现 - 微信发送器
    public class WeChatSender : IMessageSender
    {
        public void SendMessage(string title, string message)
        {
            Console.WriteLine("  [微信通知]");
            Console.WriteLine($"    标题: {title}");
            Console.WriteLine($"    消息: {message}");
            Console.WriteLine("    状态: 微信消息已推送");
        }
    }

    // 抽象部分 - 通知类
    public abstract class Notification
    {
        protected IMessageSender messageSender;

        protected Notification(IMessageSender sender)
        {
            this.messageSender = sender;
        }

        public abstract void Notify(string message);
    }

    // 扩展抽象 - 系统通知
    public class SystemNotification : Notification
    {
        public SystemNotification(IMessageSender sender) : base(sender) { }

        public override void Notify(string message)
        {
            messageSender.SendMessage("系统通知", message);
        }

        public void Send(string title, string message)
        {
            Notify($"{title}: {message}");
        }

        public IMessageSender Sender
        {
            get { return messageSender; }
            set { messageSender = value; }
        }
    }

    // 扩展抽象 - 紧急通知
    public class UrgentNotification : Notification
    {
        public UrgentNotification(IMessageSender sender) : base(sender) { }

        public override void Notify(string message)
        {
            messageSender.SendMessage("【紧急】", message);
            // 紧急通知发送多次
            Console.WriteLine("    (紧急通知将重复发送3次)");
        }

        public void Send(string title, string message)
        {
            Notify($"{title}: {message}");
        }

        public IMessageSender Sender
        {
            get { return messageSender; }
            set { messageSender = value; }
        }
    }

    // 扩展抽象 - 日常通知
    public class DailyNotification : Notification
    {
        public DailyNotification(IMessageSender sender) : base(sender) { }

        public override void Notify(string message)
        {
            var time = DateTime.Now.ToString("yyyy-MM-dd");
            messageSender.SendMessage($"日常通知 [{time}]", message);
        }
    }
}