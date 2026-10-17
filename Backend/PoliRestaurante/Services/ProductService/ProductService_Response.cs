using PoliRestaurante.Models.Entity;

public class ProductService_Response
{
    public bool Response;
    public string Description;
    public Product? product;

    public ProductService_Response(bool response, string description, Product? productObject)
    {
        Response = response;
        Description = description;
        product = productObject;
    }

    public ProductService_Response()
    {
        Response = false;
        Description = "";
        product = null;
    }
}