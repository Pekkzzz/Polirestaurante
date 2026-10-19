using PoliRestaurante.Models.Entity;

public interface IOrderDetailRepository
{
  bool Create(OrderDetail OrderDetail);
  OrderDetail? Get(int id);
  List<OrderDetail> GetByOrderId(int id);
  bool Exists(int id);
  bool Update(OrderDetail OrderDetail);
  bool Delete(OrderDetail OrderDetail);
}