using PoliRestaurante.Models;

public interface IUserRepository
{
  bool CreateUser(User User);
  User? GetUser(int id);
  bool UserExists(int id);
  bool UserExists(string username);
  bool UpdateUser(User User);
  bool DeleteUser(User User);
}