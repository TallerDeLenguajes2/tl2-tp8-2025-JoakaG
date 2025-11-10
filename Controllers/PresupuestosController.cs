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
        var presupuesto = presupuestoRepository.Listar()
            .FirstOrDefault(p => p.IdPresupuesto == id);

        if (presupuesto == null)
            return NotFound();

        return View(presupuesto);
    }

}