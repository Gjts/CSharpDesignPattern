namespace _15Template
{
    // AI模型训练模板方法模式
    public abstract class AIModelTrainer
    {
        protected string _modelName;
        protected Dictionary<string, object> _hyperparameters;

        protected AIModelTrainer(string modelName)
        {
            _modelName = modelName;
            _hyperparameters = new Dictionary<string, object>();
        }

        // 模板方法 - 定义训练流程
        public void Train()
        {
            Console.WriteLine($"开始训练 {_modelName} 模型");
            
            // 步骤1：数据预处理
            var data = LoadData();
            var processedData = PreprocessData(data);
            
            // 步骤2：特征工程
            var features = ExtractFeatures(processedData);
            
            // 步骤3：数据分割
            var (trainSet, valSet, testSet) = SplitData(features);
            
            // 步骤4：模型初始化
            InitializeModel();
            
            // 步骤5：训练循环
            for (int epoch = 1; epoch <= GetEpochs(); epoch++)
            {
                TrainEpoch(trainSet, epoch);
                
                if (ShouldValidate(epoch))
                {
                    Validate(valSet, epoch);
                }
                
                if (ShouldEarlySto())
                {
                    Console.WriteLine("  早停机制触发，停止训练");
                    break;
                }
            }
            
            // 步骤6：模型评估
            Evaluate(testSet);
            
            // 步骤7：保存模型
            if (ShouldSaveModel())
            {
                SaveModel();
            }
            
            Console.WriteLine($"{_modelName} 训练完成\n");
        }

        // 抽象方法 - 必须由子类实现
        protected abstract object LoadData();
        protected abstract object PreprocessData(object data);
        protected abstract object ExtractFeatures(object data);
        protected abstract void InitializeModel();
        protected abstract void TrainEpoch(object trainData, int epoch);
        protected abstract void Evaluate(object testData);

        // 虚方法 - 可被重写
        protected virtual (object, object, object) SplitData(object data)
        {
            Console.WriteLine("  分割数据: 70% 训练, 15% 验证, 15% 测试");
            return (data, data, data);
        }

        protected virtual void Validate(object valData, int epoch)
        {
            Console.WriteLine($"  Epoch {epoch}: 验证模型性能");
        }

        protected virtual bool ShouldValidate(int epoch)
        {
            return epoch % 5 == 0;
        }

        protected virtual bool ShouldEarlySto()
        {
            return false;
        }

        protected virtual bool ShouldSaveModel()
        {
            return true;
        }

        protected virtual void SaveModel()
        {
            Console.WriteLine($"  保存模型: {_modelName}.model");
        }

        protected virtual int GetEpochs()
        {
            return 10;
        }
    }

    // GPT模型训练器
    public class GPTModelTrainer : AIModelTrainer
    {
        private double _learningRate;
        private int _batchSize;

        public GPTModelTrainer() : base("GPT-Custom")
        {
            _learningRate = 0.001;
            _batchSize = 32;
        }

        protected override object LoadData()
        {
            Console.WriteLine("  加载文本语料库...");
            return "大规模文本数据";
        }

        protected override object PreprocessData(object data)
        {
            Console.WriteLine("  文本预处理: 分词、编码、填充");
            return data;
        }

        protected override object ExtractFeatures(object data)
        {
            Console.WriteLine("  提取特征: Word Embeddings, Position Encoding");
            return data;
        }

        protected override void InitializeModel()
        {
            Console.WriteLine("  初始化Transformer架构");
            Console.WriteLine($"    - 学习率: {_learningRate}");
            Console.WriteLine($"    - 批次大小: {_batchSize}");
            Console.WriteLine("    - 注意力头: 12");
            Console.WriteLine("    - 隐藏层: 768");
        }

        protected override void TrainEpoch(object trainData, int epoch)
        {
            Console.WriteLine($"  Epoch {epoch}: 训练中...");
            Console.WriteLine($"    Loss: {0.5 - epoch * 0.03:F3}, Perplexity: {Math.Exp(2.5 - epoch * 0.1):F2}");
        }

        protected override void Evaluate(object testData)
        {
            Console.WriteLine("  评估模型:");
            Console.WriteLine("    - BLEU Score: 0.85");
            Console.WriteLine("    - Perplexity: 12.5");
        }

        protected override int GetEpochs()
        {
            return 15;
        }
    }

    // 图像分类模型训练器
    public class CNNModelTrainer : AIModelTrainer
    {
        public CNNModelTrainer() : base("ResNet-50")
        {
        }

        protected override object LoadData()
        {
            Console.WriteLine("  加载图像数据集 (ImageNet)...");
            return "图像数据";
        }

        protected override object PreprocessData(object data)
        {
            Console.WriteLine("  图像预处理: 缩放、归一化、数据增强");
            return data;
        }

        protected override object ExtractFeatures(object data)
        {
            Console.WriteLine("  提取特征: 卷积特征图");
            return data;
        }

        protected override void InitializeModel()
        {
            Console.WriteLine("  初始化ResNet-50架构");
            Console.WriteLine("    - 卷积层: 50层");
            Console.WriteLine("    - 优化器: Adam");
            Console.WriteLine("    - 损失函数: CrossEntropy");
        }

        protected override void TrainEpoch(object trainData, int epoch)
        {
            Console.WriteLine($"  Epoch {epoch}: 训练中...");
            Console.WriteLine($"    Loss: {1.5 - epoch * 0.1:F3}, Accuracy: {60 + epoch * 3}%");
        }

        protected override void Evaluate(object testData)
        {
            Console.WriteLine("  评估模型:");
            Console.WriteLine("    - Top-1 Accuracy: 92.5%");
            Console.WriteLine("    - Top-5 Accuracy: 98.2%");
        }

        protected override bool ShouldValidate(int epoch)
        {
            return epoch % 2 == 0;
        }
    }

    // 推荐系统模型训练器
    public class RecommenderModelTrainer : AIModelTrainer
    {
        public RecommenderModelTrainer() : base("协同过滤推荐模型")
        {
        }

        protected override object LoadData()
        {
            Console.WriteLine("  加载用户行为数据...");
            return "用户-物品交互矩阵";
        }

        protected override object PreprocessData(object data)
        {
            Console.WriteLine("  数据预处理: 缺失值填充、归一化");
            return data;
        }

        protected override object ExtractFeatures(object data)
        {
            Console.WriteLine("  提取特征: 用户嵌入、物品嵌入");
            return data;
        }

        protected override void InitializeModel()
        {
            Console.WriteLine("  初始化矩阵分解模型");
            Console.WriteLine("    - 嵌入维度: 128");
            Console.WriteLine("    - 正则化系数: 0.01");
        }

        protected override void TrainEpoch(object trainData, int epoch)
        {
            Console.WriteLine($"  Epoch {epoch}: 训练中...");
            Console.WriteLine($"    RMSE: {1.2 - epoch * 0.05:F3}, MAE: {0.9 - epoch * 0.03:F3}");
        }

        protected override void Evaluate(object testData)
        {
            Console.WriteLine("  评估模型:");
            Console.WriteLine("    - Precision@10: 0.75");
            Console.WriteLine("    - Recall@10: 0.82");
            Console.WriteLine("    - NDCG@10: 0.88");
        }

        protected override bool ShouldEarlySto()
        {
            // 模拟早停条件
            return false;
        }
    }
}
