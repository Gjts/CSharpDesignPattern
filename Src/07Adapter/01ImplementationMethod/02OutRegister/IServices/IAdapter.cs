namespace _07Adapter._01ImplementationMethod._02OutRegister.IServices
{
    // 适配器的通用接口
    public interface IAdapter<TInput, TOutput>
    {
        TOutput Adapt(TInput input);
    }
}
