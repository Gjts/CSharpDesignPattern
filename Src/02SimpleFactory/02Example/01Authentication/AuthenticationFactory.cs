namespace _SimpleFactory._02Example._01Authentication
{
    // 简单工厂类
    public class AuthenticationFactory
    {
        public T CreateAuthentication<T>() where T : Authentication, new()
        {
            return new T();
        }
    }
}
