namespace BlazorBootstrapApp.Services;

using Microsoft.EntityFrameworkCore;
using BlazorBootstrapApp.Context;
using BlazorBootstrapApp.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Internal;

public class EstudianteService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Estudiante estudiante)
    {
        if (estudiante.EstudianteId == 0)
        {
            return await Insertar(estudiante);
        }
        else
        {
            return await Modificar(estudiante);
        }
    }
    
    public async Task<bool> Insertar(Estudiante estudiante)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        contexto.Estudiantes.Add(estudiante);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Estudiante estudiante)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        contexto.Update(estudiante);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Estudiante> Buscar(int estudianteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Estudiantes.FirstOrDefaultAsync(E => E.EstudianteId == estudianteId);
    }

    public async Task<bool> Existe(string nombre)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Estudiantes.AnyAsync(E => E.Nombres == nombre);
    }

   public async Task<bool> Eliminar(int estudianteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Estudiantes.AsNoTracking().Where(E => E.EstudianteId == estudianteId).ExecuteDeleteAsync() > 0;
    } 

    public async Task<List<Estudiante>> Listar(Expression<Func<Estudiante, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Estudiantes.Where(criterio).AsNoTracking().ToListAsync();
    }
}