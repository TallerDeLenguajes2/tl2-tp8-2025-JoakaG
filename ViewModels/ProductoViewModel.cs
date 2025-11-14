public class ProductoViewModel
{
    public int Id {get; set;}
    public string Descripcion {get; set;}
    public ProductoViewModel(string Descripcion, int Id)
    {
        this.Id = Id;
        this.Descripcion = Descripcion;
    }
}