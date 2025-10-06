using CrudAPI.Context;
using CrudAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Services;

public class PerfilService
{
    private readonly AppDbContext _context;

    public PerfilService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PerfilDTO>> ObtenerListaAsync()
    {
        return await _context
            .Perfiles
            .AsNoTracking()
            .Select(p => new PerfilDTO(p.IdPerfil, p.Nombre))
            .ToListAsync();
    }
}