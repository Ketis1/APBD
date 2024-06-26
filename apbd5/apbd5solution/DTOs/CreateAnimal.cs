﻿using System.ComponentModel.DataAnnotations;

namespace apbd5solution.DTOs;

public record CreateAnimalRequest(
    [Required]  int IdAnimal,
    [Required] [MaxLength(200)] string Name,
    [MaxLength(200)] string Description, 
    [Required] [MaxLength(200)] string Category,
    [Required] [MaxLength(200)] string Area
);

public record CreateAnimalResponse(int IdAnimal,string Name,string Description,string Category,string Area)
{
    public CreateAnimalResponse(int IdAnimal, CreateAnimalRequest request) : this(IdAnimal, request.Name,
        request.Description, request.Category, request.Area){}
};