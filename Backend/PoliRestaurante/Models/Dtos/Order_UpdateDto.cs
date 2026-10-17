
namespace PoliRestaurante.Models;

public partial class Order_UpdateDto
{
    public int Id { get; set; }
    public int OrderTypeId { get; set; }

    public int? TableId { get; set; }
}