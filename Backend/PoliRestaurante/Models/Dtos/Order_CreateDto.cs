
namespace PoliRestaurante.Models;

public partial class Order_CreateDto
{
    public int OrderTypeId { get; set; }

    public int? TableId { get; set; }
    public string username { get; set; }
    public string Password { get; set; }
}