using Microsoft.Data.SqlClient;
using PoliRestaurante.Models;

namespace Polirestaurante.Repository;

public class UserRepository : IUserRepository
{
  //private readonly ApplicationDbContext _db;

  private readonly string _connectionString;

  public UserRepository(IConfiguration configuration)
  {
      _connectionString = configuration.GetConnectionString("DefaultConnection");
  }

  /*
  public UserRepository(ApplicationDbContext db)
  {
    _db = db;
  }
  */

  public bool CreateUser(User user)
  {
    using SqlConnection connection = new SqlConnection(_connectionString);

    connection.Open();

    string query = """
        INSERT INTO [User] (Username, Name, Email, PasswordHash, UserRole_ID, CreationDate)
        VALUES (@Username, @Name, @Email, @PasswordHash, @UserRole_ID, @CreationDate)
        """;

    using SqlCommand command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@Username", user.Username);
    command.Parameters.AddWithValue("@Name", user.Name);
    command.Parameters.AddWithValue("@Email", user.Email);
    command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
    command.Parameters.AddWithValue("@UserRole_ID", 1);
    command.Parameters.AddWithValue("@CreationDate", DateTime.Now);

    int rowsAffected = command.ExecuteNonQuery();

    return rowsAffected > 0;
  }

  public User? GetUser(int id)
  {
    using SqlConnection connection = new SqlConnection(_connectionString);

    connection.Open();

    string query = """
        SELECT Id, Username, Name, Email, PasswordHash, UserRole_ID, CreationDate
        FROM [User]
        WHERE Id = @Id
        """;

    using SqlCommand command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@Id", id);

    using SqlDataReader reader = command.ExecuteReader();

    if (reader.Read())
    {
        return new User
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Username = reader.GetString(reader.GetOrdinal("Username")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Email = reader.GetString(reader.GetOrdinal("Email")),
            PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
            UserRoleId = reader.GetInt32(reader.GetOrdinal("UserRole_ID")),
            CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate"))
        };
    }

    return null;
  }

  public bool UpdateUser(User user)
  {
    using SqlConnection connection = new SqlConnection(_connectionString);

    connection.Open();

    string query = """
        UPDATE [User]
        SET Username = @Username,
            Name = @Name,
            Email = @Email,
            PasswordHash = @PasswordHash,
            CreationDate = @CreationDate,
            isDeleted = @IsDeleted,
            DeletedDate = @DeletedDate,
            UserRole_ID = @UserRole_ID
        WHERE Id = @Id
        """;

    using SqlCommand command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@Id", user.Id);
    command.Parameters.AddWithValue("@Username", user.Username);
    command.Parameters.AddWithValue("@Name", user.Name);
    command.Parameters.AddWithValue("@Email", user.Email);
    command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
    command.Parameters.AddWithValue("@CreationDate", user.CreationDate);
    command.Parameters.AddWithValue("@IsDeleted", user.IsDeleted);
    command.Parameters.AddWithValue("@DeletedDate", user.DeletedDate);
    command.Parameters.AddWithValue("@UserRole_ID", user.UserRoleId);


    int rowsAffected = command.ExecuteNonQuery();

    return rowsAffected > 0;
  }
  public bool DeleteUser(User user)
  {
      using SqlConnection connection = new SqlConnection(_connectionString);

      connection.Open();

      string query = """
          DELETE FROM [User]
          WHERE Id = @Id
          """;

      using SqlCommand command = new SqlCommand(query, connection);

      command.Parameters.AddWithValue("@Id", user.Id);

      int rowsAffected = command.ExecuteNonQuery();

      return rowsAffected > 0;
  }
  public bool UserExists(int id)
  {
   // return _db.Users.Any(c => c.Id == id);
   using SqlConnection connection = new SqlConnection(_connectionString);

    connection.Open();

    string query = """
        SELECT COUNT(1)
        FROM [User]
        WHERE Id = @Id
        """;

    using SqlCommand command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@Id", id);

    int count = (int)command.ExecuteScalar();

    return count > 0;
  }

  public bool UserExists(string name)
  {
      using SqlConnection connection = new SqlConnection(_connectionString);

      connection.Open();

      string query = """
          SELECT COUNT(1)
          FROM [User]
          WHERE LOWER(LTRIM(RTRIM(Name))) = LOWER(LTRIM(RTRIM(@Name)))
          """;

      using SqlCommand command = new SqlCommand(query, connection);

      command.Parameters.AddWithValue("@Name", name);

      int count = (int)command.ExecuteScalar();

      return count > 0;
  }

}