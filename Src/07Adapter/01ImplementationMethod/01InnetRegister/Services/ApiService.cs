using _07Adapter._01ImplementationMethod._01InnetRegister.Business;
using _07Adapter._01ImplementationMethod._01InnetRegister.IServices;
using _07Adapter._01ImplementationMethod.Base;

namespace _07Adapter._01ImplementationMethod._01InnetRegister.Services
{
    public class ApiService : IApiService
    {
        private readonly ITokenService _tokenService;
        private readonly ApiStrategyFactory _strategyFactory;

        public ApiService(ITokenService tokenService, ApiStrategyFactory strategyFactory)
        {
            _tokenService = tokenService;
            _strategyFactory = strategyFactory;

            // 在构造函数中注册所有策略
            _strategyFactory.RegisterStrategy<CreateLableInput>(new CreateApiStrategy());
            _strategyFactory.RegisterStrategy<UpdateGyspcInput>(new UpdateApiStrategy());
        }


        public async Task<IOutput> CallApiAsync<TInput, TOutput>(TInput input)
            where TInput : IInput
            where TOutput : IOutput, new()
        {
            string? token = await _tokenService.GetTokenAsync();

            // 获取对应的策略并执行
            var strategy = _strategyFactory.GetStrategy<TInput>();
            return await strategy.ExecuteAsync(token, input);
        }
    }
}
