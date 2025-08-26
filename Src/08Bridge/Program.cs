using _Bridge._02Example.Device;
using _Bridge._02Example.Notification;

namespace _08Bridge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 桥接模式 (Bridge Pattern) ================================");
            Console.WriteLine("适用场景：当抽象和实现都需要独立变化时；避免在两个维度上的继承爆炸问题");
            Console.WriteLine("特点：将抽象部分与实现部分分离，使它们可以独立变化");
            Console.WriteLine("优点：分离抽象和实现；提高系统扩展性；符合开闭原则\n");

            Console.WriteLine("-------------------------------- 智能设备控制系统 ----------------------------------");
            
            // 创建不同的设备
            var tv = new Television();
            var speaker = new SmartSpeaker();
            var light = new SmartLight();
            
            // 使用基础遥控器
            Console.WriteLine("1. 基础遥控器控制：");
            var basicRemote = new BasicRemote(tv);
            basicRemote.Power();
            basicRemote.VolumeUp();
            
            // 使用高级遥控器
            Console.WriteLine("\n2. 高级遥控器控制：");
            var advancedRemote = new AdvancedRemote(speaker);
            advancedRemote.Power();
            advancedRemote.Mute();
            
            // 切换设备
            Console.WriteLine("\n3. 同一遥控器控制不同设备：");
            advancedRemote.Device = light;
            advancedRemote.Power();

            Console.WriteLine("\n-------------------------------- 消息通知系统 ----------------------------------");
            
            // 创建不同的消息发送器
            var emailSender = new EmailSender();
            var smsSender = new SmsSender();
            var pushSender = new PushSender();
            
            // 发送不同类型的通知
            Console.WriteLine("1. 系统通知（多渠道）：");
            var systemNotification = new SystemNotification(emailSender);
            systemNotification.Send("系统维护", "今晚10点系统维护");
            systemNotification.Sender = smsSender;
            systemNotification.Send("系统维护", "今晚10点系统维护");
            
            Console.WriteLine("\n2. 紧急通知（推送）：");
            var urgentNotification = new UrgentNotification(pushSender);
            urgentNotification.Send("紧急", "服务器异常，请立即处理");
            
            Console.WriteLine("\n3. 营销通知（邮件）：");
            var marketingNotification = new MarketingNotification(emailSender);
            marketingNotification.Send("优惠活动", "双11大促销");
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 抽象（遥控器/通知类型）和实现（设备/发送方式）可以独立变化");
            Console.WriteLine("- 避免了多维度继承导致的类爆炸问题");
            Console.WriteLine("- 运行时可以动态切换实现");
        }
    }
}
