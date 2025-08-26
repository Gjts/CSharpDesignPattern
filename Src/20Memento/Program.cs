using _20Memento.Example.AIModelCheckpoint;

namespace _20Memento
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 备忘录模式 (Memento Pattern) ================================");
            Console.WriteLine("适用场景：需要保存对象的历史状态以支持撤销/恢复操作，如编辑器、游戏存档、事务回滚等");
            Console.WriteLine("特点：在不破坏封装性的前提下，捕获并外部化对象的内部状态，以便以后恢复");
            Console.WriteLine("优点：提供了状态恢复机制，实现了信息的封装，简化了发起人类\n");

            Console.WriteLine("-------------------------------- AI模型训练检查点示例 ----------------------------------");
            
            // 创建AI模型和检查点管理器
            var model = new AIModel("ResNet-50");
            var checkpointManager = new CheckpointManager();
            
            Console.WriteLine("1. 开始模型训练：");
            Console.WriteLine("--------------------------------");
            
            // 训练过程中创建检查点
            for (int epoch = 1; epoch <= 10; epoch++)
            {
                model.Train();
                
                // 每3个epoch自动保存
                if (epoch % 3 == 0)
                {
                    var autoCheckpoint = model.CreateCheckpoint($"自动保存 Epoch {epoch}");
                    checkpointManager.AutoSave(autoCheckpoint);
                }
                
                // 在特定epoch创建命名检查点
                if (epoch == 5)
                {
                    var midCheckpoint = model.CreateCheckpoint("训练中期");
                    checkpointManager.SaveCheckpoint("mid_training", midCheckpoint);
                }
                
                // 模拟训练问题，需要调整超参数
                if (epoch == 7)
                {
                    Console.WriteLine("\n⚠️ 检测到过拟合，调整学习率...");
                    var beforeChange = model.CreateCheckpoint("学习率调整前");
                    checkpointManager.SaveCheckpoint("before_lr_change", beforeChange);
                    model.SetHyperparameter("learning_rate", 0.0001);
                }
            }
            
            // 保存最终模型
            var finalCheckpoint = model.CreateCheckpoint("训练完成");
            checkpointManager.SaveCheckpoint("final", finalCheckpoint);
            
            // 显示当前状态
            Console.WriteLine("\n2. 当前模型状态：");
            Console.WriteLine("--------------------------------");
            model.DisplayState();
            
            // 列出所有检查点
            Console.WriteLine("\n3. 可用检查点列表：");
            Console.WriteLine("--------------------------------");
            checkpointManager.ListCheckpoints();
            
            // 恢复到中期检查点
            Console.WriteLine("\n4. 状态恢复演示：");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("恢复到训练中期检查点：");
            var midPoint = checkpointManager.LoadCheckpoint("mid_training");
            model.RestoreFromCheckpoint(midPoint);
            model.DisplayState();
            
            // 找到并恢复最佳检查点
            Console.WriteLine("\n5. 自动选择最佳检查点：");
            Console.WriteLine("--------------------------------");
            var bestCheckpoint = checkpointManager.FindBestCheckpoint();
            if (bestCheckpoint != null)
            {
                model.RestoreFromCheckpoint(bestCheckpoint);
                model.DisplayState();
            }
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 检查点保存了模型的完整状态（权重、超参数、训练进度等）");
            Console.WriteLine("- 支持命名检查点和自动保存机制");
            Console.WriteLine("- 可以随时恢复到任意历史状态，用于调试和优化");
        }
    }
}
