using Microsoft.Data.SqlClient;
using PoliRestaurante.Models;

namespace Polirestaurante.Repository;

public class ReservationStatusRepository : IReservationStatusRepository
{
  private readonly string _connectionString;


public bool CreateReservationStatus(ReservationStatus reservationStatus)
  {
    using SqlConnection connection = new SqlConnection(_connectionString);

    connection.Open();

    string query = """
        INSERT INTO ReservationStatus (Name, Description)
        VALUES (@Name, @Description)
        """;

    using SqlCommand command = new SqlCommand(query, connection);
    
    command.Parameters.AddWithValue("@Name", reservationStatus.Name);
    command.Parameters.AddWithValue("@Description", reservationStatus.Description);

    int rowsAffected = command.ExecuteNonQuery();

    return rowsAffected > 0;
  }

public ReservationStatus? GetReservationStatus(int id)
{
    using SqlConnection connection = new SqlConnection(_connectionString);
    connection.Open();

    string query = "SELECT Id, Name, Description FROM ReservationStatus WHERE Id = @Id";

    using SqlCommand command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@Id", id);

    using SqlDataReader reader = command.ExecuteReader();
    if (reader.Read())
    {
        return new ReservationStatus
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Description = reader.GetString(reader.GetOrdinal("Description"))
        };
    }

    return null;
}

public bool ReservationStatusExists(int id)
{
    using SqlConnection connection = new SqlConnection(_connectionString);
    connection.Open();

    string query = "SELECT COUNT(1) FROM ReservationStatus WHERE Id = @Id";

    using SqlCommand command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@Id", id);

    int count = (int)command.ExecuteScalar();
    return count > 0;
}

public bool ReservationStatusExists(string name)
{
    using SqlConnection connection = new SqlConnection(_connectionString);
    connection.Open();

    string query = "SELECT COUNT(1) FROM ReservationStatus WHERE Name = @Name";

    using SqlCommand command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@Name", name);

    int count = (int)command.ExecuteScalar();
    return count > 0;
}

public bool UpdateReservationStatus(ReservationStatus reservationStatus)
{
    using SqlConnection connection = new SqlConnection(_connectionString);
    connection.Open();

    string query = """
        UPDATE ReservationStatus
        SET Name = @Name, Description = @Description
        WHERE Id = @Id
        """;

    using SqlCommand command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@Id", reservationStatus.Id);
    command.Parameters.AddWithValue("@Name", reservationStatus.Name);
    command.Parameters.AddWithValue("@Description", reservationStatus.Description);

    int rowsAffected = command.ExecuteNonQuery();
    return rowsAffected > 0;
}

public bool DeleteReservationStatus(ReservationStatus reservationStatus)
{
    using SqlConnection connection = new SqlConnection(_connectionString);
    connection.Open();

    string query = "DELETE FROM ReservationStatus WHERE Id = @Id";

    using SqlCommand command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@Id", reservationStatus.Id);

    int rowsAffected = command.ExecuteNonQuery();
    return rowsAffected > 0;
}
}