using _07Adapter._01ImplementationMethod.Base;

namespace _07Adapter._01ImplementationMethod._01InnetRegister.IServices
{
    public interface IApiService
    {
        Task<IOutput> CallApiAsync<TInput, TOutput>(TInput input)
            where TInput : IInput
            where TOutput : IOutput, new();
    }
}
