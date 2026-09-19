using Microsoft.AspNetCore.Mvc;
using PoliRestaurante.Models;

namespace ApiEcommerce.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserRoleController : ControllerBase
  {
    private readonly IUserRoleService _service;

    public UserRoleController(IUserRoleService userRoleService)
    {
      _service = userRoleService;
    }

    //This is for get a specific userRole
    [HttpGet("{id:int}", Name = "GetUserRoleByID")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetUserRoleByID(int id)
    {
        UserRoleService_Response userRoleService_Response = _service.GetByID(id);
        if (!userRoleService_Response.Response && userRoleService_Response.userRole == null)
        {
          return NotFound(userRoleService_Response.Description);
          
        }
        return Ok(userRoleService_Response.userRole);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateUserRole([FromBody] UserRole_CreateDto createUserRoleDto)
    {
      if (createUserRoleDto == null)
      {
        return BadRequest(ModelState);
      }
      UserRoleService_Response userRoleService_Response = _service.Create(createUserRoleDto);
      if (!userRoleService_Response.Response && userRoleService_Response.userRole == null)
      {
        ModelState.AddModelError("CustomError", userRoleService_Response.Description);
        return StatusCode(500, ModelState);
      }
      else
      {
        return CreatedAtRoute("GetUserRoleByID", new { id = userRoleService_Response.userRole.Id }, userRoleService_Response.userRole);
      }

      
    }

    [HttpPatch(Name = "UpdateUserRole")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateUser([FromBody] UserRole_UpdateDto updateUserRoleDto)
    {
      if (updateUserRoleDto == null)
      {
        return BadRequest(ModelState);
      }
      UserRoleService_Response serviceResponse = _service.Update(updateUserRoleDto);

      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }


      return Ok(serviceResponse.userRole);
    }

    [HttpDelete("{id:int}", Name = "DeleteUserRoleByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteUser(int id)
    {
      UserRoleService_Response serviceResponse = _service.Detele(id);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      return Ok();
    }

    
  }
}