using CrudAPI.Context;
using CrudAPI.DTOs;
using CrudAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmpleadoController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<EmpleadoDTO>>> Get() =>
            Ok(
               await _context.Empleados
                    .AsNoTracking()
                    .Select(e => new EmpleadoDTO
                        {
                            IdEmpleado = e.IdEmpleado,
                            NombreCompleto = e.NombreCompleto,
                            Sueldo = e.Sueldo,
                            IdPerfil = e.IdPerfil,
                            NombrePerfil = e.PerfilReferencia.Nombre
                        }
                    ).ToListAsync()
            );

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<ActionResult<EmpleadoDTO>> Get(int id) =>
            Ok(
                await _context.Empleados
                    .AsNoTracking()
                    .Where(e => e.IdEmpleado == id)
                    .Select(e => new EmpleadoDTO
                        {
                            IdEmpleado = e.IdEmpleado,
                            NombreCompleto = e.NombreCompleto,
                            Sueldo = e.Sueldo,
                            IdPerfil = e.IdPerfil,
                            NombrePerfil = e.PerfilReferencia.Nombre
                        })
                    .FirstOrDefaultAsync()
            );

        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult<EmpleadoDTO>> Guardar([FromBody] EmpleadoDTO empleado)
        {
            var empleadoDB = new Empleado
            {
                NombreCompleto = empleado.NombreCompleto,
                Sueldo = empleado.Sueldo.Value,
                IdPerfil = empleado.IdPerfil
            };

            await _context.Empleados.AddAsync(empleadoDB);
            await _context.SaveChangesAsync();
            return Ok("Empleado agregado");
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<ActionResult> Editar([FromBody] EmpleadoDTO empleado)
        {
            var empleadoDB = await _context.Empleados
                .FirstOrDefaultAsync(e => e.IdEmpleado == empleado.IdEmpleado);
            
            empleadoDB.NombreCompleto = empleado.NombreCompleto;
            empleadoDB.Sueldo = empleado.Sueldo.Value;
            empleadoDB.IdPerfil = empleado.IdPerfil;
            _context.Empleados.Update(empleadoDB);
            await _context.SaveChangesAsync();
            return Ok("Empleado editado correctamente.");
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult<EmpleadoDTO>> Eliminar(int id)
        {
            var empleadoDB = await _context.Empleados
                .FirstOrDefaultAsync(e => e.IdEmpleado == id);
            
            if(empleadoDB is null)
                return NotFound("Empleado no encontrado");
            
            _context.Empleados.Remove(empleadoDB);
            await _context.SaveChangesAsync();
            return Ok("Empleado eliminado correctamente.");
        }
    }
}
