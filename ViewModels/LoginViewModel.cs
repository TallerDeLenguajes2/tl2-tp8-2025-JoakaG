
using System.ComponentModel.DataAnnotations;
public class LoginViewModel
{
      [Required(ErrorMessage = "El Usuario es Obligatorio")]
    public string Username {get; set;}
    [Required(ErrorMessage = "La contraseña es Obligatoria")]
    public string Password {get; set;}
    public string ?ErrorMessage {get; set;}
}