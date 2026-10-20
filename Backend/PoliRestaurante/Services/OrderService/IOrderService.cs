using PoliRestaurante.Models;

public interface IOrderService
{
    public OrderService_Response GetByID(int id);
    public OrderService_Response Create(Order_CreateDto newOrder);
    public OrderService_Response Update(Order_UpdateDto Order_UpdateDto);
    public OrderService_Response Delete(int id);
}