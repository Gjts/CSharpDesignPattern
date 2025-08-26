using _15Template.Example.AITraining;

namespace _15Template
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 模板方法模式 (Template Method Pattern) ================================");
            Console.WriteLine("适用场景：多个类有相似的算法结构，但某些步骤的实现不同；需要控制子类扩展的场景");
            Console.WriteLine("特点：定义算法骨架，将某些步骤延迟到子类实现，子类可以重定义某些步骤而不改变算法结构");
            Console.WriteLine("优点：封装不变部分，扩展可变部分；提取公共代码，便于维护；控制子类扩展\n");

            Console.WriteLine("-------------------------------- AI模型训练流程 ----------------------------------");
            
            Console.WriteLine("1. GPT语言模型训练：");
            Console.WriteLine("------------------------");
            var gptTrainer = new GPTModelTrainer();
            gptTrainer.Train();
            
            Console.WriteLine("2. ResNet图像分类模型训练：");
            Console.WriteLine("------------------------");
            var cnnTrainer = new CNNModelTrainer();
            cnnTrainer.Train();
            
            Console.WriteLine("3. 协同过滤推荐系统训练：");
            Console.WriteLine("------------------------");
            var recommenderTrainer = new RecommenderModelTrainer();
            recommenderTrainer.Train();
            
            Console.WriteLine("说明：");
            Console.WriteLine("- Train()方法定义了训练流程的模板");
            Console.WriteLine("- 各个具体训练器实现自己的数据加载、预处理、训练等步骤");
            Console.WriteLine("- 训练流程的整体结构保持不变，细节可以变化");
        }
    }
}
