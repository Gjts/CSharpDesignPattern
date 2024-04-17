namespace _05Builder._02Example.WMSWarehouse
{
    public interface IItemBuilder<T> where T : new()
    {
        void BuildName(string name);
        void BuildLocation(string location);
        void BuildQuantity(int quantity);
        T GetResult();
    }
}
