using _Prototype._02Example.Document;
using _Prototype._02Example.GameCharacter;

namespace _06Prototype
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 原型模式 (Prototype Pattern) ================================");
            Console.WriteLine("适用场景：当创建对象成本较大，且需要创建多个相似对象时");
            Console.WriteLine("特点：通过复制现有实例来创建新对象，而不是通过new操作符");
            Console.WriteLine("优点：避免重复的初始化代码；隐藏创建对象的复杂性；提高性能\n");

            Console.WriteLine("-------------------------------- 文档模板系统 ----------------------------------");
            
            // 创建原型文档
            var contractTemplate = new ContractDocument
            {
                Title = "标准合同模板",
                Content = "合同条款内容...",
                Author = "法务部",
                CreateDate = DateTime.Now
            };
            
            // 克隆并修改
            Console.WriteLine("1. 原始合同模板：");
            contractTemplate.Display();
            
            Console.WriteLine("\n2. 克隆并定制销售合同：");
            var salesContract = (ContractDocument)contractTemplate.Clone();
            salesContract.Title = "销售合同 - 客户A";
            salesContract.AddClause("付款方式：月结30天");
            salesContract.Display();
            
            Console.WriteLine("\n3. 克隆并定制采购合同：");
            var purchaseContract = (ContractDocument)contractTemplate.Clone();
            purchaseContract.Title = "采购合同 - 供应商B";
            purchaseContract.AddClause("交货期限：15个工作日");
            purchaseContract.Display();

            Console.WriteLine("\n-------------------------------- 游戏角色系统 ----------------------------------");
            
            // 创建原型角色
            var warriorPrototype = new GameCharacter
            {
                Name = "战士模板",
                Level = 1,
                Health = 100,
                Attack = 50,
                Defense = 30
            };
            warriorPrototype.AddSkill("重击");
            warriorPrototype.AddSkill("防御姿态");
            
            Console.WriteLine("1. 原型战士角色：");
            warriorPrototype.Display();
            
            Console.WriteLine("\n2. 克隆精英战士：");
            var eliteWarrior = warriorPrototype.DeepClone();
            eliteWarrior.Name = "精英战士";
            eliteWarrior.Level = 10;
            eliteWarrior.Health = 500;
            eliteWarrior.Attack = 150;
            eliteWarrior.AddSkill("旋风斩");
            eliteWarrior.Display();
            
            Console.WriteLine("\n3. 克隆BOSS战士：");
            var bossWarrior = warriorPrototype.DeepClone();
            bossWarrior.Name = "战士BOSS";
            bossWarrior.Level = 50;
            bossWarrior.Health = 5000;
            bossWarrior.Attack = 500;
            bossWarrior.AddSkill("毁灭打击");
            bossWarrior.Display();
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 通过克隆避免了重复的初始化过程");
            Console.WriteLine("- 支持深克隆和浅克隆两种方式");
            Console.WriteLine("- 特别适用于创建成本高但差异小的对象");
        }
    }
}
