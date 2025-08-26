namespace _15Template.Example.AITraining
{
    public class AITrainingExample
    {
        public static void Run()
        {
            Console.WriteLine("\n=== AI模型训练流程示例 ===");
            
            // GPT语言模型训练
            Console.WriteLine("\n1. GPT语言模型训练:");
            Console.WriteLine("------------------------");
            var gptTrainer = new GPTModelTrainer();
            gptTrainer.Train();

            // CNN图像分类模型训练
            Console.WriteLine("2. ResNet图像分类模型训练:");
            Console.WriteLine("------------------------");
            var cnnTrainer = new CNNModelTrainer();
            cnnTrainer.Train();

            // 推荐系统模型训练
            Console.WriteLine("3. 协同过滤推荐系统训练:");
            Console.WriteLine("------------------------");
            var recommenderTrainer = new RecommenderModelTrainer();
            recommenderTrainer.Train();
        }
    }
}
