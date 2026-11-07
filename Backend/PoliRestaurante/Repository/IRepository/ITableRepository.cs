
using PoliRestaurante.Models.Entity;

public interface ITableRepository
{
  bool CreateTable(Table table);
  Table? GetTable(int id);
  Table? GetTableByNumber(int id);
  List<Table>? GetAll();
  bool TableExists(int id);
  bool TableNumberExists(int tableNum);
  bool UpdateTable(Table table);
  bool DeleteTable(Table table);
}