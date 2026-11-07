using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public class UserService_Response
{
    public bool Response;
    public string Description;
    public User? user;
    public List<User>? users;

    public UserService_Response(bool response, string description, User? userObject, List<User>? usersObject)
    {
        Response = response;
        Description = description;
        user = userObject;
        users = usersObject;
    }

    public UserService_Response()
    {
        Response = false;
        Description = "";
        user = null;
        users = null;
    }
}