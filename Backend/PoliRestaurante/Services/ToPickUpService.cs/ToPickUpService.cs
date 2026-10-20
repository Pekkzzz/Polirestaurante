using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PoliRestaurante.Models.Entity;

public class ToPickUpService : IToPickUpService
{
    private readonly IToPickUpRepository _repository;



    private string? secretKey;

    public ToPickUpService(IToPickUpRepository repository, IToPickUpService toPickUpService, IConfiguration configuration)
    {
        _repository = repository;
        _toPickUpService = toPickUpService;
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

        if (!_repository.UpdateToPickUp(toPickUp_UpdateDto.Id, toPickUp_UpdateDto))
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

        if (!response.Response || response.ToPickUp == null)
        {
            response.Response = false;
            response.Description = "ToPickUp not found";
            response.toPickUp = null;
            return response;
        }
}
}
    