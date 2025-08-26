namespace _22State
{
    // 订单状态接口
    public interface IOrderState
    {
        void Process(Order order);
        void Ship(Order order);
        void Deliver(Order order);
        void Cancel(Order order);
    }

    // 新建状态
    public class NewOrderState : IOrderState
    {
        public void Process(Order order)
        {
            Console.WriteLine("订单已支付，状态变更为：已支付");
            order.SetState(new PaidOrderState());
        }

        public void Ship(Order order)
        {
            Console.WriteLine("订单尚未支付，无法发货");
        }

        public void Deliver(Order order)
        {
            Console.WriteLine("订单尚未支付，无法交付");
        }

        public void Cancel(Order order)
        {
            Console.WriteLine("订单已取消");
            order.SetState(new CancelledOrderState());
        }
    }

    // 已支付状态
    public class PaidOrderState : IOrderState
    {
        public void Process(Order order)
        {
            Console.WriteLine("订单已经支付过了");
        }

        public void Ship(Order order)
        {
            Console.WriteLine("订单已发货，状态变更为：已发货");
            order.SetState(new ShippedOrderState());
        }

        public void Deliver(Order order)
        {
            Console.WriteLine("订单尚未发货，无法交付");
        }

        public void Cancel(Order order)
        {
            Console.WriteLine("已支付订单已取消，将进行退款");
            order.SetState(new CancelledOrderState());
        }
    }

    // 已发货状态
    public class ShippedOrderState : IOrderState
    {
        public void Process(Order order)
        {
            Console.WriteLine("订单已发货，无法再次处理");
        }

        public void Ship(Order order)
        {
            Console.WriteLine("订单已经发货了");
        }

        public void Deliver(Order order)
        {
            Console.WriteLine("订单已送达，状态变更为：已完成");
            order.SetState(new DeliveredOrderState());
        }

        public void Cancel(Order order)
        {
            Console.WriteLine("已发货订单无法取消");
        }
    }

    // 已完成状态
    public class DeliveredOrderState : IOrderState
    {
        public void Process(Order order)
        {
            Console.WriteLine("订单已完成");
        }

        public void Ship(Order order)
        {
            Console.WriteLine("订单已完成");
        }

        public void Deliver(Order order)
        {
            Console.WriteLine("订单已经送达了");
        }

        public void Cancel(Order order)
        {
            Console.WriteLine("已完成订单无法取消");
        }
    }

    // 已取消状态
    public class CancelledOrderState : IOrderState
    {
        public void Process(Order order)
        {
            Console.WriteLine("订单已取消，无法处理");
        }

        public void Ship(Order order)
        {
            Console.WriteLine("订单已取消，无法发货");
        }

        public void Deliver(Order order)
        {
            Console.WriteLine("订单已取消，无法交付");
        }

        public void Cancel(Order order)
        {
            Console.WriteLine("订单已经取消了");
        }
    }

    // 订单类
    public class Order
    {
        private IOrderState _state;

        public Order()
        {
            _state = new NewOrderState();
            Console.WriteLine("创建新订单");
        }

        public void SetState(IOrderState state)
        {
            _state = state;
        }

        public void Process()
        {
            _state.Process(this);
        }

        public void Ship()
        {
            _state.Ship(this);
        }

        public void Deliver()
        {
            _state.Deliver(this);
        }

        public void Cancel()
        {
            _state.Cancel(this);
        }
    }

    // 游戏角色状态接口
    public interface IPlayerState
    {
        void Move(Player player);
        void Attack(Player player);
        void Rest(Player player);
        void TakeDamage(Player player, int damage);
    }

    // 正常状态
    public class NormalState : IPlayerState
    {
        public void Move(Player player)
        {
            Console.WriteLine($"{player.Name} 正常移动");
        }

        public void Attack(Player player)
        {
            Console.WriteLine($"{player.Name} 发起攻击！造成100点伤害");
        }

        public void Rest(Player player)
        {
            Console.WriteLine($"{player.Name} 休息中，恢复20点生命值");
            player.Health += 20;
        }

        public void TakeDamage(Player player, int damage)
        {
            player.Health -= damage;
            Console.WriteLine($"{player.Name} 受到{damage}点伤害，剩余生命值: {player.Health}");
            
            if (player.Health <= 0)
            {
                player.SetState(new DeadState());
            }
            else if (player.Health < 30)
            {
                player.SetState(new WeakenedState());
            }
        }
    }

    // 虚弱状态
    public class WeakenedState : IPlayerState
    {
        public void Move(Player player)
        {
            Console.WriteLine($"{player.Name} 虚弱地移动（速度减半）");
        }

        public void Attack(Player player)
        {
            Console.WriteLine($"{player.Name} 虚弱地攻击！造成50点伤害");
        }

        public void Rest(Player player)
        {
            Console.WriteLine($"{player.Name} 休息中，恢复30点生命值");
            player.Health += 30;
            if (player.Health > 30)
            {
                Console.WriteLine($"{player.Name} 恢复正常状态");
                player.SetState(new NormalState());
            }
        }

        public void TakeDamage(Player player, int damage)
        {
            player.Health -= damage;
            Console.WriteLine($"{player.Name} 受到{damage}点伤害，剩余生命值: {player.Health}");
            
            if (player.Health <= 0)
            {
                player.SetState(new DeadState());
            }
        }
    }

    // 死亡状态
    public class DeadState : IPlayerState
    {
        public void Move(Player player)
        {
            Console.WriteLine($"{player.Name} 已死亡，无法移动");
        }

        public void Attack(Player player)
        {
            Console.WriteLine($"{player.Name} 已死亡，无法攻击");
        }

        public void Rest(Player player)
        {
            Console.WriteLine($"{player.Name} 已死亡，无法休息");
        }

        public void TakeDamage(Player player, int damage)
        {
            Console.WriteLine($"{player.Name} 已死亡");
        }
    }

    // 游戏角色
    public class Player
    {
        private IPlayerState _state;
        public string Name { get; }
        public int Health { get; set; }

        public Player(string name)
        {
            Name = name;
            Health = 100;
            _state = new NormalState();
            Console.WriteLine($"创建角色: {Name}，生命值: {Health}");
        }

        public void SetState(IPlayerState state)
        {
            _state = state;
        }

        public void Move()
        {
            _state.Move(this);
        }

        public void Attack()
        {
            _state.Attack(this);
        }

        public void Rest()
        {
            _state.Rest(this);
        }

        public void TakeDamage(int damage)
        {
            _state.TakeDamage(this, damage);
        }

        public void UseRevive()
        {
            if (_state is DeadState)
            {
                Console.WriteLine($"{Name} 使用复活道具！");
                Health = 50;
                _state = new NormalState();
                Console.WriteLine($"{Name} 复活了！生命值: {Health}");
            }
            else
            {
                Console.WriteLine($"{Name} 还活着，无需使用复活道具");
            }
        }
    }
}

