using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_JoakaG.Models;

public class PresupuestosController : Controller
{

    private PresupuestoRepository presupuestoRepository;
    public PresupuestosController()
    {
        presupuestoRepository = new PresupuestoRepository();
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




    [HttpPost]
    public IActionResult Create(Presupuesto presupuesto)
    {
        presupuestoRepository.Crear(presupuesto);
        return RedirectToAction("Presupuestos/presupuesto");
    }
    [HttpGet]
    public IActionResult AgregarP(int idPresupuesto)
    {
        return View(idPresupuesto);
    }

    [HttpPost]
    public IActionResult AgregarP(int idPresupuesto, int idProducto, int cantidad)
    {
        presupuestoRepository.agregarProductoAPresupuesto(idPresupuesto, idProducto, cantidad);
        return RedirectToAction("index");
    }
    [HttpGet]
    public IActionResult Delete(int IdPresupuesto)
    {
        return View(IdPresupuesto);
    }


}