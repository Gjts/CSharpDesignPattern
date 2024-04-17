namespace _06Prototype._01ImplementationMethod
{
    // 原型接口
    public interface IPrototype<T>
    {
        T ShallowCopy();

        T DeepCopy();
    }
}
