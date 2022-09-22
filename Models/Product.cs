namespace MySimpleNetApi.Models;

public class Product
{
    public string Id { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;

    public string CategoryId { get; set; }
    public Category Category { get; set; }
}