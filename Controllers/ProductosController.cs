using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_JoakaG.Models;


public class ProductosController : Controller
{

    private ProductoRepository productoRepository;
    public ProductosController()
    {
        productoRepository = new ProductoRepository();
    }

    public IActionResult Index()
    {
        return View(productoRepository.Listar());
    }

    public IActionResult Error()
    {
        return View();
    }

    public IActionResult Details(int id)
    {
        var producto = productoRepository.Listar().FirstOrDefault(p => p.IdProducto == id);

        if (producto == null)
            return NotFound();
        var prodViewModel = new ProductoViewModel(producto.Descripcion, producto.IdProducto);
        return View(prodViewModel);
    } 
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Producto nuevoProducto)
    {
        productoRepository.Crear(nuevoProducto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var producto = productoRepository.ObtenerDetalle(id);
        return View(producto);
    }
    [HttpPost]
    public IActionResult Edit(ProductoViewModel pvw)
    {
        productoRepository.ModificarProducto(pvw.Id, pvw.Descripcion);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var producto = productoRepository.ObtenerDetalle(id);
        return View(producto);
    }

    [HttpPost]
    public IActionResult DeleteC(int IdProducto)
    {

        if (productoRepository.Eliminar(IdProducto))
            return RedirectToAction("Index");
        else
            return RedirectToAction("Error");

    }
}