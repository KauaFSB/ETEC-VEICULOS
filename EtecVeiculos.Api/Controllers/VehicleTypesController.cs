using EtecVeiculos.Api.Data;
using EtecVeiculos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }

