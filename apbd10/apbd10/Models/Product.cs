using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd10.Models;

[Table("Products")]
public class Product
{
    [Key]
    [Column("PK_product")]
    public int ProductId { get; set; }
    
    [Column("name")]
    [MaxLength(100)]
    public string ProductName { get; set; }
    
    public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
    public IEnumerable<Product_Categories> ProductCategoriesEnumerable { get; set; }
    
    [Column("weight")]
    [Range(0,5.2)]
    public decimal Weight { get; set; }
    
    [Column("width")]
    [Range(0,5.2)]
    public decimal Width { get; set; }
    
    [Column("height")]
    [Range(0,5.2)]
    public decimal Height { get; set; }
    
    [Column("depth")]
    [Range(0,5.2)]
    public decimal Depth { get; set; }
}