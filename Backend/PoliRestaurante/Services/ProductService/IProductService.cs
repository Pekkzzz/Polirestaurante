using PoliRestaurante.Models;

public interface IProductService
{
    public ProductService_Response GetByID(int id);
    public ProductService_Response GetByName(string name);
    public ProductService_Response GetAll();
    public ProductService_Response Create(Product_CreateDto newProduct);
    public ProductService_Response Update(Product_UpdateDto Product_UpdateDto);
    public ProductService_Response Delete(int id);
}