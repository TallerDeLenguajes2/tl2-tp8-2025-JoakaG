using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Services;
using MVC.Interfaces;
using tl2_tp8_2025_JoakaG.Models;

public class PresupuestosController : Controller
{

    private readonly IPresupuestoRepository presupuestoRepository;
    private readonly IAuthenticationServices _authService;

    private readonly IProductoRepository productoRepository;

    public PresupuestosController(IPresupuestoRepository presupuestoRepository, IAuthenticationServices authService, IProductoRepository productoRepository)
    {
        this.presupuestoRepository = presupuestoRepository;
        this.productoRepository = productoRepository;
        _authService = authService;
    }

    public IActionResult Index()
    {
        if (!_authService.IsAuthenticated() || (!_authService.HasAccessLevel("Administrador") && !_authService.HasAccessLevel("Cliente")))
        {
                return RedirectToAction("Index", "Login");
        }
        return View(presupuestoRepository.Listar());
    }
    private IActionResult CheckAdminPermissions()
    {
        // 1. No logueado? -> vuelve al login
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }


        // 2. No es Administrador? -> Da Error
        if (!_authService.HasAccessLevel("Administrador"))
        {
            // Llamamos a AccesoDenegado (llama a la vista correspondiente de Productos)
            return RedirectToAction(nameof(AccesoDenegado));
        }
        return null; // Permiso concedido
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
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        return View();
    }


    [HttpPost]
    public IActionResult Create(PresupuestoViewModel vwm)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        if (vwm.FechaCreacion.Date > DateTime.Today.Date)
        {
            ModelState.AddModelError("FechaCreacion", "La fecha no puede ser futura.");
            return View(vwm);
        }
        if (!ModelState.IsValid)
        {
            return View(vwm);
        }

        var presupuesto = new Presupuesto();
        presupuesto.IdPresupuesto = vwm.IdPresupuesto;
        presupuesto.NombreDestinatario = vwm.NombreDestinatario;
        presupuesto.FechaCreacion = vwm.FechaCreacion;
        presupuestoRepository.Crear(presupuesto);
        return RedirectToAction("index");
    }
    [HttpGet]
    public IActionResult AgregarP(int idPresupuesto)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        var productosDispo = productoRepository.Listar();
        var model = new AgregarProductoViewModel();
        model.IdPresupuesto = idPresupuesto;
        model.ProductosDisponibles = new SelectList(productosDispo, "IdProducto", "Descripcion");
        return View(model);
    }
    // mas prolijo directamente no mostrar los productos que ya fueron agregados...
    [HttpPost]
    public IActionResult AgregarP(AgregarProductoViewModel vwm)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        var productosDispo = productoRepository.Listar();
        vwm.ProductosDisponibles = new SelectList(productosDispo, "IdProducto", "Descripcion");
        if (!ModelState.IsValid)
        {
            return View(vwm);
        }
        if (presupuestoRepository.ObtenerDetalle(vwm.IdPresupuesto).Any(x => x.Producto.IdProducto == vwm.IdProducto))
        {
            ModelState.AddModelError("IdProducto", "El producto ya fue agregado al presupuesto Anteriormente");
            return View(vwm);
        }

        presupuestoRepository.agregarProductoAPresupuesto(vwm.IdPresupuesto, vwm.IdProducto, vwm.Cantidad);
        return RedirectToAction(nameof(Details), new { id = vwm.IdPresupuesto });
    }
    [HttpGet]
    public IActionResult Delete(int IdPresupuesto)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        return View(IdPresupuesto);
    }
    [HttpPost]
    public IActionResult DeleteC(int IdPresupuesto)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        presupuestoRepository.Eliminar(IdPresupuesto);
        return RedirectToAction("index");
    }

    public IActionResult DeleteP(int IdPresupuesto, int IdProducto)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
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
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        presupuestoRepository.EliminarProductoDetalle(IdPresupuesto, IdProducto);
        return RedirectToAction(nameof(Details), new { id = IdPresupuesto });
    }

    public IActionResult AccesoDenegado()
    {
        return View();
    }

}