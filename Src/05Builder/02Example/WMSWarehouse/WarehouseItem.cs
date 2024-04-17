namespace _05Builder._02Example.WMSWarehouse
{
    public class WarehouseItem
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int Quantity { get; set; }

        public void DisplayItem()
        {
            Console.WriteLine($"Item: {Name}, Location: {Location}, Quantity: {Quantity}");
        }
    }
}
