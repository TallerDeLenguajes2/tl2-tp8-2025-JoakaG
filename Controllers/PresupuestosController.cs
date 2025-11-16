using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_JoakaG.Models;

public class PresupuestosController : Controller
{

    private readonly PresupuestoRepository presupuestoRepository;
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
        return View(idPresupuesto);
    }

    [HttpPost]
    public IActionResult AgregarP(AgregarProductoViewModel vwm)
    {
        presupuestoRepository.agregarProductoAPresupuesto(vwm.IdPresupuesto, vwm.IdProducto, vwm.Cantidad);
        return RedirectToAction("index");
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

    //     public IActionResult DeleteP(int IdPresupuesto)
    // {
    //     return View(IdPresupuesto);
    // }
    // [HttpPost]
    // public IActionResult DeletePC(int IdPresupuesto, int IdProducto)
    // {
    //     presupuestoRepository.EliminarProductoDetalle(IdPresupuesto, IdProducto);
    //     return RedirectToAction("index");
    // }

}