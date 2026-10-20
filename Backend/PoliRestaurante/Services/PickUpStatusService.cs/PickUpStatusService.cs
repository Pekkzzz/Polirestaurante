using PoliRestaurante.Models.Entity;

public class PickUpStatusService: IPickUpStatusService
{
    private readonly IPickUpStatusRepository _repository;

    private string? secretKey;

    public PickUpStatusService(IPickUpStatusRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        secretKey = configuration.GetValue<string>("ApiSettings:Secret");
    }

    public PickUpStatusService_Response Create(PickupStatus_CreateDto newPickUpStatus)
    {
        PickupStatus pickUpStatus = new PickupStatus
        {
            Name = newPickUpStatus.Name,
            Description = newPickUpStatus.Description
        };

        PickUpStatusService_Response response = new PickUpStatusService_Response();

        if (_repository.CreatePickUpStatus(pickUpStatus))
        {
            response.Response = true;
            response.Description = "PickUpStatus created successfully";
            response.pickUpStatus = pickUpStatus;
        }
        else
        {
           response.Response = false;
            response.Description = "Failed to create PickUpStatus";
            response.pickUpStatus = pickUpStatus;
        }

        return response;
    }

    public PickUpStatusService_Response GetByID(int id)
    {
        PickUpStatusService_Response response = new PickUpStatusService_Response();

        if (id <= 0)
        {
            response.Response = false;
            response.Description = "PickUpStatus id cant be negative or 0";
            return response;
        }

        PickupStatus? pickUpStatus = _repository.GetPickUpStatus(id);
        if (pickUpStatus == null)
        {
            response.Response = false;
            response.Description = "PickUpStatus not found";
            return response;
        }

        response.Response = true;
        response.Description = "PickUpStatus found";
        response.pickUpStatus = pickUpStatus;

        return response;
    }

    public PickUpStatusService_Response Update(PickUpStatus_UpdateDto pickUpStatus_UpdateDto)
    {
        PickUpStatusService_Response response = new PickUpStatusService_Response();
        PickupStatus pickUpStatus = new PickupStatus
        {
            Id = pickUpStatus_UpdateDto.Id,
            Name = pickUpStatus_UpdateDto.Name,
            Description = pickUpStatus_UpdateDto.Description 

        };

        if (!_repository.UpdatePickUpStatus(pickUpStatus))
        {
            response.Response = true;
            response.Description = "PickUpStatus updated successfully";
            return response;
        }

        else
        {
            response.Response = false;
            response.Description = "Failed to update PickUpStatus";
            return response;
        }

    }
         public PickUpStatusService_Response Delete(int id)
    {
        PickUpStatusService_Response response = new PickUpStatusService_Response();
        PickupStatus pickupStatus = _repository.GetPickUpStatus(id);

        if (pickupStatus == null)
        {
            response.Response = false;
            response.Description = "PickUpStatus not found";
            response.pickUpStatus = null;
            return response;
        }

        if (!_repository.DeletePickUpStatus(pickupStatus))
        {
            response.Response = true;
            response.Description = "PickUpStatus deleted successfully";
            response.pickUpStatus = null;
            return response;
        }

        else
        {
            response.Response = false;
            response.Description = "Failed to delete PickUpStatus";
            response.pickUpStatus = null;
            return response;
        }
    } 
}