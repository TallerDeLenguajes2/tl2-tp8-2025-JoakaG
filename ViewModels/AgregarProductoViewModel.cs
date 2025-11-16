using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class AgregarProductoViewModel
{
    public int IdPresupuesto { get; set; }

    public int IdProducto { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "La cantidad debe ser positiva")]
    public int Cantidad { get; set; }
    public SelectList ProductosDisponibles;


    public AgregarProductoViewModel(){}
    public AgregarProductoViewModel(int IdPresupuesto, int Cantidad, int IdProducto)
    {
        this.IdPresupuesto = IdPresupuesto;
        this.IdProducto = IdProducto;
        this.Cantidad = Cantidad;
    }
}


