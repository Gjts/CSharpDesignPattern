namespace _Prototype._02Example.GameCharacter
{
    // 游戏角色原型
    public abstract class GameCharacter : ICloneable
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public List<string> Skills { get; set; }
        public Dictionary<string, int> Equipment { get; set; }

        protected GameCharacter()
        {
            Skills = new List<string>();
            Equipment = new Dictionary<string, int>();
        }

        public abstract object Clone();

        public virtual void Display()
        {
            Console.WriteLine($"  角色: {Name} (Lv.{Level})");
            Console.WriteLine($"  生命值: {Health}, 攻击力: {Attack}, 防御力: {Defense}");
            Console.WriteLine($"  技能: {string.Join(", ", Skills)}");
            Console.WriteLine($"  装备: {string.Join(", ", Equipment.Select(e => $"{e.Key}(+{e.Value})"))}");
        }
    }

    // 战士角色
    public class Warrior : GameCharacter
    {
        public int Rage { get; set; }

        public override object Clone()
        {
            var cloned = new Warrior
            {
                Name = this.Name,
                Level = this.Level,
                Health = this.Health,
                Attack = this.Attack,
                Defense = this.Defense,
                Rage = this.Rage,
                Skills = new List<string>(this.Skills),
                Equipment = new Dictionary<string, int>(this.Equipment)
            };
            return cloned;
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"  怒气值: {Rage}");
        }
    }

    // 法师角色
    public class Mage : GameCharacter
    {
        public int Mana { get; set; }

        public override object Clone()
        {
            var cloned = new Mage
            {
                Name = this.Name,
                Level = this.Level,
                Health = this.Health,
                Attack = this.Attack,
                Defense = this.Defense,
                Mana = this.Mana,
                Skills = new List<string>(this.Skills),
                Equipment = new Dictionary<string, int>(this.Equipment)
            };
            return cloned;
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"  法力值: {Mana}");
        }
    }

    // 角色管理器
    public class CharacterManager
    {
        private Dictionary<string, GameCharacter> _prototypes = new Dictionary<string, GameCharacter>();

        public void RegisterPrototype(string key, GameCharacter prototype)
        {
            _prototypes[key] = prototype;
        }

        public GameCharacter CreateCharacter(string key)
        {
            if (_prototypes.ContainsKey(key))
            {
                return (GameCharacter)_prototypes[key].Clone();
            }
            return null;
        }
    }
}