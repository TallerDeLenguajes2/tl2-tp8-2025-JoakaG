namespace MVC.Interfaces;
public interface IUsuarioRepository
{
 Usuario ?GetUser(string username, string password);
}
