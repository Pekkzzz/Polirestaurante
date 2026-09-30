using PoliRestaurante.Models;

public interface IUserService
{
    public UserService_Response Save();
    public UserService_Response GetByID(int id);
    public UserService_Response Create(User_CreateDto newUser);
    public UserService_Response Update(User_UpdateDto newUser);
    public UserService_Response Detele(int id);
}