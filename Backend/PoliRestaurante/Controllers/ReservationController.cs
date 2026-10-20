using Microsoft.AspNetCore.Mvc;
using PoliRestaurante.Models;

namespace ApiEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}", Name = "GetReservationByID")]
        public IActionResult GetByID(int id)
        {
            var response = _service.GetByID(id);

            if (!response.Response)
            {
                ModelState.AddModelError(
                    "CustomError", response.Description);

                return BadRequest(ModelState);
            }

            if (response.Reservation == null)
            {
                return NotFound();
            }

            var reservation =
                ReservationsMapper.ReservationToDto(response.Reservation);

            return Ok(reservation);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _service.GetAll();

            if (!response.Response)
            {
                ModelState.AddModelError(
                    "CustomError", response.Description);

                return BadRequest(ModelState);
            }

            var reservations = response.Reservations
                .Select(ReservationsMapper.ReservationToDto)
                .ToList();

            return Ok(reservations);
        }

        [HttpPost]
        public IActionResult Create(
            [FromBody] Reservation_CreateDto reservationDto)
        {
            if (reservationDto == null)
            {
                return BadRequest(ModelState);
            }

            var response = _service.Create(reservationDto);

            if (!response.Response || response.Reservation == null)
            {
                ModelState.AddModelError(
                    "CustomError", response.Description);

                return BadRequest(ModelState);
            }

            var reservation =
                ReservationsMapper.ReservationToDto(response.Reservation);

            return CreatedAtRoute(
                "GetReservationByID",
                new { id = reservation.Id },
                reservation);
        }

        [HttpPatch]
        public IActionResult Update(
            [FromBody] Reservation_UpdateDto reservationDto)
        {
            if (reservationDto == null)
            {
                return BadRequest(ModelState);
            }

            var response = _service.Update(reservationDto);

            if (!response.Response || response.Reservation == null)
            {
                ModelState.AddModelError(
                    "CustomError", response.Description);

                return BadRequest(ModelState);
            }

            var reservation =
                ReservationsMapper.ReservationToDto(response.Reservation);

            return Ok(reservation);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var response = _service.Delete(id);

            if (!response.Response)
            {
                ModelState.AddModelError(
                    "CustomError", response.Description);

                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
