namespace MySimpleNetApi.Resource;

public class ProductResponse
{
    public string Id { get; set; }
    public string ProductName { get; set; }
    public CategoryResponse Category { get; set; }
}