namespace _22State
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 状态模式 (State Pattern) ================================");
            Console.WriteLine("适用场景：对象的行为取决于它的状态，且状态会在运行时改变；代码中包含大量与状态相关的条件语句");
            Console.WriteLine("特点：允许对象在内部状态改变时改变它的行为，对象看起来好像修改了它的类");
            Console.WriteLine("优点：将状态转换逻辑分布到状态类中；消除了庞大的条件分支语句；状态类职责明确\n");

            Console.WriteLine("-------------------------------- 订单状态管理 ----------------------------------");
            
            var order = new Order();
            
            Console.WriteLine("1. 订单流程演示：");
            order.Process();  // 新建 -> 已支付
            order.Ship();     // 已支付 -> 已发货
            order.Deliver();  // 已发货 -> 已完成
            
            Console.WriteLine("\n2. 异常流程演示：");
            order.Cancel();   // 已完成状态无法取消
            
            Console.WriteLine("\n3. 新订单取消流程：");
            var order2 = new Order();
            order2.Cancel();  // 新建状态可以取消

            Console.WriteLine("\n-------------------------------- 游戏角色状态 ----------------------------------");
            
            var player = new Player("勇者");
            
            Console.WriteLine("1. 正常状态行为：");
            player.Move();
            player.Attack();
            
            Console.WriteLine("\n2. 受伤后状态变化：");
            player.TakeDamage(30);
            player.Move();
            player.Attack();
            
            Console.WriteLine("\n3. 休息恢复：");
            player.Rest();
            player.Move();
            
            Console.WriteLine("\n4. 死亡状态：");
            player.TakeDamage(80);
            player.Move();
            player.Attack();
            
            Console.WriteLine("\n5. 复活：");
            player.UseRevive();
            player.Move();
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 每个状态都是一个独立的类，封装了该状态下的行为");
            Console.WriteLine("- 状态转换由状态类自己控制，符合开闭原则");
            Console.WriteLine("- 消除了大量的if-else判断，代码更清晰");
        }
    }
}
