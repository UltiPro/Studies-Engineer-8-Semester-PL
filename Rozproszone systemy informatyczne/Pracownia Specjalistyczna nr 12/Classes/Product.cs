namespace WebApplication1.Classes;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }

    public Product(int Id, string Name, string Description, int Price)
    {
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
        this.Price = Price;
    }
}