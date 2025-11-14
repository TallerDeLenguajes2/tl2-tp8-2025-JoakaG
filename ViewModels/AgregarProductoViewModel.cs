public class AgregarProductoViewModel
{
    public int IdPresupuesto{get; set;}
    public List<Producto> Productos {get; set;}
    public Producto Producto {get; set;}
    public int Cantidad {get; set;}
    public double MontoTotal {get; set;}

    public AgregarProductoViewModel(int IdPresupuesto, List<Producto> Productos, double MontoTotal, Producto Producto, int Cantidad)
    {
        this.IdPresupuesto = IdPresupuesto;
        this.Productos = Productos;
        this.MontoTotal = MontoTotal;
        this.Producto = Producto;
        this.Cantidad = Cantidad;
    }

}