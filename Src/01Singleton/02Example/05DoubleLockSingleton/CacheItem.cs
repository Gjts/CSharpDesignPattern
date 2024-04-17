namespace _Singleton._02Example._05DoubleLockSingleton
{
    // 缓存项
    public class CacheItem<T>
    {
        public T Value { get; }
        public DateTime ExpirationTime { get; }

        public CacheItem(T value, TimeSpan expirationTime)
        {
            Value = value;
            ExpirationTime = DateTime.Now.Add(expirationTime);
        }
    }
}
