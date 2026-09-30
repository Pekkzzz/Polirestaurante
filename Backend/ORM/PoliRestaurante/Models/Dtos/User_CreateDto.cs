using System.ComponentModel.DataAnnotations;
public class User_CreateDto
{
    [Required(ErrorMessage = "Username is Required")]
    [MaxLength(255, ErrorMessage ="Username cant have more than 50 characters")]
    [MinLength(3, ErrorMessage ="Username cant have less than 3 characters")]
    public string Username {get; set;} = string.Empty;

    [Required(ErrorMessage = "Name is Required")]
    [MinLength(3, ErrorMessage ="Name cant have less than 3 characters")]
    public string Name {get; set;} = string.Empty;

    [Required(ErrorMessage = "Email is Required")]
    [MinLength(10, ErrorMessage ="Email cant have less than 10 characters")]
    public string Email {get; set;} = string.Empty;

    [Required(ErrorMessage = "Password Is Required")]
    [MaxLength(50, ErrorMessage ="Password cant have more than 50 characters")]
    [MinLength(5, ErrorMessage ="Username cant have less than 5 characters")]
    public string Password {get; set;} = string.Empty;
}