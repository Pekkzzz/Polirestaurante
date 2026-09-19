public class User_Dto
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;

    public string Username {get; set; } = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Password {get; set;} = string.Empty;
    public int UserRoleID {get; set;}
    public DateTime CreationDate {get; set;}
}
