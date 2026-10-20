using System.ComponentModel.DataAnnotations;

public class PickupStatus_CreateDto
{
    [Required(ErrorMessage = "Name is Required")]
    [MaxLength(50, ErrorMessage ="Name cant have more than 50 characters")]
    [MinLength(3, ErrorMessage ="Name cant have less than 3 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is Required")]
    [MaxLength(255, ErrorMessage ="Description cant have more than 255 characters")]
    [MinLength(3, ErrorMessage ="Description cant have less than 3 characters")]
    public string Description { get; set; } = string.Empty;
    
}