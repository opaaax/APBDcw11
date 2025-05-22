using CW11.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IDbService _dbService;

    public PatientController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPatientData(int id)
    {
        try
        {
            var patient = await _dbService.GetPatientData(id);
            return Ok(patient);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}