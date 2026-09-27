namespace BlazorBootstrapApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Prestamo
{
    [Key]
    public int PrestamoId { get; set; }
    public DateOnly FechaPrestamo { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly? FechaDevolucion { get; set; }

    public int EstudianteId { get; set; }
    [ForeignKey("EstudianteId")]
    [InverseProperty("Prestamo")]
    public Estudiante estudiante { get; set; }

    public int LibroId { get; set; }
    [ForeignKey("LibroId")]
    [InverseProperty("Prestamo")]
    public Libro libro { get; set; }
}