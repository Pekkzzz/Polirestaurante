using PoliRestaurante.Models.Entity;

public class OrderService_Response
{
    public bool Response;
    public string Description;
    public Order? Order;

    public OrderService_Response(bool response, string description, Order? OrderObject)
    {
        Response = response;
        Description = description;
        Order = OrderObject;
    }

    public OrderService_Response()
    {
        Response = false;
        Description = "";
        Order = null;
    }
}