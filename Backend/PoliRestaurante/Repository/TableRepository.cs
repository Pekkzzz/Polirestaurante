using Microsoft.Data.SqlClient;
using PoliRestaurante.Models;

namespace Polirestaurante.Repository;

public class TableRepository : ITableRepository
{
    private readonly string _connectionString;

    public TableRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public bool CreateTable(Table table)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);

        connection.Open();

        string query = """
            INSERT INTO [Table] (TableNum, Capacity)
            VALUES (@TableNum, @Capacity)
            """;

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@TableNum", table.TableNum);
        command.Parameters.AddWithValue("@Capacity", table.Capacity);

        int rowsAffected = command.ExecuteNonQuery();

        return rowsAffected > 0;
    }

    public Table? GetTable(int id)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);

        connection.Open();

        string query = """
            SELECT ID, TableNum, Capacity
            FROM [Table]
            WHERE ID = @Id
            """;

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Table
            {
                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                TableNum = reader.GetInt32(reader.GetOrdinal("TableNum")),
                Capacity = reader.GetInt32(reader.GetOrdinal("Capacity"))
            };
        }

        return null;
    }

    public bool UpdateTable(Table table)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);

        connection.Open();

        string query = """
            UPDATE [Table]
            SET TableNum = @TableNum,
                Capacity = @Capacity
            WHERE ID = @Id
            """;

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", table.Id);
        command.Parameters.AddWithValue("@TableNum", table.TableNum);
        command.Parameters.AddWithValue("@Capacity", table.Capacity);

        int rowsAffected = command.ExecuteNonQuery();

        return rowsAffected > 0;
    }

    public bool DeleteTable(Table table)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);

        connection.Open();

        string query = """
            DELETE FROM [Table]
            WHERE ID = @Id
            """;

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", table.Id);

        int rowsAffected = command.ExecuteNonQuery();

        return rowsAffected > 0;
    }

    public bool TableExists(int id)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);

        connection.Open();

        string query = """
            SELECT COUNT(1)
            FROM [Table]
            WHERE ID = @Id
            """;

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

        int count = (int)command.ExecuteScalar();

        return count > 0;
    }

    public bool TableNumberExists(int tableNum)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);

        connection.Open();

        string query = """
            SELECT COUNT(1)
            FROM [Table]
            WHERE TableNum = @TableNum
            """;

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@TableNum", tableNum);

        int count = (int)command.ExecuteScalar();

        return count > 0;
    }
}