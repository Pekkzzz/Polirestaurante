using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public static class TablesMapper
{

    public static Table TableToEnity(Table_CreateDto table)
    {
        return new Table
        {
            TableNum = table.TableNum,
            Capacity = table.Capacity
        };
    }

    public static Table TableToEnity(Table_UpdateDto table)
    {
        return new Table
        {
            Id = table.Id,
            TableNum = table.TableNum,
            Capacity = table.Capacity
        };
    }

    public static Table_Dto TableToDto(Table table)
    {
        return new Table_Dto
        {
            Id = table.Id, 
            TableNum = table.TableNum,
            Capacity = table.Capacity
        };
    }


}