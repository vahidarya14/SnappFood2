namespace Core.Application;

public class ProductCreateUpdateDto
{
    public string Title { get; set; }
    public int InventoryCount { get; set; }
    public long Price { get; set; }
    public double Discount { get; set; }
}
