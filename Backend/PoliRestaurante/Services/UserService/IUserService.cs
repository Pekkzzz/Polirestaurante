using PoliRestaurante.Models;

public interface IUserService
{
    public UserService_Response Save();
    public UserService_Response GetByID(int id);
    public UserService_Response GetByName(string name);
    public UserService_Response Create(User_CreateDto newUser);
    public UserService_Response Update(User_UpdateDto newUser);
    public UserService_Response Delete(int id);
    public Task<User_LoginResponseDto> Login(User_LoginDto user_LoginDto);
    public Task<UserService_Response> Register(User_CreateDto user_CreateDto);
    public UserService_Response ComprobateUser(User_LoginDto user_LoginDto);
}