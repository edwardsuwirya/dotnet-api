namespace MySimpleNetApi.Models;

public class Category
{
    public string Id { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new List<Product>();

    public Category()
    {
    }

    public Category(string id, string categoryName)
    {
        Id = id;
        CategoryName = categoryName;
    }
}