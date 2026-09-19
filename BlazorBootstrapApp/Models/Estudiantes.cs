namespace BlazorBootstrapApp.Models;

using System.ComponentModel.DataAnnotations;

public class Estudiante
{
    [Key]
    public int EstudianteId { get; set; }
    public string? Nombres  { get; set; }
    public string? Direccion { get; set; }
    public string? Email { get; set; }
    public string? FechaNacimiento { get; set; }
}