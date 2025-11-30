namespace MVC.Interfaces;
public interface IAuthenticationServices
{
 bool Login(string username, string password);
 void Logout();
 bool IsAuthenticated();
 // Verifica si el usuario actual tiene el rol requerido (ej. "Administrador").
 public bool HasAccessLevel(string requiredAccessLevel);
}