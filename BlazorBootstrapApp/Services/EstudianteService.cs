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

    
}