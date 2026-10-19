using PoliRestaurante.Models.Entity;

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
            response.Response = false;
            response.Description = "User Role id cant be negative or 0";
            return response;
        }

        UserRole? userRole = _repository.GetUserRole(id);
        if (userRole == null)
        {
            response.Response = false;
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

    public UserRoleService_Response GetByName(string name)
    {
        UserRoleService_Response response = new UserRoleService_Response();

        if (string.IsNullOrEmpty(name))
        {
            response.Response = false;
            response.Description = "User Role id cant be empty";
            return response;
        }

        UserRole? userRole = _repository.GetUserRole(name);
        if (userRole == null)
        {
            response.Response = false;
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
        UserRoleService_Response response = new UserRoleService_Response();
        if (!_repository.UserRoleExists(userRole_UpdateDto.Id))
        {
            response.Response = false;
            response.Description = "User Role id cant be finded";
            response.userRole= null;
            return response;
        }

        UserRole userRole = UsersMapper.UserRoleToEnity(userRole_UpdateDto);
        if (!_repository.UpdateUserRole(userRole))
        {
            response.Response = false;
            response.Description = "User Role cant be updated by a asystem error";
            response.userRole = null;
            return response;
        }

        response.Response = true;
        response.Description = "User Role updated";
        response.userRole = userRole;
        return response;
    }

    public UserRoleService_Response Detele(int id)
    {
        UserRoleService_Response response = GetByID(id);
        if (!response.Response || response.userRole ==null)
        {
            response.Response = false;
            response.Description = "User Role cant be founded";
            response.userRole = null;
            return response;
        }
        if (!_repository.DeleteUserRole(response.userRole))
        {
            response.Response = false;
            response.Description = "User Cant Be Deleted";
            response.userRole = null;
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "User Deleted";
            response.userRole = null;
            return response;
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