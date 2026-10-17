public partial class Order_Dto
{
    public int Id { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal Payments { get; set; }

    public decimal TotalBalance { get; set; }

    public DateTime CreationDate { get; set; }

    public int PaymentStatusId { get; set; }

    public int OrderStatusId { get; set; }

    public int OrderTypeId { get; set; }

    public int? TableId { get; set; }
}