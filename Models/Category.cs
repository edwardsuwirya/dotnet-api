namespace MySimpleNetApi.Models;

public class Category
{
    public string Id { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;

    public Category()
    {
    }

    public Category(string id, string categoryName)
    {
        Id = id;
        CategoryName = categoryName;
    }
}