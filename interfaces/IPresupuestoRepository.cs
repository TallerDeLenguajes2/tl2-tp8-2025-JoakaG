public interface IPresupuestoRepository
{
    public void Crear(Presupuesto X);
    public List<Presupuesto> Listar();
    public List<PresupuestoDetalle> ?ObtenerDetalle(int id);
    public bool agregarProductoAPresupuesto(int idPresupuesto, int idProducto, int cantidad);
    public bool Eliminar(int id);
    public bool EliminarProductoDetalle(int IdPresupuesto, int IdProducto);
}