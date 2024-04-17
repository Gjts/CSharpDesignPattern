using _07Adapter._01ImplementationMethod._01InnetRegister.IServices;
using _07Adapter._01ImplementationMethod.Base;

namespace _07Adapter._01ImplementationMethod._01InnetRegister.Business
{
    public class UpdateApiStrategy : IApiStrategy
    {
        public async Task<IOutput> ExecuteAsync(string? token, IInput input)
        {
            await Task.Run(() => { });
            return new CreateLableOutput();
        }
    }
}