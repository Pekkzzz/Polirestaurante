using PoliRestaurante.Models.Entity;

public class OrderTypeService_Response
{
    public bool Response;
    public string Description;
    public OrderType? OrderType;
    public List<OrderType>? OrderTypes;

    public OrderTypeService_Response(bool response, string description, OrderType? OrderTypeObject, List<OrderType>? orderTypes)
    {
        Response = response;
        Description = description;
        OrderType = OrderTypeObject;
        OrderTypes = orderTypes;
    }

    public OrderTypeService_Response()
    {
        Response = false;
        Description = "";
        OrderType = null;
        OrderTypes = null;
    }

}