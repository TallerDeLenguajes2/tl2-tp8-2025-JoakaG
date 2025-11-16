using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class AgregarProductoViewModel
{
    public int IdPresupuesto { get; set; }

    public int IdProducto { get; set; }
    public string Descripcion { get; set; }
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "La cantidad debe ser positiva")]
    public int Cantidad { get; set; }

    SelectList ProductosDisponibles;
    public AgregarProductoViewModel(string Descripcion, int Id, int Cantidad, int IdProducto)
    {
        IdPresupuesto = Id;
        this.Descripcion = Descripcion;
        this.IdProducto = IdProducto;
        this.Cantidad = Cantidad;
    }
}


