using System.ComponentModel.DataAnnotations;

public class User_LoginDto
{
    [Required(ErrorMessage = "This field is required")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "This field is required")]
    public required string Password { get; set; }
}