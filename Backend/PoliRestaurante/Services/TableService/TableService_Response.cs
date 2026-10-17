using PoliRestaurante.Models.Entity;

public class TableService_Response
{
    public bool Response;
    public string Description;
    public Table? Table;

    public TableService_Response(bool response, string description, Table? TableObject)
    {
        Response = response;
        Description = description;
        Table = TableObject;
    }

    public TableService_Response()
    {
        Response = false;
        Description = "";
        Table = null;
    }
}