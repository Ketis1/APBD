using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd10.Models;

[Table("Product_Categories")]
public class Product_Categories
{
    [Key]
    [Column("Product_categoties_id")]
    public int PC_id { get; set; }
    
    [ForeignKey("Product")]
    [Column("FK_product")]
    public int AccountId { get; set; }
    
    [ForeignKey("Category")]
    [Column("FK_category")]
    public int ProductId { get; set; }
    
    public Product Product { get; set; }
    public Category Category { get; set; }
}