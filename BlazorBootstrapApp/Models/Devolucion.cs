namespace BlazorBootstrapApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Devolucion
{
    [Key]
    public int DevolucionId { get; set; }
    public int PrestamoId { get; set; }
    [ForeignKey("PrestamoId")]
    public Prestamo Prestamo { get; set; } = null!;
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}
