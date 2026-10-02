namespace BlazorBootstrapApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Prestamo
{
    [Key]
    public int PrestamoId { get; set; }
    public DateOnly FechaPrestamo { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly? FechaDevolucion { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un libro valido")]
    public int EstudianteId { get; set; }

    [ForeignKey("EstudianteId")]
    [InverseProperty("Prestamos")]
    public virtual Estudiante Estudiante { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un libro valido")]
    public int LibroId { get; set; }

    [ForeignKey("LibroId")]
    [InverseProperty("Prestamos")]
    public virtual Libro Libro { get; set; }
}