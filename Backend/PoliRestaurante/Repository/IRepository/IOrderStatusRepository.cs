using PoliRestaurante.Models.Entity;

public interface IOrderStatusRepository
{
  bool Create(OrderStatus OrderStatus);
  OrderStatus? Get(int id);
  OrderStatus? Get(string name);
  bool Exists(int id);
  bool Exists(string name);
  bool Update(OrderStatus OrderStatus);
  bool Delete(OrderStatus OrderStatus);
}