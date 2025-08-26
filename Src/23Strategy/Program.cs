namespace _23Strategy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 策略模式 (Strategy Pattern) ===\n");

            Console.WriteLine("示例1：支付策略");
            Console.WriteLine("------------------------");
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddItem("笔记本电脑", 5000);
            shoppingCart.AddItem("鼠标", 100);
            
            shoppingCart.SetPaymentStrategy(new CreditCardPayment("1234-5678-9012-3456"));
            shoppingCart.Checkout();
            
            shoppingCart.SetPaymentStrategy(new AlipayPayment("user@example.com"));
            shoppingCart.Checkout();
            
            shoppingCart.SetPaymentStrategy(new WeChatPayment("13800138000"));
            shoppingCart.Checkout();

            Console.WriteLine("\n示例2：排序策略");
            Console.WriteLine("------------------------");
            var sorter = new DataSorter();
            int[] data = { 64, 34, 25, 12, 22, 11, 90 };
            
            sorter.SetStrategy(new BubbleSortStrategy());
            sorter.Sort(data);
            
            data = new int[] { 64, 34, 25, 12, 22, 11, 90 };
            sorter.SetStrategy(new QuickSortStrategy());
            sorter.Sort(data);
            
            data = new int[] { 64, 34, 25, 12, 22, 11, 90 };
            sorter.SetStrategy(new MergeSortStrategy());
            sorter.Sort(data);
        }
    }
}
