using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public static class ProductsMapper
{
    public static Product ProductToEnity(Product_CreateDto product)
    {
        return new Product
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ProductTypeId = product.ProductTypeId
        };
    }

    public static Product ProductToEnity(Product_UpdateDto product)
    {
        return new Product
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Stock = product.Stock,
            Price = product.Price,
            ProductTypeId = product.ProductTypeId
        };
    }

    public static Product_Dto ProductToDto(Product product)
    {
        return new Product_Dto
        {
            Id = product.Id, 
            Name = product.Name,
            Description = product.Description,
            Stock = product.Stock,
            Price = product.Price,
            ProductTypeId = product.ProductTypeId
        };
    }

    public static ProductType ProductTypeToEnity(ProductType_CreateDto productType)
    {
        return new ProductType
        {
            Name = productType.Name,
            Description = productType.Description
        };
    }

    public static ProductType ProductTypeToEnity(ProductType_UpdateDto productType)
    {
        return new ProductType
        {
            Id = productType.Id,
            Name = productType.Name,
            Description = productType.Description
        };
    }

    public static ProductType_Dto ProductTypeToDto(ProductType productType)
    {
        return new ProductType_Dto
        {
            Id = productType.Id,
            Name = productType.Name,
            Description = productType.Description
        };
    }


}