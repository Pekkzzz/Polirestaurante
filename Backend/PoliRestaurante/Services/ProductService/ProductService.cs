using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IProductTypeRepository _productTypeRepository;

    //Services

    public ProductService(IProductRepository Repository, IProductTypeRepository productTypeRepository)
    {
        _repository = Repository;
        _productTypeRepository = productTypeRepository;
    }

    public ProductService_Response Create(Product_CreateDto order_CreateDto)
    {
        ProductService_Response response = new ProductService_Response();

        ProductType? productType = _productTypeRepository.Get(order_CreateDto.ProductTypeId);
        if(productType == null)
        {
            response.Response = false;
            response.Description = "Cannot find product type";
            response.product = null;
            return response;
        }

        Product product = ProductsMapper.ProductToEnity(order_CreateDto);
        response = ComprobateProduct(product);
        if (!response.Response)
        {
            return response;
        }

        if(_repository.Create(product))
        {
            response.Response = true;
            response.Description = "Product Created";
            response.product = product;
            return response;
        }
        else
        {
            response.Response = false;
            response.Description = "Product Cant Be Created by a system error";
            response.product = null;
            return response;
        }

        

    }

    public ProductService_Response GetByID(int id)
    {
        ProductService_Response response = new ProductService_Response();

        if (id <= 0)
        {
            response.Response = false;
            response.Description = "Product id cant be negative or 0";
            response.product = null;
            return response;
        }

        Product? Product = _repository.Get(id);
        if (Product == null)
        {
            response.Response = false;
            response.Description = "Product id cant be finded";
            response.product = null;
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "Product Finded";
            response.product = Product;
            return response;
        }
    }

    public ProductService_Response GetByName(string name)
    {
        ProductService_Response response = new ProductService_Response();

        Product? Product = _repository.Get(name);
        if (Product == null)
        {
            response.Response = false;
            response.Description = "Product name cant be finded";
            response.product = null;
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "Product Finded";
            response.product = Product;
            return response;
        }
    }

    public ProductService_Response GetAll()
    {
        ProductService_Response response = new ProductService_Response();

        List<Product>? products = _repository.GetProducts();
        if (products == null || products.Count < 1)
        {
            response.Response = false;
            response.Description = "Products catn be finded or not exists";
            response.product = null;
            response.products = null;
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "Products Finded";
            response.product = null;
            response.products = products;
            return response;
        }
    }

    public ProductService_Response Update(Product_UpdateDto product_UpdateDto)
    {
        ProductService_Response response = new ProductService_Response();
        if (!_repository.Exists(product_UpdateDto.Id))
        {
            response.Response = false;
            response.Description = "Product id cant be finded";
            response.product= null;
            return response;
        }

        Product product = ProductsMapper.ProductToEnity(product_UpdateDto);
        if (!_repository.Update(product))
        {
            response.Response = false;
            response.Description = "Product cant be updated by a system error";
            response.product = null;
            return response;
        }

        response.Response = true;
        response.Description = "Product updated";
        response.product = product;
        return response;
    }

    public ProductService_Response Delete(int id)
    {
        ProductService_Response response = GetByID(id);
        if (!response.Response || response.product == null)
        {
            response.Response = false;
            response.Description = "Product id cant be finded";
            response.product= null;
            return response;
        }

        if (!_repository.Delete(response.product))
        {
            response.Response = false;
            response.Description = "Product cant be removed by a asystem error";
            response.product = null;
            return response;
        }

        response.Response = true;
        response.Description = "Product removed";
        response.product = null;
        return response;
    }

    //----Utility----
    private ProductService_Response ComprobateProduct(Product newProduct)
    {

        return new ProductService_Response(true, "User Role is available", null);
    }
}