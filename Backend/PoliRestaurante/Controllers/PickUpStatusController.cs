using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PickUpStatusController : ControllerBase
  {
    private readonly IPickUpStatusService _service;

    public PickUpStatusController(IPickUpStatusService service)
    {
      _service = service;
    }

    [HttpGet("{id:int}", Name = "GetPickUpStatusByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]


    public IActionResult GetByID(int id)
    {
      PickUpStatusService_Response serviceResponse = _service.GetByID(id);
            if (!serviceResponse.Response)
            {
                ModelState.AddModelError("CustomError", serviceResponse.Description);
                return BadRequest(ModelState);
            }
            if (serviceResponse.pickUpStatus == null)
            {
                return StatusCode(500);
            }

            PickUpStatus_Dto pickUpStatus = new PickUpStatus_Dto()
            {
                Id = serviceResponse.pickUpStatus.Id,

                Name = serviceResponse.pickUpStatus.Name,

                Description = serviceResponse.pickUpStatus.Description
            };
            return Ok(pickUpStatus);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreatePickUpStatus([FromBody] PickupStatus_CreateDto createPickUpStatusDto)

    {
      if (createPickUpStatusDto == null)
      {
        return BadRequest(ModelState);
      }

        PickUpStatusService_Response serviceResponse = _service.Create(createPickUpStatusDto);

        if (!serviceResponse.Response || serviceResponse.pickUpStatus == null)
        {
            ModelState.AddModelError("CustomError", serviceResponse.Description);
            return BadRequest(ModelState);
        }


       PickUpStatus_Dto pickUpStatus = new PickUpStatus_Dto()
      {
        Id = serviceResponse.pickUpStatus.Id,

        Name = serviceResponse.pickUpStatus.Name,

        Description = serviceResponse.pickUpStatus.Description

      };
      return CreatedAtRoute("GetPickUpStatusByID", new { id = pickUpStatus.Id }, pickUpStatus);
    }    

    [HttpPatch(Name = "UpdatePickUpStatus")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public IActionResult UpdatePickUpStatus([FromBody] PickUpStatus_UpdateDto updatePickUpStatusDto)
    {
        if (updatePickUpStatusDto == null)
        {
            return BadRequest(ModelState);
        }

        PickUpStatusService_Response serviceResponse = _service.Update(updatePickUpStatusDto);

        if (!serviceResponse.Response || serviceResponse.pickUpStatus == null)
        {
            ModelState.AddModelError("CustomError", serviceResponse.Description);
            return BadRequest(ModelState);
        }

        PickUpStatus_Dto pickUpStatus = new PickUpStatus_Dto()
        {
            Id = serviceResponse.pickUpStatus.Id,

            Name = serviceResponse.pickUpStatus.Name,

            Description = serviceResponse.pickUpStatus.Description
        };
        return Ok(pickUpStatus);
    }

    [HttpDelete("{id:int}", Name = "DeletePickUpStatusByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public IActionResult DeletePickUpStatus(int id)
    {
        PickUpStatusService_Response serviceResponse = _service.Delete(id);

        if (!serviceResponse.Response)
        {
            ModelState.AddModelError("CustomError", serviceResponse.Description);
            return BadRequest(ModelState);
        }
        if (serviceResponse.pickUpStatus == null)
        {
            return StatusCode(500);
        }
        return Ok();

    }
  }
}