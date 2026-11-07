using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public class UserRoleService_Response
{
    public bool Response;
    public string Description;
    public UserRole? userRole;
    public List<UserRole>? userRoles;

    public UserRoleService_Response(bool response, string description, UserRole? userRoleObject, List<UserRole>? userRolesObject)
    {
        Response = response;
        Description = description;
        userRole = userRoleObject;
        userRoles = userRolesObject;
    }

    public UserRoleService_Response()
    {
        Response = false;
        Description = "";
        userRole = null;
        userRoles = null;
    }
}