using PoliRestaurante.Models.Entity;

public interface IUserRepository
{
  bool CreateUser(User User);
  User? GetUser(int id);
  User? GetUser(string name);
  bool UserExists(int id);
  bool UserExists(string username);
  bool UpdateUser(User User);
  bool DeleteUser(User User);
}