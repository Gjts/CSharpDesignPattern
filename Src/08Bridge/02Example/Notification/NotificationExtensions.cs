namespace _Bridge._02Example.Notification
{
    // 具体实现 - 推送发送器
    public class PushSender : IMessageSender
    {
        public void SendMessage(string title, string message)
        {
            Console.WriteLine("   [推送通知]");
            Console.WriteLine($"     标题: {title}");
            Console.WriteLine($"     内容: {message}");
            Console.WriteLine("     状态: 推送已发送到手机");
        }
    }

    // 扩展抽象 - 营销通知
    public class MarketingNotification : Notification
    {
        public MarketingNotification(IMessageSender sender) : base(sender) { }

        public override void Notify(string message)
        {
            messageSender.SendMessage("【营销推广】", message);
        }

        public void Send(string title, string message)
        {
            Notify($"{title}: {message}");
        }
    }


}