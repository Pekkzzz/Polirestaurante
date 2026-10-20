using PoliRestaurante.Models.Entity;

public interface IPickUpStatusService
{
    public PickUpStatusService_Response Create(PickupStatus_CreateDto pickUpStatus);
    public PickUpStatusService_Response GetByID(int id);
    public PickUpStatusService_Response Update(PickUpStatus_UpdateDto pickUpStatus);
    public PickUpStatusService_Response Delete(int id);

}