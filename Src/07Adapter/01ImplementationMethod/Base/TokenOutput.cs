namespace _07Adapter._01ImplementationMethod.Base
{
    public class TokenOutput
    {
        /// <summary>
        /// Token Value
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiryTime;
    }
}
