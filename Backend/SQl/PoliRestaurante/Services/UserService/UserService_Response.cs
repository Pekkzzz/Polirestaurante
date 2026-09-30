using PoliRestaurante.Models;

public class UserService_Response
{
    public bool Response;
    public string Description;
    public User? user;

    public UserService_Response(bool response, string description, User? userObject)
    {
        Response = response;
        Description = description;
        user = userObject;
    }

    public UserService_Response()
    {
        Response = false;
        Description = "";
        user = null;
    }
}