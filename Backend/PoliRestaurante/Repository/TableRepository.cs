using PoliRestaurante.Data;
using PoliRestaurante.Models.Entity;

namespace Polirestaurante.Repository;

public class TableRepository : ITableRepository
{
    private readonly ApplicationDbContext _db; 
    
    public TableRepository(ApplicationDbContext db) 
    { 
        _db = db; 
    }

    public bool CreateTable (Table table)
    {
        _db.Tables.Add(table);

        return _db.SaveChanges() > 0;
    }

    public Table? GetTable (int id)
    {
        return _db.Tables.FirstOrDefault(t => t.Id == id);
    }

    public bool UpdateTable (Table table)
    {
        _db.Tables.Update(table);

        return _db.SaveChanges() > 0;
    }

    public bool DeleteTable (Table table)
    {
        _db.Tables.Remove(table);

        return _db.SaveChanges() > 0;
    }

    public bool TableExists(int id)
    {
        return _db.Tables.Any(t => t.Id == id);
    }

    public bool TableNumberExists (int tableNum)
    {
         return _db.Tables.Any(t => t.TableNum == tableNum);
    }

   
}