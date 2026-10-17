using PoliRestaurante.Models.Entity;

public interface IOrderDetailStatusRepository
{
  bool Create(OrderDetailStatus OrderDetailStatus);
  OrderDetailStatus? Get(int id);
  List<OrderDetailStatus> GetOrderDetailStatuses();
  bool Exists(int id);
  bool Update(OrderDetailStatus OrderDetailStatus);
  bool Delete(OrderDetailStatus OrderDetailStatus);
}