namespace _15Template
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 模板方法模式 (Template Method Pattern) ===\n");
            Console.WriteLine("实际案例：AI模型训练流程\n");

            // 示例1：GPT语言模型训练
            Console.WriteLine("示例1：GPT语言模型训练");
            Console.WriteLine("------------------------");
            var gptTrainer = new GPTModelTrainer();
            gptTrainer.Train();

            // 示例2：CNN图像分类模型训练
            Console.WriteLine("示例2：ResNet图像分类模型训练");
            Console.WriteLine("------------------------");
            var cnnTrainer = new CNNModelTrainer();
            cnnTrainer.Train();

            // 示例3：推荐系统模型训练
            Console.WriteLine("示例3：协同过滤推荐系统训练");
            Console.WriteLine("------------------------");
            var recommenderTrainer = new RecommenderModelTrainer();
            recommenderTrainer.Train();
        }
    }
}