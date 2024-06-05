namespace apbd10.ResponseModels;

public class AddProductRequestModel
{
    
    public string ProductName { get; set; }

    
    public decimal ProductWeight { get; set; }

    
    public decimal ProductWidth { get; set; }

    
    public decimal ProductHeight { get; set; }

    
    public decimal ProductDepth { get; set; }

    
    public List<int> ProductCategories { get; set; }
}