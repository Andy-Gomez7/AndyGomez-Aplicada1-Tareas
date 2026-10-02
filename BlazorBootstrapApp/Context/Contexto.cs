namespace BlazorBootstrapApp.Context;

using Microsoft.EntityFrameworkCore;
using BlazorBootstrapApp.Models;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options){}
    public DbSet<Libro> Libros { get; set; }
    public DbSet<Estudiante> Estudiantes { get; set; }
}