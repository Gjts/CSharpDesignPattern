namespace _15Template
{
    // 抽象游戏AI模板类
    public abstract class GameAI
    {
        // 模板方法 - 定义AI行为的骨架
        public void TakeTurn()
        {
            Console.WriteLine($"{GetCharacterType()}的回合:");
            
            // 步骤1：收集信息
            CollectInformation();
            
            // 步骤2：分析局势
            var situation = AnalyzeSituation();
            
            // 步骤3：决定策略
            var strategy = DecideStrategy(situation);
            
            // 步骤4：执行行动
            if (ShouldUseSkill())
            {
                UseSkill();
            }
            else if (ShouldDefend(situation))
            {
                Defend();
            }
            else
            {
                Attack();
            }
            
            // 步骤5：移动（可选）
            if (ShouldMove(strategy))
            {
                Move();
            }
            
            // 步骤6：结束回合
            EndTurn();
            
            Console.WriteLine();
        }
        
        // 抽象方法 - 必须由子类实现
        protected abstract string GetCharacterType();
        protected abstract void CollectInformation();
        protected abstract string AnalyzeSituation();
        protected abstract string DecideStrategy(string situation);
        protected abstract void Attack();
        protected abstract void UseSkill();
        
        // 通用方法 - 可被子类重写
        protected virtual void Defend()
        {
            Console.WriteLine("  执行防御动作");
        }
        
        protected virtual void Move()
        {
            Console.WriteLine("  移动到更好的位置");
        }
        
        protected virtual void EndTurn()
        {
            Console.WriteLine("  回合结束");
        }
        
        // 钩子方法
        protected virtual bool ShouldUseSkill() => false;
        protected virtual bool ShouldDefend(string situation) => situation == "危险";
        protected virtual bool ShouldMove(string strategy) => strategy == "撤退";
    }
    
    // 具体实现：战士AI
    public class WarriorAI : GameAI
    {
        protected override string GetCharacterType() => "战士";
        
        protected override void CollectInformation()
        {
            Console.WriteLine("  扫描附近的敌人...");
            Console.WriteLine("  检查自己的生命值和护甲...");
        }
        
        protected override string AnalyzeSituation()
        {
            Console.WriteLine("  分析战场局势...");
            Console.WriteLine("  判断敌人数量和距离...");
            return "安全"; // 模拟返回
        }
        
        protected override string DecideStrategy(string situation)
        {
            Console.WriteLine("  制定战斗策略...");
            if (situation == "安全")
            {
                Console.WriteLine("  决定采取进攻策略");
                return "进攻";
            }
            return "防守";
        }
        
        protected override void Attack()
        {
            Console.WriteLine("  使用重击攻击最近的敌人!");
            Console.WriteLine("  造成150点物理伤害");
        }
        
        protected override void UseSkill()
        {
            Console.WriteLine("  释放旋风斩!");
            Console.WriteLine("  对周围所有敌人造成100点伤害");
        }
        
        protected override bool ShouldUseSkill()
        {
            Console.WriteLine("  检查是否有多个敌人...");
            return new Random().Next(100) > 70; // 30%概率使用技能
        }
    }
    
    // 具体实现：法师AI
    public class MageAI : GameAI
    {
        protected override string GetCharacterType() => "法师";
        
        protected override void CollectInformation()
        {
            Console.WriteLine("  感知魔法能量...");
            Console.WriteLine("  检查法力值...");
            Console.WriteLine("  扫描敌人的魔法抗性...");
        }
        
        protected override string AnalyzeSituation()
        {
            Console.WriteLine("  分析敌人弱点...");
            Console.WriteLine("  计算法术射程...");
            return "安全";
        }
        
        protected override string DecideStrategy(string situation)
        {
            Console.WriteLine("  选择最佳法术组合...");
            return "远程攻击";
        }
        
        protected override void Attack()
        {
            Console.WriteLine("  施放火球术!");
            Console.WriteLine("  造成200点魔法伤害");
        }
        
        protected override void UseSkill()
        {
            Console.WriteLine("  释放陨石术!");
            Console.WriteLine("  对区域内所有敌人造成300点魔法伤害");
        }
        
        protected override void Defend()
        {
            Console.WriteLine("  施放魔法护盾");
            Console.WriteLine("  吸收接下来的200点伤害");
        }
        
        protected override bool ShouldMove(string strategy)
        {
            // 法师总是保持距离
            return true;
        }
        
        protected override void Move()
        {
            Console.WriteLine("  传送到安全距离");
        }
        
        protected override bool ShouldUseSkill()
        {
            Console.WriteLine("  检查法力值是否充足...");
            return new Random().Next(100) > 50; // 50%概率使用大招
        }
    }
    
    // 具体实现：弓箭手AI
    public class ArcherAI : GameAI
    {
        protected override string GetCharacterType() => "弓箭手";
        
        protected override void CollectInformation()
        {
            Console.WriteLine("  观察风向和距离...");
            Console.WriteLine("  标记优先目标...");
            Console.WriteLine("  检查箭矢数量...");
        }
        
        protected override string AnalyzeSituation()
        {
            Console.WriteLine("  计算最佳射击位置...");
            Console.WriteLine("  评估掩体位置...");
            return "有利";
        }
        
        protected override string DecideStrategy(string situation)
        {
            Console.WriteLine("  选择狙击策略...");
            return "精准射击";
        }
        
        protected override void Attack()
        {
            Console.WriteLine("  瞄准要害射击!");
            Console.WriteLine("  造成180点穿刺伤害");
            Console.WriteLine("  50%概率造成暴击");
        }
        
        protected override void UseSkill()
        {
            Console.WriteLine("  释放箭雨!");
            Console.WriteLine("  对指定区域进行覆盖射击");
            Console.WriteLine("  每支箭造成50点伤害");
        }
        
        protected override void Move()
        {
            Console.WriteLine("  快速移动到制高点");
            Console.WriteLine("  获得射程和精准度加成");
        }
        
        protected override bool ShouldDefend(string situation)
        {
            // 弓箭手很少防御，更倾向于保持距离
            return false;
        }
        
        protected override void EndTurn()
        {
            base.EndTurn();
            Console.WriteLine("  设置陷阱防止敌人接近");
        }
    }
}