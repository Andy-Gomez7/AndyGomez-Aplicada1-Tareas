namespace BlazorBootstrapApp.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Libro
{
    [Key]
    public int LibroId { get; set; }
    public string? Titulo { get; set; }
    public string? Autor { get; set; }
    public int AnoPublicacion { get; set; }

    [InverseProperty("Libro")]
    public virtual ICollection<Prestamo> Prestamos { get; set; }= new List<Prestamo>();
    
}