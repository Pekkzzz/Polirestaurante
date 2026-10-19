using PoliRestaurante.Models;

public interface IProductTypeService
{
    public ProductTypeService_Response GetByID(int id);
    public ProductTypeService_Response GetByName(string name);
    public ProductTypeService_Response GetAll();
    public ProductTypeService_Response Create(ProductType_CreateDto newProduct);
    public ProductTypeService_Response Update(ProductType_UpdateDto Product_UpdateDto);
    public ProductTypeService_Response Detele(int id);
}