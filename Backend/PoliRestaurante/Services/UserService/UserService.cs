using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PoliRestaurante.Models.Entity;
public class UserService : IUserService
{   
    //Repositories
    private readonly IUserRepository _repository;

    //Services
    private readonly IUserRoleService _userRoleService;

    private string? secretKey;

    public UserService(IUserRepository Repository, IUserRoleService userRoleService, IConfiguration configuration)
    {
        _repository = Repository;
        _userRoleService = userRoleService;
        secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
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
            response.Description = "User not Found";
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
        bool userExists = _repository.UserExists(user_UpdateDto.ID);
        UserService_Response userService_Response = new UserService_Response();
        if (!userExists)
        {
            userService_Response.Response = false;
            userService_Response.Description = "User dont exists";
            return userService_Response;
        }
        User user = UsersMapper.UserToEnity(user_UpdateDto);

        UserRoleService_Response userRoleService_Response = _userRoleService.GetByID(user_UpdateDto.UserRoleId);
        if (!userRoleService_Response.Response)
        {
            userService_Response.Response = false;
            userService_Response.Description = userRoleService_Response.Description;
            userService_Response.user = null;
            return userService_Response;
        }
        
        
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
        UserService_Response response = GetByID(id);

        if (!response.Response)
        {
            return response;
        }

        if (response.user == null)
        {
            response.Response = false;
            response.Description = "Something is wrong";
            return response;
        }

        if (_repository.DeleteUser(response.user))
        {
            response.Response = true;
            response.Description = "User deleted successfully";
            response.user = null;
            return response;
        }

        response.Response = false;
        response.Description = "User can't be deleted";
        response.user = null;
        return response;
        
    }

    // LOGIN & REGISTER
    public async Task<UserService_Response> Register(User_CreateDto user_CreateDto)
    {
        // to use this method needs to install in project the bcrypt
        // dotnet add package BCrypt.Net-Next --version 4.2.0
        var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(user_CreateDto.Password);

        User user = new User
        {
        Username = user_CreateDto.Username ?? "No Username",
        Name = user_CreateDto.Name,
        PasswordHash = encryptedPassword,
        Email = user_CreateDto.Email

        };

        if (string.IsNullOrEmpty(user_CreateDto.Username))
        {
            return new UserService_Response
            {
              Response = false,
              Description = "Username is null or empty",
              user = null  
            };
        }

        if (_repository.UserExists(user_CreateDto.Username))
        {
            return new UserService_Response
            {
              Response = false,
              Description = "Username already exists",
              user = null  
            };
        }

        User_CreateDto tocreateDto = new User_CreateDto
        {
          Username = user.Username,
          Name = user.Name,
          Password = user.PasswordHash,
          Email = user.Email
        };

        UserService_Response userService_CreationResponse = Create(tocreateDto);
        UserService_Response userService_Response = new UserService_Response();

        if(!userService_CreationResponse.Response || userService_CreationResponse.user == null )
        {
            userService_Response.Response = false;
            userService_Response.Description = userService_CreationResponse.Description;
            userService_Response.user = null;
            return userService_Response;
        }
        else
        {
            userService_Response.Response = true;
            userService_Response.Description = "User Registed Correctly";
            user.PasswordHash = "";
            userService_Response.user = user;
            return userService_Response;
        }
 
    }

    public async Task<User_LoginResponseDto> Login(User_LoginDto user_LoginDto)
    {
        UserService_Response userService_Response = new UserService_Response();
        if (string.IsNullOrEmpty(user_LoginDto.Username))
        {
            userService_Response = new UserService_Response
            {
                Response = false,
                Description = "Username is Required",
                user = null
            };

            return new User_LoginResponseDto()
            {
                userService_Response = userService_Response,
                Token = ""
            };
        }

        User? user = _repository.GetUser(user_LoginDto.Username);
        if (user == null)
        {
            userService_Response = new UserService_Response
            {
                Response = false,
                Description = "User not found",
                user = null
            };

            return new User_LoginResponseDto()
            {
                userService_Response = userService_Response,
                Token = ""
            };
        }

        if (!BCrypt.Net.BCrypt.Verify(user_LoginDto.Password, user.PasswordHash))
        {
            userService_Response = new UserService_Response
            {
                Response = false,
                Description = "Incorrect Credentials",
                user = null
            };

            return new User_LoginResponseDto()
            {
                userService_Response = userService_Response,
                Token = ""
            };
        }

        //JWT
        var handlerToken = new JwtSecurityTokenHandler();
        if (string.IsNullOrWhiteSpace(secretKey))
        {
            throw new InvalidOperationException("Secret key is not configurated");
        }
        var key = Encoding.UTF8.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
             new Claim("id", user.Id.ToString()),
             new Claim("username", user.Username.ToString()),
             new Claim("role_id", user.UserRoleId.ToString())

            }
            ),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handlerToken.CreateToken(tokenDescriptor);
        return new User_LoginResponseDto()
        {
            Token = handlerToken.WriteToken(token),
            userService_Response = new UserService_Response()
            {
                Response = true,
                Description = "Login Succesfully",
                user = new User()
                {
                    Username = user.Username,
                    Name = user.Name,
                    UserRoleId = user.UserRoleId
                }
            }
        };
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
        if (!UtilityInternalTools.EmailValidityCheck(newUser.Email))
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