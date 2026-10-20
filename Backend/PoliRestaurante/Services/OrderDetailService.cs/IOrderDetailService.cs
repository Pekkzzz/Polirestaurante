using PoliRestaurante.Models;

public interface IOrderDetailService
{
    public OrderDetailService_Response GetByID(int id);
    public OrderDetailService_Response Create(OrderDetail_CreateDto newOrderDetail);
    public OrderDetailService_Response Update(OrderDetail_UpdateDto OrderDetail_UpdateDto);
    public OrderDetailService_Response Delete(int id);
}