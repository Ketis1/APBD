using System.ComponentModel.DataAnnotations;

namespace apbd6solution.Models;

public class GetProduct
{
    [Required]
    public int IdProduct { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
}