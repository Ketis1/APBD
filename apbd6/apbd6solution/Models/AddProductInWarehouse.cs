using System.ComponentModel.DataAnnotations;

namespace apbd6solution.Models;

public record AddProductInWarehouse
(
    [Required]
     int IdProduct,
    
    [Required] 
     int IdWarehouse,
    
    
    [Required]
    // Amount musi być większe od 0!
    [Range(1,int.MaxValue,ErrorMessage = "Ilość musi być większa od 0!")]
     int Amount,

    [Required] 
     DateTime CreatedAt
);