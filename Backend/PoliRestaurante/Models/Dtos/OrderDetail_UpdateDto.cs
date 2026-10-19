public partial class OrderDetail_UpdateDto
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;
    public string Observations { get; set; } = null!;

    public int ProductId { get; set; }

    public int OrderId { get; set; }

    public int OrderDetailStatusId { get; set; }
}