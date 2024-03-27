using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05Builder._02Example.WMSWarehouse
{
    public class WarehouseItemDirector
    {
        public void Construct(IItemBuilder<WarehouseItem> builder, string name, string location, int quantity)
        {
            builder.BuildName(name);
            builder.BuildLocation(location);
            builder.BuildQuantity(quantity);
        }
    }
}
