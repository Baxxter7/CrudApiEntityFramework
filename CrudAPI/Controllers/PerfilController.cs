using CrudAPI.Context;
using CrudAPI.DTOs;
using CrudAPI.Entities;
using CrudAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly PerfilService _perfilService;

        public PerfilController(PerfilService perfilService)
        {
            _perfilService = perfilService;
          
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<PerfilDTO>>> Get() =>
            Ok(await _perfilService.ObtenerListaAsync());
      
    }
    
}
