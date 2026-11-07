using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public class PickUpStatusService_Response
{
    public bool Response;
    
    public string Description;
    public PickupStatus? pickUpStatus;
    public List<PickupStatus>? pickupStatuses;

    public PickUpStatusService_Response(bool response, string description, PickupStatus? pickUpStatusObject, List<PickupStatus>? pickupStatusesObject)
    {
        Response = response;
        Description = description;
        pickUpStatus = pickUpStatusObject;
        pickupStatuses = pickupStatusesObject;
    
    }
    public PickUpStatusService_Response()
    {
        Response = false;
        Description = "";
        pickUpStatus = null;
        pickupStatuses = null;
    }
}