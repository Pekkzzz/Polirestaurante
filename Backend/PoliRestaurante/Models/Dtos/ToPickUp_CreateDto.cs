using System.ComponentModel.DataAnnotations;
public class ToPickUp_CreateDto
{
    [Required(ErrorMessage = "Address is Required")]
    [MaxLength(255, ErrorMessage ="Address cant have more than 255 characters")]
    [MinLength(3, ErrorMessage ="Address cant have less than 3 characters")]
    public string Address {get; set;} = string.Empty;

    [Required(ErrorMessage = "PickupStatusId is Required")]
    public int PickupStatusId {get; set;}

    [Required(ErrorMessage = "UserId is Required")]
    public int UserId {get; set;}

    [Required(ErrorMessage = "OrderId is Required")]
    public int OrderId {get; set;}
    
}