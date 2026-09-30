using PoliRestaurante.Models;

public interface ITableRepository
{
  bool CreateTable(Table table);
  Table? GetTable(int id);
  bool TableExists(int id);
  bool TableNumberExists(int tableNum);
  bool UpdateTable(Table table);
  bool DeleteTable(Table table);
}