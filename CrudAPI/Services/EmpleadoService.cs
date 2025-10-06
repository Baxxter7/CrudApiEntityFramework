using CrudAPI.Context;
using CrudAPI.DTOs;
using CrudAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Services;

public interface IEmpleadoService
{
    public Task<List<EmpleadoDTO>> ObtenerListaAsync();
    public Task<EmpleadoDTO?> ObtenerEmpleadoByIdAsync(int id);
    public Task GuardarEmpleadoAsync(EmpleadoDTO empleado);
    public Task EditarEmpleadoAsync([FromBody] EmpleadoDTO empleado);

    public Task EliminarEmpleadoAsync(int id);
}
    
public class EmpleadoService : IEmpleadoService
{
    private readonly AppDbContext _context;

    public EmpleadoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmpleadoDTO>> ObtenerListaAsync()
    {
        return await _context.Empleados
            .AsNoTracking()
            .Select(e => new EmpleadoDTO
                {
                    IdEmpleado = e.IdEmpleado,
                    NombreCompleto = e.NombreCompleto,
                    Sueldo = e.Sueldo,
                    IdPerfil = e.IdPerfil,
                    NombrePerfil = e.PerfilReferencia.Nombre
                }
            ).ToListAsync();
    }
    public async Task<EmpleadoDTO?> ObtenerEmpleadoByIdAsync(int id)
    {
        return await _context.Empleados
            .AsNoTracking()
            .Select(e => new EmpleadoDTO
            {
                IdEmpleado = e.IdEmpleado,
                NombreCompleto = e.NombreCompleto,
                Sueldo = e.Sueldo,
                IdPerfil = e.IdPerfil,
                NombrePerfil = e.PerfilReferencia.Nombre
            })
            .FirstOrDefaultAsync(e => e.IdEmpleado == id);
    }

    public async Task GuardarEmpleadoAsync(EmpleadoDTO empleado)
    {
        var empleadoDb = new Empleado
        {
            NombreCompleto = empleado.NombreCompleto,
            Sueldo = empleado.Sueldo.Value,
            IdPerfil = empleado.IdPerfil
        };
        
        await _context.Empleados.AddAsync(empleadoDb);
        await _context.SaveChangesAsync();
    }
    private async Task<Empleado?>  ObtenerEmpleadoAsync(int id) => 
        await _context.Empleados
        .FirstOrDefaultAsync(e => e.IdEmpleado == id);
    public async Task EditarEmpleadoAsync(EmpleadoDTO empleado)
    {
        var empleadoDb = await ObtenerEmpleadoAsync(empleado.IdEmpleado);
        
        empleadoDb.NombreCompleto = empleado.NombreCompleto;
        empleadoDb.Sueldo = empleado.Sueldo.Value;
        empleadoDb.IdPerfil = empleado.IdPerfil;

        _context.Empleados.Update(empleadoDb);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarEmpleadoAsync(int id)
    {
        var empleadoDb = await ObtenerEmpleadoAsync(id);
        if(empleadoDb is null)
            throw new Exception("Empleado no encontrado");
            
        _context.Empleados.Remove(empleadoDb);
        await _context.SaveChangesAsync();
    }
    
}