using _07Adapter._01ImplementationMethod._01InnetRegister.IServices;
using _07Adapter._01ImplementationMethod.Base;

namespace _07Adapter._01ImplementationMethod._01InnetRegister.Services
{
    public class ApiStrategyFactory
    {
        private readonly IDictionary<Type, IApiStrategy> _strategies;

        public ApiStrategyFactory()
        {
            _strategies = new Dictionary<Type, IApiStrategy>();
        }

        public void RegisterStrategy<TInput>(IApiStrategy strategy) where TInput : IInput
        {
            _strategies[typeof(TInput)] = strategy;
        }

        public IApiStrategy GetStrategy<TInput>() where TInput : IInput
        {
            if (_strategies.TryGetValue(typeof(TInput), out var strategy))
            {
                return strategy;
            }

            throw new ArgumentException("没有为给定的输入类型注册策略");
        }
    }
}
