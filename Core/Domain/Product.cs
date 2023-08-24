namespace Core.Domain;

public class Product
{
    public long Id { get; set; }
    public string Title { get; set; }
    public int InventoryCount { get; set; }
    public long Price { get; set; }
    public double Discount { get; set; }

    public Product(string title)
    {
        if (title.Length > 40)
            throw new Exception("title must be less than 40");
        Title = title;
        InventoryCount = 1;
    }

    public void IncreseInventoryCount(int inventoryCount)
    {
        InventoryCount+= inventoryCount;
    }
}
