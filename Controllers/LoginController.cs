// Crear un controlador LoginController con métodos para mostrar la vista de login y
// manejar las solicitudes POST.
// •Acción Index: Muestra la página de login.
// •Acción Login: Procesa las credenciales y maneja la autenticación.
// •Acción Logout: Cierra la sesión del usuario y redirige al login.

// implementación del LoginController.cs
using Microsoft.AspNetCore.Mvc;
using MVC.Interfaces;

public class LoginController : Controller
{
    private readonly IAuthenticationServices _authenticationService;
    public LoginController(IAuthenticationServices authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }
    // [HttpPost] Procesa el login
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
        {
            model.ErrorMessage = "Debe ingresar usuario y contraseña.";
            return View("Index", model);
        }
        if (_authenticationService.Login(model.Username, model.Password))
        {
            return RedirectToAction("Index", "Home");
        }
        model.ErrorMessage = "Credenciales inválidas.";
        return View("Index", model);
    }
    // [HttpGet] Cierra sesión

    [HttpGet]
    public IActionResult Logout()
    {
        _authenticationService.Logout();
        return RedirectToAction("Index");
    }
}