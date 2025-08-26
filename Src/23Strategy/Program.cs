namespace _23Strategy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 策略模式 (Strategy Pattern) ================================");
            Console.WriteLine("适用场景：有多种算法可以完成同一个任务，需要在运行时选择；需要避免多重条件判断");
            Console.WriteLine("特点：定义一系列算法，把它们封装起来，并使它们可以相互替换");
            Console.WriteLine("优点：算法可以自由切换；避免多重条件判断；扩展性良好\n");

            Console.WriteLine("-------------------------------- 支付策略系统 ----------------------------------");
            
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddItem("笔记本电脑", 5000);
            shoppingCart.AddItem("无线鼠标", 100);
            shoppingCart.AddItem("键盘", 300);
            
            Console.WriteLine("购物车商品总额: ¥5400\n");
            
            Console.WriteLine("1. 信用卡支付：");
            shoppingCart.SetPaymentStrategy(new CreditCardPayment("1234-5678-9012-3456"));
            shoppingCart.Checkout();
            
            Console.WriteLine("\n2. 支付宝支付：");
            shoppingCart.SetPaymentStrategy(new AlipayPayment("user@example.com"));
            shoppingCart.Checkout();
            
            Console.WriteLine("\n3. 微信支付：");
            shoppingCart.SetPaymentStrategy(new WeChatPayment("13800138000"));
            shoppingCart.Checkout();

            Console.WriteLine("\n-------------------------------- 数据排序策略 ----------------------------------");
            
            var sorter = new DataSorter();
            
            Console.WriteLine("原始数据: [64, 34, 25, 12, 22, 11, 90]");
            
            Console.WriteLine("\n1. 冒泡排序：");
            int[] data1 = { 64, 34, 25, 12, 22, 11, 90 };
            sorter.SetStrategy(new BubbleSortStrategy());
            sorter.Sort(data1);
            
            Console.WriteLine("\n2. 快速排序：");
            int[] data2 = { 64, 34, 25, 12, 22, 11, 90 };
            sorter.SetStrategy(new QuickSortStrategy());
            sorter.Sort(data2);
            
            Console.WriteLine("\n3. 归并排序：");
            int[] data3 = { 64, 34, 25, 12, 22, 11, 90 };
            sorter.SetStrategy(new MergeSortStrategy());
            sorter.Sort(data3);
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 策略类封装了具体的算法实现");
            Console.WriteLine("- 客户端可以在运行时切换策略");
            Console.WriteLine("- 新增策略不需要修改现有代码");
        }
    }
}
