namespace Core.Application;

public class ProductDto
{
    public long Id { get; set; }
    public string Title { get;private set; }
    public int InventoryCount { get; set; }
    public long Price { get; set; }
    public double ProperPrice => (Price - (Price * Discount / 100));
    public double Discount { get; set; }

    public ProductDto(string title)
    {
        if (title.Length > 40)
            throw new Exception("title must be less than 40");
        Title = title;
        InventoryCount = 1;
    }

    public void IncreseInventoryCount(int inventoryCount)
    {
        if(inventoryCount<1)
            throw new Exception("inventoryCount must be bigger than 0");

        InventoryCount += inventoryCount;
    }
}
