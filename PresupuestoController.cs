using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
public class PresupuestoController : ControllerBase
{
    private PresupuestoRepository presupuestoRepository;
    public PresupuestoController()
    {
        presupuestoRepository = new PresupuestoRepository();
    }

    [HttpPost("Presupuesto")]
    public IActionResult AltaPresupuesto(Presupuesto nuevoPresupuesto)
    {
        presupuestoRepository.Crear(nuevoPresupuesto);
        return Ok("Presupuesto dado de alta exitosamente");
    }
    [HttpGet("Presupuesto")]
    public IActionResult GetPresupuesto()
    {
        return Ok(presupuestoRepository.Listar());
    }
    [HttpPost("Presupuesto/{idPresupuesto}")]
    public IActionResult agregarProducto(int idPresupuesto, int idProducto, int cantidad)
    {
        if (presupuestoRepository.agregarProductoAPresupuesto(idPresupuesto, idProducto, cantidad))
            return Created();
        else
            return BadRequest($"No se pudo agregar el producto de id '{idProducto}' de cantidad {cantidad} al presupuesto de id {idPresupuesto}");
    }

    [HttpGet("Presupuesto/{idPresupuesto}")]
    public IActionResult obtenerPresupuesto(int idPresupuesto)
    {
        return Ok(presupuestoRepository.ObtenerDetalle(idPresupuesto));
    }

    [HttpDelete("Producto")]
    public IActionResult borrarPresupuesto(int id)
    {
        if (presupuestoRepository.Eliminar(id))
            return NoContent();
        else
            return BadRequest($"No se pudo borrar el presupuesto de Id '{id}' porfavor inténtelo nuevamente, asegurese que existe");
    }
}

