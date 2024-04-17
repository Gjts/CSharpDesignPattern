namespace _06Prototype._02Example.VirtualMachine
{
    // 虚拟机/容器接口
    public interface IPrototype<T>
    {
        T Clone();
    }
}
