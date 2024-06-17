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
public class VehicleTypesController : ControllerBase
{
    private AppDbContext _context;

    public VehicleTypesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<VehicleType>>> Get()
    {
        var types = await _context.VehicleTypes.ToListAsync();
        return Ok(types);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VehicleType>> Get(int id)
    {
        var type = await _context.VehicleTypes.FindAsync(id);
        if (type == null)
            return NotFound("Vehicle Type not located!");
        return Ok(type);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(VehicleTypeVM vehicleTypeVM)
    {
        if (ModelState.IsValid)
        {
            VehicleType vehicleType = new()
            {
                Name = vehicleTypeVM.Name
            };
            await _context.AddAsync(vehicleType);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = vehicleType.Id});
        }
        return BadRequest("Check the data provided!");
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Edit(int id, VehicleType vehicleType)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (!_context.VehicleTypes.Any(q => q.Id == id))

                    return NotFound("Vehicle type not found!");
                if (id != vehicleType.Id)
                    return BadRequest("Check the data provided!");

                _context.Entry(vehicleType).State = EntityState.Modified;
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
            var vehicleType = await _context.VehicleTypes
            .FirstOrDefaultAsync(q => q.Id == id);
            if (vehicleType == null)
                return NotFound("Vehicle type not found!");
                
            _context.Remove(vehicleType);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"There was a problem: {ex.Message}");
        }
    }

}

