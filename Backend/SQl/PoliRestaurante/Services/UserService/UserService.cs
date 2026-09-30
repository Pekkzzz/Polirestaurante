using PoliRestaurante.Models;

public class UserService : IUserService
{   
    //Repositories
    private readonly IUserRepository _repository;

    //Services
    private readonly IUserRoleService _userRoleService;

    public UserService(IUserRepository Repository, IUserRoleService userRoleService)
    {
        _repository = Repository;
        _userRoleService = userRoleService;
    }

    public UserService_Response Create(User_CreateDto newUserDto)
    {
        User newUser = UsersMapper.UserToEnity(newUserDto);
        newUser.UserRoleId = 1; //reserved userRolesID for default user creation
        UserService_Response response = ComprobateUser(newUser);
        if (!response.Response)
        {
            return response;
        }

        if (_repository.UserExists(newUser.Username))
        {
            response.Response = false;
            response.Description = "User with this username already exists";
            response.user = null;
            return response;

        }

        if (!_userRoleService.GetByID(newUser.UserRoleId).Response)
        {
            UserRole_CreateDto userRole_CreateDto = new UserRole_CreateDto();
            userRole_CreateDto.Name = "Default";
            userRole_CreateDto.Description = "Default Role for all new users";
            userRole_CreateDto.IsOwner = false;
            userRole_CreateDto.CanEditSystems = false;
            userRole_CreateDto.CanEditOrders = false;
            userRole_CreateDto.CanSetOrdersStatus = false;
            userRole_CreateDto.CanSetToPickUpStatus = false;
            userRole_CreateDto.CanSetProductStock = false;

            if (!_userRoleService.Create(userRole_CreateDto).Response)
            {
                response.Response = false;
                response.Description = "Error Creating Default role for new users, user and role not created";
                response.user = null;
                return response;
            }
            
        }

        newUser.CreationDate = DateTime.Now;
        if (_repository.CreateUser(newUser))
        {
            response.Response = true;
            response.Description = "User Created Succesfully";
            response.user = newUser;
            return response;
        }
        else
        {
            response.Response = false;
            response.Description = "Internal Error: Somethng is wrong when system try to create user";
            response.user = null;
            return response;
        }
        

    }

    public UserService_Response GetByID(int id)
    {
        UserService_Response response = new UserService_Response();

        if (id <= 0)
        {
            response.Description = "User id cant be negative or 0";
            return response;
        }

        User? user = _repository.GetUser(id);
        if (user == null)
        {
            response.Description = "User id cant be finded";
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "User Finded";
            response.user = user;
            return response;
        }
    }

    public UserService_Response Save()
    {
        throw new NotImplementedException();
    }

    public UserService_Response Update(User_UpdateDto user_UpdateDto)
    {
        UserService_Response userService_Response = GetByID(user_UpdateDto.ID);
        if (!userService_Response.Response)
        {
            return userService_Response;
        }

        UserRoleService_Response userRoleService_Response = _userRoleService.GetByID(user_UpdateDto.UserRoleId);
        if (!userRoleService_Response.Response)
        {
            userService_Response.Response = false;
            userService_Response.Description = userRoleService_Response.Description;
            userService_Response.user = null;
            return userService_Response;
        }

        User user = UsersMapper.UserToEnity(user_UpdateDto);

        userService_Response.Response = _repository.UpdateUser(user);
        if (!userService_Response.Response)
        {
            userService_Response.user = null;
            userService_Response.Description = "User cant be updated";
            return userService_Response;
        }
        
        userService_Response = GetByID(user_UpdateDto.ID);
        userService_Response.Description = "User Updated";
        return userService_Response;
    }

    public UserService_Response Detele(int id)
    {
        UserService_Response userService_Response = GetByID(id);
        if (!userService_Response.Response)
        {
            return userService_Response;
        }
        if (userService_Response.user == null)
        {
            userService_Response.Response = false;
            userService_Response.Description = "Something is wrong";
            userService_Response.user = null;
            return userService_Response;
        }
        else
        {
            userService_Response.Response = _repository.DeleteUser(userService_Response.user);
            if (!userService_Response.Response)
            {
                userService_Response.Description = "User Cant Be Deleted";
                userService_Response.user = null;
                return userService_Response;
            }
            else
            {
                userService_Response.Response = false;
                userService_Response.Description = "Something is wrong";
                userService_Response.user = null;
                return userService_Response;
            }
            
        }

        
    }

    //----Utility----
    private UserService_Response ComprobateUser(User newUser)
    {
        if (!UtilityInternalTools.UsernameValidityCheck(newUser.Username))
        {
            return new UserService_Response(false, "Username is empty or invalid", null);
        }
        if (!UtilityInternalTools.NameValidityCheck(newUser.Name))
        {
            return new UserService_Response(false, "Name is empty or is not a valid name", null);
        }
        if (!UtilityInternalTools.EmailValidityCheck(newUser.Name))
        {
            return new UserService_Response(false, "Email is empty or is not a valid email", null);
        }
        if (string.IsNullOrEmpty(newUser.PasswordHash))
        {
            return new UserService_Response(false, "Password is empty or is not a valid password", null);
        }

        return new UserService_Response(true, "User is available", null);
    }
}