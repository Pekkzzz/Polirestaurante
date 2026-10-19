using PoliRestaurante.Models.Entity;

public class ProductTypeService_Response
{
    public bool Response;
    public string Description;
    public ProductType? productType;
    public List<ProductType>? productTypes;

    public ProductTypeService_Response(bool response, string description, ProductType? productTypeObject, List<ProductType>? types)
    {
        Response = response;
        Description = description;
        productType = productTypeObject;
        productTypes = types;
    }

    public ProductTypeService_Response()
    {
        Response = false;
        Description = "";
        productType = null;
        productTypes = null;
    }
}