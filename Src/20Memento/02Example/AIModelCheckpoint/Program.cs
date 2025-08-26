namespace _20Memento.Example.AIModelCheckpoint
{
    public class AICheckpointExample
    {
        public static void Run()
        {
            Console.WriteLine("\n=== AI模型检查点示例 ===");
            
            var model = new AIModel("ResNet-50");
            var checkpointManager = new CheckpointManager();
            
            // 训练过程
            Console.WriteLine("\n开始训练:");
            Console.WriteLine("------------------------");
            
            for (int i = 1; i <= 10; i++)
            {
                model.Train();
                
                // 每3个epoch自动保存
                if (i % 3 == 0)
                {
                    checkpointManager.AutoSave(model.CreateCheckpoint($"自动保存 Epoch {i}"));
                }
                
                // 在特定epoch创建命名检查点
                if (i == 5)
                {
                    checkpointManager.SaveCheckpoint("mid_training", model.CreateCheckpoint("训练中期"));
                }
                
                if (i == 7)
                {
                    // 模拟训练出现问题，调整超参数
                    Console.WriteLine("\n⚠️ 检测到过拟合，调整学习率...");
                    model.SetHyperparameter("learning_rate", 0.0001);
                    checkpointManager.SaveCheckpoint("before_lr_change", model.CreateCheckpoint("学习率调整前"));
                }
            }
            
            // 保存最终模型
            checkpointManager.SaveCheckpoint("final", model.CreateCheckpoint("训练完成"));
            
            // 显示当前状态
            model.DisplayState();
            
            // 列出所有检查点
            checkpointManager.ListCheckpoints();
            
            // 恢复到中期检查点
            Console.WriteLine("\n恢复到训练中期检查点:");
            var midCheckpoint = checkpointManager.LoadCheckpoint("mid_training");
            model.RestoreFromCheckpoint(midCheckpoint);
            model.DisplayState();
            
            // 找到并恢复最佳检查点
            Console.WriteLine("\n寻找最佳检查点:");
            var bestCheckpoint = checkpointManager.FindBestCheckpoint();
            model.RestoreFromCheckpoint(bestCheckpoint);
            model.DisplayState();
        }
    }
}

