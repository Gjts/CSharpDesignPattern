namespace _07Adapter._01ImplementationMethod._02OutRegister.IServices
{
    // 适配器工厂接口
    public interface IAdapterFactory
    {
        TOutput DetermineResult<TInput, TOutput>(TInput inputValue);
        IAdapter<TInput, TOutput> CreateAdapter<TInput, TOutput, TParam>(TParam input);
        IAdapter<TInput, TOutput> CreateAdapter<TInput, TOutput>();
        void RegisterAdapter<TInput, TOutput>(Func<TInput, TOutput> adaptFunction);
        void RegisterStrategy<TInput, TOutput>(IAdaptationStrategy<TInput, TOutput> strategy);
        void RegisterStrategy<TInput, TOutput, TParam>(IAdaptationStrategy<TInput, TOutput> strategy);
    }
}
