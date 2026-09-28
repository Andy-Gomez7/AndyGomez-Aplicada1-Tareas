using Microsoft.EntityFrameworkCore;
using BlazorBootstrapApp.Context;
using BlazorBootstrapApp.Models;
using System.Linq.Expressions;
using Aplicada1.Core;

namespace BlazorBootstrapApp.Services;

public class PrestamoService(IDbContextFactory<Contexto> DbFactory) : IService<Prestamo, int>    
{
    public async Task<bool> Guardar(Prestamo prestamo)
    {
        if (prestamo.PrestamoId == 0)
        {
            return await Insertar(prestamo);
        }
        else
        {
            return await Modificar(prestamo);
        }
    }
    
    public async Task<bool> Insertar(Prestamo prestamo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var libro = await contexto.Libros.FindAsync(prestamo.LibroId);
        if (libro is null || !libro.Disponible)
        {
            return false;
        }

        libro.Disponible = false;

        contexto.Prestamos.Add(prestamo);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Prestamo prestamo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        contexto.Update(prestamo);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Prestamo?> Buscar(int prestamoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Prestamos.Include(E => E.Estudiante).Include(L => L.Libro).FirstOrDefaultAsync(E => E.PrestamoId == prestamoId);
    }

    public async Task<bool> Existe(int PrestamoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Prestamos.AnyAsync(E => E.PrestamoId == PrestamoId);
    }

   public async Task<bool> Eliminar(int PrestamoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Prestamos.AsNoTracking().Where(E => E.PrestamoId == PrestamoId).ExecuteDeleteAsync() > 0;
    } 

    public async Task<List<Prestamo>> GetList(Expression<Func<Prestamo, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Prestamos
        .Include(L => L.Libro)
        .Include(E => E.Estudiante)
        .Where(criterio)
        .AsNoTracking()
        .ToListAsync();
    }
}
