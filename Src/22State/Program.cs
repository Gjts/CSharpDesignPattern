namespace _22State
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 状态模式 (State Pattern) ===\n");

            Console.WriteLine("示例1：订单状态管理");
            Console.WriteLine("------------------------");
            var order = new Order();
            
            order.Process();  // 新建 -> 已支付
            order.Ship();     // 已支付 -> 已发货
            order.Deliver();  // 已发货 -> 已完成
            order.Cancel();   // 无法取消已完成订单

            Console.WriteLine("\n示例2：游戏角色状态");
            Console.WriteLine("------------------------");
            var player = new Player("勇者");
            
            player.Move();
            player.Attack();
            
            player.TakeDamage(30);
            player.Move();
            player.Attack();
            
            player.Rest();
            player.Move();
            
            player.TakeDamage(80);
            player.Move();
            player.UseRevive();
            player.Move();
        }
    }
}
