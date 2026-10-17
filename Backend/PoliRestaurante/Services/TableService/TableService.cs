using PoliRestaurante.Models.Entity;

public class TableService : ITableService
{
    private readonly ITableRepository _repository;

    public TableService(ITableRepository Repository)
    {
        _repository = Repository;
    }

    public TableService_Response Create(Table_CreateDto order_CreateDto)
    {
        Table newTable = TablesMapper.TableToEnity(order_CreateDto);
        TableService_Response response = ComprobateTable(newTable);
        if (!response.Response)
        {
            return response;
        }

        if (_repository.TableNumberExists(newTable.TableNum))
        {
            response.Response = false;
            response.Description = "Table Cant be created: Table already exists";
            response.Table = null;
            return response;
        }

        if (_repository.CreateTable(newTable))
        {
            response.Response = true;
            response.Description = "Table created";
            response.Table = newTable;
            return response;    
        }
        else
        {
            response.Response = false;
            response.Description = "Internal Error: Somethng is wrong when system try to create a new order";
            response.Table = null;
            return response;
        }
        

    }

    public TableService_Response GetByID(int id)
    {
        TableService_Response response = new TableService_Response();

        if (id <= 0)
        {
            response.Description = "Table id cant be negative or 0";
            return response;
        }

        Table? Table = _repository.GetTable(id);
        if (Table == null)
        {
            response.Description = "Table id cant be finded";
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "Table Finded";
            response.Table = Table;
            return response;
        }
    }

    public TableService_Response GetByNumber(int number)
    {
        TableService_Response response = new TableService_Response();

        Table? Table = _repository.GetTableByNumber(number);
        if (Table == null)
        {
            response.Description = "Table number cant be finded";
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "Table Finded";
            response.Table = Table;
            return response;
        }
    }

    public TableService_Response Update(Table_UpdateDto order_UpdateDto)
    {
        TableService_Response TableService_Response = new TableService_Response();
        if (!_repository.TableExists(order_UpdateDto.Id))
        {
            TableService_Response.Response = false;
            TableService_Response.Description = "Table id cant be finded";
            TableService_Response.Table = null;
            return TableService_Response;
        }

        Table table = TablesMapper.TableToEnity(order_UpdateDto);

        TableService_Response.Response = _repository.UpdateTable(table);
        if (!TableService_Response.Response)
        {
            TableService_Response.Table = null;
            TableService_Response.Description = "Table cant be updated";
            return TableService_Response;
        }
        
        TableService_Response = GetByID(order_UpdateDto.Id);
        TableService_Response.Description = "Table Updated";
        TableService_Response.Table = table;
        return TableService_Response;
    }

    public TableService_Response Detele(int id)
    {
        TableService_Response TableService_Response = GetByID(id);
        if (!TableService_Response.Response)
        {
            return TableService_Response;
        }
        if (TableService_Response.Table == null)
        {
            TableService_Response.Response = false;
            TableService_Response.Description = "Something is wrong";
            TableService_Response.Table = null;
            return TableService_Response;
        }
        else
        {
            TableService_Response.Response = _repository.DeleteTable(TableService_Response.Table);
            if (!TableService_Response.Response)
            {
                TableService_Response.Description = "User Cant Be Deleted";
                TableService_Response.Table = null;
                return TableService_Response;
            }
            else
            {
                return TableService_Response;
            }
            
        }

        
    }

    //----Utility----
    private TableService_Response ComprobateTable(Table newTable)
    {
        
        return new TableService_Response(true, "User Role is available", null);
    }
}