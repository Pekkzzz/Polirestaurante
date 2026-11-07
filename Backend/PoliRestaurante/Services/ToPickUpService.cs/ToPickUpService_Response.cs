using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public class ToPickUpService_Response
{
    public bool Response;
    public string Description;
    public ToPickUp? toPickUp;
    public List<ToPickUp>? toPickUps;

    public ToPickUpService_Response(bool response, string description, ToPickUp? toPickUpObject, List<ToPickUp>? toPickUpsObject)
    {
        Response = response;
        Description = description;
        toPickUp = toPickUpObject;
        toPickUps = toPickUpsObject;
    }

    public ToPickUpService_Response()
    {
        Response = false;
        Description = "";
        toPickUp = null;
        toPickUps = null;
    }
}