using CW11.DTOs;
using CW11.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{   
    private readonly IDbService _dbService;

    public PrescriptionController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionAddDTO prescription)
    {
        try
        {
            await _dbService.AddPrescription(prescription);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Created();
    }
}