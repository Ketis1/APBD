using Microsoft.AspNetCore.Mvc;

namespace API_APBD4.Controllers;

[ApiController]
[Route("visits")]
public class VisitsController : ControllerBase
{
    private IMockDb _mockDb;

    public VisitsController(IMockDb mockDb)
    {
        _mockDb = mockDb;
    }

    [HttpGet]
    public IActionResult GetVisitsForAnimal(int animalId)
    {
        return Ok(_mockDb.GetVisitsForAnimal(animalId));
    }

    [HttpPost]
    public IActionResult AddVisit(Visit visit)
    {
        _mockDb.AddVisit(visit);
        return Created($"visits/{visit.Id}", visit);
    }
}