using PoliRestaurante.Models.Entity;

public interface IToPickUpService
{
    public ToPickUpService_Response Create(ToPickUp_CreateDto toPickUp);
    public ToPickUpService_Response GetByID(int id);
    public ToPickUpService_Response Update(ToPickUp_UpdateDto toPickUp);
    public ToPickUpService_Response Delete(int id);
}