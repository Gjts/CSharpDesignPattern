using _07Adapter._01ImplementationMethod._01InnetRegister.IServices;
using _07Adapter._01ImplementationMethod.Base;

namespace _07Adapter._01ImplementationMethod._01InnetRegister.Business
{
    public class TokenService : ITokenService
    {
        private static TokenOutput? _token;

        private readonly SemaphoreSlim _tokenSemaphore = new SemaphoreSlim(1, 1);

        public async Task<string?> GetTokenAsync()
        {
            await _tokenSemaphore.WaitAsync();
            try
            {
                if (_token?.AccessToken == null || DateTime.Now >= _token?.ExpiryTime)
                {
                    _token = await RequestCATLTokenAsync();
                    // 假设Token有效期为1小时
                    _token.ExpiryTime = DateTime.Now.AddHours(1);
                }

                return _token?.AccessToken;
            }
            finally
            {
                _tokenSemaphore.Release();
            }
        }


        private async Task<TokenOutput> RequestCATLTokenAsync()
        {
            await Task.Run(() => { });
            return new TokenOutput { };
        }
    }
}
