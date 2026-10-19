using PoliRestaurante.Models.Entity;

public class ProductService_Response
{
    public bool Response;
    public string Description;
    public Product? product;
    public List<Product>? products;

    public ProductService_Response(bool response, string description, Product? productObject, List<Product>? productsObject = null)
    {
        Response = response;
        Description = description;
        product = productObject;
        products = productsObject;
    }

    public ProductService_Response()
    {
        Response = false;
        Description = "";
        product = null;
        products = null;
    }
}