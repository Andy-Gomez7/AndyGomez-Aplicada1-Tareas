using Microsoft.EntityFrameworkCore;
using BlazorBootstrapApp.Context;
using BlazorBootstrapApp.Models;
using System.Linq.Expressions;

namespace BlazorBootstrapApp.Services;

public class LibroService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Libro libro)
    {
        if (libro.LibroId == 0)
            return await Insertar(libro);
        else
        return await Modificar(libro);
    }

    public async Task<bool> Insertar(Libro libro)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        contexto.Libros.Add(libro);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Libro libro)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        contexto.Update(libro);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Libro> Buscar(int libroId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Libros
            .FirstOrDefaultAsync(l => l.LibroId == libroId);
    }

    public async Task<bool> Existe(string titulo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Libros
            .AnyAsync(l => l.Titulo == titulo);
    }

    public async Task<bool> Eliminar(int libroId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Libros
            .AsNoTracking()
            .Where(l => l.LibroId == libroId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Libro>> Listar(Expression<Func<Libro, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Libros
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}