using Microsoft.AspNetCore.Mvc;
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
      
      var user = UsersMapper.UserToDto(serviceResponse.user);
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

      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }

      var user = UsersMapper.UserToEnity(createUserDto);
      return CreatedAtRoute("GetUserByID", new { id = user.Id }, user);
    }

    [HttpPatch(Name = "UpdateUser")]
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

      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }


      return Ok(serviceResponse.user);
    }

    [HttpDelete("{id:int}", Name = "DeleteUserByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteUser(int id)
    {
      UserService_Response serviceResponse = _service.Detele(id);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      return Ok();
    }



    
  }
}