using PoliRestaurante.Models.Entity;

public class OrderDetailService_Response
{
    public bool Response;
    public string Description;
    public OrderDetail? OrderDetail;

    public OrderDetailService_Response(bool response, string description, OrderDetail? OrderDetailObject)
    {
        Response = response;
        Description = description;
        OrderDetail = OrderDetailObject;
    }

    public OrderDetailService_Response()
    {
        Response = false;
        Description = "";
        OrderDetail = null;
    }
}