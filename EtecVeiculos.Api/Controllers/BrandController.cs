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
public class BrandsController : ControllerBase
{
    private AppDbContext _context;

    public BrandsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Brand>>> Get()
    {
        var brands = await _context.Brands.ToListAsync();
        return Ok(brands);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Brand>> Get(int id)
    {
        var brand = await _context.Brands.FindAsync(id);
        if (brand == null)
            return NotFound("Vehicle Brand not located!");
        return Ok(brand);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(BrandVM brandVM)
    {
        if (ModelState.IsValid)
        {
            Brand brand = new()
            {
                Name = brandVM.Name
            };
            await _context.AddAsync(brand);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = brand.Id});
        }
        return BadRequest("Check the data provided!");
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Edit(int id, Brand brand)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (!_context.Brands.Any(q => q.Id == id))

                    return NotFound("Vehicle brand not found!");
                if (id != brand.Id)
                    return BadRequest("Check the data provided!");

                _context.Entry(brand).State = EntityState.Modified;
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
            var brand = await _context.Brands
            .FirstOrDefaultAsync(q => q.Id == id);
            if (brand == null)
                return NotFound("Vehicle brand not found!");
                
            _context.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"There was a problem: {ex.Message}");
        }
    }

}

