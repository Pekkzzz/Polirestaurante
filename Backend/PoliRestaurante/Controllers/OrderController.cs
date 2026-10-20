using Microsoft.AspNetCore.Mvc;
using PoliRestaurante.Models;

namespace ApiEcommerce.Controllers
{
  [Route("api/[controller]")] // EVAN: Requeriment RF 01 Y 04
  [ApiController]
  public class OrderController : ControllerBase
  {
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
      _service = service;
    }

    [HttpGet("{id:int}", Name = "GetOrderByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetByID(int id)
    {
      OrderService_Response serviceResponse = _service.GetByID(id);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      if (serviceResponse.Order == null)
      {
        return StatusCode(500);
      }
      
      var order = OrdersMapper.OrderToDto(serviceResponse.Order);
      return Ok(order);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateOrder([FromBody] Order_CreateDto createOrderDto)
    {
      if (createOrderDto == null)
      {
        return BadRequest(ModelState);
      }
      OrderService_Response serviceResponse = _service.Create(createOrderDto);

      if (!serviceResponse.Response || serviceResponse.Order == null)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }

      Order_Dto order_Dto = OrdersMapper.OrderToDto(serviceResponse.Order);
      return CreatedAtRoute("GetOrderByID", new { id = order_Dto.Id }, order_Dto);
    }

    [HttpPatch(Name = "UpdateOrder")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateOrder([FromBody] Order_UpdateDto updateOrderDto)
    {
      if (updateOrderDto == null)
      {
        return BadRequest(ModelState);
      }
      OrderService_Response serviceResponse = _service.Update(updateOrderDto);

      if (!serviceResponse.Response || serviceResponse.Order == null)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }

      var order = OrdersMapper.OrderToDto(serviceResponse.Order);
      return Ok(order);
    }

    [HttpDelete("{id:int}", Name = "DeleteOrderByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteOrder(int id)
    {
      OrderService_Response serviceResponse = _service.Delete(id);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      return Ok();
    }



    
  }
}