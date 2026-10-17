using PoliRestaurante.Models.Entity;

public interface IPaymentStatusRepository
{
  bool Create(PaymentStatus PaymentStatus);
  PaymentStatus? Get(int id);
  PaymentStatus? Get(string name);
  bool Exists(int id);
  bool Exists(string name);
  bool Update(PaymentStatus PaymentStatus);
  bool Delete(PaymentStatus PaymentStatus);
}