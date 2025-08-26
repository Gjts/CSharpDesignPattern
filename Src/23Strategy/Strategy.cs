namespace _23Strategy
{
    // 支付策略接口
    public interface IPaymentStrategy
    {
        void Pay(decimal amount);
    }

    // 信用卡支付
    public class CreditCardPayment : IPaymentStrategy
    {
        private string _cardNumber;

        public CreditCardPayment(string cardNumber)
        {
            _cardNumber = cardNumber;
        }

        public void Pay(decimal amount)
        {
            Console.WriteLine($"使用信用卡 {_cardNumber} 支付 ¥{amount:F2}");
            Console.WriteLine("正在连接银行...");
            Console.WriteLine("支付成功！");
        }
    }

    // 支付宝支付
    public class AlipayPayment : IPaymentStrategy
    {
        private string _account;

        public AlipayPayment(string account)
        {
            _account = account;
        }

        public void Pay(decimal amount)
        {
            Console.WriteLine($"使用支付宝账号 {_account} 支付 ¥{amount:F2}");
            Console.WriteLine("跳转到支付宝...");
            Console.WriteLine("扫码支付成功！");
        }
    }

    // 微信支付
    public class WeChatPayment : IPaymentStrategy
    {
        private string _phone;

        public WeChatPayment(string phone)
        {
            _phone = phone;
        }

        public void Pay(decimal amount)
        {
            Console.WriteLine($"使用微信 {_phone} 支付 ¥{amount:F2}");
            Console.WriteLine("打开微信支付...");
            Console.WriteLine("指纹验证成功，支付完成！");
        }
    }

    // 购物车
    public class ShoppingCart
    {
        private List<(string name, decimal price)> _items = new();
        private IPaymentStrategy? _paymentStrategy;

        public void AddItem(string name, decimal price)
        {
            _items.Add((name, price));
        }

        public void SetPaymentStrategy(IPaymentStrategy strategy)
        {
            _paymentStrategy = strategy;
        }

        public void Checkout()
        {
            decimal total = _items.Sum(item => item.price);
            Console.WriteLine($"\n订单总额: ¥{total:F2}");
            
            if (_paymentStrategy != null)
            {
                _paymentStrategy.Pay(total);
            }
            else
            {
                Console.WriteLine("请选择支付方式");
            }
        }
    }

    // 排序策略接口
    public interface ISortStrategy
    {
        void Sort(int[] array);
    }

    // 冒泡排序
    public class BubbleSortStrategy : ISortStrategy
    {
        public void Sort(int[] array)
        {
            Console.WriteLine("使用冒泡排序...");
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
            Console.WriteLine($"排序结果: {string.Join(", ", array)}");
        }
    }

    // 快速排序
    public class QuickSortStrategy : ISortStrategy
    {
        public void Sort(int[] array)
        {
            Console.WriteLine("使用快速排序...");
            QuickSort(array, 0, array.Length - 1);
            Console.WriteLine($"排序结果: {string.Join(", ", array)}");
        }

        private void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right);
                QuickSort(array, left, pivot - 1);
                QuickSort(array, pivot + 1, right);
            }
        }

        private int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;
            
            for (int j = left; j < right; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            
            int temp2 = array[i + 1];
            array[i + 1] = array[right];
            array[right] = temp2;
            
            return i + 1;
        }
    }

    // 归并排序
    public class MergeSortStrategy : ISortStrategy
    {
        public void Sort(int[] array)
        {
            Console.WriteLine("使用归并排序...");
            MergeSort(array, 0, array.Length - 1);
            Console.WriteLine($"排序结果: {string.Join(", ", array)}");
        }

        private void MergeSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                MergeSort(array, left, mid);
                MergeSort(array, mid + 1, right);
                Merge(array, left, mid, right);
            }
        }

        private void Merge(int[] array, int left, int mid, int right)
        {
            int[] temp = new int[right - left + 1];
            int i = left, j = mid + 1, k = 0;
            
            while (i <= mid && j <= right)
            {
                if (array[i] <= array[j])
                {
                    temp[k++] = array[i++];
                }
                else
                {
                    temp[k++] = array[j++];
                }
            }
            
            while (i <= mid)
            {
                temp[k++] = array[i++];
            }
            
            while (j <= right)
            {
                temp[k++] = array[j++];
            }
            
            for (i = 0; i < temp.Length; i++)
            {
                array[left + i] = temp[i];
            }
        }
    }

    // 数据排序器
    public class DataSorter
    {
        private ISortStrategy? _strategy;

        public void SetStrategy(ISortStrategy strategy)
        {
            _strategy = strategy;
        }

        public void Sort(int[] data)
        {
            if (_strategy != null)
            {
                _strategy.Sort(data);
            }
            else
            {
                Console.WriteLine("请选择排序策略");
            }
        }
    }
}

