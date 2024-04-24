using System.ComponentModel.DataAnnotations;

namespace apbd5solution.DTOs;

public record ReplaceAnimalRequest(
    [Required]  int IdAnimal,
    [Required] [MaxLength(200)] string Name,
    [MaxLength(200)] string Description, 
    [Required] [MaxLength(200)] string Category,
    [Required] [MaxLength(200)] string Area
    );