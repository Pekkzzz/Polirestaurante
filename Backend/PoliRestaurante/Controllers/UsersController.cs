using Microsoft.AspNetCore.Mvc;
using PoliRestaurante.Models.Entity;
using PoliRestaurante;

namespace ApiEcommerce.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
      _service = service;
    }

    [HttpGet("{id:int}", Name = "GetUserByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetByID(int id)
    {
      UserService_Response serviceResponse = _service.GetByID(id);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      if (serviceResponse.user == null)
      {
        return StatusCode(500);
      }
      
      User_Dto user = UsersMapper.UserToDto(serviceResponse.user);
      return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateUser([FromBody] User_CreateDto createUserDto)
    {
      if (createUserDto == null)
      {
        return BadRequest(ModelState);
      }
      UserService_Response serviceResponse = _service.Create(createUserDto);

      if (!serviceResponse.Response || serviceResponse.user == null)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      
      User_Dto user = UsersMapper.UserToDto(serviceResponse.user);
      return CreatedAtRoute("GetUserByID", new { id = user.Id }, user);
    }

    [HttpPut(Name = "UpdateUser")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateUser([FromBody] User_UpdateDto updateUserDto)
    {
      if (updateUserDto == null)
      {
        return BadRequest(ModelState);
      }
      UserService_Response serviceResponse = _service.Update(updateUserDto);

      if (!serviceResponse.Response || serviceResponse.user == null)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }

      User_Dto user_Dto = UsersMapper.UserToDto(serviceResponse.user);
      return Ok(user_Dto);
    }

    [HttpDelete("{id:int}", Name = "DeleteUserByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteUser(int id)
    {
      UserService_Response serviceResponse = _service.Detele(id);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      return NoContent();
    }



    
  }
}