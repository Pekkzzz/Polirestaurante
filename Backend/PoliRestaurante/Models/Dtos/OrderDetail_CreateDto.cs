public partial class OrderDetail_CreateDto
{
    public string Description { get; set; } = null!;

    public required int ProductId { get; set; }

    public required int OrderId { get; set; }
}