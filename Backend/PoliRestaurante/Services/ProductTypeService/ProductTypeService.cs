using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public class ProductTypeService : IProductTypeService
{
    private readonly IProductTypeRepository _repository;

    //Services

    public ProductTypeService(IProductTypeRepository Repository)
    {
        _repository = Repository;
    }

    public ProductTypeService_Response Create(ProductType_CreateDto productType_CreateDto)
    {
        ProductTypeService_Response response = new ProductTypeService_Response();

        ProductType? productType = _repository.Get(productType_CreateDto.Name);
        if(productType != null)
        {
            response.Response = false;
            response.Description = "Already Exists Product Type";
            response.productType = null;
            return response;
        }

        productType = ProductsMapper.ProductTypeToEnity(productType_CreateDto);
        response = ComprobateProductType(productType);
        if (!response.Response || response.productType == null)
        {
            return response;
        }

        if(_repository.Create(productType))
        {
            response.Response = true;
            response.Description = "Product Created";
            response.productType = productType;
            return response;
        }
        else
        {
            response.Response = false;
            response.Description = "Product Cant Be Created by a system error";
            response.productType = null;
            return response;
        }

        

    }

    public ProductTypeService_Response GetByID(int id)
    {
        ProductTypeService_Response response = new ProductTypeService_Response();

        if (id <= 0)
        {
            response.Description = "Product id cant be negative or 0";
            return response;
        }

        ProductType? productType = _repository.Get(id);
        if (productType == null)
        {
            response.Response = false;
            response.Description = "productType id cant be finded";
            response.productType = null;
            response.productTypes = null;
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "productType Finded";
            response.productType = productType;
            response.productTypes = null;
            return response;
        }
    }

    public ProductTypeService_Response GetByName(string name)
    {
        ProductTypeService_Response response = new ProductTypeService_Response();

        ProductType? productType = _repository.Get(name);
        if (productType == null)
        {
            response.Response = false;
            response.Description = "productType name cant be finded";
            response.productType = null;
            response.productTypes = null;
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "productType Finded";
            response.productType = productType;
            response.productTypes = null;
            return response;
        }
    }

    public ProductTypeService_Response GetAll()
    {
        ProductTypeService_Response response = new ProductTypeService_Response();

        List<ProductType> productTypes = _repository.GetProductTypes();
        if (productTypes == null || productTypes.Count < 1)
        {
            response.Response = false;
            response.Description = "No Product Types to show";
            response.productType = null;
            response.productTypes = null;
            return response;
        }

        response.Response = true;
        response.Description = "productTypes Finded";
        response.productType = null;
        response.productTypes = productTypes;
        return response;


    }

    public ProductTypeService_Response Update(ProductType_UpdateDto productType_UpdateDto)
    {
        ProductTypeService_Response ProductTypeService_Response = new ProductTypeService_Response();
        if (!_repository.Exists(productType_UpdateDto.Id))
        {
            ProductTypeService_Response.Response = false;
            ProductTypeService_Response.Description = "Product Type id cant be finded";
            ProductTypeService_Response.productType = null;
            return ProductTypeService_Response;
        }

        ProductTypeService_Response.productType = ProductsMapper.ProductTypeToEnity(productType_UpdateDto);
        if (!_repository.Update(ProductTypeService_Response.productType))
        {
            ProductTypeService_Response = new ProductTypeService_Response()
            {
            Response = false,
            Description = "Product Type cant be updated for a system error",
            productType = null  
            };
            return ProductTypeService_Response;
        }


        ProductTypeService_Response = new ProductTypeService_Response()
        {
          Response = true,
          Description = "Product Type Updated",
          productType = ProductTypeService_Response.productType  
        };
        return ProductTypeService_Response;
    }

    public ProductTypeService_Response Delete(int id)
    {
        ProductTypeService_Response response = GetByID(id);
        if (!response.Response || response.productType == null)
        {
            response.Response = false;
            response.Description = "Product id cant be finded";
            response.productType= null;
            return response;
        }

        if (!_repository.Delete(response.productType))
        {
            response.Response = false;
            response.Description = "Product cant be removed by a asystem error";
            response.productType = null;
            return response;
        }

        response.Response = true;
        response.Description = "Product removed";
        response.productType = null;
        return response;
        
    }

    //----Utility----
    private ProductTypeService_Response ComprobateProductType(ProductType newProductType)
    {

        return new ProductTypeService_Response(true, "Product type is available", newProductType, null);
    }
}