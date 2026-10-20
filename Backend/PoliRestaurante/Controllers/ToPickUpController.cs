using Microsoft.AspNetCore.Mvc;
using PoliRestaurante.Models;

namespace ApiEcommerce.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ToPickUpController : ControllerBase
  {
    private readonly IToPickUpService _service;

    public ToPickUpController(IToPickUpService service)
    {
      _service = service;
    }

    [HttpGet("{id:int}", Name = "GetToPickUpByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetByID(int id)
    {
    ToPickUpService_Response serviceResponse = _service.GetByID(id);
            if (!serviceResponse.Response)
            {
                ModelState.AddModelError("CustomError", serviceResponse.Description);
                return BadRequest(ModelState);
            }
            if (serviceResponse.toPickUp == null)
            {
                return StatusCode(500);
            }

            ToPickUp_Dto toPickUp = new ToPickUp_Dto()
            {
                Id = serviceResponse.toPickUp.Id,

                Address = serviceResponse.toPickUp.Address,

                CreationTime = serviceResponse.toPickUp.CreationTime,

                CompletedTime = serviceResponse.toPickUp.CompletedTime,

                PickupStatusId = serviceResponse.toPickUp.PickupStatusId,

                UserId = serviceResponse.toPickUp.UserId,

                OrderId = serviceResponse.toPickUp.OrderId
            };
            return Ok(toPickUp);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateToPickUp([FromBody] ToPickUp_CreateDto createToPickUpDto)
    {
    if (createToPickUpDto == null)
    {
        return BadRequest(ModelState);
    }
    ToPickUpService_Response serviceResponse = _service.Create(createToPickUpDto);

            if (!serviceResponse.Response || serviceResponse.toPickUp == null)
            {
                ModelState.AddModelError("CustomError", serviceResponse.Description);
                return BadRequest(ModelState);
            }
            ToPickUp_Dto toPickUp = new ToPickUp_Dto()
            {
                Id = serviceResponse.toPickUp.Id,

                Address = serviceResponse.toPickUp.Address,

                CreationTime = serviceResponse.toPickUp.CreationTime,

                CompletedTime = serviceResponse.toPickUp.CompletedTime,

                PickupStatusId = serviceResponse.toPickUp.PickupStatusId,

                UserId = serviceResponse.toPickUp.UserId,

                OrderId = serviceResponse.toPickUp.OrderId
            };
            return CreatedAtRoute("GetToPickUpByID", new { id = toPickUp.Id }, toPickUp);
    }

    [HttpPatch(Name = "UpdateToPickUp")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public IActionResult UpdateToPickUp([FromBody] ToPickUp_UpdateDto updateToPickUpDto)
    {
    if (updateToPickUpDto == null)
    {
        return BadRequest(ModelState);
    }
    ToPickUpService_Response serviceResponse = _service.Update(updateToPickUpDto);

            if (!serviceResponse.Response || serviceResponse.toPickUp == null)
            {
                ModelState.AddModelError("CustomError", serviceResponse.Description);
                return BadRequest(ModelState);
            }
            ToPickUp_Dto toPickUp = new ToPickUp_Dto()
            {
                Id = serviceResponse.toPickUp.Id,

                Address = serviceResponse.toPickUp.Address,

                CreationTime = serviceResponse.toPickUp.CreationTime,

                CompletedTime = serviceResponse.toPickUp.CompletedTime,

                PickupStatusId = serviceResponse.toPickUp.PickupStatusId,

                UserId = serviceResponse.toPickUp.UserId,

                OrderId = serviceResponse.toPickUp.OrderId
            };
            return Ok(toPickUp);
    }
    [HttpDelete("{id:int}", Name = "DeleteToPickUpByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteToPickUp(int id)
    {
    ToPickUpService_Response serviceResponse = _service.Delete(id);
            if (!serviceResponse.Response)
            {
                ModelState.AddModelError("CustomError", serviceResponse.Description);
                return BadRequest(ModelState);
            }
            if (serviceResponse.toPickUp == null)
            {
                return StatusCode(500);
            }

            return Ok();
    }
  }
}