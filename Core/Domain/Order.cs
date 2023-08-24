namespace Core.Domain;

public class Order
{
    public long Id { get; set; }
    public long BuyerId { get; set; }
    public long ProductId { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public User Buyer { get; set; }
    public Product Product { get; set; }
}