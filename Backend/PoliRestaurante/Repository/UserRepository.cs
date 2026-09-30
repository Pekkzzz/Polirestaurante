using PoliRestaurante.Data;
using PoliRestaurante.Models.Entity;

namespace Polirestaurante.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    //private readonly string _connectionString;

    /*
    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    */

  
    public UserRepository(ApplicationDbContext db)
    {
    _db = db;
    }
  

    public bool CreateUser(User user)
    {
        _db.Users.Add(user);

        return _db.SaveChanges() > 0;
    }

    public User? GetUser(int id)
    {
        return _db.Users.FirstOrDefault(u => u.Id == id);
    }

    public User? GetUser(string username)
    {
        return _db.Users.FirstOrDefault(u => u.Username == username);
    }

    public bool UpdateUser(User user)
    {
        _db.Users.Update(user);

        return _db.SaveChanges() > 0;
    }

    public bool DeleteUser(User user)
    {
        _db.Users.Remove(user);

        return _db.SaveChanges() > 0;
    }

    public bool UserExists(int id)
    {
        return _db.Users.Any(u => u.Id == id);
    }

    public bool UserExists(string name)
    {
        return _db.Users.Any(u =>
            u.Name.Trim().ToLower() == name.Trim().ToLower());
    }

}