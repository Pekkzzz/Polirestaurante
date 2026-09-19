using Microsoft.Data.SqlClient;
using PoliRestaurante.Models;

namespace Polirestaurante.Repository;

public class UserRoleRepository : IUserRoleRepository
{

  //private readonly ApplicationDbContext _db;

  private readonly string _connectionString;

  public UserRoleRepository(IConfiguration configuration)
  {
      _connectionString = configuration.GetConnectionString("DefaultConnection");
  }

  public bool CreateUserRole(UserRole userRole)
  {
    using SqlConnection connection = new SqlConnection(_connectionString);

    connection.Open();

    string query = """
        INSERT INTO UserRole (Name, Description, IsOwner, CanEditSystems, CanEditOrders, CanSetOrdersStatus, CanSetToPickUpStatus, CanSetProductStock)
        VALUES (@Name, @Description, @IsOwner, @CanEditSystems, @CanEditOrders, @CanSetOrdersStatus, @CanSetToPickUpStatus, @CanSetProductStock)
        """;

    using SqlCommand command = new SqlCommand(query, connection);
    
    command.Parameters.AddWithValue("@Name", userRole.Name);
    command.Parameters.AddWithValue("@Description", userRole.Description);
    command.Parameters.AddWithValue("@IsOwner", userRole.IsOwner);
    command.Parameters.AddWithValue("@CanEditSystems", userRole.CanEditSystems);
    command.Parameters.AddWithValue("@CanEditOrders", userRole.CanEditOrders);
    command.Parameters.AddWithValue("@CanSetOrdersStatus", userRole.CanSetOrdersStatus);
    command.Parameters.AddWithValue("@CanSetToPickUpStatus", userRole.CanSetToPickUpStatus);
    command.Parameters.AddWithValue("@CanSetProductStock", userRole.CanSetProductStock);


    int rowsAffected = command.ExecuteNonQuery();

    return rowsAffected > 0;
  }

  public UserRole? GetUserRole(int id)
  {
    using SqlConnection connection = new SqlConnection(_connectionString);

    connection.Open();

    string query = """
        SELECT Id, Name, Description, IsOwner, CanEditSystems, CanEditOrders, CanSetOrdersStatus, CanSetToPickUpStatus, CanSetProductStock
        FROM UserRole
        WHERE Id = @Id
        """;

    using SqlCommand command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@Id", id);

    using SqlDataReader reader = command.ExecuteReader();

    if (reader.Read())
    {
        return new UserRole
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Description = reader.GetString(reader.GetOrdinal("Description")),
            IsOwner = reader.GetBoolean(reader.GetOrdinal("IsOwner")),
            CanEditSystems = reader.GetBoolean(reader.GetOrdinal("CanEditSystems")),
            CanEditOrders = reader.GetBoolean(reader.GetOrdinal("CanEditOrders")),
            CanSetOrdersStatus = reader.GetBoolean(reader.GetOrdinal("CanSetOrdersStatus")),
            CanSetToPickUpStatus = reader.GetBoolean(reader.GetOrdinal("CanSetToPickUpStatus")),
            CanSetProductStock = reader.GetBoolean(reader.GetOrdinal("CanSetProductStock")),
        };
    }

    return null;
  }

  public bool UpdateUserRole(UserRole userRole)
  {
    using SqlConnection connection = new SqlConnection(_connectionString);

    connection.Open();

    string query = """
        UPDATE UserRole
        SET Name = @Name,
            Description = @Description,
            IsOwner = @IsOwner,
            CanEditSystems = @CanEditSystems,
            CanEditOrders = @CanEditOrders,
            CanSetOrdersStatus = @CanSetOrdersStatus,
            CanSetToPickUpStatus = @CanSetToPickUpStatus,
            CanSetProductStock = @CanSetProductStock
        WHERE Id = @Id
        """;

    using SqlCommand command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@Id", userRole.Id);
    command.Parameters.AddWithValue("@Name", userRole.Name);
    command.Parameters.AddWithValue("@Description", userRole.Description);
    command.Parameters.AddWithValue("@IsOwner", userRole.IsOwner);
    command.Parameters.AddWithValue("@CanEditSystems", userRole.CanEditSystems);
    command.Parameters.AddWithValue("@CanEditOrders", userRole.CanEditOrders);
    command.Parameters.AddWithValue("@CanSetOrdersStatus", userRole.CanSetOrdersStatus);
    command.Parameters.AddWithValue("@CanSetToPickUpStatus", userRole.CanSetToPickUpStatus);
    command.Parameters.AddWithValue("@CanSetProductStock", userRole.CanSetProductStock);


    int rowsAffected = command.ExecuteNonQuery();

    return rowsAffected > 0;
  }
  public bool DeleteUserRole(UserRole user)
  {
      using SqlConnection connection = new SqlConnection(_connectionString);

      connection.Open();

      string query = """
          DELETE FROM UserRole
          WHERE Id = @Id
          """;

      using SqlCommand command = new SqlCommand(query, connection);

      command.Parameters.AddWithValue("@Id", user.Id);

      int rowsAffected = command.ExecuteNonQuery();

      return rowsAffected > 0;
  }
  public bool UserRoleExists(int id)
  {
    // return _db.UserRoles.Any(c => c.Id == id);
    using SqlConnection connection = new SqlConnection(_connectionString);

    connection.Open();

    string query = """
        SELECT COUNT(1)
        FROM UserRole
        WHERE Id = @Id
        """;

    using SqlCommand command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@Id", id);

    int count = (int)command.ExecuteScalar();

    return count > 0;
  }

  public bool UserRoleExists(string name)
  {
      using SqlConnection connection = new SqlConnection(_connectionString);

      connection.Open();

      string query = """
          SELECT COUNT(1)
          FROM UserRole
          WHERE LOWER(LTRIM(RTRIM(Name))) = LOWER(LTRIM(RTRIM(@Name)))
          """;

      using SqlCommand command = new SqlCommand(query, connection);

      command.Parameters.AddWithValue("@Name", name);

      int count = (int)command.ExecuteScalar();

      return count > 0;
  }
}