using PoliRestaurante.Data;
using PoliRestaurante.Models.Entity;

namespace PoliRestaurante.Repository;

public class PickUpStatusRepository : IPickUpStatusRepository
{
    private readonly ApplicationDbContext _db;

    public PickUpStatusRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool CreatePickUpStatus(PickupStatus pickUpStatus)
    {
        _db.PickUpStatuses.Add(pickUpStatus);

        return _db.SaveChanges() > 0;
    }

    public PickupStatus? GetPickUpStatus(int id)
    {
        return _db.PickUpStatuses.FirstOrDefault(t => t.Id == id);
    }

    public bool UpdatePickUpStatus(PickupStatus pickUpStatus)
    {
        _db.PickUpStatuses.Update(pickUpStatus);

        return _db.SaveChanges() > 0;
    }

    public bool DeletePickUpStatus(PickupStatus pickUpStatus)
    {
        _db.PickUpStatuses.Remove(pickUpStatus);

        return _db.SaveChanges() > 0;
    }

    public bool PickUpStatusExists(int id)
    {
        return _db.PickUpStatuses.Any(t => t.Id == id);
    }
}