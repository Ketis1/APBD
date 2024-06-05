using apbd10.Contexts;
using apbd10.Models;
using apbd10.ResponseModels;

namespace apbd10.Services;

public interface IProductService
{
    Task AddProductAsync(AddProductRequestModel addProductRequestModel);
}

public class ProductService(DatabaseContext context) : IProductService
{
    public async Task AddProductAsync(AddProductRequestModel addProductRequestModel)
    {
        var newProduct = new Product
        {
            ProductName = addProductRequestModel.ProductName,
            Weight = addProductRequestModel.ProductWeight,
            Width = addProductRequestModel.ProductWeight,
            Height = addProductRequestModel.ProductWeight,
            Depth = addProductRequestModel.ProductDepth,
            ProductCategoriesEnumerable = addProductRequestModel.ProductCategories.Select(pc => new Product_Categories
            {
                PC_id = pc
            }).ToList()

        };
        context.Products.Add(newProduct);
        await context.SaveChangesAsync();
    }
}