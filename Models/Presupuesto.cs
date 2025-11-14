public class Presupuesto
{
    private int idPresupuesto;
    private string nombreDestinatario;
    private DateTime fechaCreacion;
    private List<PresupuestoDetalle> detalle;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }


    public Presupuesto()
    {
        Detalle = new List<PresupuestoDetalle>();
    }
    public double MontoPresupuesto()
    {
        double monto = 0;

        foreach (var detalleProducto in detalle)
        {
            var producto = detalleProducto.Producto;
            monto += producto.Precio * detalleProducto.Cantidad;
        }

        return monto;
    }
    public double MontoPresupuestoConIva()
    {
        return MontoPresupuesto() * 1.21;
    } 
    public int CantidadProductos()
    {
        int cant = 0;

        foreach (var detalleProducto in detalle)
        {
            cant += detalleProducto.Cantidad;
        }

        return cant;
    } //contar total de productos (sumador d todas las cantidades del detalle)
}