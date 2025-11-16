using System.ComponentModel.DataAnnotations;

public class PresupuestoViewModel
{

    public int IdPresupuesto { get; set; }
    [Required]
    public string NombreDestinatario { get; set; }
    [Required]
    // falta validacion que no sea fecha futura
    public DateTime FechaCreacion { get; set; }
    public PresupuestoViewModel(){}

    public PresupuestoViewModel(int IdPresupuesto, string NombreDestinatario, DateTime FechaCreacion)
    {
        this.IdPresupuesto = IdPresupuesto;
        this.NombreDestinatario = NombreDestinatario;
        this.FechaCreacion = FechaCreacion;
    }
}