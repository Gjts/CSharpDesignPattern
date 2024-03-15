using _SimpleFactory._02Example._01Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _SimpleFactory._02Example._02Warehousing
{
    // 简单工厂类
    public class WarehousingFactory
    {
        public T CreateWarehousing<T>() where T : Warehousing, new()
        {
            return new T();
        }
    }
}
