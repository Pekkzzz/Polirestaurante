using Microsoft.AspNetCore.Mvc;
using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

namespace ApiEcommerce.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private readonly IProductService _service;
    private readonly IProductTypeService _productTypeService;

    public ProductController(IProductService service, IProductTypeService productTypeService)
    {
      _service = service;
      _productTypeService = productTypeService;
    }

    [HttpGet("{id:int}", Name = "GetProductByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetByID(int id)
    {
      ProductService_Response serviceResponse = _service.GetByID(id);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      if (serviceResponse.product == null)
      {
        return StatusCode(500);
      }
      
      var order = ProductsMapper.ProductToDto(serviceResponse.product);
      return Ok(order);
    }

    [HttpGet("name/{name}", Name = "GetProductByName")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetByName(string name)
    {
      ProductService_Response serviceResponse = _service.GetByName(name);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      if (serviceResponse.product == null)
      {
        return StatusCode(500);
      }
      
      var order = ProductsMapper.ProductToDto(serviceResponse.product);
      return Ok(order);
    }

    [HttpGet(Name = "GetAll")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
      ProductService_Response serviceResponse = _service.GetAll();
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      if (serviceResponse.products == null)
      {
        return StatusCode(500);
      }
      
      List<Product_Dto> product_Dtos = new List<Product_Dto>();
      foreach (Product product in serviceResponse.products)
      {
        Product_Dto product_Dto = ProductsMapper.ProductToDto(product);
        product_Dtos.Add(product_Dto);
      }
      return Ok(product_Dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateProduct([FromBody] Product_CreateDto createProductDto)
    {
      if (createProductDto == null)
      {
        return BadRequest(ModelState);
      }
      ProductService_Response serviceResponse = _service.Create(createProductDto);

      if (!serviceResponse.Response || serviceResponse.product == null)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }

      Product_Dto order_Dto = ProductsMapper.ProductToDto(serviceResponse.product);
      return CreatedAtRoute("GetProductByID", new { id = order_Dto.Id }, order_Dto);
    }

    [HttpPatch(Name = "UpdateProduct")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateProduct([FromBody] Product_UpdateDto updateProductDto)
    {
      if (updateProductDto == null)
      {
        return BadRequest(ModelState);
      }
      ProductService_Response serviceResponse = _service.Update(updateProductDto);

      if (!serviceResponse.Response || serviceResponse.product == null)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }

      var order = ProductsMapper.ProductToDto(serviceResponse.product);
      return Ok(order);
    }

    [HttpDelete("{id:int}", Name = "DeleteProductByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteProduct(int id)
    {
      ProductService_Response serviceResponse = _service.Detele(id);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      return Ok();
    }

/// <summary>
/// ----------[HttpPost("Login", Name ="LoginUser")] ---------- //
/// </summary>
/// 
    [HttpGet("ProductType/{id:int}", Name = "GetProductTypeByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetProductTypeByID(int id)
    {
      ProductTypeService_Response serviceResponse = _productTypeService.GetByID(id);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      if (serviceResponse.productType == null)
      {
        return StatusCode(500);
      }
      
      var order = ProductsMapper.ProductTypeToDto(serviceResponse.productType);
      return Ok(order);
    }

    [HttpGet("ProductType/name/{name}", Name = "GetProductTypeByName")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetProductTypeByName(string name)
    {
      ProductTypeService_Response serviceResponse = _productTypeService.GetByName(name);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      if (serviceResponse.productType == null)
      {
        return StatusCode(500);
      }
      
      var order = ProductsMapper.ProductTypeToDto(serviceResponse.productType);
      return Ok(order);
    }

    [HttpGet("ProductType", Name = "GetProductTypeAll")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetProductTypeAll()
    {
      ProductTypeService_Response serviceResponse = _productTypeService.GetAll();
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      if (serviceResponse.productTypes == null)
      {
        return StatusCode(500);
      }
      
      List<ProductType_Dto> product_Dtos = new List<ProductType_Dto>();
      foreach (ProductType productType in serviceResponse.productTypes)
      {
        ProductType_Dto product_Dto = ProductsMapper.ProductTypeToDto(productType);
        product_Dtos.Add(product_Dto);
      }
      return Ok(product_Dtos);
    }

    [HttpPost("ProductType")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateProductType([FromBody] ProductType_CreateDto createProductDto)
    {
      if (createProductDto == null)
      {
        return BadRequest(ModelState);
      }
      ProductTypeService_Response serviceResponse = _productTypeService.Create(createProductDto);

      if (!serviceResponse.Response || serviceResponse.productType == null)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }

      ProductType_Dto order_Dto = ProductsMapper.ProductTypeToDto(serviceResponse.productType);
      return CreatedAtRoute("GetProductTypeByID", new { id = order_Dto.Id }, order_Dto);
    }

    [HttpPatch("ProductType",Name = "UpdateProductType")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateProductType([FromBody] ProductType_UpdateDto updateProductDto)
    {
      if (updateProductDto == null)
      {
        return BadRequest(ModelState);
      }
      ProductTypeService_Response serviceResponse = _productTypeService.Update(updateProductDto);

      if (!serviceResponse.Response || serviceResponse.productType == null)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }

      var order = ProductsMapper.ProductTypeToDto(serviceResponse.productType);
      return Ok(order);
    }

    [HttpDelete("ProductType/{id:int}", Name = "DeleteProductTypeByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteProductType(int id)
    {
      ProductTypeService_Response serviceResponse = _productTypeService.Detele(id);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      return Ok();
    }



    
  }
}