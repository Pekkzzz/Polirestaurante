using PoliRestaurante.Models.Entity;

public class TableService_Response
{
    public bool Response;
    public string Description;
    public Table? Table;
    public List<Table>? Tables;

    public TableService_Response(bool response, string description, Table? TableObject, List<Table>? tablesObject)
    {
        Response = response;
        Description = description;
        Table = TableObject;
        Tables = tablesObject;
    }

    public TableService_Response()
    {
        Response = false;
        Description = "";
        Table = null;
        Tables = null;
    }
}