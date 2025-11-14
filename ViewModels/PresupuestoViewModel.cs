public class PresupuestoViewModel
{

   public int IdPresupuesto {get; set;}
   public string NombreDestinatario {get; set;}
   public DateTime FechaCreacion {get; set;}
   public List<PresupuestoDetalle> Detalle {get; set;}

    public PresupuestoViewModel(int IdPresupuesto, string NombreDestinatario, DateTime FechaCreacion, List<PresupuestoDetalle> Detalle)
    {
        this.IdPresupuesto = IdPresupuesto;
        this.NombreDestinatario = NombreDestinatario;
        this.FechaCreacion = FechaCreacion;
        this.Detalle = Detalle;
    }
}