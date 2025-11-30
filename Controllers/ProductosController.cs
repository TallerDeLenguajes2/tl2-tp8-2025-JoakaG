using Microsoft.AspNetCore.Mvc;
using MVC.Interfaces;


public class ProductosController : Controller
{

    private readonly IProductoRepository productoRepository;
    private readonly IAuthenticationServices _authService;

    public ProductosController(IProductoRepository repo, IAuthenticationServices authService)
    {
        productoRepository = repo;
        _authService = authService;
    }

    public IActionResult Index()
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        return View(productoRepository.Listar());
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
    public IActionResult AccesoDenegado()
    {
        // El usuario está logueado, pero no tiene el rol suficiente.
        return View();
    }


    public IActionResult Error()
    {
        return View();
    }

    // public IActionResult Details(int id)
    // {
    //     var producto = productoRepository.Listar().FirstOrDefault(p => p.IdProducto == id);

    //     if (producto == null)
    //         return NotFound();
    //     var prodViewModel = new ProductoViewModel(producto.Descripcion, producto.IdProducto);
    //     return View(prodViewModel);
    // } 
    [HttpGet]
    public IActionResult Create()
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        return View();
    }
    [HttpPost]
    public IActionResult Create(ProductoViewModel vwm)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        if (!ModelState.IsValid)
        {
            return View(vwm);
        }
        var nuevoProducto = new Producto();
        nuevoProducto.Precio = vwm.Precio;
        nuevoProducto.Descripcion = vwm.Descripcion;
        productoRepository.Crear(nuevoProducto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        var producto = productoRepository.ObtenerDetalle(id);
        if (producto.IdProducto != id)
            return NotFound();

        var vwmProducto = new ProductoViewModel(producto.Descripcion, producto.IdProducto, producto.Precio);

        return View(vwmProducto);
    }
    [HttpPost]
    public IActionResult Edit(int id, ProductoViewModel pvw)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        if (pvw.IdProducto != id) return NotFound();

        if (!ModelState.IsValid)
        {
            return View(pvw);
        }

        productoRepository.ModificarProducto(pvw.IdProducto, pvw.Descripcion, pvw.Precio);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        var producto = productoRepository.ObtenerDetalle(id);
        return View(producto);
    }

    [HttpPost]
    public IActionResult DeleteC(int IdProducto)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        if (productoRepository.Eliminar(IdProducto))
            return RedirectToAction("Index");
        else
            return RedirectToAction("Error");

    }
}