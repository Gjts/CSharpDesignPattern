namespace _17Command
{
    // WMS仓库系统
    public class WarehouseSystem
    {
        private Dictionary<string, int> _inventory = new Dictionary<string, int>();
        private Dictionary<string, string> _locations = new Dictionary<string, string>();
        private List<string> _operationLog = new List<string>();

        public void AddStock(string sku, int quantity, string location)
        {
            if (!_inventory.ContainsKey(sku))
            {
                _inventory[sku] = 0;
            }
            _inventory[sku] += quantity;
            _locations[sku] = location;
            
            var log = $"入库: SKU={sku}, 数量={quantity}, 位置={location}";
            _operationLog.Add(log);
            Console.WriteLine($"  {log}");
        }

        public void RemoveStock(string sku, int quantity)
        {
            if (_inventory.ContainsKey(sku) && _inventory[sku] >= quantity)
            {
                _inventory[sku] -= quantity;
                var log = $"出库: SKU={sku}, 数量={quantity}";
                _operationLog.Add(log);
                Console.WriteLine($"  {log}");
            }
            else
            {
                Console.WriteLine($"  出库失败: 库存不足");
            }
        }

        public void MoveStock(string sku, string fromLocation, string toLocation)
        {
            if (_locations.ContainsKey(sku) && _locations[sku] == fromLocation)
            {
                _locations[sku] = toLocation;
                var log = $"移库: SKU={sku}, 从{fromLocation}到{toLocation}";
                _operationLog.Add(log);
                Console.WriteLine($"  {log}");
            }
        }

        public int GetStock(string sku)
        {
            return _inventory.ContainsKey(sku) ? _inventory[sku] : 0;
        }

        public string GetLocation(string sku)
        {
            return _locations.ContainsKey(sku) ? _locations[sku] : "未知";
        }

        public void PrintInventory()
        {
            Console.WriteLine("\n当前库存状态:");
            foreach (var item in _inventory)
            {
                var location = _locations.ContainsKey(item.Key) ? _locations[item.Key] : "未知";
                Console.WriteLine($"  {item.Key}: 数量={item.Value}, 位置={location}");
            }
        }
    }

    // WMS命令接口
    public interface IWMSCommand
    {
        void Execute();
        void Undo();
        string GetDescription();
    }

    // 入库命令
    public class InboundCommand : IWMSCommand
    {
        private WarehouseSystem _warehouse;
        private string _sku;
        private int _quantity;
        private string _location;

        public InboundCommand(WarehouseSystem warehouse, string sku, int quantity, string location)
        {
            _warehouse = warehouse;
            _sku = sku;
            _quantity = quantity;
            _location = location;
        }

        public void Execute()
        {
            Console.WriteLine($"执行入库命令: {GetDescription()}");
            _warehouse.AddStock(_sku, _quantity, _location);
        }

        public void Undo()
        {
            Console.WriteLine($"撤销入库: SKU={_sku}, 数量={_quantity}");
            _warehouse.RemoveStock(_sku, _quantity);
        }

        public string GetDescription()
        {
            return $"SKU={_sku}, 数量={_quantity}, 位置={_location}";
        }
    }

    // 出库命令
    public class OutboundCommand : IWMSCommand
    {
        private WarehouseSystem _warehouse;
        private string _sku;
        private int _quantity;
        private int _previousQuantity;

        public OutboundCommand(WarehouseSystem warehouse, string sku, int quantity)
        {
            _warehouse = warehouse;
            _sku = sku;
            _quantity = quantity;
        }

        public void Execute()
        {
            Console.WriteLine($"执行出库命令: {GetDescription()}");
            _previousQuantity = _warehouse.GetStock(_sku);
            _warehouse.RemoveStock(_sku, _quantity);
        }

        public void Undo()
        {
            Console.WriteLine($"撤销出库: 恢复SKU={_sku}, 数量={_quantity}");
            var location = _warehouse.GetLocation(_sku);
            _warehouse.AddStock(_sku, _quantity, location);
        }

        public string GetDescription()
        {
            return $"SKU={_sku}, 数量={_quantity}";
        }
    }

    // 移库命令
    public class TransferCommand : IWMSCommand
    {
        private WarehouseSystem _warehouse;
        private string _sku;
        private string _fromLocation;
        private string _toLocation;

        public TransferCommand(WarehouseSystem warehouse, string sku, string fromLocation, string toLocation)
        {
            _warehouse = warehouse;
            _sku = sku;
            _fromLocation = fromLocation;
            _toLocation = toLocation;
        }

        public void Execute()
        {
            Console.WriteLine($"执行移库命令: {GetDescription()}");
            _warehouse.MoveStock(_sku, _fromLocation, _toLocation);
        }

        public void Undo()
        {
            Console.WriteLine($"撤销移库: SKU={_sku}, 恢复到{_fromLocation}");
            _warehouse.MoveStock(_sku, _toLocation, _fromLocation);
        }

        public string GetDescription()
        {
            return $"SKU={_sku}, 从{_fromLocation}到{_toLocation}";
        }
    }

    // 批量操作命令
    public class BatchCommand : IWMSCommand
    {
        private List<IWMSCommand> _commands;
        private string _batchName;

        public BatchCommand(string batchName)
        {
            _batchName = batchName;
            _commands = new List<IWMSCommand>();
        }

        public void AddCommand(IWMSCommand command)
        {
            _commands.Add(command);
        }

        public void Execute()
        {
            Console.WriteLine($"\n执行批量操作: {_batchName}");
            foreach (var command in _commands)
            {
                command.Execute();
            }
        }

        public void Undo()
        {
            Console.WriteLine($"\n撤销批量操作: {_batchName}");
            // 反向撤销
            for (int i = _commands.Count - 1; i >= 0; i--)
            {
                _commands[i].Undo();
            }
        }

        public string GetDescription()
        {
            return $"{_batchName} (包含{_commands.Count}个操作)";
        }
    }

    // 盘点命令
    public class InventoryCheckCommand : IWMSCommand
    {
        private WarehouseSystem _warehouse;
        private Dictionary<string, int> _adjustments;
        private Dictionary<string, int> _originalQuantities;

        public InventoryCheckCommand(WarehouseSystem warehouse, Dictionary<string, int> adjustments)
        {
            _warehouse = warehouse;
            _adjustments = adjustments;
            _originalQuantities = new Dictionary<string, int>();
        }

        public void Execute()
        {
            Console.WriteLine("执行盘点调整:");
            foreach (var adjustment in _adjustments)
            {
                _originalQuantities[adjustment.Key] = _warehouse.GetStock(adjustment.Key);
                var diff = adjustment.Value - _originalQuantities[adjustment.Key];
                
                if (diff > 0)
                {
                    Console.WriteLine($"  盘盈: SKU={adjustment.Key}, 增加{diff}");
                    _warehouse.AddStock(adjustment.Key, diff, _warehouse.GetLocation(adjustment.Key));
                }
                else if (diff < 0)
                {
                    Console.WriteLine($"  盘亏: SKU={adjustment.Key}, 减少{-diff}");
                    _warehouse.RemoveStock(adjustment.Key, -diff);
                }
            }
        }

        public void Undo()
        {
            Console.WriteLine("撤销盘点调整:");
            foreach (var original in _originalQuantities)
            {
                var currentQuantity = _warehouse.GetStock(original.Key);
                var diff = original.Value - currentQuantity;
                
                if (diff > 0)
                {
                    _warehouse.AddStock(original.Key, diff, _warehouse.GetLocation(original.Key));
                }
                else if (diff < 0)
                {
                    _warehouse.RemoveStock(original.Key, -diff);
                }
            }
        }

        public string GetDescription()
        {
            return $"盘点调整 {_adjustments.Count} 个SKU";
        }
    }

    // WMS操作管理器
    public class WMSOperationManager
    {
        private Stack<IWMSCommand> _history = new Stack<IWMSCommand>();
        private Stack<IWMSCommand> _redoStack = new Stack<IWMSCommand>();
        private Queue<IWMSCommand> _pendingCommands = new Queue<IWMSCommand>();

        public void ExecuteCommand(IWMSCommand command)
        {
            command.Execute();
            _history.Push(command);
            _redoStack.Clear();
        }

        public void QueueCommand(IWMSCommand command)
        {
            _pendingCommands.Enqueue(command);
            Console.WriteLine($"命令已加入队列: {command.GetDescription()}");
        }

        public void ProcessQueue()
        {
            Console.WriteLine($"\n处理队列中的 {_pendingCommands.Count} 个命令");
            while (_pendingCommands.Count > 0)
            {
                var command = _pendingCommands.Dequeue();
                ExecuteCommand(command);
            }
        }

        public void Undo()
        {
            if (_history.Count > 0)
            {
                var command = _history.Pop();
                command.Undo();
                _redoStack.Push(command);
            }
            else
            {
                Console.WriteLine("没有可撤销的操作");
            }
        }

        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                var command = _redoStack.Pop();
                Console.WriteLine($"重做: {command.GetDescription()}");
                command.Execute();
                _history.Push(command);
            }
            else
            {
                Console.WriteLine("没有可重做的操作");
            }
        }
    }
}
