
namespace PoliRestaurante.Models;

public partial class Product_CreateDto
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int ProductTypeId { get; set; }
}