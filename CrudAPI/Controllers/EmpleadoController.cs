using CrudAPI.DTOs;
using CrudAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoService _service;
        public EmpleadoController(EmpleadoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<EmpleadoDTO>>> Get() =>
            Ok(
               await _service.ObtenerListaAsync()
            );

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<ActionResult<EmpleadoDTO>> Get(int id) =>
            Ok(
               await _service.ObtenerEmpleadoByIdAsync(id)
            );

        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult> Guardar([FromBody] EmpleadoDTO empleado)
        {
            await _service.GuardarEmpleadoAsync(empleado);
            return Ok("Empleado guardado exitosamente");
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<ActionResult> Editar([FromBody] EmpleadoDTO empleado)
        {
            await _service.EditarEmpleadoAsync(empleado);
            return Ok("Empleado editado correctamente.");
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            await _service.EliminarEmpleadoAsync(id);
            return Ok("Empleado eliminado correctamente.");
        }
    }
}
