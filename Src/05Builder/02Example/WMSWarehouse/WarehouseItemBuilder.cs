using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05Builder._02Example.WMSWarehouse
{
    public class WarehouseItemBuilder : IItemBuilder<WarehouseItem>
    {
        private WarehouseItem _item = new WarehouseItem();

        public void BuildName(string name)
        {
            _item.Name = name;
        }

        public void BuildLocation(string location)
        {
            _item.Location = location;
        }

        public void BuildQuantity(int quantity)
        {
            _item.Quantity = quantity;
        }

        public WarehouseItem GetResult()
        {
            return _item;
        }
    }
}
