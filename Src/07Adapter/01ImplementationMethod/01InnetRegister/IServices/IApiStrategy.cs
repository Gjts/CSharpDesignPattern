using _07Adapter._01ImplementationMethod.Base;

namespace _07Adapter._01ImplementationMethod._01InnetRegister.IServices
{
    public interface IApiStrategy
    {
        Task<IOutput> ExecuteAsync(string? token, IInput input);
    }
}
