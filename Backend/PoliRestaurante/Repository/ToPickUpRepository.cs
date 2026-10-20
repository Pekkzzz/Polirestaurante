using PoliRestaurante.Data;
using PoliRestaurante.Models.Entity;

namespace Polirestaurante.Repository;

public class ToPickUpRepository : IToPickUpRepository
{
    private readonly ApplicationDbContext _db;


    public ToPickUpRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool CreateToPickUp(ToPickUp toPickUp)
    {
        _db.ToPickUps.Add(toPickUp);

        return _db.SaveChanges() > 0;
    }

    public ToPickUp? GetToPickUp(int id)
    {
        return _db.ToPickUps.FirstOrDefault(t => t.Id == id);
    }


    public bool UpdateToPickUp(ToPickUp toPickUp)
    {
        _db.ToPickUps.Update(toPickUp);

        return _db.SaveChanges() > 0;
    }

    public bool DeleteToPickUp(ToPickUp toPickUp)
    {
        _db.ToPickUps.Remove(toPickUp);

        return _db.SaveChanges() > 0;
    }

    public bool ToPickUpExists(int id)
    {
        return _db.ToPickUps.Any(t => t.Id == id);
    }


}
