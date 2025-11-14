public class AgregarProductoViewModel
{
    int IdPresupuesto{get; set;}
    List<Producto> Productos {get; set;}
    double MontoTotal {get; set;}

    public AgregarProductoViewModel(int IdPresupuesto, List<Producto> Productos, double MontoTotal)
    {
        this.IdPresupuesto = IdPresupuesto;
        this.Productos = Productos;
        this.MontoTotal = MontoTotal;
    }


}