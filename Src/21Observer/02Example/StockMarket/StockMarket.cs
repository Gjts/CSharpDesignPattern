namespace _21Observer._02Example.StockMarket
{
    // 观察者接口
    public interface IInvestor
    {
        void Update(Stock stock);
        string Name { get; }
    }

    // 主题接口
    public interface IStock
    {
        void Attach(IInvestor investor);
        void Detach(IInvestor investor);
        void Notify();
    }

    // 具体主题 - 股票
    public class Stock : IStock
    {
        private List<IInvestor> _investors;
        private string _symbol;
        private decimal _price;
        private decimal _previousPrice;
        private int _volume;

        public string Symbol => _symbol;
        public decimal Price => _price;
        public decimal PreviousPrice => _previousPrice;
        public int Volume => _volume;

        public Stock(string symbol, decimal initialPrice)
        {
            _symbol = symbol;
            _price = initialPrice;
            _previousPrice = initialPrice;
            _volume = 0;
            _investors = new List<IInvestor>();
        }

        public void Attach(IInvestor investor)
        {
            _investors.Add(investor);
            Console.WriteLine($"  ➕ {investor.Name} 关注了 {_symbol}");
        }

        public void Detach(IInvestor investor)
        {
            _investors.Remove(investor);
            Console.WriteLine($"  ➖ {investor.Name} 取消关注 {_symbol}");
        }

        public void Notify()
        {
            Console.WriteLine($"\n📢 {_symbol} 价格更新通知:");
            foreach (var investor in _investors)
            {
                investor.Update(this);
            }
        }

        public void SetPrice(decimal newPrice, int volume)
        {
            _previousPrice = _price;
            _price = newPrice;
            _volume = volume;
            
            decimal change = _price - _previousPrice;
            decimal changePercent = (_previousPrice != 0) ? (change / _previousPrice * 100) : 0;
            
            string trend = change > 0 ? "📈" : change < 0 ? "📉" : "➡️";
            Console.WriteLine($"\n{trend} {_symbol}: ¥{_price:F2} ({(change >= 0 ? "+" : "")}{change:F2}, {(changePercent >= 0 ? "+" : "")}{changePercent:F2}%) 成交量: {volume}");
            
            // 如果价格变化超过1%，通知所有观察者
            if (Math.Abs(changePercent) >= 1)
            {
                Notify();
            }
        }
    }

    // 具体观察者 - 个人投资者
    public class IndividualInvestor : IInvestor
    {
        private string _name;
        private Dictionary<string, decimal> _portfolio;
        private decimal _buyThreshold;
        private decimal _sellThreshold;

        public string Name => _name;

        public IndividualInvestor(string name, decimal buyThreshold = -5, decimal sellThreshold = 5)
        {
            _name = name;
            _portfolio = new Dictionary<string, decimal>();
            _buyThreshold = buyThreshold;
            _sellThreshold = sellThreshold;
        }

        public void Update(Stock stock)
        {
            decimal changePercent = ((stock.Price - stock.PreviousPrice) / stock.PreviousPrice) * 100;
            
            Console.WriteLine($"  👤 {_name} 收到 {stock.Symbol} 的更新");
            
            if (changePercent <= _buyThreshold)
            {
                Console.WriteLine($"    💰 {_name}: 价格下跌{changePercent:F2}%，考虑买入!");
                Buy(stock.Symbol, stock.Price);
            }
            else if (changePercent >= _sellThreshold)
            {
                Console.WriteLine($"    💵 {_name}: 价格上涨{changePercent:F2}%，考虑卖出!");
                Sell(stock.Symbol);
            }
            else
            {
                Console.WriteLine($"    👀 {_name}: 继续观望");
            }
        }

        private void Buy(string symbol, decimal price)
        {
            _portfolio[symbol] = price;
            Console.WriteLine($"    ✅ {_name} 买入 {symbol} @ ¥{price:F2}");
        }

        private void Sell(string symbol)
        {
            if (_portfolio.ContainsKey(symbol))
            {
                Console.WriteLine($"    ✅ {_name} 卖出 {symbol}");
                _portfolio.Remove(symbol);
            }
        }
    }

    // 具体观察者 - 机构投资者
    public class InstitutionalInvestor : IInvestor
    {
        private string _name;
        private decimal _riskTolerance;
        private List<string> _alerts;

        public string Name => _name;

        public InstitutionalInvestor(string name, decimal riskTolerance)
        {
            _name = name;
            _riskTolerance = riskTolerance;
            _alerts = new List<string>();
        }

        public void Update(Stock stock)
        {
            decimal changePercent = ((stock.Price - stock.PreviousPrice) / stock.PreviousPrice) * 100;
            
            Console.WriteLine($"  🏢 {_name} 收到 {stock.Symbol} 的更新");
            
            // 风险评估
            if (Math.Abs(changePercent) > _riskTolerance)
            {
                string alert = $"⚠️ 高风险警报: {stock.Symbol} 波动 {changePercent:F2}%";
                _alerts.Add(alert);
                Console.WriteLine($"    {alert}");
                Console.WriteLine($"    📊 {_name}: 启动风险对冲策略");
            }
            else
            {
                Console.WriteLine($"    📊 {_name}: 波动在可接受范围内");
            }
            
            // 大宗交易决策
            if (stock.Volume > 1000000)
            {
                Console.WriteLine($"    🔔 {_name}: 检测到大宗交易，分析市场趋势");
            }
        }

        public void ShowAlerts()
        {
            Console.WriteLine($"\n🚨 {_name} 的风险警报:");
            foreach (var alert in _alerts)
            {
                Console.WriteLine($"  {alert}");
            }
        }
    }

    // 具体观察者 - 算法交易系统
    public class AlgorithmicTrader : IInvestor
    {
        private string _name;
        private decimal _movingAverage;
        private List<decimal> _priceHistory;
        private int _windowSize;

        public string Name => _name;

        public AlgorithmicTrader(string name, int windowSize = 5)
        {
            _name = name;
            _windowSize = windowSize;
            _priceHistory = new List<decimal>();
            _movingAverage = 0;
        }

        public void Update(Stock stock)
        {
            Console.WriteLine($"  🤖 {_name} 收到 {stock.Symbol} 的更新");
            
            // 更新价格历史
            _priceHistory.Add(stock.Price);
            if (_priceHistory.Count > _windowSize)
            {
                _priceHistory.RemoveAt(0);
            }
            
            // 计算移动平均
            decimal oldMA = _movingAverage;
            _movingAverage = _priceHistory.Average();
            
            Console.WriteLine($"    📈 移动平均: ¥{_movingAverage:F2}");
            
            // 交易信号
            if (stock.Price > _movingAverage && oldMA > 0)
            {
                Console.WriteLine($"    🟢 {_name}: 买入信号 - 价格突破移动平均线");
                ExecuteTrade("BUY", stock.Symbol, stock.Price, 1000);
            }
            else if (stock.Price < _movingAverage && oldMA > 0)
            {
                Console.WriteLine($"    🔴 {_name}: 卖出信号 - 价格跌破移动平均线");
                ExecuteTrade("SELL", stock.Symbol, stock.Price, 1000);
            }
        }

        private void ExecuteTrade(string action, string symbol, decimal price, int quantity)
        {
            Console.WriteLine($"    ⚡ 自动执行: {action} {quantity}股 {symbol} @ ¥{price:F2}");
            Thread.Sleep(100); // 模拟交易延迟
            Console.WriteLine($"    ✅ 交易完成");
        }
    }

    // 新闻发布者 - 另一个观察者模式示例
    public interface INewsSubscriber
    {
        void ReceiveNews(string category, string headline, string content);
        string Name { get; }
    }

    public class NewsAgency
    {
        private Dictionary<string, List<INewsSubscriber>> _subscribers;
        private string _name;

        public NewsAgency(string name)
        {
            _name = name;
            _subscribers = new Dictionary<string, List<INewsSubscriber>>();
        }

        public void Subscribe(string category, INewsSubscriber subscriber)
        {
            if (!_subscribers.ContainsKey(category))
            {
                _subscribers[category] = new List<INewsSubscriber>();
            }
            
            _subscribers[category].Add(subscriber);
            Console.WriteLine($"  ➕ {subscriber.Name} 订阅了 [{category}] 新闻");
        }

        public void Unsubscribe(string category, INewsSubscriber subscriber)
        {
            if (_subscribers.ContainsKey(category))
            {
                _subscribers[category].Remove(subscriber);
                Console.WriteLine($"  ➖ {subscriber.Name} 取消订阅 [{category}] 新闻");
            }
        }

        public void PublishNews(string category, string headline, string content)
        {
            Console.WriteLine($"\n📰 {_name} 发布新闻:");
            Console.WriteLine($"  类别: {category}");
            Console.WriteLine($"  标题: {headline}");
            Console.WriteLine($"  内容: {content}");
            
            if (_subscribers.ContainsKey(category))
            {
                Console.WriteLine($"\n  通知 {category} 订阅者:");
                foreach (var subscriber in _subscribers[category])
                {
                    subscriber.ReceiveNews(category, headline, content);
                }
            }
            
            // 通知所有订阅了"全部"类别的订阅者
            if (_subscribers.ContainsKey("全部") && category != "全部")
            {
                foreach (var subscriber in _subscribers["全部"])
                {
                    subscriber.ReceiveNews(category, headline, content);
                }
            }
        }
    }

    // 新闻订阅者
    public class NewsReader : INewsSubscriber
    {
        private string _name;
        private List<string> _readingList;

        public string Name => _name;

        public NewsReader(string name)
        {
            _name = name;
            _readingList = new List<string>();
        }

        public void ReceiveNews(string category, string headline, string content)
        {
            Console.WriteLine($"    📱 {_name} 收到 [{category}] 新闻: {headline}");
            _readingList.Add($"[{category}] {headline}");
            
            // 根据类别采取不同行动
            switch (category)
            {
                case "财经":
                    Console.WriteLine($"      💹 {_name}: 关注市场动向");
                    break;
                case "科技":
                    Console.WriteLine($"      🔬 {_name}: 了解最新技术");
                    break;
                case "体育":
                    Console.WriteLine($"      ⚽ {_name}: 查看比赛结果");
                    break;
                default:
                    Console.WriteLine($"      📖 {_name}: 稍后阅读");
                    break;
            }
        }

        public void ShowReadingList()
        {
            Console.WriteLine($"\n📚 {_name} 的阅读列表:");
            foreach (var item in _readingList)
            {
                Console.WriteLine($"  • {item}");
            }
        }
    }
}