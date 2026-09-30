using PoliRestaurante.Models;

public interface IUserRoleService
{
    public UserRoleService_Response GetByID(int id);
    public UserRoleService_Response Create(UserRole_CreateDto newUser);
    public UserRoleService_Response Update(UserRole_UpdateDto userRole_UpdateDto);
    public UserRoleService_Response Detele(int id);
}