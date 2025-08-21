using _07Adapter._01ImplementationMethod._02OutRegister.IServices;
using System.Collections.Concurrent;
using static _07Adapter.Program;

namespace _07Adapter._01ImplementationMethod._02OutRegister.Services
{
    // 适配器工厂类
    public class AdapterFactory : IAdapterFactory
    {
        // 不同类型适配存储
        private readonly ConcurrentDictionary<(Type, Type), Func<object, object>> _adapters = new ConcurrentDictionary<(Type, Type), Func<object, object>>();
        private readonly ConcurrentDictionary<(Type, Type), object> _strategies = new ConcurrentDictionary<(Type, Type), object>();

        // 相同类型适配存储
        private readonly ConcurrentDictionary<(Type, Type, Type), Func<object, object>> _sameadapters = new ConcurrentDictionary<(Type, Type, Type), Func<object, object>>();
        private readonly ConcurrentDictionary<(Type, Type, Type), object> _samestrategies = new ConcurrentDictionary<(Type, Type, Type), object>();

        public void RegisterAdapter<TInput, TOutput>(Func<TInput, TOutput> adaptFunction)
        {
            if (adaptFunction == null)
            {
                throw new ArgumentNullException(nameof(adaptFunction));
            }

            var key = (typeof(TInput), typeof(TOutput));
            _adapters[key] = input =>
            {
                if (input == null)
                {
                    // 根据需求进行处理，例如返回null或抛出异常
                    throw new ArgumentNullException(nameof(input));
                }
                return adaptFunction((TInput)input) ?? throw new InvalidOperationException("适配函数返回了 null");
            };
        }

        public void RegisterStrategy<TInput, TOutput>(IAdaptationStrategy<TInput, TOutput> strategy)
        {
            var key = (typeof(TInput), typeof(TOutput));
            _strategies[key] = strategy;
        }

        public void RegisterStrategy<TInput, TOutput, TParam>(IAdaptationStrategy<TInput, TOutput> strategy)
        {
            var key = (typeof(TInput), typeof(TOutput), typeof(TParam));
            _samestrategies[key] = strategy;
        }

        public IAdapter<TInput, TOutput>? CreateAdapter<TInput, TOutput, TParam>(TParam input)
        {
            var key = (typeof(TInput), typeof(TOutput), typeof(TParam));
            if (_sameadapters.TryGetValue(key, out var adaptFunction))
            {
                return new GenericAdapter<TInput, TOutput>(input =>
                {
                    if (input == null)
                    {
                        // 根据需求进行处理，例如返回null或抛出异常
                        throw new ArgumentNullException(nameof(input));
                    }
                    return (TOutput)adaptFunction(input);
                });
            }
            else if (_samestrategies.TryGetValue(key, out var strategy))
            {
                return new StrategyAdapter<TInput, TOutput>((IAdaptationStrategy<TInput, TOutput>)strategy);
            }
            else
            {
                return null; // 返回 null 而不是抛出异常
            }
        }

        public IAdapter<TInput, TOutput>? CreateAdapter<TInput, TOutput>()
        {
            var key = (typeof(TInput), typeof(TOutput));
            if (_adapters.TryGetValue(key, out var adaptFunction))
            {
                return new GenericAdapter<TInput, TOutput>(input =>
                {
                    if (input == null)
                    {
                        // 根据需求进行处理，例如返回null或抛出异常
                        throw new ArgumentNullException(nameof(input));
                    }
                    return (TOutput)adaptFunction(input);
                });
            }
            else if (_strategies.TryGetValue(key, out var strategy))
            {
                return new StrategyAdapter<TInput, TOutput>((IAdaptationStrategy<TInput, TOutput>)strategy);
            }
            else
            {
                return null; // 返回 null 而不是抛出异常
            }
        }

        public TOutput? DetermineResult<TInput, TOutput>(TInput inputValue)
        {
            if (typeof(TInput) == typeof(decimal) && typeof(TOutput) == typeof(string))
            {
                decimal amount = Convert.ToDecimal(inputValue);

                // 根据金额大小来返回不同的字符串结果
                return (TOutput)(object)(amount < 100 ? "Amount is less than 100" : "Amount is greater than or equal to 100");
            }
            else if (typeof(TInput) == typeof(string) && typeof(TOutput) == typeof(int))
            {
                string? inputString = Convert.ToString(inputValue);

                // 根据字符串内容来返回字符串长度
                return (TOutput)(object)(inputString?.Length ?? 0);
            }
            else
            {
                // 其他类型的判断逻辑
                return default(TOutput);
            }
        }


    }
}
