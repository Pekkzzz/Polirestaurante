using ApiEcommerce;
using PoliRestaurante.Models;

public class UserRoleService_Response
{
    public bool Response;
    public string Description;
    public UserRole? userRole;

    public UserRoleService_Response(bool response, string description, UserRole? userRoleObject)
    {
        Response = response;
        Description = description;
        userRole = userRoleObject;
    }

    public UserRoleService_Response()
    {
        Response = false;
        Description = "";
        userRole = null;
    }
}