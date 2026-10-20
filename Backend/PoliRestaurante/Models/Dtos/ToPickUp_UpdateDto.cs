public class ToPickUp_UpdateDto
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public DateTime CreationTime { get; set; }

    public DateTime? CompletedTime { get; set; }

    public int PickupStatusId { get; set; }

    public int UserId { get; set; }

    public int OrderId { get; set; }
    
}