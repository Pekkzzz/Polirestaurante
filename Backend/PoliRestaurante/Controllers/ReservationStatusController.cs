using Microsoft.AspNetCore.Mvc;
using Polirestaurante.Repository;
using PoliRestaurante;
using PoliRestaurante.Models;

namespace ApiEcommerce.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReservationStatusController : ControllerBase
  {
    private readonly IReservationStatusService _service;

    public ReservationStatusController(IReservationStatusService service)
    {
      _service = service;
    }

    [HttpGet("{id:int}", Name = "GetReservationStatusByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetByID(int id)
    {
      ReservationStatusService_Response serviceResponse = _service.GetByID(id);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      if (serviceResponse.reservationStatus == null)
      {
        return StatusCode(500);
      }
      
      var user = ReservationsMapper.ReservationStatusToDto(serviceResponse.reservationStatus);
      return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateUser([FromBody] ReservationStatus_CreateDto createReservationStatusDto)
    {
      if (createReservationStatusDto == null)
      {
        return BadRequest(ModelState);
      }
      ReservationStatusService_Response serviceResponse = _service.Create(createReservationStatusDto);

      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }

      var user = ReservationsMapper.ReservationStatusToEnity(createReservationStatusDto);
      return CreatedAtRoute("GetReservationStatusByID", new { id = user.Id }, user);
    }

    [HttpPatch(Name = "UpdateReservationStatus")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateUser([FromBody] ReservationStatus_UpdateDto updateReservationStatusDto)
    {
      if (updateReservationStatusDto == null)
      {
        return BadRequest(ModelState);
      }
      ReservationStatusService_Response serviceResponse = _service.Update(updateReservationStatusDto);

      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }


      return Ok(serviceResponse.reservationStatus);
    }

    [HttpDelete("{id:int}", Name = "DeleteReservationStatusByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteUser(int id)
    {
      ReservationStatusService_Response serviceResponse = _service.Detele(id);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      return Ok();
    }



    
  }
}