using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public class PickUpStatusService_Response
{
    public bool Response;
    
    public string Description;
    public PickupStatus? pickUpStatus;

    public PickUpStatusService_Response(bool response, string description, PickupStatus? pickUpStatusObject)
    {
        Response = response;
        Description = description;
        pickUpStatus = pickUpStatusObject;
    
    }
    public PickUpStatusService_Response()
    {
        Response = false;
        Description = "";
        pickUpStatus = null;
    }
}