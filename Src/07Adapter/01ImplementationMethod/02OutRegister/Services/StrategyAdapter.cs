using _07Adapter._01ImplementationMethod._02OutRegister.IServices;

namespace _07Adapter._01ImplementationMethod._02OutRegister.Services
{
    // 策略适配器类
    public class StrategyAdapter<TInput, TOutput> : IAdapter<TInput, TOutput>
    {
        private readonly IAdaptationStrategy<TInput, TOutput> _strategy;

        public StrategyAdapter(IAdaptationStrategy<TInput, TOutput> strategy)
        {
            _strategy = strategy;
        }

        public TOutput Adapt(TInput input)
        {
            return _strategy.Adapt(input);
        }
    }
}
