using PoliRestaurante.Models.Entity;

public interface IOrderRepository
{
  bool Create(Order Order);
  Order? Get(int id);
  bool Exists(int id);
  bool Update(Order Order);
  bool Delete(Order Order);

  // cola de pedidos
  List<Order> GetQueue();
}