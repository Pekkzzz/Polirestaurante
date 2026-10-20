using PoliRestaurante.Models.Entity;

public interface IToPickUpRepository
{
    bool CreateToPickUp(ToPickUp toPickUp);
    ToPickUp? GetToPickUp(int id);
    bool UpdateToPickUp(ToPickUp toPickUp);
    bool DeleteToPickUp(ToPickUp toPickUp);
    bool ToPickUpExists(int id);
}