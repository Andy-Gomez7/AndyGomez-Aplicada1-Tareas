using Microsoft.EntityFrameworkCore;
using BlazorBootstrapApp.Context;
using BlazorBootstrapApp.Models;
using System.Linq.Expressions;
using Aplicada1.Core;

namespace BlazorBootstrapApp.Services;

public class DevolucionService(IDbContextFactory<Contexto> DbFactory) : IService<Devolucion, int>
{
    public async Task<bool> Guardar(Devolucion devolucion)
    {
        if (devolucion.DevolucionId == 0)
        {
            return await Insertar(devolucion);
        }
        else
        {
            return await Modificar(devolucion);
        }
    }

    public async Task<bool> Insertar(Devolucion devolucion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var prestamo = await contexto.Prestamos
            .Include(P => P.Libro)
            .Include(P => P.Devolucion)
            .FirstOrDefaultAsync(P => P.PrestamoId == devolucion.PrestamoId);

        if (prestamo is null || prestamo.Devolucion is not null)
        {
            return false;
        }

        prestamo.Libro.Disponible = true;

        contexto.Devolucion
        .Add(devolucion);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Devolucion devolucion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        contexto.Update(devolucion);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Devolucion?> Buscar(int devolucionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Devolucion

            .Include(D => D.Prestamo).ThenInclude(P => P.Libro)
            .Include(D => D.Prestamo).ThenInclude(P => P.Estudiante)
            .FirstOrDefaultAsync(D => D.DevolucionId == devolucionId);
    }

    public async Task<bool> Existe(int devolucionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Devolucion
        .AnyAsync(D => D.DevolucionId == devolucionId);
    }

    public async Task<bool> Eliminar(int devolucionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Devolucion

            .AsNoTracking()
            .Where(D => D.DevolucionId == devolucionId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Devolucion>> GetList(Expression<Func<Devolucion, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Devolucion

            .Include(D => D.Prestamo).ThenInclude(P => P.Libro)
            .Include(D => D.Prestamo).ThenInclude(P => P.Estudiante)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
