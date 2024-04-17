using _07Adapter._01ImplementationMethod._02OutRegister.IServices;

namespace _07Adapter._01ImplementationMethod._02OutRegister.Services
{
    // 通用适配器类
    public class GenericAdapter<TInput, TOutput> : IAdapter<TInput, TOutput>
    {
        private readonly Func<TInput, TOutput> _adaptFunction;

        public GenericAdapter(Func<TInput, TOutput> adaptFunction)
        {
            _adaptFunction = adaptFunction;
        }

        public TOutput Adapt(TInput input)
        {
            return _adaptFunction(input);
        }
    }
}
