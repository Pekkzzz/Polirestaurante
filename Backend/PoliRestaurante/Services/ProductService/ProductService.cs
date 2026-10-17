using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    //Services

    public ProductService(IProductRepository Repository)
    {
        _repository = Repository;
    }

    public ProductService_Response Create(Product_CreateDto order_CreateDto)
    {
        ProductService_Response response = new ProductService_Response();
        return response;
        

    }

    public ProductService_Response GetByID(int id)
    {
        ProductService_Response response = new ProductService_Response();

        if (id <= 0)
        {
            response.Description = "Product id cant be negative or 0";
            return response;
        }

        Product? Product = _repository.Get(id);
        if (Product == null)
        {
            response.Description = "Product id cant be finded";
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "Product Finded";
            response.product = null;
            return response;
        }
    }

    public ProductService_Response GetByName(string name)
    {
        ProductService_Response response = new ProductService_Response();

        Product? Product = _repository.Get(name);
        if (Product == null)
        {
            response.Description = "Product id cant be finded";
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "Product Finded";
            response.product = null;
            return response;
        }
    }

    public ProductService_Response Update(Product_UpdateDto order_UpdateDto)
    {
        ProductService_Response ProductService_Response = GetByID(order_UpdateDto.Id);
        if (!ProductService_Response.Response)
        {
            return ProductService_Response;
        }

        return ProductService_Response;
    }

    public ProductService_Response Detele(int id)
    {
        ProductService_Response ProductService_Response = GetByID(id);
        return ProductService_Response;

        
    }

    //----Utility----
    private ProductService_Response ComprobateProduct(Product newProduct)
    {
        
        return new ProductService_Response(true, "User Role is available", null);
    }
}