using PoliRestaurante.Models.Entity;

public interface IOrderTypeRepository
{
  bool Create(OrderType OrderType);
  OrderType? Get(int id);
  OrderType? Get(string name);
  bool Exists(int id);
  bool Exists(string name);
  bool Update(OrderType OrderType);
  bool Delete(OrderType OrderType);
}