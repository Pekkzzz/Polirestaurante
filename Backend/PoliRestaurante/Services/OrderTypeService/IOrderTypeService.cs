using PoliRestaurante.Models;

public interface IOrderTypeService
{
    public OrderTypeService_Response GetByID(int id);
    public OrderTypeService_Response GetByName(string name);
    public OrderTypeService_Response Create(OrderType_CreateDto newOrder);
    public OrderTypeService_Response Update(OrderType_UpdateDto Order_UpdateDto);
    public OrderTypeService_Response Delete(int id);
}