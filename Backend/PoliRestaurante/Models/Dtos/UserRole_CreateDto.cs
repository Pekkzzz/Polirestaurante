public class UserRole_CreateDto
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsOwner { get; set; } = false!;
    public bool CanEditSystems { get; set; } = false!;
    public bool CanEditOrders { get; set; } = false!;
    public bool CanSetOrdersStatus { get; set; } = false!;
    public bool CanSetToPickUpStatus { get; set; } = false!;
    public bool CanSetProductStock { get; set; } = false!;
}