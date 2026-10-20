using PoliRestaurante.Models.Entity;

public class ToPickUpService : IToPickUpService
{
    private readonly IToPickUpRepository _repository;
    private string? secretKey;

    public ToPickUpService(IToPickUpRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        secretKey = configuration.GetValue<string>("ApiSettings:Secret");
    }

    public ToPickUpService_Response Create(ToPickUp_CreateDto newToPickUp)
    {
        ToPickUp toPickUp = new ToPickUp
        {
            Address = newToPickUp.Address,
            PickupStatusId = newToPickUp.PickupStatusId,
            UserId = newToPickUp.UserId,
            OrderId = newToPickUp.OrderId
        };

        ToPickUpService_Response response = new ToPickUpService_Response();

        if (_repository.CreateToPickUp(toPickUp))
        {
            response.Response = true;
            response.Description = "ToPickUp created successfully";
            response.toPickUp = toPickUp;
        }
        else
        {
           response.Response = false;
            response.Description = "Failed to create ToPickUp";
            response.toPickUp = toPickUp;
        }

        return response;
    }

    public ToPickUpService_Response GetByID(int id)
    {
        ToPickUpService_Response response = new ToPickUpService_Response();

        if (id <= 0)
        {
            response.Response = false;
            response.Description = "ToPickUp id cant be negative or 0";
            return response;
        }

        ToPickUp? toPickUp = _repository.GetToPickUp(id);
        if (toPickUp == null)
        {
            response.Response = false;
            response.Description = "ToPickUp not found";
            return response;
        }

        response.Response = true;
        response.Description = "ToPickUp found";
        response.toPickUp = toPickUp;
        return response;
    }


    public ToPickUpService_Response Update(ToPickUp_UpdateDto toPickUp_UpdateDto)
    {
        ToPickUpService_Response response = new ToPickUpService_Response();

        ToPickUp toPickUp = new ToPickUp
        {
            Id = toPickUp_UpdateDto.Id,

            Address = toPickUp_UpdateDto.Address,

            CreationTime = toPickUp_UpdateDto.CreationTime,

            CompletedTime = toPickUp_UpdateDto.CompletedTime,

            PickupStatusId = toPickUp_UpdateDto.PickupStatusId,

            UserId = toPickUp_UpdateDto.UserId,

            OrderId = toPickUp_UpdateDto.OrderId
        };

        if (!_repository.UpdateToPickUp(toPickUp))
        {
            response.Response = true;
            response.Description = "ToPickUp updated successfully";
            return response;
        }
        else
        {
            response.Response = false;
            response.Description = "Failed to update ToPickUp";
            return response;
        }
    }

    public ToPickUpService_Response Delete(int id)
    {
        ToPickUpService_Response response = new ToPickUpService_Response();

        
        ToPickUp toPickUp = _repository.GetToPickUp(id);
        if (toPickUp == null)
        {
            response.Response = false;
            response.Description = "ToPickUp not found";
            response.toPickUp = null;
            return response;
        }
        
        if (_repository.DeleteToPickUp(toPickUp))
        {
            response.Response = true;
            response.Description = "ToPickUp deleted";
            response.toPickUp = null;
            return response;
        }
        else
        {
            response.Response = false;
            response.Description = "ToPickUp ncant be deleted";
            response.toPickUp = null;
            return response;
        }





        
}
}
    