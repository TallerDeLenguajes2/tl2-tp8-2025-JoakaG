public interface IProductoRepository
{
    public void Crear(Producto X);
    public List<Producto> Listar();

    public bool ModificarProducto(int id, string Descripcion, double Precio);
    public Producto ?ObtenerDetalle(int id);
    public bool Eliminar(int id);
}