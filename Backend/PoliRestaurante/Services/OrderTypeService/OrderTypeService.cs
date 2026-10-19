using PoliRestaurante.Models.Entity;

public class OrderTypeService : IOrderTypeService
{
    private readonly IOrderTypeRepository _repository;

    //Services

    public OrderTypeService(IOrderTypeRepository repository)
    {
        _repository = repository;

    }

    public OrderTypeService_Response Create(OrderType_CreateDto OrderType_CreateDto)
    {
        OrderTypeService_Response response = new OrderTypeService_Response();
        OrderType newOrderType = OrdersMapper.OrderTypeToEnity(OrderType_CreateDto);
        
        
        if (_repository.Exists(newOrderType.Name))
        {
            response.Response = false;
            response.Description = "Order Already Exists";
            response.OrderType = null;
            return response;
        }

        if(_repository.Create(newOrderType))
        {
            response.Response = true;
            response.Description = "Order Type Created";
            response.OrderType = newOrderType;
            return response;
        }
        else
        {
            response.Response = false;
            response.Description = "Order Type Cant Be Created by a System error";
            response.OrderType = null;
            return response;
        }

    }

    public OrderTypeService_Response GetByID(int id)
    {
        OrderTypeService_Response response = new OrderTypeService_Response();

        OrderType? OrderType = _repository.Get(id);
        if (OrderType == null)
        {
            response.Description = "Order id cant be finded";
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "Order Finded";
            response.OrderType = OrderType;
            return response;
        }
    }

    public OrderTypeService_Response GetByName(string name)
    {
        OrderTypeService_Response response = new OrderTypeService_Response();

        OrderType? OrderType = _repository.Get(name);
        if (OrderType == null)
        {
            response.Description = "Order name cant be finded";
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "Order Finded";
            response.OrderType = OrderType;
            return response;
        }
    }

    public OrderTypeService_Response Update(OrderType_UpdateDto OrderType_UpdateDto)
    {
        OrderTypeService_Response response = new OrderTypeService_Response();
        if (!_repository.Exists(OrderType_UpdateDto.Id))
        {
            response.Response = false;
            response.Description = "Order Type id cant be finded";
            response.OrderType = null;
            return response;
        }

        OrderType OrderType = OrdersMapper.OrderTypeToEnity(OrderType_UpdateDto);

        if(_repository.Update(OrderType))
        {
            response.Response = true;
            response.Description = "Order Type updated";
            response.OrderType = OrderType;
            return response;
        }
        else
        {
            response.Response = false;
            response.Description = "Order Type cant be updated by a system error";
            response.OrderType = null;
            return response;
        }
        
    }

    public OrderTypeService_Response Detele(int id)
    {
        OrderTypeService_Response OrderTypeService_Response = GetByID(id);
        if (!OrderTypeService_Response.Response)
        {
            return OrderTypeService_Response;
        }

        if (OrderTypeService_Response.OrderType == null)
        {
            OrderTypeService_Response.Response = false;
            OrderTypeService_Response.Description = "Something is wrong";
            OrderTypeService_Response.OrderType = null;
            return OrderTypeService_Response;
        }
        else
        {
            OrderTypeService_Response.Response = _repository.Delete(OrderTypeService_Response.OrderType);
            if (!OrderTypeService_Response.Response)
            {
                OrderTypeService_Response.Description = "User Cant Be Deleted";
                OrderTypeService_Response.OrderType = null;
                return OrderTypeService_Response;
            }
            else
            {
                return OrderTypeService_Response;
            }
            
        }

        
    }



    //----Utility----
    private OrderTypeService_Response ComprobateOrder(OrderType newOrder)
    {
        
        return new OrderTypeService_Response(true, "User Role is available", newOrder, null);
    }
}