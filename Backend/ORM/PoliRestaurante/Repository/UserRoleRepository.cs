using PoliRestaurante.Data;
using PoliRestaurante.Models.Entity;

namespace Polirestaurante.Repository;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly ApplicationDbContext _db;

    public UserRoleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool CreateUserRole(UserRole userRole)
    {
        _db.UserRoles.Add(userRole);

        return _db.SaveChanges() > 0;
    }

    public UserRole? GetUserRole(int id)
    {
        return _db.UserRoles.FirstOrDefault(ur => ur.Id == id);
    }

    public UserRole? GetUserRole(string name)
    {
        return _db.UserRoles.FirstOrDefault(ur => ur.Name == name);
    }

    public bool UpdateUserRole(UserRole userRole)
    {
        _db.UserRoles.Update(userRole);

        return _db.SaveChanges() > 0;
    }

    public bool DeleteUserRole(UserRole userRole)
    {
        _db.UserRoles.Remove(userRole);

        return _db.SaveChanges() > 0;
    }

    public bool UserRoleExists(int id)
    {
        return _db.UserRoles.Any(ur => ur.Id == id);
    }

    public bool UserRoleExists(string name)
    {
        return _db.UserRoles.Any(ur =>
            ur.Name.Trim().ToLower() == name.Trim().ToLower());
    }
}