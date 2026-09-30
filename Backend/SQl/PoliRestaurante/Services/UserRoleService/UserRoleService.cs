using PoliRestaurante.Models;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _repository;

    public UserRoleService(IUserRoleRepository Repository)
    {
        _repository = Repository;
    }

    public UserRoleService_Response Create(UserRole_CreateDto newUserRoleDto)
    {
        UserRole newUserRole = UsersMapper.UserRoleToEnity(newUserRoleDto);
        UserRoleService_Response response = ComprobateUserRole(newUserRole);
        if (!response.Response)
        {
            return response;
        }

        if (_repository.UserRoleExists(newUserRole.Name))
        {
            response.Response = false;
            response.Description = "User Role with this username already exists";
            response.userRole = null;
            return response;

        }

        if (_repository.CreateUserRole(newUserRole))
        {
            response.Response = true;
            response.Description = "User Role Created Succesfully";
            response.userRole = newUserRole;
            return response;
        }
        else
        {
            response.Response = false;
            response.Description = "Internal Error: Somethng is wrong when system try to create user role";
            response.userRole = null;
            return response;
        }
        

    }

    public UserRoleService_Response GetByID(int id)
    {
        UserRoleService_Response response = new UserRoleService_Response();

        if (id <= 0)
        {
            response.Description = "User Role id cant be negative or 0";
            return response;
        }

        UserRole? userRole = _repository.GetUserRole(id);
        if (userRole == null)
        {
            response.Description = "User Role id cant be finded";
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "User Role Finded";
            response.userRole = userRole;
            return response;
        }
    }

    public UserRoleService_Response Update(UserRole_UpdateDto userRole_UpdateDto)
    {
        UserRoleService_Response userRoleService_Response = GetByID(userRole_UpdateDto.Id);
        if (!userRoleService_Response.Response)
        {
            return userRoleService_Response;
        }

        UserRole userRole = UsersMapper.UserRoleToEnity(userRole_UpdateDto);

        userRoleService_Response.Response = _repository.UpdateUserRole(userRole);
        if (!userRoleService_Response.Response)
        {
            userRoleService_Response.userRole = null;
            userRoleService_Response.Description = "UserRole cant be updated";
            return userRoleService_Response;
        }
        
        userRoleService_Response = GetByID(userRole_UpdateDto.Id);
        userRoleService_Response.Description = "UserRole Updated";
        return userRoleService_Response;
    }

    public UserRoleService_Response Detele(int id)
    {
        UserRoleService_Response userRoleService_Response = GetByID(id);
        if (!userRoleService_Response.Response)
        {
            return userRoleService_Response;
        }
        if (userRoleService_Response.userRole == null)
        {
            userRoleService_Response.Response = false;
            userRoleService_Response.Description = "Something is wrong";
            userRoleService_Response.userRole = null;
            return userRoleService_Response;
        }
        else
        {
            userRoleService_Response.Response = _repository.DeleteUserRole(userRoleService_Response.userRole);
            if (!userRoleService_Response.Response)
            {
                userRoleService_Response.Description = "User Cant Be Deleted";
                userRoleService_Response.userRole = null;
                return userRoleService_Response;
            }
            else
            {
                return userRoleService_Response;
            }
            
        }

        
    }

    //----Utility----
    private UserRoleService_Response ComprobateUserRole(UserRole newUserRole)
    {
        if (!UtilityInternalTools.NameValidityCheck(newUserRole.Name))
        {
            return new UserRoleService_Response(false, "User Role name is null or invalid", null);
        }
        return new UserRoleService_Response(true, "User Role is available", null);
    }
}