using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd10.Models;

[Table("Shopping_Carts")]
public class ShoppingCart
{
    [ForeignKey("Account")]
    [Column("FK_account")]
    public int AccountId { get; set; }
    
    [ForeignKey("Product")]
    [Column("FK_product")]
    public int ProductId { get; set; }
    
    [Column("amount")]
    public int Amount { get; set; }
    
    public Account Account { get; set; }
    public Product Product { get; set; }
}