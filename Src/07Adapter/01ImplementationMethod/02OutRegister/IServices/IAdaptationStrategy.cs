namespace _07Adapter._01ImplementationMethod._02OutRegister.IServices
{
    // 定义策略接口：首先定义一个策略接口，包含一个执行策略的方法。
    public interface IAdaptationStrategy<TInput, TOutput>
    {
        TOutput Adapt(TInput input);
    }
}
