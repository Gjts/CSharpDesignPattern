namespace _20Memento.Example.AIModelCheckpoint
{
    // AI模型状态
    public class ModelState
    {
        public int Epoch { get; set; }
        public double Loss { get; set; }
        public double Accuracy { get; set; }
        public Dictionary<string, double[]> Weights { get; set; }
        public Dictionary<string, object> Hyperparameters { get; set; }
        public DateTime Timestamp { get; set; }

        public ModelState()
        {
            Weights = new Dictionary<string, double[]>();
            Hyperparameters = new Dictionary<string, object>();
            Timestamp = DateTime.Now;
        }

        public ModelState Clone()
        {
            var clone = new ModelState
            {
                Epoch = this.Epoch,
                Loss = this.Loss,
                Accuracy = this.Accuracy,
                Timestamp = this.Timestamp,
                Weights = new Dictionary<string, double[]>(),
                Hyperparameters = new Dictionary<string, object>(this.Hyperparameters)
            };

            foreach (var kvp in this.Weights)
            {
                clone.Weights[kvp.Key] = (double[])kvp.Value.Clone();
            }

            return clone;
        }
    }

    // AI模型（发起人）
    public class AIModel
    {
        private string _modelName;
        private ModelState _currentState;
        private Random _random = new Random();

        public AIModel(string modelName)
        {
            _modelName = modelName;
            _currentState = new ModelState
            {
                Epoch = 0,
                Loss = 1.0,
                Accuracy = 0.1,
                Hyperparameters = new Dictionary<string, object>
                {
                    ["learning_rate"] = 0.001,
                    ["batch_size"] = 32,
                    ["optimizer"] = "Adam"
                }
            };
            InitializeWeights();
        }

        private void InitializeWeights()
        {
            _currentState.Weights["layer1"] = GenerateRandomWeights(784 * 128);
            _currentState.Weights["layer2"] = GenerateRandomWeights(128 * 64);
            _currentState.Weights["layer3"] = GenerateRandomWeights(64 * 10);
        }

        private double[] GenerateRandomWeights(int size)
        {
            var weights = new double[size];
            for (int i = 0; i < size; i++)
            {
                weights[i] = _random.NextDouble() * 0.1 - 0.05;
            }
            return weights;
        }

        public void Train()
        {
            _currentState.Epoch++;
            
            // 模拟训练过程
            _currentState.Loss *= 0.9 + _random.NextDouble() * 0.05;
            _currentState.Accuracy = Math.Min(0.99, _currentState.Accuracy + 0.05 + _random.NextDouble() * 0.02);
            
            // 更新权重（简化模拟）
            foreach (var key in _currentState.Weights.Keys.ToList())
            {
                var weights = _currentState.Weights[key];
                for (int i = 0; i < Math.Min(10, weights.Length); i++)
                {
                    weights[i] += (_random.NextDouble() - 0.5) * 0.001;
                }
            }

            Console.WriteLine($"[{_modelName}] Epoch {_currentState.Epoch}: Loss={_currentState.Loss:F4}, Accuracy={_currentState.Accuracy:F2}%");
        }

        public ModelCheckpoint CreateCheckpoint(string description = "")
        {
            var checkpoint = new ModelCheckpoint(_currentState.Clone(), description);
            Console.WriteLine($"  ✅ 创建检查点: Epoch {_currentState.Epoch} - {description}");
            return checkpoint;
        }

        public void RestoreFromCheckpoint(ModelCheckpoint checkpoint)
        {
            if (checkpoint != null)
            {
                _currentState = checkpoint.GetState().Clone();
                Console.WriteLine($"  ♻️ 恢复到检查点: Epoch {_currentState.Epoch} - {checkpoint.Description}");
            }
        }

        public void DisplayState()
        {
            Console.WriteLine($"\n[{_modelName}] 当前状态:");
            Console.WriteLine($"  Epoch: {_currentState.Epoch}");
            Console.WriteLine($"  Loss: {_currentState.Loss:F4}");
            Console.WriteLine($"  Accuracy: {_currentState.Accuracy:P}");
            Console.WriteLine($"  学习率: {_currentState.Hyperparameters["learning_rate"]}");
        }

        public void SetHyperparameter(string name, object value)
        {
            _currentState.Hyperparameters[name] = value;
            Console.WriteLine($"  设置超参数: {name} = {value}");
        }
    }

    // 模型检查点（备忘录）
    public class ModelCheckpoint
    {
        private ModelState _state;
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ModelCheckpoint(ModelState state, string description)
        {
            _state = state;
            Description = description;
            CreatedAt = DateTime.Now;
        }

        public ModelState GetState()
        {
            return _state;
        }

        public long GetSizeInBytes()
        {
            long size = 0;
            foreach (var weights in _state.Weights.Values)
            {
                size += weights.Length * sizeof(double);
            }
            return size;
        }
    }

    // 检查点管理器（管理者）
    public class CheckpointManager
    {
        private Dictionary<string, ModelCheckpoint> _checkpoints = new();
        private Stack<ModelCheckpoint> _autoSaveCheckpoints = new();
        private int _maxAutoSaves = 5;

        public void SaveCheckpoint(string name, ModelCheckpoint checkpoint)
        {
            _checkpoints[name] = checkpoint;
            Console.WriteLine($"[检查点管理器] 保存检查点: {name}");
        }

        public void AutoSave(ModelCheckpoint checkpoint)
        {
            _autoSaveCheckpoints.Push(checkpoint);
            
            // 限制自动保存数量
            if (_autoSaveCheckpoints.Count > _maxAutoSaves)
            {
                var oldCheckpoints = new Stack<ModelCheckpoint>();
                for (int i = 0; i < _maxAutoSaves; i++)
                {
                    oldCheckpoints.Push(_autoSaveCheckpoints.Pop());
                }
                _autoSaveCheckpoints = oldCheckpoints;
            }
            
            Console.WriteLine($"[检查点管理器] 自动保存 (当前{_autoSaveCheckpoints.Count}个)");
        }

        public ModelCheckpoint LoadCheckpoint(string name)
        {
            if (_checkpoints.ContainsKey(name))
            {
                Console.WriteLine($"[检查点管理器] 加载检查点: {name}");
                return _checkpoints[name];
            }
            return null;
        }

        public ModelCheckpoint GetLatestAutoSave()
        {
            if (_autoSaveCheckpoints.Count > 0)
            {
                return _autoSaveCheckpoints.Peek();
            }
            return null;
        }

        public void ListCheckpoints()
        {
            Console.WriteLine("\n[检查点管理器] 可用检查点:");
            
            Console.WriteLine("  命名检查点:");
            foreach (var kvp in _checkpoints)
            {
                var cp = kvp.Value;
                var state = cp.GetState();
                Console.WriteLine($"    - {kvp.Key}: Epoch {state.Epoch}, Accuracy {state.Accuracy:P}, Size {cp.GetSizeInBytes() / 1024}KB");
            }
            
            Console.WriteLine($"  自动保存: {_autoSaveCheckpoints.Count} 个");
        }

        public ModelCheckpoint FindBestCheckpoint()
        {
            ModelCheckpoint best = null;
            double bestAccuracy = 0;

            foreach (var checkpoint in _checkpoints.Values)
            {
                if (checkpoint.GetState().Accuracy > bestAccuracy)
                {
                    bestAccuracy = checkpoint.GetState().Accuracy;
                    best = checkpoint;
                }
            }

            foreach (var checkpoint in _autoSaveCheckpoints)
            {
                if (checkpoint.GetState().Accuracy > bestAccuracy)
                {
                    bestAccuracy = checkpoint.GetState().Accuracy;
                    best = checkpoint;
                }
            }

            if (best != null)
            {
                Console.WriteLine($"[检查点管理器] 最佳检查点: Accuracy={bestAccuracy:P}");
            }

            return best;
        }
    }
}
