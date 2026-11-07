using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public static class PickUpMapper
{
    public static PickupStatus PickupStatusToEnity(PickUpStatus_Dto pickUpStatus)
    {
        return new PickupStatus
        {
            Name = pickUpStatus.Name,
            Description = pickUpStatus.Description
        };
    }

    public static PickUpStatus_Dto PickupStatusToDto(PickupStatus pickUpStatus)
    {
        return new PickUpStatus_Dto
        {
            Id = pickUpStatus.Id, 
            Name = pickUpStatus.Name,
            Description = pickUpStatus.Description
        };
    }

    public static ToPickUp ToPickupToEnity(ToPickUp_Dto toPickUp)
    {
        return new ToPickUp
        {
            Address = toPickUp.Address,
            CreationTime = toPickUp.CreationTime,
            CompletedTime = toPickUp.CompletedTime,
            PickupStatusId = toPickUp.PickupStatusId,
            UserId = toPickUp.UserId,
            OrderId = toPickUp.OrderId
        };
    }

    public static ToPickUp_Dto ToPickupStatusToDto(ToPickUp toPickUp)
    {
        return new ToPickUp_Dto
        {
            Id = toPickUp.Id,
            Address = toPickUp.Address,
            CreationTime = toPickUp.CreationTime,
            CompletedTime = toPickUp.CompletedTime,
            PickupStatusId = toPickUp.PickupStatusId,
            UserId = toPickUp.UserId,
            OrderId = toPickUp.OrderId
        };
    }


}