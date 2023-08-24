namespace Core.Domain;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();
}
