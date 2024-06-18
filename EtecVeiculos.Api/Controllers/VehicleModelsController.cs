using System.Runtime.CompilerServices;
using EtecVeiculos.Api.DTO;
using EtecVeiculos.Api.Data;
using EtecVeiculos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EtecVeiculos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleModelsController : ControllerBase
{
    private AppDbContext _context;

    public VehicleModelsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<VehicleModel>>> Get()
    {
        var models = await _context.VehicleModels.ToListAsync();
        return Ok(models);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VehicleModel>> Get(int id)
    {
        var model = await _context.VehicleModels.FindAsync(id);
        if (model == null)
            return NotFound("Vehicle Model not located!");
        return Ok(model);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(VehicleModelVM vehicleModelVM)
    {
        if (ModelState.IsValid)
        {
            VehicleModel vehicleModel = new()
            {
                Name = vehicleModelVM.Name
            };
            await _context.AddAsync(vehicleModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = vehicleModel.Id});
        }
        return BadRequest("Check the data provided!");
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Edit(int id, VehicleModel vehicleModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (!_context.VehicleModels.Any(q => q.Id == id))

                    return NotFound("Vehicle model not found!");
                if (id != vehicleModel.Id)
                    return BadRequest("Check the data provided!");

                _context.Entry(vehicleModel).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"There was a problem: {ex.Message}");
            }
        }
        return BadRequest("Check the data provided!");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]



    
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var vehicleModel = await _context.VehicleModels
            .FirstOrDefaultAsync(q => q.Id == id);
            if (vehicleModel == null)
                return NotFound("Vehicle model not found!");
                
            _context.Remove(vehicleModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"There was a problem: {ex.Message}");
        }
    }

}

