namespace _SimpleFactory._02Example._02Warehousing
{
    // 简单工厂类
    public class WarehousingFactory
    {
        public T CreateWarehousing<T>() where T : Warehousing, new()
        {
            return new T();
        }
    }
}
