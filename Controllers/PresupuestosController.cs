using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using tl2_tp8_2025_JoakaG.Models;

public class PresupuestosController : Controller
{

    private readonly PresupuestoRepository presupuestoRepository;

    private readonly ProductoRepository productoRepository = new ProductoRepository();

    public PresupuestosController()
    {
        presupuestoRepository = new PresupuestoRepository();
        productoRepository = new ProductoRepository();
    }

    public IActionResult Index()
    {
        return View(presupuestoRepository.Listar());
    }

    public IActionResult Details(int id)
    {
        var detalles = presupuestoRepository.ObtenerDetalle(id);
        var presupuesto = presupuestoRepository.Listar()
                          .FirstOrDefault(p => p.IdPresupuesto == id);
        if (presupuesto == null)
        {
            return NotFound();
        }
        presupuesto.Detalle = detalles;
        return View(presupuesto);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Create(PresupuestoViewModel vwm)
    {
        var presupuesto = new Presupuesto();
        presupuesto.IdPresupuesto = vwm.IdPresupuesto;
        presupuesto.NombreDestinatario = vwm.NombreDestinatario;
        //VALIDAR FECHA
        presupuesto.FechaCreacion = vwm.FechaCreacion;
        presupuestoRepository.Crear(presupuesto);
        return RedirectToAction("index");
    }
    [HttpGet]
    public IActionResult AgregarP(int idPresupuesto)
    {
        var productosDispo = productoRepository.Listar();
        var model = new AgregarProductoViewModel();
        model.IdPresupuesto = idPresupuesto;
        model.ProductosDisponibles = new SelectList(productosDispo, "IdProducto", "Descripcion");
        return View(model);
    }

    [HttpPost]
    public IActionResult AgregarP(AgregarProductoViewModel vwm)
    {
        if (!ModelState.IsValid)
        {
            var productosDispo = productoRepository.Listar();
            vwm.ProductosDisponibles = new SelectList(productosDispo, "IdProducto", "Descripcion");
            return View(vwm);
        }
            presupuestoRepository.agregarProductoAPresupuesto(vwm.IdPresupuesto, vwm.IdProducto, vwm.Cantidad);
            return RedirectToAction(nameof(Details), new { id = vwm.IdPresupuesto });
    }
    [HttpGet]
    public IActionResult Delete(int IdPresupuesto)
    {
        return View(IdPresupuesto);
    }
    [HttpPost]
    public IActionResult DeleteC(int IdPresupuesto)
    {
        presupuestoRepository.Eliminar(IdPresupuesto);
        return RedirectToAction("index");
    }

         public IActionResult DeleteP(int IdPresupuesto, int IdProducto)
     {
        var producto = productoRepository.ObtenerDetalle(IdProducto);
        if (IdProducto != producto.IdProducto) return NotFound();
        var model = new BorrarProductoViewModel();
        model.IdProducto = IdProducto;
        model.IdPresupuesto = IdPresupuesto;
        model.Descripcion = producto.Descripcion;
        model.Precio = producto.Precio;

         return View(model);
     }
     [HttpPost]
     public IActionResult DeletePC(int IdPresupuesto, int IdProducto)
     {
         presupuestoRepository.EliminarProductoDetalle(IdPresupuesto, IdProducto);
         return RedirectToAction(nameof(Details), new { id = IdPresupuesto });
     }

}