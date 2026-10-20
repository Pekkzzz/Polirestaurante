using PoliRestaurante.Models.Entity;
public interface IPickUpStatusRepository
{
    bool CreatePickUpStatus(PickupStatus pickUpStatus);
    PickupStatus? GetPickUpStatus(int id);
    bool UpdatePickUpStatus(PickupStatus pickUpStatus);
    bool DeletePickUpStatus(PickupStatus pickUpStatus);
    bool PickUpStatusExists(int id);
    
}