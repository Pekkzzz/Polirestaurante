using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public class ToPickUpService_Response
{
    public bool Response;
    public string Description;
    public ToPickUp? toPickUp;

    public ToPickUpService_Response(bool response, string description, ToPickUp? toPickUpObject)
    {
        Response = response;
        Description = description;
        toPickUp = toPickUpObject;
    }

    public ToPickUpService_Response()
    {
        Response = false;
        Description = "";
        toPickUp = null;
    }
}