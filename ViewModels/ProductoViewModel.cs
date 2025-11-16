using System.ComponentModel.DataAnnotations;

public class ProductoViewModel
{
    public int IdProducto { get; set; }
    [StringLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres")]
    public string Descripcion { get; set; }
    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser positivo")]
    public double Precio { get; set; }


    public ProductoViewModel(){}
    public ProductoViewModel(string Descripcion, int IdProducto, double Precio)
    {
        this.IdProducto = IdProducto;
        this.Descripcion = Descripcion;
        this.Precio = Precio;
    }
}