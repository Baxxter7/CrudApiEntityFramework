using CrudAPI.Context;
using CrudAPI.DTOs;
using CrudAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private AppDbContext _context;

        public PerfilController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<PerfilDTO>>> Get() =>
            Ok(await _context
                .Perfiles
                .Select(p => new PerfilDTO(p.IdPerfil, p.Nombre))
                .ToListAsync());
      
    }
    
}
