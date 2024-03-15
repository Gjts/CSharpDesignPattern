using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _SimpleFactory._02Example._02Warehousing
{
    // 抽象入库类
    public abstract class Warehousing
    {
        public abstract void Process();
    }

    // 具体入库类：普通入库
    public class NormalWarehousing : Warehousing
    {
        public override void Process()
        {
            Console.WriteLine("普通入库");
        }
    }

    // 具体入库类：冷链入库
    public class ColdChainWarehousing : Warehousing
    {
        public override void Process()
        {
            Console.WriteLine("冷链入库");
        }
    }

    // 具体入库类：危险品入库
    public class HazardousMaterialWarehousing : Warehousing
    {
        public override void Process()
        {
            Console.WriteLine("危险品入库");
        }
    }
}
