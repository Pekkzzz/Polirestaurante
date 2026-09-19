using PoliRestaurante.Models;

public interface IUserRoleRepository
{
  bool CreateUserRole(UserRole userRole);
  UserRole? GetUserRole(int id);
  bool UserRoleExists(int id);
  bool UserRoleExists(string name);
  bool UpdateUserRole(UserRole userRole);
  bool DeleteUserRole(UserRole userRole);
}