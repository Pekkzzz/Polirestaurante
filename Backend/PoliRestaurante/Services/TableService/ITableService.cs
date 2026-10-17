using PoliRestaurante.Models;

public interface ITableService
{
    public TableService_Response GetByID(int id);
    public TableService_Response GetByNumber(int number);
    public TableService_Response Create(Table_CreateDto newTable);
    public TableService_Response Update(Table_UpdateDto Table_UpdateDto);
    public TableService_Response Detele(int id);
}