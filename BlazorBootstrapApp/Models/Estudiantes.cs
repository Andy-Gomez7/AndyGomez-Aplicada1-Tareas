namespace BlazorBootstrapApp.Models;

using System.ComponentModel.DataAnnotations;

public class Estudiante
{
    [Key]
    public int EstudianteId { get; set; }
    [Required (ErrorMessage ="El campo nombre es obligatorio")]
    public string? Nombres  { get; set; }
    [Required (ErrorMessage ="El campo direccion es obligatorio")]
    public string? Direccion { get; set; }
    [Required (ErrorMessage ="El campo Email es obligatorio")]
    public string? Email { get; set; }
    [Required (ErrorMessage = "El campo fecha de nacimiento es obligatorio")]
    public DateOnly FechaNacimiento { get; set; }
}