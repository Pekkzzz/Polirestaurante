using Polirestaurante.Models;

public interface IToPickUpService
{
    public ToPickUpService_Response CreateToPickUp(ToPickUp toPickUp);
    public ToPickUpService_Response GetToPickUp(int id);
    public ToPickUpService_Response GetToPickUp(string name);
    public ToPickUpService_Response UpdateToPickUp(ToPickUp toPickUp);
    public ToPickUpService_Response DeleteToPickUp(int id);
}