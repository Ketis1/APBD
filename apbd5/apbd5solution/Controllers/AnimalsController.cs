using System.Data.SqlClient;
using System.Data.SqlTypes;
using apbd5solution.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace apbd5solution.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetAnimals(string orderBy)
    {
        var response = new List<GetAnimalsResposne>();
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            using var cmd = new SqlCommand($"SELECT IdAnimal,Name,Description,Category,Area FROM Animal" +
                                           $" ORDER BY {orderBy}",sqlConnection);
            cmd.Connection.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                response.Add(new GetAnimalsResposne(
                    reader.GetInt32(0), 
                    reader.GetString(1), 
                    reader.GetString(2), 
                    reader.GetString(3), 
                    reader.GetString(4)
                )
                );
            }
        }

        return Ok(response);

    }

    [HttpPost]
    public IActionResult CreateAnimal(CreateAnimalRequest request)
    {
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var sqlCommand = new SqlCommand(
                "INSERT INTO Animal (Name, Description, Category, Area) values (@1, @2, @3, @4); SELECT CAST(SCOPE_IDENTITY() as int)",
                sqlConnection
            );
            sqlCommand.Parameters.AddWithValue("@1", request.Name);
            sqlCommand.Parameters.AddWithValue("@2", request.Description);
            sqlCommand.Parameters.AddWithValue("@3", request.Category);
            sqlCommand.Parameters.AddWithValue("@4", request.Area);
            sqlCommand.Connection.Open();
            
            var id = sqlCommand.ExecuteScalar();
            
            return Created($"animals/{id}", new CreateAnimalResponse((int)id, request));
        }
    }

    [HttpPut]
    public IActionResult ReplaceAnimal(int id, ReplaceAnimalRequest request)
    {
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var sqlCommand = new SqlCommand(
                "UPDATE Animal SET Name = @1, Description = @2, Category = @3, Area = @4 WHERE IdAnimal = @5",
                sqlConnection
            );
            sqlCommand.Parameters.AddWithValue("@1", request.Name);
            sqlCommand.Parameters.AddWithValue("@2", request.Description);
            sqlCommand.Parameters.AddWithValue("@3", request.Category);
            sqlCommand.Parameters.AddWithValue("@4", request.Area);
            sqlCommand.Parameters.AddWithValue("@5", id);
            sqlCommand.Connection.Open();

            var affectedRows = sqlCommand.ExecuteNonQuery();
            return affectedRows == 0 ? NotFound() : NoContent();
        }
    }
    
    [HttpDelete("{id}")]
    public IActionResult RemoveAnimal(int id)
    {
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var command = new SqlCommand("DELETE FROM Animal WHERE ID = @1", sqlConnection);
            command.Parameters.AddWithValue("@1", id);
            command.Connection.Open();

            var affectedRows = command.ExecuteNonQuery();

            return affectedRows == 0 ? NotFound() : NoContent();
        }
    }


}