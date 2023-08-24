namespace Core.Application;

public class IncreseInventoryCountDto
{
    public long ProductId { get; set; }
    public int InventoryCount { get; set; }
}