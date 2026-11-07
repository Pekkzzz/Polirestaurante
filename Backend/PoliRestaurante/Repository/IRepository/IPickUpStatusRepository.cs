using PoliRestaurante.Models.Entity;
public interface IPickUpStatusRepository
{
    bool CreatePickUpStatus(PickupStatus pickUpStatus);
    PickupStatus? GetPickUpStatus(int id);
    List<PickupStatus>? GetAll();
    bool UpdatePickUpStatus(PickupStatus pickUpStatus);
    bool DeletePickUpStatus(PickupStatus pickUpStatus);
    bool PickUpStatusExists(int id);
    
}