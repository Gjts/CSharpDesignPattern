namespace _19Mediator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 中介者模式 (Mediator Pattern) ===\n");

            Console.WriteLine("示例1：聊天室系统");
            Console.WriteLine("------------------------");
            var chatRoom = new ChatRoom();
            
            var alice = new ChatUser("Alice");
            var bob = new ChatUser("Bob");
            var charlie = new ChatUser("Charlie");
            
            chatRoom.RegisterUser(alice);
            chatRoom.RegisterUser(bob);
            chatRoom.RegisterUser(charlie);
            
            alice.Send("大家好！");
            bob.Send("你好Alice！");
            charlie.Send("欢迎！");

            Console.WriteLine("\n示例2：事件总线系统");
            Console.WriteLine("------------------------");
            var eventBus = new EventBus();
            
            var ui = new UIComponent("主界面");
            var data = new DataComponent("数据管理器");
            var logger = new LogComponent("日志记录器");
            
            ui.SetEventBus(eventBus);
            data.SetEventBus(eventBus);
            logger.SetEventBus(eventBus);
            
            ui.ButtonClick();
            eventBus.Publish("UserLogin", "用户张三");
        }
    }
}
